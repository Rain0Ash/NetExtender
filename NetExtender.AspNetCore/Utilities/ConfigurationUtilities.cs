// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using Microsoft.Extensions.Configuration;

namespace NetExtender.Utilities.AspNetCore.Types
{
    public static class ConfigurationUtilities
    {
        public static T GetConfigurations<T>(this IConfiguration configuration, String section) where T : class
        {
            if (configuration is null)
            {
                throw new ArgumentNullException(nameof(configuration));
            }
            
            return configuration.GetSection(section).GetConfigurations<T>();
        }
    }
}