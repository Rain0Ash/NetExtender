// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Runtime.CompilerServices;

namespace NetExtender.Utilities.Types
{
    //TODO: DateTime analog methods
    public static class DateTimeOffsetUtilities
    {
        /// <summary>
        /// Determines whether the provided text represents a valid <see cref="DateTimeOffset"/>.
        /// </summary>
        /// <param name="text">A string, possibly representing a valid date and time.</param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean IsDateTimeOffset(String text)
        {
            return DateTimeOffset.TryParse(text, out _);
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