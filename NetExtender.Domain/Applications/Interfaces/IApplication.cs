// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Threading;
using System.Threading.Tasks;
using NetExtender.Types.Dispatchers.Interfaces;
using NetExtender.UserInterface.Interfaces;
using NetExtender.Utils.Application;

namespace NetExtender.Domains.Applications.Interfaces
{
    public interface IApplication
    {
        public IDispatcher? Dispatcher { get; }
        public ApplicationShutdownMode ShutdownMode { get; set; }
        
        public IApplication Run();
        public IApplication Run<T>(T window) where T : IWindow;
        public void Shutdown(Int32 code = 0);
        public void Shutdown(Boolean force);
        public void Shutdown(Int32 code, Boolean force);
        public Task<Boolean> ShutdownAsync(Int32 code, Int32 milli);
        public Task<Boolean> ShutdownAsync(Int32 code, Int32 milli, CancellationToken token);
        public Task<Boolean> ShutdownAsync(Int32 code, Int32 milli, Boolean force);
        public Task<Boolean> ShutdownAsync(Int32 code, Int32 milli, Boolean force, CancellationToken token);
        public void Restart(Int32 milli = ApplicationUtils.DefaultMilliRestart);
        public void Restart(CancellationToken token);
        public void Restart(Int32 milli, CancellationToken token);
    }
}