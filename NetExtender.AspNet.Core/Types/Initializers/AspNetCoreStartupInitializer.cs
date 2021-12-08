// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using AspNet.Core.Types.Initializers.Interfaces;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace NetExtender.AspNetCore.Types.Initializers
{
    public sealed record AspNetCoreStartupInitializer : IStartupRegistrationProvider
    {
        private Action<IServiceCollection>? Collection { get; set; }
        private Action<IApplicationBuilder, IServiceProvider>? Configuration { get; set; }
        private Action? Callback { get; set; }

        public AspNetCoreStartupInitializer()
        {
        }
        
        public AspNetCoreStartupInitializer(Action<IServiceCollection>? collection)
        {
            Collection = collection;
        }
        
        public AspNetCoreStartupInitializer(Action<IApplicationBuilder, IServiceProvider>? configuration)
        {
            Configuration = configuration;
        }
        
        public AspNetCoreStartupInitializer(Action<IServiceCollection>? collection, Action<IApplicationBuilder, IServiceProvider>? configuration)
        {
            Collection = collection;
            Configuration = configuration;
        }
        
        public AspNetCoreStartupInitializer(Action<IApplicationBuilder>? application)
            : this(null, application)
        {
        }
        
        public AspNetCoreStartupInitializer(Action<IServiceCollection>? collection, Action<IApplicationBuilder>? application)
            : this(collection, application is not null ? (builder, _) => application.Invoke(builder) : null)
        {
        }
        
        public AspNetCoreStartupInitializer(Action<IServiceProvider>? provider)
            : this((Action<IServiceCollection>?) null, provider)
        {
        }
        
        public AspNetCoreStartupInitializer(Action<IServiceCollection>? collection, Action<IServiceProvider>? provider)
            : this(collection, provider is not null ? (_, service) => provider.Invoke(service) : null)
        {
        }
        
        public AspNetCoreStartupInitializer(Action<IApplicationBuilder>? application, Action<IServiceProvider>? provider)
            : this(null, application, provider)
        {
        }
        
        public AspNetCoreStartupInitializer(Action<IServiceCollection>? collection, Action<IApplicationBuilder>? application, Action<IServiceProvider>? provider)
            : this(collection, application is not null && provider is not null ? (builder, service) => { application?.Invoke(builder); provider?.Invoke(service); } : null)
        {
        }

        public IStartupRegistrationProvider Register(Action<IServiceCollection> collection)
        {
            Collection += collection ?? throw new ArgumentNullException(nameof(collection));
            return this;
        }

        public IStartupRegistrationProvider Register(Action<IApplicationBuilder> application)
        {
            if (application is null)
            {
                throw new ArgumentNullException(nameof(application));
            }

            Register((builder, _) => application.Invoke(builder));
            return this;
        }
        
        public IStartupRegistrationProvider Register(Action<IServiceProvider> provider)
        {
            if (provider is null)
            {
                throw new ArgumentNullException(nameof(provider));
            }

            Register((_, prov) => provider.Invoke(prov));
            return this;
        }
        
        public IStartupRegistrationProvider Register(Action<IApplicationBuilder> application, Action<IServiceProvider> provider)
        {
            if (application is null)
            {
                throw new ArgumentNullException(nameof(application));
            }

            if (provider is null)
            {
                throw new ArgumentNullException(nameof(provider));
            }
            
            Register((builder, service) => { application?.Invoke(builder); provider?.Invoke(service); });
            return this;
        }
        
        public IStartupRegistrationProvider Register(Action<IApplicationBuilder, IServiceProvider> configuration)
        {
            Configuration += configuration ?? throw new ArgumentNullException(nameof(configuration));
            return this;
        }

        public IStartupRegistrationProvider RegisterCallback(Action callback)
        {
            Callback += callback ?? throw new ArgumentNullException(nameof(callback));
            return this;
        }

        public void ConfigureServices(IServiceCollection collection)
        {
            Collection?.Invoke(collection);
        }

        public void Configure(IApplicationBuilder application, IServiceProvider provider)
        {
            Configuration?.Invoke(application, provider);
            Callback?.Invoke();
        }
    }
}