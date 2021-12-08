using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using NetExtender.Types.Events;
using NetExtender.Types.Interprocess.Interfaces;
using NetExtender.Utilities.Types;
using ProtoBuf;

namespace NetExtender.Types.Interprocess
{
    public sealed class InterprocessMessageBus : IInterprocessMessageBus
    {
        public static IInterprocessMessageBus Fake
        {
            get
            {
                return FakeInterprocessMessageBus.Instance;
            }
        }
        
        public static TimeSpan DefaultMinimumMessageAge
        {
            get
            {
                return Time.Second.Half;
            }
        }

        private Guid InstanceId { get; } = Guid.NewGuid();
        private TimeSpan MessageAge { get; }
        private IInterprocessMemoryMappedFile? MappedFile { get; set; }
        private ConcurrentQueue<InterprocessLogEntry> ReceivedMessagesQueue { get; } = new ConcurrentQueue<InterprocessLogEntry>();

        private Object MessageReaderLock { get; } = ConcurrentUtilities.Synchronization;
        private Object HandlerTaskLock { get; } = ConcurrentUtilities.Synchronization;
        private Object HandlerLock { get; } = ConcurrentUtilities.Synchronization;
        private Int64 LastEntryId { get; set; }

        private InterprocessMessageStatistics Statistics { get; } = new InterprocessMessageStatistics();

        private IReadOnlyList<Task> HandlerTasks { get; set; } = new List<Task>();

        public event EventHandler<TypeHandledEventArgs<Byte[]>> MessageReceived = null!;

        public Int64 SendedMessages
        {
            get
            {
                return Statistics.Published;
            }
        }

        public Int64 ReceivedMessages
        {
            get
            {
                return Statistics.Received;
            }
        }

