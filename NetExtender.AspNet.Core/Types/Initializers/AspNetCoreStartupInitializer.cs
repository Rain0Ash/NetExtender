// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using AspNet.Core.Types.Initializers.Interfaces;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;

namespace NetExtender.AspNet.Core.Types.Initializers
{
    public sealed record AspNetCoreStartupInitializer : IStartupRegistrationProvider
    {
        public Action<IServiceCollection>? Service { get; private set; }
        public Action<IApplicationBuilder, IWebHostEnvironment>? Configuration { get; private set; }

        public AspNetCoreStartupInitializer()
        {
        }
        
        public AspNetCoreStartupInitializer(Action<IServiceCollection>? service)
        {
            Service = service;
        }
        
        public AspNetCoreStartupInitializer(Action<IApplicationBuilder, IWebHostEnvironment>? configuration)
        {
            Configuration = configuration;
        }
        
        public AspNetCoreStartupInitializer(Action<IServiceCollection>? service, Action<IApplicationBuilder, IWebHostEnvironment>? configuration)
        {
            Service = service;
            Configuration = configuration;
        }
        
        public AspNetCoreStartupInitializer(Action<IApplicationBuilder>? application)
            : this(null, application)
        {
        }
        
        public AspNetCoreStartupInitializer(Action<IServiceCollection>? service, Action<IApplicationBuilder>? application)
            : this(service, application is not null ? (app, _) => application.Invoke(app) : null)
        {
        }
        
        public AspNetCoreStartupInitializer(Action<IWebHostEnvironment>? environment)
            : this((Action<IServiceCollection>?) null, environment)
        {
        }
        
        public AspNetCoreStartupInitializer(Action<IServiceCollection>? service, Action<IWebHostEnvironment>? environment)
            : this(service, environment is not null ? (_, env) => environment.Invoke(env) : null)
        {
        }
        
        public AspNetCoreStartupInitializer(Action<IApplicationBuilder>? application, Action<IWebHostEnvironment>? environment)
            : this(null, application, environment)
        {
        }
        
        public AspNetCoreStartupInitializer(Action<IServiceCollection>? service, Action<IApplicationBuilder>? application, Action<IWebHostEnvironment>? environment)
            : this(service, application is not null && environment is not null ? (app, env) => { application?.Invoke(app); environment?.Invoke(env); } : null)
        {
        }

        public IStartupRegistrationProvider Register(Action<IServiceCollection> service)
        {
            Service += service ?? throw new ArgumentNullException(nameof(service));
            return this;
        }

        public IStartupRegistrationProvider Register(Action<IApplicationBuilder> application)
        {
            if (application is null)
            {
                throw new ArgumentNullException(nameof(application));
            }

            Register((app, _) => application.Invoke(app));
            return this;
        }
        
        public IStartupRegistrationProvider Register(Action<IWebHostEnvironment> environment)
        {
            if (environment is null)
            {
                throw new ArgumentNullException(nameof(environment));
            }

            Register((_, env) => environment.Invoke(env));
            return this;
        }
        
        public IStartupRegistrationProvider Register(Action<IApplicationBuilder> application, Action<IWebHostEnvironment> environment)
        {
            if (application is null)
            {
                throw new ArgumentNullException(nameof(application));
            }

            if (environment is null)
            {
                throw new ArgumentNullException(nameof(environment));
            }
            
            Register((app, env) => { application?.Invoke(app); environment?.Invoke(env); });
            return this;
        }
        
        public IStartupRegistrationProvider Register(Action<IApplicationBuilder, IWebHostEnvironment> configuration)
        {
            Configuration += configuration ?? throw new ArgumentNullException(nameof(configuration));
            return this;
        }

        public void ConfigureServices(IServiceCollection collection)
        {
            Service?.Invoke(collection);
        }

        public void Configure(IApplicationBuilder application, IWebHostEnvironment environment)
        {
            Configuration?.Invoke(application, environment);
        }
    }
}