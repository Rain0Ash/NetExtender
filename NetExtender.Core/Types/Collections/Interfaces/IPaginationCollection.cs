// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Collections.Generic;
using NetExtender.Types.Enumerables.Interfaces;

namespace NetExtender.Types.Collections.Interfaces
{
    public interface IPaginationCollection<T, out TCollection> : IPaginationEnumerable<T, TCollection>, IPaginationCollection<T> where TCollection : class, ICollection<T>
    {
    }
    
    public interface IPaginationCollection<T> : IPaginationEnumerable<T>, ICollection<T>
    {
        public new Int32 Count { get; }
    }
}