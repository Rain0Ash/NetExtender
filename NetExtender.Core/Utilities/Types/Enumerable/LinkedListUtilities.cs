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
                throw new ArgumentOutOfRangeException(nameof(index));
            }

            LinkedListNode<T>? node = collection.NodeAt(index);

            if (node is null)
            {
                throw new ArgumentOutOfRangeException(nameof(index));
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
                throw new ArgumentOutOfRangeException(nameof(index));
            }

            LinkedListNode<T>? node = collection.NodeAt(index);

            if (node is null)
            {
                throw new ArgumentOutOfRangeException(nameof(index));
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
        
        public static void RemoveAt<T>(this LinkedList<T> collection, Int32 index)
        {
            if (collection is null)
            {
                throw new ArgumentNullException(nameof(collection));
            }

            if (index < 0 || index >= collection.Count)
            {
                throw new ArgumentNullException(nameof(index));
            }

            LinkedListNode<T>? node = collection.NodeAt(index);

            if (node is null)
            {
                throw new ArgumentOutOfRangeException(nameof(index));
            }
            
            collection.Remove(node);
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
                throw new ArgumentOutOfRangeException(nameof(index1));
            }
            
            if (index2 < 0 || index2 >= collection.Count)
            {
                throw new ArgumentOutOfRangeException(nameof(index2));
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
                throw new ArgumentOutOfRangeException(nameof(index));
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