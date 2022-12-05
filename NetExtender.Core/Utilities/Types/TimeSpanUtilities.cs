// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading;
using NetExtender.Types.Exceptions;
using NetExtender.Utilities.Numerics;

namespace NetExtender.Utilities.Types
{
    public static class TimeSpanUtilities
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean IsDefault(this TimeSpan value)
        {
            return value == default;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean IsNotDefault(this TimeSpan value)
        {
            return !IsDefault(value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean IsPositive(this TimeSpan value)
        {
            return value.Ticks >= 0;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean IsNegative(this TimeSpan value)
        {
            return value.Ticks < 0;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean IsInfinity(this TimeSpan value)
        {
            return value == Timeout.InfiniteTimeSpan;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean IsTimeout(this TimeSpan value)
        {
            return IsPositive(value) || IsInfinity(value);
        }

        public static TimeSpan From(this TimeType type)
        {
            return From(type, 1);
        }

        public static TimeSpan From(this TimeType type, Double count)
        {
            return type switch
            {
                TimeType.Milliseconds => TimeSpan.FromMilliseconds(count),
                TimeType.Seconds => TimeSpan.FromSeconds(count),
                TimeType.Minutes => TimeSpan.FromMinutes(count),
                TimeType.Hours => TimeSpan.FromHours(count),
                TimeType.Days => TimeSpan.FromDays(count),
                _ => throw new EnumUndefinedOrNotSupportedException<TimeType>(type, nameof(type), null)
            };
        }

        public static Int32 ToMilliseconds(this TimeSpan value)
        {
            return (Int32) value.TotalMilliseconds.ToRange(Int32.MinValue, Int32.MaxValue);
        }

        public static Int64 ToLongMilliseconds(this TimeSpan value)
        {
            return (Int64) value.TotalMilliseconds.ToRange(Int64.MinValue, Int64.MaxValue);
        }

        public static Int32 ToTimeoutMilliseconds(this TimeSpan value)
        {
            if (value == Timeout.InfiniteTimeSpan)
            {
                return -1;
            }

            return value > TimeSpan.Zero ? value.ToMilliseconds() : 0;
        }

        public static TimeSpan Sum(this IEnumerable<TimeSpan> source)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            return source.Aggregate(TimeSpan.Zero, (current, next) => current + next);
        }

        public static TimeSpan Sum<T>(this IEnumerable<T> source, Func<T, TimeSpan> selector)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            if (selector is null)
            {
                throw new ArgumentNullException(nameof(selector));
            }

            return source.Aggregate(TimeSpan.Zero, (current, next) => current + selector(next));
        }

        public static TimeSpan Multiply(this TimeSpan value, Int32 factor)
        {
            return new TimeSpan(value.Ticks * factor);
        }

        public static TimeSpan Multiply(this TimeSpan value, UInt32 factor)
        {
            return new TimeSpan(value.Ticks * factor);
        }

        public static TimeSpan Multiply(this TimeSpan value, Int64 factor)
        {
            return new TimeSpan(value.Ticks * factor);
        }

        public static TimeSpan Multiply(this TimeSpan value, Double factor)
        {
            return new TimeSpan((Int64) (value.Ticks * factor));
        }

        public static TimeSpan Divide(this TimeSpan value, Int32 factor)
        {
            return new TimeSpan(value.Ticks / factor);
        }

        public static TimeSpan Divide(this TimeSpan value, UInt32 factor)
        {
            return new TimeSpan(value.Ticks / factor);
        }

        public static TimeSpan Divide(this TimeSpan value, Int64 factor)
        {
            return new TimeSpan(value.Ticks / factor);
        }

        public static TimeSpan Divide(this TimeSpan value, Double factor)
        {
            return new TimeSpan((Int64) (value.Ticks / factor));
        }

        public static Double Divide(this TimeSpan value, TimeSpan other)
        {
            return (Double) value.Ticks / other.Ticks;
        }

        public static IEnumerable<TimeSpan> Range(TimeSpan stop)
        {
            return Range(TimeSpan.Zero, stop);
        }

        public static IEnumerable<TimeSpan> Range(TimeSpan start, TimeSpan stop, TimeType type = TimeType.Minutes)
        {
            return Range(start, stop, From(type));
        }

        public static IEnumerable<TimeSpan> Range(TimeSpan start, TimeSpan stop, TimeSpan step)
        {
            for (TimeSpan current = start; current < stop; current += step)
            {
                yield return current;
            }
        }

        /// <summary>
        /// Averages a list of TimeSpans
        /// </summary>
        /// <param name="source">List of TimeSpans</param>
        /// <returns>The average value</returns>
        public static TimeSpan Average(this IEnumerable<TimeSpan> source)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            Double? average = source.AverageOrDefault(time => time.Ticks);
            return average is not null ? new TimeSpan((Int64) average) : TimeSpan.Zero;
        }

        /// <summary>
        /// Days in the TimeSpan minus the months and years
        /// </summary>
        /// <param name="span">TimeSpan to get the days from</param>
        /// <returns>The number of days minus the months and years that the TimeSpan has</returns>
        public static Int32 DaysRemainder(this TimeSpan span)
        {
            return (DateTime.MinValue + span).Day - 1;
        }

        /// <summary>
        /// Months in the TimeSpan
        /// </summary>
        /// <param name="span">TimeSpan to get the months from</param>
        /// <returns>The number of months that the TimeSpan has</returns>
        public static Int32 Months(this TimeSpan span)
        {
            return (DateTime.MinValue + span).Month - 1;
        }

        /// <summary>
        /// Years in the TimeSpan
        /// </summary>
        /// <param name="span">TimeSpan to get the years from</param>
        /// <returns>The number of years that the TimeSpan has</returns>
        public static Int32 Years(this TimeSpan span)
        {
            return (DateTime.MinValue + span).Year - 1;
        }

        /// <summary>
        /// Replaces negative <paramref name="timeout"/> value with <see cref="Timeout.InfiniteTimeSpan"/>.
        /// </summary>
        /// <remarks>
        /// Use case scenario: methods that accept timeout often accept only <see cref="Timeout.InfiniteTimeSpan"/>
        /// but not other negative values.
        /// </remarks>
        public static TimeSpan AdjustTimeout(this TimeSpan timeout)
        {
            return AdjustTimeout(timeout, false);
        }

        /// <summary>
        /// Replaces negative <paramref name="timeout"/> value with <see cref="Timeout.InfiniteTimeSpan"/>.
        /// If <paramref name="infinity"/> is <c>true</c>, <see cref="TimeSpan.Zero"/> value is treated as <see cref="Timeout.InfiniteTimeSpan"/>
        /// </summary>
        /// <remarks>
        /// Use case scenario: methods that accept timeout often accept only <see cref="Timeout.InfiniteTimeSpan"/>
        /// but not other negative values.
        /// Motivation for <paramref name="infinity"/>:
        /// default timeout in configs often means 'infinite timeout', not 'do not wait and return immediately'.
        /// </remarks>
        public static TimeSpan AdjustTimeout(this TimeSpan timeout, Boolean infinity)
        {
            if (infinity)
            {
                return timeout <= TimeSpan.Zero ? Timeout.InfiniteTimeSpan : timeout;
            }

            return timeout < TimeSpan.Zero ? Timeout.InfiniteTimeSpan : timeout;
        }

        /// <summary>
        /// Limits timeout by upper limit.
        /// Replaces negative <paramref name="timeout"/> value with <see cref="Timeout.InfiniteTimeSpan"/>.
        /// </summary>
        /// <remarks>
        /// Use case scenario: methods that accept timeout often accept only <see cref="Timeout.InfiniteTimeSpan"/>
        /// but not other negative values.
        /// </remarks>
        public static TimeSpan AdjustTimeout(this TimeSpan timeout, TimeSpan upper)
        {
            return AdjustTimeout(timeout, upper, false);
        }

        /// <summary>
        /// Limits timeout by upper limit.
        /// Replaces negative <paramref name="timeout"/> value with <see cref="Timeout.InfiniteTimeSpan"/>.
        /// If <paramref name="infinite"/> is <c>true</c>, <see cref="TimeSpan.Zero"/> value is treated as <see cref="Timeout.InfiniteTimeSpan"/>
        /// </summary>
        /// <remarks>
        /// Use case scenario: methods that accept timeout often accept only <see cref="Timeout.InfiniteTimeSpan"/>
        /// but not other negative values.
        /// Motivation for <paramref name="infinite"/>:
        /// default timeout in configs often means 'infinite timeout', not 'do not wait and return immediately'.
        /// </remarks>
        public static TimeSpan AdjustTimeout(this TimeSpan timeout, TimeSpan upper, Boolean infinite)
        {
            timeout = timeout.AdjustTimeout(infinite);
            upper = upper.AdjustTimeout(infinite);

            if (upper < TimeSpan.Zero)
            {
                return timeout;
            }

            if (timeout < TimeSpan.Zero || timeout > upper)
            {
                return upper;
            }

            return timeout;
        }

        /// <summary>
        /// Calculates exponential backoff timeout for specific retry attempt.
        /// Returns timeout equal to (2^(<paramref name="attempt"/>-1)) limited to <paramref name="tries"/>
        /// Method applies ±20% jitter to the return value to provide even distribution of the timeouts.
        /// </summary>
        /// <remarks>
        /// </remarks>
        public static TimeSpan ExponentialBackoffTimeout(Int32 attempt, TimeSpan interval, TimeSpan tries)
        {
            if (interval <= TimeSpan.Zero)
            {
                return interval;
            }

            if (attempt <= 0)
            {
                attempt = 1;
            }

            // 0.8..1.2
            Double jitter = 0.8 + RandomUtilities.NextDouble() * 0.4;

            // (2^retryCount - 1) * jitter
            Double scale = Math.Pow(2.0, attempt - 1.0);

            // limit before multiply to ensure we are in valid values range.
            Double maxScale = tries.TotalMilliseconds / interval.TotalMilliseconds;

            // Apply scale coefficient and jitter
            Double resultScale = Math.Min(scale, maxScale) * jitter;

            // Truncate by max retry interval
            return interval.Multiply(resultScale);
        }
    }
}