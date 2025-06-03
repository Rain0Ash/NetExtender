// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using NetExtender.Types.Entities;
using NetExtender.Types.Lists.Interfaces;
using NetExtender.Types.Nodes.Interfaces;
using NetExtender.Utilities.Types;

namespace NetExtender.Utilities.Types
{
    public static class LinkedListNodeUtilities
    {
        public static ImmutableDictionary<LinkedList<T>, ImmutableArray<LinkedListNode<T>>> UniqueSort<T>(this IEnumerable<LinkedListNode<T>> source)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }
            
            ConcurrentHashSet<LinkedListNode<T>> unique = new ConcurrentHashSet<LinkedListNode<T>>(source.WhereNotNull());
            Dictionary<LinkedList<T>, List<LinkedListNode<T>>?> group = unique.GroupNotNullBy(static node => node.List).ToDictionary()!;

            // ReSharper disable once LocalFunctionHidesMethod
            static void Sort(LinkedList<T> list, ref List<LinkedListNode<T>>? reference, ConcurrentHashSet<LinkedListNode<T>> unique)
            {
                if (reference is null)
                {
                    return;
                }
                
                List<LinkedListNode<T>> result = new List<LinkedListNode<T>>(reference.Count);
                LinkedListNode<T>? current = list.First;

                while (current is not null && unique.Count > 0)
                {
                    if (unique.Remove(current))
                    {
                        result.Add(current);
                    }

                    current = current.Next;
                }

                reference = result;
            }

