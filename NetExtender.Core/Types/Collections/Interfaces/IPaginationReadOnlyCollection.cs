// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Collections.Generic;
using NetExtender.Types.Enumerables.Interfaces;

namespace NetExtender.Types.Collections.Interfaces
{
    public interface IPaginationReadOnlyCollection<out T, out TCollection> : IPaginationEnumerable<T, TCollection>, IPaginationReadOnlyCollection<T> where TCollection : class, IReadOnlyCollection<T>
    {
    }
    
    public interface IPaginationReadOnlyCollection<out T> : IPaginationEnumerable<T>, IReadOnlyCollection<T>
    {
        public new Int32 Count { get; }
    }
}