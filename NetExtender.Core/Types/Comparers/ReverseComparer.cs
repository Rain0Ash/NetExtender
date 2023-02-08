// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Collections.Generic;
using NetExtender.Types.Comparers.Interfaces;

namespace NetExtender.Types.Comparers
{
    public class ReverseComparer<T> : IReverseComparer<T>
    {
        public static ReverseComparer<T> Default { get; } = new ReverseComparer<T>();

        public IComparer<T> Original { get; }

        public ReverseComparer()
            : this(Comparer<T>.Default)
        {
        }

        public ReverseComparer(IComparer<T> comparer)
        {
            Original = comparer ?? throw new ArgumentNullException(nameof(comparer));
        }

        public Int32 Compare(T? x, T? y)
        {
            return -Original.Compare(x, y);
        }
    }
}