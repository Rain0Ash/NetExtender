// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Linq;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using NetExtender.Domains.AspNetCore.Applications;
using NetExtender.Domains.View;
using NetExtender.Domains.View.Interfaces;
using NetExtender.Exceptions;

namespace NetExtender.Domains.AspNetCore.View
{
    public class AspNetCoreView : ApplicationView
    {
        protected IHost? Context { get; private set; }
        protected Action<IWebHostBuilder>? Builder { get; }
        
        public AspNetCoreView(IHost host)
        {
            Context = host ?? throw new ArgumentNullException(nameof(host));
        }
        
        public AspNetCoreView(Action<IWebHostBuilder> builder)
        {
            Builder = builder ?? throw new ArgumentNullException(nameof(builder));
        }

        protected override void InitializeInternal()
        {
            Domain.ShutdownMode = ApplicationShutdownMode.OnExplicitShutdown;
        }

        protected IHostBuilder CreateHostBuilder()
        {
            return CreateHostBuilder(Arguments.ToArray());
        }

        protected virtual IHostBuilder CreateHostBuilder(String[] args)
        {
            return Host.CreateDefaultBuilder(args).ConfigureWebHostDefaults(Builder!);
        }

        protected override IApplicationView Run()
        {
            return Run(Context ?? CreateHostBuilder().Build());
        }

        protected virtual IApplicationView Run(IHost? host)
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
            
            AspNetCoreApplication application = Domain.Current.Application as AspNetCoreApplication ?? throw new InitializeException("Application is not AspNetCore");
            application.Run(Context);
            return this;
        }

        protected override IApplicationView Run<T>(T window)
        {
            return Run();
        }
    }
}