// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace NetExtender.Utilities.AspNetCore.Types
{
    public static class ServiceCollectionUtilities
    {
        public static IServiceCollection AddGenericLogging(this IServiceCollection services)
        {
            return AddGenericLogging<Object?>(services);
        }

        public static IServiceCollection AddGenericLogging<T>(this IServiceCollection services)
        {
            if (services is null)
            {
                throw new ArgumentNullException(nameof(services));
            }
            
            return services.AddSingleton<ILogger>(provider => provider.GetRequiredService<ILogger<T>>());
        }
    }
}