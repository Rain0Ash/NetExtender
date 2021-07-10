// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using AspNet.Core.Types.Initializers.Interfaces;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;

namespace NetExtender.AspNet.Core.Types.Initializers
{
    public sealed record AspNetCoreStartupInitializer : IStartupProvider
    {
        public Action<IServiceCollection>? Services { get; }
        public Action<IApplicationBuilder, IWebHostEnvironment>? Configuration { get; }

        public AspNetCoreStartupInitializer()
        {
        }
        
        public AspNetCoreStartupInitializer(Action<IServiceCollection>? services)
        {
            Services = services;
        }
        
        public AspNetCoreStartupInitializer(Action<IApplicationBuilder, IWebHostEnvironment>? configuration)
        {
            Configuration = configuration;
        }
        
        public AspNetCoreStartupInitializer(Action<IServiceCollection>? services, Action<IApplicationBuilder, IWebHostEnvironment>? configuration)
        {
            Services = services;
            Configuration = configuration;
        }
        
        public AspNetCoreStartupInitializer(Action<IApplicationBuilder>? application)
            : this(null, application)
        {
        }
        
        public AspNetCoreStartupInitializer(Action<IServiceCollection>? services, Action<IApplicationBuilder>? application)
            : this(services, application is not null ? (app, _) => application.Invoke(app) : null)
        {
        }
        
        public AspNetCoreStartupInitializer(Action<IWebHostEnvironment>? environment)
            : this((Action<IServiceCollection>?) null, environment)
        {
        }
        
        public AspNetCoreStartupInitializer(Action<IServiceCollection>? services, Action<IWebHostEnvironment>? environment)
            : this(services, environment is not null ? (_, env) => environment.Invoke(env) : null)
        {
        }
        
        public AspNetCoreStartupInitializer(Action<IApplicationBuilder>? application, Action<IWebHostEnvironment>? environment)
            : this(null, application, environment)
        {
        }
        
        public AspNetCoreStartupInitializer(Action<IServiceCollection>? services, Action<IApplicationBuilder>? application, Action<IWebHostEnvironment>? environment)
            : this(services, application is not null && environment is not null ? (app, env) => { application?.Invoke(app); environment?.Invoke(env); } : null)
        {
        }
        
        public void ConfigureServices(IServiceCollection collection)
        {
            Services?.Invoke(collection);
        }

        public void Configure(IApplicationBuilder application, IWebHostEnvironment environment)
        {
            Configuration?.Invoke(application, environment);
        }
    }
}