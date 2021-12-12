// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Collections.Concurrent;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Net;
using System.Runtime.CompilerServices;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using NetExtender.AspNetCore.Types.Exceptions;
using NetExtender.AspNetCore.Types.Initializers;
using NetExtender.AspNetCore.Types.Initializers.Interfaces;
using NetExtender.Utilities.Network;
using NetExtender.Utilities.Types;

namespace NetExtender.Utilities.AspNetCore.Types
{
    public static class WebHostBuilderUtilities
    {
        public static IWebHostBuilder UseIf(this IWebHostBuilder builder, Func<IWebHostBuilder, IWebHostBuilder> factory, Boolean condition)
        {
            if (builder is null)
            {
                throw new ArgumentNullException(nameof(builder));
            }
	
            if (factory is null)
            {
                throw new ArgumentNullException(nameof(factory));
            }
	
            return condition ? factory(builder) : builder;
        }

        public static IWebHostBuilder UseIfNot(this IWebHostBuilder builder, Func<IWebHostBuilder, IWebHostBuilder> factory, Boolean condition)
        {
            if (builder is null)
            {
                throw new ArgumentNullException(nameof(builder));
            }
	
            if (factory is null)
            {
                throw new ArgumentNullException(nameof(factory));
            }
	
            return !condition ? factory(builder) : builder;
        }
        
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
        public static IWebHostBuilder Configure(this IWebHostBuilder builder, Action<IApplicationBuilder, IServiceProvider>? configuration)
        {
            return Configure(builder, null, configuration);
        }
        
        public static IWebHostBuilder Configure(this IWebHostBuilder builder, Action<IServiceCollection>? services, Action<IApplicationBuilder, IServiceProvider>? configuration)
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
            
            public static Boolean TryGetProvider(IWebHostBuilder builder, [MaybeNullWhen(false)] out IStartupRegistrationProvider provider)
            {
                if (builder is null)
                {
                    throw new ArgumentNullException(nameof(builder));
                }

                return Initializers.TryGetValue(builder, out provider);
            }
            
            public static Boolean Remove(IWebHostBuilder builder, [MaybeNullWhen(false)] out IStartupRegistrationProvider provider)
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

        public static IWebHostBuilder Register(this IWebHostBuilder builder, Action<IServiceCollection> collection)
        {
            if (builder is null)
            {
                throw new ArgumentNullException(nameof(builder));
            }

            if (collection is null)
            {
                throw new ArgumentNullException(nameof(collection));
            }

            builder.InitializeBuilder().Register(collection);
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
        
        public static IWebHostBuilder Register(this IWebHostBuilder builder, Action<IServiceProvider> provider)
        {
            if (builder is null)
            {
                throw new ArgumentNullException(nameof(builder));
            }

            if (provider is null)
            {
                throw new ArgumentNullException(nameof(provider));
            }
            
            builder.InitializeBuilder().Register(provider);
            return builder;
        }
        
        public static IWebHostBuilder Register(this IWebHostBuilder builder, Action<IApplicationBuilder> application, Action<IServiceProvider> provider)
        {
            if (builder is null)
            {
                throw new ArgumentNullException(nameof(builder));
            }

            if (application is null)
            {
                throw new ArgumentNullException(nameof(application));
            }

            if (provider is null)
            {
                throw new ArgumentNullException(nameof(provider));
            }
            
            builder.InitializeBuilder().Register(application, provider);
            return builder;
        }
        
        public static IWebHostBuilder Register(this IWebHostBuilder builder, Action<IApplicationBuilder, IServiceProvider> configuration)
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

            static void UseDeveloperPage(IApplicationBuilder application, IServiceProvider provider)
            {
                IWebHostEnvironment? environment = provider.GetService<IWebHostEnvironment>();

                if (environment is null)
                {
                    throw new ServiceNotFoundException(typeof(IWebHostEnvironment));
                }
                
                if (environment.IsDevelopment())
                {
                    application.UseDeveloperExceptionPage();
                }
            }
            
            return builder.Register(UseDeveloperPage);
        }
        
