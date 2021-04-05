using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using NetExtender.Events.Args;
using NetExtender.Network.IPC.IO;
using NetExtender.Times;
using ProtoBuf;

namespace NetExtender.Network.IPC.Messaging
{
    public class InterprocessMessageBus : IInterprocessMessageBus
    {
        private struct InterprocessMessageStatistics
        {
            private Int64 _published;
            public Int64 Published
            {
                readonly get
                {
                    return _published;
                }
                set
                {
                    _published = value;
                }
            }

            private Int64 _received;
            public Int64 Received
            {
                readonly get
                {
                    return _received;
                }
                set
                {
                    _received = value;
                }
            }

            private Int32 _handlers;
            public Int32 Handlers
            {
                readonly get
                {
                    return _handlers;
                }
                set
                {
                    _handlers = value;
                }
            }

            private Int32 _receivers;
            public Int32 Receivers
            {
                readonly get
                {
                    return _receivers;
                }
                set
                {
                    _receivers = value;
                }
            }

            public Int64 PublishedIncrement()
            {
                Interlocked.Increment(ref _published);
                return Published;
            }
            
            public Int64 PublishedDecrement()
            {
                Interlocked.Decrement(ref _published);
                return Published;
            }
            
            public Int64 ReceivedIncrement()
            {
                Interlocked.Increment(ref _received);
                return Received;
            }
            
            public Int64 ReceivedDecrement()
            {
                Interlocked.Decrement(ref _received);
                return Received;
            }
            
            public Int32 HandlersIncrement()
            {
                Interlocked.Increment(ref _handlers);
                return Handlers;
            }
            
            public Int32 HandlersDecrement()
            {
                Interlocked.Decrement(ref _handlers);
                return Handlers;
            }
            
            public Int32 ReceiversIncrement()
            {
                Interlocked.Increment(ref _receivers);
                return Receivers;
            }
            
            public Int32 ReceiversDecrement()
            {
                Interlocked.Decrement(ref _receivers);
                return Receivers;
            }

            public void Reset()
            {
                Published = 0;
                Received = 0;
            }
        }

        public static IInterprocessMessageBus Fake
        {
            get
            {
                return FakeInterprocessMessageBus.Instance;
            }
        }

        private Boolean DisposeFile { get; }
        private Guid InstanceId { get; } = Guid.NewGuid();
        private TimeSpan MinMessageAge { get; }
        private IInterprocessMemoryMappedFile MemoryMappedFile { get; }
        private ConcurrentQueue<LogEntry> ReceivedMessagesQueue { get; } = new ConcurrentQueue<LogEntry>();

        private Object MessageReaderLock { get; } = new Object();
        private Object HandlerTaskLock { get; } = new Object();
        private Object HandlerLock { get; } = new Object();

        private Boolean Disposed { get; set; }
        private Int64 LastEntryId { get; set; }
        
        private InterprocessMessageStatistics Statistics { get; }

        private IReadOnlyList<Task> HandlerTasks { get; set; } = new List<Task>();

        /// <summary>
        /// Called whenever a new message is received
        /// </summary>
        public event SenderTypeHandler<TypeHandledEventArgs<Byte[]>> MessageReceived;

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

        public static readonly TimeSpan DefaultMinMessageAge = Time.Second.Half;

