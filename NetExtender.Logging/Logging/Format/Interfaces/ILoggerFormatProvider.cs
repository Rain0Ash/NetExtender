// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using NetExtender.Logging.Common;

namespace NetExtender.Logging.Format.Interfaces
{
    public interface ILoggerFormatProvider<in TLevel> where TLevel : unmanaged, Enum
    {
        public String? Format(String? message, TLevel level, LoggingMessageOptions options, DateTimeOffset offset);
        public String? Format(String? message, TLevel level, LoggingMessageOptions options, DateTimeOffset offset, IFormatProvider? provider);
    }
}