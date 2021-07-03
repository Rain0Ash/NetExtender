// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;

namespace NetExtender.Domain.AspNetCore.Common
{
    public class StartupInvoker
    {
        public WebHostBuilderContext? Context { get; }
        public Action<IServiceCollection>? Services { get; }
        public Action<IApplicationBuilder, IWebHostEnvironment> Configuration { get; }

        public StartupInvoker(WebHostBuilderContext? context, Action<IServiceCollection>? services, Action<IApplicationBuilder, IWebHostEnvironment> configuration)
        {
            Context = context;
            Services = services;
            Configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
        }
            
        public void ConfigureServices(IServiceCollection services)
        {
            Services?.Invoke(services);
        }

        public void Configure(IApplicationBuilder application, IWebHostEnvironment environment)
        {
            Configuration.Invoke(application, environment);
        }
    }
}