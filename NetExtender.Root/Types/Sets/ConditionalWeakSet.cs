// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using NetExtender.Types.Sets.Interfaces;
using NetExtender.Utilities.Types;
#pragma warning disable CS8634

namespace NetExtender.Types.Sets
{
    public class ConditionalWeakSet<T> : ConditionalWeakSet, IWeakSet<T>, IReadOnlyWeakSet<T> where T : class?
    {
        protected ConditionalWeakTable<T, SyncRoot> Internal { get; } = new ConditionalWeakTable<T, SyncRoot>();

        public Boolean Contains(T item)
        {
            return item is not null && Internal.Contains(item);
        }

        public Boolean Add(T item)
        {
            if (item is null)
            {
                return false;
            }

            try
            {
                if (Contains(item))
                {
                    return false;
                }

                Internal.Add(item, SyncRoot);
                return true;
            }
            catch (ArgumentException)
            {
                return false;
            }
        }

        public Boolean Remove(T item)
        {
            return item is not null && Internal.Remove(item);
        }

        public void Clear()
        {
            Internal.Clear();
        }

        public IEnumerator<T> GetEnumerator()
        {
            foreach ((T key, _) in Internal)
            {
                yield return key;
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }

    public abstract class ConditionalWeakSet : IEnumerable
    {
        protected static SyncRoot SyncRoot { get; } = SyncRoot.Create();

        IEnumerator IEnumerable.GetEnumerator()
        {
            return (this as IEnumerable).GetEnumerator();
        }
    }
}