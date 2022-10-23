// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Text;
using NetExtender.Logging.Common;
using NetExtender.Logging.Format.Interfaces;

namespace NetExtender.Logging.Format
{
    public class LoggerFormatProvider : ILoggerFormatProvider
    {
        public static ILoggerFormatProvider Default { get; } = new LoggerFormatProvider();
        
        public String? Format(String? message, LoggingMessageType type, LoggingMessageOptions options, DateTimeOffset offset)
        {
            return Format(message, type, options, offset, null);
        }

        public virtual String? Format(String? message, LoggingMessageType type, LoggingMessageOptions options, DateTimeOffset offset, IFormatProvider? provider)
        {
            if (message is null)
            {
                return null;
            }
            
            String? prefix = Prefix(type, options, offset, provider);
            String? time = Time(type, options, offset, provider);
            String? thread = Thread(type, options, offset, provider);

            Int32 capacity = time?.Length ?? 0 + prefix?.Length ?? 0 + thread?.Length ?? 0 + message.Length;

            if (capacity <= 0)
            {
                return null;
            }

            capacity += 2;
            StringBuilder result = new StringBuilder(capacity, capacity);
            result.Append(prefix).Append(time).Append(thread);
            
            return result.Length > 0 ? result.Append(": ").Append(message).ToString() : null;
        }

        protected virtual String? Prefix(LoggingMessageType type, LoggingMessageOptions options, DateTimeOffset offset, IFormatProvider? provider)
        {
            return options.HasFlag(LoggingMessageOptions.Prefix) ? $"[{type}]" : null;
        }

        protected virtual String? Time(LoggingMessageType type, LoggingMessageOptions options, DateTimeOffset offset, IFormatProvider? provider)
        {
            return (options & LoggingMessageOptions.DateTime) switch
            {
                LoggingMessageOptions.Date => $"({offset.Date.ToString("dd-MM-yyyy", provider)})",
                LoggingMessageOptions.Time => $"({offset.ToString("T", provider)})",
                LoggingMessageOptions.DateTime => $"({offset.ToString("dd-MM-yyyy hh:mm:sszz", provider)})",
                _ => null
            };
        }
        
        protected virtual String? Thread(LoggingMessageType type, LoggingMessageOptions options, DateTimeOffset offset, IFormatProvider? provider)
        {
            return options.HasFlag(LoggingMessageOptions.Thread) ? $"|{Environment.CurrentManagedThreadId.ToString(provider)}|" : null;
        }
    }
}