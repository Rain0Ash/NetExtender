// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using NetExtender.Types.Comparers;

namespace NetExtender.Types.Immutable.LinkedLists
{
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

    public readonly struct ImmutableLinkedList<T> : ICollection<T>, IReadOnlyCollection<T>, IEquatable<ImmutableLinkedList<T>>
    {
        public static ImmutableLinkedList<T> Empty
        {
            get
            {
                return default;
            }
        }

        internal static ImmutableLinkedList<T> Create(T value)
        {
            return new ImmutableLinkedList<T>(new Node(value), 1);
        }

        internal static ImmutableLinkedList<T> CreateRange(IEnumerable<T> values)
        {
            if (values is null)
            {
                throw new ArgumentNullException(nameof(values));
            }

            if (values is ImmutableLinkedList<T> source)
            {
                return source;
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

        private Node? Head { get; init; }

        /// <summary>
        /// The first element in the list. Throws <see cref="InvalidOperationException"/> if the list is empty.
        /// </summary>
        public T First
        {
            get
            {
                if (Head is null || Count <= 0)
                {
                    throw new InvalidOperationException();
                }

                return Head.Value;
            }
            private init
            {
                Head = new Node(value);
            }
        }

        /// <summary>
        /// A list consisting of all elements except the first. Throws <see cref="InvalidOperationException"/> if the list is empty.
        /// This property is O(1) and does not require any copying.
        /// </summary>
        public ImmutableLinkedList<T> Tail
        {
            get
            {
                if (Head is null || Count <= 0)
                {
                    throw new InvalidOperationException();
                }

                return new ImmutableLinkedList<T>(Head.Next, Count - 1);
            }
        }

        /// <summary>
        /// The length of the list
        /// </summary>
        public Int32 Count { get; }

        Boolean ICollection<T>.IsReadOnly
        {
            get
            {
                return true;
            }
        }

        private ImmutableLinkedList(Node? head, Int32 count)
        {
            Head = head;
            Count = count;
        }

        /// <summary>
        /// Splits the list into head and tail in a single operation. Throws <see cref="InvalidOperationException"/> if the
        /// list is empty. This method can be invoked implicitly using tuple deconstruction syntax:
        /// This method runs in O(1) and does not require any copying.
        /// </summary>
        public void Deconstruct(out T head, out ImmutableLinkedList<T> tail)
        {
            if (Head is null || Count <= 0)
            {
                throw new InvalidOperationException();
            }

            head = Head.Value;
            tail = new ImmutableLinkedList<T>(Head.Next, Count - 1);
        }

        /// <summary>
        /// Equivalent to <see cref="Deconstruct(out T, out ImmutableLinkedList{T})"/>, but 
        /// returns false rather than throwing if the list is empty
        /// </summary>
        public Boolean TryDeconstruct([MaybeNullWhen(false)] out T head, out ImmutableLinkedList<T> tail)
        {
            if (Head is null || Count <= 0)
            {
                head = default;
                tail = default;
                return false;
            }

            head = Head.Value;
            tail = new ImmutableLinkedList<T>(Head.Next, Count - 1);
            return true;
        }

        /// <summary>
        /// Returns true if <paramref name="value"/> is an element of the list.
        /// This method is O(N)
        /// </summary>
        public Boolean Contains(T value)
        {
            for (Node? current = Head; current is not null; current = current.Next)
            {
                if (EqualityComparer<T>.Default.Equals(current.Value, value))
                {
                    return true;
                }
            }

            return false;
        }

        void ICollection<T>.Add(T item)
        {
            throw new NotSupportedException();
        }

        /// <summary>
        /// Returns a new list with <paramref name="value"/> appended.
        /// This method is O(n) and requires copying the entire list.
        /// </summary>
        public ImmutableLinkedList<T> Append(T value)
        {
            if (Head is null || Count <= 0)
            {
                return Create(value);
            }

            if (!CopyRange(Head, null, out Node? first, out Node? last))
            {
                return this;
            }
            
            last.Next = new Node(value);
            return new ImmutableLinkedList<T>(first, Count + 1);
        }

        /// <summary>
        /// Returns a new list with <paramref name="values"/> appended.
        /// This method is O(n + k) where k is the number of elements in <paramref name="values"/> and requires copying the entire list.
        /// </summary>
        public ImmutableLinkedList<T> AppendRange(IEnumerable<T> values)
        {
            return AppendRange(CreateRange(values));
        }

        /// <summary>
        /// Same as <see cref="AppendRange(ImmutableLinkedList{T})"/>, but optimized for appending 
        /// another instance of <see cref="ImmutableLinkedList{T}"/>
        /// </summary>
        public ImmutableLinkedList<T> AppendRange(ImmutableLinkedList<T> source)
        {
            return source.PrependRange(this);
        }

        /// <summary>
        /// Returns a new list with <paramref name="value"/> prepended.
        /// This method is O(1) and requires no copying.
        /// </summary>
        public ImmutableLinkedList<T> Prepend(T value)
        {
            return new ImmutableLinkedList<T>(new Node(value) { Next = Head }, Count + 1);
        }

        /// <summary>
        /// Returns a new list with <paramref name="values"/> prepended such that the first element of <paramref name="values"/> values
        /// becomes the first element of the returned list.
        /// This method is O(k) where k is the number of elements in <paramref name="values"/> and requires no copying.
        /// </summary>
        public ImmutableLinkedList<T> PrependRange(IEnumerable<T> values)
        {
            if (values is null)
            {
                throw new ArgumentNullException(nameof(values));
            }

            if (Count <= 0)
            {
                return CreateRange(values);
            }

            if (values is ImmutableLinkedList<T> source)
            {
                return PrependRange(source);
            }

            using IEnumerator<T> enumerator = values.GetEnumerator();
            if (!enumerator.MoveNext())
            {
                return this;
            }

            Node first = new Node(enumerator.Current);
            Node last = first;

            Int32 count = 1;
            while (enumerator.MoveNext())
            {
                last = last.Next = new Node(enumerator.Current);
                ++count;
            }

            last.Next = Head;
            return new ImmutableLinkedList<T>(first, count + Count);
        }

        /// <summary>
        /// Same as <see cref="PrependRange(IEnumerable{T})"/>, but optimized for prepending another
        /// instance of <see cref="ImmutableLinkedList{T}"/>
        /// </summary>
        public ImmutableLinkedList<T> PrependRange(ImmutableLinkedList<T> source)
        {
            if (source.Head is null || source.Count <= 0)
            {
                return this;
            }

            if (Count <= 0)
            {
                return source;
            }

            if (!CopyRange(source.Head, null, out Node? first, out Node? last))
            {
                return this;
            }
            
            last.Next = Head;
            return new ImmutableLinkedList<T>(first, Count + source.Count);
        }

        /// <summary>
        /// Same as <see cref="Enumerable.Skip{TSource}(IEnumerable{TSource}, int)"/>, except the
        /// return type is <see cref="ImmutableLinkedList{T}"/> and allocations are avoided.
        /// This method is O(<paramref name="count"/>) and does not result in any copying.
        /// </summary>
        public ImmutableLinkedList<T> Skip(Int32 count)
        {
            if (count >= Count)
            {
                return Empty;
            }

            Int32 skip = 0;
            Node? current = Head;
            while (current is not null && skip < count)
            {
                current = current.Next;
                ++skip;
            }

            return new ImmutableLinkedList<T>(current, Count - skip);
        }

        /// <summary>
        /// Same as <see cref="Enumerable.SkipWhile{TSource}(IEnumerable{TSource}, Func{TSource, bool})"/>, except
        /// the return type is <see cref="ImmutableLinkedList{T}"/> and allocations are avoided.
        /// This method is O(skipped) and does not result in any copying.
        /// </summary>
        public ImmutableLinkedList<T> SkipWhile(Func<T, Boolean> predicate)
        {
            if (predicate is null)
            {
                throw new ArgumentNullException(nameof(predicate));
            }

            Int32 count = 0;
            Node? head = Head;
            while (head is not null && predicate(head.Value))
            {
                head = head.Next;
                ++count;
            }

            return new ImmutableLinkedList<T>(head, Count - count);
        }

        /// <summary>
        /// Same as <see cref="Enumerable.SkipWhile{TSource}(IEnumerable{TSource}, Func{TSource, int, bool})"/>, except
        /// the return type is <see cref="ImmutableLinkedList{T}"/> and allocations are avoided.
        /// This method is O(skipped) and does not result in any copying.
        /// </summary>
        public ImmutableLinkedList<T> SkipWhile(Func<T, Int32, Boolean> predicate)
        {
            if (predicate is null)
            {
                throw new ArgumentNullException(nameof(predicate));
            }

            Int32 count = 0;
            Int32 index = 0;
            Node? head = Head;
            while (head is not null && predicate(head.Value, index))
            {
                head = head.Next;
                ++index;
                ++count;
            }

            return new ImmutableLinkedList<T>(head, Count - count);
        }

        Boolean ICollection<T>.Remove(T item)
        {
            throw new NotSupportedException();
        }

        /// <summary>
        /// Returns a new list with the first instance of <paramref name="value"/> removed (if present).
        /// This method is O(n). If <paramref name="value"/> exists in the list and is removed, all elements 
        /// prior to <paramref name="value"/> must be copied.
        /// </summary>
        public ImmutableLinkedList<T> Remove(T value)
        {
            for (Node? current = Head; current is not null; current = current.Next)
            {
                if (!EqualityComparer<T>.Default.Equals(current.Value, value))
                {
                    continue;
                }

                if (current == Head)
                {
                    return new ImmutableLinkedList<T>(Head.Next, Count - 1);
                }

                if (!CopyRange(Head, current, out Node? first, out Node? last))
                {
                    return this;
                }
                
                last.Next = current.Next;
                return new ImmutableLinkedList<T>(first, Count - 1);
            }

            return this;
        }

        /// <summary>
        /// Returns a new list with the <paramref name="index"/>th element removed. Throws <see cref="ArgumentOutOfRangeException"/>
        /// if <paramref name="index"/> is not a valid list index.
        /// This method is O(<paramref name="index"/>) and will result in the copying of the first <paramref name="index"/> - 1 elements.
        /// </summary>
        public ImmutableLinkedList<T> RemoveAt(Int32 index)
        {
            return RemoveAt(index, out _);
        }

        /// <summary>
        /// Same as <see cref="RemoveAt(int)"/>, but also returns the <paramref name="removed"/> value
        /// </summary>
        public ImmutableLinkedList<T> RemoveAt(Int32 index, out T? removed)
        {
            if (Head is null || index < 0 || index >= Count)
            {
                throw new ArgumentOutOfRangeException(nameof(index), index, @"must be non-negative and less than the length of the list");
            }

            if (index <= 0)
            {
                removed = Head.Value;
                return new ImmutableLinkedList<T>(Head.Next, Count - 1);
            }

            Node? current = Head.Next;
            for (Int32 i = 1; current is not null && i < index; ++i)
            {
                current = current.Next;
            }

            if (!CopyRange(Head, current, out Node? first, out Node? last))
            {
                removed = default;
                return this;
            }
            
            last.Next = current?.Next;
            removed = current is not null ? current.Value : default;
            return new ImmutableLinkedList<T>(first, Count - 1);
        }

        /// <summary>
        /// Returns a new list with all elements matching <paramref name="predicate"/> removed.
        /// This method is O(n). All retained elements prior to the last removed element are copied.
        /// </summary>
        // ReSharper disable once CognitiveComplexity
        public ImmutableLinkedList<T> RemoveAll(Func<T, Boolean> predicate)
        {
            if (predicate is null)
            {
                throw new ArgumentNullException(nameof(predicate));
            }

            ImmutableLinkedList<T> without = SkipWhile(predicate);
            if (without.Count <= 0 || without.Head is not { } first)
            {
                return Empty;
            }

            Node? last = first;
            Node? current = first.Next;
            Int32 count = 0;
            while (current is not null)
            {
                if (predicate(current.Value))
                {
                    if (count <= 0)
                    {
                        CopyRange(first, current, out first, out last);
                    }

                    ++count;
                    continue;
                }

                if (count > 0 && last is not null)
                {
                    last = last.Next = new Node(current.Value);
                    continue;
                }

                last = current;
                current = current.Next;
            }

            return new ImmutableLinkedList<T>(first, without.Count - count);
        }

        void ICollection<T>.Clear()
        {
            throw new NotSupportedException();
        }

        /// <summary>
        /// Same as <see cref="Enumerable.SequenceEqual{TSource}(IEnumerable{TSource}, IEnumerable{TSource}, IEqualityComparer{TSource})"/>,
        /// but optimized for comparing two instances of <see cref="ImmutableLinkedList{T}"/>.
        /// This method is O(n).
        /// </summary>
        public Boolean SequenceEqual(ImmutableLinkedList<T> source)
        {
            return SequenceEqual(source, null);
        }

        /// <summary>
        /// Same as <see cref="Enumerable.SequenceEqual{TSource}(IEnumerable{TSource}, IEnumerable{TSource}, IEqualityComparer{TSource})"/>,
        /// but optimized for comparing two instances of <see cref="ImmutableLinkedList{T}"/>.
        /// This method is O(n).
        /// </summary>
        public Boolean SequenceEqual(ImmutableLinkedList<T> source, IEqualityComparer<T>? comparer)
        {
            if (Count != source.Count)
            {
                return false;
            }

            comparer ??= EqualityComparer<T>.Default;

            Node? current = Head;
            Node? other = source.Head;
            while (true)
            {
                if (current == other)
                {
                    return true;
                }

                if (current is null || !comparer.Equals(current.Value, other!.Value))
                {
                    return false;
                }

                current = current.Next;
                other = other.Next;
            }
        }

        /// <summary>
        /// Same as <see cref="Enumerable.Reverse{TSource}(IEnumerable{TSource})"/>, but the return type is <see cref="ImmutableLinkedList{T}"/>.
        /// This method is O(n) and involves copying the entire list for lists of length two or greater.
        /// </summary>
        public ImmutableLinkedList<T> Reverse()
        {
            if (Head is null || Count <= 1)
            {
                return this;
            }

            Node current = new Node(Head.Value);
            for (Node? next = Head.Next; next is not null; next = next.Next)
            {
                current = new Node(next.Value) { Next = current };
            }

            return new ImmutableLinkedList<T>(current, Count);
        }

        public ImmutableLinkedList<T> Sort()
        {
            return Sort(null);
        }

        public ImmutableLinkedList<T> Sort(IComparer<T>? comparer)
        {
            if (Head is null || Count <= 1)
            {
                return this;
            }

            SortInternal(Head, Count, comparer, out Segment sorted, out Node? _);
            return new ImmutableLinkedList<T>(sorted.First, Count);
        }

        private static void SortInternal(Node head, Int32 count, IComparer<T>? comparer, out Segment sorted, out Node? next)
        {
            comparer ??= ComparisonComparer<T>.Default;
            
            if (count == 1)
            {
                sorted = new Segment { First = head, Copied = null, Last = head };
                next = head.Next;
                return;
            }

            Int32 half = count >> 1;
            SortInternal(head, half, comparer, out Segment segment1, out Node? half2);

            if (half2 is null)
            {
                sorted = segment1;
                next = head.Next;
                return;
            }
            
            SortInternal(half2, count - half, comparer, out Segment segment2, out next);

            if (!TryShortCircuitMerge(ref segment1, ref segment2, count, comparer, out sorted))
            {
                Merge(ref segment1, ref segment2, comparer, out sorted);
            }
        }

        private static Boolean TryShortCircuitMerge(ref Segment first, ref Segment second, Int32 count, IComparer<T>? comparer, out Segment result)
        {
            comparer ??= Comparer<T>.Default;

            if (comparer.Compare(first.Last.Value, second.First.Value) <= 0)
            {
                if (first.Last.Next != second.First)
                {
                    EnsureFullCopy(ref first);
                    first.Last.Next = second.First;
                }

                result = new Segment { First = first.First, Copied = second.Copied, Last = second.Last };
                return true;
            }

            if (count != 2 && comparer.Compare(second.Last.Value, first.First.Value) > 0)
            {
                result = default;
                return false;
            }

            EnsureFullCopy(ref first);
            EnsureFullCopy(ref second);
            second.Last.Next = first.First;
            result = new Segment { First = second.First, Last = first.Last, Copied = first.Last };
            return true;
        }

        // ReSharper disable once CognitiveComplexity
        private static void Merge(ref Segment segment1, ref Segment segment2, IComparer<T> comparer, out Segment result)
        {
            EnsureFullCopy(ref segment1);
            Node? next1 = segment1.First;
            Node? next2 = segment2.First;
            Boolean iscopied = segment2.Copied is not null;
            Node? first = null;
            Node? last = null;
            
            do
            {
                if (comparer.Compare(next1.Value, next2.Value) <= 0)
                {
                    last = first is null || last is null ? first = next1 : last.Next = next1;
                    next1 = next1 == segment1.Last ? null : next1.Next;
                    continue;
                }

                Node copy;
                if (iscopied)
                {
                    copy = next2;
                    if (next2 == segment2.Copied)
                    {
                        iscopied = false;
                    }
                }
                else
                {
                    copy = new Node(next2.Value);
                }

                last = first is null || last is null ? first = copy : last.Next = copy;
                next2 = next2 == segment2.Last ? null : next2.Next;
            } while (next1 is not null && next2 is not null);

            Node? copied;
            if (next1 is null)
            {
                copied = iscopied ? segment2.Copied : last;
                last.Next = next2;
                last = segment2.Last;
            }
            else
            {
                last.Next = next1;
                copied = last = segment1.Last;
            }

            result = new Segment { First = first, Copied = copied, Last = last };
        }

        /// <summary>
        /// Returns an <see cref="ImmutableLinkedList{T}"/> for the index range described
        /// by <paramref name="start"/> and <paramref name="count"/> (similar to
        /// <see cref="String.Substring(int, int)"/>).
        /// This method is O(<paramref name="start"/> + <paramref name="count"/>) and
        /// copies all elements in the returned sublist unless the sublist extends to the
        /// end of the current list.
        /// </summary>
        public ImmutableLinkedList<T> SubList(Int32 start, Int32 count)
        {
            if (start < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(start), start, null);
            }

            if (count < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(count), count, null);
            }

            if (start + count > Count)
            {
                throw new ArgumentOutOfRangeException(nameof(count), count, null);
            }

            if (count <= 0)
            {
                return Empty;
            }

            ImmutableLinkedList<T> skip = Skip(start);
            if (count == skip.Count)
            {
                return skip;
            }

            Node? current = skip.Head?.Next;
            for (Int32 i = 1; i < count; ++i)
            {
                current = current?.Next;
            }

            return CopyRange(skip.Head, current, out Node? head, out _) ? new ImmutableLinkedList<T>(head, count) : this;
        }

        private static Boolean CopyRange(Node? first, Node? last, [MaybeNullWhen(false)] out Node newhead, [MaybeNullWhen(false)] out Node newlast)
        {
            if (first is null)
            {
                newhead = default;
                newlast = default;
                return false;
            }
            
            Node current = newhead = new Node(first.Value);
            for (Node? next = first.Next; next is not null && next != last; next = next.Next)
            {
                current = current.Next = new Node(next.Value);
            }

            newlast = current;
            return true;
        }

        private static void FullCopy(ref Segment segment)
        {
            if (segment.Copied is null)
            {
                if (!CopyRange(segment.First, segment.Last.Next, out Node? first, out Node? last))
                {
                    return;
                }

                segment.First = first;
                segment.Last = last;
                segment.Copied = segment.Last;
            }
            else
            {
                if (!CopyRange(segment.Copied.Next, segment.Last.Next, out Node? copy, out Node? last))
                {
                    return;
                }

                segment.Last = last;
                segment.Copied.Next = copy;
                segment.Copied = segment.Last;
            }
        }

        private static void EnsureFullCopy(ref Segment segment)
        {
            if (segment.Copied != segment.Last)
            {
                FullCopy(ref segment);
            }
        }

        /// <summary>
        /// Prevents boxing when using lists with <see cref="EqualityComparer{T}.Default"/>
        /// </summary>
        public Boolean Equals(ImmutableLinkedList<T> other)
        {
            return Head == other.Head;
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

            for (Node? current = Head; current is not null; current = current.Next)
            {
                array[arrayIndex++] = current.Value;
            }
        }

        public Enumerator GetEnumerator()
        {
            return new Enumerator(Head);
        }

        IEnumerator<T> IEnumerable<T>.GetEnumerator()
        {
            return GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
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
        /// An enumerator over <see cref="ImmutableLinkedList{T}"/>. See <see cref="ImmutableLinkedList{T}.GetEnumerator"/>
        /// for more details
        /// </summary>
        public struct Enumerator : IEnumerator<T>
        {
            private Node? Next { get; set; }

            /// <summary>
            /// The current value. Behavior is undefined before enumeration begins
            /// and after it ends
            /// </summary>
            public T Current { get; private set; }

            readonly Object? IEnumerator.Current
            {
                get
                {
                    return Current;
                }
            }

            internal Enumerator(Node? first)
            {
                Next = first;
                Current = default!;
            }

            /// <summary>
            /// Advances the enumerator, returning false if the end of the list has been reached
            /// </summary>
            public Boolean MoveNext()
            {
                if (Next is null)
                {
                    Current = default!;
                    return false;
                }

                Current = Next.Value;
                Next = Next.Next;
                return true;
            }

            void IEnumerator.Reset()
            {
                throw new NotSupportedException();
            }

            /// <summary>
            /// Cleans up any resources held by the enumerator (currently none)
            /// </summary>
            public void Dispose()
            {
            }
        }

        private struct Segment
        {
            public Node First { get; set; }
            public Node Last { get; set; }
            public Node? Copied { get; set; }
        }
    }
}