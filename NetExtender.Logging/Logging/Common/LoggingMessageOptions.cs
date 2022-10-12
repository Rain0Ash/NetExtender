// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;

namespace NetExtender.Logging.Common
{
    [Flags]
    public enum LoggingMessageOptions
    {
        None = 0,
        Utc = 1,
        Date = 2,
        UtcDate = Date | Utc,
        Time = 4,
        UtcTime = Time | Utc,
        DateTime = Date | Time,
        UtcDateTime = DateTime | Utc,
        Prefix = 8,
        Color = 16,
        All = Color | Prefix | DateTime
    }
}