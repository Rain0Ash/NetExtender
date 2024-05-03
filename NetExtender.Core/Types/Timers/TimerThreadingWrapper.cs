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
        
        private DateTimeFactory _factory = DateTimeFactory.Factory;

        public DateTime Now
        {
            get
            {
                return _factory.Now;
            }
        }
        
        public DateTimeKind Kind
        {
            get
            {
                return _factory.Kind;
            }
            set
            {
                _factory.Kind = value;
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

            Tick?.Invoke(timer, new TimeEventArgs(DateTime.Now));
        }

        public Boolean TrySetKind(DateTimeKind kind)
        {
            Kind = kind;
            return true;
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