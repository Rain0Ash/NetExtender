// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Diagnostics.CodeAnalysis;
using System.Threading;
using System.Threading.Tasks;
using NetExtender.Domains.Applications.Interfaces;
using NetExtender.Types.Dispatchers.Interfaces;
using NetExtender.Utilities.Application;

namespace NetExtender.Domains.Applications
{
    [SuppressMessage("ReSharper", "MethodSupportsCancellation")]
    public abstract class Application : IApplication
    {
        public virtual Boolean? Elevate { get; init; }

        public virtual Boolean? IsElevate
        {
            get
            {
                return null;
            }
        }

        public abstract IDispatcher? Dispatcher { get; }
        public abstract ApplicationShutdownMode ShutdownMode { get; set; }

        public virtual CancellationToken ShutdownToken { get; protected set; }

        protected virtual CancellationTokenRegistration RegisterShutdownToken(CancellationToken token)
        {
            ShutdownToken = token;
            return ShutdownToken.Register(Shutdown);
        }

        public virtual IApplication Run()
        {
            return RunAsync().GetAwaiter().GetResult();
        }

        public Task<IApplication> RunAsync()
        {
            return RunAsync(CancellationToken.None);
        }

        public abstract Task<IApplication> RunAsync(CancellationToken token);

        public void Shutdown()
        {
            Shutdown(0);
        }

        public virtual void Shutdown(Int32 code)
        {
            ApplicationUtilities.Shutdown(code);
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

        public Task<Boolean> ShutdownAsync()
        {
            return ShutdownAsync(0);
        }

        public Task<Boolean> ShutdownAsync(CancellationToken token)
        {
            return ShutdownAsync(0, token);
        }

        public Task<Boolean> ShutdownAsync(Int32 code)
        {
            return ShutdownAsync(code, CancellationToken.None);
        }

        public Task<Boolean> ShutdownAsync(Int32 code, CancellationToken token)
        {
            return ShutdownAsync(code, 0, token);
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

        public void Restart()
        {
            RestartAsync(0).GetAwaiter().GetResult();
        }

        public Task<Boolean> RestartAsync()
        {
            return RestartAsync(ApplicationUtilities.DefaultMilliRestart);
        }

        public Task<Boolean> RestartAsync(Int32 milli)
        {
            return RestartAsync(milli, CancellationToken.None);
        }

        public Task<Boolean> RestartAsync(CancellationToken token)
        {
            return RestartAsync(ApplicationUtilities.DefaultMilliRestart, token);
        }

        public virtual Task<Boolean> RestartAsync(Int32 milli, CancellationToken token)
        {
            return ApplicationUtilities.Restart(milli, Dispatcher, Shutdown, token);
        }
    }

    public abstract class Application<T> : Application, IApplication<T> where T : class?
    {
        protected T? Context { get; set; }

        public virtual IApplication Run(T? context)
        {
            return RunAsync(context).GetAwaiter().GetResult();
        }

        public Task<IApplication> RunAsync(T? context)
        {
            return RunAsync(context, CancellationToken.None);
        }

        public override Task<IApplication> RunAsync(CancellationToken token)
        {
            return RunAsync(Context, token);
        }

        public abstract Task<IApplication> RunAsync(T? context, CancellationToken token);
    }
}