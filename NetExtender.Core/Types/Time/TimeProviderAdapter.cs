using System;
#if NET8_0_OR_GREATER
using System.Threading;
using System.Threading.Tasks;
using NetExtender.Types.Timers;
#endif
using NetExtender.Types.Times.Interfaces;

namespace NetExtender.Types.Times
{
    public class TimeProviderAdapter : TimeProvider
#if NET8_0_OR_GREATER
        , ITimeProvider
#endif
    {
        public new static ITimeProvider System
        {
            get
            {
#if NET8_0_OR_GREATER
                return TimeProviderWrapper.System;
#else
                return TimeProvider.System;
#endif
            }
        }

#if NET8_0_OR_GREATER
        public override NetExtender.Types.Timers.Interfaces.ITimer CreateTimer(TimerCallback callback, Object? state, TimeSpan dueTime, TimeSpan period)
        {
            return new TimerThreadingWrapper(callback ?? throw new ArgumentNullException(nameof(callback)), state, dueTime, period);
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
#endif
    }
}