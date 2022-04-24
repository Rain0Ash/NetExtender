// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using Microsoft.Extensions.Primitives;

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
    }
}