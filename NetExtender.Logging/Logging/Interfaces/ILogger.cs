// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using NetExtender.Logging.Common;
using NetExtender.Utilities.Types;

namespace NetExtender.Logging.Interfaces
{
    public interface ILogger : ILogger<LoggingMessageLevel>
    {
    }

    public interface ILogger<in TLevel> : ILoggerInfo<TLevel>, IDisposable, IAsyncDisposable where TLevel : unmanaged, Enum
    {
        public void Log<T>(T value, TLevel level);
        public void Log<T>(T value, TLevel level, LoggingMessageOptions options);
        public void Log<T>(T value, TLevel level, EscapeType escape);
        public void Log<T>(T value, TLevel level, LoggingMessageOptions options, EscapeType escape);
        public void Log<T>(T value, TLevel level, IFormatProvider? provider);
        public void Log<T>(T value, TLevel level, LoggingMessageOptions options, IFormatProvider? provider);
        public void Log<T>(T value, TLevel level, EscapeType escape, IFormatProvider? provider);
        public void Log<T>(T value, TLevel level, LoggingMessageOptions options, EscapeType escape, IFormatProvider? provider);
        public void Log<T>(T value, TLevel level, String? format);
        public void Log<T>(T value, TLevel level, LoggingMessageOptions options, String? format);
        public void Log<T>(T value, TLevel level, EscapeType escape, String? format);
        public void Log<T>(T value, TLevel level, LoggingMessageOptions options, EscapeType escape, String? format);
        public void Log<T>(T value, TLevel level, String? format, IFormatProvider? provider);
        public void Log<T>(T value, TLevel level, LoggingMessageOptions options, String? format, IFormatProvider? provider);
        public void Log<T>(T value, TLevel level, EscapeType escape, String? format, IFormatProvider? provider);
        public void Log<T>(T value, TLevel level, LoggingMessageOptions options, EscapeType escape, String? format, IFormatProvider? provider);
    }
}