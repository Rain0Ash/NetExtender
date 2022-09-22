// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Threading;
using System.Threading.Tasks;
using NetExtender.Types.Events;
using NetExtender.Types.Timers;
using NetExtender.Types.Timers.Interfaces;
using NetExtender.Utilities.Numerics;

namespace NetExtender.Utilities.Types
{
    public delegate void TickHandler(Object? sender, TimeEventArgs args);
    public delegate void ItemTickHandler<in T>(Object? sender, TimeEventArgs args, T? item);

    public static class TimerUtilities
    {
        public static ITimer Create()
        {
            return Create(Time.Second.One);
        }

        public static ITimer Create(TimeSpan interval)
        {
            return new TimerWrapper(interval);
        }
        
        public static Double CheckInterval(Double interval)
        {
            if (!interval.InRange(0, Int32.MaxValue, MathPositionType.Right))
            {
                throw new ArgumentOutOfRangeException(nameof(interval));
            }

            return interval;
        }

        public static TimeSpan CheckInterval(TimeSpan interval)
        {
            CheckInterval(interval.TotalMilliseconds);
            return interval;
        }

        public static Int32 ToInterval(Double interval)
        {
            return (Int32) Math.Ceiling(CheckInterval(interval));
        }
        
        public static Int32 ToInterval(TimeSpan interval)
        {
            return ToInterval(interval.TotalMilliseconds);
        }

        public static ValueTask<Boolean> WaitForNextTickAsync(this ITimer timer)
        {
            return WaitForNextTickAsync(timer, CancellationToken.None);
        }

        public static ValueTask<Boolean> WaitForNextTickAsync(this ITimer timer, CancellationToken token)
        {
            if (timer is null)
            {
                throw new ArgumentNullException(nameof(timer));
            }

            if (token.IsCancellationRequested)
            {
                return new ValueTask<Boolean>(false);
            }
            
            TaskCompletionSource<Boolean> source = new TaskCompletionSource<Boolean>();

            token.Register(Cancel);
                
            void Cancel()
            {
                timer.Tick -= Tick;
                source.SetResult(false);
            }
            
            void Tick(Object? sender, TimeEventArgs args)
            {
                timer.Tick -= Tick;
                source.SetResult(true);
            }

            timer.Tick += Tick;

            return new ValueTask<Boolean>(source.Task);
        }

        public static Boolean Stop(this Timer timer)
        {
            if (timer is null)
            {
                throw new ArgumentNullException(nameof(timer));
            }

            return timer.Change(Timeout.Infinite, Timeout.Infinite);
        }

        public static Boolean TryChange(this Timer timer, TimeSpan dueTime, TimeSpan period)
        {
            if (timer is null)
            {
                throw new ArgumentNullException(nameof(timer));
            }

            try
            {
                return timer.Change(dueTime, period);
            }
            catch (ObjectDisposedException)
            {
                return false;
            }
        }
        
        public static Boolean TryChange(this Timer timer, Int32 dueTime, Int32 period)
        {
            if (timer is null)
            {
                throw new ArgumentNullException(nameof(timer));
            }

            try
            {
                return timer.Change(dueTime, period);
            }
            catch (ObjectDisposedException)
            {
                return false;
            }
        }
        
        public static Boolean TryChange(this Timer timer, UInt32 dueTime, UInt32 period)
        {
            if (timer is null)
            {
                throw new ArgumentNullException(nameof(timer));
            }

            try
            {
                return timer.Change(dueTime, period);
            }
            catch (ObjectDisposedException)
            {
                return false;
            }
        }
        
        public static Boolean TryChange(this Timer timer, Int64 dueTime, Int64 period)
        {
            if (timer is null)
            {
                throw new ArgumentNullException(nameof(timer));
            }

            try
            {
                return timer.Change(dueTime, period);
            }
            catch (ObjectDisposedException)
            {
                return false;
            }
        }
    }
}