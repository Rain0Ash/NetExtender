// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

#if !NET8_0_OR_GREATER
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Threading;
using System.Threading.Tasks;
using NetExtender.Types.Timers;
using NetExtender.Types.Timers.Interfaces;
using NetExtender.Types.Times.Interfaces;

namespace System
{
    public class TimeProvider : ITimeProvider
    {
        public static TimeProvider System { get; } = new SystemTimeProvider();
        
        private static readonly Int64 minimum = DateTime.MinValue.Ticks;
        private static readonly Int64 maximum = DateTime.MaxValue.Ticks;

        [SuppressMessage("ReSharper", "NullCoalescingConditionIsAlwaysNotNullAccordingToAPIContract")]
        public virtual TimeZoneInfo LocalTimeZone
        {
            get
            {
                return TimeZoneInfo.Local ?? TimeZoneInfo.Utc;
            }
        }

        public virtual Int64 TimestampFrequency
        {
            get
            {
                return Stopwatch.Frequency;
            }
        }

        public virtual ITimer CreateTimer(TimerCallback callback, Object? state, TimeSpan dueTime, TimeSpan period)
        {
            return new TimerThreadingWrapper(callback ?? throw new ArgumentNullException(nameof(callback)), state, dueTime, period);
        }

        public virtual Int64 GetTimestamp()
        {
            return Stopwatch.GetTimestamp();
        }

        public virtual DateTimeOffset GetUtcNow()
        {
            return DateTimeOffset.UtcNow;
        }

        public DateTimeOffset GetLocalNow()
        {
            DateTimeOffset now = GetUtcNow();
            TimeZoneInfo zone = LocalTimeZone;
            TimeSpan offset = zone.GetUtcOffset(now);
            
            if (offset.Ticks <= 0)
            {
                return now;
            }

            unchecked
            {
                Int64 ticks = now.Ticks + offset.Ticks;
                if ((UInt64) ticks > (UInt64) maximum)
                {
                    ticks = ticks < minimum ? minimum : maximum;
                }

                return new DateTimeOffset(ticks, offset);
            }
        }

        public TimeSpan GetElapsedTime(Int64 start)
        {
            return GetElapsedTime(start, GetTimestamp());
        }

        public TimeSpan GetElapsedTime(Int64 start, Int64 end)
        {
            Int64 frequency = TimestampFrequency;
            if (frequency <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(frequency), frequency, "Frequency must be greater than zero.");
            }

            return new TimeSpan((Int64) ((end - start) * (10000000.0 / frequency)));
        }

        public virtual void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(Boolean disposing)
        {
        }

        public virtual ValueTask DisposeAsync()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
            return ValueTask.CompletedTask;
        }

        private sealed class SystemTimeProvider : TimeProvider
        {
        }
    }
}
#endif