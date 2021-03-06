// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;
using NetExtender.Events.Args;

namespace NetExtender.Network.IPC.Messaging
{
    internal sealed class FakeInterprocessMessageBus : IInterprocessMessageBus
    {
        public static FakeInterprocessMessageBus Instance { get; } = new FakeInterprocessMessageBus();
        
        [SuppressMessage("ReSharper", "ValueParameterNotUsed")]
        public event SenderTypeHandler<TypeHandledEventArgs<Byte[]>> MessageReceived
        {
            add
            {
            }
            remove
            {
            }
        }
        
        public Int64 SendedMessages
        {
            get
            {
                return 0;
            }
        }

        public Int64 ReceivedMessages
        {
            get
            {
                return 0;
            }
        }

        private FakeInterprocessMessageBus()
        {
        }
        
        public void ResetMetrics()
        {
        }

        public Task SendMessageAsync(Byte[] message)
        {
            return Task.CompletedTask;
        }

        public Task SendMessageAsync(IEnumerable<Byte[]> messages)
        {
            return Task.CompletedTask;
        }

        public void Dispose()
        {
        }
    }
}