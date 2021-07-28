// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using NetExtender.AspNetCore.Windows.Services.Types.Services;
using NetExtender.Windows.Services.Utils;

namespace NetExtender.AspNetCore.Windows.Services.Utils
{
    public static class AspNetCoreWindowsServiceUtils
    {
        public static IHost RunAsService(this IHost host)
        {
            return RunAsServiceInternal(host, false);
        }
        
        public static IWebHost RunAsService(this IWebHost host)
        {
            return RunAsServiceInternal(host, false);
        }
        
        public static IHost RunAsServiceQuiet(this IHost host)
        {
            return RunAsServiceInternal(host, true);
        }
        
        public static IWebHost RunAsServiceQuiet(this IWebHost host)
        {
            return RunAsServiceInternal(host, true);
        }
        
        private static IHost RunAsServiceInternal(this IHost host, Boolean quiet)
        {
            if (host is null)
            {
                throw new ArgumentNullException(nameof(host));
            }

            new HostService(host).Run(quiet);
            return host;
        }
        
        private static IWebHost RunAsServiceInternal(this IWebHost host, Boolean quiet)
        {
            if (host is null)
            {
                throw new ArgumentNullException(nameof(host));
            }

            new WebHostService(host).Run(quiet);
            return host;
        }
    }
}