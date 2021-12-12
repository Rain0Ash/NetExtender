// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Threading;
using System.Threading.Tasks;
using NetExtender.Types.Dispatchers.Interfaces;

namespace NetExtender.Domains.Applications.Interfaces
{
    public interface IApplication
    {
        public Boolean? Elevate { get; }
        public Boolean? IsElevate { get; }
        public IDispatcher? Dispatcher { get; }
        public ApplicationShutdownMode ShutdownMode { get; set; }
        public CancellationToken ShutdownToken { get; }
        
        public IApplication Run();
        public Task<IApplication> RunAsync();
        public Task<IApplication> RunAsync(CancellationToken token);
        public void Shutdown();
        public void Shutdown(Int32 code);
        public void Shutdown(Boolean force);
        public void Shutdown(Int32 code, Boolean force);
        public Task<Boolean> ShutdownAsync();
        public Task<Boolean> ShutdownAsync(CancellationToken token);
        public Task<Boolean> ShutdownAsync(Int32 code);
        public Task<Boolean> ShutdownAsync(Int32 code, CancellationToken token);
        public Task<Boolean> ShutdownAsync(Int32 code, Int32 milli);
        public Task<Boolean> ShutdownAsync(Int32 code, Int32 milli, CancellationToken token);
        public Task<Boolean> ShutdownAsync(Int32 code, Int32 milli, Boolean force);
        public Task<Boolean> ShutdownAsync(Int32 code, Int32 milli, Boolean force, CancellationToken token);
        public void Restart();
        public Task<Boolean> RestartAsync();
        public Task<Boolean> RestartAsync(Int32 milli);
        public Task<Boolean> RestartAsync(CancellationToken token);
        public Task<Boolean> RestartAsync(Int32 milli, CancellationToken token);
    }
}