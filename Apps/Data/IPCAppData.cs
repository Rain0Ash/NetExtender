// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;
using NetExtender.Apps.Data.Common;
using NetExtender.Apps.Data.Interfaces;
using NetExtender.Crypto;
using NetExtender.Events.Args;
using NetExtender.Network.IPC.Messaging;

namespace NetExtender.Apps.Data
{
    public class IPCAppData : AppDataEx, IIPCAppData
    {
        public Int64 SendedMessages
        {
            get
            {
                return _bus.MessagesPublished;
            }
        }

        public Int64 ReceivedMessages
        {
            get
            {
                return _bus.MessagesReceived;
            }
        }

        private readonly Boolean _busCreated;
        
        private readonly ITinyMessageBus _bus;
        public event SenderTypeHandler<TypeHandledEventArgs<Byte[]>> MessageReceived
        {
            add
            {
                if (_bus is null)
                {
                    return;
                }
                
                _bus.MessageReceived += value;
            }
            remove
            {
                if (_bus is null)
                {
                    return;
                }
                
                _bus.MessageReceived -= value;
            }
        }

        private readonly Mutex _mutex;

        public Boolean AlreadyStarted
        {
            get
            {
                try
                {
                    return !_mutex.WaitOne(0);
                }
                catch (AbandonedMutexException)
                {
                    return false;
                }
            }
        }

        public IPCAppData(AppVersion version, AppStatus status = AppStatus.Release, AppBranch branch = AppBranch.Master)
            : this(Process.GetCurrentProcess().ProcessName, version, status, branch)
        {
        }

        public IPCAppData(String name, AppVersion version, AppStatus status = AppStatus.Release, AppBranch branch = AppBranch.Master, ITinyMessageBus bus = null)
            : this(name, ToShortName(name), version, status, branch, bus)
        {
        }
        
        public IPCAppData(String name, String sname, AppVersion version, AppStatus status = AppStatus.Release, AppBranch branch = AppBranch.Master, ITinyMessageBus bus = null)
            : base(name, sname, version, status, branch)
        {
            if (bus is null)
            {
                bus = new TinyMessageBus(AppName.Hashing().ToString());
                _busCreated = true;
            }
            else
            {
                _busCreated = false;
            }

            _bus = bus;
            
            _mutex = new Mutex(true, AppName);
        }
        
        public Task SendMessageAsync(Byte[] message)
        {
            return _bus.PublishAsync(message);
        }

        public Task SendMessageAsync(IEnumerable<Byte[]> message)
        {
            return _bus.PublishAsync(message);
        }

        public void ResetMetrics()
        {
            _bus.ResetMetrics();
        }
        
        public override void Dispose()
        {
            try
            {
                if (_busCreated)
                {
                    (_bus as TinyMessageBus)?.Dispose();
                }
                
                _mutex.ReleaseMutex();
                _mutex.Dispose();
            }
            catch (Exception)
            {
                // ignored
            }
        }
    }
}