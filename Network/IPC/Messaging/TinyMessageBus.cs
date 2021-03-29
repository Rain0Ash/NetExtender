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
    public class TinyMessageBus : IDisposable, ITinyMessageBus
    {
        public static ITinyMessageBus Fake { get; } = new FakeTinyMessageBus();
        
        private readonly Boolean _disposeFile;
        private readonly Guid _instanceId = Guid.NewGuid();
        private readonly TimeSpan _minMessageAge;
        private readonly ITinyMemoryMappedFile _memoryMappedFile;
        private readonly ConcurrentQueue<LogEntry> _receivedMessages = new ConcurrentQueue<LogEntry>();

        private readonly Object _messageReaderLock = new Object();
        private readonly Object _handlerTaskLock = new Object();
        private readonly Object _handlerLock = new Object();

        private Boolean _disposed;
        private Int64 _lastEntryId;
        private Int64 _messagesPublished;
        private Int64 _messagesReceived;
        private Int32 _waitingHandlers;
        private Int32 _waitingReceivers;

        private IReadOnlyList<Task> _handlerTasks = new List<Task>();

        /// <summary>
        /// Called whenever a new message is received
        /// </summary>
        public event SenderTypeHandler<TypeHandledEventArgs<Byte[]>> MessageReceived;

        public Int64 MessagesPublished
        {
            get
            {
                return _messagesPublished;
            }
        }

        public Int64 MessagesReceived
        {
            get
            {
                return _messagesReceived;
            }
        }

        public static readonly TimeSpan DefaultMinMessageAge = Time.Second.Half;

        static TinyMessageBus()
        {
            Serializer.PrepareSerializer<LogBook>();
        }

        /// <summary>
        /// Initializes a new instance of the TinyMessageBus class.
        /// </summary>
        /// <param name="name">A unique system wide name of this message bus, internal primitives will be prefixed before use</param>
        public TinyMessageBus(String name)
            : this(new TinyMemoryMappedFile(name), true)
        {
        }

        /// <summary>
        /// Initializes a new instance of the TinyMessageBus class.
        /// </summary>
        /// <param name="name">A unique system wide name of this message bus, internal primitives will be prefixed before use</param>
        /// <param name="minMessageAge">The minimum amount of time messages are required to live before removal from the file, default is half a second</param>
        public TinyMessageBus(String name, TimeSpan minMessageAge)
            : this(new TinyMemoryMappedFile(name), true, minMessageAge)
        {
        }

        /// <summary>
        /// Initializes a new instance of the TinyMessageBus class.
        /// </summary>
        /// <param name="memoryMappedFile">
        /// An instance of a ITinyMemoryMappedFile that will be used to transmit messages.
        /// The file should be larger than the size of all messages that can be expected to be transmitted, including message overhead, per half second.
        /// </param>
        /// <param name="disposeFile">Set to true if the file is to be disposed when this instance is disposed</param>
        public TinyMessageBus(ITinyMemoryMappedFile memoryMappedFile, Boolean disposeFile)
            : this(memoryMappedFile, disposeFile, DefaultMinMessageAge)
        {
        }

        /// <summary>
        /// Initializes a new instance of the TinyMessageBus class.
        /// </summary>
        /// <param name="memoryMappedFile">
        /// An instance of a ITinyMemoryMappedFile that will be used to transmit messages.
        /// The file should be larger than the size of all messages that can be expected to be transmitted, including message overhead, per minMessageAge.
        /// </param>
        /// <param name="disposeFile">Set to true if the file is to be disposed when this instance is disposed</param>
        /// <param name="minMessageAge">The minimum amount of time messages are required to live before removal from the file, default is half a second</param>
        public TinyMessageBus(ITinyMemoryMappedFile memoryMappedFile, Boolean disposeFile, TimeSpan minMessageAge)
        {
            _memoryMappedFile = memoryMappedFile ?? throw new ArgumentNullException(nameof(memoryMappedFile));
            _disposeFile = disposeFile;
            _minMessageAge = minMessageAge;

            memoryMappedFile.FileUpdated += WhenFileUpdated;

            _lastEntryId = DeserializeLogBook(memoryMappedFile.Read()).LastId;
        }

        public void Dispose()
        {
            _memoryMappedFile.FileUpdated -= WhenFileUpdated;

            _disposed = true;

            lock (_messageReaderLock)
            {
                if (_disposeFile && _memoryMappedFile is TinyMemoryMappedFile tinyMemoryMappedFile)
                {
                    tinyMemoryMappedFile.Dispose();
                }
            }
        }

        /// <summary>
        /// Resets MessagesSent and MessagesReceived counters
        /// </summary>
        public void ResetMetrics()
        {
            _messagesPublished = 0;
            _messagesReceived = 0;
        }

        /// <summary>
        /// Publishes a message to the message bus as soon as possible in a background task
        /// </summary>
        /// <param name="message"></param>
        public Task PublishAsync(Byte[] message)
        {
            if (_disposed)
            {
                throw new ObjectDisposedException("Can not publish messages when diposed");
            }

            if (message is null || message.Length == 0)
            {
                throw new ArgumentException(@"Message can not be empty", nameof(message));
            }

            return PublishAsync(new[] { message });
        }

        /// <summary>
        /// Publish a number of messages to the message bus
        /// </summary>
        /// <param name="messages"></param>
        public Task PublishAsync(IEnumerable<Byte[]> messages)
        {
            if (_disposed)
            {
                throw new ObjectDisposedException("Can not publish messages when diposed");
            }

            if (messages is null)
            {
                throw new ArgumentNullException(nameof(messages), @"Message list can not be empty");
            }

            return Task.Run(() =>
            {
                Queue<LogEntry> publishQueue = new Queue<LogEntry>(messages.Select(message => new LogEntry { Instance = _instanceId, Message = message }));

                while (publishQueue.Count > 0)
                {
                    _memoryMappedFile.ReadWrite(data => PublishMessages(data, publishQueue, TimeSpan.FromMilliseconds(100)));
                }
            });
        }

        private Byte[] PublishMessages(Byte[] data, Queue<LogEntry> publishQueue, TimeSpan timeout)
        {
            LogBook logBook = DeserializeLogBook(data);
            logBook.TrimStaleEntries(DateTime.UtcNow - _minMessageAge);
            Int64 logSize = logBook.CalculateLogSize();

            // Start slot timer after deserializing log so deserialization doesn't starve the slot time
            Stopwatch slotTimer = Stopwatch.StartNew();
            DateTime batchTime = DateTime.UtcNow;

            // Try to exhaust the publish queue but don't keep a write lock forever
            while (publishQueue.Count > 0 && slotTimer.Elapsed < timeout)
            {
                // Check if the next message will fit in the log
                if (logSize + LogEntry.Overhead + publishQueue.Peek().Message.Length > _memoryMappedFile.MaxFileSize)
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

                Interlocked.Increment(ref _messagesPublished);
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

            lock (_handlerTaskLock)
            {
                return Task.WhenAll(_handlerTasks.ToArray());
            }
        }

        private void WhenFileUpdated(Object sender, EventArgs args)
        {
            ReceiveMessages();
            HandleReceivedMessages();
        }

        private void HandleReceivedMessages()
        {
            if (_waitingHandlers > 0 || _disposed)
            {
                return;
            }

            Interlocked.Increment(ref _waitingHandlers);

            lock (_handlerTaskLock)
            {
                Task handlerTask = Task.Run(() =>
                {
                    lock (_handlerLock)
                    {
                        Interlocked.Decrement(ref _waitingHandlers);

                        while (_receivedMessages.TryDequeue(out LogEntry entry))
                        {
                            MessageReceived?.Invoke(this, new TypeHandledEventArgs<Byte[]>(entry.Message));
                        }
                    }
                });

                List<Task> runningTasks = _handlerTasks.Where(x => x.Status == TaskStatus.Running).ToList();
                runningTasks.Add(handlerTask);

                _handlerTasks = runningTasks;
            }
        }

        private void ReceiveMessages()
        {
            if (_waitingReceivers > 0 || _disposed)
            {
                return;
            }

            Interlocked.Increment(ref _waitingReceivers);

            lock (_messageReaderLock)
            {
                Interlocked.Decrement(ref _waitingReceivers);

                if (_disposed)
                {
                    return;
                }

                LogBook logBook = DeserializeLogBook(_memoryMappedFile.Read());
                Int64 readFrom = _lastEntryId;
                _lastEntryId = logBook.LastId;

                foreach (LogEntry entry in logBook.Entries.Where(entry => entry.Id > readFrom && entry.Instance != _instanceId && entry.Message.Length != 0))
                {
                    _receivedMessages.Enqueue(entry);

                    Interlocked.Increment(ref _messagesReceived);
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
