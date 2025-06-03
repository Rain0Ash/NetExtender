using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using NetExtender.Types.Lists.Interfaces;
using NetExtender.Utilities.Types;

namespace NetExtender.Types.Lists
{
    public class SortedList<T> : ISortedList<T>, IReadOnlySortedList<T>, ISortedList
    {
        private List<T> Internal { get; }
        public IComparer<T> Comparer { get; }

        public Int32 Count
        {
            get
            {
                return Internal.Count;
            }
        }

        public Int32 Capacity
        {
            get
            {
                return Internal.Capacity;
            }
            set
            {
                Internal.Capacity = value;
            }
        }

        Boolean ICollection<T>.IsReadOnly
        {
            get
            {
                return ((ICollection<T>) Internal).IsReadOnly;
            }
        }

        Boolean IList.IsReadOnly
        {
            get
            {
                return ((IList) Internal).IsReadOnly;
            }
        }

        Boolean IList.IsFixedSize
        {
            get
            {
                return ((IList) Internal).IsFixedSize;
            }
        }

        Object ICollection.SyncRoot
        {
            get
            {
                return ((ICollection) Internal).SyncRoot;
            }
        }

        Boolean ICollection.IsSynchronized
        {
            get
            {
                return ((ICollection) Internal).IsSynchronized;
            }
        }

        public SortedList()
            : this(default(IComparer<T>))
        {
        }

        public SortedList(IComparer<T>? comparer)
        {
            Internal = new List<T>();
            Comparer = comparer ?? Comparer<T>.Default;
        }

        public SortedList(Comparison<T>? comparison)
            : this(comparison?.ToComparer())
        {
        }

        public SortedList(IEnumerable<T>? collection)
            : this(collection, default(IComparer<T>))
        {
        }

        public SortedList(IEnumerable<T>? collection, IComparer<T>? comparer)
        {
            Internal = collection is not null ? new List<T>(collection) : new List<T>();
            Comparer = comparer ?? Comparer<T>.Default;
            Internal.Sort(Comparer);
        }

        public SortedList(IEnumerable<T>? collection, Comparison<T>? comparison)
            : this(collection, comparison?.ToComparer())
        {
        }

        public SortedList(Int32 capacity)
            : this(capacity, default(IComparer<T>))
        {
        }

        public SortedList(Int32 capacity, IComparer<T>? comparer)
        {
            Internal = new List<T>(capacity);
            Comparer = comparer ?? Comparer<T>.Default;
        }

        public SortedList(Int32 capacity, Comparison<T>? comparison)
            : this(capacity, comparison?.ToComparer())
        {
        }

        protected internal SortedList(List<T> list)
            : this(list, null)
        {
        }

        protected internal SortedList(List<T> list, IComparer<T>? comparer)
        {
            Internal = list ?? throw new ArgumentNullException(nameof(list));
            Comparer = comparer ?? Comparer<T>.Default;
        }

        public Int32 EnsureCapacity(Int32 capacity)
        {
            return Internal.EnsureCapacity(capacity);
        }

        public void TrimExcess()
        {
            Internal.TrimExcess();
        }

        public Boolean Contains(T item)
        {
            return BinarySearch(item) >= 0;
        }

        Boolean IList.Contains(Object? item)
        {
            return Contains((T) item!);
        }

        public Boolean Exists(Predicate<T> match)
        {
            if (match is null)
            {
                throw new ArgumentNullException(nameof(match));
            }

            return Internal.Exists(match);
        }

        public Int32 IndexOf(T item)
        {
            Int32 index = BinarySearch(item);
            
            if (index < 0)
            {
                return -1;
            }

            while (--index >= 0 && EqualityComparer<T>.Default.Equals(Internal[index], item))
            {
            }

            return index + 1;
        }

        Int32 IList.IndexOf(Object? item)
        {
            return IndexOf((T) item!);
        }

