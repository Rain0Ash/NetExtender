using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using NetExtender.Types.Immutable.Maps.Interfaces;
using NetExtender.Types.Maps.Interfaces;

namespace NetExtender.Utilities.Types
{
    public static class ValidationUtilities
    {
        private static IImmutableMap<Char, Char> Brackets { get; } = new Dictionary<Char, Char>(4)
        {
            {'(', ')'},
            {'{', '}'},
            {'[', ']'},
            {'<', '>'}
        }.ToImmutableMap();

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean IsBracketsWellFormed(this String value)
        {
            return IsBracketsWellFormed(value, Brackets);
        }

        // ReSharper disable once CognitiveComplexity
        public static Boolean IsBracketsWellFormed(this String value, IEnumerable<KeyValuePair<Char, Char>> pairs)
        {
            if (value is null)
            {
                throw new ArgumentNullException(nameof(value));
            }

            if (pairs is null)
            {
                throw new ArgumentNullException(nameof(pairs));
            }

            if (value.Length <= 0)
            {
                return true;
            }

            IReadOnlyMap<Char, Char> brackets = pairs.AsIReadOnlyMap();

            Stack<Char> order = new Stack<Char>();

            foreach (Char character in value)
            {
                if (brackets.ContainsKey(character))
                {
                    order.Push(character);
                    continue;
                }

                if (!brackets.ContainsValue(character))
                {
                    continue;
                }

                if (order.Count <= 0)
                {
                    return false;
                }

                if (character != brackets.GetValue(order.Peek()))
                {
                    return false;
                }

                order.Pop();
            }

            return order.Count <= 0;
        }
    }
}