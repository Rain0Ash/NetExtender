// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Collections.Generic;

namespace NetExtender.Types.SpanCollections
{
    public static class SpanList
    {
        public const Int32 Size = 1;
    }
    
    //TODO: LastIndexOf, Insert, other list methods
    public ref struct SpanList<T>
    {
        public static implicit operator Span<T>(SpanList<T> value)
        {
            return value.Internal.Slice(0, value.Count);
        }
        
        public static implicit operator ReadOnlySpan<T>(SpanList<T> value)
        {
            return value.Internal.Slice(0, value.Count);
        }

        private Span<T> Internal { get; set; }
        public Int32 Count { get; set; }

        public readonly Int32 Capacity
        {
            get
            {
                return Internal.Length;
            }
        }
        
        private Int32 Version { get; set; }

        public SpanList(Span<T> destination)
        {
            Internal = destination;
            Count = 0;
            Version = 0;
        }
        
        public void EnsureCapacity(Span<T> destination)
        {
            if (destination.Length <= Count || !Internal.Slice(0, Count).TryCopyTo(destination))
            {
                throw new InvalidOperationException("Destination span must be larger than the current count.");
            }

            Internal = destination;
            Internal.Slice(Count).Clear();
            Version++;
        }
        
        public void TrimExcess()
        {
            if (Count >= Capacity - Capacity / 10)
            {
                return;
            }

            Internal = Internal.Slice(0, Count + Count / 10);
            Version++;
        }
        
        public readonly Boolean Contains(T item)
        {
            return IndexOf(item) != -1;
        }
        
        public readonly Int32 IndexOf(T item)
        {
            return IndexOf(item, 0, Count);
        }

        public readonly Int32 IndexOf(T item, Int32 index)
        {
            if (index < 0 || index > Count)
            {
                throw new ArgumentOutOfRangeException(nameof(index), index, null);
            }

            return IndexOf(item, index, Count - index);
        }
        
        public readonly Int32 IndexOf(T item, Int32 index, Int32 count)
        {
            if (index < 0 || index > Count)
            {
                throw new ArgumentOutOfRangeException(nameof(index), index, null);
            }

            if (count < 0 || index > Count - count)
            {
                throw new ArgumentOutOfRangeException(nameof(count), count, null);
            }

            for (Int32 i = index; i < count; i++)
            {
                if (EqualityComparer<T>.Default.Equals(Internal[i], item))
                {
                    return i;
                }
            }

            return -1;
        }
        
        public readonly Boolean Exists(Predicate<T> match)
        {
            return FindIndex(match) != -1;
        }
        
        public readonly Boolean TrueForAll(Predicate<T> match)
        {
            if (match is null)
            {
                throw new ArgumentNullException(nameof(match));
            }

            for (Int32 i = 0; i < Count; i++)
            {
                if (!match(Internal[i]))
                {
                    return false;
                }
            }

            return true;
        }
        
        public readonly T? Find(Predicate<T> match)
        {
            if (match is null)
            {
                throw new ArgumentNullException(nameof(match));
            }

            Int32 index = FindIndex(match);
            return index >= 0 ? Internal[index] : default;
        }
        
        public readonly Int32 FindIndex(Predicate<T> match)
        {
            return FindIndex(0, Count, match);
        }

        public readonly Int32 FindIndex(Int32 start, Predicate<T> match)
        {
            return FindIndex(start, Count - start, match);
        }

        public readonly Int32 FindIndex(Int32 start, Int32 count, Predicate<T> match)
        {
            if (match is null)
            {
                throw new ArgumentNullException(nameof(match));
            }

            if (start < 0 || start >= Count)
            {
                throw new ArgumentOutOfRangeException(nameof(start), start, null);
            }

            if (count < 0 || start > Count - count)
            {
                throw new ArgumentOutOfRangeException(nameof(count), count, null);
            }

            Int32 index = start + count;
            for (Int32 i = start; i < index; i++)
            {
                if (match(Internal[i]))
                {
                    return i;
                }
            }
            
            return -1;
        }
        
        public readonly T? FindLast(Predicate<T> match)
        {
            if (match is null)
            {
                throw new ArgumentNullException(nameof(match));
            }

            Int32 index = FindLastIndex(match);
            return index >= 0 ? Internal[index] : default;
        }
        
        public readonly Int32 FindLastIndex(Predicate<T> match)
        {
            return FindLastIndex(Count - 1, Count, match);
        }

        public readonly Int32 FindLastIndex(Int32 start, Predicate<T> match)
        {
            return FindLastIndex(start, start + 1, match);
        }

        public readonly Int32 FindLastIndex(Int32 start, Int32 count, Predicate<T> match)
        {
            if (match is null)
            {
                throw new ArgumentNullException(nameof(match));
            }

            if (start < 0 || start >= Count)
            {
                throw new ArgumentOutOfRangeException(nameof(start), start, null);
            }

            if (count < 0 || start - count + 1 < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(count), count, null);
            }

            Int32 index = start - count;
            for (Int32 i = start; i > index; i--)
            {
                if (match(Internal[i]))
                {
                    return i;
                }
            }
            
            return -1;
        }
        
