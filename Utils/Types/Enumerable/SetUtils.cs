// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Runtime.CompilerServices;
using JetBrains.Annotations;
using NetExtender.Types.Sets;
using NetExtender.Types.Sets.Interfaces;
using NetExtender.Utils.Numerics;

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

        public static HashSet<T> ToHashSet<T>([NotNull] IEnumerable<T> source)
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

        public static void ExceptWith<T>(this ISet<T> set, [NotNull] params T[] except)
        {
            if (except is null)
            {
                throw new ArgumentNullException(nameof(except));
            }

            if (except.Length <= 0)
            {
                return;
            }

            set.ExceptWith(except);
        }

        public static void IntersectWith<T>(this ISet<T> set, [NotNull] params T[] intersect)
        {
            if (intersect is null)
            {
                throw new ArgumentNullException(nameof(intersect));
            }

            set.IntersectWith(intersect);
        }

        public static T NearestLower<T>([NotNull] this SortedSet<T> set, T value)
        {
            return NearestLower(set, value, out T result) ? result : default;
        }
        
        public static IEnumerable<T> NearestsLower<T>([NotNull] this SortedSet<T> set, T value)
        {
            if (set is null)
            {
                throw new ArgumentNullException(nameof(set));
            }

            return NearestsLower(set, value, set.Comparer);
        }
        
        public static IEnumerable<T> NearestsLower<T>([NotNull] this SortedSet<T> set, T value, Int32 count)
        {
            return NearestsLower(set, value).Take(count);
        }

        public static IEnumerable<T> NearestsLower<T>([NotNull] this SortedSet<T> set, T value, IComparer<T> comparer)
        {
            if (set is null)
            {
                throw new ArgumentNullException(nameof(set));
            }

            comparer ??= set.Comparer;

            return set.Where(item => comparer.Compare(item, value) < 0);
        }
        
        public static IEnumerable<T> NearestsLower<T>([NotNull] this SortedSet<T> set, T value, IComparer<T> comparer, Int32 count)
        {
            return NearestsLower(set, value, comparer).Take(count);
        }

        public static Boolean NearestLower<T>([NotNull] this SortedSet<T> set, T value, out T result)
        {
            if (set is null)
            {
                throw new ArgumentNullException(nameof(set));
            }

            return NearestLower(set, value, set.Comparer, out result);
        }

        public static T NearestLower<T>([NotNull] this SortedSet<T> set, T value, IComparer<T> comparer)
        {
            return NearestLower(set, value, comparer, out T result) ? result : default;
        }

        public static Boolean NearestLower<T>([NotNull] this SortedSet<T> set, T value, IComparer<T> comparer, out T result)
        {
            if (set is null)
            {
                throw new ArgumentNullException(nameof(set));
            }

            comparer ??= set.Comparer;

            switch (set.Count)
            {
                case 0:
                    result = default;
                    return false;
                case 1:
                    result = set.First();
                    return true;
                default:
                    return set.TryGetLast(item => comparer.Compare(item, value) < 0, out result);
            }
        }
        
        public static IEnumerable<T> NearestsHigher<T>([NotNull] this SortedSet<T> set, T value)
        {
            if (set is null)
            {
                throw new ArgumentNullException(nameof(set));
            }

            return NearestsHigher(set, value, set.Comparer);
        }
        
        public static IEnumerable<T> NearestsHigher<T>([NotNull] this SortedSet<T> set, T value, Int32 count)
        {
            return NearestsHigher(set, value).Take(count);
        }

        public static IEnumerable<T> NearestsHigher<T>([NotNull] this SortedSet<T> set, T value, IComparer<T> comparer)
        {
            if (set is null)
            {
                throw new ArgumentNullException(nameof(set));
            }

            comparer ??= set.Comparer;

            return set.Where(item => comparer.Compare(item, value) > 0);
        }
        
        public static IEnumerable<T> NearestsHigher<T>([NotNull] this SortedSet<T> set, T value, IComparer<T> comparer, Int32 count)
        {
            return NearestsHigher(set, value, comparer).Take(count);
        }

        public static T NearestHigher<T>([NotNull] this SortedSet<T> set, T value)
        {
            return NearestHigher(set, value, out T result) ? result : default;
        }
        
        public static Boolean NearestHigher<T>([NotNull] this SortedSet<T> set, T value, out T result)
        {
            if (set is null)
            {
                throw new ArgumentNullException(nameof(set));
            }

            return NearestHigher(set, value, set.Comparer, out result);
        }

        public static T NearestHigher<T>([NotNull] this SortedSet<T> set, T value, IComparer<T> comparer)
        {
            return NearestHigher(set, value, comparer, out T result) ? result : default;
        }

        public static Boolean NearestHigher<T>([NotNull] this SortedSet<T> set, T value, IComparer<T> comparer, out T result)
        {
            if (set is null)
            {
                throw new ArgumentNullException(nameof(set));
            }

            comparer ??= set.Comparer;

            switch (set.Count)
            {
                case 0:
                    result = default;
                    return false;
                case 1:
                    result = set.First();
                    return true;
                default:
                    return set.TryGetFirst(item => comparer.Compare(item, value) > 0, out result);
            }
        }

        public static (T, T) Nearest<T>([NotNull] this SortedSet<T> set, T value)
        {
            return Nearest(set, value, out _);
        }

        public static (T, T) Nearest<T>([NotNull] this SortedSet<T> set, T value, out MathPositionType result)
        {
            if (set is null)
            {
                throw new ArgumentNullException(nameof(set));
            }

            return Nearest(set, value, set.Comparer, out result);
        }

        public static (T, T) Nearest<T>([NotNull] this SortedSet<T> set, T value, IComparer<T> comparer)
        {
            return Nearest(set, value, comparer, out _);
        }

        public static (T, T) Nearest<T>([NotNull] this SortedSet<T> set, T value, IComparer<T> comparer, out MathPositionType result)
        {
            if (set is null)
            {
                throw new ArgumentNullException(nameof(set));
            }

            comparer ??= set.Comparer;

            if (set.Count <= 0)
            {
                result = MathPositionType.None;
                return default;
            }
            
            Boolean first = set.TryGetLast(item => comparer.Compare(item, value) < 0, out T left);
            Boolean second = set.TryGetFirst(item => comparer.Compare(item, value) > 0, out T right);

            result = first switch
            {
                true when second => MathPositionType.Both,
                true => MathPositionType.Left,
                _ => second ? MathPositionType.Right : MathPositionType.None
            };

            return (left, right);
        }
        
        public static IEnumerable<T> NearestsLower<T>([NotNull] this ImmutableSortedSet<T> set, T value)
        {
            if (set is null)
            {
                throw new ArgumentNullException(nameof(set));
            }

            return NearestsLower(set, value, set.KeyComparer);
        }
        
        public static IEnumerable<T> NearestsLower<T>([NotNull] this ImmutableSortedSet<T> set, T value, Int32 count)
        {
            return NearestsLower(set, value).Take(count);
        }

        public static IEnumerable<T> NearestsLower<T>([NotNull] this ImmutableSortedSet<T> set, T value, IComparer<T> comparer)
        {
            if (set is null)
            {
                throw new ArgumentNullException(nameof(set));
            }

            comparer ??= set.KeyComparer;

            return set.Where(item => comparer.Compare(item, value) < 0);
        }
        
        public static IEnumerable<T> NearestsLower<T>([NotNull] this ImmutableSortedSet<T> set, T value, IComparer<T> comparer, Int32 count)
        {
            return NearestsLower(set, value, comparer).Take(count);
        }

        public static Boolean NearestLower<T>([NotNull] this ImmutableSortedSet<T> set, T value, out T result)
        {
            if (set is null)
            {
                throw new ArgumentNullException(nameof(set));
            }

            return NearestLower(set, value, set.KeyComparer, out result);
        }

        public static T NearestLower<T>([NotNull] this ImmutableSortedSet<T> set, T value, IComparer<T> comparer)
        {
            return NearestLower(set, value, comparer, out T result) ? result : default;
        }

        public static Boolean NearestLower<T>([NotNull] this ImmutableSortedSet<T> set, T value, IComparer<T> comparer, out T result)
        {
            if (set is null)
            {
                throw new ArgumentNullException(nameof(set));
            }

            comparer ??= set.KeyComparer;

            switch (set.Count)
            {
                case 0:
                    result = default;
                    return false;
                case 1:
                    result = set.First();
                    return true;
                default:
                    return set.TryGetLast(item => comparer.Compare(item, value) < 0, out result);
            }
        }
        
        public static IEnumerable<T> NearestsHigher<T>([NotNull] this ImmutableSortedSet<T> set, T value)
        {
            if (set is null)
            {
                throw new ArgumentNullException(nameof(set));
            }

            return NearestsHigher(set, value, set.KeyComparer);
        }
        
        public static IEnumerable<T> NearestsHigher<T>([NotNull] this ImmutableSortedSet<T> set, T value, Int32 count)
        {
            return NearestsHigher(set, value).Take(count);
        }

        public static IEnumerable<T> NearestsHigher<T>([NotNull] this ImmutableSortedSet<T> set, T value, IComparer<T> comparer)
        {
            if (set is null)
            {
                throw new ArgumentNullException(nameof(set));
            }

            comparer ??= set.KeyComparer;

            return set.Where(item => comparer.Compare(item, value) > 0);
        }
        
        public static IEnumerable<T> NearestsHigher<T>([NotNull] this ImmutableSortedSet<T> set, T value, IComparer<T> comparer, Int32 count)
        {
            return NearestsHigher(set, value, comparer).Take(count);
        }

        public static T NearestHigher<T>([NotNull] this ImmutableSortedSet<T> set, T value)
        {
            return NearestHigher(set, value, out T result) ? result : default;
        }
        
        public static Boolean NearestHigher<T>([NotNull] this ImmutableSortedSet<T> set, T value, out T result)
        {
            if (set is null)
            {
                throw new ArgumentNullException(nameof(set));
            }

            return NearestHigher(set, value, set.KeyComparer, out result);
        }

        public static T NearestHigher<T>([NotNull] this ImmutableSortedSet<T> set, T value, IComparer<T> comparer)
        {
            return NearestHigher(set, value, comparer, out T result) ? result : default;
        }

        public static Boolean NearestHigher<T>([NotNull] this ImmutableSortedSet<T> set, T value, IComparer<T> comparer, out T result)
        {
            if (set is null)
            {
                throw new ArgumentNullException(nameof(set));
            }

            comparer ??= set.KeyComparer;

            switch (set.Count)
            {
                case 0:
                    result = default;
                    return false;
                case 1:
                    result = set.First();
                    return true;
                default:
                    return set.TryGetFirst(item => comparer.Compare(item, value) > 0, out result);
            }
        }

        public static (T, T) Nearest<T>([NotNull] this ImmutableSortedSet<T> set, T value)
        {
            return Nearest(set, value, out _);
        }

        public static (T, T) Nearest<T>([NotNull] this ImmutableSortedSet<T> set, T value, out MathPositionType result)
        {
            if (set is null)
            {
                throw new ArgumentNullException(nameof(set));
            }

            return Nearest(set, value, set.KeyComparer, out result);
        }

        public static (T, T) Nearest<T>([NotNull] this ImmutableSortedSet<T> set, T value, IComparer<T> comparer)
        {
            return Nearest(set, value, comparer, out _);
        }

        public static (T, T) Nearest<T>([NotNull] this ImmutableSortedSet<T> set, T value, IComparer<T> comparer, out MathPositionType result)
        {
            if (set is null)
            {
                throw new ArgumentNullException(nameof(set));
            }

            comparer ??= set.KeyComparer;

            if (set.Count <= 0)
            {
                result = MathPositionType.None;
                return default;
            }
            
            Boolean first = set.TryGetLast(item => comparer.Compare(item, value) < 0, out T left);
            Boolean second = set.TryGetFirst(item => comparer.Compare(item, value) > 0, out T right);

            result = first switch
            {
                true when second => MathPositionType.Both,
                true => MathPositionType.Left,
                _ => second ? MathPositionType.Right : MathPositionType.None
            };

            return (left, right);
        }

        //TODO:
        /*
        public static Decimal Interpolate([NotNull] this SortedSet<Decimal> set)
        {
            if (set is null)
            {
                throw new ArgumentNullException(nameof(set));
            }

            return set.Count switch
            {
                0 => 0,
                1 => set.First(),
                
            };
        }
        */
    }
}