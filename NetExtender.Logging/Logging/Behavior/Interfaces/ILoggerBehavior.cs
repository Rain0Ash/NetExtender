// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using NetExtender.Logging.Common;
using NetExtender.Utilities.Types;

namespace NetExtender.Logging.Behavior.Interfaces
{
    public interface ILoggerBehavior : IDisposable, IAsyncDisposable
    {
        public Boolean IsThreadSafe { get; }

        public Boolean Log<T>(T value, LoggingMessageType type, LoggingMessageOptions options, EscapeType escape, DateTimeOffset offset, IFormatProvider? provider);
        public Boolean Log<T>(T value, LoggingMessageType type, LoggingMessageOptions options, EscapeType escape, DateTimeOffset offset, String? format, IFormatProvider? provider);
    }
}