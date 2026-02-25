using System;
using System.Collections;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;
using NetExtender.Monads;
using NetExtender.Types.Monads;
using NetExtender.Utilities.Types;

namespace NetExtender.Types.Storages
{
    public class KeyWeakStorage<TKey, TValue> : IReadOnlyCollection<TValue> where TValue : class
    {
        public TryParseHandler<TValue, TKey> Handler { get; }
        public Action<Key<TKey, TValue>>? Notify { get; }

        private SyncRoot SyncRoot { get; } = SyncRoot.Create();
        private ConcurrentQueue<Key<TKey, TValue>> Pool { get; } = new ConcurrentQueue<Key<TKey, TValue>>();
        private ConcurrentDictionary<Key<TKey, TValue>, Key<TKey, TValue>> Storage { get; }

        public Boolean AutoTrim { get; init; } = true;

        public Int32 Capacity
        {
            get
            {
                lock (SyncRoot)
                {
                    return Pool.Count + Storage.Count;
                }
            }
        }

        public Int32 Count
        {
            get
            {
                return Storage.Count;
            }
        }

        public KeyWeakStorage(Func<TValue, TKey> handler)
            : this(Convert(handler))
        {
        }

        public KeyWeakStorage(TryParseHandler<TValue, TKey> handler)
            : this(handler, null)
        {
        }

        public KeyWeakStorage(Func<TValue, TKey> handler, IEqualityComparer<TValue>? comparer)
            : this(Convert(handler), comparer)
        {
        }

        public KeyWeakStorage(TryParseHandler<TValue, TKey> handler, IEqualityComparer<TValue>? comparer)
            : this(handler, 0, comparer)
        {
        }

        public KeyWeakStorage(Func<TValue, TKey> handler, UInt16 capacity)
            : this(Convert(handler), capacity)
        {
        }

        public KeyWeakStorage(TryParseHandler<TValue, TKey> handler, UInt16 capacity)
            : this(handler, capacity, null)
        {
        }

        public KeyWeakStorage(Func<TValue, TKey> handler, UInt16 capacity, IEqualityComparer<TValue>? comparer)
            : this(Convert(handler), capacity, comparer)
        {
        }

        public KeyWeakStorage(TryParseHandler<TValue, TKey> handler, UInt16 capacity, IEqualityComparer<TValue>? comparer)
        {
            Handler = handler ?? throw new ArgumentNullException(nameof(handler));
            Storage = new ConcurrentDictionary<Key<TKey, TValue>, Key<TKey, TValue>>(new Comparer(comparer));
            Notify = Dispose;
            EnsureCapacity(capacity);
        }

        [return: NotNullIfNotNull("handler")]
        private static TryParseHandler<TValue, TKey>? Convert(Func<TValue, TKey>? handler)
        {
            if (handler is null)
            {
                return null;
            }

            Boolean Core(TValue value, [MaybeNullWhen(false)] out TKey result)
            {
                try
                {
                    result = handler(value);
                    return true;
                }
                catch (Exception)
                {
                    result = default;
                    return false;
                }
            }

            return Core;
        }

        public void TrimExcess()
        {
            Pool.Clear();
        }

        public Int32 TrimExcess(Int32 capacity)
        {
            if (capacity < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(capacity), capacity, null);
            }

            Int32 remove = Capacity - capacity;

            if (remove <= 0)
            {
                return 0;
            }

            Int32 count = 0;
            while (count < remove && Pool.TryDequeue(out Key<TKey, TValue>? node))
            {
                node.Dispose();
                count++;
            }

            return count;
        }

        public void EnsureCapacity(UInt16 capacity)
        {
            Int32 current = Capacity;
            if (current >= capacity)
            {
                return;
            }

            Int32 required = capacity - current;
            for (Int32 i = 0; i < required; i++)
            {
                Pool.Enqueue(Key.Create(Handler, Notify));
            }
        }

        public Boolean ContainsKey(TKey item)
        {
            return Storage.ContainsKey(Key.Search<TKey, TValue>(item));
        }

        public Boolean Contains(TValue item)
        {
            return Storage.ContainsKey(Key.Search(Handler, item));
        }

        public Boolean TryGetValue(TKey item, [MaybeNullWhen(false)] out TValue value)
        {
            return this[item].Unwrap(out value);
        }

        public Boolean TryGetValue(TValue item, [MaybeNullWhen(false)] out TValue value)
        {
            if (this[item].Unwrap(out KeyValuePair<TKey, TValue> result))
            {
                value = result.Value;
                return true;
            }

            value = default;
            return false;
        }

        public Boolean TryGetValue(TValue item, out KeyValuePair<TKey, TValue> value)
        {
            return this[item].Unwrap(out value);
        }

        public Maybe<TKey> Add(TValue item)
        {
            Boolean pool = true;
            if (!Pool.TryDequeue(out Key<TKey, TValue>? node))
            {
                node = Key.Create(Handler, Notify);
                pool = false;
            }

            if (!node.Set(item))
            {
                Return(node, false, pool);
                return default;
            }

            if (Storage.TryAdd(node, node))
            {
                return node.Key;
            }

            Return(node, true, pool);
            return default;
        }

        public KeyValuePair<TKey, TValue> GetOrAdd(TValue item)
        {
            if (TryGetValue(item, out KeyValuePair<TKey, TValue> result))
            {
                return result;
            }

            Boolean pool = true;
            if (!Pool.TryDequeue(out Key<TKey, TValue>? node))
            {
                node = Key.Create(Handler, Notify);
                pool = false;
            }

            if (!node.Set(item))
            {
                Return(node, false, pool);
                throw new ArgumentException($"Value '{item}' cannot be used to create a valid key.", nameof(item));
            }

            Key<TKey, TValue> @new = Storage.GetOrAdd(node, node);
            result = new KeyValuePair<TKey, TValue>(@new.Key, @new.Value);

            if (!ReferenceEquals(@new, node))
            {
                Return(node, true, pool);
            }

            return result;
        }

