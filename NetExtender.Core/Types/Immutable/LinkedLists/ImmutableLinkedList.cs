// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace NetExtender.Types.Immutable.LinkedLists
{
    /// <summary>
    /// Static factory methods and extensions for <see cref="ImmutableLinkedList{T}"/>
    /// </summary>
    public static class ImmutableLinkedList
    {
        /// <summary>
        /// Creates an <see cref="ImmutableLinkedList{T}"/> with a single element <paramref name="value"/>.
        /// </summary>
        public static ImmutableLinkedList<T> Create<T>(T value)
        {
            return ImmutableLinkedList<T>.Create(value);
        }

        /// <summary>
        /// Creates an <see cref="ImmutableLinkedList{T}"/> with the given <paramref name="values"/>.
        /// </summary>
        public static ImmutableLinkedList<T> CreateRange<T>(IEnumerable<T> values)
        {
            return ImmutableLinkedList<T>.CreateRange(values);
        }

        /// <summary>
        /// Same as <see cref="CreateRange{T}(IEnumerable{T})"/>, but exposed as an extension method
        /// </summary>
        public static ImmutableLinkedList<T> ToImmutableLinkedList<T>(this IEnumerable<T> source)
        {
            return CreateRange(source);
        }
    }

    /// <summary>
    /// An immutable linked list data structure
    /// </summary>
    public readonly struct ImmutableLinkedList<T> : IReadOnlyCollection<T>, IEquatable<ImmutableLinkedList<T>>, ICollection<T>
    {
        /// <summary>
        /// The empty list
        /// </summary>
        public static ImmutableLinkedList<T> Empty
        {
            get
            {
                return default;
            }
        }

        private readonly Node? _head;
        
        /// <summary>
        /// The length of the list
        /// </summary>
        public Int32 Count { get; }

        /// <summary>
        /// The first element in the list. Throws <see cref="InvalidOperationException"/> if the list is empty.
        /// </summary>
        public T Head
        {
            get
            {
                if (Count == 0)
                {
                    ThrowEmpty();
                }

                return _head!.Value;
            }
        }

        /// <summary>
        /// A list consisting of all elements except the first. Throws <see cref="InvalidOperationException"/> if the list is empty.
        /// 
        /// This property is O(1) and does not require any copying.
        /// </summary>
        public ImmutableLinkedList<T> Tail
        {
            get
            {
                if (Count == 0)
                {
                    ThrowEmpty();
                }

                return new ImmutableLinkedList<T>(_head!.Next, Count - 1);
            }
        }

        private ImmutableLinkedList(Node? head, Int32 count)
        {
            _head = head;
            Count = count;
        }

        internal static ImmutableLinkedList<T> Create(T value)
        {
            return new ImmutableLinkedList<T>(new Node(value), 1);
        }

        internal static ImmutableLinkedList<T> CreateRange(IEnumerable<T> values)
        {
            // ReSharper disable once ConvertIfStatementToSwitchStatement
            if (values is null)
            {
                throw new ArgumentNullException(nameof(values));
            }

            if (values is ImmutableLinkedList<T> list)
            {
                return list;
            }

            using IEnumerator<T> enumerator = values.GetEnumerator();
            if (!enumerator.MoveNext())
            {
                return Empty;
            }

            Node head = new Node(enumerator.Current);
            Node last = head;
            Int32 count = 1;
            while (enumerator.MoveNext())
            {
                last = last.Next = new Node(enumerator.Current);
                ++count;
            }

            return new ImmutableLinkedList<T>(head, count);
        }

        /// <summary>
        /// Splits the list into head and tail in a single operation. Throws <see cref="InvalidOperationException"/> if the
        /// list is empty. This method can be invoked implicitly using tuple deconstruction syntax:
        /// <code>
        ///     var (head, tail) = list;
        /// </code>
        /// 
        /// This method runs in O(1) and does not require any copying.
        /// </summary>
        public void Deconstruct(out T head, out ImmutableLinkedList<T> tail)
        {
            if (Count == 0)
            {
                ThrowEmpty();
            }

            head = _head!.Value;
            tail = new ImmutableLinkedList<T>(_head.Next, Count - 1);
        }

        /// <summary>
        /// Equivalent to <see cref="Deconstruct(out T, out ImmutableLinkedList{T})"/>, but 
        /// returns false rather than throwing if the list is empty
        /// </summary>
        public Boolean TryDeconstruct(out T head, out ImmutableLinkedList<T> tail)
        {
            if (Count != 0)
            {
                head = _head!.Value;
                tail = new ImmutableLinkedList<T>(_head.Next, Count - 1);
                return true;
            }

            head = default!;
            tail = default;
            return false;
        }

        /// <summary>
        /// Prevents boxing when using lists with <see cref="EqualityComparer{T}.Default"/>
        /// </summary>
        Boolean IEquatable<ImmutableLinkedList<T>>.Equals(ImmutableLinkedList<T> other)
        {
            return _head == other._head;
        }

        private static void ThrowEmpty()
        {
            throw new InvalidOperationException("the list is empty");
        }

        private static void CopyNonEmptyRange(Node head, Node? last, out Node newHead, out Node newLast)
        {
            Node newCurrent = newHead = new Node(head.Value);
            for (Node? current = head.Next; current != last; current = current.Next)
            {
                newCurrent = newCurrent.Next = new Node(current!.Value);
            }

            newLast = newCurrent;
        }

        /// <summary>
        /// Returns an <see cref="IEnumerator{T}"/> that iterates through the list. 
        /// 
        /// This method returns a value type enumerator (similar to <see cref="List{T}.Enumerator"/>). This improves
        /// the efficiency of foreach loops but means that the <see cref="Enumerator"/> has value type semantics with
        /// respect to copying. For example, copying an <see cref="Enumerator"/> value to another value captures a
        /// "snapshot" of the enumerator state; the original variable can continue to be enumerated over while the
        /// snapshot remains at the original state (and can be advanced from there)
        /// </summary>
        public Enumerator GetEnumerator()
        {
            return new Enumerator(_head);
        }

        IEnumerator<T> IEnumerable<T>.GetEnumerator()
        {
            return GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        /// <summary>
        /// An enumerator over <see cref="ImmutableLinkedList{T}"/>. See <see cref="ImmutableLinkedList{T}.GetEnumerator"/>
        /// for more details
        /// </summary>
        public struct Enumerator : IEnumerator<T>
        {
            private Node? _next;

            internal Enumerator(Node? first)
            {
                _next = first;
                Current = default!;
            }

            /// <summary>
            /// The current value. Behavior is undefined before enumeration begins
            /// and after it ends
            /// </summary>
            public T Current { get; private set; }

            Object? IEnumerator.Current
            {
                get
                {
                    return Current;
                }
            }

            /// <summary>
            /// Cleans up any resources held by the enumerator (currently none)
            /// </summary>
            public void Dispose()
            {
            }

            /// <summary>
            /// Advances the enumerator, returning false if the end of the list has been reached
            /// </summary>
            public Boolean MoveNext()
            {
                if (_next is null)
                {
                    Current = default!;
                    return false;
                }

                Current = _next.Value;
                _next = _next.Next;
                return true;
            }

            void IEnumerator.Reset()
            {
                throw new NotSupportedException();
            }
        }


        Boolean ICollection<T>.IsReadOnly
        {
            get
            {
                return true;
            }
        }

        void ICollection<T>.Add(T item)
        {
            throw ReadOnly();
        }

        void ICollection<T>.Clear()
        {
            throw ReadOnly();
        }

        /// <summary>
        /// Copies the elements of the list to <paramref name="array"/>, starting at <paramref name="arrayIndex"/>
        /// </summary>
        public void CopyTo(T[] array, Int32 arrayIndex)
        {
            if (array is null)
            {
                throw new ArgumentNullException(nameof(array));
            }

            if (arrayIndex < 0 || arrayIndex > array.Length)
            {
                throw new ArgumentOutOfRangeException(nameof(arrayIndex), arrayIndex, @"must be non-negative and less than or equal to the array length");
            }

            if (arrayIndex + Count > array.Length)
            {
                throw new ArgumentException(@"destination array was not long enough", nameof(array));
            }

            Int32 currentIndex = arrayIndex;
            for (Node? current = _head; current is not null; current = current.Next)
            {
                array[currentIndex++] = current.Value;
            }
        }

        Boolean ICollection<T>.Remove(T item)
        {
            throw ReadOnly();
        }

        private static NotSupportedException ReadOnly()
        {
            return new NotSupportedException("the collection is read-only");
        }

        internal sealed class Node
        {
            internal Node? Next;
            internal readonly T Value;

            public Node(T value)
            {
                Value = value;
            }
        }

        /// <summary>
        /// Returns true if <paramref name="value"/> is an element of the list.
        /// 
        /// This method is O(N)
        /// </summary>
        public Boolean Contains(T value)
        {
            EqualityComparer<T> comparer = EqualityComparer<T>.Default;
            for (Node? current = _head; current is not null; current = current.Next)
            {
                if (comparer.Equals(current.Value, value))
                {
                    return true;
                }
            }

            return false;
        }

        /// <summary>
        /// Same as <see cref="Enumerable.SequenceEqual{TSource}(IEnumerable{TSource}, IEnumerable{TSource}, IEqualityComparer{TSource})"/>,
        /// but optimized for comparing two instances of <see cref="ImmutableLinkedList{T}"/>.
        /// 
        /// This method is O(n).
        /// </summary>
        public Boolean SequenceEqual(ImmutableLinkedList<T> that, IEqualityComparer<T>? comparer = null)
        {
            if (Count != that.Count)
            {
                return false;
            }

            IEqualityComparer<T> comparerToUse = comparer ?? EqualityComparer<T>.Default;

            Node? thisCurrent = _head;
            Node? thatCurrent = that._head;
            while (true)
            {
                if (thisCurrent == thatCurrent)
                {
                    return true;
                }

                if (thisCurrent is null || !comparerToUse.Equals(thisCurrent.Value, thatCurrent!.Value))
                {
                    return false;
                }

                thisCurrent = thisCurrent.Next;
                thatCurrent = thatCurrent.Next;
            }
        }
        
        /// <summary>
        /// Returns a new list with <paramref name="value"/> prepended.
        /// 
        /// This method is O(1) and requires no copying.
        /// </summary>
        public ImmutableLinkedList<T> Prepend(T value)
        {
            return new ImmutableLinkedList<T>(new Node(value) {Next = _head}, Count + 1);
        }

        /// <summary>
        /// Returns a new list with <paramref name="values"/> prepended such that the first element of <paramref name="values"/> values
        /// becomes the first element of the returned list.
        /// 
        /// This method is O(k) where k is the number of elements in <paramref name="values"/> and requires no copying.
        /// </summary>
        public ImmutableLinkedList<T> PrependRange(IEnumerable<T> values)
        {
            if (Count == 0)
            {
                return CreateRange(values);
            }

            // ReSharper disable once ConvertIfStatementToSwitchStatement
            if (values is null)
            {
                throw new ArgumentNullException(nameof(values));
            }

            if (values is ImmutableLinkedList<T> list)
            {
                return PrependRange(list);
            }

            using IEnumerator<T> enumerator = values.GetEnumerator();
            if (!enumerator.MoveNext())
            {
                return this;
            }

            Node head = new Node(enumerator.Current);
            Node last = head;
            Int32 count = 1;
            while (enumerator.MoveNext())
            {
                last = last.Next = new Node(enumerator.Current);
                ++count;
            }

            last.Next = _head;
            return new ImmutableLinkedList<T>(head, count + Count);
        }

        /// <summary>
        /// Same as <see cref="PrependRange(IEnumerable{T})"/>, but optimized for prepending another
        /// instance of <see cref="ImmutableLinkedList{T}"/>
        /// </summary>
        public ImmutableLinkedList<T> PrependRange(ImmutableLinkedList<T> list)
        {
            if (list.Count == 0)
            {
                return this;
            }

            if (Count == 0)
            {
                return list;
            }

            CopyNonEmptyRange(list._head!, null, out Node newHead, out Node newLast);
            newLast.Next = _head;
            return new ImmutableLinkedList<T>(newHead, Count + list.Count);
        }
        
        /// <summary>
        /// Returns a new list with <paramref name="value"/> appended.
        /// 
        /// This method is O(n) and requires copying the entire list.
        /// </summary>
        public ImmutableLinkedList<T> Append(T value)
        {
            if (Count == 0)
            {
                return Create(value);
            }

            CopyNonEmptyRange(_head!, null, out Node newHead, out Node newLast);
            newLast.Next = new Node(value);
            return new ImmutableLinkedList<T>(newHead, Count + 1);
        }

        /// <summary>
        /// Returns a new list with <paramref name="values"/> appended.
        /// 
        /// This method is O(n + k) where k is the number of elements in <paramref name="values"/>. It requires
        /// copying the entire list.
        /// </summary>
        public ImmutableLinkedList<T> AppendRange(IEnumerable<T> values)
        {
            return AppendRange(CreateRange(values));
        }

        /// <summary>
        /// Same as <see cref="AppendRange(ImmutableLinkedList{T})"/>, but optimized for appending 
        /// another instance of <see cref="ImmutableLinkedList{T}"/>
        /// </summary>
        public ImmutableLinkedList<T> AppendRange(ImmutableLinkedList<T> list)
        {
            return list.PrependRange(this);
        }
        
        /// <summary>
        /// Returns a new list with the first instance of <paramref name="value"/> removed (if present).
        /// 
        /// This method is O(n). If <paramref name="value"/> exists in the list and is removed, all elements 
        /// prior to <paramref name="value"/> must be copied.
        /// </summary>
        public ImmutableLinkedList<T> Remove(T value)
        {
            EqualityComparer<T> comparer = EqualityComparer<T>.Default;
            for (Node? current = _head; current is not null; current = current.Next)
            {
                if (!comparer.Equals(current.Value, value))
                {
                    continue;
                }

                if (current == _head)
                {
                    return new ImmutableLinkedList<T>(_head.Next, Count - 1);
                }

                CopyNonEmptyRange(_head!, current, out Node newHead, out Node newLast);
                newLast.Next = current.Next;
                return new ImmutableLinkedList<T>(newHead, Count - 1);
            }

            return this;
        }

        /// <summary>
        /// Returns a new list with the <paramref name="index"/>th element removed. Throws <see cref="ArgumentOutOfRangeException"/>
        /// if <paramref name="index"/> is not a valid list index.
        /// 
        /// This method is O(<paramref name="index"/>) and will result in the copying of the first <paramref name="index"/> - 1 elements.
        /// </summary>
        public ImmutableLinkedList<T> RemoveAt(Int32 index)
        {
            return RemoveAt(index, out _);
        }

        /// <summary>
        /// Same as <see cref="RemoveAt(int)"/>, but also returns the <paramref name="removed"/> value
        /// </summary>
        public ImmutableLinkedList<T> RemoveAt(Int32 index, out T removed)
        {
            if (index < 0 || index >= Count)
            {
                throw new ArgumentOutOfRangeException(nameof(index), index, @"must be non-negative and less than the length of the list");
            }

            // remove at 0 requires no copying
            if (index == 0)
            {
                removed = _head!.Value;
                return new ImmutableLinkedList<T>(_head.Next, Count - 1);
            }

            Node? current = _head!.Next;
            for (Int32 i = 1; i < index; ++i)
            {
                current = current!.Next;
            }

            CopyNonEmptyRange(_head, current, out Node newHead, out Node newLast);
            newLast.Next = current!.Next;
            removed = current.Value;
            return new ImmutableLinkedList<T>(newHead, Count - 1);
        }

        /// <summary>
        /// Returns a new list with all elements matching <paramref name="predicate"/> removed.
        /// 
        /// This method is O(n). All retained elements prior to the last removed element are copied.
        /// </summary>
        public ImmutableLinkedList<T> RemoveAll(Func<T, Boolean> predicate)
        {
            // first, remove prefix since we can do this without any copying
            ImmutableLinkedList<T> withoutPrefix = SkipWhile(predicate);
            if (withoutPrefix.Count == 0)
            {
                return Empty;
            }

            // now, remove any elements from the middle/end, copying only if necessary
            Node newHead = withoutPrefix._head!;
            Node lastNonRemoved = newHead;
            Int32 countRemoved = 0;
            Node? current = newHead.Next;
            while (current is not null)
            {
                if (predicate(current.Value))
                {
                    if (countRemoved == 0)
                    {
                        // force a copy
                        CopyNonEmptyRange(newHead, current, out Node copiedNewHead, out Node copiedLastNonRemoved);
                        newHead = copiedNewHead;
                        lastNonRemoved = copiedLastNonRemoved;
                    }

                    ++countRemoved;
                }
                else if (countRemoved != 0)
                {
                    // if we copied, copy the current node
                    lastNonRemoved = lastNonRemoved.Next = new Node(current.Value);
                }
                else
                {
                    // if we didn't copy, we can just advance our last pointer
                    lastNonRemoved = current;
                }

                current = current.Next;
            }

            return new ImmutableLinkedList<T>(newHead, withoutPrefix.Count - countRemoved);
        }
        
        /// <summary>
        /// Same as <see cref="Enumerable.Skip{TSource}(IEnumerable{TSource}, int)"/>, except the
        /// return type is <see cref="ImmutableLinkedList{T}"/> and allocations are avoided.
        /// 
        /// This method is O(<paramref name="count"/>) and does not result in any copying.
        /// </summary>
        public ImmutableLinkedList<T> Skip(Int32 count)
        {
            if (count >= Count)
            {
                return Empty;
            }

            Int32 skipped = 0;
            Node? current = _head;
            while (current is not null && skipped < count)
            {
                current = current.Next;
                ++skipped;
            }

            return new ImmutableLinkedList<T>(current, Count - skipped);
        }

        /// <summary>
        /// Same as <see cref="Enumerable.SkipWhile{TSource}(IEnumerable{TSource}, Func{TSource, bool})"/>, except
        /// the return type is <see cref="ImmutableLinkedList{T}"/> and allocations are avoided.
        /// 
        /// This method is O(skipped) and does not result in any copying.
        /// </summary>
        public ImmutableLinkedList<T> SkipWhile(Func<T, Boolean> predicate)
        {
            if (predicate is null)
            {
                throw new ArgumentNullException(nameof(predicate));
            }

            Node? newHead = _head;
            Int32 countRemoved = 0;
            while (newHead is not null && predicate(newHead.Value))
            {
                newHead = newHead.Next;
                ++countRemoved;
            }

            return new ImmutableLinkedList<T>(newHead, Count - countRemoved);
        }

        /// <summary>
        /// Same as <see cref="Enumerable.SkipWhile{TSource}(IEnumerable{TSource}, Func{TSource, int, bool})"/>, except
        /// the return type is <see cref="ImmutableLinkedList{T}"/> and allocations are avoided.
        /// 
        /// This method is O(skipped) and does not result in any copying.
        /// </summary>
        public ImmutableLinkedList<T> SkipWhile(Func<T, Int32, Boolean> predicate)
        {
            if (predicate is null)
            {
                throw new ArgumentNullException(nameof(predicate));
            }

            Node? newHead = _head;
            Int32 index = 0;
            Int32 countRemoved = 0;
            while (newHead is not null && predicate(newHead.Value, index))
            {
                newHead = newHead.Next;
                ++index;
                ++countRemoved;
            }

            return new ImmutableLinkedList<T>(newHead, Count - countRemoved);
        }

        /// <summary>
        /// Returns an <see cref="ImmutableLinkedList{T}"/> for the index range described
        /// by <paramref name="startIndex"/> and <paramref name="count"/> (similar to
        /// <see cref="String.Substring(int, int)"/>).
        /// 
        /// This method is O(<paramref name="startIndex"/> + <paramref name="count"/>) and
        /// copies all elements in the returned sublist unless the sublist extends to the
        /// end of the current list.
        /// </summary>
        public ImmutableLinkedList<T> SubList(Int32 startIndex, Int32 count)
        {
            if (startIndex < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(startIndex), startIndex, @"must be non-negative");
            }

            if (count < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(count), count, @"must be non-negative");
            }

            if (startIndex + count > Count)
            {
                throw new ArgumentOutOfRangeException($"({nameof(startIndex)}, {nameof(count)})", $"({startIndex}, {count})", @"must represent a range of indices within the list");
            }

            if (count == 0)
            {
                return Empty;
            }

            ImmutableLinkedList<T> skipped = Skip(startIndex);
            if (count == skipped.Count)
            {
                return skipped;
            }

            Node? current = skipped._head!.Next;
            for (Int32 i = 1; i < count; ++i)
            {
                current = current!.Next;
            }

            CopyNonEmptyRange(skipped._head, current, out Node subListHead, out _);
            return new ImmutableLinkedList<T>(subListHead, count);
        }

        /// <summary>
        /// Same as <see cref="Enumerable.Reverse{TSource}(IEnumerable{TSource})"/>, but the return type is <see cref="ImmutableLinkedList{T}"/>.
        /// 
        /// This method is O(n) and involves copying the entire list for lists of length two or greater.
        /// </summary>
        public ImmutableLinkedList<T> Reverse()
        {
            if (Count < 2)
            {
                return this;
            }

            Node current = new Node(_head!.Value);
            for (Node? next = _head.Next; next is not null; next = next.Next)
            {
                current = new Node(next.Value) {Next = current};
            }

            return new ImmutableLinkedList<T>(current, Count);
        }

        /// <summary>
        /// Returns a new list with the elements sorted. The sort is stable.
        /// 
        /// This method uses the merge sort algorithm and is O(nlg(n)). However, it 
        /// can take advantage of existing sorted structure in the input to run as quickly
        /// as O(n) in some cases (e. g. already-sorted input).
        /// 
        /// Elements are only copied as needed. For example, if a trailing portion of the 
        /// original list remains unchanged in the sorted output, those elements will not 
        /// be copied. As an extension of this, calling this method on an already-sorted list 
        /// results in no copying (the original list is returned).
        /// </summary>
        public ImmutableLinkedList<T> Sort(IComparer<T>? comparer = null)
        {
            if (Count < 2)
            {
                return this;
            }

            SortHelper(_head!, Count, comparer ?? Comparer<T>.Default, out SortedSegment sorted, out Node? _);

            return new ImmutableLinkedList<T>(sorted.First, Count);
        }

        private static void SortHelper(
            Node head,
            Int32 count,
            IComparer<T> comparer,
            out SortedSegment sorted,
            out Node? next)
        {
            // base case
            if (count == 1)
            {
                sorted = new SortedSegment {First = head, LastCopied = null, Last = head};
                next = head.Next;
                return;
            }

            // sort the first half
            Int32 firstHalfCount = count >> 1;
            SortHelper(head, firstHalfCount, comparer, out SortedSegment sortedFirstHalf, out Node? secondHalfHead);

            // sort the second half
            SortHelper(secondHalfHead!, count - firstHalfCount, comparer, out SortedSegment sortedSecondHalf, out next);

            // merge

            // see if we can short-circuit
            if (TryShortCircuitMerge(ref sortedFirstHalf, ref sortedSecondHalf, count, comparer, out sorted))
            {
                return;
            }

            // full-on merge
            Merge(ref sortedFirstHalf, ref sortedSecondHalf, comparer, out sorted);
        }

        /// <summary>
        /// An optimistic version of <see cref="Merge(ref SortedSegment, ref SortedSegment, IComparer{T}, out SortedSegment)"/> which
        /// can skip most of the work in the case where one segment goes entirely before the other. This allows us to achieve O(n)
        /// performance for mostly-sorted or mostly-reverse sorted input and generally take advantage of existing sorted subsequences
        /// in the data.
        /// </summary>
        private static Boolean TryShortCircuitMerge(
            // passed by ref to avoid copying
            ref SortedSegment segment1,
            ref SortedSegment segment2,
            Int32 count,
            IComparer<T> comparer,
            out SortedSegment merged)
        {
            // see if the segments are already in order
            if (comparer.Compare(segment1.Last.Value, segment2.First.Value) <= 0)
            {
                if (segment1.Last.Next != segment2.First)
                {
                    // we only have to link the two if they aren't already linked
                    EnsureFullyCopied(ref segment1);
                    segment1.Last.Next = segment2.First;
                }

                merged = new SortedSegment {First = segment1.First, LastCopied = segment2.LastCopied, Last = segment2.Last};
                return true;
            }

            // see if all of second goes before all of first (always true if count is 2)
            if (count == 2 || comparer.Compare(segment2.Last.Value, segment1.First.Value) <= 0)
            {
                // since we're doing a swap, both segments must be fully copied
                EnsureFullyCopied(ref segment1);
                EnsureFullyCopied(ref segment2);
                segment2.Last.Next = segment1.First;
                merged = new SortedSegment {First = segment2.First, Last = segment1.Last, LastCopied = segment1.Last};
                return true;
            }

            merged = default;
            return false;
        }

        /// <summary>
        /// The standard merge algorithm, adjusted to manage proper copying. Assumes that
        /// <see cref="TryShortCircuitMerge(ref SortedSegment, ref SortedSegment, int, IComparer{T}, out SortedSegment)"/>
        /// has already been called and returned false.
        /// </summary>
        private static void Merge(
            // passed by ref to avoid copying
            ref SortedSegment segment1,
            ref SortedSegment segment2,
            IComparer<T> comparer,
            out SortedSegment merged)
        {
            // if we need to do a full merge, then no nodes in segment1 are retaining
            // their positions. Therefore segment1 must be fully copied
            EnsureFullyCopied(ref segment1);
            Node? next1 = segment1.First;
            Node? next2 = segment2.First;
            Boolean isNext2Copied = segment2.LastCopied is not null;
            Node? mergedFirst = null;
            Node? mergedLast = null;
            do
            {
                if (comparer.Compare(next1.Value, next2.Value) <= 0)
                {
                    mergedLast = mergedFirst is null
                        ? mergedFirst = next1
                        : mergedLast!.Next = next1;
                    next1 = next1 == segment1.Last ? null : next1.Next;
                }
                else
                {
                    // this loop runs so long as there are nodes remaining in both 1 and 2.
                    // In that case, any node added as part of this loop will be followed by
                    // at least one node from the other segment. Therefore, only copies may
                    // be added

                    Node copiedNext2;
                    if (isNext2Copied)
                    {
                        copiedNext2 = next2;
                        if (next2 == segment2.LastCopied)
                        {
                            isNext2Copied = false;
                        }
                    }
                    else
                    {
                        copiedNext2 = new Node(next2.Value);
                    }

                    mergedLast = mergedFirst is null
                        ? mergedFirst = copiedNext2
                        : mergedLast!.Next = copiedNext2;
                    next2 = next2 == segment2.Last ? null : next2.Next;
                }
            } while (next1 is not null && next2 is not null);

            // add the remainder
            Node? mergedLastCopied;
            if (next1 is null)
            {
                // the remainder is the rest of segment2

                // if we are still in the copied region of segment2, then the last
                // copied node is the last copied node from segment 2 (which we haven't reached).
                // Otherwise, the last copied node is simply the last node added in the loop, since
                // only copies are added there
                mergedLastCopied = isNext2Copied ? segment2.LastCopied : mergedLast;
                mergedLast.Next = next2;
                mergedLast = segment2.Last;
            }
            else
            {
                // the remainder is the rest of segment1
                mergedLast.Next = next1;
                // In this case, the last copied node is the last node in merged since all of segment1 has been copied
                mergedLastCopied = mergedLast = segment1.Last;
            }

            merged = new SortedSegment {First = mergedFirst, LastCopied = mergedLastCopied, Last = mergedLast};
        }

        private static void EnsureFullyCopied(ref SortedSegment segment)
        {
            if (segment.LastCopied != segment.Last)
            {
                FullyCopy(ref segment);
            }
        }

        private static void FullyCopy(ref SortedSegment segment)
        {
            if (segment.LastCopied is null)
            {
                CopyNonEmptyRange(segment.First, segment.Last.Next, out segment.First, out segment.Last);
            }
            else
            {
                CopyNonEmptyRange(segment.LastCopied.Next!, segment.Last!.Next, out Node firstCopied, out segment.Last);
                segment.LastCopied.Next = firstCopied;
            }

            segment.LastCopied = segment.Last;
        }

        private struct SortedSegment
        {
            public Node First, Last;
            public Node? LastCopied;
        }
    }
}