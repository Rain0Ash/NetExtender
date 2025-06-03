// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

#if NET8_0_OR_GREATER
using System;
using System.Threading;
using System.Threading.Tasks;
using NetExtender.Types.Events;
using NetExtender.Types.Exceptions;
using NetExtender.Types.Times.Interfaces;
using NetExtender.Utilities.Types;

namespace NetExtender.AspNetCore
{
    public interface ITimeServiceTimer : NetExtender.Types.Timers.Interfaces.ITimer, ITimer
    {
        public new Boolean Change(TimeSpan dueTime, TimeSpan period);
    }
    
    public sealed class TimeProviderTimerWrapper : ITimeServiceTimer
    {
        private ITimeProvider Provider { get; }
        private ITimer? Timer { get; set; }
        private TimerCallback? Callback { get; }
        public event TickHandler? Tick;
        public Boolean IsStarted { get; private set; }

        public DateTime Now
        {
            get
            {
                return Kind switch
                {
                    DateTimeKind.Unspecified => Provider.GetLocalNow().LocalDateTime,
                    DateTimeKind.Utc => Provider.GetUtcNow().UtcDateTime,
                    DateTimeKind.Local => Provider.GetLocalNow().LocalDateTime,
                    _ => throw new EnumUndefinedOrNotSupportedException<DateTimeKind>(Kind, nameof(Kind), null)
                };
            }
        }

        public DateTimeKind Kind { get; set; }

        private TimeSpan _interval = Time.Second.One;
        public TimeSpan Interval
        {
            get
            {
                return _interval;
            }
            set
            {
                if (_interval == value)
                {
                    return;
                }

                _interval = TimerUtilities.CheckInterval(value);

                if (IsStarted && Timer is not null)
                {
                    Timer.Change(Interval, Interval);
                }
            }
        }
        
#if NET8_0_OR_GREATER
        public TimeProviderTimerWrapper(TimeProvider provider, TimerCallback callback, Object? state, TimeSpan dueTime, TimeSpan period)
            : this(callback ?? throw new ArgumentNullException(nameof(callback)), provider is not null ? (ITimeProvider) new TimeProviderWrapper(provider) : throw new ArgumentNullException(nameof(provider)), state, dueTime, period)
        {
        }
#endif

        public TimeProviderTimerWrapper(ITimeProvider provider, TimerCallback callback, Object? state, TimeSpan dueTime, TimeSpan period)
            : this(callback, provider, state, dueTime, period)
        {
        }

        private TimeProviderTimerWrapper(TimerCallback callback, ITimeProvider provider, Object? state, TimeSpan dueTime, TimeSpan period)
        {
            Provider = provider ?? throw new ArgumentNullException(nameof(provider));
            Callback = callback ?? throw new ArgumentNullException(nameof(callback));
            IsStarted = true;
            Timer = (ITimer) provider.CreateTimer(OnTick, state, dueTime, period);
        }

        private void OnTick(Object? state)
        {
            if (!IsStarted)
            {
                return;
            }

            ITimer? timer = Timer;

            if (timer is null)
            {
                return;
            }

            Callback?.Invoke(state);
            Tick?.Invoke(timer, new TimeEventArgs(Now));
        }

        public Boolean TrySetKind(DateTimeKind kind)
        {
            Kind = kind;
            return true;
        }

        public Boolean Change(TimeSpan dueTime, TimeSpan period)
        {
            ITimer timer = Timer ?? throw new ObjectDisposedException(nameof(TimeProviderTimerWrapper));
            return timer.Change(dueTime, period);
        }

        public void Start()
        {
            ITimer timer = Timer ?? throw new ObjectDisposedException(nameof(TimeProviderTimerWrapper));
            timer.Change(Interval, Interval);
            IsStarted = true;
        }

        public void Stop()
        {
            ITimer timer = Timer ?? throw new ObjectDisposedException(nameof(TimeProviderTimerWrapper));
            timer.Change(Timeout.InfiniteTimeSpan, Timeout.InfiniteTimeSpan);
            IsStarted = false;
        }

        public void Dispose()
        {
            Timer?.Dispose();
        }

        public ValueTask DisposeAsync()
        {
            return Timer?.DisposeAsync() ?? ValueTask.CompletedTask;
        }
    }
}
#endif