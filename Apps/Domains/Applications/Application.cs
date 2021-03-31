// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Threading;
using NetExtender.Apps.Domains.Applications.Interfaces;
using NetExtender.GUI;
using NetExtender.GUI.Common.Interfaces;
using NetExtender.Utils.Application;

namespace NetExtender.Apps.Domains.Applications
{
    public abstract class Application : IApplication
    {
        public abstract GUIType GUIType { get; }
        public abstract Dispatcher Dispatcher { get; }
        public abstract ShutdownMode ShutdownMode { get; set; }

        public abstract void Run();
        public abstract void Run<T>(T window) where T : IWindow;

        public virtual void Shutdown(Int32 code = 0)
        {
            ApplicationUtils.Shutdown(Dispatcher, code);
        }
        
        public void Shutdown(Boolean force)
        {
            Shutdown(0, force);
        }

        public void Shutdown(Int32 code, Boolean force)
        {
            if (force)
            {
                Environment.Exit(code);
                return;
            }

            Shutdown(code);
        }

        public Task<Boolean> ShutdownAsync(Int32 code, Int32 milli)
        {
            return ShutdownAsync(code, milli, CancellationToken.None);
        }

        public Task<Boolean> ShutdownAsync(Int32 code, Int32 milli, CancellationToken token)
        {
            return ShutdownAsync(code, milli, false, token);
        }

        public Task<Boolean> ShutdownAsync(Int32 code, Int32 milli, Boolean force)
        {
            return ShutdownAsync(code, milli, force, CancellationToken.None);
        }

        public async Task<Boolean> ShutdownAsync(Int32 code, Int32 milli, Boolean force, CancellationToken token)
        {
            if (milli > 0)
            {
                try
                {
                    await Task.Delay(milli, token).ConfigureAwait(false);
                }
                catch (Exception)
                {
                    return false;
                }
            }

            if (token.IsCancellationRequested)
            {
                return false;
            }
            
            Shutdown(code, force);
            return true;
        }

        public void Restart(Int32 milli = ApplicationUtils.DefaultMilliRestart)
        {
            Restart(milli, CancellationToken.None);
        }

        public void Restart(CancellationToken token)
        {
            Restart(ApplicationUtils.DefaultMilliRestart, token);
        }

        public virtual void Restart(Int32 milli, CancellationToken token)
        {
            ApplicationUtils.Restart(milli, Dispatcher, Shutdown, token);
        }
    }
}