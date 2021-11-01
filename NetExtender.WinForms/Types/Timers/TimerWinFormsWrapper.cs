// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Threading.Tasks;
using System.Windows.Forms;
using NetExtender.Times.Timers.Interfaces;
using NetExtender.Types.Events;
using NetExtender.Utilities.Types;

namespace NetExtender.Types.Timers
{
    public sealed class TimerWinFormsWrapper : ITimer
    {
        public static implicit operator TimerWinFormsWrapper(Timer timer)
        {
            return new TimerWinFormsWrapper(timer);
        }
        
        public static implicit operator Timer(TimerWinFormsWrapper wrapper)
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
                _timer.Interval = TimerUtilities.ToInterval(value);
            }
        }

        public event TickHandler Tick = null!;

        public TimerWinFormsWrapper(Int32 interval)
            : this(new Timer { Interval = interval })
        {
        }
        
        public TimerWinFormsWrapper(Double interval)
            : this(TimerUtilities.ToInterval(interval))
        {
        }
        
        public TimerWinFormsWrapper(TimeSpan interval)
            : this(interval.TotalMilliseconds)
        {
        }

        public TimerWinFormsWrapper(Timer timer)
        {
            _timer = timer ?? throw new ArgumentNullException(nameof(timer));
            _timer.Tick += OnTick;
        }

        private void OnTick(Object? sender, EventArgs _)
        {
            Tick?.Invoke(sender, new TimeEventArgs());
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
            _timer.Tick -= OnTick;
            _timer.Dispose();
        }
        
        public ValueTask DisposeAsync()
        {
            Dispose();
            return ValueTask.CompletedTask;
        }

        public override Boolean Equals(Object? obj)
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