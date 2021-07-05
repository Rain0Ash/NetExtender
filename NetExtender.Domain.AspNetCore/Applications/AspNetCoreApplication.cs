// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using Microsoft.Extensions.Hosting;
using NetExtender.Domains.Applications;
using NetExtender.Domains.Applications.Interfaces;
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

        public override IApplication Run()
        {
            return Run(null);
        }

        public virtual IApplication Run(IHost? host)
        {
            if (host is null)
            {
                return this;
            }

            Context = host;
            Context.Run();
            return this;
        }

        public override IApplication Run<T>(T window)
        {
            return Run();
        }

        public override void Shutdown(Int32 code)
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