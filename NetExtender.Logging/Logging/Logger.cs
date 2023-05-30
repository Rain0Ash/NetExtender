// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Threading.Tasks;
using NetExtender.Logging.Behavior.Interfaces;
using NetExtender.Logging.Common;
using NetExtender.Logging.Interfaces;
using NetExtender.Utilities.Types;

namespace NetExtender.Logging
{
    public class Logger<TLevel> : ILogger<TLevel> where TLevel : unmanaged, Enum
    {
        protected ILoggerBehavior<TLevel> Behavior { get; }
        public LoggingMessageOptions Options { get; }
        public EscapeType Escape { get; }
        public TimeSpan Offset { get; }
        public IFormatProvider? Provider { get; }

        public Boolean IsUtc
        {
            get
            {
                return Offset == default && Options.HasFlag(LoggingMessageOptions.Utc);
            }
        }

        public Boolean IsDate
        {
            get
            {
                return Options.HasFlag(LoggingMessageOptions.Date);
            }
        }

        public Boolean IsUtcDate
        {
            get
            {
                return Options.HasFlag(LoggingMessageOptions.UtcDate);
            }
        }

        public Boolean IsTime
        {
            get
            {
                return Options.HasFlag(LoggingMessageOptions.Time);
            }
        }

        public Boolean IsUtcTime
        {
            get
            {
                return Options.HasFlag(LoggingMessageOptions.UtcTime);
            }
        }

        public Boolean IsDateTime
        {
            get
            {
                return Options.HasFlag(LoggingMessageOptions.DateTime);
            }
        }

        public Boolean IsUtcDateTime
        {
            get
            {
                return Options.HasFlag(LoggingMessageOptions.UtcDateTime);
            }
        }

        public Boolean IsPrefix
        {
            get
            {
                return Options.HasFlag(LoggingMessageOptions.Prefix);
            }
        }

        public Boolean IsColor
        {
            get
            {
                return Options.HasFlag(LoggingMessageOptions.Color);
            }
        }

        public Boolean IsThreadSafe
        {
            get
            {
                return Behavior.IsThreadSafe;
            }
        }

        protected internal Logger(ILoggerBehavior<TLevel> behavior, LoggingMessageOptions options, EscapeType escape, TimeSpan offset, IFormatProvider? provider)
        {
            Behavior = behavior ?? throw new ArgumentNullException(nameof(behavior));
            Options = options;
            Escape = escape;
            Offset = offset;
            Provider = provider;
        }

        protected virtual DateTimeOffset Now(LoggingMessageOptions options)
        {
            return Offset != default ? DateTimeOffset.UtcNow.ToOffset(Offset) : options.HasFlag(LoggingMessageOptions.Utc) ? DateTimeOffset.UtcNow : DateTimeOffset.Now;
        }

        public void Log<T>(T value, TLevel level)
        {
            Log(value, level, Options);
        }

        public void Log<T>(T value, TLevel level, LoggingMessageOptions options)
        {
            Log(value, level, options, Escape);
        }

        public void Log<T>(T value, TLevel level, EscapeType escape)
        {
            Log(value, level, Options, escape);
        }

        public void Log<T>(T value, TLevel level, LoggingMessageOptions options, EscapeType escape)
        {
            Log(value, level, options, escape, Provider);
        }

        public void Log<T>(T value, TLevel level, IFormatProvider? provider)
        {
            Log(value, level, Options, provider);
        }

        public void Log<T>(T value, TLevel level, LoggingMessageOptions options, IFormatProvider? provider)
        {
            Log(value, level, options, Escape, provider);
        }

        public void Log<T>(T value, TLevel level, EscapeType escape, IFormatProvider? provider)
        {
            Log(value, level, Options, escape, provider);
        }

        public void Log<T>(T value, TLevel level, LoggingMessageOptions options, EscapeType escape, IFormatProvider? provider)
        {
            DateTimeOffset now = Now(options);
            Behavior.Log(value, level, options, escape, now, provider);
        }

        public void Log<T>(T value, TLevel level, String? format)
        {
            Log(value, level, Options, format);
        }

        public void Log<T>(T value, TLevel level, LoggingMessageOptions options, String? format)
        {
            Log(value, level, options, Escape, format);
        }

        public void Log<T>(T value, TLevel level, EscapeType escape, String? format)
        {
            Log(value, level, Options, escape, format);
        }

        public void Log<T>(T value, TLevel level, LoggingMessageOptions options, EscapeType escape, String? format)
        {
            Log(value, level, options, escape, format, Provider);
        }

        public void Log<T>(T value, TLevel level, String? format, IFormatProvider? provider)
        {
            Log(value, level, Options, format, provider);
        }

        public void Log<T>(T value, TLevel level, LoggingMessageOptions options, String? format, IFormatProvider? provider)
        {
            Log(value, level, options, Escape, format, provider);
        }

        public void Log<T>(T value, TLevel level, EscapeType escape, String? format, IFormatProvider? provider)
        {
            Log(value, level, Options, escape, format, provider);
        }

        public void Log<T>(T value, TLevel level, LoggingMessageOptions options, EscapeType escape, String? format, IFormatProvider? provider)
        {
            DateTimeOffset now = Now(options);
            Behavior.Log(value, level, options, escape, now, format, provider);
        }

        public Boolean IsEnabled(TLevel level)
        {
            return Behavior.IsEnabled(level);
        }

        public Boolean Enable(TLevel level)
        {
            return Behavior.Enable(level);
        }

        public Boolean Disable(TLevel level)
        {
            return Behavior.Disable(level);
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
            if (disposing)
            {
                Behavior.Dispose();
            }
        }

        protected virtual async ValueTask DisposeAsync(Boolean disposing)
        {
            if (disposing)
            {
                await Behavior.DisposeAsync().ConfigureAwait(false);
            }
        }

        ~Logger()
        {
            Dispose(false);
        }
    }
}