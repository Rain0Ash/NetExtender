// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using NetExtender.Domains.Applications;
using NetExtender.Types.Dispatchers.Interfaces;

namespace NetExtender.Domains.AspNetCore.Applications
{
    public class AspNetCoreApplication : Application
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
        
        protected IHost? Context { get; set; }

        public override void Run()
        {
            Run(null);
        }

        public virtual void Run(IHost? host)
        {
            if (host is null)
            {
                return;
            }

            Context = host;
            Context.Run();
        }

        public override void Run<T>(T window)
        {
            Run();
        }

        public override void Shutdown(Int32 code = 0)
        {
            try
            {
                Context?.StopAsync().RunSynchronously();
            }
            finally
            {
                base.Shutdown(code);
            }
        }
    }
}