// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using JetBrains.Annotations;
using NetExtender.Configuration.Common;
using NetExtender.Configuration.Interfaces;

namespace NetExtender.Utils.Config
{
    public static class ConfigUtils
    {
        public static IPropertyConfig Create([NotNull] this IConfigBehavior behavior)
        {
            if (behavior is null)
            {
                throw new ArgumentNullException(nameof(behavior));
            }

            return NetExtender.Configuration.Config.Create(behavior);
        }
        
        public static IPropertyConfig Create(this ConfigType type)
        {
            return Create(type, null);
        }
        
        public static IPropertyConfig Create(this ConfigType type, ConfigOptions options)
        {
            return NetExtender.Configuration.Config.Create(null, type, options);
        }

        public static IPropertyConfig Create(this ConfigType type, String path)
        {
            return NetExtender.Configuration.Config.Create(path, type);
        }
        
        public static IPropertyConfig Create(this ConfigType type, String path, ConfigOptions options)
        {
            return Create(type, options, path);
        }

        public static IPropertyConfig Create(this ConfigType type, ConfigOptions options, String path)
        {
            return NetExtender.Configuration.Config.Create(path, type, options);
        }
    }
}