        public KeyValuePair<TKey, TValue> AddOrUpdate(TValue item)
        {
            Boolean pool = true;
            if (!Pool.TryDequeue(out Key<TKey, TValue>? node))
            {
                node = Key.Create(Handler, Notify);
                pool = false;
            }

            if (!node.Set(item))
            {
                Return(node, false, pool);
                throw new ArgumentException($"Can't set node value '{item}'.", nameof(item));
            }

            Key<TKey, TValue> @new = Storage.AddOrUpdate(node, node, (_, existing) => existing.With(item));
            KeyValuePair<TKey, TValue> result = new KeyValuePair<TKey, TValue>(@new.Key, @new.Value);

            if (!ReferenceEquals(@new, node))
            {
                Return(node, true, pool);
            }

            return result;
        }

        private Boolean Remove(Key<TKey, TValue> node, out Maybe<TKey> key, out Maybe<TValue> value)
        {
            if (!Storage.TryRemove(node, out node!))
            {
                key = default;
                value = default;
                return false;
            }

            key = node.SafeKey;
            value = node.SafeValue;
            Return(node);
            return true;
        }

        public Boolean Remove(TKey item)
        {
            return Remove(item, out Maybe<TValue> _);
        }

        public Boolean Remove(TKey item, [MaybeNullWhen(false)] out TValue value)
        {
            Remove(item, out Maybe<TValue> maybe);
            return maybe.Unwrap(out value);
        }

        public Boolean Remove(TKey item, out Maybe<TValue> value)
        {
            return Remove(Key.Search<TKey, TValue>(item), out _, out value);
        }

        public Boolean Remove(TValue item)
        {
            return Remove(item, out Maybe<TValue> _);
        }

        public Boolean Remove(TValue item, [MaybeNullWhen(false)] out TValue value)
        {
            Remove(item, out Maybe<TValue> maybe);
            return maybe.Unwrap(out value);
        }

        public Boolean Remove(TValue item, out Maybe<TValue> value)
        {
            return Remove(Key.Search(Handler, item), out _, out value);
        }

        public Int32 RemoveAll(TKey item)
        {
            return RemoveAll(item, out Int32 _);
        }

        public Int32 RemoveAll(TKey item, out Int32 alive)
        {
            Int32 count = alive = 0;
            Key<TKey, TValue> node = Key.Search<TKey, TValue>(item);

            lock (Storage)
            {
                while (Remove(node, out _, out Maybe<TValue> value))
                {
                    count++;

                    if (value.HasValue)
                    {
                        alive++;
                    }
                }
            }

            return count;
        }

        public Int32 RemoveAll(TKey item, out TValue[] values)
        {
            Key<TKey, TValue> node = Key.Search<TKey, TValue>(item);

            Int32 count = 0;
            List<TValue> result = new List<TValue>(4);

            lock (Storage)
            {
                while (Remove(node, out _, out Maybe<TValue> maybe))
                {
                    count++;

                    if (maybe.Unwrap(out TValue? value))
                    {
                        result.Add(value);
                    }
                }
            }

            values = result.ToArray();
            return count;
        }

        public void Clear()
        {
            KeyValuePair<Key<TKey, TValue>, Key<TKey, TValue>>[]? clear;

            lock (Storage)
            {
                clear = Storage.ToArray();
                Storage.Clear();
            }

            Parallel.ForEach(clear, pair => Return(pair.Key));
        }

        public IEnumerator<TValue> GetEnumerator()
        {
            foreach (Key<TKey, TValue> node in Storage.Keys)
            {
                if (node.Unwrap(out TValue? item))
                {
                    yield return item;
                }
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        private Key<TKey, TValue>? this[Key<TKey, TValue> node]
        {
            get
            {
                return Storage.TryGetValue(node, out node!) ? node : null;
            }
        }

        public Maybe<TValue> this[TKey item]
        {
            get
            {
                return this[Key.Search<TKey, TValue>(item)]?.SafeValue ?? default;
            }
        }

        public Maybe<KeyValuePair<TKey, TValue>> this[TValue item]
        {
            get
            {
                if (this[Key.Search(Handler, item)] is { } node)
                {
                    return new KeyValuePair<TKey, TValue>(node.Key, node.Value);
                }

                return default;
            }
        }

        private Boolean Return(Key<TKey, TValue> node, Boolean clear = true, Boolean force = false)
        {
            if (!force && AutoTrim)
            {
                return false;
            }

            if (clear)
            {
                node.Dispose();
            }

            Pool.Enqueue(node);
            return true;
        }

        private void Dispose(Key<TKey, TValue> node)
        {
            if (Storage.TryRemove(node, out node!))
            {
                Return(node);
            }
        }

        private sealed class Comparer : IEqualityComparer<Key<TKey, TValue>>
        {
            private IEqualityComparer<TValue> Inner { get; }

            public Comparer(IEqualityComparer<TValue>? inner)
            {
                Inner = inner ?? EqualityComparer<TValue>.Default;
            }

            public Int32 GetHashCode(Key<TKey, TValue>? other)
            {
                return other?.GetHashCode() ?? 0;
            }

            public Boolean Equals(Key<TKey, TValue>? x, Key<TKey, TValue>? y)
            {
                if (ReferenceEquals(x, y))
                {
                    return true;
                }

                if (x is null || y is null)
                {
                    return false;
                }

                return x.Equals(y, Inner);
            }
        }
    }
}