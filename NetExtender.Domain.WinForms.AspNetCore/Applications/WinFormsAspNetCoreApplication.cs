// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using NetExtender.Domains.Applications.Interfaces;
using NetExtender.Domains.AspNetCore.Server;
using NetExtender.Domains.AspNetCore.Server.Interfaces;
using NetExtender.Domains.WinForms.Applications;

namespace NetExtender.Domains.WinForms.AspNetCore.Applications
{
    public class WinFormsAspNetCoreApplicationAbstraction<THost> : WinFormsApplication where THost : class
    {
        protected IAspNetCoreApplicationServer<THost>? Server { get; set; }
        
        protected IAspNetCoreApplicationServer<THost> Create(THost host)
        {
            return Create<THost>(host);
        }

        protected virtual IAspNetCoreApplicationServer<T> Create<T>(T host) where T : class
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

        public override Task<IApplication> RunAsync(Form? form, CancellationToken token)
        {
            return RunAsync(form, null, token);
        }

        public virtual Task<IApplication> RunAsync(THost? host, CancellationToken token)
        {
            return RunAsync(null, host, token);
        }

        [STAThread]
        public virtual async Task<IApplication> RunAsync(Form? form, THost? host, CancellationToken token)
        {
            if (host is null)
            {
                return this;
            }

            Server ??= Create(host);
            Server.Start();
            await base.RunAsync(form, token).ConfigureAwait(false);
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
    
    public class WinFormsAspNetCoreApplication : WinFormsAspNetCoreApplicationAbstraction<IHost>
    {
    }
    
    public class WinFormsAspNetCoreWebApplication : WinFormsAspNetCoreApplicationAbstraction<IWebHost>
    {
    }
}