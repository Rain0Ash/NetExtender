// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Collections.Generic;

namespace NetExtender.Types.Comparers
{
    public sealed class ComparerEqualityWrapper<T> : IComparer<T>, IEqualityComparer<T>
    {
        private IComparer<T> Internal { get; }
        
        public ComparerEqualityWrapper(IComparer<T> comparer)
        {
            Internal = comparer ?? throw new ArgumentNullException(nameof(comparer));
        }

        public Int32 Compare(T? x, T? y)
        {
            return Internal.Compare(x, y);
        }

        public Boolean Equals(T? x, T? y)
        {
            return Compare(x, y) == 0;
        }

        public Int32 GetHashCode(T obj)
        {
            return obj?.GetHashCode() ?? 0;
        }
    }
}