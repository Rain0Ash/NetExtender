// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Collections.Concurrent;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;
using AspNet.Core.Types.Initializers.Interfaces;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using NetExtender.AspNet.Core.Types.Initializers;

namespace NetExtender.Utils.AspNetCore.Types
{
    public static class WebHostBuilderUtils
    {
        public static IWebHostBuilder UseStartup(this IWebHostBuilder builder, IStartupProvider provider)
        {
            if (builder is null)
            {
                throw new ArgumentNullException(nameof(builder));
            }

            if (provider is null)
            {
                throw new ArgumentNullException(nameof(provider));
            }

            return builder.UseStartup(_ => provider);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IWebHostBuilder Configure(this IWebHostBuilder builder, AspNetCoreStartupInitializer initializer)
        {
            return UseStartup(builder, initializer);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IWebHostBuilder Configure(this IWebHostBuilder builder, Action<IApplicationBuilder, IWebHostEnvironment>? configuration)
        {
            return Configure(builder, null, configuration);
        }
        
        public static IWebHostBuilder Configure(this IWebHostBuilder builder, Action<IServiceCollection>? services, Action<IApplicationBuilder, IWebHostEnvironment>? configuration)
        {
            if (builder is null)
            {
                throw new ArgumentNullException(nameof(builder));
            }

            if (services is null && configuration is null)
            {
                return builder;
            }

            return builder.UseStartup(new AspNetCoreStartupInitializer(services, configuration));
        }

        private static class BuilderInitializer
        {
            private static ConcurrentDictionary<IWebHostBuilder, IStartupRegistrationProvider> Initializers { get; }

            static BuilderInitializer()
            {
                Initializers = new ConcurrentDictionary<IWebHostBuilder, IStartupRegistrationProvider>();
            }

            public static Boolean Contains(IWebHostBuilder builder)
            {
                if (builder is null)
                {
                    throw new ArgumentNullException(nameof(builder));
                }

                return Initializers.ContainsKey(builder);
            }
            
            public static Boolean TryGetProvider(IWebHostBuilder builder, [MaybeNullWhen(false)] out IStartupRegistrationProvider? provider)
            {
                if (builder is null)
                {
                    throw new ArgumentNullException(nameof(builder));
                }

                return Initializers.TryGetValue(builder, out provider);
            }
            
            public static Boolean Remove(IWebHostBuilder builder, [MaybeNullWhen(false)] out IStartupRegistrationProvider? provider)
            {
                if (builder is null)
                {
                    throw new ArgumentNullException(nameof(builder));
                }

                return Initializers.TryRemove(builder, out provider);
            }
            
            private static IStartupRegistrationProvider Factory(IWebHostBuilder builder)
            {
                return new AspNetCoreStartupInitializer().RegisterCallback(() => Remove(builder, out _));
            }
            
            public static IStartupRegistrationProvider Initialize(IWebHostBuilder builder)
            {
                if (builder is null)
                {
                    throw new ArgumentNullException(nameof(builder));
                }

                IStartupRegistrationProvider provider = Initializers.GetOrAdd(builder, Factory);
                builder.UseStartup(provider);
                return provider;
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static IStartupRegistrationProvider InitializeBuilder(this IWebHostBuilder builder)
        {
            return BuilderInitializer.Initialize(builder);
        }

        public static IWebHostBuilder Register(this IWebHostBuilder builder, Action<IServiceCollection> service)
        {
            if (builder is null)
            {
                throw new ArgumentNullException(nameof(builder));
            }

            if (service is null)
            {
                throw new ArgumentNullException(nameof(service));
            }

            builder.InitializeBuilder().Register(service);
            return builder;
        }

        public static IWebHostBuilder Register(this IWebHostBuilder builder, Action<IApplicationBuilder> application)
        {
            if (builder is null)
            {
                throw new ArgumentNullException(nameof(builder));
            }

            if (application is null)
            {
                throw new ArgumentNullException(nameof(application));
            }
            
            builder.InitializeBuilder().Register(application);
            return builder;
        }
        
        public static IWebHostBuilder Register(this IWebHostBuilder builder, Action<IWebHostEnvironment> environment)
        {
            if (builder is null)
            {
                throw new ArgumentNullException(nameof(builder));
            }

            if (environment is null)
            {
                throw new ArgumentNullException(nameof(environment));
            }
            
            builder.InitializeBuilder().Register(environment);
            return builder;
        }
        
        public static IWebHostBuilder Register(this IWebHostBuilder builder, Action<IApplicationBuilder> application, Action<IWebHostEnvironment> environment)
        {
            if (builder is null)
            {
                throw new ArgumentNullException(nameof(builder));
            }

            if (application is null)
            {
                throw new ArgumentNullException(nameof(application));
            }

            if (environment is null)
            {
                throw new ArgumentNullException(nameof(environment));
            }
            
            builder.InitializeBuilder().Register(application, environment);
            return builder;
        }
        
        public static IWebHostBuilder Register(this IWebHostBuilder builder, Action<IApplicationBuilder, IWebHostEnvironment> configuration)
        {
            if (builder is null)
            {
                throw new ArgumentNullException(nameof(builder));
            }

            if (configuration is null)
            {
                throw new ArgumentNullException(nameof(configuration));
            }
            
            builder.InitializeBuilder().Register(configuration);
            return builder;
        }
        
        public static IWebHostBuilder UseDevelopmentExceptionPage(this IWebHostBuilder builder)
        {
            if (builder is null)
            {
                throw new ArgumentNullException(nameof(builder));
            }

            static void UseDeveloperPage(IApplicationBuilder application, IWebHostEnvironment environment)
            {
                if (environment.IsDevelopment())
                {
                    application.UseDeveloperExceptionPage();
                }
            }
            
            return builder.Register(UseDeveloperPage);
        }
        
        public static IWebHostBuilder LoggingOff(this IWebHostBuilder builder)
        {
            if (builder is null)
            {
                throw new ArgumentNullException(nameof(builder));
            }

            return builder.ConfigureLogging(LoggingBuilderUtils.LoggingOff);
        }
    }
}