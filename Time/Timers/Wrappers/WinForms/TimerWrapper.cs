// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Threading.Tasks;
using System.Windows.Forms;
using NetExtender.Events.Args;
using NetExtender.Times.Timers.Interfaces;
using NetExtender.Utils.Types;

namespace NetExtender.Times.Timers.Wrappers
{
    public sealed class TimerWrapperWinForms : ITimer
    {
        public static implicit operator TimerWrapperWinForms(Timer timer)
        {
            return new TimerWrapperWinForms(timer);
        }
        
        public static implicit operator Timer(TimerWrapperWinForms wrapper)
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
                _timer.Interval = TimerUtils.ToInterval(value);
            }
        }

        public event TickHandler Tick;

        public TimerWrapperWinForms(Int32 interval)
            : this(new Timer { Interval = interval })
        {
        }
        
        public TimerWrapperWinForms(Double interval)
            : this(TimerUtils.ToInterval(interval))
        {
        }
        
        public TimerWrapperWinForms(TimeSpan interval)
            : this(interval.TotalMilliseconds)
        {
        }

        public TimerWrapperWinForms(Timer timer)
        {
            _timer = timer ?? throw new ArgumentNullException(nameof(timer));
            _timer.Tick += OnTick;
        }

        private void OnTick(Object sender, EventArgs _)
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