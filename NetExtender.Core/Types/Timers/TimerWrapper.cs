// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;
using System.Timers;
using NetExtender.Types.Timers.Interfaces;
using NetExtender.Types.Times;
using NetExtender.Utilities.Types;

namespace NetExtender.Types.Timers
{
    public sealed class TimerWrapper : ITimer
    {
        [return: NotNullIfNotNull("timer")]
        public static implicit operator TimerWrapper?(Timer? timer)
        {
            return timer is not null ? new TimerWrapper(timer) : null;
        }

        [return: NotNullIfNotNull("wrapper")]
        public static implicit operator Timer?(TimerWrapper? wrapper)
        {
            return wrapper?.Timer;
        }

        private Timer Timer { get; }
        public event TickHandler? Tick;
        
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

        public TimeSpan Interval
        {
            get
            {
                return TimeSpan.FromMilliseconds(Timer.Interval);
            }
            set
            {
                Timer.Interval = TimerUtilities.CheckInterval(value.TotalMilliseconds);
            }
        }

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
            Timer = timer ?? throw new ArgumentNullException(nameof(timer));
            Timer.Elapsed += OnTick;
        }

        private void OnTick(Object? sender, ElapsedEventArgs args)
        {
            args.SetSignalTime(args.SignalTime.ToKind(Kind));
            Tick?.Invoke(sender, args);
        }

        public Boolean TrySetKind(DateTimeKind kind)
        {
            try
            {
                Kind = kind;
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public Boolean Change(TimeSpan dueTime, TimeSpan period)
        {
            return false;
        }

        public void Start()
        {
            Timer.Start();
        }

        public void Stop()
        {
            Timer.Stop();
        }

        public override Int32 GetHashCode()
        {
            return Timer.GetHashCode();
        }

        public override Boolean Equals(Object? other)
        {
            return ReferenceEquals(this, other) || Timer.Equals(other);
        }

        public override String ToString()
        {
            return Timer.ToString();
        }

        public void Dispose()
        {
            Timer.Elapsed -= OnTick;
            Timer.Dispose();
        }

        public ValueTask DisposeAsync()
        {
            Dispose();
            return ValueTask.CompletedTask;
        }
    }
}