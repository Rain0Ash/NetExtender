// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Threading;
using System.Threading.Tasks;
using NetExtender.Types.Events;
using NetExtender.Types.Timers.Interfaces;
using NetExtender.Types.Times;
using NetExtender.Utilities.Types;

namespace NetExtender.Types.Timers
{
    public sealed class DebounceTimer : IDebounceTimer
    {
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
                
                if (IsStarted)
                {
                    Debounce();
                }
            }
        }

        public DebounceTimer()
        {
            Timer = new Timer(OnTick);
        }

        public DebounceTimer(Int32 interval)
            : this(TimeSpan.FromMilliseconds(TimerUtilities.CheckInterval(interval)))
        {
        }

        public DebounceTimer(Double interval)
            : this(TimeSpan.FromMilliseconds(TimerUtilities.CheckInterval(interval)))
        {
        }

        public DebounceTimer(TimeSpan interval)
        {
            Interval = TimerUtilities.CheckInterval(interval);
            Timer = new Timer(OnTick);
        }

        private void OnTick(Object? state)
        {
            Timer?.Change(Timeout.Infinite, Timeout.Infinite);
            Tick?.Invoke(this, new TimeEventArgs(Now));
        }
        
        public Boolean TrySetKind(DateTimeKind kind)
        {
            Kind = kind;
            return true;
        }

        public void Debounce()
        {
            if (Timer is null)
            {
                throw new ObjectDisposedException(nameof(DebounceTimer));
            }
            
            Timer.Change(Interval, Timeout.InfiniteTimeSpan);
        }

        public void Debounce(TimeSpan interval)
        {
            Interval = TimerUtilities.CheckInterval(interval);
            Debounce();
        }

        public void Start()
        {
            if (Timer is null)
            {
                throw new ObjectDisposedException(nameof(DebounceTimer));
            }

            Timer.Change(Interval, Timeout.InfiniteTimeSpan);
            IsStarted = true;
        }

        public void Stop()
        {
            if (Timer is null)
            {
                throw new ObjectDisposedException(nameof(DebounceTimer));
            }

            Timer.Change(Timeout.Infinite, Timeout.Infinite);
            IsStarted = false;
        }

        public void Dispose()
        {
            Timer?.Dispose();
            Timer = null;
        }

        public ValueTask DisposeAsync()
        {
            return Timer?.DisposeAsync() ?? ValueTask.CompletedTask;
        }
    }
}