using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Runtime.CompilerServices;
using NetExtender.Interfaces;
using NetExtender.Types.Dictionaries;
using NetExtender.Types.Lists;

namespace NetExtender.Types.Enumerators
{
    public readonly struct EnumeratorFiller<T, TEnumerator, TComparer> where TEnumerator : IReadOnlyCollection<T>, IEnumerator<T>, ICloneable<TEnumerator> where TComparer : IComparer<T>, IEqualityComparer<T>
    {
        private TEnumerator Enumerator { get; }
        private TComparer? Comparer { get; }

        public EnumeratorFiller(TEnumerator enumerator)
            : this(enumerator, default)
        {
        }

        public EnumeratorFiller(TEnumerator enumerator, TComparer? comparer)
        {
            Enumerator = enumerator ?? throw new ArgumentNullException(nameof(enumerator));
            Comparer = comparer;
        }

        [MethodImpl(MethodImplOptions.AggressiveOptimization)]
        public T[] ToArray()
        {
            using TEnumerator enumerator = Enumerator.Clone();
            Int32 count = enumerator.Count;

            if (count <= 0)
            {
                return Array.Empty<T>();
            }
            
            T[] array = new T[count];
            for (Int32 i = 0; i < array.Length && enumerator.MoveNext(); i++)
            {
                array[i] = enumerator.Current;
            }

            return array;
        }

        [MethodImpl(MethodImplOptions.AggressiveOptimization)]
        public ImmutableArray<T> ToImmutableArray()
        {
            using TEnumerator enumerator = Enumerator.Clone();
            Int32 count = enumerator.Count;

            if (count <= 0)
            {
                return ImmutableArray<T>.Empty;
            }

            ImmutableArray<T>.Builder builder = ImmutableArray.CreateBuilder<T>(count);
            for (Int32 i = 0; i < builder.Capacity && enumerator.MoveNext(); i++)
            {
                builder.Add(enumerator.Current);
            }

            return builder.MoveToImmutable();
        }
        
        [MethodImpl(MethodImplOptions.AggressiveOptimization)]
        public List<T> ToList()
        {
            using TEnumerator enumerator = Enumerator.Clone();
            Int32 count = enumerator.Count;

            if (count <= 0)
            {
                return new List<T>(0);
            }
            
            List<T> list = new List<T>(count);
            for (Int32 i = 0; i < count && enumerator.MoveNext(); i++)
            {
                list.Add(enumerator.Current);
            }

            return list;
        }
        
        [MethodImpl(MethodImplOptions.AggressiveOptimization)]
        public ImmutableList<T> ToImmutableList()
        {
            using TEnumerator enumerator = Enumerator.Clone();
            Int32 count = enumerator.Count;

            if (count <= 0)
            {
                return ImmutableList<T>.Empty;
            }

            ImmutableList<T>.Builder builder = ImmutableList.CreateBuilder<T>();
            for (Int32 i = 0; i < count && enumerator.MoveNext(); i++)
            {
                builder.Add(enumerator.Current);
            }

            return builder.ToImmutable();
        }

        [MethodImpl(MethodImplOptions.AggressiveOptimization)]
        public SortedList<T> ToSortedList()
        {
            return new SortedList<T>(ToList(), Comparer);
        }

        [MethodImpl(MethodImplOptions.AggressiveOptimization)]
        public HashSet<T> ToHashSet()
        {
            using TEnumerator enumerator = Enumerator.Clone();
            Int32 count = enumerator.Count;

            if (count <= 0)
            {
                return new HashSet<T>(0, Comparer);
            }

            HashSet<T> set = new HashSet<T>(count, Comparer);
            for (Int32 i = 0; i < count && enumerator.MoveNext(); i++)
            {
                set.Add(enumerator.Current);
            }

            return set;
        }
        
        [MethodImpl(MethodImplOptions.AggressiveOptimization)]
        public ImmutableHashSet<T> ToImmutableHashSet()
        {
            using TEnumerator enumerator = Enumerator.Clone();
            Int32 count = enumerator.Count;

            if (count <= 0)
            {
                return ImmutableHashSet<T>.Empty.WithComparer(Comparer);
            }
            
            ImmutableHashSet<T>.Builder builder = ImmutableHashSet.CreateBuilder(Comparer);
            for (Int32 i = 0; i < count && enumerator.MoveNext(); i++)
            {
                builder.Add(enumerator.Current);
            }

            return builder.ToImmutable();
        }
        
        [MethodImpl(MethodImplOptions.AggressiveOptimization)]
        public SortedSet<T> ToSortedSet()
        {
            using TEnumerator enumerator = Enumerator.Clone();
            Int32 count = enumerator.Count;

            if (count <= 0)
            {
                return new SortedSet<T>(Comparer);
            }
            
            SortedSet<T> set = new SortedSet<T>(Comparer);
            for (Int32 i = 0; i < count && enumerator.MoveNext(); i++)
            {
                set.Add(enumerator.Current);
            }

            return set;
        }
        
        [MethodImpl(MethodImplOptions.AggressiveOptimization)]
        public ImmutableSortedSet<T> ToImmutableSortedSet()
        {
            using TEnumerator enumerator = Enumerator.Clone();
            Int32 count = enumerator.Count;

            if (count <= 0)
            {
                return ImmutableSortedSet<T>.Empty.WithComparer(Comparer);
            }
            
            ImmutableSortedSet<T>.Builder builder = ImmutableSortedSet.CreateBuilder(Comparer);
            for (Int32 i = 0; i < count && enumerator.MoveNext(); i++)
            {
                builder.Add(enumerator.Current);
            }

            return builder.ToImmutable();
        }

        [MethodImpl(MethodImplOptions.AggressiveOptimization)]
        public void CopyTo(T[] array)
        {
            CopyTo(array, 0);
        }

        [MethodImpl(MethodImplOptions.AggressiveOptimization)]
        public void CopyTo(T[] array, Int32 index)
        {
            if (array is null)
            {
                throw new ArgumentNullException(nameof(array));
            }

            if (index < 0 || index > array.Length)
            {
                throw new ArgumentOutOfRangeException(nameof(index), index, null);
            }

            using TEnumerator enumerator = Enumerator.Clone();

            if (array.Length - index - 1 < enumerator.Count)
            {
                throw new ArgumentOutOfRangeException(nameof(array), array.Length, null);
            }

            while (enumerator.MoveNext())
            {
                array[index++] = enumerator.Current;
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveOptimization)]
        public Boolean TryCopyTo(Span<T> destination)
        {
            return TryCopyTo(destination, out _);
        }

        [MethodImpl(MethodImplOptions.AggressiveOptimization)]
        public Boolean TryCopyTo(Span<T> destination, out Int32 written)
        {
            using TEnumerator enumerator = Enumerator.Clone();
            
            written = 0;
            if (destination.Length < enumerator.Count)
            {
                return false;
            }

            while (enumerator.MoveNext())
            {
                destination[written++] = enumerator.Current;
            }

            return true;
        }
    }
}