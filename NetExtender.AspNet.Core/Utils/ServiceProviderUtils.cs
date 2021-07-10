// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;
using NetExtender.AspNet.Core.Exceptions;

namespace NetExtender.Utils.AspNetCore.Types
{
    public static class ServiceProviderUtils
    {
        public static T? GetService<T>(this IServiceProvider provider)
        {
            if (provider is null)
            {
                throw new ArgumentNullException(nameof(provider));
            }

            return (T?) provider.GetService(typeof(T));
        }
        
        public static Object GetServiceRequired(this IServiceProvider provider, Type service)
        {
            if (provider is null)
            {
                throw new ArgumentNullException(nameof(provider));
            }

            return provider.GetService(service) ?? throw new ServiceNotFoundException(service);
        }

        public static T GetServiceRequired<T>(this IServiceProvider provider)
        {
            if (provider is null)
            {
                throw new ArgumentNullException(nameof(provider));
            }

            return provider.GetService<T>() ?? throw new ServiceNotFoundException(typeof(T));
        }
        
        public static Boolean Exists(this IServiceProvider provider, Type service)
        {
            if (provider is null)
            {
                throw new ArgumentNullException(nameof(provider));
            }

            try
            {
                return provider.GetService(service) is not null;
            }
            catch
            {
                return false;
            }
        }

        public static Boolean Exists<T>(this IServiceProvider provider)
        {
            if (provider is null)
            {
                throw new ArgumentNullException(nameof(provider));
            }

            try
            {
                return provider.GetService<T>() is not null;
            }
            catch
            {
                return false;
            }
        }

        public static ILogger<T> GetLoggerService<T>(this IServiceProvider provider) where T : class
        {
            if (provider is null)
            {
                throw new ArgumentNullException(nameof(provider));
            }

            return provider.Exists<ILogger<T>>() ? provider.GetServiceRequired<ILogger<T>>() : new NullLogger<T>();
        }
    }
}