// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Net;
using System.Runtime.CompilerServices;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using NetExtender.AspNetCore.Types.Middlewares;
using NetExtender.AspNetCore.Windows.Services.Interfaces;
using NetExtender.AspNetCore.Windows.Services.Types.Services;
using NetExtender.Utilities.AspNetCore.Types;
using NetExtender.Windows.Services.Types.Services;
using NetExtender.Windows.Services.Utilities;

namespace NetExtender.AspNetCore.Windows.Services.Utilities
{
    public static class AspNetCoreWindowsServiceUtilities
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static WindowsService AsService(this IHost host)
        {
            if (host is null)
            {
                throw new ArgumentNullException(nameof(host));
            }

            return new HostService(host);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static WindowsService AsService(this IWebHost host)
        {
            if (host is null)
            {
                throw new ArgumentNullException(nameof(host));
            }

            return new WebHostService(host);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IHost RunAsService(this IHost host)
        {
            return RunAsServiceInternal(host, false);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IWebHost RunAsService(this IWebHost host)
        {
            return RunAsServiceInternal(host, false);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IHost RunAsServiceQuiet(this IHost host)
        {
            return RunAsServiceInternal(host, true);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IWebHost RunAsServiceQuiet(this IWebHost host)
        {
            return RunAsServiceInternal(host, true);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static IHost RunAsServiceInternal(this IHost host, Boolean quiet)
        {
            if (host is null)
            {
                throw new ArgumentNullException(nameof(host));
            }

            host.AsService().Run(quiet);
            return host;
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static IWebHost RunAsServiceInternal(this IWebHost host, Boolean quiet)
        {
            if (host is null)
            {
                throw new ArgumentNullException(nameof(host));
            }

            host.AsService().Run(quiet);
            return host;
        }

        public static IServiceCollection AddWindowsServicePauseStateService(this IServiceCollection collection)
        {
            if (collection is null)
            {
                throw new ArgumentNullException(nameof(collection));
            }

            return collection.AddSingleton<IWindowsServicePauseStateService, WindowsServicePauseStateService>();
        }

        private static Boolean UseExternalAccessRestrictionMiddlewareOnWindowsServicePauseStateCondition(HttpContext context)
        {
            IWindowsServicePauseStateService? service = ServiceProviderServiceExtensions.GetService<IWindowsServicePauseStateService>(context.RequestServices);

            return service is not null && service.IsPaused;
        }

        public static IApplicationBuilder UseExternalAccessRestrictionMiddlewareOnWindowsServicePauseState(this IApplicationBuilder builder)
        {
            if (builder is null)
            {
                throw new ArgumentNullException(nameof(builder));
            }

            return builder.UseMiddlewareWhen<ExternalAccessRestrictionMiddleware>(UseExternalAccessRestrictionMiddlewareOnWindowsServicePauseStateCondition);
        }

        public static IApplicationBuilder UseExternalAccessRestrictionMiddlewareOnWindowsServicePauseState(this IApplicationBuilder builder, Int32 reject)
        {
            if (builder is null)
            {
                throw new ArgumentNullException(nameof(builder));
            }
            
            return builder.UseMiddlewareWhen<ExternalAccessRestrictionMiddleware>(UseExternalAccessRestrictionMiddlewareOnWindowsServicePauseStateCondition, reject);
        }
        
        public static IApplicationBuilder UseExternalAccessRestrictionMiddlewareOnWindowsServicePauseState(this IApplicationBuilder builder, HttpStatusCode reject)
        {
            if (builder is null)
            {
                throw new ArgumentNullException(nameof(builder));
            }
            
            return builder.UseMiddlewareWhen<ExternalAccessRestrictionMiddleware>(UseExternalAccessRestrictionMiddlewareOnWindowsServicePauseStateCondition, reject);
        }
    }
}