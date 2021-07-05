// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using AspNet.Core.Types.Initializers.Interfaces;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using NetExtender.Exceptions;

namespace NetExtender.AspNet.Core.Types.Initializers
{
    public class AspNetCoreInitializerBuilder
    {
        public static AspNetCoreInitializerBuilder Create(WebHostBuilderContext context)
        {
            return new AspNetCoreInitializerBuilder(context);
        }
        
        private WebHostBuilderContext Context { get; }
        private IServiceCollection Services { get; set; } = null!;
        private IApplicationBuilder Application { get; set; } = null!;
        private IWebHostEnvironment Environment { get; set; } = null!;

        public AspNetCoreInitializerBuilder(WebHostBuilderContext context)
        {
            Context = context ?? throw new ArgumentNullException(nameof(context));
        }
        
        public void ConfigureServices(IServiceCollection services)
        {
            Services = services;
        }

        public void Configure(IApplicationBuilder application, IWebHostEnvironment environment)
        {
            Application = application;
            Environment = environment;
        }

        public virtual IAspNetCoreInitializer Build()
        {
            return new AspNetCoreInitializer(
                Application ?? throw new InitializeException($"{nameof(Application)} not initialized", nameof(Application)),
                Environment ?? throw new InitializeException($"{nameof(Environment)} not initialized", nameof(Environment)),
                Services ?? throw new InitializeException($"{nameof(Services)} not initialized", nameof(Services)),
                Context.Configuration ?? throw new InitializeException($"{nameof(Context.Configuration)} not initialized", nameof(Context.Configuration)));
        }
    }
}