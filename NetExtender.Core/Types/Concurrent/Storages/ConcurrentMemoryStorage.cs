// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Threading;
using NetExtender.Types.Concurrent.Storages.Interfaces;
using NetExtender.Types.Storages;
using NetExtender.Types.Storages.Interfaces;

namespace NetExtender.Types.Concurrent.Storages
{
    public sealed class ConcurrentMemoryStorage<TKey, TValue> : ConcurrentMemoryStorage<TKey, TValue, MemoryStorage<TKey, TValue>> where TKey : class where TValue : struct, IEquatable<TValue>
    {
    }

    public abstract class ConcurrentMemoryStorage<TKey, TValue, TStorage> : IConcurrentMemoryStorage<TKey, TValue>, IReadOnlyConcurrentMemoryStorage<TKey, TValue> where TKey : class where TValue : struct, IEquatable<TValue> where TStorage : class, IMemoryStorage<TKey, TValue>, new()
    {
        protected TStorage Storage { get; } = new TStorage();

        public TKey? Current
        {
            get
            {
                return Storage.Current;
            }
        }

        public TValue Value
        {
            get
            {
                return Storage.Value;
            }
        }

        public Boolean IsLock
        {
            get
            {
                return Monitor.IsEntered(Storage);
            }
        }

        public Boolean Lock()
        {
            return Monitor.TryEnter(Storage);
        }

        public Boolean Lock(TimeSpan timeout)
        {
            return Monitor.TryEnter(Storage, timeout);
        }

        public Boolean Unlock()
        {
            try
            {
                Monitor.Exit(Storage);
                return true;
            }
            catch (SynchronizationLockException)
            {
                return false;
            }
        }

        public Boolean Contains(TKey key)
        {
            lock (Storage)
            {
                return Storage.Contains(key);
            }
        }

        public Boolean TryGetValue(TKey key, out TValue value)
        {
            lock (Storage)
            {
                return Storage.TryGetValue(key, out value);
            }
        }

        public Boolean TryGetKey(TValue value, [MaybeNullWhen(false)] out TKey key)
        {
            lock (Storage)
            {
                return Storage.TryGetKey(value, out key);
            }
        }

        public void Add(TKey key, TValue value)
        {
            lock (Storage)
            {
                Storage.Add(key, value);
            }
        }

        public void AddOrUpdate(TKey key, TValue value)
        {
            lock (Storage)
            {
                Storage.AddOrUpdate(key, value);
            }
        }

        public TValue GetOrAdd(TKey key, TValue value)
        {
            lock (Storage)
            {
                return Storage.GetOrAdd(key, value);
            }
        }

        public TValue GetOrAdd(TKey key, Func<TValue> factory)
        {
            lock (Storage)
            {
                return Storage.GetOrAdd(key, factory);
            }
        }

        public TValue GetOrAdd(TKey key, Func<TKey, TValue> factory)
        {
            lock (Storage)
            {
                return Storage.GetOrAdd(key, factory);
            }
        }

        public Boolean Remove(TKey key)
        {
            lock (Storage)
            {
                return Storage.Remove(key);
            }
        }

        public void Clear()
        {
            lock (Storage)
            {
                Storage.Clear();
            }
        }

        public IEnumerator<KeyValuePair<TKey, TValue>> GetEnumerator()
        {
            return Storage.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return ((IEnumerable) Storage).GetEnumerator();
        }

        public TValue this[TKey key]
        {
            get
            {
                lock (Storage)
                {
                    return Storage[key];
                }
            }
            set
            {
                lock (Storage)
                {
                    Storage[key] = value;
                }
            }
        }
    }
}