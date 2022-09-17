// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Collections.Generic;
using NetExtender.Types.Combinatoric.Interfaces;

namespace NetExtender.Types.Combinatoric
{
    public static class Combinatorics
    {
        public static ICombinatoricCollection<T> Create<T>(IList<T> values, CombinatoricsType type)
        {
            return Create(values, type, 2);
        }

        public static ICombinatoricCollection<T> Create<T>(IList<T> values, CombinatoricsType type, Int32 take)
        {
            return type switch
            {
                CombinatoricsType.Permutations => new Permutations<T>(values),
                CombinatoricsType.PermutationsWithRepetition => new Permutations<T>(values, true),
                CombinatoricsType.Combinations => new Combinations<T>(values, take),
                CombinatoricsType.CombinationsWithRepetition => new Combinations<T>(values, take, true),
                CombinatoricsType.Variations => new Variations<T>(values, take),
                CombinatoricsType.VariationsWithRepetition => new Variations<T>(values, take, true),
                _ => throw new NotSupportedException()
            };
        }
        
        public static ICombinatoricCollection<T> Create<T>(this CombinatoricsType type, IList<T> values)
        {
            return Create(values, type, 2);
        }

        public static ICombinatoricCollection<T> Create<T>(this CombinatoricsType type, IList<T> values, Int32 take)
        {
            return Create(values, type, take);
        }
    }
}