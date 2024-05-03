// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Collections;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using NetExtender.Types.History.Interfaces;

namespace NetExtender.Types.History
{
    public abstract class HistoryCollection<T> : IHistoryCollection<T> where T : IHistoryEntry
    {
        private ConcurrentStack<T> History { get; } = new ConcurrentStack<T>();

        public abstract T Snapshot { get; }

        public Int32 Count
        {
            get
            {
                return History.Count;
            }
        }

        public Boolean TryPeek([MaybeNullWhen(false)] out T entry)
        {
            return History.TryPeek(out entry);
        }

        public Boolean TryPop([MaybeNullWhen(false)] out T entry)
        {
            return History.TryPop(out entry);
        }
            
        public void Push(T entry)
        {
            if (entry is null)
            {
                throw new ArgumentNullException(nameof(entry));
            }

            History.Push(entry);
        }

        public T Save()
        {
            T entry = Snapshot;
            Push(entry);
            return entry;
        }

        public void Clear()
        {
            History.Clear();
        }

        public IEnumerator<T> GetEnumerator()
        {
            return History.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}