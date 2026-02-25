using System;
using System.Diagnostics.CodeAnalysis;
using System.Numerics;
using System.Runtime.CompilerServices;
using System.Text;
using NetExtender.Types.Spans;
using NetExtender.Utilities.Types;

namespace NetExtender.Types.Strings
{
    public sealed class StringPool<T> : StringPool
    {
        public new static StringPool<T> Default { get; } = new StringPool<T>();

        public StringPool()
        {
        }

        public StringPool(Int32 capacity)
            : base(capacity)
        {
        }
    }

    public class StringPool
    {
        public static StringPool Default
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                return StringPool<Object?>.Default;
            }
        }

        private const Int32 DefaultSize = 2048;
        private const Int32 MinimumSize = 32;
        private readonly FixedSizePriorityMap[] _maps;
        private readonly Int32 _count;

        public Int32 Size { get; }

        public StringPool()
            : this(DefaultSize)
        {
        }

        public StringPool(Int32 capacity)
        {
            if (capacity <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(capacity), capacity, null);
            }

            capacity = Math.Max(capacity, MinimumSize);

            static void FindFactors(Int32 size, Int32 factor, out UInt32 x, out UInt32 y)
            {
                Double a = Math.Sqrt((Double) size / factor);
                Double b = factor * a;

                x = BitOperations.RoundUpToPowerOf2((UInt32) a);
                y = BitOperations.RoundUpToPowerOf2((UInt32) b);
            }

            FindFactors(capacity, 2, out UInt32 x2, out UInt32 y2);
            FindFactors(capacity, 3, out UInt32 x3, out UInt32 y3);
            FindFactors(capacity, 4, out UInt32 x4, out UInt32 y4);

            UInt32 p2 = x2 * y2;
            UInt32 p3 = x3 * y3;
            UInt32 p4 = x4 * y4;

            if (p3 < p2)
            {
                p2 = p3;
                x2 = x3;
                y2 = y3;
            }

            if (p4 < p2)
            {
                p2 = p4;
                x2 = x4;
                y2 = y4;
            }

            Span<FixedSizePriorityMap> span = _maps = new FixedSizePriorityMap[x2];
            foreach (ref FixedSizePriorityMap map in span)
            {
                map = new FixedSizePriorityMap((Int32) y2);
            }

            _count = (Int32) x2;
            Size = (Int32) p2;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public String? Get(ReadOnlySpan<Char> span)
        {
            return Get(span, out String? value) ? value : null;
        }

        public Boolean Get(ReadOnlySpan<Char> span, [NotNullWhen(true)] out String? value)
        {
            if (span.IsEmpty)
            {
                value = String.Empty;
                return true;
            }

            Int32 code = GetHashCode(span);
            Int32 bucket = code & (_count - 1);

            ref FixedSizePriorityMap map = ref _maps.Reference(bucket);

            lock (map.SyncRoot)
            {
                return map.TryGet(span, code, out value);
            }
        }

        public Boolean Add(ReadOnlySpan<Char> value)
        {
            return Add(value.ToString());
        }

        public Boolean Add(String? value)
        {
            if (String.IsNullOrEmpty(value))
            {
                return false;
            }

            Int32 hash = GetHashCode(value.AsSpan());
            Int32 bucket = hash & (_count - 1);

            ref FixedSizePriorityMap map = ref _maps.Reference(bucket);

            lock (map.SyncRoot)
            {
                map.Add(value, hash);
            }

            return true;
        }

        public String GetOrAdd(ReadOnlySpan<Char> value)
        {
            if (value.IsEmpty)
            {
                return String.Empty;
            }

            Int32 hash = GetHashCode(value);
            Int32 bucket = hash & (_count - 1);

            ref FixedSizePriorityMap map = ref _maps.Reference(bucket);

            lock (map.SyncRoot)
            {
                return map.GetOrAdd(value, hash);
            }
        }

        [return: NotNullIfNotNull("value")]
        public String? GetOrAdd(String? value)
        {
            if (value is null)
            {
                return null;
            }

            if (value.Length <= 0)
            {
                return String.Empty;
            }

            Int32 hash = GetHashCode(value.AsSpan());
            Int32 bucket = hash & (_count - 1);

            ref FixedSizePriorityMap map = ref _maps.Reference(bucket);

            lock (map.SyncRoot)
            {
                return map.GetOrAdd(value, hash);
            }
        }

        public unsafe String GetOrAdd(ReadOnlySpan<Byte> span, Encoding? encoding)
        {
            if (span.IsEmpty)
            {
                return String.Empty;
            }

            encoding ??= Encoding.UTF8;
            Int32 length = encoding.GetMaxCharCount(span.Length);

            using SpanOwner<Char> buffer = SpanOwner<Char>.Allocate(length);

            fixed (Byte* source = span)
            fixed (Char* destination = &buffer.Reference)
            {
                length = encoding.GetChars(source, span.Length, destination, length);
                return GetOrAdd(new ReadOnlySpan<Char>(destination, length));
            }
        }

        public void Reset()
        {
            foreach (ref FixedSizePriorityMap map in _maps.AsSpan())
            {
                lock (map.SyncRoot)
                {
                    map.Reset();
                }
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static Int32 GetHashCode(ReadOnlySpan<Char> span)
        {
            HashCode code = new HashCode();
            code.AddBytes(span.AsBytes());
            return code.ToHashCode();
        }

        private struct FixedSizePriorityMap
        {
            private readonly Int32[] _buckets;
            private readonly MapEntry[] _map;
            private readonly HeapEntry[] _heap;
            private Int32 _count;
            private UInt32 _timestamp;
            private const Int32 EndOfList = -1;

            public readonly Object SyncRoot
            {
                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                get
                {
                    return _buckets;
                }
            }

            public FixedSizePriorityMap(Int32 capacity)
            {
                _buckets = new Int32[capacity];
                _map = new MapEntry[capacity];
                _heap = new HeapEntry[capacity];
                _count = 0;
                _timestamp = 0;
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public Boolean TryGet(ReadOnlySpan<Char> span, Int32 hash, [NotNullWhen(true)] out String? value)
            {
                ref String result = ref TryGet(span, hash);

                if (!Unsafe.IsNullRef(ref result))
                {
                    value = result;
                    return true;
                }

                value = null;
                return false;
            }

            [MethodImpl(MethodImplOptions.NoInlining)]
            private ref String TryGet(ReadOnlySpan<Char> span, Int32 hash)
            {
                ref MapEntry entries = ref _map.Reference();
                ref MapEntry entry = ref Unsafe.NullRef<MapEntry>();

                Int32 length = _buckets.Length;
                Int32 bucket = hash & (length - 1);

                for (Int32 i = _buckets.Reference(bucket) - 1; (UInt32) i < (UInt32) length; i = entry.NextIndex)
                {
                    entry = ref Unsafe.Add(ref entries, (IntPtr) (UInt32) i);

                    if (entry.HashCode != hash || !MemoryExtensions.SequenceEqual(entry.Value!.AsSpan(), span))
                    {
                        continue;
                    }

                    UpdateTimestamp(ref entry.HeapIndex);
                    return ref entry.Value!;
                }

                return ref Unsafe.NullRef<String>();
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public void Add(String value, Int32 hash)
            {
                ref String target = ref TryGet(value.AsSpan(), hash);

                if (Unsafe.IsNullRef(ref target))
                {
                    Insert(value, hash);
                    return;
                }

                target = value;
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public String GetOrAdd(ReadOnlySpan<Char> span, Int32 hash)
            {
                ref String result = ref TryGet(span, hash);

                if (!Unsafe.IsNullRef(ref result))
                {
                    return result;
                }

                String value = span.ToString();

                Insert(value, hash);
                return value;
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public String GetOrAdd(String value, Int32 hash)
            {
                ref String result = ref TryGet(value.AsSpan(), hash);

                if (!Unsafe.IsNullRef(ref result))
                {
                    return result;
                }

                Insert(value, hash);
                return value;
            }

            [MethodImpl(MethodImplOptions.NoInlining)]
            private String Insert(String value, Int32 hashcode)
            {
                unchecked
                {
                    ref Int32 buckets = ref _buckets.Reference();
                    ref MapEntry emap = ref _map.Reference();
                    ref HeapEntry eheap = ref _heap.Reference();
                    Int32 mindex, hindex;

                    if (_count == _map.Length)
                    {
                        mindex = eheap.MapIndex;
                        hindex = 0;

                        ref MapEntry removed = ref Unsafe.Add(ref emap, (IntPtr) (UInt32) mindex);
                        Remove(removed.HashCode, mindex);
                    }
                    else
                    {
                        mindex = _count;
                        hindex = _count;
                    }

                    Int32 index = hashcode & (_buckets.Length - 1);
                    ref Int32 target = ref Unsafe.Add(ref buckets, (IntPtr) (UInt32) index);
                    ref MapEntry map = ref Unsafe.Add(ref emap, (IntPtr) (UInt32) mindex);
                    ref HeapEntry heap = ref Unsafe.Add(ref eheap, (IntPtr) (UInt32) hindex);

                    map.HashCode = hashcode;
                    map.Value = value;
                    map.NextIndex = target - 1;
                    map.HeapIndex = hindex;

                    target = mindex + 1;
                    _count++;

                    heap.MapIndex = mindex;

                    UpdateTimestamp(ref map.HeapIndex);
                    return value;
                }
            }

            [MethodImpl(MethodImplOptions.NoInlining)]
            private void Remove(Int32 hash, Int32 mindex)
            {
                unchecked
                {
                    ref MapEntry emap = ref _map.Reference();
                    Int32 bucket = hash & (_buckets.Length - 1);
                    Int32 entry = _buckets.Reference(bucket) - 1;
                    Int32 last = EndOfList;

                    while (true)
                    {
                        ref MapEntry candidate = ref Unsafe.Add(ref emap, (IntPtr) (UInt32) entry);

                        if (entry == mindex)
                        {
                            if (last != EndOfList)
                            {
                                ref MapEntry current = ref Unsafe.Add(ref emap, (IntPtr) (UInt32) last);
                                current.NextIndex = candidate.NextIndex;
                            }
                            else
                            {
                                _buckets.Reference(bucket) = candidate.NextIndex + 1;
                            }

                            _count--;
                            return;
                        }

                        last = entry;
                        entry = candidate.NextIndex;
                    }
                }
            }

            [MethodImpl(MethodImplOptions.NoInlining)]
            [SuppressMessage("ReSharper", "CognitiveComplexity")]
            private void UpdateTimestamp(ref Int32 hindex)
            {
                unchecked
                {
                    Int32 current = hindex;
                    Int32 count = _count;
                    ref MapEntry map = ref _map.Reference();
                    ref HeapEntry heap = ref _heap.Reference();
                    ref HeapEntry root = ref Unsafe.Add(ref heap, (IntPtr) (UInt32) current);
                    UInt32 timestamp = _timestamp;

                    if (timestamp is UInt32.MaxValue)
                    {
                        goto fallback;
                    }

                    downheap:
                    root.Timestamp = _timestamp = timestamp + 1;

                    while (true)
                    {
                        ref HeapEntry minimum = ref root;
                        Int32 left = (current * 2) + 1;
                        Int32 right = (current * 2) + 2;
                        Int32 index = current;

                        if (left < count)
                        {
                            ref HeapEntry child = ref Unsafe.Add(ref heap, (IntPtr) (UInt32) left);

                            if (child.Timestamp < minimum.Timestamp)
                            {
                                minimum = ref child;
                                index = left;
                            }
                        }

                        if (right < count)
                        {
                            ref HeapEntry child = ref Unsafe.Add(ref heap, (IntPtr) (UInt32) right);

                            if (child.Timestamp < minimum.Timestamp)
                            {
                                minimum = ref child;
                                index = right;
                            }
                        }

                        if (Unsafe.AreSame(ref root, ref minimum))
                        {
                            hindex = index;
                            return;
                        }

                        Unsafe.Add(ref map, (IntPtr) (UInt32) root.MapIndex).HeapIndex = index;
                        Unsafe.Add(ref map, (IntPtr) (UInt32) minimum.MapIndex).HeapIndex = current;

                        current = index;

                        (root, minimum) = (minimum, root);
                        root = ref Unsafe.Add(ref heap, (IntPtr) (UInt32) current);
                    }

                    fallback:
                    UpdateAllTimestamps();

                    timestamp = (UInt32) (count - 1);
                    goto downheap;
                }
            }

            [MethodImpl(MethodImplOptions.NoInlining)]
            private readonly void UpdateAllTimestamps()
            {
                Int32 count = _count;
                ref HeapEntry heap = ref _heap.Reference();

                for (UInt32 i = 0; i < count; i++)
                {
                    Unsafe.Add(ref heap, (UIntPtr) i).Timestamp = i;
                }
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public void Reset()
            {
                _buckets.AsSpan().Clear();
                _map.AsSpan().Clear();
                _heap.AsSpan().Clear();
                _count = 0;
                _timestamp = 0;
            }

            private struct MapEntry
            {
                public Int32 HashCode;
                public String? Value;
                public Int32 NextIndex;
                public Int32 HeapIndex;
            }

            private struct HeapEntry
            {
                public UInt32 Timestamp;
                public Int32 MapIndex;
            }
        }
    }
}