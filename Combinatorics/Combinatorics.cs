﻿﻿// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

  using System;
  using System.Collections.Generic;

  namespace NetExtender.Combinatorics
{
    public enum CombinatoricsType
    {
        Permutations,
        PermutationsWithRepetition,

        Combinations,
        CombinationsWithRepetition,

        Variations,
        VariationsWithRepetition
    }
    
    public static class Combinatorics
    {
        public static IMetaCollection<T> Create<T>(IList<T> values, CombinatoricsType type, Int32 take = 2)
        {
            return type switch
            {
                CombinatoricsType.Permutations => new Permutations<T>(values),
                CombinatoricsType.PermutationsWithRepetition => new Permutations<T>(values, GenerateOption.WithRepetition),
                CombinatoricsType.Combinations => new Combinations<T>(values, take),
                CombinatoricsType.CombinationsWithRepetition => new Combinations<T>(values, take, GenerateOption.WithRepetition),
                CombinatoricsType.Variations => new Variations<T>(values, take),
                CombinatoricsType.VariationsWithRepetition => new Variations<T>(values, take, GenerateOption.WithRepetition),
                _ => throw new NotSupportedException()
            };
        }
    }
}