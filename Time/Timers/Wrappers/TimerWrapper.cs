// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Threading.Tasks;
using System.Timers;
using NetExtender.Times.Timers.Interfaces;
using NetExtender.Utils.Types;

namespace NetExtender.Times.Timers.Wrappers
{
    public sealed class TimerWrapper : ITimer
    {
        public static implicit operator TimerWrapper(Timer timer)
        {
            return new TimerWrapper(timer);
        }
        
        public static implicit operator Timer(TimerWrapper wrapper)
        {
            return wrapper._timer;
        }
        
        private readonly Timer _timer;
        
        public Boolean IsStarted
        {
            get
            {
                return _timer.Enabled;
            }
            set
            {
                _timer.Enabled = value;
            }
        }

        public TimeSpan Interval
        {
            get
            {
                return TimeSpan.FromMilliseconds(_timer.Interval);
            }
            set
            {
                _timer.Interval = TimerUtils.CheckInterval(value.TotalMilliseconds);
            }
        }

        public event TickHandler Tick;

        public TimerWrapper(Int32 interval)
            : this((Double) interval)
        {
        }
        
        public TimerWrapper(Double interval)
            : this(new Timer(interval))
        {
        }

        public TimerWrapper(TimeSpan interval)
            : this(interval.TotalMilliseconds)
        {
        }
        
        public TimerWrapper(Timer timer)
        {
            _timer = timer ?? throw new ArgumentNullException(nameof(timer));
            _timer.Elapsed += OnTick;
        }

        private void OnTick(Object sender, ElapsedEventArgs e)
        {
            Tick?.Invoke(sender, e);
        }

        public void Start()
        {
            _timer.Start();
        }

        public void Stop()
        {
            _timer.Stop();
        }

        public void Dispose()
        {
            _timer.Elapsed -= OnTick;
            _timer.Dispose();
        }

        public ValueTask DisposeAsync()
        {
            Dispose();
            return ValueTask.CompletedTask;
        }

        public override Boolean Equals(Object obj)
        {
            return ReferenceEquals(this, obj) || _timer.Equals(obj);
        }

        public override Int32 GetHashCode()
        {
            return _timer.GetHashCode();
        }

        public override String ToString()
        {
            return _timer.ToString();
        }
    }
}