        static InterprocessMessageBus()
        {
            Serializer.PrepareSerializer<LogBook>();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="InterprocessMessageBus"/> class.
        /// </summary>
        /// <param name="name">A unique system wide name of this message bus, internal primitives will be prefixed before use</param>
        public InterprocessMessageBus(String name)
            : this(new InterprocessMemoryMappedFile(name), true)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="InterprocessMessageBus"/> class.
        /// </summary>
        /// <param name="name">A unique system wide name of this message bus, internal primitives will be prefixed before use</param>
        /// <param name="minMessageAge">The minimum amount of time messages are required to live before removal from the file, default is half a second</param>
        public InterprocessMessageBus(String name, TimeSpan minMessageAge)
            : this(new InterprocessMemoryMappedFile(name), true, minMessageAge)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="InterprocessMessageBus"/> class.
        /// </summary>
        /// <param name="memoryMappedFile">
        /// An instance of a <see cref="IInterprocessMemoryMappedFile"/> that will be used to transmit messages.
        /// The file should be larger than the size of all messages that can be expected to be transmitted, including message overhead, per half second.
        /// </param>
        /// <param name="disposeFile">Set to true if the file is to be disposed when this instance is disposed</param>
        public InterprocessMessageBus(IInterprocessMemoryMappedFile memoryMappedFile, Boolean disposeFile)
            : this(memoryMappedFile, disposeFile, DefaultMinMessageAge)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="InterprocessMessageBus"/> class.
        /// </summary>
        /// <param name="memoryMappedFile">
        /// An instance of a <see cref="IInterprocessMemoryMappedFile"/> that will be used to transmit messages.
        /// The file should be larger than the size of all messages that can be expected to be transmitted, including message overhead, per minMessageAge.
        /// </param>
        /// <param name="disposeFile">Set to true if the file is to be disposed when this instance is disposed</param>
        /// <param name="minMessageAge">The minimum amount of time messages are required to live before removal from the file, default is half a second</param>
        public InterprocessMessageBus(IInterprocessMemoryMappedFile memoryMappedFile, Boolean disposeFile, TimeSpan minMessageAge)
        {
            MemoryMappedFile = memoryMappedFile ?? throw new ArgumentNullException(nameof(memoryMappedFile));
            DisposeFile = disposeFile;
            MinMessageAge = minMessageAge;

            memoryMappedFile.FileUpdated += WhenFileUpdated;

            LastEntryId = DeserializeLogBook(memoryMappedFile.Read()).LastId;
        }

        public void Dispose()
        {
            MemoryMappedFile.FileUpdated -= WhenFileUpdated;

            Disposed = true;

            lock (MessageReaderLock)
            {
                if (DisposeFile && MemoryMappedFile is InterprocessMemoryMappedFile file)
                {
                    file.Dispose();
                }
            }
        }

        /// <summary>
        /// Resets MessagesSent and MessagesReceived counters
        /// </summary>
        public void ResetMetrics()
        {
            Statistics.Reset();
        }

        /// <summary>
        /// Publishes a message to the message bus as soon as possible in a background task
        /// </summary>
        /// <param name="message"></param>
        public Task SendMessageAsync(Byte[] message)
        {
            if (Disposed)
            {
                throw new ObjectDisposedException("Can not publish messages when diposed");
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
            if (Disposed)
            {
                throw new ObjectDisposedException("Can not publish messages when diposed");
            }

            if (messages is null)
            {
                throw new ArgumentNullException(nameof(messages), @"Message list can not be empty");
            }

            return Task.Run(() =>
            {
                Queue<LogEntry> publishQueue = new Queue<LogEntry>(messages.Select(message => new LogEntry { Instance = InstanceId, Message = message }));

                while (publishQueue.Count > 0)
                {
                    MemoryMappedFile.ReadWrite(data => PublishMessages(data, publishQueue, TimeSpan.FromMilliseconds(100)));
                }
            });
        }

        private Byte[] PublishMessages(Byte[] data, Queue<LogEntry> publishQueue, TimeSpan timeout)
        {
            LogBook logBook = DeserializeLogBook(data);
            logBook.TrimStaleEntries(DateTime.UtcNow - MinMessageAge);
            Int64 logSize = logBook.CalculateLogSize();

            // Start slot timer after deserializing log so deserialization doesn't starve the slot time
            Stopwatch slotTimer = Stopwatch.StartNew();
            DateTime batchTime = DateTime.UtcNow;

            // Try to exhaust the publish queue but don't keep a write lock forever
            while (publishQueue.Count > 0 && slotTimer.Elapsed < timeout)
            {
                // Check if the next message will fit in the log
                if (logSize + LogEntry.Overhead + publishQueue.Peek().Message.Length > MemoryMappedFile.MaxFileSize)
                {
                    break;
                }

                // Write the entry to the log
                LogEntry entry = publishQueue.Dequeue();
                entry.Id = ++logBook.LastId;
                entry.Timestamp = batchTime;
                logBook.Entries.Add(entry);

                logSize += LogEntry.Overhead + entry.Message.Length;

                // Skip counting empty messages though, they are skipped on the receiving end anyway
                if (entry.Message.Length == 0)
                {
                    continue;
                }

                Statistics.PublishedIncrement();
            }

            // Flush the updated log to the memory mapped file
            using MemoryStream memoryStream = new MemoryStream((Int32)logSize);
            Serializer.Serialize(memoryStream, logBook);
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

        private void WhenFileUpdated(Object sender, EventArgs args)
        {
            ReceiveMessages();
            HandleReceivedMessages();
        }

        private void HandleReceivedMessages()
        {
            if (Statistics.Handlers > 0 || Disposed)
            {
                return;
            }

            Statistics.HandlersIncrement();

            lock (HandlerTaskLock)
            {
                Task handlerTask = Task.Run(() =>
                {
                    lock (HandlerLock)
                    {
                        Statistics.HandlersDecrement();

                        while (ReceivedMessagesQueue.TryDequeue(out LogEntry entry))
                        {
                            MessageReceived?.Invoke(this, new TypeHandledEventArgs<Byte[]>(entry.Message));
                        }
                    }
                });

                List<Task> runningTasks = HandlerTasks.Where(x => x.Status == TaskStatus.Running).ToList();
                runningTasks.Add(handlerTask);

                HandlerTasks = runningTasks;
            }
        }

        private void ReceiveMessages()
        {
            if (Statistics.Receivers > 0 || Disposed)
            {
                return;
            }

            Statistics.ReceiversIncrement();

            lock (MessageReaderLock)
            {
                Statistics.ReceiversDecrement();

                if (Disposed)
                {
                    return;
                }

                LogBook logBook = DeserializeLogBook(MemoryMappedFile.Read());
                Int64 readFrom = LastEntryId;
                LastEntryId = logBook.LastId;

                foreach (LogEntry entry in logBook.Entries.Where(entry => entry.Id > readFrom && entry.Instance != InstanceId && entry.Message.Length != 0))
                {
                    ReceivedMessagesQueue.Enqueue(entry);

                    Statistics.ReceivedIncrement();
                }
            }
        }

        private static LogBook DeserializeLogBook(Byte[] data)
        {
            if (data.Length == 0)
            {
                return new LogBook();
            }

            using MemoryStream memoryStream = new MemoryStream(data);
            return Serializer.Deserialize<LogBook>(memoryStream);
        }

        [ProtoContract]
        private class LogBook
        {
            [ProtoMember(1)]
            public Int64 LastId { get; set; }

            [ProtoMember(2)]
            public List<LogEntry> Entries { get; set; } = new List<LogEntry>();

            public Int64 CalculateLogSize()
            {
                return sizeof(Int64) + Entries.Sum(entry => LogEntry.Overhead + entry.Message.Length);
            }

            public void TrimStaleEntries(DateTime cutoffPoint)
            {
                Int32 i = 0;
                for (; i < Entries.Count; i++)
                {
                    if (Entries[i].Timestamp >= cutoffPoint)
                    {
                        break;
                    }
                }
                Entries.RemoveRange(0, i);
            }
        }

        [ProtoContract]
        private class LogEntry
        {
            private static readonly Byte[] EmptyMessage = Array.Empty<Byte>();

            public static Int64 Overhead { get; }

            [ProtoMember(1)]
            public Int64 Id { get; set; }

            [ProtoMember(2)]
            public Guid Instance { get; set; }

            [ProtoMember(3)]
            public DateTime Timestamp { get; set; }

            [ProtoMember(4)]
            public Byte[] Message { get; set; } = EmptyMessage;

            static LogEntry()
            {
                using MemoryStream memoryStream = new MemoryStream();
                Serializer.Serialize(memoryStream, new LogEntry { Id = Int64.MaxValue, Instance = Guid.Empty, Timestamp = DateTime.UtcNow });
                Overhead = memoryStream.Length;
            }
        }
    }
}
