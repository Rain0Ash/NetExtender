using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using NetExtender.Types.Events;

namespace NetExtender.Types.Interprocess.Interfaces
{
    public interface IInterprocessMessageBus : IDisposable
    {
        /// <summary>
        /// Called whenever a new message is received
        /// </summary>
        public event EventHandler<TypeHandledEventArgs<Byte[]>> MessageReceived;

        /// <summary>
        /// Number of messages that have been published by this message bus
        /// </summary>
        public Int64 SendedMessages { get; }

        /// <summary>
        /// Number of messages that have been received by this message bus
        /// </summary>
        public Int64 ReceivedMessages { get; }

        /// <summary>
        /// Publish a message to the message bus
        /// </summary>
        /// <param name="message"></param>
        public Task SendMessageAsync(Byte[] message);

        /// <summary>
        /// Publish a number of messages to the message bus
        /// </summary>
        /// <param name="messages"></param>
        public Task SendMessageAsync(IEnumerable<Byte[]> messages);
        
        /// <summary>
        /// Resets MessagesSent and MessagesReceived counters
        /// </summary>
        public void ResetMetrics();
    }
}
