// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Threading;
using NetExtender.Events;
using NetExtender.Times.Timers.Interfaces;
using NetExtender.Types.Timers;
using NetExtender.Utils.Numerics;
using NetExtender.Utils.Static;

namespace NetExtender.Utils.Types
{
    public delegate void TickHandler(Object? sender, TimeEventArgs args);
    public delegate void ItemTickHandler<in T>(Object? sender, TimeEventArgs args, T? item);

    public static class TimerUtils
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

        public static Boolean Stop(this Timer timer)
        {
            if (timer is null)
            {
                throw new ArgumentNullException(nameof(timer));
            }

            return timer.Change(Timeout.Infinite, Timeout.Infinite);
        }
    }
}