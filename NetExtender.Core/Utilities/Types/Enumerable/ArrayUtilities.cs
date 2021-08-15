// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using NetExtender.Types.Arrays;

namespace NetExtender.Utilities.Types
{
    public static class ArrayUtilities
    {
        /// <summary>
        /// Adds the provided item to the end of the array.
        /// </summary>
        /// <typeparam name="T">Type of the items in the array.</typeparam>
        /// <param name="array">The array to add the item to.</param>
        /// <param name="item">The item to add to the array.</param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T[] Merge<T>(this T[] array, T item)
        {
            if (array is null)
            {
                throw new ArgumentNullException(nameof(array));
            }

            Array.Resize(ref array, array.Length + 1);
            array[^1] = item;
            return array;
        }

        /// <summary>
        /// Adds the provided item to the end of the array.
        /// </summary>
        /// <typeparam name="T">Type of the items in the array.</typeparam>
        /// <param name="array">The array to add the item to.</param>
        /// <param name="item">The item to add to the array.</param>
        /// <param name="items">Other items to add to the array</param>
        public static T[] Merge<T>(this T[] array, T item, params T[]? items)
        {
            if (array is null)
            {
                throw new ArgumentNullException(nameof(array));
            }

            if (items is null || items.Length <= 0)
            {
                return Merge(array, item);
            }

            Int32 length = array.Length;
            Array.Resize(ref array, array.Length + items.Length + 1);

            array[length] = item;
            Array.Copy(items, 0, array, length + 1, items.Length);

            return array;
        }

        /// <summary>
        /// Adds the provided item to the end of the array.
        /// </summary>
        /// <typeparam name="T">Type of the items in the array.</typeparam>
        /// <param name="array">The array to add the item to.</param>
        /// <param name="items">The items to add to the array.</param>
        public static T[] Merge<T>(this T[] array, params T[]? items)
        {
            if (array is null)
            {
                throw new ArgumentNullException(nameof(array));
            }

            if (items is null || items.Length <= 0)
            {
                return array;
            }

            Int32 length = array.Length;
            Array.Resize(ref array, length + items.Length);
            Array.Copy(items, 0, array, length, items.Length);

            return array;
        }
        
        /// <summary>
        /// Adds the provided item to the end of the array.
        /// </summary>
        /// <typeparam name="T">Type of the items in the array.</typeparam>
        /// <param name="array">The array to add the item to.</param>
        /// <param name="items">The items to add to the array.</param>
        public static T[] Merge<T>(this T[] array, params T[][]? items)
        {
            if (array is null)
            {
                throw new ArgumentNullException(nameof(array));
            }

            if (items is null || items.Length <= 0)
            {
                return array;
            }

            Int32 sum = items.WhereNotNull().Sum(item => item.Length);

            Int32 position = array.Length;
            Array.Resize(ref array, array.Length + sum);
            
            foreach (T[] item in items.WhereNotNull())
            {
                Array.Copy(item, 0, array, position, item.Length);
                position += item.Length;
            }
            
            return array;
        }

        public static T[] InnerChange<T>(this T[] array, Func<T, T> selector)
        {
            if (array is null)
            {
                throw new ArgumentNullException(nameof(array));
            }

            if (selector is null)
            {
                throw new ArgumentNullException(nameof(selector));
            }

            for (Int32 i = 0; i < array.Length; i++)
            {
                T item = array[i];
                array[i] = selector(item);
            }

            return array;
        }
        
        public static T[] InnerChangeWhere<T>(this T[] array, Func<T, Boolean> where, Func<T, T> selector)
        {
            if (array is null)
            {
                throw new ArgumentNullException(nameof(array));
            }

            if (where is null)
            {
                throw new ArgumentNullException(nameof(where));
            }

            if (selector is null)
            {
                throw new ArgumentNullException(nameof(selector));
            }
            
            for (Int32 i = 0; i < array.Length; i++)
            {
                T item = array[i];
                
                if (where(item))
                {
                    array[i] = selector(item);
                }
            }

            return array;
        }
        
