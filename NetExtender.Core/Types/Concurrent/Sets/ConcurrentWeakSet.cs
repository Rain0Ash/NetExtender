// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System.Collections.Generic;
using NetExtender.Types.Sets;
using NetExtender.Types.Sets.Interfaces;

namespace System.Collections.Concurrent
{
    public class ConcurrentWeakSet<T> : IWeakSet<T>, IReadOnlyWeakSet<T> where T : class
    {
        protected IWeakSet<T> Internal { get; } = new ConditionalWeakSet<T>();

        public Boolean Contains(T item)
        {
            lock (Internal)
            {
                return Internal.Contains(item);
            }
        }

        public Boolean Add(T item)
        {
            lock (Internal)
            {
                return Internal.Add(item);
            }
        }

        public Boolean Remove(T item)
        {
            lock (Internal)
            {
                return Internal.Remove(item);
            }
        }

        public void Clear()
        {
            lock (Internal)
            {
                Internal.Clear();
            }
        }

        public IEnumerator<T> GetEnumerator()
        {
            return Internal.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return ((IEnumerable) Internal).GetEnumerator();
        }
    }
}