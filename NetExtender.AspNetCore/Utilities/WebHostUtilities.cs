// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using NetExtender.AspNetCore.Types.Wrappers;

namespace NetExtender.Utilities.AspNetCore.Types
{
    public static class WebHostUtilities
    {
        public static IHost ToHost(this IWebHost host)
        {
            if (host is null)
            {
                throw new ArgumentNullException(nameof(host));
            }

            return host as IHost ?? new WebHostWrapper(host);
        }
        
        public static IWebHost ToWebHost(this IHost host)
        {
            if (host is null)
            {
                throw new ArgumentNullException(nameof(host));
            }

            return host as IWebHost ?? new HostWrapper(host);
        }
    }
}