        public static T[] InnerChangeWhereNot<T>(this T[] array, Func<T, Boolean> where, Func<T, T> selector)
        {
            if (array is null)
            {
                throw new ArgumentNullException(nameof(array));
            }

            if (where is null)
            {
                throw new ArgumentNullException(nameof(where));
            }

            if (selector is null)
            {
                throw new ArgumentNullException(nameof(selector));
            }
            
            for (Int32 i = 0; i < array.Length; i++)
            {
                T item = array[i];
                
                if (!where(item))
                {
                    array[i] = selector(item);
                }
            }

            return array;
        }

        /// <summary>
        /// Performs the specified action for each element in the array. Supports multiple dimensions, the second parameter of the action are current indices for the dimensions.
        /// <para>
        /// To use the indices, you can use the <see cref="Array.GetValue(int[])"/> and <see cref="Array.SetValue(object, int[])"/>.
        /// </para>
        /// </summary>
        /// <param name="array">The array that contains the elements.</param>
        /// <param name="action">An action to invoke for each element. The second parameter is an int array that represents the indexes of the current element for each dimension.</param>
        public static Array ForEach(this Array array, Action<Array, Int32[]> action)
        {
            if (array is null)
            {
                throw new ArgumentNullException(nameof(array));
            }

            if (action is null)
            {
                throw new ArgumentNullException(nameof(action));
            }

            if (array.LongLength == 0)
            {
                return array;
            }

            ArrayTraverse walker = new ArrayTraverse(array);

            do
            {
                action(array, walker.Position);
                
            } while (walker.Step());

            return array;
        }

