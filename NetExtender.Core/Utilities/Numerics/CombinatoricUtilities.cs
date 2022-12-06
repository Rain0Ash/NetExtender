// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Collections.Generic;
using NetExtender.Types.Combinatoric;
using NetExtender.Types.Combinatoric.Interfaces;
using NetExtender.Types.Exceptions;

namespace NetExtender.Utilities.Numerics
{
    public static class CombinatoricUtilities
    {
        public static ICombinatoricCollection<T> Combinatoric<T>(IEnumerable<T> values, CombinatoricsType type)
        {
            return Combinatoric(values, type, 2);
        }

        public static ICombinatoricCollection<T> Combinatoric<T>(IEnumerable<T> values, CombinatoricsType type, Int32 take)
        {
            return type switch
            {
                CombinatoricsType.Permutations => new Permutations<T>(values),
                CombinatoricsType.PermutationsWithRepetition => new Permutations<T>(values, true),
                CombinatoricsType.Combinations => new Combinations<T>(values, take),
                CombinatoricsType.CombinationsWithRepetition => new Combinations<T>(values, take, true),
                CombinatoricsType.Variations => new Variations<T>(values, take),
                CombinatoricsType.VariationsWithRepetition => new Variations<T>(values, take, true),
                _ => throw new EnumUndefinedOrNotSupportedException<CombinatoricsType>(type, nameof(type), null)
            };
        }

        public static ICombinatoricCollection<T> Combinatoric<T>(this CombinatoricsType type, IEnumerable<T> values)
        {
            return Combinatoric(values, type, 2);
        }

        public static ICombinatoricCollection<T> Combinatoric<T>(this CombinatoricsType type, IEnumerable<T> values, Int32 take)
        {
            return Combinatoric(values, type, take);
        }
    }
}