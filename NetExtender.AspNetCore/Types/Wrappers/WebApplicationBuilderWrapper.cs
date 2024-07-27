// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Diagnostics.CodeAnalysis;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace NetExtender.AspNetCore.Types.Wrappers
{
    public sealed class WebApplicationBuilderWrapper : IHostBuilder
    {
        [return: NotNullIfNotNull("value")]
        public static implicit operator WebApplicationBuilderWrapper?(WebApplicationBuilder? value)
        {
            return value is not null ? new WebApplicationBuilderWrapper(value) : null;
        }

        [return: NotNullIfNotNull("value")]
        public static implicit operator WebApplicationBuilder?(WebApplicationBuilderWrapper? value)
        {
            return value?.Builder;
        }

        public WebApplicationBuilder Builder { get; }
        
        public IDictionary<Object, Object> Properties
        {
            get
            {
                return ImmutableDictionary<Object, Object>.Empty;
            }
        }
        
        public WebApplicationBuilderWrapper(String[] arguments)
        {
            Builder = WebApplication.CreateBuilder(arguments);
        }
        
        public WebApplicationBuilderWrapper(WebApplicationOptions options)
        {
            Builder = WebApplication.CreateBuilder(options);
        }

        public WebApplicationBuilderWrapper(WebApplicationBuilder builder)
        {
            Builder = builder ?? throw new ArgumentNullException(nameof(builder));
        }

        public IHost Build()
        {
            return Builder.Build();
        }

        public IHostBuilder ConfigureAppConfiguration(Action<HostBuilderContext, IConfigurationBuilder> configure)
        {
            return this;
        }

        public IHostBuilder ConfigureContainer<TContainerBuilder>(Action<HostBuilderContext, TContainerBuilder> configure)
        {
            return this;
        }

        public IHostBuilder ConfigureHostConfiguration(Action<IConfigurationBuilder> configure)
        {
            return this;
        }

        public IHostBuilder ConfigureServices(Action<HostBuilderContext, IServiceCollection> configure)
        {
            return this;
        }

        public IHostBuilder UseServiceProviderFactory<TContainerBuilder>(IServiceProviderFactory<TContainerBuilder> factory) where TContainerBuilder : notnull
        {
            return this;
        }

        public IHostBuilder UseServiceProviderFactory<TContainerBuilder>(Func<HostBuilderContext, IServiceProviderFactory<TContainerBuilder>> factory) where TContainerBuilder : notnull
        {
            return this;
        }
    }
}