        private readonly struct ArrayTraverse
        {
            public Int32[] Position { get; }
            private Int32[] Lengths { get; }

            public ArrayTraverse(Array array)
            {
                Position = new Int32[array.Rank];

                Lengths = new Int32[array.Rank];
                for (Int32 i = 0; i < array.Rank; i++)
                {
                    Lengths[i] = array.GetLength(i) - 1;
                }
            }

            public Boolean Step()
            {
                for (Int32 i = 0; i < Position.Length; i++)
                {
                    if (Position[i] >= Lengths[i])
                    {
                        continue;
                    }

                    Position[i]++;
                    for (Int32 j = 0; j < i; j++)
                    {
                        Position[j] = 0;
                    }

                    return true;
                }

                return false;
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void Swap<T>(this T[] source, Int32 first, Int32 second)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            (source[first], source[second]) = (source[second], source[first]);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void Swap<T>(this T[,] source, Int32 x1, Int32 y1, Int32 x2, Int32 y2)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            (source[x1, y1], source[x2, y2]) = (source[x2, y2], source[x1, y1]);
        }

        /// <inheritdoc cref="Array.Clear"/>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void Clear(this Array array)
        {
            Array.Clear(array, 0, array.Length);
        }

        /// <inheritdoc cref="Array.Clear"/>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void Clear(this Array array, Int32 index)
        {
            Array.Clear(array, index, array.Length - index);
        }

        /// <inheritdoc cref="Array.Clear"/>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void Clear(this Array array, Int32 index, Int32 length)
        {
            Array.Clear(array, index, length);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Span<T> Slice<T>(this T[] array, Int32 start)
        {
            if (array is null)
            {
                throw new ArgumentNullException(nameof(array));
            }

            return array.AsSpan().Slice(start);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Span<T> Slice<T>(this T[] array, Int32 start, Int32 length)
        {
            if (array is null)
            {
                throw new ArgumentNullException(nameof(array));
            }

            return array.AsSpan().Slice(start, length);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void Fill<T>(this T[] array, T value)
        {
            if (array is null)
            {
                throw new ArgumentNullException(nameof(array));
            }

            array.AsSpan().Fill(value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void Fill<T>(this T[] array, T value, Int32 start)
        {
            if (array is null)
            {
                throw new ArgumentNullException(nameof(array));
            }

            array.AsSpan().Slice(start).Fill(value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void Fill<T>(this T[] array, T value, Int32 start, Int32 length)
        {
            if (array is null)
            {
                throw new ArgumentNullException(nameof(array));
            }

            array.AsSpan().Slice(start, length).Fill(value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T[] SubArray<T>(this T[] array, Int32 length)
        {
            return SubArray(array, 0, length);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T[] SubArrayFrom<T>(this T[] array, Int32 index)
        {
            if (array is null)
            {
                throw new ArgumentNullException(nameof(array));
            }

            return SubArray(array, index, array.Length - index);
        }

        public static T[] SubArray<T>(this T[] array, Int32 index, Int32 length)
        {
            if (array is null)
            {
                throw new ArgumentNullException(nameof(array));
            }

            if (length < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(length));
            }

            length = (array.Length - index) % (length + 1);

            if (length == 0)
            {
                return Array.Empty<T>();
            }

            T[] result = new T[length];
            Array.Copy(array, index, result, 0, length);
            return result;
        }

        /// <inheritdoc cref="Array.BinarySearch{T}(T[],T)"/>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Int32 BinarySearch<T>(this T[] array, T value)
        {
            return Array.BinarySearch(array, value);
        }

        /// <inheritdoc cref="Array.BinarySearch{T}(T[],T,IComparer{T})"/>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Int32 BinarySearch<T>(this T[] array, T value, IComparer<T>? comparer)
        {
            return Array.BinarySearch(array, value, comparer);
        }

        /// <inheritdoc cref="Array.BinarySearch{T}(T[],Int32,Int32,T)"/>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Int32 BinarySearch<T>(this T[] array, Int32 index, Int32 length, T value)
        {
            return Array.BinarySearch(array, index, length, value);
        }

        /// <inheritdoc cref="Array.BinarySearch{T}(T[],Int32,Int32,T,IComparer{T})"/>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Int32 BinarySearch<T>(this T[] array, Int32 index, Int32 length, T value, IComparer<T>? comparer)
        {
            return Array.BinarySearch(array, index, length, value, comparer);
        }

        /// <inheritdoc cref="Array.ConstrainedCopy"/>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void ConstrainedCopy(this Array array, Int32 index, Array destination, Int32 destindex, Int32 length)
        {
            Array.ConstrainedCopy(array, index, destination, destindex, length);
        }

        /// <inheritdoc cref="Array.ConvertAll{TInput,TOutput}"/>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static TOutput[] ConvertAll<TInput, TOutput>(this TInput[] array, Converter<TInput, TOutput> converter)
        {
            return Array.ConvertAll(array, converter);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T[] ConvertAll<T>(this Object[] array)
        {
            return Array.ConvertAll(array, item => (T) item);
        }

        /// <inheritdoc cref="Array.Copy(System.Array,System.Array,Int32)"/>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void Copy(this Array source, Array destination, Int32 length)
        {
            Array.Copy(source, destination, length);
        }

        /// <inheritdoc cref="Array.Copy(System.Array,System.Array,Int64)"/>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void Copy(this Array source, Array destination, Int64 length)
        {
            Array.Copy(source, destination, length);
        }

        /// <inheritdoc cref="Array.Copy(System.Array,Int32,System.Array,Int32,Int32)"/>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void Copy(this Array source, Int32 index, Array destination, Int32 destindex, Int32 length)
        {
            Array.Copy(source, index, destination, destindex, length);
        }
        
        /// <inheritdoc cref="Array.Copy(System.Array,Int64,System.Array,Int64,Int64)"/>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void Copy(this Array source, Int64 index, Array destination, Int64 destindex, Int64 length)
        {
            Array.Copy(source, index, destination, destindex, length);
        }

        /// <inheritdoc cref="Array.Exists{T}"/>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean Exists<T>(this T[] array, Predicate<T> match)
        {
            return Array.Exists(array, match);
        }

        /// <inheritdoc cref="Array.Find{T}(T[],Predicate{T})"/>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T? Find<T>(this T[] array, Predicate<T> match)
        {
            return Array.Find(array, match);
        }

        /// <inheritdoc cref="Array.FindAll{T}"/>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T[] FindAll<T>(this T[] array, Predicate<T> match)
        {
            return Array.FindAll(array, match);
        }

        /// <inheritdoc cref="Array.FindIndex{T}(T[],Predicate{T})"/>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Int32 FindIndex<T>(this T[] array, Predicate<T> match)
        {
            return Array.FindIndex(array, match);
        }

        /// <inheritdoc cref="Array.FindIndex{T}(T[],Int32,Predicate{T})"/>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Int32 FindIndex<T>(this T[] array, Int32 start, Predicate<T> match)
        {
            return Array.FindIndex(array, start, match);
        }

        /// <inheritdoc cref="Array.FindIndex{T}(T[],Int32,Int32,Predicate{T})"/>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Int32 FindIndex<T>(this T[] array, Int32 start, Int32 count, Predicate<T> match)
        {
            return Array.FindIndex(array, start, count, match);
        }

        /// <inheritdoc cref="Array.FindLast{T}(T[],Predicate{T})"/>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T? FindLast<T>(this T[] array, Predicate<T> match)
        {
            return Array.FindLast(array, match);
        }

        /// <inheritdoc cref="Array.FindLastIndex{T}(T[],Predicate{T})"/>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Int32 FindLastIndex<T>(this T[] array, Predicate<T> match)
        {
            return Array.FindLastIndex(array, match);
        }

        /// <inheritdoc cref="Array.FindLastIndex{T}(T[],Int32,Predicate{T})"/>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Int32 FindLastIndex<T>(this T[] array, Int32 start, Predicate<T> match)
        {
            return Array.FindLastIndex(array, start, match);
        }

        /// <inheritdoc cref="Array.FindLastIndex{T}(T[],Int32,Int32,Predicate{T})"/>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Int32 FindLastIndex<T>(this T[] array, Int32 start, Int32 count, Predicate<T> match)
        {
            return Array.FindLastIndex(array, start, count, match);
        }

        /// <inheritdoc cref="Array.ForEach{T}"/>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T[] ForEach<T>(this T[] array, Action<T> action)
        {
            Array.ForEach(array, action);
            return array;
        }

        /// <inheritdoc cref="Array.IndexOf{T}(T[],T)"/>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Int32 IndexOf<T>(this T[] array, T value)
        {
            return Array.IndexOf(array, value);
        }

        /// <inheritdoc cref="Array.IndexOf{T}(T[],T,Int32)"/>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Int32 IndexOf<T>(this T[] array, T value, Int32 startIndex)
        {
            return Array.IndexOf(array, value, startIndex);
        }

        /// <inheritdoc cref="Array.IndexOf{T}(T[],T,Int32,Int32)"/>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Int32 IndexOf<T>(this T[] array, T value, Int32 startIndex, Int32 count)
        {
            return Array.IndexOf(array, value, startIndex, count);
        }

        /// <inheritdoc cref="Array.LastIndexOf{T}(T[],T)"/>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Int32 LastIndexOf<T>(this T[] array, T value)
        {
            return Array.LastIndexOf(array, value);
        }

        /// <inheritdoc cref="Array.LastIndexOf{T}(T[],T,Int32)"/>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Int32 LastIndexOf<T>(this T[] array, T value, Int32 startIndex)
        {
            return Array.LastIndexOf(array, value, startIndex);
        }

        /// <inheritdoc cref="Array.LastIndexOf{T}(T[],T,Int32,Int32)"/>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Int32 LastIndexOf<T>(this T[] array, T value, Int32 startIndex, Int32 count)
        {
            return Array.LastIndexOf(array, value, startIndex, count);
        }

        /// <inheritdoc cref="Array.Reverse{T}(T[])"/>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T[] InnerReverse<T>(this T[] array)
        {
            Array.Reverse(array);
            return array;
        }

        /// <inheritdoc cref="Array.Reverse{T}(T[],Int32,Int32)"/>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T[] InnerReverse<T>(this T[] array, Int32 index, Int32 length)
        {
            Array.Reverse(array, index, length);
            return array;
        }

        /// <inheritdoc cref="Array.Reverse{T}(T[],Int32,Int32)"/>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T[] ReverseCopy<T>(this T[] array, Int32 index, Int32 length)
        {
            T[] copy = new T[array.Length];
            Array.Copy(array, copy, array.Length);
            Array.Reverse(copy, index, length);
            return copy;
        }

        /// <inheritdoc cref="Array.Sort{T}(T[])"/>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T[] SortArray<T>(this T[] array)
        {
            Array.Sort(array);
            return array;
        }
        
        /// <inheritdoc cref="Array.Sort{T}(T[],IComparer{T})"/>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T[] SortArray<T>(this T[] array, IComparer<T>? comparer)
        {
            Array.Sort(array, comparer);
            return array;
        }

        /// <inheritdoc cref="Array.Sort{T}(T[],Comparison{T})"/>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T[] SortArray<T>(this T[] array, Comparison<T> comparison)
        {
            Array.Sort(array, comparison);
            return array;
        }

        /// <inheritdoc cref="Array.Sort{T}(T[],Int32,Int32)"/>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T[] SortArray<T>(this T[] array, Int32 index, Int32 length)
        {
            Array.Sort(array, index, length);
            return array;
        }

        /// <inheritdoc cref="Array.Sort{T}(T[],Int32,Int32,IComparer{T})"/>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T[] SortArray<T>(this T[] array, Int32 index, Int32 length, IComparer<T>? comparer)
        {
            Array.Sort(array, index, length, comparer);
            return array;
        }

        /// <inheritdoc cref="Array.TrueForAll{T}"/>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean TrueForAll<T>(this T[] array, Predicate<T> match)
        {
            return Array.TrueForAll(array, match);
        }

        /// <summary>
        /// Converts to <see cref="ReadOnlyArray"/>.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="source"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ReadOnlyArray<T> ToReadOnlyArray<T>(this IEnumerable<T> source)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            return source is T[] array ? ToReadOnlyArray(array) : new ReadOnlyArray<T>(source.ToArray());
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ReadOnlyArray<T> AsReadOnlyArray<T>(this IEnumerable<T>? source)
        {
            return source is not null ? source as ReadOnlyArray<T> ?? ToReadOnlyArray(source) : ReadOnlyArray.Empty<T>();
        }

        /// <summary>
        /// Converts to <see cref="ReadOnlyArray{T}"/>.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="source"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ReadOnlyArray<T> ToReadOnlyArray<T>(this T[] source)
        {
            return new ReadOnlyArray<T>(source);
        }

        private const Int32 BoundLength = 5;

        /// <summary>
        /// Returns true, if length and content of <paramref name="first"/> equals <paramref name="second"/>.
        /// </summary>
        /// <param name="first">The first array to compare.</param>
        /// <param name="second">The second array to compare.</param>
        /// <returns>True, if length and content of <paramref name="first"/> equals <paramref name="second"/>.</returns>
        public static unsafe Boolean EqualsTo(this Char[]? first, Char[]? second)
        {
            if (first == second)
            {
                return true;
            }

            if (first is null || second is null)
            {
                return false;
            }

            if (first.Length != second.Length)
            {
                return false;
            }

            if (first.Length <= 0)
            {
                return true;
            }

            if (first.Length < BoundLength)
            {
                return !first.Where((value, i) => value != second[i]).Any();
            }

            fixed (void* pa = &first[0], pb = &second[0])
            {
                return MemoryUtilities.Compare((Byte*) pa, (Byte*) pb, first.Length * sizeof(Char));
            }
        }

        /// <summary>
        /// Returns true, if length and content of <paramref name="first"/> equals <paramref name="second"/>.
        /// </summary>
        /// <param name="first">The first array to compare.</param>
        /// <param name="second">The second array to compare.</param>
        /// <returns>True, if length and content of <paramref name="first"/> equals <paramref name="second"/>.</returns>
        public static unsafe Boolean EqualsTo(this SByte[]? first, SByte[]? second)
        {
            if (first == second)
            {
                return true;
            }

            if (first is null || second is null)
            {
                return false;
            }

            if (first.Length != second.Length)
            {
                return false;
            }

            if (first.Length <= 0)
            {
                return true;
            }

            if (first.Length < BoundLength)
            {
                return !first.Where((value, i) => value != second[i]).Any();
            }

            fixed (void* pa = &first[0], pb = &second[0])
            {
                return MemoryUtilities.Compare((Byte*) pa, (Byte*) pb, first.Length * sizeof(SByte));
            }
        }

        /// <summary>
        /// Returns true, if length and content of <paramref name="first"/> equals <paramref name="second"/>.
        /// </summary>
        /// <param name="first">The first array to compare.</param>
        /// <param name="second">The second array to compare.</param>
        /// <returns>True, if length and content of <paramref name="first"/> equals <paramref name="second"/>.</returns>
        public static unsafe Boolean EqualsTo(this Byte[]? first, Byte[]? second)
        {
            if (first == second)
            {
                return true;
            }

            if (first is null || second is null)
            {
                return false;
            }

            if (first.Length != second.Length)
            {
                return false;
            }

            if (first.Length <= 0)
            {
                return true;
            }

            if (first.Length < BoundLength)
            {
                return !first.Where((value, i) => value != second[i]).Any();
            }

            fixed (void* pa = &first[0], pb = &second[0])
            {
                return MemoryUtilities.Compare((Byte*) pa, (Byte*) pb, first.Length * sizeof(Byte));
            }
        }

        /// <summary>
        /// Returns true, if length and content of <paramref name="first"/> equals <paramref name="second"/>.
        /// </summary>
        /// <param name="first">The first array to compare.</param>
        /// <param name="second">The second array to compare.</param>
        /// <returns>True, if length and content of <paramref name="first"/> equals <paramref name="second"/>.</returns>
        public static unsafe Boolean EqualsTo(this Int16[]? first, Int16[]? second)
        {
            if (first == second)
            {
                return true;
            }

            if (first is null || second is null)
            {
                return false;
            }

            if (first.Length != second.Length)
            {
                return false;
            }

            if (first.Length <= 0)
            {
                return true;
            }

            if (first.Length < BoundLength)
            {
                return !first.Where((value, i) => value != second[i]).Any();
            }

            fixed (void* pa = &first[0], pb = &second[0])
            {
                return MemoryUtilities.Compare((Byte*) pa, (Byte*) pb, first.Length * sizeof(Int16));
            }
        }

        /// <summary>
        /// Returns true, if length and content of <paramref name="first"/> equals <paramref name="second"/>.
        /// </summary>
        /// <param name="first">The first array to compare.</param>
        /// <param name="second">The second array to compare.</param>
        /// <returns>True, if length and content of <paramref name="first"/> equals <paramref name="second"/>.</returns>
        public static unsafe Boolean EqualsTo(this UInt16[]? first, UInt16[]? second)
        {
            if (first == second)
            {
                return true;
            }

            if (first is null || second is null)
            {
                return false;
            }

            if (first.Length != second.Length)
            {
                return false;
            }

            if (first.Length <= 0)
            {
                return true;
            }

            if (first.Length < BoundLength)
            {
                return !first.Where((value, i) => value != second[i]).Any();
            }

            fixed (void* pa = &first[0], pb = &second[0])
            {
                return MemoryUtilities.Compare((Byte*) pa, (Byte*) pb, first.Length * sizeof(UInt16));
            }
        }

        /// <summary>
        /// Returns true, if length and content of <paramref name="first"/> equals <paramref name="second"/>.
        /// </summary>
        /// <param name="first">The first array to compare.</param>
        /// <param name="second">The second array to compare.</param>
        /// <returns>True, if length and content of <paramref name="first"/> equals <paramref name="second"/>.</returns>
        public static unsafe Boolean EqualsTo(this Int32[]? first, Int32[]? second)
        {
            if (first == second)
            {
                return true;
            }

            if (first is null || second is null)
            {
                return false;
            }

            if (first.Length != second.Length)
            {
                return false;
            }

            if (first.Length <= 0)
            {
                return true;
            }

            if (first.Length < BoundLength)
            {
                return !first.Where((value, i) => value != second[i]).Any();
            }

            fixed (void* pa = &first[0], pb = &second[0])
            {
                return MemoryUtilities.Compare((Byte*) pa, (Byte*) pb, first.Length * sizeof(Int32));
            }
        }

        /// <summary>
        /// Returns true, if length and content of <paramref name="first"/> equals <paramref name="second"/>.
        /// </summary>
        /// <param name="first">The first array to compare.</param>
        /// <param name="second">The second array to compare.</param>
        /// <returns>True, if length and content of <paramref name="first"/> equals <paramref name="second"/>.</returns>
        public static unsafe Boolean EqualsTo(this UInt32[]? first, UInt32[]? second)
        {
            if (first == second)
            {
                return true;
            }

            if (first is null || second is null)
            {
                return false;
            }

            if (first.Length != second.Length)
            {
                return false;
            }

            if (first.Length <= 0)
            {
                return true;
            }

            if (first.Length < BoundLength)
            {
                return !first.Where((value, i) => value != second[i]).Any();
            }

            fixed (void* pa = &first[0], pb = &second[0])
            {
                return MemoryUtilities.Compare((Byte*) pa, (Byte*) pb, first.Length * sizeof(UInt32));
            }
        }

        /// <summary>
        /// Returns true, if length and content of <paramref name="first"/> equals <paramref name="second"/>.
        /// </summary>
        /// <param name="first">The first array to compare.</param>
        /// <param name="second">The second array to compare.</param>
        /// <returns>True, if length and content of <paramref name="first"/> equals <paramref name="second"/>.</returns>
        public static unsafe Boolean EqualsTo(this Int64[]? first, Int64[]? second)
        {
            if (first == second)
            {
                return true;
            }

            if (first is null || second is null)
            {
                return false;
            }

            if (first.Length != second.Length)
            {
                return false;
            }

            if (first.Length <= 0)
            {
                return true;
            }

            if (first.Length < BoundLength)
            {
                return !first.Where((value, i) => value != second[i]).Any();
            }

            fixed (void* pa = &first[0], pb = &second[0])
            {
                return MemoryUtilities.Compare((Byte*) pa, (Byte*) pb, first.Length * sizeof(Int64));
            }
        }

        /// <summary>
        /// Returns true, if length and content of <paramref name="first"/> equals <paramref name="second"/>.
        /// </summary>
        /// <param name="first">The first array to compare.</param>
        /// <param name="second">The second array to compare.</param>
        /// <returns>True, if length and content of <paramref name="first"/> equals <paramref name="second"/>.</returns>
        public static unsafe Boolean EqualsTo(this UInt64[]? first, UInt64[]? second)
        {
            if (first == second)
            {
                return true;
            }

            if (first is null || second is null)
            {
                return false;
            }

            if (first.Length != second.Length)
            {
                return false;
            }

            if (first.Length <= 0)
            {
                return true;
            }

            if (first.Length < BoundLength)
            {
                return !first.Where((value, i) => value != second[i]).Any();
            }

            fixed (void* pa = &first[0], pb = &second[0])
            {
                return MemoryUtilities.Compare((Byte*) pa, (Byte*) pb, first.Length * sizeof(UInt64));
            }
        }

        /// <summary>
        /// Returns true, if length and content of <paramref name="first"/> equals <paramref name="second"/>.
        /// </summary>
        /// <param name="first">The first array to compare.</param>
        /// <param name="second">The second array to compare.</param>
        /// <returns>True, if length and content of <paramref name="first"/> equals <paramref name="second"/>.</returns>
        public static unsafe Boolean EqualsTo(this Single[]? first, Single[]? second)
        {
            if (first == second)
            {
                return true;
            }

            if (first is null || second is null)
            {
                return false;
            }

            if (first.Length != second.Length)
            {
                return false;
            }

            if (first.Length <= 0)
            {
                return true;
            }

            if (first.Length < BoundLength)
            {
                return !first.Where((value, i) => Math.Abs(value - second[i]) >= Single.Epsilon).Any();
            }

            fixed (void* pa = &first[0], pb = &second[0])
            {
                return MemoryUtilities.Compare((Byte*) pa, (Byte*) pb, first.Length * sizeof(Single));
            }
        }

        /// <summary>
        /// Returns true, if length and content of <paramref name="first"/> equals <paramref name="second"/>.
        /// </summary>
        /// <param name="first">The first array to compare.</param>
        /// <param name="second">The second array to compare.</param>
        /// <returns>True, if length and content of <paramref name="first"/> equals <paramref name="second"/>.</returns>
        public static unsafe Boolean EqualsTo(this Double[]? first, Double[]? second)
        {
            if (first == second)
            {
                return true;
            }

            if (first is null || second is null)
            {
                return false;
            }

            if (first.Length != second.Length)
            {
                return false;
            }

            if (first.Length <= 0)
            {
                return true;
            }

            if (first.Length < BoundLength)
            {
                return !first.Where((value, i) => Math.Abs(value - second[i]) >= Double.Epsilon).Any();
            }

            fixed (void* pa = &first[0], pb = &second[0])
            {
                return MemoryUtilities.Compare((Byte*) pa, (Byte*) pb, first.Length * sizeof(Double));
            }
        }

        /// <summary>
        /// Returns true, if length and content of <paramref name="first"/> equals <paramref name="second"/>.
        /// </summary>
        /// <param name="first">The first array to compare.</param>
        /// <param name="second">The second array to compare.</param>
        /// <returns>True, if length and content of <paramref name="first"/> equals <paramref name="second"/>.</returns>
        public static unsafe Boolean EqualsTo(this Decimal[]? first, Decimal[]? second)
        {
            if (first == second)
            {
                return true;
            }

            if (first is null || second is null)
            {
                return false;
            }

            if (first.Length != second.Length)
            {
                return false;
            }

            if (first.Length <= 0)
            {
                return true;
            }

            if (first.Length < BoundLength)
            {
                return !first.Where((value, i) => value != second[i]).Any();
            }

            fixed (void* pa = &first[0], pb = &second[0])
            {
                return MemoryUtilities.Compare((Byte*) pa, (Byte*) pb, first.Length * sizeof(Decimal));
            }
        }

        /// <summary>
        /// Returns true, if length and content of <paramref name="first"/> equals <paramref name="second"/>.
        /// </summary>
        /// <param name="first">The first array to compare.</param>
        /// <param name="second">The second array to compare.</param>
        /// <returns>True, if length and content of <paramref name="first"/> equals <paramref name="second"/>.</returns>
        public static unsafe Boolean EqualsTo(this TimeSpan[]? first, TimeSpan[]? second)
        {
            if (first == second)
            {
                return true;
            }

            if (first is null || second is null)
            {
                return false;
            }

            if (first.Length != second.Length)
            {
                return false;
            }

            if (first.Length <= 0)
            {
                return true;
            }

            if (first.Length < BoundLength)
            {
                return !first.Where((value, i) => value != second[i]).Any();
            }

            fixed (void* pa = &first[0], pb = &second[0])
            {
                return MemoryUtilities.Compare((Byte*) pa, (Byte*) pb, first.Length * sizeof(TimeSpan));
            }
        }

        /// <summary>
        /// Returns true, if length and content of <paramref name="first"/> equals <paramref name="second"/>.
        /// </summary>
        /// <param name="first">The first array to compare.</param>
        /// <param name="second">The second array to compare.</param>
        /// <returns>True, if length and content of <paramref name="first"/> equals <paramref name="second"/>.</returns>
        public static unsafe Boolean EqualsTo(this Guid[]? first, Guid[]? second)
        {
            if (first == second)
            {
                return true;
            }

            if (first is null || second is null)
            {
                return false;
            }

            if (first.Length != second.Length)
            {
                return false;
            }

            if (first.Length <= 0)
            {
                return true;
            }

            fixed (void* pa = &first[0], pb = &second[0])
            {
                return MemoryUtilities.Compare((Byte*) pa, (Byte*) pb, first.Length * sizeof(Guid));
            }
        }

        /// <summary>
        /// Returns true, if length and content of <paramref name="first"/> equals <paramref name="second"/>.
        /// </summary>
        /// <param name="first">The first array to compare.</param>
        /// <param name="second">The second array to compare.</param>
        /// <returns>True, if length and content of <paramref name="first"/> equals <paramref name="second"/>.</returns>
        public static unsafe Boolean EqualsTo<T>(this T[]? first, T[]? second) where T : unmanaged
        {
            if (first == second)
            {
                return true;
            }

            if (first is null || second is null)
            {
                return false;
            }

            if (first.Length != second.Length)
            {
                return false;
            }

            if (first.Length <= 0)
            {
                return true;
            }

            fixed (void* pa = &first[0], pb = &second[0])
            {
                return MemoryUtilities.Compare((Byte*) pa, (Byte*) pb, first.Length * sizeof(T));
            }
        }
    }
}