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
    public class Logger : ILogger
    {
        protected ILoggerBehavior Behavior { get; }
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

        protected internal Logger(ILoggerBehavior behavior, LoggingMessageOptions options, EscapeType escape, TimeSpan offset, IFormatProvider? provider)
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

        public void Log<T>(T value, LoggingMessageType type)
        {
            Log(value, type, Options);
        }

        public void Log<T>(T value, LoggingMessageType type, LoggingMessageOptions options)
        {
            Log(value, type, options, Escape);
        }

        public void Log<T>(T value, LoggingMessageType type, EscapeType escape)
        {
            Log(value, type, Options, escape);
        }

        public void Log<T>(T value, LoggingMessageType type, LoggingMessageOptions options, EscapeType escape)
        {
            Log(value, type, options, escape, Provider);
        }

        public void Log<T>(T value, LoggingMessageType type, IFormatProvider? provider)
        {
            Log(value, type, Options, provider);
        }

        public void Log<T>(T value, LoggingMessageType type, LoggingMessageOptions options, IFormatProvider? provider)
        {
            Log(value, type, options, Escape, provider);
        }

        public void Log<T>(T value, LoggingMessageType type, EscapeType escape, IFormatProvider? provider)
        {
            Log(value, type, Options, escape, provider);
        }

        public void Log<T>(T value, LoggingMessageType type, LoggingMessageOptions options, EscapeType escape, IFormatProvider? provider)
        {
            DateTimeOffset now = Now(options);
            Behavior.Log(value, type, options, escape, now, provider);
        }

        public void Log<T>(T value, LoggingMessageType type, String? format)
        {
            Log(value, type, Options, format);
        }

        public void Log<T>(T value, LoggingMessageType type, LoggingMessageOptions options, String? format)
        {
            Log(value, type, options, Escape, format);
        }

        public void Log<T>(T value, LoggingMessageType type, EscapeType escape, String? format)
        {
            Log(value, type, Options, escape, format);
        }

        public void Log<T>(T value, LoggingMessageType type, LoggingMessageOptions options, EscapeType escape, String? format)
        {
            Log(value, type, options, escape, format, Provider);
        }

        public void Log<T>(T value, LoggingMessageType type, String? format, IFormatProvider? provider)
        {
            Log(value, type, Options, format, provider);
        }

        public void Log<T>(T value, LoggingMessageType type, LoggingMessageOptions options, String? format, IFormatProvider? provider)
        {
            Log(value, type, options, Escape, format, provider);
        }

        public void Log<T>(T value, LoggingMessageType type, EscapeType escape, String? format, IFormatProvider? provider)
        {
            Log(value, type, Options, escape, format, provider);
        }

        public void Log<T>(T value, LoggingMessageType type, LoggingMessageOptions options, EscapeType escape, String? format, IFormatProvider? provider)
        {
            DateTimeOffset now = Now(options);
            Behavior.Log(value, type, options, escape, now, format, provider);
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
            if (disposing)
            {
                Behavior.Dispose();
            }
        }

        protected virtual async ValueTask DisposeAsync(Boolean disposing)
        {
            if (disposing)
            {
                await Behavior.DisposeAsync();
            }
        }

        ~Logger()
        {
            Dispose(false);
        }
    }
}