        public Int32 IndexOf(T item, Int32 index)
        {
            if (index < 0)
            {
                return IndexOf(item);
            }
            
            index = BinarySearch(index, item);
            
            if (index < 0)
            {
                return -1;
            }

            while (--index >= 0 && EqualityComparer<T>.Default.Equals(Internal[index], item))
            {
            }

            return index + 1;
        }

        public Int32 IndexOf(T item, Int32 index, Int32 count)
        {
            index = BinarySearch(index, count, item);
            
            if (index < 0)
            {
                return -1;
            }

            while (--index >= 0 && EqualityComparer<T>.Default.Equals(Internal[index], item))
            {
            }

            return index + 1;
        }

        public Int32 LastIndexOf(T item)
        {
            Int32 index = BinarySearch(item);
            
            if (index < 0)
            {
                return -1;
            }

            while (++index < Internal.Count && EqualityComparer<T>.Default.Equals(Internal[index], item))
            {
            }

            return index - 1;
        }

        public Int32 LastIndexOf(T item, Int32 index)
        {
            if (index < 0)
            {
                return IndexOf(item);
            }
            
            index = BinarySearch(index, item);
            
            if (index < 0)
            {
                return -1;
            }

            while (++index < Internal.Count && EqualityComparer<T>.Default.Equals(Internal[index], item))
            {
            }

            return index - 1;
        }

        public Int32 LastIndexOf(T item, Int32 index, Int32 count)
        {
            index = BinarySearch(index, count, item);
            
            if (index < 0)
            {
                return -1;
            }

            while (++index < Internal.Count && EqualityComparer<T>.Default.Equals(Internal[index], item))
            {
            }

            return index - 1;
        }

        public T? Find(Predicate<T> match)
        {
            if (match is null)
            {
                throw new ArgumentNullException(nameof(match));
            }

            return Internal.Find(match);
        }

        public SortedList<T> FindAll(Predicate<T> match)
        {
            if (match is null)
            {
                throw new ArgumentNullException(nameof(match));
            }

            return new SortedList<T>(Internal.FindAll(match), Comparer);
        }

        public Int32 FindIndex(Predicate<T> match)
        {
            if (match is null)
            {
                throw new ArgumentNullException(nameof(match));
            }

            return Internal.FindIndex(match);
        }

        public Int32 FindIndex(Int32 index, Predicate<T> match)
        {
            if (match is null)
            {
                throw new ArgumentNullException(nameof(match));
            }

            return Internal.FindIndex(index, match);
        }

        public Int32 FindIndex(Int32 index, Int32 count, Predicate<T> match)
        {
            if (match is null)
            {
                throw new ArgumentNullException(nameof(match));
            }

            return Internal.FindIndex(index, count, match);
        }

        public T? FindLast(Predicate<T> match)
        {
            if (match is null)
            {
                throw new ArgumentNullException(nameof(match));
            }

            return Internal.FindLast(match);
        }

        public Int32 FindLastIndex(Predicate<T> match)
        {
            if (match is null)
            {
                throw new ArgumentNullException(nameof(match));
            }

            return Internal.FindLastIndex(match);
        }

        public Int32 FindLastIndex(Int32 index, Predicate<T> match)
        {
            if (match is null)
            {
                throw new ArgumentNullException(nameof(match));
            }

            return Internal.FindLastIndex(index, match);
        }

        public Int32 FindLastIndex(Int32 index, Int32 count, Predicate<T> match)
        {
            if (match is null)
            {
                throw new ArgumentNullException(nameof(match));
            }

            return Internal.FindLastIndex(index, count, match);
        }

        public Boolean TrueForAll(Predicate<T> match)
        {
            if (match is null)
            {
                throw new ArgumentNullException(nameof(match));
            }

            return Internal.TrueForAll(match);
        }

        public Int32 BinarySearch(T item)
        {
            return Internal.BinarySearch(item, Comparer);
        }

