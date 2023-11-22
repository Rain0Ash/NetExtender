// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Collections;
using System.Collections.Generic;

namespace NetExtender.Types.Enumerables.Interfaces
{
    public interface IPaginationEnumerable<out T, out TCollection> : IPaginationEnumerable<T> where TCollection : class, IEnumerable<T>
    {
        public TCollection Source { get; }
    }
    
    public interface IPaginationEnumerable<out T> : IPaginationEnumerable, IEnumerable<T>
    {
    }

    public interface IPaginationEnumerable : IEnumerable
    {
        public Int32 Page { get; }
        public Int32 Total { get; }
        public Int32 Items { get; }
        public Int32 Size { get; }
        
        public Boolean HasPrevious { get; }
        public Boolean HasNext { get; }
    }
}