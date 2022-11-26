// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Threading;
using NetExtender.Types.Concurrent.Stores.Interfaces;
using NetExtender.Types.Stores;
using NetExtender.Types.Stores.Interfaces;

namespace NetExtender.Types.Concurrent.Stores
{
    public class ConcurrentRegisterStore<TKey> : ConcurrentRegisterStore<TKey, UInt64, RegisterStore<TKey>>, IConcurrentRegisterStore<TKey> where TKey : class
    {
    }

    public abstract class ConcurrentRegisterStore<TKey, TValue, TStore> : IConcurrentRegisterStore<TKey, TValue>
        where TKey : class where TValue : struct, IEquatable<TValue> where TStore : class, IRegisterStore<TKey, TValue>, new()
    {
        protected TStore Store { get; } = new TStore();

        public TKey? Current
        {
            get
            {
                return Store.Current;
            }
        }

        public TValue Value
        {
            get
            {
                return Store.Value;
            }
        }

        public Boolean IsLock
        {
            get
            {
                return Monitor.IsEntered(Store);
            }
        }

        public Boolean Lock()
        {
            return Monitor.TryEnter(Store);
        }

        public Boolean Lock(TimeSpan timeout)
        {
            return Monitor.TryEnter(Store, timeout);
        }

        public Boolean Unlock()
        {
            try
            {
                Monitor.Exit(Store);
                return true;
            }
            catch (SynchronizationLockException)
            {
                return false;
            }
        }

        public Boolean Contains(TKey key)
        {
            lock (Store)
            {
                return Store.Contains(key);
            }
        }

        public Boolean TryGetValue(TKey key, out TValue value)
        {
            lock (Store)
            {
                return Store.TryGetValue(key, out value);
            }
        }

        public Boolean TryGetKey(TValue value, [MaybeNullWhen(false)] out TKey key)
        {
            lock (Store)
            {
                return Store.TryGetKey(value, out key);
            }
        }

        public TValue Register(TKey key)
        {
            lock (Store)
            {
                return Store.Register(key);
            }
        }

        public Boolean Remove(TKey key)
        {
            lock (Store)
            {
                return Store.Remove(key);
            }
        }

        public void Clear()
        {
            lock (Store)
            {
                Store.Clear();
            }
        }

        public IEnumerator<KeyValuePair<TKey, TValue>> GetEnumerator()
        {
            return Store.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return ((IEnumerable) Store).GetEnumerator();
        }

        public TValue this[TKey key]
        {
            get
            {
                lock (Store)
                {
                    return Store[key];
                }
            }
        }
    }
}