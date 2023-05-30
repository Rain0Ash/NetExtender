// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;
using NetExtender.AspNetCore.Windows.Services.Utilities;
using NetExtender.Domains.Applications.Interfaces;
using NetExtender.Domains.Service.Applications;
using NetExtender.Windows.Services.Types.Installers;
using NetExtender.Windows.Services.Types.Services;
using NetExtender.Windows.Services.Types.Services.Interfaces;

namespace NetExtender.Domains.AspNetCore.Service.Applications
{
    public class AspNetCoreWindowsServiceApplication : WindowsServiceApplication, IApplication<IHost>
    {
        protected IHost? Host { get; set; }

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

        public override Task<IApplication> RunAsync(CancellationToken token)
        {
            return RunAsync(null, token);
        }

        public virtual IApplication Run(IHost? context)
        {
            return RunAsync(context).GetAwaiter().GetResult();
        }

        public Task<IApplication> RunAsync(IHost? context)
        {
            return RunAsync(context, CancellationToken.None);
        }

        public virtual async Task<IApplication> RunAsync(IHost? host, CancellationToken token)
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