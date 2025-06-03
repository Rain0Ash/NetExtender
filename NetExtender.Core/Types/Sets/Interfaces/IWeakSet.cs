// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Collections.Generic;

namespace NetExtender.Types.Sets.Interfaces
{
    public interface IWeakSet<T> : IEnumerable<T> where T : class
    {
        public Boolean Contains(T item);
        public Boolean Add(T item);
        public Boolean Remove(T item);
        public void Clear();
    }
}