        static InterprocessMessageBus()
        {
            Serializer.PrepareSerializer<InterprocessLogBook>();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="InterprocessMessageBus"/> class.
        /// </summary>
        /// <param name="name">A unique system wide name of this message bus, internal primitives will be prefixed before use</param>
        public InterprocessMessageBus(String name)
            : this(new InterprocessMemoryMappedFile(name))
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="InterprocessMessageBus"/> class.
        /// </summary>
        /// <param name="name">A unique system wide name of this message bus, internal primitives will be prefixed before use</param>
        /// <param name="age">The minimum amount of time messages are required to live before removal from the file, default is half a second</param>
        public InterprocessMessageBus(String name, TimeSpan age)
            : this(new InterprocessMemoryMappedFile(name), age)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="InterprocessMessageBus"/> class.
        /// </summary>
        /// <param name="interprocess">
        /// An instance of a <see cref="IInterprocessMemoryMappedFile"/> that will be used to transmit messages.
        /// The file should be larger than the size of all messages that can be expected to be transmitted, including message overhead, per half second.
        /// </param>
        public InterprocessMessageBus(IInterprocessMemoryMappedFile interprocess)
            : this(interprocess, DefaultMinimumMessageAge)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="InterprocessMessageBus"/> class.
        /// </summary>
        /// <param name="interprocess">
        /// An instance of a <see cref="IInterprocessMemoryMappedFile"/> that will be used to transmit messages.
        /// The file should be larger than the size of all messages that can be expected to be transmitted, including message overhead, per minMessageAge.
        /// </param>
        /// <param name="age">The minimum amount of time messages are required to live before removal from the file, default is half a second</param>
        public InterprocessMessageBus(IInterprocessMemoryMappedFile interprocess, TimeSpan age)
        {
            MappedFile = interprocess ?? throw new ArgumentNullException(nameof(interprocess));
            MessageAge = age;

            interprocess.Changed += WhenChanged;

            LastEntryId = DeserializeLogBook(interprocess.Read()).LastId;
        }

        /// <summary>
        /// Publishes a message to the message bus as soon as possible in a background task
        /// </summary>
        /// <param name="message"></param>
        public Task SendMessageAsync(Byte[] message)
        {
            if (MappedFile is null)
            {
                throw new ObjectDisposedException(nameof(MappedFile));
            }

            if (message is null || message.Length == 0)
            {
                throw new ArgumentException(@"Message can not be empty", nameof(message));
            }

            return SendMessageAsync(new[] { message });
        }

        /// <summary>
        /// Publish a number of messages to the message bus
        /// </summary>
        /// <param name="messages"></param>
        public Task SendMessageAsync(IEnumerable<Byte[]> messages)
        {
            if (MappedFile is null)
            {
                throw new ObjectDisposedException(nameof(MappedFile));
            }

            if (messages is null)
            {
                throw new ArgumentNullException(nameof(messages), @"Message list can not be empty");
            }

            void Internal()
            {
                Queue<InterprocessLogEntry> queue = new Queue<InterprocessLogEntry>(messages.Select(message => new InterprocessLogEntry { Instance = InstanceId, Message = message }));

                while (queue.Count > 0)
                {
                    MappedFile.ReadWrite(data => PublishMessages(data, queue, Time.Millisecond.Hundred));
                }
            }

            return Task.Run(Internal);
        }

        private Byte[] PublishMessages(Byte[] data, Queue<InterprocessLogEntry> queue, TimeSpan timeout)
        {
            if (MappedFile is null)
            {
                throw new ObjectDisposedException(nameof(MappedFile));
            }
            
            InterprocessLogBook book = DeserializeLogBook(data);
            book.TrimEntries(DateTime.UtcNow - MessageAge);
            Int64 size = book.LogSize;

            // Start slot timer after deserializing log so deserialization doesn't starve the slot time
            Stopwatch timer = Stopwatch.StartNew();
            DateTime batch = DateTime.UtcNow;

            // Try to exhaust the publish queue but don't keep a write lock forever
            while (queue.Count > 0 && timer.Elapsed < timeout)
            {
                // Check if the next message will fit in the log
                if (size + InterprocessLogEntry.Overhead + queue.Peek().Message.Length > MappedFile.MaximumFileSize)
                {
                    break;
                }

                // Write the entry to the log
                InterprocessLogEntry entry = queue.Dequeue();
                entry.Id = ++book.LastId;
                entry.Timestamp = batch;
                book.Entries.Add(entry);

                size += InterprocessLogEntry.Overhead + entry.Message.Length;

                // Skip counting empty messages though, they are skipped on the receiving end anyway
                if (entry.Message.Length == 0)
                {
                    continue;
                }

                Statistics.PublishedIncrement();
            }

            // Flush the updated log to the memory mapped file
            using MemoryStream memoryStream = new MemoryStream((Int32)size);
            Serializer.Serialize(memoryStream, book);
            return memoryStream.ToArray();
        }

        internal Task ReadAsync()
        {
            ReceiveMessages();
            HandleReceivedMessages();

            lock (HandlerTaskLock)
            {
                return Task.WhenAll(HandlerTasks.ToArray());
            }
        }

        private void WhenChanged(Object? sender, EventArgs args)
        {
            ReceiveMessages();
            HandleReceivedMessages();
        }

        private void HandleReceivedMessages()
        {
            if (MappedFile is null || Statistics.Handlers > 0)
            {
                return;
            }

            Statistics.HandlersIncrement();

            lock (HandlerTaskLock)
            {
                void Internal()
                {
                    lock (HandlerLock)
                    {
                        Statistics.HandlersDecrement();

                        while (ReceivedMessagesQueue.TryDequeue(out InterprocessLogEntry? entry))
                        {
                            MessageReceived?.Invoke(this, new TypeHandledEventArgs<Byte[]>(entry.Message));
                        }
                    }
                }

                Task task = Task.Run(Internal);
                List<Task> tasks = HandlerTasks.Where(item => item.Status == TaskStatus.Running).ToList();
                tasks.Add(task);

                HandlerTasks = tasks;
            }
        }

        private void ReceiveMessages()
        {
            if (MappedFile is null || Statistics.Receivers > 0)
            {
                return;
            }

            Statistics.ReceiversIncrement();

            lock (MessageReaderLock)
            {
                Statistics.ReceiversDecrement();

                if (MappedFile is null)
                {
                    return;
                }

                InterprocessLogBook book = DeserializeLogBook(MappedFile.Read());
                
                Int64 read = LastEntryId;
                LastEntryId = book.LastId;

                foreach (InterprocessLogEntry entry in book.Entries.Where(entry => entry.Id > read && entry.Instance != InstanceId && entry.Message.Length > 0))
                {
                    ReceivedMessagesQueue.Enqueue(entry);
                    Statistics.ReceivedIncrement();
                }
            }
        }
        
        private static InterprocessLogBook DeserializeLogBook(Byte[] data)
        {
            if (data is null)
            {
                throw new ArgumentNullException(nameof(data));
            }

            if (data.Length == 0)
            {
                return new InterprocessLogBook();
            }

            using MemoryStream stream = new MemoryStream(data);
            return Serializer.Deserialize<InterprocessLogBook>(stream);
        }
        
        /// <summary>
        /// Resets MessagesSent and MessagesReceived counters
        /// </summary>
        public void ResetMetrics()
        {
            Statistics.Reset();
        }
        
        public void Dispose()
        {
            if (MappedFile is null)
            {
                return;
            }
            
            MappedFile.Changed -= WhenChanged;

            lock (MessageReaderLock)
            {
                MappedFile.Dispose();
                MappedFile = null;
            }
        }
    }
}
