// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using NetExtender.Configuration.Behavior.Interfaces;
using NetExtender.Configuration.Builder;
using NetExtender.Configuration.Builder.Interfaces;
using NetExtender.Configuration.Common;
using NetExtender.Configuration.Interfaces;
using NetExtender.Utilities.Configuration;

namespace NetExtender.Configuration.Utilities
{
    public static class ConfigurationBuilderUtilities
    {
        public static IConfigBuilder ToBuilder(this IConfigBehavior behavior)
        {
            if (behavior is null)
            {
                throw new ArgumentNullException(nameof(behavior));
            }

            return behavior.Create().ToBuilder();
        }
        
        public static IConfigBuilder ToBuilder(this IConfigInfo configuration)
        {
            if (configuration is null)
            {
                throw new ArgumentNullException(nameof(configuration));
            }

            return new ConfigBuilder().Add(configuration);
        }

        public static IConfigBuilder Add(this IConfigBuilder builder, IConfigBuilder source)
        {
            if (builder is null)
            {
                throw new ArgumentNullException(nameof(builder));
            }

            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            return builder.Add(source.Build());
        }
        
        public static IConfigBuilder Add(this IConfigBuilder builder, IConfigBuilder source, Func<IConfigInfo, ConfigurationValueEntry[]> selector)
        {
            if (builder is null)
            {
                throw new ArgumentNullException(nameof(builder));
            }

            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            if (selector is null)
            {
                throw new ArgumentNullException(nameof(selector));
            }

            return builder.Add(source.Build(), selector);
        }

        public static IConfigBuilder Union(this IConfigBuilder builder)
        {
            if (builder is null)
            {
                throw new ArgumentNullException(nameof(builder));
            }

            IConfig config = builder.Build();
            return builder.Clear().Add(config);
        }
        
        public static IConfigBuilder Union(this IConfigBuilder builder, Func<IConfigInfo, ConfigurationValueEntry[]> selector)
        {
            if (builder is null)
            {
                throw new ArgumentNullException(nameof(builder));
            }

            if (selector is null)
            {
                throw new ArgumentNullException(nameof(selector));
            }

            IConfig config = builder.Build();
            return builder.Clear().Add(config, selector);
        }
    }
}