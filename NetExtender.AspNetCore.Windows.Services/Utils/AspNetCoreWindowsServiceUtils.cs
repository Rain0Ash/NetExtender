// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.ServiceProcess;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using NetExtender.AspNetCore.Windows.Services.Types.Services;

namespace NetExtender.AspNetCore.Windows.Services.Utils
{
    public static class AspNetCoreWindowsServiceUtils
    {
        public static IHost RunAsService(this IHost host)
        {
            if (host is null)
            {
                throw new ArgumentNullException(nameof(host));
            }

            ServiceBase.Run(new HostService(host));
            return host;
        }
        
        public static IWebHost RunAsService(this IWebHost host)
        {
            if (host is null)
            {
                throw new ArgumentNullException(nameof(host));
            }

            ServiceBase.Run(new WebHostService(host));
            return host;
        }
    }
}