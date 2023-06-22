// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using NetExtender.Logging.Common;
using NetExtender.Types.Behavior.Interfaces;
using NetExtender.Utilities.Types;

namespace NetExtender.Logging.Behavior.Interfaces
{
    public interface ILoggerBehavior<in TLevel> : IBehavior, IDisposable, IAsyncDisposable where TLevel : unmanaged, Enum
    {
        public Boolean Log<T>(T value, TLevel level, LoggingMessageOptions options, EscapeType escape, DateTimeOffset offset, IFormatProvider? provider);
        public Boolean Log<T>(T value, TLevel level, LoggingMessageOptions options, EscapeType escape, DateTimeOffset offset, String? format, IFormatProvider? provider);
        public Boolean IsEnabled(TLevel level);
        public Boolean Enable(TLevel level);
        public Boolean Disable(TLevel level);
    }
}