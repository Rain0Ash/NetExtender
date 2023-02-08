// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using NetExtender.Logging.Common;
using NetExtender.Utilities.Types;

namespace NetExtender.Logging.Interfaces
{
    public interface ILoggerInfo<in TLevel> : ILoggerInfo where TLevel : unmanaged, Enum
    {
        public Boolean IsEnabled(TLevel level);
        public Boolean Enable(TLevel level);
        public Boolean Disable(TLevel level);
    }
    
    public interface ILoggerInfo
    {
        public LoggingMessageOptions Options { get; }
        public EscapeType Escape { get; }
        public TimeSpan Offset { get; }
        public IFormatProvider? Provider { get; }
        public Boolean IsUtc { get; }
        public Boolean IsDate { get; }
        public Boolean IsUtcDate { get; }
        public Boolean IsTime { get; }
        public Boolean IsUtcTime { get; }
        public Boolean IsDateTime { get; }
        public Boolean IsUtcDateTime { get; }
        public Boolean IsPrefix { get; }
        public Boolean IsColor { get; }
        public Boolean IsThreadSafe { get; }
    }
}