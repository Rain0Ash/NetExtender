using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using NetExtender.Events.Args;

namespace NetExtender.Network.IPC.Messaging
{
	public interface ITinyMessageBus
	{
		/// <summary>
		/// Called whenever a new message is received
		/// </summary>
		public event SenderTypeHandler<TypeHandledEventArgs<Byte[]>> MessageReceived;

		/// <summary>
		/// Number of messages that have been published by this message bus
		/// </summary>
		public Int64 MessagesPublished { get; }

		/// <summary>
		/// Number of messages that have been received by this message bus
		/// </summary>
		public Int64 MessagesReceived { get; }

		/// <summary>
		/// Resets MessagesSent and MessagesReceived counters
		/// </summary>
		public void ResetMetrics();

		/// <summary>
		/// Publish a message to the message bus
		/// </summary>
		/// <param name="message"></param>
		public Task PublishAsync(Byte[] message);

		/// <summary>
		/// Publish a number of messages to the message bus
		/// </summary>
		/// <param name="messages"></param>
		public Task PublishAsync(IEnumerable<Byte[]> messages);
	}
}
