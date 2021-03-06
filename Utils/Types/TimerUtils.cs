// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using NetExtender.Events.Args;
using NetExtender.Times;
using NetExtender.Times.Timers.Interfaces;
using NetExtender.Times.Timers.Wrappers;
using NetExtender.Times.Timers.Wrappers.Threading;
using NetExtender.Utils.Numerics;

namespace NetExtender.Utils.Types
{
    public delegate void TickHandler(Object sender, TimeEventArgs args);
    public delegate void ItemTickHandler<in T>(Object sender, TimeEventArgs args, T item);

    public enum TimerType
    {
        Default,
        Accuracy,
        WinForms,
        Threading
    }
    
    public static class TimerUtils
    {
        public static ITimer Create()
        {
            return Create(TimerType.Default);
        }
        
        public static ITimer Create(TimeSpan interval)
        {
            return Create(TimerType.Default, interval);
        }

        public static ITimer Create(this TimerType type)
        {
            return Create(type, Time.Second.One);
        }

        public static ITimer Create(this TimerType type, TimeSpan interval)
        {
            return type switch
            {
                TimerType.Default => new TimerWrapper(interval),
                TimerType.WinForms => new TimerWrapperWinForms(interval),
                TimerType.Threading => new TimerWrapperThreading(interval),
                _ => throw new NotImplementedException()
            };
        }
        
        internal static Double CheckInterval(Double interval)
        {
            if (!interval.InRange(0, Int32.MaxValue, MathPositionType.Right))
            {
                throw new ArgumentOutOfRangeException(nameof(interval));
            }

            return interval;
        }

        internal static TimeSpan CheckInterval(TimeSpan interval)
        {
            CheckInterval(interval.TotalMilliseconds);
            return interval;
        }

        internal static Int32 ToInterval(Double interval)
        {
            return (Int32) Math.Ceiling(CheckInterval(interval));
        }
        
        internal static Int32 ToInterval(TimeSpan interval)
        {
            return ToInterval(interval.TotalMilliseconds);
        }

        public static Boolean Stop(this System.Threading.Timer timer)
        {
            if (timer is null)
            {
                throw new ArgumentNullException(nameof(timer));
            }

            return timer.Change(System.Threading.Timeout.Infinite, System.Threading.Timeout.Infinite);
        }
    }
}