// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using NetExtender.AspNetCore.Windows.Services.Utilities;
using NetExtender.Domains.Applications.Interfaces;
using NetExtender.Domains.Service.Applications;
using NetExtender.Windows.Services.Types.Installers;
using NetExtender.Windows.Services.Types.Services;
using NetExtender.Windows.Services.Types.Services.Interfaces;

namespace NetExtender.Domains.AspNetCore.Service.Applications
{
    public abstract class AspNetCoreWindowsServiceApplication<T> : WindowsServiceApplication, IApplication<T> where T : class
    {
        protected T? Host { get; set; }

        protected AspNetCoreWindowsServiceApplication()
        {
        }

        protected AspNetCoreWindowsServiceApplication(LazyWindowsServiceInitializer? initializer)
            : base(initializer)
        {
        }

        protected AspNetCoreWindowsServiceApplication(WindowsServiceInstaller? installer)
            : base(installer)
        {
        }

        protected AspNetCoreWindowsServiceApplication(LazyWindowsServiceInitializer? initializer, WindowsServiceInstaller? installer)
            : base(initializer, installer)
        {
        }
        
        public override Task<IApplication> RunAsync(CancellationToken token)
        {
            return RunAsync(null, token);
        }

        public virtual IApplication Run(T? context)
        {
            return RunAsync(context).GetAwaiter().GetResult();
        }

        public Task<IApplication> RunAsync(T? context)
        {
            return RunAsync(context, CancellationToken.None);
        }

        public abstract Task<IApplication> RunAsync(T? host, CancellationToken token);
    }

    public class AspNetCoreWindowsServiceApplication : AspNetCoreWindowsServiceApplication<IHost>
    {
        public AspNetCoreWindowsServiceApplication()
        {
        }

        public AspNetCoreWindowsServiceApplication(LazyWindowsServiceInitializer? initializer)
            : base(initializer)
        {
        }

        public AspNetCoreWindowsServiceApplication(WindowsServiceInstaller? installer)
            : base(installer)
        {
        }

        public AspNetCoreWindowsServiceApplication(LazyWindowsServiceInitializer? initializer, WindowsServiceInstaller? installer)
            : base(initializer, installer)
        {
        }

        public override async Task<IApplication> RunAsync(IHost? host, CancellationToken token)
        {
            if (host is null)
            {
                return this;
            }

            Host = host;
            RegisterShutdownToken(token);
            IWindowsService service = Host.AsService();
            return await RunAsync(service, token).ConfigureAwait(false);
        }

        public override void Shutdown(Int32 code)
        {
            try
            {
                Host?.StopAsync().GetAwaiter().GetResult();
            }
            finally
            {
                base.Shutdown(code);
            }
        }
    }
    
    public class AspNetCoreWindowsServiceWebApplication : AspNetCoreWindowsServiceApplication<IWebHost>
    {
        public AspNetCoreWindowsServiceWebApplication()
        {
        }

        public AspNetCoreWindowsServiceWebApplication(LazyWindowsServiceInitializer? initializer)
            : base(initializer)
        {
        }

        public AspNetCoreWindowsServiceWebApplication(WindowsServiceInstaller? installer)
            : base(installer)
        {
        }

        public AspNetCoreWindowsServiceWebApplication(LazyWindowsServiceInitializer? initializer, WindowsServiceInstaller? installer)
            : base(initializer, installer)
        {
        }

        public override async Task<IApplication> RunAsync(IWebHost? host, CancellationToken token)
        {
            if (host is null)
            {
                return this;
            }

            Host = host;
            RegisterShutdownToken(token);
            IWindowsService service = Host.AsService();
            return await RunAsync(service, token).ConfigureAwait(false);
        }

        public override void Shutdown(Int32 code)
        {
            try
            {
                Host?.StopAsync().GetAwaiter().GetResult();
            }
            finally
            {
                base.Shutdown(code);
            }
        }
    }
}