        public readonly void ForEach(Action<T> action)
        {
            if (action is null)
            {
                throw new ArgumentNullException(nameof(action));
            }

            Int32 version = Version;
            for (Int32 i = 0; i < Count; i++)
            {
                if (version != Version)
                {
                    break;
                }

                action(Internal[i]);
            }

            if (version != Version)
            {
                throw new InvalidOperationException("Collection was modified");
            }
        }

        public void Add(T item)
        {
            if (Count >= Capacity)
            {
                throw new InvalidOperationException("Destination span is too small. Reinitialize with bigger span");
            }

            Internal[Count++] = item;
            Version++;
        }

        public void AddRange(Span<T> source)
        {
            AddRange((ReadOnlySpan<T>) source);
        }

        public void AddRange(ReadOnlySpan<T> source)
        {
            if (source.Length > Capacity - Count || !source.TryCopyTo(Internal.Slice(Count)))
            {
                throw new InvalidOperationException("Destination span is too small. Reinitialize with bigger span");
            }

            Count += source.Length;
            Version++;
        }

        public void AddRange(IEnumerable<T> source)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            Int32 count = Count;
            foreach (T item in source)
            {
                if (count >= Capacity)
                {
                    Internal.Slice(Count, count - Count).Clear();
                    throw new InvalidOperationException("Destination span is too small. Reinitialize with bigger span");
                }

                Internal[count++] = item;
            }
            
            Count = count;
            Version++;
        }
        
        public void Remove(T item)
        {
            Int32 index = IndexOf(item);
            if (index != -1)
            {
                RemoveAt(index);
            }
        }
        
        public void RemoveAt(Int32 index)
        {
            if (index < 0 || index >= Count)
            {
                throw new ArgumentOutOfRangeException(nameof(index), index, null);
            }

            Internal.Slice(0, index).CopyTo(Internal.Slice(index, Count - index));
            Count--;
            Version++;
        }

        public readonly void Reverse()
        {
            Internal.Slice(0, Count).Reverse();
        }
        
        public readonly void Sort()
        {
            Sort(default(IComparer<T>));
        }

        public readonly void Sort<TComparer>(TComparer? comparer) where TComparer : IComparer<T>?
        {
            Sort(0, Count, comparer);
        }

        public readonly void Sort<TComparer>(Int32 index, Int32 count, TComparer? comparer) where TComparer : IComparer<T>?
        {
            if (index < 0 || index >= Count)
            {
                throw new ArgumentOutOfRangeException(nameof(index), index, null);
            }

            if (count < 0 || index + count > Count)
            {
                throw new ArgumentOutOfRangeException(nameof(count), count, null);
            }

            Internal.Slice(index, count).Sort(comparer);
        }

        public void Clear()
        {
            Internal.Clear();
            Count = 0;
            Version++;
        }
        
        public readonly void CopyTo(Span<T> destination)
        {
            Internal.Slice(0, Count).CopyTo(destination);
        }
        
        public readonly Boolean TryCopyTo(Span<T> destination)
        {
            return Internal.Slice(0, Count).TryCopyTo(destination);
        }
        
        public readonly Enumerator GetEnumerator()
        {
            return new Enumerator(this);
        }
        
        public T this[Int32 index]
        {
            readonly get
            {
                if (index < 0 || index >= Count)
                {
                    throw new ArgumentOutOfRangeException(nameof(index), index, null);
                }

                return Internal[index];
            }
            set
            {
                if (index < 0 || index >= Count)
                {
                    throw new ArgumentOutOfRangeException(nameof(index), index, null);
                }

                Internal[index] = value;
                Version++;
            }
        }
        
        public ref struct Enumerator
        {
            private SpanList<T> Internal { get; }
            public T Current { get; private set; }
            private Int32 Version { get; }
            private Int32 Index { get; set; }

            internal Enumerator(SpanList<T> source)
            {
                Internal = source;
                Current = default!;
                Version = source.Version;
                Index = 0;
            }

            public Boolean MoveNext()
            {
                if (Version != Internal.Version)
                {
                    throw new InvalidOperationException("Collection was modified");
                }

                if (Index < Internal.Count)
                {
                    Current = Internal[Index];
                    Index++;
                    return true;
                }

                Index = Internal.Count;
                Current = default!;
                return false;
            }

            public void Reset()
            {
                if (Version != Internal.Version)
                {
                    throw new InvalidOperationException("Collection was modified");
                }

                Index = 0;
                Current = default!;
            }
        }
    }
}