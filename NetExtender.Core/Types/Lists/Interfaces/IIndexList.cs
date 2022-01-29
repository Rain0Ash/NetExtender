// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System.Collections.Generic;

namespace NetExtender.Initializer.Types.Lists.Interfaces
{
    public interface IIndexList<T> : IList<T>
    {
        public IEqualityComparer<T> Comparer { get; }
    }
}