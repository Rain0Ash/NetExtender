// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Linq;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using NetExtender.Domain.AspNetCore.Common;
using NetExtender.Domains.AspNetCore.Applications;
using NetExtender.Domains.View;
using NetExtender.Exceptions;

namespace NetExtender.Domains.AspNetCore.View
{
    public class AspNetCoreView : ApplicationView
    {
        protected Action<WebHostBuilderContext>? Builder { get; }
        protected Action<IServiceCollection>? Services { get; }
        protected Action<IApplicationBuilder, IWebHostEnvironment> Configuration { get; }
        
        public AspNetCoreView(Action<WebHostBuilderContext>? builder, Action<IServiceCollection>? services, Action<IApplicationBuilder, IWebHostEnvironment> configuration)
        {
            Builder = builder;
            Services = services;
            Configuration = configuration;
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
                builder.UseStartup(context =>
                {
                    Builder?.Invoke(context);
                    return new StartupInvoker(context, Services, Configuration);
                });
            });
        }

        protected override void Run()
        {
            Run(CreateHostBuilder().Build());
        }

        protected virtual void Run(IHost? host)
        {
            AspNetCoreApplication application = Domain.Current.Application as AspNetCoreApplication ?? throw new InitializeException("Application is not wpf");
            application.Run(host);
        }

        protected override void Run<T>(T window)
        {
            Run();
        }
    }
}