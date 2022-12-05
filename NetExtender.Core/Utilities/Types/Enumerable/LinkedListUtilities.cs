// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;

namespace NetExtender.Utilities.Types
{
    public static class LinkedListUtilities
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static LinkedList<T> ToLinkedList<T>(this IEnumerable<T> source)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            return new LinkedList<T>(source);
        }

        public static LinkedListNode<T> Add<T>(this LinkedList<T> collection, T item)
        {
            if (collection is null)
            {
                throw new ArgumentNullException(nameof(collection));
            }

            return collection.AddLast(item);
        }

        public static void Add<T>(this LinkedList<T> collection, LinkedListNode<T> node)
        {
            if (collection is null)
            {
                throw new ArgumentNullException(nameof(collection));
            }

            if (node is null)
            {
                throw new ArgumentNullException(nameof(node));
            }

            collection.AddLast(node);
        }

        public static LinkedListNode<T> Insert<T>(this LinkedList<T> collection, Int32 index, T item)
        {
            if (collection is null)
            {
                throw new ArgumentNullException(nameof(collection));
            }

            if (index < 0 || index >= collection.Count)
            {
                throw new ArgumentOutOfRangeException(nameof(index), index, null);
            }

            LinkedListNode<T>? node = collection.NodeAt(index);

            if (node is null)
            {
                throw new ArgumentOutOfRangeException(nameof(index), index, null);
            }

            return collection.AddBefore(node, item);
        }

        public static LinkedListNode<T> Insert<T>(this LinkedList<T> collection, Index index, T item)
        {
            if (collection is null)
            {
                throw new ArgumentNullException(nameof(collection));
            }

            return Insert(collection, index.GetOffset(collection.Count), item);
        }

        public static void Insert<T>(this LinkedList<T> collection, Int32 index, LinkedListNode<T> newNode)
        {
            if (collection is null)
            {
                throw new ArgumentNullException(nameof(collection));
            }

            if (newNode is null)
            {
                throw new ArgumentNullException(nameof(newNode));
            }

            if (index < 0 || index >= collection.Count)
            {
                throw new ArgumentOutOfRangeException(nameof(index), index, null);
            }

            LinkedListNode<T>? node = collection.NodeAt(index);

            if (node is null)
            {
                throw new ArgumentOutOfRangeException(nameof(index), index, null);
            }

            collection.AddBefore(node, newNode);
        }

        public static void Insert<T>(this LinkedList<T> collection, Index index, LinkedListNode<T> node)
        {
            if (collection is null)
            {
                throw new ArgumentNullException(nameof(collection));
            }

            if (node is null)
            {
                throw new ArgumentNullException(nameof(node));
            }

            Insert(collection, index.GetOffset(collection.Count), node);
        }

        public static LinkedListNode<T> RemoveAt<T>(this LinkedList<T> collection, Int32 index)
        {
            if (collection is null)
            {
                throw new ArgumentNullException(nameof(collection));
            }

            if (index < 0 || index >= collection.Count)
            {
                throw new ArgumentOutOfRangeException(nameof(index), index, null);
            }

            LinkedListNode<T>? node = collection.NodeAt(index);

            if (node is null)
            {
                throw new ArgumentOutOfRangeException(nameof(index), index, null);
            }

            collection.Remove(node);
            return node;
        }

        public static Boolean TryRemoveFirst<T>(this LinkedList<T> collection)
        {
            if (collection is null)
            {
                throw new ArgumentNullException(nameof(collection));
            }

            if (collection.Count <= 0)
            {
                return false;
            }

            try
            {
                collection.RemoveFirst();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public static Boolean TryRemoveLast<T>(this LinkedList<T> collection)
        {
            if (collection is null)
            {
                throw new ArgumentNullException(nameof(collection));
            }

            if (collection.Count <= 0)
            {
                return false;
            }

            try
            {
                collection.RemoveLast();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public static Int32 IndexOf<T>(this LinkedList<T> collection, T item)
        {
            if (collection is null)
            {
                throw new ArgumentNullException(nameof(collection));
            }

            IEqualityComparer<T> comparer = EqualityComparer<T>.Default;

            switch (collection.Count)
            {
                case 0:
                    return -1;
                case 1:
                    return comparer.Equals(collection.First!.Value, item) ? 0 : -1;
                case 2:
                    return comparer.Equals(collection.First!.Value, item) ? 0 : comparer.Equals(collection.Last!.Value, item) ? 1 : -1;
                default:
                    Int32 index = 0;
                    foreach (T value in collection)
                    {
                        if (comparer.Equals(value, item))
                        {
                            return index;
                        }

                        index++;
                    }

                    return -1;
            }
        }

        public static T Peek<T>(this LinkedList<T> collection)
        {
            if (collection is null)
            {
                throw new ArgumentNullException(nameof(collection));
            }

            LinkedListNode<T>? node = collection.First;
            return node is not null ? node.Value : throw new InvalidOperationException();
        }

        public static Boolean TryPeek<T>(this LinkedList<T> collection, [MaybeNullWhen(false)] out T result)
        {
            if (collection is null)
            {
                throw new ArgumentNullException(nameof(collection));
            }

            LinkedListNode<T>? node = collection.First;

            if (node is null)
            {
                result = default;
                return false;
            }

            result = node.Value;
            return true;
        }

        public static void Enqueue<T>(this LinkedList<T> collection, T item)
        {
            if (collection is null)
            {
                throw new ArgumentNullException(nameof(collection));
            }

            collection.AddLast(item);
        }

        public static T Dequeue<T>(this LinkedList<T> collection)
        {
            if (collection is null)
            {
                throw new ArgumentNullException(nameof(collection));
            }

            LinkedListNode<T>? node = collection.First;

            if (node is null)
            {
                throw new InvalidOperationException();
            }

            collection.RemoveFirst();
            return node.Value;
        }

        public static Boolean TryDequeue<T>(this LinkedList<T> collection, [MaybeNullWhen(false)] out T result)
        {
            if (collection is null)
            {
                throw new ArgumentNullException(nameof(collection));
            }

            LinkedListNode<T>? node = collection.First;

            if (node is null)
            {
                result = default;
                return false;
            }

            collection.TryRemoveFirst();
            result = node.Value;
            return true;
        }

        public static T DequeueLast<T>(this LinkedList<T> collection)
        {
            if (collection is null)
            {
                throw new ArgumentNullException(nameof(collection));
            }

            LinkedListNode<T>? node = collection.Last;

            if (node is null)
            {
                throw new InvalidOperationException();
            }

            collection.RemoveLast();
            return node.Value;
        }

        public static Boolean TryDequeueLast<T>(this LinkedList<T> collection, [MaybeNullWhen(false)] out T result)
        {
            if (collection is null)
            {
                throw new ArgumentNullException(nameof(collection));
            }

            LinkedListNode<T>? node = collection.Last;

            if (node is null)
            {
                result = default;
                return false;
            }

            collection.TryRemoveLast();
            result = node.Value;
            return true;
        }

        public static void Swap<T>(this LinkedListNode<T> first, LinkedListNode<T> second)
        {
            if (first is null)
            {
                throw new ArgumentNullException(nameof(first));
            }

            if (second is null)
            {
                throw new ArgumentNullException(nameof(second));
            }

            (first.Value, second.Value) = (second.Value, first.Value);
        }

        [SuppressMessage("ReSharper", "CognitiveComplexity")]
        public static void Swap<T>(this LinkedList<T> collection, Int32 index1, Int32 index2)
        {
            if (collection is null)
            {
                throw new ArgumentNullException(nameof(collection));
            }

            if (index1 < 0 || index1 >= collection.Count)
            {
                throw new ArgumentOutOfRangeException(nameof(index1), index1, null);
            }

            if (index2 < 0 || index2 >= collection.Count)
            {
                throw new ArgumentOutOfRangeException(nameof(index2), index2, null);
            }

            if (index1 == index2)
            {
                return;
            }

            if (index1 > index2)
            {
                (index1, index2) = (index2, index1);
            }

            Int32 right = collection.Count - index2 - 1;

            LinkedListNode<T>? first;
            LinkedListNode<T>? second;
            if (index1 <= right)
            {
                first = collection.NodeAt(index1);

                Int32 offset = index2 - index1;
                if (right < offset)
                {
                    second = collection.Last;
                    for (Int32 i = collection.Count - 1; i > index2 && second is not null; i--)
                    {
                        second = second.Previous;
                    }
                }
                else
                {
                    second = first;
                    for (Int32 i = 0; i < offset && second is not null; i++)
                    {
                        second = second.Next;
                    }
                }
            }
            else
            {
                first = collection.NodeAt(index2);

                Int32 offset = index1 - index2;
                if (right > offset)
                {
                    second = collection.First;
                    for (Int32 i = 0; i < index1 && second is not null; i++)
                    {
                        second = second.Next;
                    }
                }
                else
                {
                    second = first;
                    for (Int32 i = offset; i > 0 && second is not null; i--)
                    {
                        second = second.Previous;
                    }
                }
            }

            if (first is null || second is null)
            {
                return;
            }

            Swap(first, second);
        }

        [SuppressMessage("ReSharper", "CognitiveComplexity")]
        public static LinkedListNode<T>? NodeAt<T>(this LinkedList<T> collection, Int32 index)
        {
            if (collection is null)
            {
                throw new ArgumentNullException(nameof(collection));
            }

            if (index < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(index), index, null);
            }

            if (index >= collection.Count)
            {
                return null;
            }

            LinkedListNode<T>? current;

            if (index <= collection.Count / 2)
            {
                current = collection.First;
                for (Int32 i = 0; i < index && current is not null; i++)
                {
                    current = current.Next;
                }
            }
            else
            {
                current = collection.Last;
                for (Int32 i = collection.Count - 1; i > index && current is not null; i--)
                {
                    current = current.Previous;
                }
            }

            return current;
        }

        public static LinkedListNode<T>? Offset<T>(this LinkedListNode<T> node, Int32 offset)
        {
            if (node is null)
            {
                throw new ArgumentNullException(nameof(node));
            }

            LinkedListNode<T>? current = node;

            switch (offset)
            {
                case 0:
                    return node;
                case > 0:
                {
                    for (Int32 i = 0; i < offset && current is not null; i++)
                    {
                        current = current.Next;
                    }

                    break;
                }
                default:
                    for (Int32 i = 0; i > offset && current is not null; i--)
                    {
                        current = current.Previous;
                    }

                    break;
            }

            return current;
        }

        public static IEnumerable<LinkedListNode<T>> AsNodeEnumerable<T>(this LinkedList<T> collection)
        {
            if (collection is null)
            {
                throw new ArgumentNullException(nameof(collection));
            }

            LinkedListNode<T>? current = collection.First;
            while (current is not null)
            {
                yield return current;
                current = current.Next;
            }
        }

        public static IEnumerable<LinkedListNode<T>> AsNodeReversedEnumerable<T>(this LinkedList<T> collection)
        {
            if (collection is null)
            {
                throw new ArgumentNullException(nameof(collection));
            }

            LinkedListNode<T>? current = collection.Last;
            while (current is not null)
            {
                yield return current;
                current = current.Previous;
            }
        }

        public static IEnumerator<LinkedListNode<T>> GetNodeEnumerator<T>(this LinkedList<T> collection)
        {
            if (collection is null)
            {
                throw new ArgumentNullException(nameof(collection));
            }

            return AsNodeEnumerable(collection).GetEnumerator();
        }

        public static IEnumerator<LinkedListNode<T>> GetReversedNodeEnumerator<T>(this LinkedList<T> collection)
        {
            if (collection is null)
            {
                throw new ArgumentNullException(nameof(collection));
            }

            return AsNodeReversedEnumerable(collection).GetEnumerator();
        }
    }
}