            Parallel.ForEach(group.Keys, key => Sort(key, ref CollectionsMarshal.GetValueRefOrNullRef(group, key), unique));
            return group.WhereValueNotNull().ToImmutableDictionary(static pair => pair.Key, static pair => pair.Value.ToImmutableArray());
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static LinkedListNode<T>? Find<T>(this LinkedListNode<T>? source, T value)
        {
            return source?.List?.Find(value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static LinkedListNode<T>? Find<T>(this LinkedListNode<T>? source, T value, IEqualityComparer<T>? comparer)
        {
            return source?.List?.Find(value, comparer);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static LinkedListNode<T>? Find<T>(this LinkedListNode<T>? source, Predicate<T> predicate)
        {
            if (predicate is null)
            {
                throw new ArgumentNullException(nameof(predicate));
            }
            
            return source?.List?.Find(predicate);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static LinkedListNode<T>? Find<T>(this LinkedListNode<T>? source, Predicate<LinkedListNode<T>> predicate)
        {
            if (predicate is null)
            {
                throw new ArgumentNullException(nameof(predicate));
            }
            
            return source?.List?.Find(predicate);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static LinkedListNode<T>? FindLast<T>(this LinkedListNode<T>? source, T value)
        {
            return source?.List?.FindLast(value);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static LinkedListNode<T>? FindLast<T>(this LinkedListNode<T>? source, T value, IEqualityComparer<T>? comparer)
        {
            return source?.List?.FindLast(value, comparer);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static LinkedListNode<T>? FindLast<T>(this LinkedListNode<T>? source, Predicate<T> predicate)
        {
            if (predicate is null)
            {
                throw new ArgumentNullException(nameof(predicate));
            }
            
            return source?.List?.FindLast(predicate);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static LinkedListNode<T>? FindLast<T>(this LinkedListNode<T>? source, Predicate<LinkedListNode<T>> predicate)
        {
            if (predicate is null)
            {
                throw new ArgumentNullException(nameof(predicate));
            }
            
            return source?.List?.FindLast(predicate);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static LinkedListNode<T>? FindPrevious<T>(this LinkedListNode<T>? source, T value)
        {
            return FindPrevious(source, value, null);
        }
        
        public static LinkedListNode<T>? FindPrevious<T>(this LinkedListNode<T>? source, T value, IEqualityComparer<T>? comparer)
        {
            if (source?.Previous is not { } current)
            {
                return null;
            }

            LinkedListNode<T>? last = source?.List?.Last;
            comparer ??= EqualityComparer<T>.Default;

            do
            {
                if (comparer.Equals(current.ValueRef, value))
                {
                    return current;
                }
                
                current = current.Previous;
                
            } while (current is not null && !ReferenceEquals(current, last));

            return null;
        }
        
        public static LinkedListNode<T>? FindPrevious<T>(this LinkedListNode<T>? source, Predicate<T> predicate)
        {
            if (predicate is null)
            {
                throw new ArgumentNullException(nameof(predicate));
            }
            
            if (source?.Previous is not { } current)
            {
                return null;
            }

            LinkedListNode<T>? last = source?.List?.Last;

            do
            {
                if (predicate(current.ValueRef))
                {
                    return current;
                }
                
                current = current.Previous;
                
            } while (current is not null && !ReferenceEquals(current, last));

            return null;
        }
        
        public static LinkedListNode<T>? FindPrevious<T>(this LinkedListNode<T>? source, Predicate<LinkedListNode<T>> predicate)
        {
            if (predicate is null)
            {
                throw new ArgumentNullException(nameof(predicate));
            }
            
            if (source?.Previous is not { } current)
            {
                return null;
            }

            LinkedListNode<T>? last = source?.List?.Last;

            do
            {
                if (predicate(current))
                {
                    return current;
                }
                
                current = current.Previous;
                
            } while (current is not null && !ReferenceEquals(current, last));

            return null;
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static LinkedListNode<T>? FindLastPrevious<T>(this LinkedListNode<T>? source, T value)
        {
            return FindLastPrevious(source, value, null);
        }
        
        public static LinkedListNode<T>? FindLastPrevious<T>(this LinkedListNode<T>? source, T value, IEqualityComparer<T>? comparer)
        {
            if (source?.List?.First is not { } current)
            {
                return null;
            }
            
            comparer ??= EqualityComparer<T>.Default;
            
            do
            {
                if (comparer.Equals(current.ValueRef, value))
                {
                    return current;
                }
                
                current = current.Next;
                
            } while (current is not null && !ReferenceEquals(current, source));
            
            return null;
        }
        
        public static LinkedListNode<T>? FindLastPrevious<T>(this LinkedListNode<T>? source, Predicate<T> predicate)
        {
            if (predicate is null)
            {
                throw new ArgumentNullException(nameof(predicate));
            }
            
            if (source?.List?.First is not { } current)
            {
                return null;
            }
            
            do
            {
                if (predicate(current.ValueRef))
                {
                    return current;
                }
                
                current = current.Next;
                
            } while (current is not null && !ReferenceEquals(current, source));
            
            return null;
        }
        
        public static LinkedListNode<T>? FindLastPrevious<T>(this LinkedListNode<T>? source, Predicate<LinkedListNode<T>> predicate)
        {
            if (predicate is null)
            {
                throw new ArgumentNullException(nameof(predicate));
            }
            
            if (source?.List?.First is not { } current)
            {
                return null;
            }
            
            do
            {
                if (predicate(current))
                {
                    return current;
                }
                
                current = current.Next;
                
            } while (current is not null && !ReferenceEquals(current, source));
            
            return null;
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static LinkedListNode<T>? FindNext<T>(this LinkedListNode<T>? source, T value)
        {
            return FindNext(source, value, null);
        }
        
        public static LinkedListNode<T>? FindNext<T>(this LinkedListNode<T>? source, T value, IEqualityComparer<T>? comparer)
        {
            if (source?.Next is not { } current)
            {
                return null;
            }
            
            LinkedListNode<T>? head = source?.List?.First;
            comparer ??= EqualityComparer<T>.Default;
            
            do
            {
                if (comparer.Equals(current.ValueRef, value))
                {
                    return current;
                }
                
                current = current.Next;

            } while (current is not null && !ReferenceEquals(current, head));
            
            return null;
        }
        
        public static LinkedListNode<T>? FindNext<T>(this LinkedListNode<T>? source, Predicate<T> predicate)
        {
            if (predicate is null)
            {
                throw new ArgumentNullException(nameof(predicate));
            }
            
            if (source?.Next is not { } current)
            {
                return null;
            }
            
            LinkedListNode<T>? head = source?.List?.First;
            
            do
            {
                if (predicate(current.ValueRef))
                {
                    return current;
                }
                
                current = current.Next;

            } while (current is not null && !ReferenceEquals(current, head));
            
            return null;
        }
        
        public static LinkedListNode<T>? FindNext<T>(this LinkedListNode<T>? source, Predicate<LinkedListNode<T>> predicate)
        {
            if (predicate is null)
            {
                throw new ArgumentNullException(nameof(predicate));
            }
            
            if (source?.Next is not { } current)
            {
                return null;
            }
            
            LinkedListNode<T>? head = source?.List?.First;
            
            do
            {
                if (predicate(current))
                {
                    return current;
                }
                
                current = current.Next;

            } while (current is not null && !ReferenceEquals(current, head));
            
            return null;
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static LinkedListNode<T>? FindLastNext<T>(this LinkedListNode<T>? source, T value)
        {
            return FindLastNext(source, value, null);
        }
        
        public static LinkedListNode<T>? FindLastNext<T>(this LinkedListNode<T>? source, T value, IEqualityComparer<T>? comparer)
        {
            if (source?.List?.Last is not { } current)
            {
                return null;
            }
            
            comparer ??= EqualityComparer<T>.Default;
            
            do
            {
                if (comparer.Equals(current.ValueRef, value))
                {
                    return current;
                }
                
                current = current.Previous;
                
            } while (current is not null && !ReferenceEquals(current, source));
            
            return null;
        }
        
        public static LinkedListNode<T>? FindLastNext<T>(this LinkedListNode<T>? source, Predicate<T> predicate)
        {
            if (predicate is null)
            {
                throw new ArgumentNullException(nameof(predicate));
            }
            
            if (source?.List?.Last is not { } current)
            {
                return null;
            }
            
            do
            {
                if (predicate(current.ValueRef))
                {
                    return current;
                }
                
                current = current.Previous;
                
            } while (current is not null && !ReferenceEquals(current, source));
            
            return null;
        }
        
        public static LinkedListNode<T>? FindLastNext<T>(this LinkedListNode<T>? source, Predicate<LinkedListNode<T>> predicate)
        {
            if (predicate is null)
            {
                throw new ArgumentNullException(nameof(predicate));
            }
            
            if (source?.List?.Last is not { } current)
            {
                return null;
            }
            
            do
            {
                if (predicate(current))
                {
                    return current;
                }
                
                current = current.Previous;
                
            } while (current is not null && !ReferenceEquals(current, source));
            
            return null;
        }
        
        public static IEnumerator<T> GetEnumerator<T>(this LinkedListNode<T>? source)
        {
            if (source is null)
            {
                yield break;
            }
            
            LinkedListNode<T>? head = source.List?.First;
            
            do
            {
                yield return source.Value;
                source = source.Next;
            } while (source is not null && !ReferenceEquals(source, head));
        }
    }
}

namespace System.Runtime.InteropServices
{
    public static class LinkedListNodesMarshal
    {
        private static class Storage<TList, TNode> where TList : class, ILinkedContainer where TNode : class, ILinkedNode
        {
            private static Func<TNode, TList>? Getter { get; }
            
            static Storage()
            {
                try
                {
                    Getter = Factory(typeof(TNode))?.Compile();
                }
                catch (Exception)
                {
                    Getter = null;
                }
            }
            
            private static Expression<Func<TNode, TList>>? Factory(Type type)
            {
                if (type is null)
                {
                    throw new ArgumentNullException(nameof(type));
                }
                
                const BindingFlags binding = BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic;
                PropertyInfo? property = type.GetProperties(binding).Where(static property => property.PropertyType.IsAssignableTo(typeof(ILinkedContainer))).MinByOrDefault(static property => property.Name switch
                {
                    nameof(LinkedListNode<Any>.List) => 0,
                    "Container" => 1,
                    _ => 2
                });
                
                FieldInfo? field = type.GetFields(binding).Where(static field => field.FieldType.IsAssignableTo(typeof(ILinkedContainer))).MinByOrDefault(static field => field.Name switch
                {
                    "_list" => 0,
                    "_container" => 1,
                    _ => 2
                });
                
                ParameterExpression instance = Expression.Parameter(typeof(TNode), nameof(instance));
                MemberExpression? expression = property is not null ? Expression.Property(instance, property) : field is not null ? Expression.Field(instance, field) : null;
                return expression is not null ? Expression.Lambda<Func<TNode, TList>>(Expression.Convert(expression, typeof(TList)), instance) : null;
            }
            
            public static TList? Get(TNode node)
            {
                if (node is null)
                {
                    throw new ArgumentNullException(nameof(node));
                }

                try
                {
                    return Getter?.Invoke(node);
                }
                catch (Exception)
                {
                    return null;
                }
            }
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static LinkedListNode<T>? First<T>(this LinkedListNode<T>? node)
        {
            return node?.List?.First;
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static LinkedListNode<T>? Last<T>(this LinkedListNode<T>? node)
        {
            return node?.List?.Last;
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static TList? List<T, TNode, TList>(this ILinkedListNode<T, TNode, TList>? node) where TNode : class, ILinkedListNode<T, TNode, TList> where TList : class, ILinkedList<T, TNode, TList>
        {
            return node?.List;
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ILinkedList<T, TNode>? List<T, TNode>(this ILinkedListNode<T, TNode>? node) where TNode : class, ILinkedListNode<T, TNode>
        {
            return node is not null ? Storage<ILinkedList<T, TNode>, ILinkedListNode<T, TNode>>.Get(node) : null;
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ILinkedList<T>? List<T>(this ILinkedListNode<T>? node)
        {
            return node is not null ? Storage<ILinkedList<T>, ILinkedListNode<T>>.Get(node) : null;
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ILinkedList? List(this ILinkedListNode? node)
        {
            return node is not null ? Storage<ILinkedList, ILinkedListNode>.Get(node) : null;
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ILinkedContainer<TNode>? List<TNode>(this ILinkedNode<TNode>? node) where TNode : class, ILinkedNode<TNode>
        {
            return node is not null ? Storage<ILinkedContainer<TNode>, ILinkedNode<TNode>>.Get(node) : null;
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ILinkedContainer? List(this ILinkedNode? node)
        {
            return node is not null ? Storage<ILinkedContainer, ILinkedNode>.Get(node) : null;
        }
    }
}