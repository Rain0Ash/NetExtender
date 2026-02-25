// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System.Collections.Generic;

namespace NetExtender.Types.Comparers.Interfaces
{
    public interface IReverseComparer<in T> : IComparer<T>
    {
        public IComparer<T> Original { get; }
    }
}