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
        public static ILogger Create(this ILoggerBehavior behavior)
        {
            return Create(behavior, null);
        }

        public static ILogger Create(this ILoggerBehavior behavior, IFormatProvider? provider)
        {
            return Create(behavior, default(TimeSpan), provider);
        }

        public static ILogger Create(this ILoggerBehavior behavior, TimeSpan offset)
        {
            return Create(behavior, offset, null);
        }

        public static ILogger Create(this ILoggerBehavior behavior, TimeSpan offset, IFormatProvider? provider)
        {
            return Create(behavior, LoggingMessageOptions.All, offset, provider);
        }

        public static ILogger Create(this ILoggerBehavior behavior, LoggingMessageOptions options)
        {
            return Create(behavior, options, null);
        }

        public static ILogger Create(this ILoggerBehavior behavior, LoggingMessageOptions options, IFormatProvider? provider)
        {
            return Create(behavior, options, ConvertUtilities.DefaultEscapeType, default, provider);
        }

        public static ILogger Create(this ILoggerBehavior behavior, LoggingMessageOptions options, TimeSpan offset)
        {
            return Create(behavior, options, offset, null);
        }

        public static ILogger Create(this ILoggerBehavior behavior, LoggingMessageOptions options, TimeSpan offset, IFormatProvider? provider)
        {
            return Create(behavior, options, ConvertUtilities.DefaultEscapeType, offset, provider);
        }

        public static ILogger Create(this ILoggerBehavior behavior, EscapeType escape)
        {
            return Create(behavior, escape, null);
        }

        public static ILogger Create(this ILoggerBehavior behavior, EscapeType escape, IFormatProvider? provider)
        {
            return Create(behavior, escape, default, provider);
        }

        public static ILogger Create(this ILoggerBehavior behavior, EscapeType escape, TimeSpan offset)
        {
            return Create(behavior, escape, offset, null);
        }

        public static ILogger Create(this ILoggerBehavior behavior, EscapeType escape, TimeSpan offset, IFormatProvider? provider)
        {
            return Create(behavior, LoggingMessageOptions.All, escape, offset, provider);
        }

        public static ILogger Create(this ILoggerBehavior behavior, LoggingMessageOptions options, EscapeType escape)
        {
            return Create(behavior, options, escape, null);
        }

        public static ILogger Create(this ILoggerBehavior behavior, LoggingMessageOptions options, EscapeType escape, IFormatProvider? provider)
        {
            return Create(behavior, options, escape, default, provider);
        }

        public static ILogger Create(this ILoggerBehavior behavior, LoggingMessageOptions options, EscapeType escape, TimeSpan offset)
        {
            return Create(behavior, options, escape, offset, null);
        }

        public static ILogger Create(this ILoggerBehavior behavior, LoggingMessageOptions options, EscapeType escape, TimeSpan offset, IFormatProvider? provider)
        {
            if (behavior is null)
            {
                throw new ArgumentNullException(nameof(behavior));
            }

            return new Logger(behavior, options, escape, offset, provider);
        }
    }
}