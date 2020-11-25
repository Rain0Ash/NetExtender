// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using NetExtender.Events.Args;

namespace NetExtender.Network.IPC.Messaging
{
    public class FakeTinyMessageBus : ITinyMessageBus
    {
        public event SenderTypeHandler<TypeHandledEventArgs<Byte[]>> MessageReceived;
        
        public Int64 MessagesPublished
        {
            get
            {
                return 0;
            }
        }

        public Int64 MessagesReceived
        {
            get
            {
                return 0;
            }
        }

        internal FakeTinyMessageBus()
        {
        }
        
        public void ResetMetrics()
        {
        }

        public Task PublishAsync(Byte[] message)
        {
            return Task.CompletedTask;
        }

        public Task PublishAsync(IEnumerable<Byte[]> messages)
        {
            return Task.CompletedTask;
        }
    }
}