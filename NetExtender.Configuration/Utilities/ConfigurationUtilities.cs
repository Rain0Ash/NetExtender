// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using NetExtender.Configuration.Interfaces;
using NetExtender.Configuration.Wrappers;

namespace NetExtender.Configuration.Utilities
{
    public static class ConfigurationUtilities
    {
        public static IReadOnlyConfig AsReadOnly(this IConfig config)
        {
            if (config is null)
            {
                throw new ArgumentNullException(nameof(config));
            }

            return new ReadOnlyConfigWrapper(config);
        }
    }
}