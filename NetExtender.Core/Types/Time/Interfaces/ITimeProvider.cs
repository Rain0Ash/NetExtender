// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Threading;
using NetExtender.Types.Timers.Interfaces;

namespace NetExtender.Types.Times.Interfaces
{
    public interface ITimeProvider : IDisposable, IAsyncDisposable
    {
        public TimeZoneInfo LocalTimeZone { get; }
        public Int64 TimestampFrequency { get; }

        public ITimer CreateTimer(TimerCallback callback, Object? state, TimeSpan dueTime, TimeSpan period);
        public Int64 GetTimestamp();
        public DateTimeOffset GetUtcNow();
        public DateTimeOffset GetLocalNow();
        public TimeSpan GetElapsedTime(Int64 start);
        public TimeSpan GetElapsedTime(Int64 start, Int64 end);
    }
}