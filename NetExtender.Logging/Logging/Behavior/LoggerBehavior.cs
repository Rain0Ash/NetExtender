// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Globalization;
using System.Threading.Tasks;
using NetExtender.Logging.Behavior.Interfaces;
using NetExtender.Logging.Common;
using NetExtender.Utilities.Types;

namespace NetExtender.Logging.Behavior
{
    public abstract class LoggerBehavior : ILoggerBehavior
    {
        public virtual Boolean IsThreadSafe
        {
            get
            {
                return false;
            }
        }
        
        protected String Format(LoggingMessageType type, LoggingMessageOptions options, DateTimeOffset offset)
        {
            return Format(type, options, offset, null);
        }

        protected virtual String Format(LoggingMessageType type, LoggingMessageOptions options, DateTimeOffset offset, IFormatProvider? provider)
        {
            offset = offset.SetMillisecond(0);
            String? time = (options & LoggingMessageOptions.DateTime) switch
            {
                LoggingMessageOptions.Date => offset.Date.ToString("dd-MM-yyyy", provider),
                LoggingMessageOptions.Time => offset.ToString("T", provider),
                LoggingMessageOptions.DateTime => offset.ToString("dd-MM-yyyy hh:mm:sszz", provider),
                _ => null
            };

            String? prefix = options.HasFlag(LoggingMessageOptions.Prefix) ? type.ToString() : null;
            return $"[{prefix}]({time}): ";
        }

        protected abstract void Log(String value, LoggingMessageType type, LoggingMessageOptions options, EscapeType escape, DateTimeOffset offset, IFormatProvider? provider);

        public virtual void Log<T>(T value, LoggingMessageType type, LoggingMessageOptions options, EscapeType escape, DateTimeOffset offset, IFormatProvider? provider)
        {
            String text = value.GetString(escape, provider ?? CultureInfo.InvariantCulture) ?? StringUtilities.NullString;
            Log(text, type, options, escape, offset, provider);
        }

        public virtual void Log<T>(T value, LoggingMessageType type, LoggingMessageOptions options, EscapeType escape, DateTimeOffset offset, String? format, IFormatProvider? provider)
        {
            String text = value.GetString(escape, format, provider ?? CultureInfo.InvariantCulture) ?? StringUtilities.NullString;
            Log(text, type, options, escape, offset, provider);
        }
        
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        public async ValueTask DisposeAsync()
        {
            await DisposeAsync(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(Boolean disposing)
        {
        }

        protected virtual ValueTask DisposeAsync(Boolean disposing)
        {
            return ValueTask.CompletedTask;
        }
    }
}