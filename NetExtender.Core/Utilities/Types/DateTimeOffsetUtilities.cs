// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Globalization;
using System.Runtime.CompilerServices;

namespace NetExtender.Utilities.Types
{
    public static class DateTimeOffsetUtilities
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static DateTimeOffset Epoch(TimeSpan offset)
        {
            return new DateTimeOffset(1970, 1, 1, 0, 0, 0, offset);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static DateTimeOffset Epoch(Calendar? calendar, TimeSpan offset)
        {
            return calendar is not null ? new DateTimeOffset(1970, 1, 1, 0, 0, 0, 0, calendar, offset) : Epoch(offset);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static TimeSpan Elapsed(this DateTimeOffset value)
        {
            return DateTimeOffset.Now - value;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static TimeSpan ElapsedUtc(this DateTimeOffset value)
        {
            return DateTimeOffset.UtcNow - value;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static DateTimeOffset DateTimeOffsetFrom(this Month month, Int32 year, Int32 day, Int32 hour = 0, Int32 minute = 0, Int32 second = 0)
        {
            return DateTimeOffsetFrom(month, year, day, hour, minute, second, TimeSpan.Zero);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static DateTimeOffset DateTimeOffsetFrom(this Month month, Int32 year, Int32 day, Int32 hour, Int32 minute, Int32 second, TimeSpan offset)
        {
            return new DateTimeOffset(year, (Int32) month, day, hour, minute, second, offset);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static DateTimeOffset DateTimeOffsetFrom(this Month month, Int32 year, Int32 day, Int32 hour, Int32 minute, Int32 second, Int32 millisecond)
        {
            return DateTimeOffsetFrom(month, year, day, hour, minute, second, millisecond, TimeSpan.Zero);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static DateTimeOffset DateTimeOffsetFrom(this Month month, Int32 year, Int32 day, Int32 hour, Int32 minute, Int32 second, Int32 millisecond, TimeSpan offset)
        {
            return new DateTimeOffset(year, (Int32) month, day, hour, minute, second, millisecond, offset);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static DateTimeOffset DateTimeOffsetFrom(this Month month, Int32 year, Int32 day, Int32 hour, Int32 minute, Int32 second, Int32 millisecond, Calendar calendar, TimeSpan offset)
        {
            return new DateTimeOffset(year, (Int32) month, day, hour, minute, second, millisecond, calendar, offset);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Int32 Age(this DateTimeOffset date)
        {
            return Age(date, DateTimeOffset.Now);
        }

        /// <summary>
        /// Calculates age based on date supplied
        /// </summary>
        /// <param name="date">Birth date</param>
        /// <param name="at">Date to calculate from</param>
        /// <returns>The total age in years</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Int32 Age(this DateTimeOffset date, DateTimeOffset at)
        {
            if (at < date)
            {
                throw new ArgumentOutOfRangeException(nameof(at));
            }

            return (at - date).Years();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static DateTimeOffset Min(this DateTimeOffset first, DateTimeOffset second)
        {
            return first < second ? first : second;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static DateTimeOffset Max(this DateTimeOffset first, DateTimeOffset second)
        {
            return first > second ? first : second;
        }

        /// <summary>
        /// Determines whether the provided text represents a valid <see cref="DateTimeOffset"/>.
        /// </summary>
        /// <param name="text">A string, possibly representing a valid date and time.</param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean IsDateTimeOffset(String text)
        {
            return DateTimeOffset.TryParse(text, out _);
        }

        /// <summary>
        /// Set date of datetime
        /// </summary>
        /// <param name="date">Date to set date</param>
        /// <param name="year">Year to set</param>
        /// <param name="month">Month to set</param>
        /// <param name="day">Day to set</param>
        /// <returns>new date</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static DateTimeOffset SetDate(this DateTimeOffset date, Int32 year, Month month, Int32 day)
        {
            return SetDate(date, year, (Int32) month + 1, day);
        }

        /// <summary>
        /// Set date of datetime
        /// </summary>
        /// <param name="date">Date to set date</param>
        /// <param name="year">Year to set</param>
        /// <param name="month">Month to set</param>
        /// <param name="day">Day to set</param>
        /// <returns>new date</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static DateTimeOffset SetDate(this DateTimeOffset date, Int32 year, Int32 month, Int32 day)
        {
            return new DateTimeOffset(year, month, day, date.Hour, date.Minute, date.Second, date.Millisecond, date.Offset);
        }

        /// <summary>
        /// Set year of date
        /// </summary>
        /// <param name="date">Date to set date</param>
        /// <param name="year">Year to set</param>
        /// <returns>new date</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static DateTimeOffset SetYear(this DateTimeOffset date, Int32 year)
        {
            return SetDate(date, year, date.Month, date.Day);
        }

        /// <summary>
        /// Set month of date
        /// </summary>
        /// <param name="date">Date to set date</param>
        /// <param name="month">Month to set</param>
        /// <returns>new date</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static DateTimeOffset SetMonth(this DateTimeOffset date, Int32 month)
        {
            return SetDate(date, date.Year, month, date.Day);
        }

        /// <summary>
        /// Set month of date
        /// </summary>
        /// <param name="date">Date to set date</param>
        /// <param name="month">Month to set</param>
        /// <returns>new date</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static DateTimeOffset SetMonth(this DateTimeOffset date, Month month)
        {
            return SetMonth(date, (Int32) month + 1);
        }

        /// <summary>
        /// Set day of date
        /// </summary>
        /// <param name="date">Date to set date</param>
        /// <param name="day">Day to set</param>
        /// <returns>new date</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static DateTimeOffset SetDay(this DateTimeOffset date, Int32 day)
        {
            return SetDate(date, date.Year, date.Month, day);
        }

        /// <summary>
        /// Set time of date
        /// </summary>
        /// <param name="date">Date to add time</param>
        /// <param name="hour">Hours to add</param>
        /// <param name="minute">Minutes to add</param>
        /// <param name="second">Seconds to add</param>
        /// <param name="millisecond">Milliseconds to add</param>
        /// <returns>Date with new time</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static DateTimeOffset SetTime(this DateTimeOffset date, Int32 hour, Int32 minute, Int32 second, Int32 millisecond)
        {
            return new DateTimeOffset(date.Year, date.Month, date.Day, hour, minute, second, millisecond, date.Offset);
        }

        /// <summary>
        /// Set time of date
        /// </summary>
        /// <param name="date">Date to add time</param>
        /// <param name="hour">Hours to add</param>
        /// <param name="minute">Minutes to add</param>
        /// <param name="second">Seconds to add</param>
        /// <returns>Date with new time</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static DateTimeOffset SetTime(this DateTimeOffset date, Int32 hour, Int32 minute, Int32 second)
        {
            return SetTime(date, hour, minute, second, date.Millisecond);
        }

        /// <summary>
        /// Set time of date
        /// </summary>
        /// <param name="date">Date to add time</param>
        /// <param name="hour">Hours to add</param>
        /// <param name="minute">Minutes to add</param>
        /// <returns>Date with new time</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static DateTimeOffset SetTime(this DateTimeOffset date, Int32 hour, Int32 minute)
        {
            return SetTime(date, hour, minute, date.Second, date.Millisecond);
        }

        /// <summary>
        /// Set hour of date
        /// </summary>
        /// <param name="date">Date to set time</param>
        /// <param name="hour">Hours to set</param>
        /// <returns>Date with new time</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static DateTimeOffset SetHour(this DateTimeOffset date, Int32 hour)
        {
            return SetTime(date, hour, date.Minute, date.Second, date.Millisecond);
        }

        /// <summary>
        /// Set minute of date
        /// </summary>
        /// <param name="date">Date to set time</param>
        /// <param name="minute">Minute to set</param>
        /// <returns>Date with new time</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static DateTimeOffset SetMinute(this DateTimeOffset date, Int32 minute)
        {
            return SetTime(date, date.Hour, minute, date.Second, date.Millisecond);
        }

        /// <summary>
        /// Set second of date
        /// </summary>
        /// <param name="date">Date to set time</param>
        /// <param name="second">Second to set</param>
        /// <returns>Date with new time</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static DateTimeOffset SetSecond(this DateTimeOffset date, Int32 second)
        {
            return SetTime(date, date.Hour, date.Minute, second, date.Millisecond);
        }

        /// <summary>
        /// Set millisecond of date
        /// </summary>
        /// <param name="date">Date to set time</param>
        /// <param name="millisecond">Millisecond to set</param>
        /// <returns>Date with new time</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static DateTimeOffset SetMillisecond(this DateTimeOffset date, Int32 millisecond)
        {
            return SetTime(date, date.Hour, date.Minute, date.Second, millisecond);
        }

        /// <summary>
        /// Set offset of date
        /// </summary>
        /// <param name="date">Date to set time</param>
        /// <param name="offset">Offset to set</param>
        /// <returns>Date with new offset</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static DateTimeOffset SetOffset(this DateTimeOffset date, TimeSpan offset)
        {
            return new DateTimeOffset(date.Year, date.Month, date.Day, date.Hour, date.Minute, date.Second, date.Millisecond, offset);
        }

        /// <summary>
        /// Sets the time portion of a specific date
        /// </summary>
        /// <param name="date">Date input</param>
        /// <param name="time">Time to set</param>
        /// <returns>Sets the time portion of the specified date</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static DateTimeOffset SetTime(this DateTimeOffset date, TimeSpan time)
        {
            return new DateTimeOffset(date.Year, date.Month, date.Day, 0, 0, 0, date.Offset).Add(time);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static DateTimeOffset TruncateToMilliseconds(this DateTimeOffset date)
        {
            return new DateTimeOffset(date.Year, date.Month, date.Day, date.Hour, date.Minute, date.Second, date.Millisecond, date.Offset);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static DateTimeOffset TruncateToSeconds(this DateTimeOffset date)
        {
            return new DateTimeOffset(date.Year, date.Month, date.Day, date.Hour, date.Minute, date.Second, 0, date.Offset);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static DateTimeOffset TruncateToMinutes(this DateTimeOffset date)
        {
            return new DateTimeOffset(date.Year, date.Month, date.Day, date.Hour, date.Minute, 0, 0, date.Offset);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static DateTimeOffset TruncateToHours(this DateTimeOffset date)
        {
            return new DateTimeOffset(date.Year, date.Month, date.Day, date.Hour, 0, 0, 0, date.Offset);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static DateTimeOffset TruncateToDays(this DateTimeOffset date)
        {
            return new DateTimeOffset(date.Year, date.Month, date.Day, 0, 0, 0, 0, date.Offset);
        }
    }
}