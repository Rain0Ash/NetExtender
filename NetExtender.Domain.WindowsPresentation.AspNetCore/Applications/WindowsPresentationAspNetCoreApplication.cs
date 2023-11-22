// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using NetExtender.Domains.Applications.Interfaces;
using NetExtender.Domains.AspNetCore.Server;
using NetExtender.Domains.AspNetCore.Server.Interfaces;
using NetExtender.Domains.WindowsPresentation.Applications;

namespace NetExtender.Domains.WindowsPresentation.AspNetCore.Applications
{
    public abstract class WindowsPresentationAspNetCoreApplicationAbstraction<THost> : WindowsPresentationApplication where THost : class
    {
        protected IAspNetCoreApplicationServer<THost>? Server { get; set; }

        protected WindowsPresentationAspNetCoreApplicationAbstraction()
            : this(new Application())
        {
        }

        protected WindowsPresentationAspNetCoreApplicationAbstraction(Application application)
            : base(application)
        {
        }

        protected IAspNetCoreApplicationServer<THost> Create(THost host)
        {
            return Create<THost>(host);
        }

        protected virtual IAspNetCoreApplicationServer<T> Create<T>(T host) where T : class, THost
        {
            if (host is null)
            {
                throw new ArgumentNullException(nameof(host));
            }

            return AspNetCoreApplicationServerAbstraction<T>.Create(host);
        }
        
        public override Task<IApplication> RunAsync(CancellationToken token)
        {
            return RunAsync(null, null, token);
        }

        public override Task<IApplication> RunAsync(Window? window, CancellationToken token)
        {
            return RunAsync(window, null, token);
        }

        public virtual Task<IApplication> RunAsync(THost? host, CancellationToken token)
        {
            return RunAsync(null, host, token);
        }

        public virtual async Task<IApplication> RunAsync(Window? window, THost? host, CancellationToken token)
        {
            if (host is null)
            {
                return this;
            }

            Server ??= Create(host);
            Server.Start();
            await base.RunAsync(window, token).ConfigureAwait(false);
            return this;
        }

        public override void Shutdown(Int32 code)
        {
            try
            {
                Server?.Stop();
            }
            finally
            {
                base.Shutdown(code);
            }
        }
    }
    
    public class WindowsPresentationAspNetCoreApplication<TApplication> : WindowsPresentationAspNetCoreApplication where TApplication : Application, new()
    {
        public WindowsPresentationAspNetCoreApplication()
            : base(new TApplication())
        {
        }

        public WindowsPresentationAspNetCoreApplication(TApplication application)
            : base(application)
        {
        }
    }

    public class WindowsPresentationAspNetCoreApplication : WindowsPresentationAspNetCoreApplicationAbstraction<IHost>
    {
        public WindowsPresentationAspNetCoreApplication()
        {
        }
        
        public WindowsPresentationAspNetCoreApplication(Application application)
            : base(application)
        {
        }
    }
    
    public class WindowsPresentationAspNetCoreWebApplication<TApplication> : WindowsPresentationAspNetCoreWebApplication where TApplication : Application, new()
    {
        public WindowsPresentationAspNetCoreWebApplication()
            : base(new TApplication())
        {
        }

        public WindowsPresentationAspNetCoreWebApplication(TApplication application)
            : base(application)
        {
        }
    }

    public class WindowsPresentationAspNetCoreWebApplication : WindowsPresentationAspNetCoreApplicationAbstraction<IWebHost>
    {
        public WindowsPresentationAspNetCoreWebApplication()
        {
        }
        
        public WindowsPresentationAspNetCoreWebApplication(Application application)
            : base(application)
        {
        }
    }
}