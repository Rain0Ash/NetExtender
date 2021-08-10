// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;
using NetExtender.Domains.Applications;
using NetExtender.Domains.Applications.Interfaces;
using NetExtender.Types.Dispatchers.Interfaces;

namespace NetExtender.Domains.AspNetCore.Applications
{
    public class AspNetCoreApplication : Application
    {
        protected IHost? Context { get; set; }
        
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

        public override Task<IApplication> RunAsync(CancellationToken token)
        {
            return RunAsync(null, token);
        }

        public virtual async Task<IApplication> RunAsync(IHost? host, CancellationToken token)
        {
            if (host is null)
            {
                return this;
            }

            Context = host;
            RegisterShutdownToken(token);
            await Context.RunAsync(token);
            return this;
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