// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Diagnostics.CodeAnalysis;
using System.Threading;
using System.Threading.Tasks;
using NetExtender.Types.Events;
using NetExtender.Types.Timers.Interfaces;
using NetExtender.Types.Times;
using NetExtender.Utilities.Types;

namespace NetExtender.Types.Timers
{
    public sealed class TimerThreadingWrapper : ITimer
    {
        [return: NotNullIfNotNull("wrapper")]
        public static explicit operator Timer?(TimerThreadingWrapper? wrapper)
        {
            return wrapper?.Timer;
        }

        private Timer? Timer { get; set; }
        public event TickHandler? Tick;
        
        public Boolean IsStarted { get; private set; }
        
        private DateTimeProvider _provider = DateTimeProvider.Provider;

        public DateTime Now
        {
            get
            {
                return _provider.Now;
            }
        }
        
        public DateTimeKind Kind
        {
            get
            {
                return _provider.Kind;
            }
            set
            {
                _provider.Kind = value;
            }
        }

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

        public TimerThreadingWrapper()
        {
            Timer = new Timer(OnTick);
        }

        public TimerThreadingWrapper(Int32 interval)
            : this((Double) interval)
        {
        }

        public TimerThreadingWrapper(Double interval)
            : this(TimeSpan.FromMilliseconds(TimerUtilities.CheckInterval(interval)))
        {
        }

        public TimerThreadingWrapper(TimeSpan interval)
        {
            Interval = TimerUtilities.CheckInterval(interval);
            Timer = new Timer(OnTick, null, Timeout.Infinite, Timeout.Infinite);
        }

        public TimerThreadingWrapper(TimerCallback callback, Object? state, TimeSpan dueTime, TimeSpan period)
            : this(callback, state, dueTime, period, null)
        {
        }

        public TimerThreadingWrapper(TimerCallback callback, Object? state, TimeSpan dueTime, TimeSpan period, TickHandler? handler)
        {
            Interval = TimerUtilities.CheckInterval(period);
            Timer = new Timer(item => { callback.Invoke(item); OnTick(item); }, state, Timeout.Infinite, Timeout.Infinite);

            if (handler is not null)
            {
                Tick += handler;
            }
            
            try
            {
                Timer.Change(dueTime, period);
                IsStarted = true;
            }
            catch (Exception)
            {
                Tick = null;
                Timer.Dispose();
                throw;
            }
        }

        private void OnTick(Object? state)
        {
            if (!IsStarted)
            {
                return;
            }

            Timer? timer = Timer;

            if (timer is null)
            {
                return;
            }

            Tick?.Invoke(timer, new TimeEventArgs(Now));
        }

        public Boolean TrySetKind(DateTimeKind kind)
        {
            Kind = kind;
            return true;
        }

        public Boolean Change(TimeSpan dueTime, TimeSpan period)
        {
            Timer timer = Timer ?? throw new ObjectDisposedException(nameof(TimerThreadingWrapper));
            return timer.Change(dueTime, period);
        }

        public void Start()
        {
            Timer timer = Timer ?? throw new ObjectDisposedException(nameof(TimerThreadingWrapper));
            timer.Change(Interval, Interval);
            IsStarted = true;
        }

        public void Stop()
        {
            Timer timer = Timer ?? throw new ObjectDisposedException(nameof(TimerThreadingWrapper));
            timer.Change(Timeout.Infinite, Timeout.Infinite);
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