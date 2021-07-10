// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Runtime.CompilerServices;
using AspNet.Core.Types.Initializers.Interfaces;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
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