        public Int32 BinarySearch(Int32 index, T item)
        {
            if (index < 0 || index >= Internal.Count)
            {
                throw new ArgumentOutOfRangeException(nameof(index), index, null);
            }
            
            return Internal.BinarySearch(index, Internal.Count - index, item, Comparer);
        }

        public Int32 BinarySearch(Int32 index, Int32 count, T item)
        {
            return Internal.BinarySearch(index, count, item, Comparer);
        }

        public Int32 Add(T item)
        {
            Int32 index = BinarySearch(item);
            if (index < 0)
            {
                index = ~index;
            }

            Internal.Insert(index, item);
            return index;
        }

        void ICollection<T>.Add(T item)
        {
            Add(item);
        }

        Int32 IList.Add(Object? item)
        {
            return Add((T) item!);
        }

        // ReSharper disable once CognitiveComplexity
        public void AddRange(IEnumerable<T>? collection)
        {
            if (collection is null)
            {
                return;
            }

            if (Internal.Count <= 0)
            {
                Internal.AddRange(collection);
                Internal.Sort(Comparer);
                return;
            }

            List<T> insert = new List<T>(collection);
            
            if (insert.Count <= 0)
            {
                return;
            }

            insert.Sort(Comparer);
            Int32 length = Internal.Count;
            for (Int32 i = insert.Count - 1; i >= 0; i--)
            {
                T item = insert[i];
                Int32 index = BinarySearch(0, length, item);

                if (index >= 0)
                {
                    while (--index >= 0 && EqualityComparer<T>.Default.Equals(Internal[index], item))
                    {
                    }

                    index++;
                }
                else
                {
                    index = ~index;
                }

                if (index <= 0)
                {
                    Internal.InsertRange(0, insert.GetRange(0, i + 1));
                    break;
                }

                length = index - 1;
                item = Internal[length];
                Int32 end = i;
                
                while (--i >= 0 && Comparer.Compare(insert[i], item) > 0)
                {
                }

                i++;
                Internal.InsertRange(index, insert.GetRange(i, end - i + 1));
            }
        }

        void IList<T>.Insert(Int32 index, T item)
        {
            if (index < 0 || index >= Internal.Count)
            {
                throw new ArgumentOutOfRangeException(nameof(index), index, null);
            }
            
            Add(item);
        }

        void IList.Insert(Int32 index, Object? item)
        {
            ((IList<T>) this).Insert(index, (T) item!);
        }

        public Boolean Remove(T item)
        {
            Int32 index = BinarySearch(item);
            if (index < 0)
            {
                return false;
            }

            Internal.RemoveAt(index);
            return true;
        }

        void IList.Remove(Object? item)
        {
            Remove((T) item!);
        }

        public void RemoveAt(Int32 index)
        {
            Internal.RemoveAt(index);
        }

        public void RemoveRange(Int32 index, Int32 count)
        {
            Internal.RemoveRange(index, count);
        }

        public Int32 RemoveAll(Predicate<T> match)
        {
            if (match is null)
            {
                throw new ArgumentNullException(nameof(match));
            }

            return Internal.RemoveAll(match);
        }

        public void Clear()
        {
            Internal.Clear();
        }

        public SortedList<T> Sort()
        {
            return Sort(default(IComparer<T>));
        }

        public SortedList<T> Sort(IComparer<T>? comparer)
        {
            comparer ??= Comparer<T>.Default;
            List<T> sort = new List<T>(Internal);
            sort.Sort(comparer);
            return new SortedList<T>(sort, comparer);
        }

        public SortedList<T> Sort(Comparison<T>? comparison)
        {
            return Sort(comparison?.ToComparer());
        }

        public SortedList<T> Reverse()
        {
            List<T> reverse = new List<T>(Internal.Count);

            for (Int32 i = Internal.Count - 1; i >= 0; i--)
            {
                reverse.Add(Internal[i]);
            }

            return new SortedList<T>(reverse, Comparer.Reverse());
        }

        public SortedList<T> GetRange(Int32 index, Int32 count)
        {
            return new SortedList<T>(Internal.GetRange(index, count), Comparer);
        }

