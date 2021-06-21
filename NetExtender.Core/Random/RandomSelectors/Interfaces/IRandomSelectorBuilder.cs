// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;

namespace NetExtender.Random
{
    /// <summary>
    /// Interface for Random Selector Builders.
    /// </summary>
    /// <typeparam name="T">Type of items that gets randomly returned</typeparam>
    public interface IRandomSelectorBuilder<out T>
    {
        IRandomSelector<T> Build();
        IRandomSelector<T> Build(Int32 seed);
    }
}