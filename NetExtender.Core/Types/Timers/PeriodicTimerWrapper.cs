// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Threading;
using System.Threading.Tasks;
using NetExtender.Types.Timers.Interfaces;
using NetExtender.Utilities.Types;

namespace NetExtender.Types.Timers
{
#if !NET6_0_OR_GREATER
    public sealed class PeriodicTimer : IDisposable
    {
        private TimerWrapper Timer { get; }
        
        public PeriodicTimer(TimeSpan interval)
        {
            Timer = new TimerWrapper(interval);
        }
        
        public ValueTask<Boolean> WaitForNextTickAsync()
        {
            return Timer.WaitForNextTickAsync();
        }

        public ValueTask<Boolean> WaitForNextTickAsync(CancellationToken token)
        {
            return Timer.WaitForNextTickAsync(token);
        }
        
        public void Dispose()
        {
            Timer.Dispose();
        }
    }
#endif
    
    public sealed class PeriodicTimerWrapper : ITimer
    {
        private TimerWrapper Timer { get; }
        
        public event TickHandler? Tick
        {
            add
            {
                Timer.Tick += value;
            }
            remove
            {
                Timer.Tick -= value;
            }
        }
        
        public Boolean IsStarted
        {
            get
            {
                return Timer.IsStarted;
            }
        }

        public TimeSpan Interval
        {
            get
            {
                return Timer.Interval;
            }
            set
            {
                Timer.Interval = value;
            }
        }
        
        public PeriodicTimerWrapper(Int32 interval)
        {
            Timer = new TimerWrapper(interval);
        }
        
        public PeriodicTimerWrapper(Double interval)
        {
            Timer = new TimerWrapper(interval);
        }

        public PeriodicTimerWrapper(TimeSpan interval)
        {
            Timer = new TimerWrapper(interval);
        }
        
        public ValueTask<Boolean> WaitForNextTickAsync()
        {
            return Timer.WaitForNextTickAsync();
        }

        public ValueTask<Boolean> WaitForNextTickAsync(CancellationToken token)
        {
            return Timer.WaitForNextTickAsync(token);
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
            Timer.Dispose();
        }

        public ValueTask DisposeAsync()
        {
            return Timer.DisposeAsync();
        }
    }
}