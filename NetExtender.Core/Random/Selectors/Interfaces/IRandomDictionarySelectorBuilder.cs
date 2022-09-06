// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Collections.Generic;
using NetExtender.Types.Dictionaries.Interfaces;

namespace NetExtender.Random
{
    /// <summary>
    /// Interface for Random Selector Builders.
    /// </summary>
    /// <typeparam name="T">Type of items that gets randomly returned</typeparam>
    // ReSharper disable once PossibleInterfaceMemberAmbiguity
    public interface IRandomDictionarySelectorBuilder<T> : IRandomSelectorBuilder<T>, IIndexDictionary<T, Double> where T : notnull
    {
    }
}