        public static IWebHostBuilder UseDevelopmentExceptionPage(this IWebHostBuilder builder, IWebHostEnvironment environment)
        {
            if (builder is null)
            {
                throw new ArgumentNullException(nameof(builder));
            }

            if (environment is null)
            {
                throw new ArgumentNullException(nameof(environment));
            }

            void UseDeveloperPage(IApplicationBuilder application)
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

            return builder.ConfigureLogging(logging => logging.LoggingOff());
        }
        
        public static IWebHostBuilder UseHttpUrls(this IWebHostBuilder builder, UInt16 port)
        {
            if (builder is null)
            {
                throw new ArgumentNullException(nameof(builder));
            }

            return builder.UseHttpUrls(IPAddress.Loopback.ToIPEndPoint(port));
        }

        public static IWebHostBuilder UseHttpUrls(this IWebHostBuilder builder, Int32 port)
        {
            if (builder is null)
            {
                throw new ArgumentNullException(nameof(builder));
            }
            
            return builder.UseHttpUrls(IPAddress.Loopback.ToIPEndPoint(port));
        }

        public static IWebHostBuilder UseHttpUrls(this IWebHostBuilder builder, params IPEndPoint[] endpoints)
        {
            if (builder is null)
            {
                throw new ArgumentNullException(nameof(builder));
            }

            return builder.UseUrls(UriUtilities.HttpDelimiter, endpoints);
        }
        
        public static IWebHostBuilder UseHttpsUrls(this IWebHostBuilder builder, UInt16 port)
        {
            if (builder is null)
            {
                throw new ArgumentNullException(nameof(builder));
            }

            return builder.UseHttpsUrls(IPAddress.Loopback.ToIPEndPoint(port));
        }

        public static IWebHostBuilder UseHttpsUrls(this IWebHostBuilder builder, Int32 port)
        {
            if (builder is null)
            {
                throw new ArgumentNullException(nameof(builder));
            }
            
            return builder.UseHttpsUrls(IPAddress.Loopback.ToIPEndPoint(port));
        }
        
        public static IWebHostBuilder UseHttpsUrls(this IWebHostBuilder builder, params IPEndPoint[] endpoints)
        {
            if (builder is null)
            {
                throw new ArgumentNullException(nameof(builder));
            }

            return builder.UseUrls(UriUtilities.HttpsDelimiter, endpoints);
        }

        public static IWebHostBuilder UseUrls(this IWebHostBuilder builder, String delemiter, params IPEndPoint[] endpoints)
        {
            if (builder is null)
            {
                throw new ArgumentNullException(nameof(builder));
            }

            return builder.UseUrls(endpoints.SelectWhereNotNull(endpoint => delemiter + endpoint).ToArray());
        }
        
        public static IWebHostBuilder UseUrls(this IWebHostBuilder builder, UInt16 http, UInt16 https)
        {
            if (builder is null)
            {
                throw new ArgumentNullException(nameof(builder));
            }

            return builder.UseUrls(IPAddress.Loopback.ToIPEndPoint(http), IPAddress.Loopback.ToIPEndPoint(https));
        }

        public static IWebHostBuilder UseUrls(this IWebHostBuilder builder, Int32 http, Int32 https)
        {
            if (builder is null)
            {
                throw new ArgumentNullException(nameof(builder));
            }
            
            return builder.UseUrls(IPAddress.Loopback.ToIPEndPoint(http), IPAddress.Loopback.ToIPEndPoint(https));
        }

        public static IWebHostBuilder UseUrls(this IWebHostBuilder builder, IPEndPoint http, IPEndPoint https)
        {
            if (builder is null)
            {
                throw new ArgumentNullException(nameof(builder));
            }

            return builder.UseUrls(UriUtilities.HttpDelimiter + http, UriUtilities.HttpsDelimiter + https);
        }
    }
}