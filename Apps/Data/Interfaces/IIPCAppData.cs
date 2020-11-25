// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using NetExtender.Events.Args;

namespace NetExtender.Apps.Data.Interfaces
{
    public interface IIPCAppData : IAppDataEx
    {
        public event SenderTypeHandler<TypeHandledEventArgs<Byte[]>> MessageReceived;
        public Int64 SendedMessages { get; }
        public Int64 ReceivedMessages { get; }
        
        public Boolean AlreadyStarted { get; }

        public Task SendMessageAsync(Byte[] message);

        public Task SendMessageAsync(IEnumerable<Byte[]> message);

        public void ResetMetrics();
    }
}