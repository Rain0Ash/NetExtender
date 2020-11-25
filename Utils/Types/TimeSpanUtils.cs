// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Threading;
using NetExtender.Utils.Numerics;

namespace NetExtender.Utils.Types
{
    public static class TimeSpanUtils
    {
	    public static TimeSpan From(this TimeType type, Double count = 1)
	    {
		    return type switch
		    {
			    TimeType.Milliseconds => TimeSpan.FromMilliseconds(count),
			    TimeType.Seconds => TimeSpan.FromSeconds(count),
			    TimeType.Minutes => TimeSpan.FromMinutes(count),
			    TimeType.Hours => TimeSpan.FromHours(count),
			    TimeType.Days => TimeSpan.FromDays(count),
			    _ => throw new NotSupportedException()
		    };
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
	    [SuppressMessage("ReSharper", "PossibleMultipleEnumeration")]
	    public static TimeSpan Average(this IEnumerable<TimeSpan> source)
	    {
		    return source?.Any() != true ? TimeSpan.Zero : new TimeSpan((Int64) source.Average(x => x.Ticks));
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
		/// If <paramref name="infiniteIfDefault"/> is <c>true</c>, <see cref="TimeSpan.Zero"/> value is treated as <see cref="Timeout.InfiniteTimeSpan"/>
		/// </summary>
		/// <remarks>
		/// Use case scenario: methods that accept timeout often accept only <see cref="Timeout.InfiniteTimeSpan"/>
		/// but not other negative values. Check <see cref="Delay{T}"/> as example.
		/// Motivation for <paramref name="infiniteIfDefault"/>:
		/// default timeout in configs often means 'infinite timeout', not 'do not wait and return immediately'.
		/// </remarks>
		public static TimeSpan AdjustTimeout(this TimeSpan timeout, Boolean infiniteIfDefault = false)
		{
			if (infiniteIfDefault)
			{
				return timeout <= TimeSpan.Zero ? Timeout.InfiniteTimeSpan : timeout;
			}

			return timeout < TimeSpan.Zero ? Timeout.InfiniteTimeSpan : timeout;
		}

		/// <summary>
		/// Limits timeout by upper limit.
		/// Replaces negative <paramref name="timeout"/> value with <see cref="Timeout.InfiniteTimeSpan"/>.
		/// If <paramref name="infiniteIfDefault"/> is <c>true</c>, <see cref="TimeSpan.Zero"/> value is treated as <see cref="Timeout.InfiniteTimeSpan"/>
		/// </summary>
		/// <remarks>
		/// Use case scenario: methods that accept timeout often accept only <see cref="Timeout.InfiniteTimeSpan"/>
		/// but not other negative values. Check <see cref="Delay{T}"/> as example.
		/// Motivation for <paramref name="infiniteIfDefault"/>:
		/// default timeout in configs often means 'infinite timeout', not 'do not wait and return immediately'.
		/// </remarks>
		public static TimeSpan AdjustTimeout(this TimeSpan timeout, TimeSpan upperLimit, Boolean infiniteIfDefault = false)
		{
			timeout = timeout.AdjustTimeout(infiniteIfDefault);
			upperLimit = upperLimit.AdjustTimeout(infiniteIfDefault);

			// Ignore upper limit if negative
			if (upperLimit < TimeSpan.Zero)
			{
				return timeout;
			}

			// Ignore timeout if negative or exceeds upper limit
			if (timeout < TimeSpan.Zero || timeout > upperLimit)
			{
				return upperLimit;
			}

			return timeout;
		}

		/// <summary>
		/// Calculates exponential backoff timeout for specific retry attempt.
		/// Returns timeout equal to (2^(<paramref name="retryAttempt"/>-1)) limited to <paramref name="maxRetryInterval"/>
		/// Method applies ±20% jitter to the return value to provide even distribution of the timeouts.
		/// </summary>
		/// <remarks>
		/// </remarks>
		public static TimeSpan ExponentialBackoffTimeout(Int32 retryAttempt, TimeSpan retryInterval, TimeSpan maxRetryInterval)
		{
			if (retryInterval <= TimeSpan.Zero)
			{
				return retryInterval;
			}

			if (retryAttempt <= 0)
			{
				retryAttempt = 1;
			}

			// 0.8..1.2
			Double jitter = 0.8 + RandomUtils.NextDouble() * 0.4;

			// (2^retryCount - 1) * jitter
			Double scale = Math.Pow(2.0, retryAttempt - 1.0);

			// limit before multiply to ensure we are in valid values range.
			Double maxScale = maxRetryInterval.TotalMilliseconds / retryInterval.TotalMilliseconds;

			// Apply scale coefficient and jitter
			Double resultScale = Math.Min(scale, maxScale) * jitter;

			// Truncate by max retry interval
			return retryInterval.Multiply(resultScale);
		}
    }
}