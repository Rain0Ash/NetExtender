// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using Microsoft.Extensions.Logging;

namespace NetExtender.Utilities.AspNetCore.Types
{
    public static class LoggingBuilderUtilities
    {
        public static ILoggingBuilder AddIf(this ILoggingBuilder builder, Func<ILoggingBuilder, ILoggingBuilder> factory, Boolean condition)
        {
            if (builder is null)
            {
                throw new ArgumentNullException(nameof(builder));
            }

            if (factory is null)
            {
                throw new ArgumentNullException(nameof(factory));
            }

            return condition ? factory(builder) : builder;
        }

        public static ILoggingBuilder AddIfNot(this ILoggingBuilder builder, Func<ILoggingBuilder, ILoggingBuilder> factory, Boolean condition)
        {
            if (builder is null)
            {
                throw new ArgumentNullException(nameof(builder));
            }

            if (factory is null)
            {
                throw new ArgumentNullException(nameof(factory));
            }

            return !condition ? factory(builder) : builder;
        }

        public static ILoggingBuilder LoggingOff(this ILoggingBuilder builder)
        {
            if (builder is null)
            {
                throw new ArgumentNullException(nameof(builder));
            }

            builder.ClearProviders();
            return builder.SetMinimumLevel(LogLevel.None);
        }
    }
}