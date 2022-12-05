// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using Microsoft.Extensions.Primitives;
using NetExtender.Types.Exceptions;

namespace NetExtender.Utilities.Types
{
    public struct StringSegmentEnumerator
    {
        private StringSegment Value { get; }
        public Char Current { get; private set; }
        private Int32 Index { get; set; }

        public StringSegmentEnumerator(StringSegment segment)
        {
            Value = segment;
            Index = 0;
            Current = Char.MinValue;
        }

        [MethodImpl(MethodImplOptions.AggressiveOptimization)]
        public Boolean MoveNext()
        {
            if (Index >= Value.Length)
            {
                Current = Char.MinValue;
                return false;
            }

            Current = Value[++Index];
            return true;
        }
    }

    public static class StringSegmentUtilities
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static StringSegmentEnumerator GetEnumerator(this StringSegment segment)
        {
            return new StringSegmentEnumerator(segment);
        }

        public static IEnumerable<Char> AsEnumerable(this StringSegment segment)
        {
            foreach (Char character in segment)
            {
                yield return character;
            }
        }

        public static Int32 CharLength(this StringSegment[] values)
        {
            if (values is null)
            {
                throw new ArgumentNullException(nameof(values));
            }

            return values.Length switch
            {
                0 => 0,
                1 => values[0].Length,
                _ => values.Sum(segment => segment.Length)
            };
        }

        public static Int32 CharLength(this IEnumerable<StringSegment> values)
        {
            if (values is null)
            {
                throw new ArgumentNullException(nameof(values));
            }

            return values.Sum(str => str.Length);
        }

        public static Int64 CharLongLength(this StringSegment[] values)
        {
            if (values is null)
            {
                throw new ArgumentNullException(nameof(values));
            }

            return values.Length switch
            {
                0 => 0,
                1 => values[0].Length,
                _ => values.Aggregate<StringSegment, Int64>(0, (current, segment) => current + segment.Length)
            };
        }

        public static Int64 CharLongLength(this IEnumerable<StringSegment> values)
        {
            if (values is null)
            {
                throw new ArgumentNullException(nameof(values));
            }

            return values.Aggregate<StringSegment, Int64>(0, (current, segment) => current + segment.Length);
        }

        public static Boolean IsNumeric(this StringSegment value)
        {
            foreach (Char character in value)
            {
                if (!Char.IsDigit(character))
                {
                    return false;
                }
            }

            return true;
        }

        public static Boolean IsAlphabetic(this StringSegment value)
        {
            foreach (Char character in value)
            {
                if (!Char.IsLetter(character))
                {
                    return false;
                }
            }

            return true;
        }

