// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using NetExtender.Logging.Behavior.Interfaces;
using NetExtender.Logging.Common;
using NetExtender.Logging.Interfaces;
using NetExtender.Utilities.Types;

namespace NetExtender.Logging.Utilities
{
    public static class LoggerUtilities
    {
        public static ILogger<TLevel> Create<TLevel>(this ILoggerBehavior<TLevel> behavior) where TLevel : unmanaged, Enum
        {
            return Create(behavior, null);
        }

        public static ILogger<TLevel> Create<TLevel>(this ILoggerBehavior<TLevel> behavior, IFormatProvider? provider) where TLevel : unmanaged, Enum
        {
            return Create(behavior, default(TimeSpan), provider);
        }

        public static ILogger<TLevel> Create<TLevel>(this ILoggerBehavior<TLevel> behavior, TimeSpan offset) where TLevel : unmanaged, Enum
        {
            return Create(behavior, offset, null);
        }

        public static ILogger<TLevel> Create<TLevel>(this ILoggerBehavior<TLevel> behavior, TimeSpan offset, IFormatProvider? provider) where TLevel : unmanaged, Enum
        {
            return Create(behavior, LoggingMessageOptions.All, offset, provider);
        }

        public static ILogger<TLevel> Create<TLevel>(this ILoggerBehavior<TLevel> behavior, LoggingMessageOptions options) where TLevel : unmanaged, Enum
        {
            return Create(behavior, options, null);
        }

        public static ILogger<TLevel> Create<TLevel>(this ILoggerBehavior<TLevel> behavior, LoggingMessageOptions options, IFormatProvider? provider) where TLevel : unmanaged, Enum
        {
            return Create(behavior, options, ConvertUtilities.DefaultEscapeType, default, provider);
        }

        public static ILogger<TLevel> Create<TLevel>(this ILoggerBehavior<TLevel> behavior, LoggingMessageOptions options, TimeSpan offset) where TLevel : unmanaged, Enum
        {
            return Create(behavior, options, offset, null);
        }

        public static ILogger<TLevel> Create<TLevel>(this ILoggerBehavior<TLevel> behavior, LoggingMessageOptions options, TimeSpan offset, IFormatProvider? provider) where TLevel : unmanaged, Enum
        {
            return Create(behavior, options, ConvertUtilities.DefaultEscapeType, offset, provider);
        }

        public static ILogger<TLevel> Create<TLevel>(this ILoggerBehavior<TLevel> behavior, EscapeType escape) where TLevel : unmanaged, Enum
        {
            return Create(behavior, escape, null);
        }

        public static ILogger<TLevel> Create<TLevel>(this ILoggerBehavior<TLevel> behavior, EscapeType escape, IFormatProvider? provider) where TLevel : unmanaged, Enum
        {
            return Create(behavior, escape, default, provider);
        }

        public static ILogger<TLevel> Create<TLevel>(this ILoggerBehavior<TLevel> behavior, EscapeType escape, TimeSpan offset) where TLevel : unmanaged, Enum
        {
            return Create(behavior, escape, offset, null);
        }

        public static ILogger<TLevel> Create<TLevel>(this ILoggerBehavior<TLevel> behavior, EscapeType escape, TimeSpan offset, IFormatProvider? provider) where TLevel : unmanaged, Enum
        {
            return Create(behavior, LoggingMessageOptions.All, escape, offset, provider);
        }

        public static ILogger<TLevel> Create<TLevel>(this ILoggerBehavior<TLevel> behavior, LoggingMessageOptions options, EscapeType escape) where TLevel : unmanaged, Enum
        {
            return Create(behavior, options, escape, null);
        }

        public static ILogger<TLevel> Create<TLevel>(this ILoggerBehavior<TLevel> behavior, LoggingMessageOptions options, EscapeType escape, IFormatProvider? provider) where TLevel : unmanaged, Enum
        {
            return Create(behavior, options, escape, default, provider);
        }

        public static ILogger<TLevel> Create<TLevel>(this ILoggerBehavior<TLevel> behavior, LoggingMessageOptions options, EscapeType escape, TimeSpan offset) where TLevel : unmanaged, Enum
        {
            return Create(behavior, options, escape, offset, null);
        }

        public static ILogger<TLevel> Create<TLevel>(this ILoggerBehavior<TLevel> behavior, LoggingMessageOptions options, EscapeType escape, TimeSpan offset, IFormatProvider? provider) where TLevel : unmanaged, Enum
        {
            if (behavior is null)
            {
                throw new ArgumentNullException(nameof(behavior));
            }

            return new Logger<TLevel>(behavior, options, escape, offset, provider);
        }
    }
}