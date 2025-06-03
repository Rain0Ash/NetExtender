using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using NetExtender.Utilities.Numerics;

namespace NetExtender.Types.Dictionaries
{
    [DebuggerDisplay("Length = {Length}")]
    public struct Glossary<TValue> : IStruct<Glossary<TValue>> where TValue : struct
    {
        private const Int32 FreeListStart = -3;

        private Int32[]? _buckets;
        private Entry[]? _entries;
        private Int32 _length;
        private Int32 _free;
        private Int32 _list;

        public readonly Int32 Length
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                return _length - _free;
            }
        }

        private readonly UInt32 BucketLength
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                return unchecked((UInt32) (_buckets?.LongLength ?? 0));
            }
        }

        public readonly Boolean IsEmpty
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                return _length <= 0;
            }
        }

        public Glossary(Int32 capacity)
        {
            if (capacity < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(capacity), capacity, "Capacity must be greater than or equal to zero.");
            }
            
            _buckets = new Int32[capacity];
            _entries = new Entry[capacity];
            
            _length = 0;
            _free = 0;
            _list = 0;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private readonly ref Int32 GetBucket(Int32 hash)
        {
            return ref GetBucket(unchecked((UInt32) hash));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private readonly ref Int32 GetBucket(UInt32 hash)
        {
            return ref _buckets![unchecked(hash % (UInt32) _buckets.Length)];
        }

        public void TrimExcess()
        {
            if (IsEmpty)
            {
                _buckets = Array.Empty<Int32>();
                _entries = Array.Empty<Entry>();
                _free = 0;
                _list = -1;
                return;
            }
            
            Entry[] old = _entries!;
            Int32 size = _length;

            Int32 count = 0;
            Int32[] buckets = new Int32[size];
            Entry[] entries = new Entry[size];

            for (Int32 i = 0; i < size; i++)
            {
                if (old[i].Next < -1)
                {
                    continue;
                }

                ref Entry entry = ref entries[count];
                entry = old[i];
                UInt32 bucket = unchecked((UInt32) entry.Key % (UInt32) size);

                entry.Next = buckets[bucket] - 1;
                buckets[bucket] = count + 1;
                count++;
            }

            _buckets = buckets;
            _entries = entries;
            _free = 0;
            _list = -1;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private void EnsureCapacity()
        {
            Resize(Glossary.GetPrime(_length == 0 ? 4 : _length << 1));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void EnsureCapacity(Int32 capacity)
        {
            if (capacity < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(capacity), capacity, "Capacity must be greater than or equal to zero.");
            }

            if (capacity <= _length)
            {
                return;
            }
            
            Resize(capacity);
        }

        private void Resize(Int32 size)
        {
            Int32 length = _length;

            Int32[] buckets = new Int32[size];
            Entry[] entries = new Entry[size];

            if (_entries is not null)
            {
                Array.Copy(_entries, entries, length);
            }
            
            for (Int32 i = 0; i < length; i++)
            {
                ref Entry entry = ref entries[i];

                if (entry.Next < -1)
                {
                    continue;
                }
                
                ref Int32 bucket = ref buckets[unchecked((UInt32) entry.Key % (UInt32) buckets.Length)];
                entry.Next = bucket - 1;
                bucket = i + 1;
            }

            _buckets = buckets;
            _entries = entries;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public readonly Boolean ContainsKey(Int32 key)
        {
            if (IsEmpty)
            {
                return false;
            }

            Entry[] entries = _entries!;
            Int32 i = _buckets![unchecked((UInt32) key % (UInt32) _buckets.Length)] - 1;

            while (unchecked((UInt32) i < (UInt32) entries.Length))
            {
                ref readonly Entry entry = ref entries[i];
                if (entry.Key == key)
                {
                    return true;
                }

                i = entry.Next;
            }

            return false;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public readonly Boolean ContainsValue(in TValue value)
        {
            return ContainsValue(value, null);
        }

        public readonly Boolean ContainsValue(in TValue value, IEqualityComparer<TValue>? comparer)
        {
            comparer ??= EqualityComparer<TValue>.Default;
            foreach (Entry entry in _entries.AsSpan(0, _length))
            {
                if (entry.Next >= -1 && comparer.Equals(entry.Value, value))
                {
                    return true;
                }
            }

            return false;
        }

        public readonly ref TValue GetValue(Int32 key)
        {
            if (IsEmpty)
            {
                throw new KeyNotFoundException($"The given key '{key}' was not present in the dictionary.");
            }

            Entry[] entries = _entries!;
            Int32 i = _buckets![unchecked((UInt32) key % (UInt32) _buckets.Length)] - 1;

            while (unchecked((UInt32) i < (UInt32) entries.Length))
            {
                ref Entry entry = ref entries[i];
                if (entry.Key == key)
                {
                    return ref entry.Value;
                }

                i = entry.Next;
            }

            throw new KeyNotFoundException($"The given key '{key}' was not present in the dictionary.");
        }

        public ref TValue GetOrAddValue(Int32 key)
        {
            if (IsEmpty)
            {
                return ref Add(key);
            }

            Entry[] entries = _entries!;
            Int32 i = _buckets![unchecked((UInt32) key % (UInt32) _buckets.Length)] - 1;

            while (unchecked((UInt32) i < (UInt32) entries.Length))
            {
                ref Entry entry = ref entries[i];
                if (entry.Key == key)
                {
                    return ref entry.Value;
                }

                i = entry.Next;
            }

            return ref Add(key);
        }

        public readonly Boolean TryGetValue(Int32 key, out TValue value)
        {
            if (IsEmpty)
            {
                value = default;
                return false;
            }

            Entry[] entries = _entries!;
            Int32 i = _buckets![unchecked((UInt32) key % (UInt32) _buckets.Length)] - 1;
            
            while (unchecked((UInt32) i < (UInt32) entries.Length))
            {
                ref readonly Entry entry = ref entries[i];
                if (entry.Key == key)
                {
                    value = entry.Value;
                    return true;
                }

                i = entry.Next;
            }

            value = default;
            return false;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ref TValue Add(Int32 key)
        {
            if (_buckets is null || _buckets.Length <= 0)
            {
                EnsureCapacity();
            }

            UInt32 hash = unchecked((UInt32) key);
            ref Int32 bucket = ref GetBucket(hash);
            Entry[] entries = _entries!;
            Int32 i = bucket - 1;

            while (unchecked((UInt32) i < (UInt32) entries.Length))
            {
                ref readonly Entry exist = ref entries[i];
                if (exist.Key == key)
                {
                    throw new ArgumentException($"An item with the same key has already been added. Key: '{key}'", nameof(key));
                }

                i = exist.Next;
            }

            Int32 index;
            if (_free > 0)
            {
                index = _list;
                _list = FreeListStart - entries[index].Next;
                _free--;
            }
            else
            {
                index = _length;
                if (index == entries.Length)
                {
                    EnsureCapacity();
                    bucket = ref GetBucket(hash);
                    entries = _entries!;
                }

                _length++;
            }

            ref Entry entry = ref entries[index];
            entry.Key = key;
            entry.Next = bucket - 1;

            bucket = index + 1;
            return ref entry.Value;
        }

        public void Add(Int32 key, in TValue value)
        {
            ref TValue entry = ref Add(key);
            entry = value;
        }

        public Boolean Remove(Int32 key, Boolean clear = false)
        {
            if (IsEmpty)
            {
                return false;
            }

            ref Int32 bucket = ref GetBucket(key);
            Entry[] entries = _entries!;
            Int32 i = bucket - 1;
            Int32 last = -1;

            while (i >= 0)
            {
                ref Entry entry = ref entries[i];
                if (entry.Key != key)
                {
                    last = i;
                    i = entry.Next;
                    continue;
                }

                if (clear)
                {
                    entry.Value = default;
                }

                if (last < 0)
                {
                    bucket = entry.Next + 1;
                }
                else
                {
                    entries[last].Next = entry.Next;
                }

                entry.Next = FreeListStart - _list;

                _free++;
                _list = i;
                return true;
            }

            return false;
        }

        public Boolean Remove(Int32 key, out TValue value)
        {
            if (IsEmpty)
            {
                value = default;
                return false;
            }

            ref Int32 bucket = ref GetBucket(key);
            Entry[] entries = _entries!;
            Int32 i = bucket - 1;
            Int32 last = -1;

            while (i >= 0)
            {
                ref Entry entry = ref entries[i];
                if (entry.Key == key)
                {
                    value = entry.Value;
                    entry.Value = default;

                    if (last < 0)
                    {
                        bucket = entry.Next + 1;
                    }
                    else
                    {
                        entries[last].Next = entry.Next;
                    }

                    entry.Next = FreeListStart - _list;

                    _free++;
                    _list = i;
                    return true;
                }

                last = i;
                i = entry.Next;
            }

            value = default;
            return false;
        }

        public void Clear()
        {
            if (IsEmpty)
            {
                return;
            }

            if (_buckets is not null)
            {
                Array.Clear(_buckets, 0, _buckets.Length);
            }

            if (_entries is not null)
            {
                Array.Clear(_entries, 0, _entries.Length);
            }

            _free = 0;
            _list = 0;
            _length = 0;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public readonly Enumerator GetEnumerator()
        {
            return new Enumerator(_entries, _length);
        }
        
        public TValue this[Int32 key]
        {
            readonly get
            {
                return GetValue(key);
            }
            set
            {
                GetOrAddValue(key) = value;
            }
        }

        public struct Entry
        {
            public static implicit operator KeyValuePair<Int32, TValue>(Entry value)
            {
                return new KeyValuePair<Int32, TValue>(value.Key, value.Value);
            }
            
            internal Int32 Next;
            internal Int32 Key;
            internal TValue Value;

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public readonly void Deconstruct(out Int32 key, out TValue value)
            {
                key = Key;
                value = Value;
            }
        }
        
        public readonly struct KeyCollection
        {
        }
        
        public readonly struct ValueCollection
        {
        }

        public ref struct Enumerator
        {
            private Int32 _index;
            private readonly Int32 _length;
            private readonly Entry[] _entries;

            public readonly ref Entry Current
            {
                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                get
                {
                    return ref _entries[_index];
                }
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            internal Enumerator(Entry[]? entries, Int32 length)
            {
                _entries = entries ?? Array.Empty<Entry>();
                _length = length;
                _index = -1;
            }

            public Boolean MoveNext()
            {
                Int32 index = _index + 1;
                Int32 length = _length;
                
                while (unchecked((UInt32) index < (UInt32) length))
                {
                    ref readonly Entry entry = ref _entries[index];
                    if (entry.Next >= -1)
                    {
                        _index = index;
                        return true;
                    }

                    index++;
                }

                _index = _length;
                return false;
            }

            public void Reset()
            {
                _index = -1;
            }
        }
    }

    internal static class Glossary
    {
        public static Int32 GetPrime(Int32 min)
        {
            foreach (Int32 prime in Primes)
            {
                if (prime >= min)
                {
                    return prime;
                }
            }

            for (Int32 candidate = min | 1; candidate < Int32.MaxValue; candidate += 2)
            {
                if (IsPrime(candidate) && (candidate - 1) % 101 != 0)
                {
                    return candidate;
                }
            }

            return min;
        }

        private static Boolean IsPrime(Int32 candidate)
        {
            if ((candidate & 1) == 0)
            {
                return candidate == 2;
            }
            
            Int32 num = (Int32) Math.Sqrt(candidate);
            for (Int32 index = 3; index <= num; index += 2)
            {
                if (candidate % index == 0)
                {
                    return false;
                }
            }

            return true;
        }

        private static readonly PrimeCollection Primes = new PrimeCollection
        (
            new []
            {
                3,
                7,
                11,
                17,
                23,
                29,
                37,
                47,
                59,
                71,
                89,
                107,
                131,
                163,
                197,
                239,
                293,
                353,
                431,
                521,
                631,
                761,
                919,
                1103,
                1327,
                1597,
                1931,
                2333,
                2801,
                3371,
                4049,
                4861,
                5839,
                7013,
                8419,
                10103,
                12143,
                14591,
                17519,
                21023,
                25229,
                30293,
                36353,
                43627,
                52361,
                62851,
                75431,
                90523,
                108631,
                130363,
                156437,
                187751,
                225307,
                270371,
                324449,
                389357,
                467237,
                560689,
                672827,
                807403,
                968897,
                1162687,
                1395263,
                1674319,
                2009191,
                2411033,
                2893249,
                3471899,
                4166287,
                4999559,
                5999471,
                7199369
            }
        );
    }
}