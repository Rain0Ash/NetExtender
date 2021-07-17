// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using NetExtender.Types.Sets;
using NetExtender.Types.Sets.Interfaces;
using NetExtender.Utils.Numerics;

namespace NetExtender.Utils.Types
{
    public static class SetUtils
    {
        public static ISet ToSet<T>(ISet<T> set)
        {
            if (set is null)
            {
                throw new ArgumentNullException(nameof(set));
            }

            return set as ISet ?? new SetAdapter<T>(set);
        }

        public static HashSet<T> ToHashSet<T>(IEnumerable<T> source)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            return new HashSetExtended<T>(source);
        }
        
        public static HashSet<T> ToHashSet<T>(IEnumerable<T> source, IEqualityComparer<T> comparer)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            return new HashSetExtended<T>(source, comparer);
        }
        
        public static HashSet<T> AsHashSet<T>(IEnumerable<T> source)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            return source as HashSet<T> ?? new HashSetExtended<T>(source);
        }
        
        public static HashSet<T> AsHashSet<T>(IEnumerable<T> source, IEqualityComparer<T> comparer)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            return source as HashSet<T> ?? new HashSetExtended<T>(source, comparer);
        }

        public static SortedSet<T> ToSortedSet<T>(this IEnumerable<T> source)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            return new SortedSetExtended<T>(source);
        }
        
        public static SortedSet<T> ToSortedSet<T>(this IEnumerable<T> source, IComparer<T>? comparer)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            return new SortedSetExtended<T>(source, comparer);
        }
        
        public static SortedSet<T> AsSortedSet<T>(this IEnumerable<T> source)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            return source as SortedSet<T> ?? new SortedSetExtended<T>(source);
        }
        
        public static SortedSet<T> AsSortedSet<T>(this IEnumerable<T> source, IComparer<T>? comparer)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            return source as SortedSet<T> ?? new SortedSetExtended<T>(source, comparer);
        }

        public static OrderedSet<T> ToOrderedSet<T>(this IEnumerable<T> source)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            return new OrderedSet<T>(source);
        }
        
        public static OrderedSet<T> ToOrderedSet<T>(this IEnumerable<T> source, IEqualityComparer<T> comparer)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            return new OrderedSet<T>(source, comparer);
        }
        
        public static OrderedSet<T> AsOrderedSet<T>(this IEnumerable<T> source)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            return source as OrderedSet<T> ?? new OrderedSet<T>(source);
        }
        
        public static OrderedSet<T> AsOrderedSet<T>(this IEnumerable<T> source, IEqualityComparer<T> comparer)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            return source as OrderedSet<T> ?? new OrderedSet<T>(source, comparer);
        }

        public static void ExceptWith<T>(this ISet<T> set, params T[] except)
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

        public static void IntersectWith<T>(this ISet<T> set, params T[] intersect)
        {
            if (intersect is null)
            {
                throw new ArgumentNullException(nameof(intersect));
            }

            set.IntersectWith(intersect);
        }

        public static T? NearestLower<T>(this SortedSet<T> set, T value)
        {
            return NearestLower(set, value, out T? result) ? result : default;
        }
        
        public static IEnumerable<T> NearestsLower<T>(this SortedSet<T> set, T value)
        {
            if (set is null)
            {
                throw new ArgumentNullException(nameof(set));
            }

            return NearestsLower(set, value, set.Comparer);
        }
        
        public static IEnumerable<T> NearestsLower<T>(this SortedSet<T> set, T value, Int32 count)
        {
            return NearestsLower(set, value).Take(count);
        }

        public static IEnumerable<T> NearestsLower<T>(this SortedSet<T> set, T value, IComparer<T>? comparer)
        {
            if (set is null)
            {
                throw new ArgumentNullException(nameof(set));
            }

            comparer ??= set.Comparer;

            return set.Where(item => comparer.Compare(item, value) < 0);
        }
        
        public static IEnumerable<T> NearestsLower<T>(this SortedSet<T> set, T value, IComparer<T>? comparer, Int32 count)
        {
            return NearestsLower(set, value, comparer).Take(count);
        }

        public static Boolean NearestLower<T>(this SortedSet<T> set, T value, [MaybeNullWhen(false)] out T result)
        {
            if (set is null)
            {
                throw new ArgumentNullException(nameof(set));
            }

            return NearestLower(set, value, set.Comparer, out result);
        }

        public static T? NearestLower<T>(this SortedSet<T> set, T value, IComparer<T>? comparer)
        {
            return NearestLower(set, value, comparer, out T? result) ? result : default;
        }

        public static Boolean NearestLower<T>(this SortedSet<T> set, T value, IComparer<T>? comparer, [MaybeNullWhen(false)] out T result)
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
        
        public static IEnumerable<T> NearestsHigher<T>(this SortedSet<T> set, T value)
        {
            if (set is null)
            {
                throw new ArgumentNullException(nameof(set));
            }

            return NearestsHigher(set, value, set.Comparer);
        }
        
        public static IEnumerable<T> NearestsHigher<T>(this SortedSet<T> set, T value, Int32 count)
        {
            return NearestsHigher(set, value).Take(count);
        }

        public static IEnumerable<T> NearestsHigher<T>(this SortedSet<T> set, T value, IComparer<T>? comparer)
        {
            if (set is null)
            {
                throw new ArgumentNullException(nameof(set));
            }

            comparer ??= set.Comparer;

            return set.Where(item => comparer.Compare(item, value) > 0);
        }
        
        public static IEnumerable<T> NearestsHigher<T>(this SortedSet<T> set, T value, IComparer<T>? comparer, Int32 count)
        {
            return NearestsHigher(set, value, comparer).Take(count);
        }

        public static T? NearestHigher<T>(this SortedSet<T> set, T value)
        {
            return NearestHigher(set, value, out T? result) ? result : default;
        }
        
        public static Boolean NearestHigher<T>(this SortedSet<T> set, T value, [MaybeNullWhen(false)] out T result)
        {
            if (set is null)
            {
                throw new ArgumentNullException(nameof(set));
            }

            return NearestHigher(set, value, set.Comparer, out result);
        }

        public static T? NearestHigher<T>(this SortedSet<T> set, T value, IComparer<T>? comparer)
        {
            return NearestHigher(set, value, comparer, out T? result) ? result : default;
        }

        public static Boolean NearestHigher<T>(this SortedSet<T> set, T value, IComparer<T>? comparer, [MaybeNullWhen(false)] out T result)
        {
            if (set is null)
            {
                throw new ArgumentNullException(nameof(set));
            }

            switch (set.Count)
            {
                case 0:
                    result = default;
                    return false;
                case 1:
                    result = set.First();
                    return true;
                default:
                    comparer ??= set.Comparer;
                    return set.TryGetFirst(item => comparer.Compare(item, value) > 0, out result);
            }
        }

        public static (T?, T?) Nearest<T>(this SortedSet<T> set, T value)
        {
            return Nearest(set, value, out _);
        }

        public static (T?, T?) Nearest<T>(this SortedSet<T> set, T value, out MathPositionType result)
        {
            if (set is null)
            {
                throw new ArgumentNullException(nameof(set));
            }

            return Nearest(set, value, set.Comparer, out result);
        }

        public static (T?, T?) Nearest<T>(this SortedSet<T> set, T value, IComparer<T>? comparer)
        {
            return Nearest(set, value, comparer, out _);
        }

        public static (T?, T?) Nearest<T>(this SortedSet<T> set, T value, IComparer<T>? comparer, out MathPositionType result)
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
            
            Boolean first = set.TryGetLast(item => comparer.Compare(item, value) < 0, out T? left);
            Boolean second = set.TryGetFirst(item => comparer.Compare(item, value) > 0, out T? right);

            result = first switch
            {
                true when second => MathPositionType.Both,
                true => MathPositionType.Left,
                _ => second ? MathPositionType.Right : MathPositionType.None
            };

            return (left, right);
        }
        
        public static IEnumerable<T> NearestsLower<T>(this ImmutableSortedSet<T> set, T value)
        {
            if (set is null)
            {
                throw new ArgumentNullException(nameof(set));
            }

            return NearestsLower(set, value, set.KeyComparer);
        }
        
        public static IEnumerable<T> NearestsLower<T>(this ImmutableSortedSet<T> set, T value, Int32 count)
        {
            return NearestsLower(set, value).Take(count);
        }

        public static IEnumerable<T> NearestsLower<T>(this ImmutableSortedSet<T> set, T value, IComparer<T>? comparer)
        {
            if (set is null)
            {
                throw new ArgumentNullException(nameof(set));
            }

            comparer ??= set.KeyComparer;

            return set.Where(item => comparer.Compare(item, value) < 0);
        }
        
        public static IEnumerable<T> NearestsLower<T>(this ImmutableSortedSet<T> set, T value, IComparer<T>? comparer, Int32 count)
        {
            return NearestsLower(set, value, comparer).Take(count);
        }

        public static Boolean NearestLower<T>(this ImmutableSortedSet<T> set, T value, [MaybeNullWhen(false)] out T result)
        {
            if (set is null)
            {
                throw new ArgumentNullException(nameof(set));
            }

            return NearestLower(set, value, set.KeyComparer, out result);
        }

        public static T? NearestLower<T>(this ImmutableSortedSet<T> set, T value, IComparer<T>? comparer)
        {
            return NearestLower(set, value, comparer, out T? result) ? result : default;
        }

        public static Boolean NearestLower<T>(this ImmutableSortedSet<T> set, T value, IComparer<T>? comparer, [MaybeNullWhen(false)] out T result)
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
        
        public static IEnumerable<T> NearestsHigher<T>(this ImmutableSortedSet<T> set, T value)
        {
            if (set is null)
            {
                throw new ArgumentNullException(nameof(set));
            }

            return NearestsHigher(set, value, set.KeyComparer);
        }
        
        public static IEnumerable<T> NearestsHigher<T>(this ImmutableSortedSet<T> set, T value, Int32 count)
        {
            return NearestsHigher(set, value).Take(count);
        }

        public static IEnumerable<T> NearestsHigher<T>(this ImmutableSortedSet<T> set, T value, IComparer<T>? comparer)
        {
            if (set is null)
            {
                throw new ArgumentNullException(nameof(set));
            }

            comparer ??= set.KeyComparer;

            return set.Where(item => comparer.Compare(item, value) > 0);
        }
        
        public static IEnumerable<T> NearestsHigher<T>(this ImmutableSortedSet<T> set, T value, IComparer<T>? comparer, Int32 count)
        {
            return NearestsHigher(set, value, comparer).Take(count);
        }

        public static T? NearestHigher<T>(this ImmutableSortedSet<T> set, T value)
        {
            return NearestHigher(set, value, out T? result) ? result : default;
        }
        
        public static Boolean NearestHigher<T>(this ImmutableSortedSet<T> set, T value, [MaybeNullWhen(false)] out T result)
        {
            if (set is null)
            {
                throw new ArgumentNullException(nameof(set));
            }

            return NearestHigher(set, value, set.KeyComparer, out result);
        }

        public static T? NearestHigher<T>(this ImmutableSortedSet<T> set, T value, IComparer<T>? comparer)
        {
            return NearestHigher(set, value, comparer, out T? result) ? result : default;
        }

        public static Boolean NearestHigher<T>(this ImmutableSortedSet<T> set, T value, IComparer<T>? comparer, [MaybeNullWhen(false)] out T result)
        {
            if (set is null)
            {
                throw new ArgumentNullException(nameof(set));
            }

            switch (set.Count)
            {
                case 0:
                    result = default;
                    return false;
                case 1:
                    result = set.First();
                    return true;
                default:
                    comparer ??= set.KeyComparer;
                    return set.TryGetFirst(item => comparer.Compare(item, value) > 0, out result);
            }
        }

        public static (T?, T?) Nearest<T>(this ImmutableSortedSet<T> set, T value)
        {
            return Nearest(set, value, out _);
        }

        public static (T?, T?) Nearest<T>(this ImmutableSortedSet<T> set, T value, out MathPositionType result)
        {
            if (set is null)
            {
                throw new ArgumentNullException(nameof(set));
            }

            return Nearest(set, value, set.KeyComparer, out result);
        }

        public static (T?, T?) Nearest<T>(this ImmutableSortedSet<T> set, T value, IComparer<T>? comparer)
        {
            return Nearest(set, value, comparer, out _);
        }

        public static (T?, T?) Nearest<T>(this ImmutableSortedSet<T> set, T value, IComparer<T>? comparer, out MathPositionType result)
        {
            if (set is null)
            {
                throw new ArgumentNullException(nameof(set));
            }

            if (set.Count <= 0)
            {
                result = MathPositionType.None;
                return default;
            }
            
            comparer ??= set.KeyComparer;
            
            Boolean first = set.TryGetLast(item => comparer.Compare(item, value) < 0, out T? left);
            Boolean second = set.TryGetFirst(item => comparer.Compare(item, value) > 0, out T? right);

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
        public static Decimal Interpolate(this SortedSet<Decimal> set)
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