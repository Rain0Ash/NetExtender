// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using Microsoft.Extensions.Hosting;
using NetExtender.Domains.Applications.Interfaces;
using NetExtender.Domains.AspNetCore.Server;
using NetExtender.Domains.AspNetCore.Server.Interfaces;
using NetExtender.Domains.WindowsPresentation.Applications;

namespace NetExtender.Domains.WindowsPresentation.AspNetCore.Applications
{
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

    public class WindowsPresentationAspNetCoreApplication : WindowsPresentationApplication
    {
        protected IAspNetCoreApplicationServer? Server { get; set; }
        
        public WindowsPresentationAspNetCoreApplication()
            : this(new Application())
        {
        }
        
        public WindowsPresentationAspNetCoreApplication(Application application)
            : base(application)
        {
        }

        protected IAspNetCoreApplicationServer Create(IHost host)
        {
            return Create<IHost>(host);
        }

        protected virtual IAspNetCoreApplicationServer Create<THost>(THost host) where THost : class, IHost
        {
            if (host is null)
            {
                throw new ArgumentNullException(nameof(host));
            }

            return new AspNetCoreApplicationServer<THost>(host);
        }
        
        public override Task<IApplication> RunAsync(CancellationToken token)
        {
            return RunAsync(null, null, token);
        }

        public override Task<IApplication> RunAsync(Window? window, CancellationToken token)
        {
            return RunAsync(window, null, token);
        }

        public virtual Task<IApplication> RunAsync(IHost? host, CancellationToken token)
        {
            return RunAsync(null, host, token);
        }

        public virtual async Task<IApplication> RunAsync(Window? window, IHost? host, CancellationToken token)
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
}