// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Linq;
using AspNet.Core.Types.Initializers.Interfaces;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using NetExtender.AspNet.Core.Types.Initializers;
using NetExtender.Domain.AspNetCore.Common;
using NetExtender.Domains.AspNetCore.Applications;
using NetExtender.Domains.View;
using NetExtender.Domains.View.Interfaces;
using NetExtender.Exceptions;

namespace NetExtender.Domains.AspNetCore.View
{
    public class AspNetCoreView : ApplicationView
    {
        protected Action<IAspNetCoreInitializer> Initializer { get; }
        
        public AspNetCoreView(Action<IAspNetCoreInitializer> initializer)
        {
            Initializer = initializer ?? throw new ArgumentNullException(nameof(initializer));
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
            return Host.CreateDefaultBuilder(args).ConfigureWebHostDefaults(builder =>
            {
                builder.UseStartup(AspNetCoreInitializerBuilder.Create);
            });
        }

        protected override IApplicationView Run()
        {
            return Run(CreateHostBuilder().Build());
        }

        protected virtual IApplicationView Run(IHost? host)
        {
            AspNetCoreApplication application = Domain.Current.Application as AspNetCoreApplication ?? throw new InitializeException("Application is not wpf");
            application.Run(host);
            return this;
        }

        protected override IApplicationView Run<T>(T window)
        {
            return Run();
        }
    }
}