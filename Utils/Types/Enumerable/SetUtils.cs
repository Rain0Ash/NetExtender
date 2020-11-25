// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using DynamicData.Annotations;
using NetExtender.Types.Sets;
using NetExtender.Types.Sets.Interfaces;

// ReSharper disable StaticMemberInGenericType

namespace NetExtender.Utils.Types
{
    public static class SetUtils
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean IsSet<T>() where T : IEnumerable
        {
            return CacheSet<T>.IsSet;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean IsGenericSet<T>() where T : IEnumerable
        {
            return CacheSet<T>.IsGenericSet;
        }

        private static class CacheSet<T> where T : IEnumerable
        {
            public static readonly Boolean IsSet;
            public static readonly Boolean IsGenericSet;
            
            static CacheSet()
            {
                Type[] interfaces = typeof(T).GetInterfaces();

                IsGenericSet = interfaces.Any(i => i.IsGenericType && i.GetGenericTypeDefinition() == typeof(ISet<>));
                IsSet = IsGenericSet || interfaces.Any(i => i == typeof(ISet));
            }
        }

        public static ISet ToSet<T>([NotNull] ISet<T> set)
        {
            if (set is null)
            {
                throw new ArgumentNullException(nameof(set));
            }
            
            return set as ISet ?? new SetAdapter<T>(set);
        }
        
        public static HashSet<T> ToHashSet<T>([NotNull] this IEnumerable<T> source)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            return source as HashSet<T> ?? new FixedHashSet<T>(source);
        }
        
        public static SortedSet<T> ToSortedSet<T>([NotNull] this IEnumerable<T> source)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            return source as SortedSet<T> ?? new FixedSortedSet<T>(source);
        }
        
        public static OrderedSet<T> ToOrderedSet<T>([NotNull] this IEnumerable<T> source)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            return source as OrderedSet<T> ?? new OrderedSet<T>(source);
        }
        
        public static void ExceptWith<T>(this ISet<T> set, params T[] except)
        {
            if (except.Length <= 0)
            {
                return;
            }
            
            set.ExceptWith(except);
        }
        
        public static void IntersectWith<T>(this ISet<T> set, params T[] intersect)
        {
            set.IntersectWith(intersect);
        }
    }
}