        // ReSharper disable once CognitiveComplexity
        public SortedList<T> WithinRange(T minimum, T maximum)
        {
            if (Comparer.Compare(minimum, maximum) > 0)
            {
                (minimum, maximum) = (maximum, minimum);
            }

            Int32 search;
            Int32 maxindex = Internal.BinarySearch(maximum, Comparer);
            if (maxindex >= 0)
            {
                search = maxindex + 1;
                while (++maxindex < Internal.Count && Comparer.Compare(maximum, Internal[maxindex]) == 0)
                {
                }

                --maxindex;
            }
            else
            {
                search = ~maxindex;
                if (search <= 0)
                {
                    return new SortedList<T>(0, Comparer);
                }

                maxindex = search - 1;
            }

            Int32 minindex = Internal.BinarySearch(0, search, minimum, Comparer);
            if (minindex >= 0)
            {
                while (--minindex >= 0 && Comparer.Compare(maximum, Internal[minindex]) == 0)
                {
                }

                ++minindex;
            }
            else
            {
                minindex = ~minindex;
                if (minindex > maxindex)
                {
                    return new SortedList<T>(0, Comparer);
                }
            }

            Int32 length = maxindex - minindex + 1;
            SortedList<T> result = new SortedList<T>(length, Comparer);
            Internal.CopyTo(minindex, result.Internal.Internal(), 0, length);
            return result;
        }

        public List<TOutput> ConvertAll<TOutput>(Converter<T, TOutput> converter)
        {
            if (converter is null)
            {
                throw new ArgumentNullException(nameof(converter));
            }
            
            return Internal.ConvertAll(converter);
        }

        public SortedList<TOutput> SortConvertAll<TOutput>(Converter<T, TOutput> converter)
        {
            return ConvertAll(converter, default(IComparer<TOutput>));
        }

        public SortedList<TOutput> ConvertAll<TOutput>(Converter<T, TOutput> converter, IComparer<TOutput>? comparer)
        {
            if (converter is null)
            {
                throw new ArgumentNullException(nameof(converter));
            }

            List<TOutput> convert = Internal.ConvertAll(converter);

            try
            {
                convert.Sort(comparer);
            }
            catch (InvalidOperationException)
            {
            }
            
            return new SortedList<TOutput>(convert, comparer);
        }

        public SortedList<TOutput> ConvertAll<TOutput>(Converter<T, TOutput> converter, Comparison<TOutput>? comparison)
        {
            if (converter is null)
            {
                throw new ArgumentNullException(nameof(converter));
            }

            return ConvertAll(converter, comparison?.ToComparer());
        }

        public void ForEach(Action<T>? action)
        {
            if (action is not null)
            {
                Internal.ForEach(action);
            }
        }

        public void CopyTo(T[] array)
        {
            Internal.CopyTo(array);
        }

        public void CopyTo(T[] array, Int32 index)
        {
            Internal.CopyTo(array, index);
        }

        void ICollection.CopyTo(Array array, Int32 index)
        {
            ((ICollection) Internal).CopyTo(array, index);
        }

        public T[] ToArray()
        {
            return Internal.ToArray();
        }

        public ReadOnlyCollection<T> AsReadOnly()
        {
            return Internal.AsReadOnly();
        }

        public List<T>.Enumerator GetEnumerator()
        {
            return Internal.GetEnumerator();
        }

        IEnumerator<T> IEnumerable<T>.GetEnumerator()
        {
            return GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public T this[Int32 index]
        {
            get
            {
                return Internal[index];
            }
            set
            {
                if (index < 0 || index >= Internal.Count)
                {
                    throw new ArgumentOutOfRangeException(nameof(index), index, null);
                }

                RemoveAt(index);
                Add(value);
            }
        }

        Object? IList.this[Int32 index]
        {
            get
            {
                return this[index];
            }
            set
            {
                this[index] = (T) value!;
            }
        }
    }
}