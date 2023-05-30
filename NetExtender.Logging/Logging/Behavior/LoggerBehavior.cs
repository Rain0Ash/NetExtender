// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Collections.Generic;
using System.Globalization;
using System.Threading.Tasks;
using NetExtender.Logging.Behavior.Interfaces;
using NetExtender.Logging.Common;
using NetExtender.Logging.Format;
using NetExtender.Logging.Format.Interfaces;
using NetExtender.Utilities.Types;

namespace NetExtender.Logging.Behavior
{
    public abstract class LoggerBehavior<TLevel> : ILoggerBehavior<TLevel> where TLevel : unmanaged, Enum
    {
        protected ILoggerFormatProvider<TLevel> Formatter { get; }
        protected HashSet<TLevel> Levels { get; }

        public virtual Boolean IsThreadSafe
        {
            get
            {
                return false;
            }
        }

        protected LoggerBehavior()
            : this(LoggerFormatProvider<TLevel>.Default)
        {
        }

        protected LoggerBehavior(ILoggerFormatProvider<TLevel> formatter)
        {
            Formatter = formatter ?? throw new ArgumentNullException(nameof(formatter));
            Levels = new HashSet<TLevel>(EnumUtilities.GetValues<TLevel>());
        }

        protected abstract Boolean Log(String? message, TLevel level, LoggingMessageOptions options, EscapeType escape, DateTimeOffset offset, IFormatProvider? provider);

        public virtual Boolean Log<T>(T value, TLevel level, LoggingMessageOptions options, EscapeType escape, DateTimeOffset offset, IFormatProvider? provider)
        {
            if (!Levels.Contains(level))
            {
                return false;
            }
            
            String message = value.GetString(escape, provider ?? CultureInfo.InvariantCulture) ?? StringUtilities.NullString;
            return Log(message, level, options, escape, offset, provider);
        }

        public virtual Boolean Log<T>(T value, TLevel level, LoggingMessageOptions options, EscapeType escape, DateTimeOffset offset, String? format, IFormatProvider? provider)
        {
            if (!Levels.Contains(level))
            {
                return false;
            }
            
            String message = value.GetString(escape, format, provider ?? CultureInfo.InvariantCulture) ?? StringUtilities.NullString;
            return Log(message, level, options, escape, offset, provider);
        }

        public virtual Boolean IsEnabled(TLevel level)
        {
            return Levels.Contains(level);
        }

        public virtual Boolean Enable(TLevel level)
        {
            return Levels.Add(level);
        }

        public virtual Boolean Disable(TLevel level)
        {
            return Levels.Remove(level);
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        public async ValueTask DisposeAsync()
        {
            await DisposeAsync(true).ConfigureAwait(false);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(Boolean disposing)
        {
        }

        protected virtual ValueTask DisposeAsync(Boolean disposing)
        {
            return ValueTask.CompletedTask;
        }

        ~LoggerBehavior()
        {
            Dispose(false);
        }
    }
}