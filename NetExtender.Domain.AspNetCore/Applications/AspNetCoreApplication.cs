// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using NetExtender.Domains.Applications;
using NetExtender.Domains.Applications.Interfaces;
using NetExtender.Types.Dispatchers.Interfaces;

namespace NetExtender.Domains.AspNetCore.Applications
{
    public abstract class AspNetCoreApplication<T> : Application<T> where T : class?
    {
        public override IDispatcher? Dispatcher
        {
            get
            {
                return null;
            }
        }

        public override ApplicationShutdownMode ShutdownMode
        {
            get
            {
                return ApplicationShutdownMode.OnExplicitShutdown;
            }
            set
            {
            }
        }
    }

    public class AspNetCoreApplication : AspNetCoreApplication<IHost?>
    {
        public override async Task<IApplication> RunAsync(IHost? host, CancellationToken token)
        {
            if (host is null)
            {
                return this;
            }

            try
            {
                Context = host;
                RegisterShutdownToken(token);
                await Context.RunAsync(token).ConfigureAwait(false);
                return this;
            }
            catch (TaskCanceledException)
            {
                Shutdown();
                return this;
            }
        }

        public override void Shutdown(Int32 code)
        {
            try
            {
                Context?.StopAsync().GetAwaiter().GetResult();
            }
            finally
            {
                base.Shutdown(code);
            }
        }
    }
    
    public class AspNetCoreWebApplication : AspNetCoreApplication<IWebHost?>
    {
        public override async Task<IApplication> RunAsync(IWebHost? host, CancellationToken token)
        {
            if (host is null)
            {
                return this;
            }

            try
            {
                Context = host;
                RegisterShutdownToken(token);
                await Context.RunAsync(token).ConfigureAwait(false);
                return this;
            }
            catch (TaskCanceledException)
            {
                Shutdown();
                return this;
            }
        }

        public override void Shutdown(Int32 code)
        {
            try
            {
                Context?.StopAsync().GetAwaiter().GetResult();
            }
            finally
            {
                base.Shutdown(code);
            }
        }
    }
}