// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Threading;
using NetExtender.GUI;
using NetExtender.Utils.Application;

namespace NetExtender.Apps.Domains.Applications.Interfaces
{
    public interface IApplication
    {
        public GUIType GUIType { get; }
        public Dispatcher Dispatcher { get; }
        public ShutdownMode ShutdownMode { get; set; }
        
        public void Run();
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