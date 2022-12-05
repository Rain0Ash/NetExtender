// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
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
        private static readonly DateTime UnixDate = new DateTime(1970, 1, 1, 0, 0, 0);

        /// <summary>
        /// Return unix time in seconds
        /// </summary>
        public static Int64 UnixTime()
        {
            return (Int64) (DateTime.Now - UnixDate).TotalSeconds;
        }

        /// <summary>
        /// Return unix time in seconds
        /// </summary>
        /// <param name="type">Return unix time in this date type</param>
        public static Int64 UnixTime(TimeType type)
        {
            return (Int64) GetTime(type);
        }

        public static Double GetTime(TimeType type)
        {
            TimeSpan time = DateTime.Now - UnixDate;

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

        public static Int64 UnixTime(this DateTime time)
        {
            if (time <= DateTime.MinValue)
            {
                return 0;
            }

            if (time < UnixDate)
            {
                return 0;
            }

            TimeSpan timeSpan = time - UnixDate;
            return (Int64) timeSpan.TotalSeconds;
        }

        public static DateTime UnixTime(Int64 time)
        {
            return time <= 0 ? DateTime.MinValue : UnixDate.AddSeconds(time);
        }
    }
}