        public static Boolean IsAlphanumeric(this StringSegment value)
        {
            foreach (Char character in value)
            {
                if (!Char.IsLetterOrDigit(character))
                {
                    return false;
                }
            }

            return true;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean StartsWith(this StringSegment value, String substring)
        {
            if (substring is null)
            {
                throw new ArgumentNullException(nameof(substring));
            }

            return value.StartsWith(substring, StringComparison.Ordinal);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean StartsWith(this StringSegment value, IEnumerable<String?> substrings)
        {
            return StartsWith(value, substrings, StringComparison.Ordinal);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean StartsWith(this StringSegment value, IEnumerable<String?> substrings, StringComparison comparison)
        {
            if (substrings is null)
            {
                throw new ArgumentNullException(nameof(substrings));
            }

            return substrings.WhereNotNull().Any(item => value.StartsWith(item, comparison));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean EndsWith(this StringSegment value, String substring)
        {
            if (substring is null)
            {
                throw new ArgumentNullException(nameof(substring));
            }

            return value.EndsWith(substring, StringComparison.Ordinal);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean EndsWith(this StringSegment value, IEnumerable<String?> substrings)
        {
            return EndsWith(value, substrings, StringComparison.Ordinal);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean EndsWith(this StringSegment value, IEnumerable<String?> substrings, StringComparison comparison)
        {
            if (substrings is null)
            {
                throw new ArgumentNullException(nameof(substrings));
            }

            return substrings.WhereNotNull().Any(item => value.EndsWith(item, comparison));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean Equals(this String value, StringSegment segment)
        {
            if (value is null)
            {
                throw new ArgumentNullException(nameof(value));
            }

            return segment.Equals(value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean Equals(this String value, StringSegment segment, StringComparison comparison)
        {
            if (value is null)
            {
                throw new ArgumentNullException(nameof(value));
            }

            return segment.Equals(value, comparison);
        }

        public static Boolean Equals(this String value, StringSegment segment, Int32 start)
        {
            if (value is null)
            {
                throw new ArgumentNullException(nameof(value));
            }

            if (start < 0 || start >= value.Length)
            {
                throw new ArgumentOutOfRangeException(nameof(start), start, "The start index cannot be less than zero or greater than the length of the string.");
            }

            return segment.Equals(new StringSegment(value, start, value.Length - start));
        }

        public static Boolean Equals(this String value, StringSegment segment, Int32 start, StringComparison comparison)
        {
            if (value is null)
            {
                throw new ArgumentNullException(nameof(value));
            }

            if (start < 0 || start >= value.Length)
            {
                throw new ArgumentOutOfRangeException(nameof(start), start, "The start index cannot be less than zero or greater than the length of the string.");
            }

            return segment.Equals(new StringSegment(value, start, value.Length - start), comparison);
        }

        public static Boolean Equals(this String value, StringSegment segment, Int32 start, Int32 length)
        {
            if (value is null)
            {
                throw new ArgumentNullException(nameof(value));
            }

            if (start < 0 || start >= value.Length)
            {
                throw new ArgumentOutOfRangeException(nameof(start), start, "The start index cannot be less than zero or greater than the length of the string.");
            }

            if (length < 0 || start + length > value.Length)
            {
                throw new ArgumentOutOfRangeException(nameof(length), length, null);
            }

            return segment.Equals(new StringSegment(value, start, length));
        }

        public static Boolean Equals(this String value, StringSegment segment, Int32 start, Int32 length, StringComparison comparison)
        {
            if (value is null)
            {
                throw new ArgumentNullException(nameof(value));
            }

            if (start < 0 || start >= value.Length)
            {
                throw new ArgumentOutOfRangeException(nameof(start), start, "The start index cannot be less than zero or greater than the length of the string.");
            }

            if (length < 0 || start + length > value.Length)
            {
                throw new ArgumentOutOfRangeException(nameof(length), length, null);
            }

            return segment.Equals(new StringSegment(value, start, length), comparison);
        }

        public static Boolean Equals(this String value, String? other, Int32 start)
        {
            if (value is null)
            {
                throw new ArgumentNullException(nameof(value));
            }

            if (start < 0 || start >= value.Length)
            {
                throw new ArgumentOutOfRangeException(nameof(start), start, "The start index cannot be less than zero or greater than the length of the string.");
            }

            return other is not null && new StringSegment(value, start, value.Length - start).Equals(other);
        }

        public static Boolean Equals(this String value, String? other, Int32 start, StringComparison comparison)
        {
            if (value is null)
            {
                throw new ArgumentNullException(nameof(value));
            }

            if (start < 0 || start >= value.Length)
            {
                throw new ArgumentOutOfRangeException(nameof(start), start, "The start index cannot be less than zero or greater than the length of the string.");
            }

            return other is not null && new StringSegment(value, start, value.Length - start).Equals(other, comparison);
        }

        public static Boolean Equals(this String value, String? other, Int32 start, Int32 length)
        {
            if (value is null)
            {
                throw new ArgumentNullException(nameof(value));
            }

            if (start < 0 || start >= value.Length)
            {
                throw new ArgumentOutOfRangeException(nameof(start), start, "The start index cannot be less than zero or greater than the length of the string.");
            }

            if (length < 0 || start + length > value.Length)
            {
                throw new ArgumentOutOfRangeException(nameof(length), length, null);
            }

            return other is not null && new StringSegment(value, start, length).Equals(other);
        }

        public static Boolean Equals(this String value, String? other, Int32 start, Int32 length, StringComparison comparison)
        {
            if (value is null)
            {
                throw new ArgumentNullException(nameof(value));
            }

            if (start < 0 || start >= value.Length)
            {
                throw new ArgumentOutOfRangeException(nameof(start), start, "The start index cannot be less than zero or greater than the length of the string.");
            }

            if (length < 0 || start + length > value.Length)
            {
                throw new ArgumentOutOfRangeException(nameof(length), length, null);
            }

            return other is not null && new StringSegment(value, start, length).Equals(other, comparison);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Int32 CompareTo(this StringSegment value, String? other)
        {
            return CompareTo(value, other, StringComparison.Ordinal);
        }

        public static Int32 CompareTo(this StringSegment value, String? other, StringComparison comparison)
        {
            return comparison switch
            {
                StringComparison.Ordinal => StringSegmentComparer.Ordinal.Compare(value, other),
                StringComparison.OrdinalIgnoreCase => StringSegmentComparer.OrdinalIgnoreCase.Compare(value, other),
                StringComparison.CurrentCulture => StringSegmentComparer.Ordinal.Compare(value, other),
                StringComparison.CurrentCultureIgnoreCase => StringSegmentComparer.OrdinalIgnoreCase.Compare(value, other),
                StringComparison.InvariantCulture => StringSegmentComparer.Ordinal.Compare(value, other),
                StringComparison.InvariantCultureIgnoreCase => StringSegmentComparer.OrdinalIgnoreCase.Compare(value, other),
                _ => throw new EnumUndefinedOrNotSupportedException<StringComparison>(comparison, nameof(comparison), null)
            };
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Int32 CompareTo(this String value, StringSegment other)
        {
            return CompareTo(value, other, StringComparison.Ordinal);
        }

        public static Int32 CompareTo(this String value, StringSegment other, StringComparison comparison)
        {
            if (value is null)
            {
                throw new ArgumentNullException(nameof(value));
            }

            return comparison switch
            {
                StringComparison.Ordinal => StringSegmentComparer.Ordinal.Compare(value, other),
                StringComparison.OrdinalIgnoreCase => StringSegmentComparer.OrdinalIgnoreCase.Compare(value, other),
                StringComparison.CurrentCulture => StringSegmentComparer.Ordinal.Compare(value, other),
                StringComparison.CurrentCultureIgnoreCase => StringSegmentComparer.OrdinalIgnoreCase.Compare(value, other),
                StringComparison.InvariantCulture => StringSegmentComparer.Ordinal.Compare(value, other),
                StringComparison.InvariantCultureIgnoreCase => StringSegmentComparer.OrdinalIgnoreCase.Compare(value, other),
                _ => throw new EnumUndefinedOrNotSupportedException<StringComparison>(comparison, nameof(comparison), null)
            };
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Int32 CompareTo(this String value, StringSegment other, Int32 start)
        {
            return CompareTo(value, other, start, StringComparison.Ordinal);
        }

        public static Int32 CompareTo(this String value, StringSegment other, Int32 start, StringComparison comparison)
        {
            if (value is null)
            {
                throw new ArgumentNullException(nameof(value));
            }

            if (start < 0 || start >= value.Length)
            {
                throw new ArgumentOutOfRangeException(nameof(start), start, "The start index cannot be less than zero or greater than the length of the string.");
            }

            StringSegment segment = new StringSegment(value, start, value.Length - start);
            return comparison switch
            {
                StringComparison.Ordinal => StringSegmentComparer.Ordinal.Compare(segment, other),
                StringComparison.OrdinalIgnoreCase => StringSegmentComparer.OrdinalIgnoreCase.Compare(segment, other),
                StringComparison.CurrentCulture => StringSegmentComparer.Ordinal.Compare(segment, other),
                StringComparison.CurrentCultureIgnoreCase => StringSegmentComparer.OrdinalIgnoreCase.Compare(segment, other),
                StringComparison.InvariantCulture => StringSegmentComparer.Ordinal.Compare(segment, other),
                StringComparison.InvariantCultureIgnoreCase => StringSegmentComparer.OrdinalIgnoreCase.Compare(segment, other),
                _ => throw new EnumUndefinedOrNotSupportedException<StringComparison>(comparison, nameof(comparison), null)
            };
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Int32 CompareTo(this String value, StringSegment other, Int32 start, Int32 length)
        {
            return CompareTo(value, other, start, length, StringComparison.Ordinal);
        }

        public static Int32 CompareTo(this String value, StringSegment other, Int32 start, Int32 length, StringComparison comparison)
        {
            if (value is null)
            {
                throw new ArgumentNullException(nameof(value));
            }

            if (start < 0 || start >= value.Length)
            {
                throw new ArgumentOutOfRangeException(nameof(start), start, "The start index cannot be less than zero or greater than the length of the string.");
            }

            if (length < 0 || start + length > value.Length)
            {
                throw new ArgumentOutOfRangeException(nameof(length), length, null);
            }

            StringSegment segment = new StringSegment(value, start, length);
            return comparison switch
            {
                StringComparison.Ordinal => StringSegmentComparer.Ordinal.Compare(segment, other),
                StringComparison.OrdinalIgnoreCase => StringSegmentComparer.OrdinalIgnoreCase.Compare(segment, other),
                StringComparison.CurrentCulture => StringSegmentComparer.Ordinal.Compare(segment, other),
                StringComparison.CurrentCultureIgnoreCase => StringSegmentComparer.OrdinalIgnoreCase.Compare(segment, other),
                StringComparison.InvariantCulture => StringSegmentComparer.Ordinal.Compare(segment, other),
                StringComparison.InvariantCultureIgnoreCase => StringSegmentComparer.OrdinalIgnoreCase.Compare(segment, other),
                _ => throw new EnumUndefinedOrNotSupportedException<StringComparison>(comparison, nameof(comparison), null)
            };
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Int32 CompareTo(this String value, String? other, Int32 start)
        {
            return CompareTo(value, other, start, StringComparison.Ordinal);
        }

        public static Int32 CompareTo(this String value, String? other, Int32 start, StringComparison comparison)
        {
            if (value is null)
            {
                throw new ArgumentNullException(nameof(value));
            }

            if (start < 0 || start >= value.Length)
            {
                throw new ArgumentOutOfRangeException(nameof(start), start, "The start index cannot be less than zero or greater than the length of the string.");
            }

            if (other is null)
            {
                return 1;
            }

            StringSegment segment = new StringSegment(value, start, value.Length - start);
            return comparison switch
            {
                StringComparison.Ordinal => StringSegmentComparer.Ordinal.Compare(segment, other),
                StringComparison.OrdinalIgnoreCase => StringSegmentComparer.OrdinalIgnoreCase.Compare(segment, other),
                StringComparison.CurrentCulture => StringSegmentComparer.Ordinal.Compare(segment, other),
                StringComparison.CurrentCultureIgnoreCase => StringSegmentComparer.OrdinalIgnoreCase.Compare(segment, other),
                StringComparison.InvariantCulture => StringSegmentComparer.Ordinal.Compare(segment, other),
                StringComparison.InvariantCultureIgnoreCase => StringSegmentComparer.OrdinalIgnoreCase.Compare(segment, other),
                _ => throw new EnumUndefinedOrNotSupportedException<StringComparison>(comparison, nameof(comparison), null)
            };
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Int32 CompareTo(this String value, String? other, Int32 start, Int32 length)
        {
            return CompareTo(value, other, start, length, StringComparison.Ordinal);
        }

        public static Int32 CompareTo(this String value, String? other, Int32 start, Int32 length, StringComparison comparison)
        {
            if (value is null)
            {
                throw new ArgumentNullException(nameof(value));
            }

            if (start < 0 || start >= value.Length)
            {
                throw new ArgumentOutOfRangeException(nameof(start), start, "The start index cannot be less than zero or greater than the length of the string.");
            }

            if (length < 0 || start + length > value.Length)
            {
                throw new ArgumentOutOfRangeException(nameof(length), length, null);
            }

            if (other is null)
            {
                return 1;
            }

            StringSegment segment = new StringSegment(value, start, length);
            return comparison switch
            {
                StringComparison.Ordinal => StringSegmentComparer.Ordinal.Compare(segment, other),
                StringComparison.OrdinalIgnoreCase => StringSegmentComparer.OrdinalIgnoreCase.Compare(segment, other),
                StringComparison.CurrentCulture => StringSegmentComparer.Ordinal.Compare(segment, other),
                StringComparison.CurrentCultureIgnoreCase => StringSegmentComparer.OrdinalIgnoreCase.Compare(segment, other),
                StringComparison.InvariantCulture => StringSegmentComparer.Ordinal.Compare(segment, other),
                StringComparison.InvariantCultureIgnoreCase => StringSegmentComparer.OrdinalIgnoreCase.Compare(segment, other),
                _ => throw new EnumUndefinedOrNotSupportedException<StringComparison>(comparison, nameof(comparison), null)
            };
        }
    }
}