// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Data.SqlTypes;
using System.Globalization;
using System.Runtime.CompilerServices;
using NetExtender.Types.Exceptions;
using NetExtender.Utilities.Numerics;

namespace NetExtender.Utilities.Types
{
    public enum Month : Byte
    {
        January,
        February,
        March,
        April,
        May,
        June,
        July,
        August,
        September,
        October,
        November,
        December
    }

    public enum Quarter : Byte
    {
        First = 1,
        Second = 2,
        Third = 3,
        Fourth = 4
    }

    [Flags]
    public enum DateCompare : Byte
    {
        None = 0,
        InFuture = 1,
        InPast = 2,
        Today = 4,
        WeekDay = 8,
        WeekEnd = 16
    }

    public enum TimeFrame : Byte
    {
        Day,
        Week,
        Month,
        Quarter,
        Year
    }

    public static class DateTimeUtilities
    {
        public static DateTime Epoch
        {
            get
            {
                return new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static TimeSpan Elapsed(this DateTime value)
        {
            return DateTime.Now - value;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static TimeSpan Elapsed(this DateTime value, Boolean utc)
        {
            return utc ? ElapsedUtc(value) : Elapsed(value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static TimeSpan ElapsedUtc(this DateTime value)
        {
            return DateTime.UtcNow - value;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static DateTime DateTimeFrom(this Month month, Int32 year, Int32 day, Int32 hour, Int32 minute, Int32 second, Int32 millisecond)
        {
            return DateTimeFrom(month, year, day, hour, minute, second, millisecond, DateTimeKind.Unspecified);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static DateTime DateTimeFrom(this Month month, Int32 year, Int32 day, Int32 hour, Int32 minute, Int32 second, Int32 millisecond, DateTimeKind kind)
        {
            return new DateTime(year, (Int32) month, day, hour, minute, second, millisecond, kind);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static DateTime DateTimeFrom(this Month month, Int32 year, Int32 day, Int32 hour = 0, Int32 minute = 0, Int32 second = 0)
        {
            return DateTimeFrom(month, year, day, hour, minute, second, DateTimeKind.Unspecified);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static DateTime DateTimeFrom(this Month month, Int32 year, Int32 day, Int32 hour, Int32 minute, Int32 second, DateTimeKind kind)
        {
            return new DateTime(year, (Int32) month, day, hour, minute, second, kind);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static DateTime DateTimeFrom(this Month month, Int32 year, Int32 day, Int32 hour, Int32 minute, Int32 second, Int32 millisecond, Calendar calendar)
        {
            return new DateTime(year, (Int32) month, day, hour, minute, second, millisecond, calendar);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static DateTime DateTimeFrom(this Month month, Int32 year, Int32 day, Int32 hour, Int32 minute, Int32 second, Int32 millisecond, Calendar calendar, DateTimeKind kind)
        {
            return new DateTime(year, (Int32) month, day, hour, minute, second, millisecond, calendar, kind);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static DateTime ToKind(this DateTime value, DateTimeKind kind)
        {
            return kind switch
            {
                DateTimeKind.Unspecified => value.ToSpecifyKind(kind),
                DateTimeKind.Utc => value.ToUtcKind(),
                DateTimeKind.Local => value.ToLocalKind(),
                _ => throw new EnumUndefinedOrNotSupportedException<DateTimeKind>(kind, nameof(kind), null)
            };
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static DateTime ToUtcKind(this DateTime value)
        {
            return value.Kind switch
            {
                DateTimeKind.Unspecified => DateTime.SpecifyKind(value, DateTimeKind.Utc),
                DateTimeKind.Local => value.ToUniversalTime(),
                _ => value
            };
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static DateTime? ToUtcKind(this DateTime? value)
        {
            return value is not null ? ToUtcKind(value.Value) : null;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static DateTime ToLocalKind(this DateTime value)
        {
            return value.Kind switch
            {
                DateTimeKind.Unspecified => DateTime.SpecifyKind(value, DateTimeKind.Local),
                DateTimeKind.Utc => value.ToLocalTime(),
                _ => value
            };
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static DateTime? ToLocalKind(this DateTime? value)
        {
            return value is not null ? ToLocalKind(value.Value) : null;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static DateTime ToSpecifyKind(this DateTimeKind kind, DateTime value)
        {
            return ToSpecifyKind(value, kind);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static DateTime ToSpecifyKind(this DateTime value, DateTimeKind kind)
        {
            return value.Kind == kind ? value : DateTime.SpecifyKind(value, kind);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static DateTime? ToSpecifyKind(this DateTimeKind kind, DateTime? value)
        {
            return ToSpecifyKind(value, kind);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static DateTime? ToSpecifyKind(this DateTime? value, DateTimeKind kind)
        {
            return value?.ToSpecifyKind(kind);
        }

        /// <summary>
        /// Adds the number of weeks to the date
        /// </summary>
        /// <param name="date">Date input</param>
        /// <param name="weeks">Number of weeks to add</param>
        /// <returns>The date after the number of weeks are added</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static DateTime AddWeeks(this DateTime date, Int32 weeks)
        {
            return date.AddDays(weeks * 7);
        }

        /// <summary>
        /// Calculates age based on date supplied
        /// </summary>
        /// <param name="date">Birth date</param>
        /// <returns>The total age in years</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Int32 Age(this DateTime date)
        {
            return Age(date, DateTime.Now);
        }

        /// <summary>
        /// Calculates age based on date supplied
        /// </summary>
        /// <param name="date">Birth date</param>
        /// <param name="at">Date to calculate from</param>
        /// <returns>The total age in years</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Int32 Age(this DateTime date, DateTime at)
        {
            if (at < date)
            {
                throw new ArgumentOutOfRangeException(nameof(at), at, null);
            }

            return (at - date).Years();
        }

        /// <summary>
        /// Beginning of a specific time frame
        /// </summary>
        /// <param name="date">Date to base off of</param>
        /// <param name="frame">Time frame to use</param>
        /// <returns>The beginning of a specific time frame</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static DateTime BeginningOf(this DateTime date, TimeFrame frame)
        {
            return BeginningOf(date, frame, null);
        }

        /// <summary>
        /// Beginning of a specific time frame
        /// </summary>
        /// <param name="date">Date to base off of</param>
        /// <param name="frame">Time frame to use</param>
        /// <param name="info">Culture to use for calculating (defaults to the invariant culture)</param>
        /// <returns>The beginning of a specific time frame</returns>
        public static DateTime BeginningOf(this DateTime date, TimeFrame frame, CultureInfo? info)
        {
            info ??= CultureInfo.InvariantCulture;
            return frame switch
            {
                TimeFrame.Day => date.Date,
                TimeFrame.Week => date.AddDays(info.DateTimeFormat.FirstDayOfWeek - date.DayOfWeek).Date,
                TimeFrame.Month => new DateTime(date.Year, date.Month, 1),
                TimeFrame.Quarter => date.BeginningOf(TimeFrame.Quarter, date.BeginningOf(TimeFrame.Year, info), info),
                _ => new DateTime(date.Year, 1, 1)
            };
        }

        /// <summary>
        /// Beginning of a specific time frame
        /// </summary>
        /// <param name="date">Date to base off of</param>
        /// <param name="frame">Time frame to use</param>
        /// <param name="start">Start of the first quarter</param>
        /// <returns>The beginning of a specific time frame</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static DateTime BeginningOf(this DateTime date, TimeFrame frame, DateTime start)
        {
            return BeginningOf(date, frame, start, null);
        }

        /// <summary>
        /// Beginning of a specific time frame
        /// </summary>
        /// <param name="date">Date to base off of</param>
        /// <param name="frame">Time frame to use</param>
        /// <param name="start">Start of the first quarter</param>
        /// <param name="info">Culture to use for calculating (defaults to the invariant culture)</param>
        /// <returns>The beginning of a specific time frame</returns>
        public static DateTime BeginningOf(this DateTime date, TimeFrame frame, DateTime start, CultureInfo? info)
        {
            if (frame != TimeFrame.Quarter)
            {
                return date.BeginningOf(frame, info);
            }

            info ??= CultureInfo.InvariantCulture;
            if (date.Between(start, start.AddMonths(3).AddDays(-1).EndOf(TimeFrame.Day, info)))
            {
                return start.Date;
            }

            if (date.Between(start.AddMonths(3), start.AddMonths(6).AddDays(-1).EndOf(TimeFrame.Day, info)))
            {
                return start.AddMonths(3).Date;
            }

            if (date.Between(start.AddMonths(6), start.AddMonths(9).AddDays(-1).EndOf(TimeFrame.Day, info)))
            {
                return start.AddMonths(6).Date;
            }

            return start.AddMonths(9).Date;
        }

        /// <summary>
        /// Gets the number of days in the time frame specified based on the date
        /// </summary>
        /// <param name="date">Date</param>
        /// <param name="frame">Time frame to calculate the number of days from</param>
        /// <returns>The number of days in the time frame</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Int32 DaysIn(this DateTime date, TimeFrame frame)
        {
            return DaysIn(date, frame, null);
        }

        /// <summary>
        /// Gets the number of days in the time frame specified based on the date
        /// </summary>
        /// <param name="date">Date</param>
        /// <param name="frame">Time frame to calculate the number of days from</param>
        /// <param name="info">Culture to use for calculating (defaults to the invariant culture)</param>
        /// <returns>The number of days in the time frame</returns>
        public static Int32 DaysIn(this DateTime date, TimeFrame frame, CultureInfo? info)
        {
            info ??= CultureInfo.InvariantCulture;
            return frame switch
            {
                TimeFrame.Day => 1,
                TimeFrame.Week => 7,
                TimeFrame.Month => info.Calendar.GetDaysInMonth(date.Year, date.Month),
                TimeFrame.Quarter => date.EndOf(TimeFrame.Quarter, info).DayOfYear - date.BeginningOf(TimeFrame.Quarter, info).DayOfYear,
                _ => info.Calendar.GetDaysInYear(date.Year)
            };
        }

        /// <summary>
        /// Gets the number of days in the time frame specified based on the date
        /// </summary>
        /// <param name="date">Date</param>
        /// <param name="frame">Time frame to calculate the number of days from</param>
        /// <param name="quarter">Start of the first quarter</param>
        /// <returns>The number of days in the time frame</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Int32 DaysIn(this DateTime date, TimeFrame frame, DateTime quarter)
        {
            return DaysIn(date, frame, quarter, null);
        }

        /// <summary>
        /// Gets the number of days in the time frame specified based on the date
        /// </summary>
        /// <param name="date">Date</param>
        /// <param name="frame">Time frame to calculate the number of days from</param>
        /// <param name="quarter">Start of the first quarter</param>
        /// <param name="info">Culture to use for calculating (defaults to the invariant culture)</param>
        /// <returns>The number of days in the time frame</returns>
        public static Int32 DaysIn(this DateTime date, TimeFrame frame, DateTime quarter, CultureInfo? info)
        {
            if (frame != TimeFrame.Quarter)
            {
                date.DaysIn(frame, info);
            }

            info ??= CultureInfo.InvariantCulture;
            return date.EndOf(TimeFrame.Quarter, info).DayOfYear - quarter.DayOfYear;
        }

        /// <summary>
        /// Gets the number of days left in the time frame specified based on the date
        /// </summary>
        /// <param name="date">Date</param>
        /// <param name="frame">Time frame to calculate the number of days left</param>
        /// <returns>The number of days left in the time frame</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Int32 DaysLeftIn(this DateTime date, TimeFrame frame)
        {
            return DaysLeftIn(date, frame, null);
        }

        /// <summary>
        /// Gets the number of days left in the time frame specified based on the date
        /// </summary>
        /// <param name="date">Date</param>
        /// <param name="frame">Time frame to calculate the number of days left</param>
        /// <param name="info">Culture to use for calculating (defaults to the invariant culture)</param>
        /// <returns>The number of days left in the time frame</returns>
        public static Int32 DaysLeftIn(this DateTime date, TimeFrame frame, CultureInfo? info)
        {
            info ??= CultureInfo.InvariantCulture;
            return frame switch
            {
                TimeFrame.Day => 1,
                TimeFrame.Week => 7 - ((Int32) date.DayOfWeek + 1),
                TimeFrame.Month => date.DaysIn(TimeFrame.Month, info) - date.Day,
                TimeFrame.Quarter => date.DaysIn(TimeFrame.Quarter, info) - (date.DayOfYear - date.BeginningOf(TimeFrame.Quarter, info).DayOfYear),
                _ => date.DaysIn(TimeFrame.Year, info) - date.DayOfYear
            };
        }

        /// <summary>
        /// Gets the number of days left in the time frame specified based on the date
        /// </summary>
        /// <param name="date">Date</param>
        /// <param name="frame">Time frame to calculate the number of days left</param>
        /// <param name="start">Start of the first quarter</param>
        /// <returns>The number of days left in the time frame</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Int32 DaysLeftIn(this DateTime date, TimeFrame frame, DateTime start)
        {
            return DaysLeftIn(date, frame, start, null);
        }

        /// <summary>
        /// Gets the number of days left in the time frame specified based on the date
        /// </summary>
        /// <param name="date">Date</param>
        /// <param name="frame">Time frame to calculate the number of days left</param>
        /// <param name="start">Start of the first quarter</param>
        /// <param name="info">Culture to use for calculating (defaults to the invariant culture)</param>
        /// <returns>The number of days left in the time frame</returns>
        public static Int32 DaysLeftIn(this DateTime date, TimeFrame frame, DateTime start, CultureInfo? info)
        {
            if (frame != TimeFrame.Quarter)
            {
                return date.DaysLeftIn(frame, info);
            }

            info ??= CultureInfo.InvariantCulture;
            return date.DaysIn(TimeFrame.Quarter, start, info) - (date.DayOfYear - start.DayOfYear);
        }

        /// <summary>
        /// End of a specific time frame
        /// </summary>
        /// <param name="date">Date to base off of</param>
        /// <param name="frame">Time frame to use</param>
        /// <returns>
        /// The end of a specific time frame (TimeFrame.Day is the only one that sets the time to
        /// 12: 59:59 PM, all else are the beginning of the day)
        /// </returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static DateTime EndOf(this DateTime date, TimeFrame frame)
        {
            return EndOf(date, frame, null);
        }

        /// <summary>
        /// End of a specific time frame
        /// </summary>
        /// <param name="date">Date to base off of</param>
        /// <param name="frame">Time frame to use</param>
        /// <param name="info">Culture to use for calculating (defaults to the invariant culture)</param>
        /// <returns>
        /// The end of a specific time frame (TimeFrame.Day is the only one that sets the time to
        /// 12: 59:59 PM, all else are the beginning of the day)
        /// </returns>
        public static DateTime EndOf(this DateTime date, TimeFrame frame, CultureInfo? info)
        {
            info ??= CultureInfo.InvariantCulture;
            return frame switch
            {
                TimeFrame.Day => new DateTime(date.Year, date.Month, date.Day, 23, 59, 59),
                TimeFrame.Week => date.BeginningOf(TimeFrame.Week, info).AddDays(6),
                TimeFrame.Month => date.AddMonths(1).BeginningOf(TimeFrame.Month, info).AddDays(-1).Date,
                TimeFrame.Quarter => date.EndOf(TimeFrame.Quarter, date.BeginningOf(TimeFrame.Year, info), info),
                _ => new DateTime(date.Year, 12, 31)
            };
        }

        /// <summary>
        /// End of a specific time frame
        /// </summary>
        /// <param name="date">Date to base off of</param>
        /// <param name="frame">Time frame to use</param>
        /// <param name="start">Start of the first quarter</param>
        /// <returns>
        /// The end of a specific time frame (TimeFrame.Day is the only one that sets the time to
        /// 12: 59:59 PM, all else are the beginning of the day)
        /// </returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static DateTime EndOf(this DateTime date, TimeFrame frame, DateTime start)
        {
            return EndOf(date, frame, start, null);
        }

        /// <summary>
        /// End of a specific time frame
        /// </summary>
        /// <param name="date">Date to base off of</param>
        /// <param name="frame">Time frame to use</param>
        /// <param name="start">Start of the first quarter</param>
        /// <param name="info">Culture to use for calculating (defaults to the invariant culture)</param>
        /// <returns>
        /// The end of a specific time frame (TimeFrame.Day is the only one that sets the time to
        /// 12: 59:59 PM, all else are the beginning of the day)
        /// </returns>
        public static DateTime EndOf(this DateTime date, TimeFrame frame, DateTime start, CultureInfo? info)
        {
            if (frame != TimeFrame.Quarter)
            {
                return date.EndOf(frame, info);
            }

            info ??= CultureInfo.InvariantCulture;
            if (date.Between(start, start.AddMonths(3).AddDays(-1).EndOf(TimeFrame.Day, info)))
            {
                return start.AddMonths(3).AddDays(-1).Date;
            }

            if (date.Between(start.AddMonths(3), start.AddMonths(6).AddDays(-1).EndOf(TimeFrame.Day, info)))
            {
                return start.AddMonths(6).AddDays(-1).Date;
            }

            if (date.Between(start.AddMonths(6), start.AddMonths(9).AddDays(-1).EndOf(TimeFrame.Day, info)))
            {
                return start.AddMonths(9).AddDays(-1).Date;
            }

            return start.AddYears(1).AddDays(-1).Date;
        }

        /// <summary>
        /// Determines if the date fulfills the comparison
        /// </summary>
        /// <param name="date">Date to check</param>
        /// <param name="comparison">
        /// Comparison type (can be combined, so you can do weekday in the future, etc)
        /// </param>
        /// <returns>True if it is, false otherwise</returns>
        public static Boolean Is(this DateTime date, DateCompare comparison)
        {
            return (comparison & DateCompare.InFuture) != 0 && DateTime.Now < date
                   || (comparison & DateCompare.InPast) != 0 && DateTime.Now > date
                   || (comparison & DateCompare.Today) != 0 && DateTime.Today == date.Date
                   || (comparison & DateCompare.WeekDay) != 0 && (Int32) date.DayOfWeek != 6 && date.DayOfWeek != 0
                   || (comparison & DateCompare.WeekEnd) != 0 && ((Int32) date.DayOfWeek == 6 || date.DayOfWeek == 0);
        }

        /// <summary>
        /// Converts a DateTime to a specific time zone
        /// </summary>
        /// <param name="date">DateTime to convert</param>
        /// <param name="zone">Time zone to convert to</param>
        /// <returns>The converted DateTime</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static DateTime To(this DateTime date, TimeZoneInfo? zone)
        {
            zone ??= TimeZoneInfo.Utc;
            return TimeZoneInfo.ConvertTime(date, zone);
        }

        /// <summary>
        /// Returns the date in int format based on an Epoch (defaults to unix epoch of 1/1/1970)
        /// </summary>
        /// <param name="date">Date to convert</param>
        /// <returns>The date in Unix format</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Int32 To(this DateTime date)
        {
            return To(date, Epoch);
        }

        /// <summary>
        /// Returns the date in int format based on an Epoch (defaults to unix epoch of 1/1/1970)
        /// </summary>
        /// <param name="date">Date to convert</param>
        /// <param name="epoch">Epoch to use (defaults to unix epoch of 1/1/1970)</param>
        /// <returns>The date in Unix format</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Int32 To(this DateTime date, DateTime epoch)
        {
            return (Int32) ((date.ToUniversalTime() - epoch).Ticks / TimeSpan.TicksPerSecond);
        }

        /// <summary>
        /// Returns the date in DateTime format based on an Epoch (defaults to unix epoch of 1/1/1970)
        /// </summary>
        /// <param name="date">Date to convert</param>
        /// <returns>The Unix Date in DateTime format</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static DateTime ToDateTime(this Int32 date)
        {
            return ToDateTime(date, Epoch);
        }

        /// <summary>
        /// Returns the date in DateTime format based on an Epoch (defaults to unix epoch of 1/1/1970)
        /// </summary>
        /// <param name="date">Date to convert</param>
        /// <param name="epoch">Epoch to use (defaults to unix epoch of 1/1/1970)</param>
        /// <returns>The Unix Date in DateTime format</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static DateTime ToDateTime(this Int32 date, DateTime epoch)
        {
            return new DateTime(date * TimeSpan.TicksPerSecond + epoch.Ticks, DateTimeKind.Utc);
        }

        /// <summary>
        /// Returns the date in DateTime format based on an Epoch (defaults to unix epoch of 1/1/1970)
        /// </summary>
        /// <param name="date">Date to convert</param>
        /// <returns>The Unix Date in DateTime format</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static DateTime ToDateTime(this Int64 date)
        {
            return ToDateTime(date, Epoch);
        }

        /// <summary>
        /// Returns the date in DateTime format based on an Epoch (defaults to unix epoch of 1/1/1970)
        /// </summary>
        /// <param name="date">Date to convert</param>
        /// <param name="epoch">Epoch to use (defaults to unix epoch of 1/1/1970)</param>
        /// <returns>The Unix Date in DateTime format</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static DateTime ToDateTime(this Int64 date, DateTime epoch)
        {
            return new DateTime(date * TimeSpan.TicksPerSecond + epoch.Ticks, DateTimeKind.Utc);
        }

        /// <summary>
        /// Gets the UTC offset
        /// </summary>
        /// <param name="date">Date to get the offset of</param>
        /// <returns>UTC offset</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Double UTCOffset(this DateTime date)
        {
            return (date - date.ToUniversalTime()).TotalHours;
        }

        /// <summary>
        /// Returns the end of the current month.
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static DateTime EndOfMonth()
        {
            return EndOfMonth(DateTime.Today);
        }

        /// <summary>
        /// Returns the end of the month of the specified date.
        /// </summary>
        /// <param name="date">The source date.</param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static DateTime EndOfMonth(this DateTime date)
        {
            return new DateTime(date.Year, date.Month, DateTime.DaysInMonth(date.Year, date.Month));
        }

        /// <summary>
        /// Returns the end of the current week (sunday).
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static DateTime EndOfWeek()
        {
            return EndOfWeek(DateTime.Today);
        }

        /// <summary>
        /// Returns the end of the week (sunday) of the specified date.
        /// </summary>
        /// <param name="date">The source date.</param>
        public static DateTime EndOfWeek(this DateTime date)
        {
            if (date.DayOfWeek == DayOfWeek.Sunday)
            {
                return date.Date;
            }

            Int32 diff = 7 - (Int32) date.DayOfWeek;

            return date.Date.AddDays(diff);
        }

        /// <summary>
        /// Determines whether the provided text represents a valid <see cref="DateTime"/>.
        /// </summary>
        /// <param name="text">A string, possibly representing a valid date and time.</param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean IsDateTime(String text)
        {
            return DateTime.TryParse(text, out _);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean IsWeekend(this DateTime date)
        {
            return date.DayOfWeek == DayOfWeek.Saturday || date.DayOfWeek == DayOfWeek.Sunday;
        }

        /// <summary>
        /// Determines whether this date is in the current month.
        /// <para>
        /// Ignores the year. 2000-01-01 and 2001-01-01 will return true.
        /// </para>
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean IsCurrentMonth(this DateTime date)
        {
            return DateTime.Today.Month == date.Month;
        }

        /// <summary>
        /// Determines whether this date is in the future. Ignores time.
        /// </summary>
        /// <param name="date">The source date.</param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean IsFutureDate(this DateTime date)
        {
            return date.Date.CompareTo(DateTime.Today) > 0;
        }

        /// <summary>
        /// Determines whether this datetime is in the future.
        /// </summary>
        /// <param name="date">The source date.</param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean IsFuture(this DateTime date)
        {
            return date.CompareTo(DateTime.Now) > 0;
        }

        /// <summary>
        /// Returns the next date from today on the specified day of the week. If the specified day of the week is today, it returns the next week.
        /// </summary>
        /// <param name="day">The target day of the week.</param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static DateTime NextOnDay(DayOfWeek day)
        {
            return NextOnDay(DateTime.Today, day);
        }

        /// <summary>
        /// Returns the next date from the specified date on the specified day of the week. If the specified day of the week is the same as the one of the specified date, this method returns the next week.
        /// </summary>
        /// <param name="date">The source date.</param>
        /// <param name="day">The target day of the week.</param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static DateTime NextOnDay(this DateTime date, DayOfWeek day)
        {
            return date.AddDays(Time.DaysInWeek - ((Int32) date.DayOfWeek - (Int32) day).Abs());
        }

        /// <summary>
        /// Returns the start of the current month.
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static DateTime StartOfMonth()
        {
            return StartOfMonth(DateTime.Today);
        }

        /// <summary>
        /// Returns the start of the month of the specified date.
        /// </summary>
        /// <param name="date">The source date.</param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static DateTime StartOfMonth(this DateTime date)
        {
            return new DateTime(date.Year, date.Month, 1);
        }

        /// <summary>
        /// Returns the start of the current week (monday).
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static DateTime StartOfWeek()
        {
            return StartOfWeek(DateTime.Today);
        }

        /// <summary>
        /// Returns the start of the week (monday) of the specified date.
        /// </summary>
        /// <param name="date">The source date.</param>
        public static DateTime StartOfWeek(this DateTime date)
        {
            if (date.DayOfWeek == DayOfWeek.Monday)
            {
                return date.Date;
            }

            Int32 current = date.DayOfWeek == DayOfWeek.Sunday ? 7 : (Int32) date.DayOfWeek;

            // Monday = 1
            Int32 diff = 1 - current;

            return date.Date.AddDays(diff);
        }

        public static DateTime GetFirstDayOfMonth(this DateTime date)
        {
            return new DateTime(date.Year, date.Month, 1, 0, 0, 0, date.Kind);
        }

        public static DateTime GetFirstDayOfMonth(this DateTime date, DayOfWeek day)
        {
            date = date.GetFirstDayOfMonth();

            DayOfWeek current = date.DayOfWeek;

            if (current == day)
            {
                return date;
            }

            Int32 lwd = day - date.DayOfWeek;

            if (lwd < 0)
            {
                lwd += 7;
            }

            return date.AddDays(lwd);
        }

        public static DateTime GetLastDayOfMonth(this DateTime date)
        {
            Int32 year = date.Year;
            Int32 month = date.Month;
            return new DateTime(year, month, DateTime.DaysInMonth(year, month));
        }

        public static DateTime GetLastDayOfMonth(this DateTime date, DayOfWeek day)
        {
            DateTime last = GetLastDayOfMonth(date);
            Int32 lwd = day - last.DayOfWeek;
            return last.AddDays(lwd);
        }

        public static DateTime GetFirstDayOfPreviousMonth(this DateTime date)
        {
            Int32 month = date.Month;
            return month <= 1 ?
                new DateTime(date.Year - 1, 12, 1, 0, 0, 0, date.Kind) :
                new DateTime(date.Year, month - 1, 1, 0, 0, 0, date.Kind);
        }

        public static DateTime GetFirstDayOfNextMonth(this DateTime date)
        {
            Int32 month = date.Month;
            return month >= 12 ?
                new DateTime(date.Year + 1, 1, 1, 0, 0, 0, date.Kind) :
                new DateTime(date.Year, month + 1, 1, 0, 0, 0, date.Kind);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Quarter Quarter(this DateTime date)
        {
            return (Quarter) QuarterNumber(date);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Int32 QuarterNumber(this DateTime date)
        {
            return (date.Month + 2) / 3;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static DateTime AddQuarters(this DateTime date, Int32 quarters)
        {
            return date.AddMonths(quarters * 3);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static DateTime GetFirstDayOfQuarter(Int32 year, Int32 quarter)
        {
            if (quarter < 1 || quarter > 4)
            {
                throw new ArgumentOutOfRangeException(nameof(quarter), quarter, @"Quarter not in range");
            }

            return new DateTime(year, 1, 1).AddQuarters(quarter - 1);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static DateTime GetFirstDayOfQuarter(Int32 year, Quarter quarter)
        {
            return new DateTime(year, 1, 1).AddQuarters((Int32) quarter - 1);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static DateTime FirstDayOfQuarter(this DateTime date)
        {
            return new DateTime(date.Year, 1, 1).AddQuarters(date.QuarterNumber() - 1);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static DateTime LastDayOfQuarter(Int32 year, Int32 quarter)
        {
            if (quarter < 1 || quarter > 4)
            {
                throw new ArgumentOutOfRangeException(nameof(quarter), quarter, @"Quarter not in range");
            }

            return new DateTime(year, 1, 1).AddQuarters(quarter).AddDays(-1);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static DateTime LastDayOfQuarter(Int32 year, Quarter quarter)
        {
            return new DateTime(year, 1, 1).AddQuarters((Int32) quarter).AddDays(-1);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static DateTime LastDayOfQuarter(this DateTime date)
        {
            return new DateTime(date.Year, 1, 1).AddQuarters(date.QuarterNumber()).AddDays(-1);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean SameYearAndQuarter(this DateTime first, DateTime second)
        {
            return FirstDayOfQuarter(first) == FirstDayOfQuarter(second);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean IsDateInYearAndQuarter(this DateTime date, Int32 year, Int32 quarter)
        {
            if (quarter < 1 || quarter > 4)
            {
                throw new ArgumentOutOfRangeException(nameof(quarter), quarter, @"Quarter not in range");
            }

            return date.Year == year && date.QuarterNumber() == quarter;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean IsDateInYearAndQuarter(this DateTime date, Int32 year, Quarter quarter)
        {
            return date.Year == year && date.Quarter() == quarter;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static DateTime Min(this DateTime first, DateTime second)
        {
            return first < second ? first : second;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static DateTime Max(this DateTime first, DateTime second)
        {
            return first > second ? first : second;
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
        public static DateTime SetDate(this DateTime date, Int32 year, Month month, Int32 day)
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
        public static DateTime SetDate(this DateTime date, Int32 year, Int32 month, Int32 day)
        {
            return new DateTime(year, month, day, date.Hour, date.Minute, date.Second, date.Millisecond, date.Kind);
        }

        /// <summary>
        /// Set year of date
        /// </summary>
        /// <param name="date">Date to set date</param>
        /// <param name="year">Year to set</param>
        /// <returns>new date</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static DateTime SetYear(this DateTime date, Int32 year)
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
        public static DateTime SetMonth(this DateTime date, Int32 month)
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
        public static DateTime SetMonth(this DateTime date, Month month)
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
        public static DateTime SetDay(this DateTime date, Int32 day)
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
        public static DateTime SetTime(this DateTime date, Int32 hour, Int32 minute, Int32 second, Int32 millisecond)
        {
            return new DateTime(date.Year, date.Month, date.Day, hour, minute, second, millisecond, date.Kind);
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
        public static DateTime SetTime(this DateTime date, Int32 hour, Int32 minute, Int32 second)
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
        public static DateTime SetTime(this DateTime date, Int32 hour, Int32 minute)
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
        public static DateTime SetHour(this DateTime date, Int32 hour)
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
        public static DateTime SetMinute(this DateTime date, Int32 minute)
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
        public static DateTime SetSecond(this DateTime date, Int32 second)
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
        public static DateTime SetMillisecond(this DateTime date, Int32 millisecond)
        {
            return SetTime(date, date.Hour, date.Minute, date.Second, millisecond);
        }

        /// <summary>
        /// Sets the time portion of a specific date
        /// </summary>
        /// <param name="date">Date input</param>
        /// <param name="time">Time to set</param>
        /// <returns>Sets the time portion of the specified date</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static DateTime SetTime(this DateTime date, TimeSpan time)
        {
            return date.Date.Add(time);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static DateTime TruncateToMilliseconds(this DateTime date)
        {
            return new DateTime(date.Year, date.Month, date.Day, date.Hour, date.Minute, date.Second, date.Millisecond);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static DateTime TruncateToSeconds(this DateTime date)
        {
            return new DateTime(date.Year, date.Month, date.Day, date.Hour, date.Minute, date.Second, 0);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static DateTime TruncateToMinutes(this DateTime date)
        {
            return new DateTime(date.Year, date.Month, date.Day, date.Hour, date.Minute, 0, 0);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static DateTime TruncateToHours(this DateTime date)
        {
            return new DateTime(date.Year, date.Month, date.Day, date.Hour, 0, 0, 0);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static DateTime TruncateToDays(this DateTime date)
        {
            return new DateTime(date.Year, date.Month, date.Day, 0, 0, 0, 0);
        }

        /// <summary>
        ///     Checks if <paramref name="value" /> is between 1/1/1753 12:00:00 AM and 12/31/9999 11:59:59 PM.
        /// </summary>
        /// <remarks>
        ///     SQL Server defines two different datetime formats:
        ///     The datetime datatype is capable of storing dates in the range 1753-01-01 to 9999-12-31.
        ///     The datetime2 datatype was introduced in SQL Server 2008. The range of dates that it is capable of storing is
        ///     0001-01-01 to 9999-12-31.
        /// </remarks>
        private static Boolean IsSqlServerDatetime(this DateTime value)
        {
            return value >= SqlDateTime.MinValue.Value && value <= SqlDateTime.MaxValue.Value;
        }
    }
}