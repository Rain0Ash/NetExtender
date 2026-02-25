// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

#if NET8_0_OR_GREATER
using System;
using System.Threading;
using System.Threading.Tasks;
using NetExtender.Types.Timers;
using NetExtender.Types.Times.Interfaces;

namespace NetExtender.Types.Times
{
    public class TimeProviderWrapper : TimeProviderWrapperBase
    {
        public new static ITimeProvider System
        {
            get
            {
                return SystemWrapper.Instance;
            }
        }

        protected sealed override TimeProvider Provider { get; }

        public TimeProviderWrapper(TimeProvider provider)
        {
            Provider = provider ?? throw new ArgumentNullException(nameof(provider));
        }

        private sealed class SystemWrapper : TimeProviderWrapperBase
        {
            public static ITimeProvider Instance { get; } = new SystemWrapper();

            protected override TimeProvider Provider
            {
                get
                {
                    return System;
                }
            }
        }
    }

    public abstract class TimeProviderWrapperBase : TimeProvider, ITimeProvider
    {
        protected abstract TimeProvider Provider { get; }

        public sealed override TimeZoneInfo LocalTimeZone
        {
            get
            {
                return Provider.LocalTimeZone;
            }
        }

        public sealed override Int64 TimestampFrequency
        {
            get
            {
                return Provider.TimestampFrequency;
            }
        }

        public override NetExtender.Types.Timers.Interfaces.ITimer CreateTimer(TimerCallback callback, Object? state, TimeSpan dueTime, TimeSpan period)
        {
            return new TimeProviderTimerWrapper(Provider, callback, state, dueTime, period);
        }

        public sealed override Int64 GetTimestamp()
        {
            return Provider.GetTimestamp();
        }

        public new DateTimeOffset GetLocalNow()
        {
            return Provider.GetLocalNow();
        }

        public sealed override DateTimeOffset GetUtcNow()
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