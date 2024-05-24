// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;
using System.Windows.Forms;
using NetExtender.Types.Events;
using NetExtender.Types.Timers.Interfaces;
using NetExtender.Types.Times;
using NetExtender.Utilities.Types;

namespace NetExtender.Types.Timers
{
    public sealed class TimerWinFormsWrapper : ITimer
    {
        [return: NotNullIfNotNull("timer")]
        public static implicit operator TimerWinFormsWrapper?(Timer? timer)
        {
            return timer is not null ? new TimerWinFormsWrapper(timer) : null;
        }

        [return: NotNullIfNotNull("wrapper")]
        public static implicit operator Timer?(TimerWinFormsWrapper? wrapper)
        {
            return wrapper?.Timer;
        }

        private Timer Timer { get; }
        public event TickHandler? Tick;

        public Boolean IsStarted
        {
            get
            {
                return Timer.Enabled;
            }
            set
            {
                Timer.Enabled = value;
            }
        }

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

        public TimeSpan Interval
        {
            get
            {
                return TimeSpan.FromMilliseconds(Timer.Interval);
            }
            set
            {
                Timer.Interval = TimerUtilities.ToInterval(value);
            }
        }

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
            Timer = timer ?? throw new ArgumentNullException(nameof(timer));
            Timer.Tick += OnTick;
        }

        private void OnTick(Object? sender, EventArgs _)
        {
            Tick?.Invoke(sender, new TimeEventArgs(Now));
        }
        
        public Boolean TrySetKind(DateTimeKind kind)
        {
            Kind = kind;
            return true;
        }

        public void Start()
        {
            Timer.Start();
        }

        public void Stop()
        {
            Timer.Stop();
        }

        public void Dispose()
        {
            Timer.Tick -= OnTick;
            Timer.Dispose();
        }

        public ValueTask DisposeAsync()
        {
            Dispose();
            return ValueTask.CompletedTask;
        }

        public override Boolean Equals(Object? other)
        {
            return ReferenceEquals(this, other) || Timer.Equals(other);
        }

        public override Int32 GetHashCode()
        {
            return Timer.GetHashCode();
        }

        public override String ToString()
        {
            return Timer.ToString();
        }
    }
}