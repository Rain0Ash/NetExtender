// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Globalization;
using System.Threading.Tasks;
using NetExtender.Logging.Behavior.Interfaces;
using NetExtender.Logging.Common;
using NetExtender.Logging.Format;
using NetExtender.Logging.Format.Interfaces;
using NetExtender.Utilities.Types;

namespace NetExtender.Logging.Behavior
{
    public abstract class LoggerBehavior : ILoggerBehavior
    {
        protected ILoggerFormatProvider Formatter { get; }
        
        public virtual Boolean IsThreadSafe
        {
            get
            {
                return false;
            }
        }

        protected LoggerBehavior()
            : this(LoggerFormatProvider.Default)
        {
        }
        
        protected LoggerBehavior(ILoggerFormatProvider formatter)
        {
            Formatter = formatter ?? throw new ArgumentNullException(nameof(formatter));
        }

        protected abstract Boolean Log(String? message, LoggingMessageType type, LoggingMessageOptions options, EscapeType escape, DateTimeOffset offset, IFormatProvider? provider);

        public virtual Boolean Log<T>(T value, LoggingMessageType type, LoggingMessageOptions options, EscapeType escape, DateTimeOffset offset, IFormatProvider? provider)
        {
            String message = value.GetString(escape, provider ?? CultureInfo.InvariantCulture) ?? StringUtilities.NullString;
            return Log(message, type, options, escape, offset, provider);
        }

        public virtual Boolean Log<T>(T value, LoggingMessageType type, LoggingMessageOptions options, EscapeType escape, DateTimeOffset offset, String? format, IFormatProvider? provider)
        {
            String message = value.GetString(escape, format, provider ?? CultureInfo.InvariantCulture) ?? StringUtilities.NullString;
            return Log(message, type, options, escape, offset, provider);
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