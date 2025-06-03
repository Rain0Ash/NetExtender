// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Runtime.CompilerServices;
using NetExtender.Types.Exceptions;

namespace NetExtender.Utilities.Types
{
    public enum TimeType
    {
        Milliseconds,
        Seconds,
        Minutes,
        Hours,
        Days
    }

    public static class TimeUtilities
    {
        public static readonly DateTime Epoch = DateTimeUtilities.Epoch;
        public static readonly DateTimeOffset OffsetEpoch = Epoch;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static TimeSpan SinceEpoch(this DateTime time)
        {
            return time - Epoch;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static TimeSpan SinceEpoch(this DateTimeOffset time)
        {
            return time - OffsetEpoch;
        }
        
        /// <summary>
        /// Return unix time in seconds
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Int64 UnixTime()
        {
            return (Int64) (DateTime.Now - Epoch).TotalSeconds;
        }
        
        /// <summary>
        /// Return unix time in seconds
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Int64 UnixUtcTime()
        {
            return (Int64) (DateTime.UtcNow - Epoch).TotalSeconds;
        }

        /// <summary>
        /// Return unix time in seconds
        /// </summary>
        /// <param name="type">Return unix time in this date type</param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Int64 UnixTime(TimeType type)
        {
            return (Int64) GetTime(type);
        }

        public static Double GetTime(TimeType type)
        {
            TimeSpan time = DateTime.Now - Epoch;

            return type switch
            {
                TimeType.Milliseconds => time.TotalMilliseconds,
                TimeType.Seconds => time.TotalSeconds,
                TimeType.Minutes => time.TotalMinutes,
                TimeType.Hours => time.TotalHours,
                TimeType.Days => time.TotalDays,
                _ => throw new EnumUndefinedOrNotSupportedException<TimeType>(type, nameof(type), null)
            };
        }

        public static Double GetUtcTime(TimeType type)
        {
            TimeSpan time = DateTime.UtcNow - Epoch;

            return type switch
            {
                TimeType.Milliseconds => time.TotalMilliseconds,
                TimeType.Seconds => time.TotalSeconds,
                TimeType.Minutes => time.TotalMinutes,
                TimeType.Hours => time.TotalHours,
                TimeType.Days => time.TotalDays,
                _ => throw new EnumUndefinedOrNotSupportedException<TimeType>(type, nameof(type), null)
            };
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static DateTime UnixTime(Int64 time)
        {
            return time <= 0 ? DateTime.MinValue : Epoch.AddSeconds(time);
        }

        public static Int64 UnixTime(this DateTime time)
        {
            if (time <= DateTime.MinValue)
            {
                return 0;
            }

            if (time < Epoch)
            {
                return 0;
            }

            TimeSpan timeSpan = time - Epoch;
            return (Int64) timeSpan.TotalSeconds;
        }
    }
}