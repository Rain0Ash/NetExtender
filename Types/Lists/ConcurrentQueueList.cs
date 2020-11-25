// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Linq;

namespace NetExtender.Types.Lists
{
    public class ConcurrentQueueList<T> : EventQueueList<T>
    {
        public new void Add(T item)
        {
            lock (this)
            {
                base.Add(item);
            }
        }

        public new T Pop(Int32 index = 0)
        {
            lock (this)
            {
                return base.Pop(index);
            }
        }

        public T Get(Int32 index)
        {
            lock (this)
            {
                return this.ElementAtOrDefault(index);
            }
        }

        public new void Clear()
        {
            lock (this)
            {
                base.Clear();
            }
        }

        public new Boolean Contains(T item)
        {
            lock (this)
            {
                return base.Contains(item);
            }
        }

        public new Boolean Exists(Predicate<T> match)
        {
            lock (this)
            {
                return base.Exists(match);
            }
        }
    }
}