// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Diagnostics.CodeAnalysis;
using System.Threading;
using System.Threading.Tasks;
using NetExtender.Times.Timers.Interfaces;
using NetExtender.Types.Events;
using NetExtender.Utilities.Types;

namespace NetExtender.Types.Timers
{
    public sealed class TimerThreadingWrapper : ITimer
    {
        [return: NotNullIfNotNull("wrapper")]
        public static explicit operator Timer?(TimerThreadingWrapper? wrapper)
        {
            return wrapper?._timer;
        }
        
        private readonly Timer _timer;
        
        public Boolean IsStarted { get; private set; }
        
        public event TickHandler Tick = null!;

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
            }
        }

        public TimerThreadingWrapper()
        {
            _timer = new Timer(OnTick);
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
            interval = TimerUtilities.CheckInterval(interval);
            
            _timer = new Timer(OnTick);
            Interval = interval;
        }

        private void OnTick(Object? state)
        {
            if (!IsStarted)
            {
                return;
            }
            
            Tick?.Invoke(_timer, new TimeEventArgs());
            _timer.Change(TimeSpan.Zero, Interval);
        }
        
        public void Start()
        {
            _timer.Change(TimeSpan.Zero, Interval);
            IsStarted = true;
        }

        public void Stop()
        {
            _timer.Stop();
            IsStarted = false;
        }

        public void Dispose()
        {
            _timer.Dispose();
        }
        
        public ValueTask DisposeAsync()
        {
            return _timer.DisposeAsync();
        }
    }
}