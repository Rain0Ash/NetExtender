// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using NetExtender.Domains.AspNetCore.View;
using NetExtender.Domains.AspNetCore.Windows.Service.Applications;
using NetExtender.Domains.View.Interfaces;
using NetExtender.Exceptions;

namespace NetExtender.Domains.Windows.Service.AspNetCore.Views
{
    public class AspNetCoreWindowsServiceView : AspNetCoreView
    {
        protected new IHost? Context { get; set; }
        
        public AspNetCoreWindowsServiceView(IHost host)
            : base(host)
        {
        }
        
        public AspNetCoreWindowsServiceView(Action<IWebHostBuilder> builder)
            : base(builder)
        {
        }

        public AspNetCoreWindowsServiceView(Action<IWebHostBuilder> builder, Boolean initialize)
            : base(builder, initialize)
        {
        }

        protected override IApplicationView Run(IHost? host)
        {
            if (host is null)
            {
                return Run();
            }

            Context ??= host;
            if (host != Context)
            {
                throw new ArgumentException($"{nameof(host)} not reference equals with {nameof(Context)}");
            }
            
            AspNetCoreWindowsServiceApplication application = Domain.Current.Application as AspNetCoreWindowsServiceApplication ?? throw new InitializeException($"{nameof(Domain.Current.Application)} is not {nameof(AspNetCoreWindowsServiceApplication)}");
            application.Run(Context);
            return this;
        }
    }
}