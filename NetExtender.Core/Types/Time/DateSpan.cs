// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Collections.Generic;
using System.Globalization;
using NetExtender.Utilities.Types;

namespace NetExtender.Types.Times
{
    /// <summary>
    /// Represents a date span
    /// </summary>
    public readonly struct DateSpan
    {
        /// <summary>
        /// Converts the object to a string
        /// </summary>
        /// <param name="value">Value to convert</param>
        /// <returns>The value as a string</returns>
        public static implicit operator String(DateSpan value)
        {
            return value.ToString();
        }
        
        /// <summary>
        /// Determines if two DateSpans are equal
        /// </summary>
        /// <param name="first">Span 1</param>
        /// <param name="second">Span 2</param>
        /// <returns>True if they are, false otherwise</returns>
        public static Boolean operator ==(DateSpan first, DateSpan second)
        {
            return first.Start == second.Start && first.End == second.End;
        }

        /// <summary>
        /// Determines if two DateSpans are not equal
        /// </summary>
        /// <param name="first">Span 1</param>
        /// <param name="second">Span 2</param>
        /// <returns>True if they are not equal, false otherwise</returns>
        public static Boolean operator !=(DateSpan first, DateSpan second)
        {
            return !(first == second);
        }

        /// <summary>
        /// Addition operator
        /// </summary>
        /// <param name="first">Span 1</param>
        /// <param name="second">Span 2</param>
        /// <returns>The combined date span</returns>
        public static DateSpan operator +(DateSpan first, DateSpan second)
        {
            DateTime start = first.Start < second.Start ? first.Start : second.Start;
            DateTime end = first.End > second.End ? first.End : second.End;
            return new DateSpan(start, end);
        }
        
        /// <summary>
        /// Start date
        /// </summary>
        public DateTime Start { get; }
        
        /// <summary>
        /// End date
        /// </summary>
        public DateTime End { get; }
        
        /// <summary>
        /// Years between the two dates
        /// </summary>
        public Int32 Years { get; }
        
        /// <summary>
        /// Months between the two dates
        /// </summary>
        public Int32 Months { get; }
        
        /// <summary>
        /// Days between the two dates
        /// </summary>
        public Int32 Days { get; }
        
        /// <summary>
        /// Hours between the two dates
        /// </summary>
        public Int32 Hours { get; }
        
        /// <summary>
        /// Minutes between the two dates
        /// </summary>
        public Int32 Minutes { get; }

        /// <summary>
        /// Seconds between the two dates
        /// </summary>
        public Int32 Seconds { get; }
        
        /// <summary>
        /// Milliseconds between the two dates
        /// </summary>
        public Int32 MilliSeconds { get; }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="start">Start of the date span</param>
        /// <param name="end">End of the date span</param>
        public DateSpan(DateTime start, DateTime end)
        {
            if (start > end)
            {
                DateTime temp = start;
                start = end;
                end = temp;
            }
            
            Start = start;
            End = end;
            
            TimeSpan diff = End - Start;

            Years = diff.Years();
            Months = diff.Months();
            Days = diff.DaysRemainder();
            Hours = diff.Hours;
            Minutes = diff.Minutes;
            Seconds = diff.Seconds;
            MilliSeconds = diff.Milliseconds;
        }

        /// <summary>
        /// Adds the specified values.
        /// </summary>
        /// <param name="first">The left.</param>
        /// <param name="second">The right.</param>
        /// <returns>The result.</returns>
        public static DateSpan Add(DateSpan first, DateSpan second)
        {
            return first + second;
        }

        /// <summary>
        /// Returns the intersecting time span between the two values
        /// </summary>
        /// <param name="span">Span to use</param>
        /// <returns>The intersection of the two time spans</returns>
        public DateSpan Intersection(DateSpan span)
        {
            if (!Overlap(span))
            {
                return span;
            }

            DateTime start = span.Start > Start ? span.Start : Start;
            DateTime end = span.End < End ? span.End : End;
            return new DateSpan(start, end);
        }

        /// <summary>
        /// Determines if two DateSpans overlap
        /// </summary>
        /// <param name="span">The span to compare to</param>
        /// <returns>True if they overlap, false otherwise</returns>
        public Boolean Overlap(DateSpan span)
        {
            return End <= span.End && End > span.Start || Start <= span.Start && End >= span.End;
        }
        
        /// <summary>
        /// Determines if two objects are equal
        /// </summary>
        /// <param name="obj">Object to check</param>
        /// <returns>True if they are, false otherwise</returns>
        public override Boolean Equals(Object? obj)
        {
            return obj is DateSpan span && span == this;
        }

        /// <summary>
        /// Gets the hash code for the date span
        /// </summary>
        /// <returns>The hash code</returns>
        public override Int32 GetHashCode()
        {
            return HashCode.Combine(Start, End);
        }

        /// <summary>
        /// Converts the DateSpan to a string
        /// </summary>
        /// <returns>The DateSpan as a string</returns>
        public override String ToString()
        {
            return $"Start: {Start.ToString(CultureInfo.InvariantCulture)} End: {End.ToString(CultureInfo.InvariantCulture)}";
        }
    }
}