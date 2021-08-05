// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Runtime.CompilerServices;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using NetExtender.AspNetCore.Windows.Services.Types.Services;
using NetExtender.Windows.Services.Types.Services;
using NetExtender.Windows.Services.Utils;

namespace NetExtender.AspNetCore.Windows.Services.Utils
{
    public static class AspNetCoreWindowsServiceUtils
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
    }
}