// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

#if NET8_0_OR_GREATER
using System;
using System.Threading;
using System.Threading.Tasks;
using NetExtender.Types.Times.Interfaces;

namespace NetExtender.AspNetCore
{
    public interface ITimeServiceTimeProvider : ITimeProvider
    {
        public new ITimeServiceTimer CreateTimer(TimerCallback callback, Object? state, TimeSpan dueTime, TimeSpan period);
    }
    
    public sealed class TimeProviderWrapper : TimeProvider, ITimeServiceTimeProvider
    {
        private TimeProvider Provider { get; }

        public override TimeZoneInfo LocalTimeZone
        {
            get
            {
                return Provider.LocalTimeZone;
            }
        }

        public override Int64 TimestampFrequency
        {
            get
            {
                return Provider.TimestampFrequency;
            }
        }
        
        public TimeProviderWrapper(TimeProvider provider)
        {
            Provider = provider ?? throw new ArgumentNullException(nameof(provider));
        }

        public override ITimeServiceTimer CreateTimer(TimerCallback callback, Object? state, TimeSpan dueTime, TimeSpan period)
        {
            return new TimeProviderTimerWrapper(Provider, callback, state, dueTime, period);
        }

        NetExtender.Types.Timers.Interfaces.ITimer ITimeProvider.CreateTimer(TimerCallback callback, Object? state, TimeSpan dueTime, TimeSpan period)
        {
            return CreateTimer(callback, state, dueTime, period);
        }

        public override Int64 GetTimestamp()
        {
            return Provider.GetTimestamp();
        }

        public new DateTimeOffset GetLocalNow()
        {
            return Provider.GetLocalNow();
        }

        public override DateTimeOffset GetUtcNow()
        {
            return Provider.GetUtcNow();
        }

        public new TimeSpan GetElapsedTime(Int64 start)
        {
            return Provider.GetElapsedTime(start);
        }

        public new TimeSpan GetElapsedTime(Int64 start, Int64 end)
        {
            return Provider.GetElapsedTime(start, end);
        }

        public override Int32 GetHashCode()
        {
            return Provider.GetHashCode();
        }

        public override Boolean Equals(Object? other)
        {
            return Provider.Equals(other);
        }

        public override String? ToString()
        {
            return Provider.ToString();
        }

        public void Dispose()
        {
        }

        public ValueTask DisposeAsync()
        {
            return ValueTask.CompletedTask;
        }
    }
}
#endif