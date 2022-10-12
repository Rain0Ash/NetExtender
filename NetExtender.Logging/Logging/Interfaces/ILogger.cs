// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using NetExtender.Logging.Common;
using NetExtender.Utilities.Types;

namespace NetExtender.Logging.Interfaces
{
    public interface ILogger : ILoggerInfo, IDisposable, IAsyncDisposable
    {
        public void Log<T>(T value, LoggingMessageType type);
        public void Log<T>(T value, LoggingMessageType type, LoggingMessageOptions options);
        public void Log<T>(T value, LoggingMessageType type, EscapeType escape);
        public void Log<T>(T value, LoggingMessageType type, LoggingMessageOptions options, EscapeType escape);
        public void Log<T>(T value, LoggingMessageType type, IFormatProvider? provider);
        public void Log<T>(T value, LoggingMessageType type, LoggingMessageOptions options, IFormatProvider? provider);
        public void Log<T>(T value, LoggingMessageType type, EscapeType escape, IFormatProvider? provider);
        public void Log<T>(T value, LoggingMessageType type, LoggingMessageOptions options, EscapeType escape, IFormatProvider? provider);
        public void Log<T>(T value, LoggingMessageType type, String? format);
        public void Log<T>(T value, LoggingMessageType type, LoggingMessageOptions options, String? format);
        public void Log<T>(T value, LoggingMessageType type, EscapeType escape, String? format);
        public void Log<T>(T value, LoggingMessageType type, LoggingMessageOptions options, EscapeType escape, String? format);
        public void Log<T>(T value, LoggingMessageType type, String? format, IFormatProvider? provider);
        public void Log<T>(T value, LoggingMessageType type, LoggingMessageOptions options, String? format, IFormatProvider? provider);
        public void Log<T>(T value, LoggingMessageType type, EscapeType escape, String? format, IFormatProvider? provider);
        public void Log<T>(T value, LoggingMessageType type, LoggingMessageOptions options, EscapeType escape, String? format, IFormatProvider? provider);
    }
}