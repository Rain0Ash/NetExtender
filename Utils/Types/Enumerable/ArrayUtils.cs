// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Collections.Generic;
using System.Linq;
using DynamicData.Annotations;
using NetExtender.Types.Arrays;

namespace NetExtender.Utils.Types
{
    public static partial class ArrayUtils
    {
        /// <summary>
        /// Adds the provided item to the end of the array.
        /// </summary>
        /// <typeparam name="T">Type of the items in the array.</typeparam>
        /// <param name="array">The array to add the item to.</param>
        /// <param name="item">The item to add to the array.</param>
        public static T[] Add<T>(ref T[] array, T item)
        {
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
        public static T[] Add<T>(ref T[] array, T item, params T[] items)
        {
            return Add(ref array, item.ParamsAppend(items));
        }

        /// <summary>
        /// Adds the provided item to the end of the array.
        /// </summary>
        /// <typeparam name="T">Type of the items in the array.</typeparam>
        /// <param name="array">The array to add the item to.</param>
        /// <param name="items">The items to add to the array.</param>
        public static T[] Add<T>(ref T[] array, T[] items)
        {
            if (items.Length <= 0)
            {
                return array;
            }

            Array.Resize(ref array, array.Length + items.Length);

            for (Int32 i = 0; i < items.Length; i++)
            {
                array[array.Length + i] = items[i];
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
        public static void ForEach(this Array array, Action<Array, Int32[]> action)
        {
            if (array.LongLength == 0)
            {
                return;
            }

            ArrayTraverse walker = new ArrayTraverse(array);

            do
            {
                action(array, walker.Position);
            } while (walker.Step());
        }

        private class ArrayTraverse
        {
            public readonly Int32[] Position;
            private readonly Int32[] _lengths;

            public ArrayTraverse(Array array)
            {
                Position = new Int32[array.Rank];

                _lengths = new Int32[array.Rank];
                for (Int32 i = 0; i < array.Rank; i++)
                {
                    _lengths[i] = array.GetLength(i) - 1;
                }
            }

            public Boolean Step()
            {
                for (Int32 i = 0; i < Position.Length; i++)
                {
                    if (Position[i] >= _lengths[i])
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

        public static void Swap<T>(ref T[] source, Int32 index1, Int32 index2)
        {
            T temp = source[index1];
            source[index1] = source[index2];
            source[index2] = temp;
        }

        public static void Swap<T>(ref T[,] source, Int32 index1, Int32 y1, Int32 x2, Int32 y2)
        {
            T temp = source[index1, y1];
            source[index1, y1] = source[x2, y2];
            source[x2, y2] = temp;
        }
        
        /// <summary>Sets all elements in an <see cref="Array" /> to the default value of each element type.</summary>
        /// <param name="array">The <see cref="Array" /> whose elements need to be cleared.</param>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="array" /> is null.</exception>
        /// <filterpriority>1</filterpriority>
        public static void Clear([NotNull] this Array array)
        {
            Array.Clear(array, 0, array.Length);
        }
        
        /// <summary>Sets a range of elements in the <see cref="Array" /> to zero, to false, or to null, depending on the element type.</summary>
        /// <param name="array">The <see cref="Array" /> whose elements need to be cleared.</param>
        /// <param name="index">The starting index of the range of elements to clear.</param>
        /// <param name="length">The number of elements to clear.</param>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="array" /> is null.</exception>
        /// <exception cref="IndexOutOfRangeException">
        /// <paramref name="index" /> is less than the lower bound of <paramref name="array" />.-or-<paramref name="length" /> is less than zero.-or-The sum of <paramref name="index" /> and <paramref name="length" /> is greater than the size of the <see cref="Array" />.</exception>
        /// <filterpriority>1</filterpriority>
        public static void Clear([NotNull] this Array array, Int32 index)
        {
            Array.Clear(array, index, array.Length - index);
        }

        /// <summary>Sets a range of elements in the <see cref="Array" /> to zero, to false, or to null, depending on the element type.</summary>
        /// <param name="array">The <see cref="Array" /> whose elements need to be cleared.</param>
        /// <param name="index">The starting index of the range of elements to clear.</param>
        /// <param name="length">The number of elements to clear.</param>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="array" /> is null.</exception>
        /// <exception cref="IndexOutOfRangeException">
        /// <paramref name="index" /> is less than the lower bound of <paramref name="array" />.-or-<paramref name="length" /> is less than zero.-or-The sum of <paramref name="index" /> and <paramref name="length" /> is greater than the size of the <see cref="Array" />.</exception>
        /// <filterpriority>1</filterpriority>
        public static void Clear([NotNull] this Array array, Int32 index, Int32 length)
        {
            Array.Clear(array, index, length);
        }
        
        public static Span<T> Slice<T>(this T[] array, Int32 start)
        {
            return array.AsSpan().Slice(start);
        }
        
        public static Span<T> Slice<T>(this T[] array, Int32 start, Int32 length)
        {
            return array.AsSpan().Slice(start, length);
        }

        public static void Fill<T>(this T[] array, T value)
        {
            array.AsSpan().Fill(value);
        }
        
        public static void Fill<T>(this T[] array, T value, Int32 start)
        {
            array.AsSpan().Slice(start).Fill(value);
        }
        
        public static void Fill<T>(this T[] array, T value, Int32 start, Int32 length)
        {
            array.AsSpan().Slice(start, length).Fill(value);
        }

        public static T[] SubArray<T>(this T[] array, Int32 length)
        {
            return SubArray(array, 0, length);
        }

        public static T[] SubArrayFrom<T>(this T[] array, Int32 index)
        {
            return SubArray(array, index, array.Length - index);
        }

        public static T[] SubArray<T>(this T[] array, Int32 index, Int32 length)
        {
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

        /// <summary>
        /// Prepend item to params
        /// </summary>
        /// <param name="item">item for prepent</param>
        /// <param name="items">other items</param>
        /// <typeparam name="T">type</typeparam>
        /// <returns>Array of items</returns>
        public static T[] ParamsAppend<T>(this T item, params T[] items)
        {
            return items.Prepend(item).ToArray();
        }

        /// <summary>Searches an entire one-dimensional sorted <see cref="Array" /> for a specific element, using the <see cref="IComparable{T}" /> generic interface implemented by each element of the <see cref="Array" /> and by the specified object.</summary>
        /// <returns>The index of the specified <paramref name="value" /> in the specified <paramref name="array" />, if <paramref name="value" /> is found. If <paramref name="value" /> is not found and <paramref name="value" /> is less than one or more elements in <paramref name="array" />, a negative number which is the bitwise complement of the index of the first element that is larger than <paramref name="value" />. If <paramref name="value" /> is not found and <paramref name="value" /> is greater than any of the elements in <paramref name="array" />, a negative number which is the bitwise complement of (the index of the last element plus 1).</returns>
        /// <param name="array">The sorted one-dimensional, zero-based <see cref="Array" /> to search. </param>
        /// <param name="value">The object to search for.</param>
        /// <typeparam name="T">The type of the elements of the array.</typeparam>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="array" /> is null.</exception>
        /// <exception cref="InvalidOperationException">
        /// <paramref name="value" /> does not implement the <see cref="IComparable{T}" /> generic interface, and the search encounters an element that does not implement the <see cref="IComparable{T}" /> generic interface.</exception>
        [Pure]
        public static Int32 BinarySearch<T>([NotNull] this T[] array, T value)
        {
            return Array.BinarySearch(array, value);
        }

        /// <summary>Searches an entire one-dimensional sorted <see cref="Array" /> for a value using the specified <see cref="IComparer{T}" /> generic interface.</summary>
        /// <returns>The index of the specified <paramref name="value" /> in the specified <paramref name="array" />, if <paramref name="value" /> is found. If <paramref name="value" /> is not found and <paramref name="value" /> is less than one or more elements in <paramref name="array" />, a negative number which is the bitwise complement of the index of the first element that is larger than <paramref name="value" />. If <paramref name="value" /> is not found and <paramref name="value" /> is greater than any of the elements in <paramref name="array" />, a negative number which is the bitwise complement of (the index of the last element plus 1).</returns>
        /// <param name="array">The sorted one-dimensional, zero-based <see cref="Array" /> to search.  </param>
        /// <param name="value">The object to search for.</param>
        /// <param name="comparer">The <see cref="IComparer{T}" /> implementation to use when comparing elements.-or- null to use the <see cref="IComparable{T}" /> implementation of each element.</param>
        /// <typeparam name="T">The type of the elements of the array.</typeparam>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="array" /> is null.</exception>
        /// <exception cref="ArgumentException">
        /// <paramref name="comparer" /> is null, and <paramref name="value" /> is of a type that is not compatible with the elements of <paramref name="array" />.</exception>
        /// <exception cref="InvalidOperationException">
        /// <paramref name="comparer" /> is null, <paramref name="value" /> does not implement the <see cref="IComparable{T}" /> generic interface, and the search encounters an element that does not implement the <see cref="IComparable{T}" /> generic interface.</exception>
        [Pure]
        public static Int32 BinarySearch<T>([NotNull] this T[] array, T value, IComparer<T> comparer)
        {
            return Array.BinarySearch(array, value, comparer);
        }

        /// <summary>Searches a range of elements in a one-dimensional sorted <see cref="Array" /> for a value, using the <see cref="IComparable{T}" /> generic interface implemented by each element of the <see cref="Array" /> and by the specified value.</summary>
        /// <returns>The index of the specified <paramref name="value" /> in the specified <paramref name="array" />, if <paramref name="value" /> is found. If <paramref name="value" /> is not found and <paramref name="value" /> is less than one or more elements in <paramref name="array" />, a negative number which is the bitwise complement of the index of the first element that is larger than <paramref name="value" />. If <paramref name="value" /> is not found and <paramref name="value" /> is greater than any of the elements in <paramref name="array" />, a negative number which is the bitwise complement of (the index of the last element plus 1).</returns>
        /// <param name="array">The sorted one-dimensional, zero-based <see cref="Array" /> to search. </param>
        /// <param name="index">The starting index of the range to search.</param>
        /// <param name="length">The length of the range to search.</param>
        /// <param name="value">The object to search for.</param>
        /// <typeparam name="T">The type of the elements of the array.</typeparam>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="array" /> is null.</exception>
        /// <exception cref="ArgumentOutOfRangeException">
        /// <paramref name="index" /> is less than the lower bound of <paramref name="array" />.-or-<paramref name="length" /> is less than zero.</exception>
        /// <exception cref="ArgumentException">
        /// <paramref name="index" /> and <paramref name="length" /> do not specify a valid range in <paramref name="array" />.-or-<paramref name="value" /> is of a type that is not compatible with the elements of <paramref name="array" />.</exception>
        /// <exception cref="InvalidOperationException">
        /// <paramref name="value" /> does not implement the <see cref="IComparable{T}" /> generic interface, and the search encounters an element that does not implement the <see cref="IComparable{T}" /> generic interface.</exception>
        [Pure]
        public static Int32 BinarySearch<T>([NotNull] this T[] array, Int32 index, Int32 length, T value)
        {
            return Array.BinarySearch(array, index, length, value);
        }

        /// <summary>Searches a range of elements in a one-dimensional sorted <see cref="Array" /> for a value, using the specified <see cref="IComparer{T}" /> generic interface.</summary>
        /// <returns>The index of the specified <paramref name="value" /> in the specified <paramref name="array" />, if <paramref name="value" /> is found. If <paramref name="value" /> is not found and <paramref name="value" /> is less than one or more elements in <paramref name="array" />, a negative number which is the bitwise complement of the index of the first element that is larger than <paramref name="value" />. If <paramref name="value" /> is not found and <paramref name="value" /> is greater than any of the elements in <paramref name="array" />, a negative number which is the bitwise complement of (the index of the last element plus 1).</returns>
        /// <param name="array">The sorted one-dimensional, zero-based <see cref="Array" /> to search. </param>
        /// <param name="index">The starting index of the range to search.</param>
        /// <param name="length">The length of the range to search.</param>
        /// <param name="value">The object to search for.</param>
        /// <param name="comparer">The <see cref="IComparer{T}" /> implementation to use when comparing elements.-or- null to use the <see cref="IComparable{T}" /> implementation of each element.</param>
        /// <typeparam name="T">The type of the elements of the array.</typeparam>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="array" /> is null.</exception>
        /// <exception cref="ArgumentOutOfRangeException">
        /// <paramref name="index" /> is less than the lower bound of <paramref name="array" />.-or-<paramref name="length" /> is less than zero.</exception>
        /// <exception cref="ArgumentException">
        /// <paramref name="index" /> and <paramref name="length" /> do not specify a valid range in <paramref name="array" />.-or-<paramref name="comparer" /> is null, and <paramref name="value" /> is of a type that is not compatible with the elements of <paramref name="array" />.</exception>
        /// <exception cref="InvalidOperationException">
        /// <paramref name="comparer" /> is null, <paramref name="value" /> does not implement the <see cref="IComparable{T}" /> generic interface, and the search encounters an element that does not implement the <see cref="IComparable{T}" /> generic interface.</exception>
        [Pure]
        public static Int32 BinarySearch<T>([NotNull] this T[] array, Int32 index, Int32 length, T value, IComparer<T> comparer)
        {
            return Array.BinarySearch(array, index, length, value, comparer);
        }

        /// <summary>Copies a range of elements from an <see cref="Array" /> starting at the specified source index and pastes them to another <see cref="Array" /> starting at the specified destination index.  Guarantees that all changes are undone if the copy does not succeed completely.</summary>
        /// <param name="array">The <see cref="Array" /> that contains the data to copy.</param>
        /// <param name="index">A 32-bit integer that represents the index in the <paramref name="array" /> at which copying begins.</param>
        /// <param name="destination">The <see cref="Array" /> that receives the data.</param>
        /// <param name="destindex">A 32-bit integer that represents the index in the <paramref name="destination" /> at which storing begins.</param>
        /// <param name="length">A 32-bit integer that represents the number of elements to copy.</param>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="array" /> is null.-or-<paramref name="destination" /> is null.</exception>
        /// <exception cref="RankException">
        /// <paramref name="array" /> and <paramref name="destination" /> have different ranks.</exception>
        /// <exception cref="ArrayTypeMismatchException">The <paramref name="array" /> type is neither the same as nor derived from the <paramref name="destination" /> type.</exception>
        /// <exception cref="InvalidCastException">At least one element in <paramref name="array" /> cannot be cast to the type of <paramref name="destination" />.</exception>
        /// <exception cref="ArgumentOutOfRangeException">
        /// <paramref name="index" /> is less than the lower bound of the first dimension of <paramref name="array" />.-or-<paramref name="destindex" /> is less than the lower bound of the first dimension of <paramref name="destination" />.-or-<paramref name="length" /> is less than zero.</exception>
        /// <exception cref="ArgumentException">
        /// <paramref name="length" /> is greater than the number of elements from <paramref name="index" /> to the end of <paramref name="array" />.-or-<paramref name="length" /> is greater than the number of elements from <paramref name="destindex" /> to the end of <paramref name="destination" />.</exception>
        public static void ConstrainedCopy([NotNull] this Array array, Int32 index, [NotNull] Array destination, Int32 destindex, Int32 length)
        {
            Array.ConstrainedCopy(array, index, destination, destindex, length);
        }

        /// <summary>Converts an array of one type to an array of another type.</summary>
        /// <returns>An array of the target type containing the converted elements from the source array.</returns>
        /// <param name="array">The one-dimensional, zero-based <see cref="Array" /> to convert to a target type.</param>
        /// <param name="converter">A <see cref="Converter{TIn, TOut}" /> that converts each element from one type to another type.</param>
        /// <typeparam name="TIn">The type of the elements of the source array.</typeparam>
        /// <typeparam name="TOut">The type of the elements of the target array.</typeparam>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="array" /> is null.-or-<paramref name="converter" /> is null.</exception>
        [NotNull, Pure]
        public static TOut[] ConvertAll<TIn, TOut>([NotNull] this TIn[] array, [NotNull, InstantHandle] Converter<TIn, TOut> converter)
        {
            return Array.ConvertAll(array, converter);
        }

        /// <summary>Converts an array of one type to an array of another type.</summary>
        /// <returns>An array of the target type containing the converted elements from the source array.</returns>
        /// <param name="array">The one-dimensional, zero-based <see cref="Array" /> to convert to a target type.</param>
        /// <typeparam name="T">The type of the elements of the target array.</typeparam>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="array" /> is null.</exception>
        [NotNull, Pure]
        public static T[] ConvertAll<T>([NotNull] this Object[] array)
        {
            return Array.ConvertAll(array, o => (T) o);
        }

        /// <summary>Copies a range of elements from an <see cref="Array" /> starting at the first element and pastes them into another <see cref="Array" /> starting at the first element. The length is specified as a 32-bit integer.</summary>
        /// <param name="source">The <see cref="Array" /> that contains the data to copy.</param>
        /// <param name="destination">The <see cref="Array" /> that receives the data.</param>
        /// <param name="length">A 32-bit integer that represents the number of elements to copy.</param>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="source" /> is null.-or-<paramref name="destination" /> is null.</exception>
        /// <exception cref="RankException">
        /// <paramref name="source" /> and <paramref name="destination" /> have different ranks.</exception>
        /// <exception cref="ArrayTypeMismatchException">
        /// <paramref name="source" /> and <paramref name="destination" /> are of incompatible types.</exception>
        /// <exception cref="InvalidCastException">At least one element in <paramref name="source" /> cannot be cast to the type of <paramref name="destination" />.</exception>
        /// <exception cref="ArgumentOutOfRangeException">
        /// <paramref name="length" /> is less than zero.</exception>
        /// <exception cref="ArgumentException">
        /// <paramref name="length" /> is greater than the number of elements in <paramref name="source" />.-or-<paramref name="length" /> is greater than the number of elements in <paramref name="destination" />.</exception>
        /// <filterpriority>1</filterpriority>
        public static void Copy([NotNull] this Array source, [NotNull] Array destination, Int32 length)
        {
            Array.Copy(source, destination, length);
        }

        /// <summary>Copies a range of elements from an <see cref="Array" /> starting at the first element and pastes them into another <see cref="Array" /> starting at the first element. The length is specified as a 64-bit integer.</summary>
        /// <param name="source">The <see cref="Array" /> that contains the data to copy.</param>
        /// <param name="destination">The <see cref="Array" /> that receives the data.</param>
        /// <param name="length">A 64-bit integer that represents the number of elements to copy. The integer must be between zero and <see cref="F:System.Int32.MaxValue" />, inclusive.</param>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="source" /> is null.-or-<paramref name="destination" /> is null.</exception>
        /// <exception cref="RankException">
        /// <paramref name="source" /> and <paramref name="destination" /> have different ranks.</exception>
        /// <exception cref="ArrayTypeMismatchException">
        /// <paramref name="source" /> and <paramref name="destination" /> are of incompatible types.</exception>
        /// <exception cref="InvalidCastException">At least one element in <paramref name="source" /> cannot be cast to the type of <paramref name="destination" />.</exception>
        /// <exception cref="ArgumentOutOfRangeException">
        /// <paramref name="length" /> is less than 0 or greater than <see cref="F:System.Int32.MaxValue" />.</exception>
        /// <exception cref="ArgumentException">
        /// <paramref name="length" /> is greater than the number of elements in <paramref name="source" />.-or-<paramref name="length" /> is greater than the number of elements in <paramref name="destination" />.</exception>
        /// <filterpriority>1</filterpriority>
        public static void Copy([NotNull] this Array source, [NotNull] Array destination, Int64 length)
        {
            Array.Copy(source, destination, length);
        }

        /// <summary>Copies a range of elements from an <see cref="Array" /> starting at the specified source index and pastes them to another <see cref="Array" /> starting at the specified destination index. The length and the indexes are specified as 32-bit integers.</summary>
        /// <param name="source">The <see cref="Array" /> that contains the data to copy.</param>
        /// <param name="index">A 32-bit integer that represents the index in the <paramref name="source" /> at which copying begins.</param>
        /// <param name="destination">The <see cref="Array" /> that receives the data.</param>
        /// <param name="destindex">A 32-bit integer that represents the index in the <paramref name="destination" /> at which storing begins.</param>
        /// <param name="length">A 32-bit integer that represents the number of elements to copy.</param>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="source" /> is null.-or-<paramref name="destination" /> is null.</exception>
        /// <exception cref="RankException">
        /// <paramref name="source" /> and <paramref name="destination" /> have different ranks.</exception>
        /// <exception cref="ArrayTypeMismatchException">
        /// <paramref name="source" /> and <paramref name="destination" /> are of incompatible types.</exception>
        /// <exception cref="InvalidCastException">At least one element in <paramref name="source" /> cannot be cast to the type of <paramref name="destination" />.</exception>
        /// <exception cref="ArgumentOutOfRangeException">
        /// <paramref name="index" /> is less than the lower bound of the first dimension of <paramref name="source" />.-or-<paramref name="destindex" /> is less than the lower bound of the first dimension of <paramref name="destination" />.-or-<paramref name="length" /> is less than zero.</exception>
        /// <exception cref="ArgumentException">
        /// <paramref name="length" /> is greater than the number of elements from <paramref name="index" /> to the end of <paramref name="source" />.-or-<paramref name="length" /> is greater than the number of elements from <paramref name="destindex" /> to the end of <paramref name="destination" />.</exception>
        /// <filterpriority>1</filterpriority>
        public static void Copy([NotNull] this Array source, Int64 index, [NotNull] Array destination, Int64 destindex, Int64 length)
        {
            Array.Copy(source, index, destination, destindex, length);
        }

        /// <summary>Copies a range of elements from an <see cref="Array" /> starting at the specified source index and pastes them to another <see cref="Array" /> starting at the specified destination index. The length and the indexes are specified as 64-bit integers.</summary>
        /// <param name="source">The <see cref="Array" /> that contains the data to copy.</param>
        /// <param name="index">A 64-bit integer that represents the index in the <paramref name="source" /> at which copying begins.</param>
        /// <param name="destination">The <see cref="Array" /> that receives the data.</param>
        /// <param name="destindex">A 64-bit integer that represents the index in the <paramref name="destination" /> at which storing begins.</param>
        /// <param name="length">A 64-bit integer that represents the number of elements to copy. The integer must be between zero and <see cref="F:System.Int32.MaxValue" />, inclusive.</param>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="source" /> is null.-or-<paramref name="destination" /> is null.</exception>
        /// <exception cref="RankException">
        /// <paramref name="source" /> and <paramref name="destination" /> have different ranks.</exception>
        /// <exception cref="ArrayTypeMismatchException">
        /// <paramref name="source" /> and <paramref name="destination" /> are of incompatible types.</exception>
        /// <exception cref="InvalidCastException">At least one element in <paramref name="source" /> cannot be cast to the type of <paramref name="destination" />.</exception>
        /// <exception cref="ArgumentOutOfRangeException">
        /// <paramref name="index" /> is outside the range of valid indexes for the <paramref name="source" />.-or-<paramref name="destindex" /> is outside the range of valid indexes for the <paramref name="destination" />.-or-<paramref name="length" /> is less than 0 or greater than <see cref="F:System.Int32.MaxValue" />.</exception>
        /// <exception cref="ArgumentException">
        /// <paramref name="length" /> is greater than the number of elements from <paramref name="index" /> to the end of <paramref name="source" />.-or-<paramref name="length" /> is greater than the number of elements from <paramref name="destindex" /> to the end of <paramref name="destination" />.</exception>
        /// <filterpriority>1</filterpriority>
        public static void Copy([NotNull] this Array source, Int32 index, [NotNull] Array destination, Int32 destindex, Int32 length)
        {
            Array.Copy(source, index, destination, destindex, length);
        }

        /// <summary>
        /// Merges the specified arrays.
        /// </summary>
        /// <typeparam name="T">the type of the array</typeparam>
        /// <param name="first">First array</param>
        /// <param name="second">Second array</param>
        /// <returns>the Merged arrays</returns>
        public static T[] Merge<T>(this T[] first, T[] second)
        {
            if (first is null)
            {
                throw new ArgumentNullException(nameof(first));
            }

            if (second is null)
            {
                return first;
            }

            T[] result = new T[first.Length + second.Length];

            for (Int32 i = 0; i < first.Length; i++)
            {
                result[i] = first[i];
            }

            for (Int32 i = 0; i < second.Length; i++)
            {
                result[i + first.Length] = second[i];
            }

            return result;
        }

        /// <summary>
        /// Merges the specified arrays.
        /// </summary>
        /// <typeparam name="T">the type of the array</typeparam>
        /// <param name="first">First array</param>
        /// <param name="second">Second array</param>
        /// <param name="arrays">Other arrays</param>
        /// <returns>the Merged arrays</returns>
        public static T[] Merge<T>(this T[] first, T[] second, params T[][] arrays)
        {
            if (first is null)
            {
                throw new ArgumentNullException(nameof(first));
            }

            if (arrays.Length == 0)
            {
                return Merge(first, second);
            }

            Int64 sum = arrays.Prepend(second)
                .Where(array => array is not null)
                .Aggregate<T[], Int64>(0, (current, array) => current + array.Length);

            if (sum > Int32.MaxValue)
            {
                throw new ArgumentException($"Summary array length is {sum}. Maximum array length is {Int32.MaxValue}");
            }

            T[] result = new T[sum];

            Int32 index = 0;
            foreach (T[] array in arrays.Prepend(second))
            {
                foreach (T item in array)
                {
                    result[index++] = item;
                }
            }

            return result;
        }

        /// <summary>Determines whether the specified array contains elements that match the conditions defined by the specified predicate.</summary>
        /// <returns>true if <paramref name="array" /> contains one or more elements that match the conditions defined by the specified predicate; otherwise, false.</returns>
        /// <param name="array">The one-dimensional, zero-based <see cref="Array" /> to search.</param>
        /// <param name="match">The <see cref="Predicate{T}" /> that defines the conditions of the elements to search for.</param>
        /// <typeparam name="T">The type of the elements of the array.</typeparam>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="array" /> is null.-or-<paramref name="match" /> is null.</exception>
        [Pure]
        public static Boolean Exists<T>([NotNull] this T[] array, [NotNull, InstantHandle] Predicate<T> match)
        {
            return Array.Exists(array, match);
        }

        /// <summary>
        /// Searches for an element that matches the conditions defined by the specified predicate, and returns the
        /// first occurrence within the entire <see cref="Array" />.
        /// </summary>
        /// <returns>
        /// The first element that matches the conditions defined by the specified predicate, if found; otherwise,
        /// the default value for type <typeparamref name="T" />.
        /// </returns>
        /// <param name="array">The one-dimensional, zero-based <see cref="Array" /> to search.</param>
        /// <param name="match">The <see cref="Predicate{T}" /> that defines the conditions of the element to search for.</param>
        /// <typeparam name="T">The type of the elements of the array.</typeparam>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="array" /> is null.-or-<paramref name="match" /> is null.</exception>
        [Pure]
        public static T Find<T>([NotNull] this T[] array, [NotNull, InstantHandle] Predicate<T> match)
        {
            return Array.Find(array, match);
        }

        /// <summary>Retrieves all the elements that match the conditions defined by the specified predicate.</summary>
        /// <returns>An <see cref="Array" /> containing all the elements that match the conditions defined by the specified predicate, if found; otherwise, an empty <see cref="Array" />.</returns>
        /// <param name="array">The one-dimensional, zero-based <see cref="Array" /> to search.</param>
        /// <param name="match">The <see cref="Predicate{T}" /> that defines the conditions of the elements to search for.</param>
        /// <typeparam name="T">The type of the elements of the array.</typeparam>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="array" /> is null.-or-<paramref name="match" /> is null.</exception>
        [NotNull, Pure]
        public static T[] FindAll<T>([NotNull] this T[] array, [NotNull, InstantHandle] Predicate<T> match)
        {
            return Array.FindAll(array, match);
        }

        /// <summary>Searches for an element that matches the conditions defined by the specified predicate, and returns the zero-based index of the first occurrence within the entire <see cref="Array" />.</summary>
        /// <returns>The zero-based index of the first occurrence of an element that matches the conditions defined by <paramref name="match" />, if found; otherwise, –1.</returns>
        /// <param name="array">The one-dimensional, zero-based <see cref="Array" /> to search.</param>
        /// <param name="match">The <see cref="Predicate{T}" /> that defines the conditions of the element to search for.</param>
        /// <typeparam name="T">The type of the elements of the array.</typeparam>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="array" /> is null.-or-<paramref name="match" /> is null.</exception>
        [Pure]
        public static Int32 FindIndex<T>([NotNull] this T[] array, [NotNull, InstantHandle] Predicate<T> match)
        {
            return Array.FindIndex(array, match);
        }

        /// <summary>Searches for an element that matches the conditions defined by the specified predicate, and returns the zero-based index of the first occurrence within the range of elements in the <see cref="Array" /> that extends from the specified index to the last element.</summary>
        /// <returns>The zero-based index of the first occurrence of an element that matches the conditions defined by <paramref name="match" />, if found; otherwise, –1.</returns>
        /// <param name="array">The one-dimensional, zero-based <see cref="Array" /> to search.</param>
        /// <param name="start">The zero-based starting index of the search.</param>
        /// <param name="match">The <see cref="Predicate{T}" /> that defines the conditions of the element to search for.</param>
        /// <typeparam name="T">The type of the elements of the array.</typeparam>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="array" /> is null.-or-<paramref name="match" /> is null.</exception>
        /// <exception cref="ArgumentOutOfRangeException">
        /// <paramref name="start" /> is outside the range of valid indexes for <paramref name="array" />.</exception>
        [Pure]
        public static Int32 FindIndex<T>([NotNull] this T[] array, Int32 start, [NotNull, InstantHandle] Predicate<T> match)
        {
            return Array.FindIndex(array, start, match);
        }

        /// <summary>Searches for an element that matches the conditions defined by the specified predicate, and returns the zero-based index of the first occurrence within the range of elements in the <see cref="Array" /> that starts at the specified index and contains the specified number of elements.</summary>
        /// <returns>The zero-based index of the first occurrence of an element that matches the conditions defined by <paramref name="match" />, if found; otherwise, –1.</returns>
        /// <param name="array">The one-dimensional, zero-based <see cref="Array" /> to search.</param>
        /// <param name="start">The zero-based starting index of the search.</param>
        /// <param name="count">The number of elements in the section to search.</param>
        /// <param name="match">The <see cref="Predicate{T}" /> that defines the conditions of the element to search for.</param>
        /// <typeparam name="T">The type of the elements of the array.</typeparam>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="array" /> is null.-or-<paramref name="match" /> is null.</exception>
        /// <exception cref="ArgumentOutOfRangeException">
        /// <paramref name="start" /> is outside the range of valid indexes for <paramref name="array" />.-or-<paramref name="count" /> is less than zero.-or-<paramref name="start" /> and <paramref name="count" /> do not specify a valid section in <paramref name="array" />.</exception>
        [Pure]
        public static Int32 FindIndex<T>([NotNull] this T[] array, Int32 start, Int32 count, [NotNull, InstantHandle] Predicate<T> match)
        {
            return Array.FindIndex(array, start, count, match);
        }

        /// <summary>
        /// Searches for an element that matches the conditions defined by the specified predicate, and returns the last
        /// occurrence within the entire <see cref="Array" />.
        /// </summary>
        /// <returns>
        /// The last element that matches the conditions defined by the specified predicate, if found; otherwise, the default
        /// value for type <typeparamref name="T"/>.</returns>
        /// <param name="array">The one-dimensional, zero-based <see cref="Array" /> to search.</param>
        /// <param name="match">The <see cref="Predicate{T}" /> that defines the conditions of the element to search for.</param>
        /// <typeparam name="T">The type of the elements of the array.</typeparam>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="array" /> is null.-or-<paramref name="match" /> is null.</exception>
        [Pure]
        public static T FindLast<T>([NotNull] this T[] array, [NotNull, InstantHandle] Predicate<T> match)
        {
            return Array.FindLast(array, match);
        }

        /// <summary>Searches for an element that matches the conditions defined by the specified predicate, and returns the zero-based index of the last occurrence within the entire <see cref="Array" />.</summary>
        /// <returns>The zero-based index of the last occurrence of an element that matches the conditions defined by <paramref name="match" />, if found; otherwise, –1.</returns>
        /// <param name="array">The one-dimensional, zero-based <see cref="Array" /> to search.</param>
        /// <param name="match">The <see cref="Predicate{T}" /> that defines the conditions of the element to search for.</param>
        /// <typeparam name="T">The type of the elements of the array.</typeparam>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="array" /> is null.-or-<paramref name="match" /> is null.</exception>
        [Pure]
        public static Int32 FindLastIndex<T>([NotNull] this T[] array, [NotNull, InstantHandle] Predicate<T> match)
        {
            return Array.FindLastIndex(array, match);
        }

        /// <summary>Searches for an element that matches the conditions defined by the specified predicate, and returns the zero-based index of the last occurrence within the range of elements in the <see cref="Array" /> that extends from the first element to the specified index.</summary>
        /// <returns>The zero-based index of the last occurrence of an element that matches the conditions defined by <paramref name="match" />, if found; otherwise, –1.</returns>
        /// <param name="array">The one-dimensional, zero-based <see cref="Array" /> to search.</param>
        /// <param name="start">The zero-based starting index of the backward search.</param>
        /// <param name="match">The <see cref="Predicate{T}" /> that defines the conditions of the element to search for.</param>
        /// <typeparam name="T">The type of the elements of the array.</typeparam>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="array" /> is null.-or-<paramref name="match" /> is null.</exception>
        /// <exception cref="ArgumentOutOfRangeException">
        /// <paramref name="start" /> is outside the range of valid indexes for <paramref name="array" />.</exception>
        [Pure]
        public static Int32 FindLastIndex<T>([NotNull] this T[] array, Int32 start, [NotNull, InstantHandle] Predicate<T> match)
        {
            return Array.FindLastIndex(array, start, match);
        }

        /// <summary>Searches for an element that matches the conditions defined by the specified predicate, and returns the zero-based index of the last occurrence within the range of elements in the <see cref="Array" /> that contains the specified number of elements and ends at the specified index.</summary>
        /// <returns>The zero-based index of the last occurrence of an element that matches the conditions defined by <paramref name="match" />, if found; otherwise, –1.</returns>
        /// <param name="array">The one-dimensional, zero-based <see cref="Array" /> to search.</param>
        /// <param name="start">The zero-based starting index of the backward search.</param>
        /// <param name="count">The number of elements in the section to search.</param>
        /// <param name="match">The <see cref="Predicate{T}" /> that defines the conditions of the element to search for.</param>
        /// <typeparam name="T">The type of the elements of the array.</typeparam>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="array" /> is null.-or-<paramref name="match" /> is null.</exception>
        /// <exception cref="ArgumentOutOfRangeException">
        /// <paramref name="start" /> is outside the range of valid indexes for <paramref name="array" />.-or-<paramref name="count" /> is less than zero.-or-<paramref name="start" /> and <paramref name="count" /> do not specify a valid section in <paramref name="array" />.</exception>
        [Pure]
        public static Int32 FindLastIndex<T>([NotNull] this T[] array, Int32 start, Int32 count, [NotNull, InstantHandle] Predicate<T> match)
        {
            return Array.FindLastIndex(array, start, count, match);
        }

        /// <summary>Performs the specified action on each element of the specified array.</summary>
        /// <param name="array">The one-dimensional, zero-based <see cref="Array" /> on whose elements the action is to be performed.</param>
        /// <param name="action">The <see cref="Action{T}" /> to perform on each element of <paramref name="array" />.</param>
        /// <typeparam name="T">The type of the elements of the array.</typeparam>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="array" /> is null.-or-<paramref name="action" /> is null.</exception>
        public static void ForEach<T>([NotNull] this T[] array, [NotNull, InstantHandle] Action<T> action)
        {
            Array.ForEach(array, action);
        }

        /// <summary>Searches for the specified object and returns the index of the first occurrence within the entire <see cref="Array" />.</summary>
        /// <returns>The zero-based index of the first occurrence of <paramref name="value" /> within the entire <paramref name="array" />, if found; otherwise, –1.</returns>
        /// <param name="array">The one-dimensional, zero-based <see cref="Array" /> to search.</param>
        /// <param name="value">The object to locate in <paramref name="array" />.</param>
        /// <typeparam name="T">The type of the elements of the array.</typeparam>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="array" /> is null.</exception>
        [Pure]
        public static Int32 IndexOf<T>([NotNull] this T[] array, T value)
        {
            return Array.IndexOf(array, value);
        }

        /// <summary>Searches for the specified object and returns the index of the first occurrence within the range of elements in the <see cref="Array" /> that extends from the specified index to the last element.</summary>
        /// <returns>The zero-based index of the first occurrence of <paramref name="value" /> within the range of elements in <paramref name="array" /> that extends from <paramref name="startIndex" /> to the last element, if found; otherwise, –1.</returns>
        /// <param name="array">The one-dimensional, zero-based <see cref="Array" /> to search.</param>
        /// <param name="value">The object to locate in <paramref name="array" />.</param>
        /// <param name="startIndex">The zero-based starting index of the search. 0 (zero) is valid in an empty array.</param>
        /// <typeparam name="T">The type of the elements of the array.</typeparam>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="array" /> is null.</exception>
        /// <exception cref="ArgumentOutOfRangeException">
        /// <paramref name="startIndex" /> is outside the range of valid indexes for <paramref name="array" />.</exception>
        [Pure]
        public static Int32 IndexOf<T>([NotNull] this T[] array, T value, Int32 startIndex)
        {
            return Array.IndexOf(array, value, startIndex);
        }

        /// <summary>Searches for the specified object and returns the index of the first occurrence within the range of elements in the <see cref="Array" /> that starts at the specified index and contains the specified number of elements.</summary>
        /// <returns>The zero-based index of the first occurrence of <paramref name="value" /> within the range of elements in <paramref name="array" /> that starts at <paramref name="startIndex" /> and contains the number of elements specified in <paramref name="count" />, if found; otherwise, –1.</returns>
        /// <param name="array">The one-dimensional, zero-based <see cref="Array" /> to search.</param>
        /// <param name="value">The object to locate in <paramref name="array" />.</param>
        /// <param name="startIndex">The zero-based starting index of the search. 0 (zero) is valid in an empty array.</param>
        /// <param name="count">The number of elements in the section to search.</param>
        /// <typeparam name="T">The type of the elements of the array.</typeparam>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="array" /> is null.</exception>
        /// <exception cref="ArgumentOutOfRangeException">
        /// <paramref name="startIndex" /> is outside the range of valid indexes for <paramref name="array" />.-or-<paramref name="count" /> is less than zero.-or-<paramref name="startIndex" /> and <paramref name="count" /> do not specify a valid section in <paramref name="array" />.</exception>
        [Pure]
        public static Int32 IndexOf<T>([NotNull] this T[] array, T value, Int32 startIndex, Int32 count)
        {
            return Array.IndexOf(array, value, startIndex, count);
        }

        /// <summary>Searches for the specified object and returns the index of the last occurrence within the entire <see cref="Array" />.</summary>
        /// <returns>The zero-based index of the last occurrence of <paramref name="value" /> within the entire <paramref name="array" />, if found; otherwise, –1.</returns>
        /// <param name="array">The one-dimensional, zero-based <see cref="Array" /> to search.</param>
        /// <param name="value">The object to locate in <paramref name="array" />.</param>
        /// <typeparam name="T">The type of the elements of the array.</typeparam>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="array" /> is null.</exception>
        [Pure]
        public static Int32 LastIndexOf<T>([NotNull] this T[] array, T value)
        {
            return Array.LastIndexOf(array, value);
        }

        /// <summary>Searches for the specified object and returns the index of the last occurrence within the range of elements in the <see cref="Array" /> that extends from the first element to the specified index.</summary>
        /// <returns>The zero-based index of the last occurrence of <paramref name="value" /> within the range of elements in <paramref name="array" /> that extends from the first element to <paramref name="startIndex" />, if found; otherwise, –1.</returns>
        /// <param name="array">The one-dimensional, zero-based <see cref="Array" /> to search.</param>
        /// <param name="value">The object to locate in <paramref name="array" />.</param>
        /// <param name="startIndex">The zero-based starting index of the backward search.</param>
        /// <typeparam name="T">The type of the elements of the array.</typeparam>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="array" /> is null.</exception>
        /// <exception cref="ArgumentOutOfRangeException">
        /// <paramref name="startIndex" /> is outside the range of valid indexes for <paramref name="array" />.</exception>
        [Pure]
        public static Int32 LastIndexOf<T>([NotNull] this T[] array, T value, Int32 startIndex)
        {
            return Array.LastIndexOf(array, value, startIndex);
        }

        /// <summary>Searches for the specified object and returns the index of the last occurrence within the range of elements in the <see cref="Array" /> that contains the specified number of elements and ends at the specified index.</summary>
        /// <returns>The zero-based index of the last occurrence of <paramref name="value" /> within the range of elements in <paramref name="array" /> that contains the number of elements specified in <paramref name="count" /> and ends at <paramref name="startIndex" />, if found; otherwise, –1.</returns>
        /// <param name="array">The one-dimensional, zero-based <see cref="Array" /> to search.</param>
        /// <param name="value">The object to locate in <paramref name="array" />.</param>
        /// <param name="startIndex">The zero-based starting index of the backward search.</param>
        /// <param name="count">The number of elements in the section to search.</param>
        /// <typeparam name="T">The type of the elements of the array.</typeparam>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="array" /> is null.</exception>
        /// <exception cref="ArgumentOutOfRangeException">
        /// <paramref name="startIndex" /> is outside the range of valid indexes for <paramref name="array" />.-or-<paramref name="count" /> is less than zero.-or-<paramref name="startIndex" /> and <paramref name="count" /> do not specify a valid section in <paramref name="array" />.</exception>
        [Pure]
        public static Int32 LastIndexOf<T>([NotNull] this T[] array, T value, Int32 startIndex, Int32 count)
        {
            return Array.LastIndexOf(array, value, startIndex, count);
        }

        /// <summary>Reverses the sequence of the elements in the entire one-dimensional <see cref="Array" />.</summary>
        /// <param name="array">The one-dimensional <see cref="Array" /> to reverse.</param>
        /// <typeparam name="T">The type of the elements of the array.</typeparam>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="array" /> is null. </exception>
        /// <exception cref="RankException">
        /// <paramref name="array" /> is multidimensional. </exception>
        /// <filterpriority>1</filterpriority>
        public static T[] ReverseInternal<T>([NotNull] this T[] array)
        {
            Array.Reverse(array);
            return array;
        }

        /// <summary>Reverses the sequence of the elements in a range of elements in the one-dimensional <see cref="Array" />.</summary>
        /// <param name="array">The one-dimensional <see cref="Array" /> to reverse.</param>
        /// <param name="index">The starting index of the section to reverse.</param>
        /// <param name="length">The number of elements in the section to reverse.</param>
        /// <typeparam name="T">The type of the elements of the array.</typeparam>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="array" /> is null.</exception>
        /// <exception cref="RankException">
        /// <paramref name="array" /> is multidimensional.</exception>
        /// <exception cref="ArgumentOutOfRangeException">
        /// <paramref name="index" /> is less than the lower bound of <paramref name="array" />.-or-<paramref name="length" /> is less than zero.</exception>
        /// <exception cref="ArgumentException">
        /// <paramref name="index" /> and <paramref name="length" /> do not specify a valid range in <paramref name="array" />.</exception>
        /// <filterpriority>1</filterpriority>
        public static T[] ReverseInternal<T>([NotNull] this T[] array, Int32 index, Int32 length)
        {
            Array.Reverse(array, index, length);
            return array;
        }

        /// <summary>Copy array and reverses the sequence of the elements in a range of elements in the one-dimensional <see cref="Array" />.</summary>
        /// <param name="array">The one-dimensional <see cref="Array" /> to reverse.</param>
        /// <param name="index">The starting index of the section to reverse.</param>
        /// <param name="length">The number of elements in the section to reverse.</param>
        /// <typeparam name="T">The type of the elements of the array.</typeparam>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="array" /> is null.</exception>
        /// <exception cref="RankException">
        /// <paramref name="array" /> is multidimensional.</exception>
        /// <exception cref="ArgumentOutOfRangeException">
        /// <paramref name="index" /> is less than the lower bound of <paramref name="array" />.-or-<paramref name="length" /> is less than zero.</exception>
        /// <exception cref="ArgumentException">
        /// <paramref name="index" /> and <paramref name="length" /> do not specify a valid range in <paramref name="array" />.</exception>
        /// <filterpriority>1</filterpriority>
        public static T[] ReverseCopy<T>([NotNull] this T[] array, Int32 index = 0, Int32 length = Int32.MaxValue)
        {
            T[] copy = new T[array.Length];
            Array.Copy(array, copy, array.Length);
            Array.Reverse(copy, index, length);
            return copy;
        }

        /// <summary>Sorts the elements in an entire <see cref="Array" /> using the <see cref="IComparable{T}" /> generic interface implementation of each element of the <see cref="Array" />.</summary>
        /// <param name="array">The one-dimensional, zero-based <see cref="Array" /> to sort.</param>
        /// <typeparam name="T">The type of the elements of the array.</typeparam>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="array" /> is null.</exception>
        /// <exception cref="InvalidOperationException">One or more elements in <paramref name="array" /> do not implement the <see cref="IComparable{T}" /> generic interface.</exception>
        public static T[] SortArray<T>([NotNull] this T[] array)
        {
            Array.Sort(array);
            return array;
        }

        /// <summary>Sorts the elements in an <see cref="Array" /> using the specified <see cref="IComparer{T}" /> generic interface.</summary>
        /// <param name="array">The one-dimensional, zero-base <see cref="Array" /> to sort</param>
        /// <param name="comparer">The <see cref="IComparer{T}" /> generic interface implementation to use when comparing elements, or null to use the <see cref="IComparable{T}" /> generic interface implementation of each element.</param>
        /// <typeparam name="T">The type of the elements of the array.</typeparam>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="array" /> is null.</exception>
        /// <exception cref="InvalidOperationException">
        /// <paramref name="comparer" /> is null, and one or more elements in <paramref name="array" /> do not implement the <see cref="IComparable{T}" /> generic interface.</exception>
        /// <exception cref="ArgumentException">The implementation of <paramref name="comparer" /> caused an error during the sort. For example, <paramref name="comparer" /> might not return 0 when comparing an item with itself.</exception>
        public static T[] SortArray<T>([NotNull] this T[] array, IComparer<T> comparer)
        {
            Array.Sort(array, comparer);
            return array;
        }

        /// <summary>Sorts the elements in an <see cref="Array" /> using the specified <see cref="Comparison{T}" />.</summary>
        /// <param name="array">The one-dimensional, zero-based <see cref="Array" /> to sort</param>
        /// <param name="comparison">The <see cref="Comparison{T}" /> to use when comparing elements.</param>
        /// <typeparam name="T">The type of the elements of the array.</typeparam>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="array" /> is null.-or-<paramref name="comparison" /> is null.</exception>
        /// <exception cref="ArgumentException">The implementation of <paramref name="comparison" /> caused an error during the sort. For example, <paramref name="comparison" /> might not return 0 when comparing an item with itself.</exception>
        public static T[] SortArray<T>([NotNull] this T[] array, [NotNull, InstantHandle] Comparison<T> comparison)
        {
            Array.Sort(array, comparison);
            return array;
        }

        /// <summary>Sorts the elements in a range of elements in an <see cref="Array" /> using the <see cref="IComparable{T}" /> generic interface implementation of each element of the <see cref="Array" />.</summary>
        /// <param name="array">The one-dimensional, zero-based <see cref="Array" /> to sort</param>
        /// <param name="index">The starting index of the range to sort.</param>
        /// <param name="length">The number of elements in the range to sort.</param>
        /// <typeparam name="T">The type of the elements of the array.</typeparam>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="array" /> is null.</exception>
        /// <exception cref="ArgumentOutOfRangeException">
        /// <paramref name="index" /> is less than the lower bound of <paramref name="array" />.-or-<paramref name="length" /> is less than zero.</exception>
        /// <exception cref="ArgumentException">
        /// <paramref name="index" /> and <paramref name="length" /> do not specify a valid range in <paramref name="array" />.</exception>
        /// <exception cref="InvalidOperationException">One or more elements in <paramref name="array" /> do not implement the <see cref="IComparable{T}" /> generic interface.</exception>
        public static T[] SortArray<T>([NotNull] this T[] array, Int32 index, Int32 length)
        {
            Array.Sort(array, index, length);
            return array;
        }

        /// <summary>Sorts the elements in a range of elements in an <see cref="Array" /> using the specified <see cref="IComparer{T}" /> generic interface.</summary>
        /// <param name="array">The one-dimensional, zero-based <see cref="Array" /> to sort.</param>
        /// <param name="index">The starting index of the range to sort.</param>
        /// <param name="length">The number of elements in the range to sort.</param>
        /// <param name="comparer">The <see cref="IComparer{T}" /> generic interface implementation to use when comparing elements, or null to use the <see cref="IComparable{T}" /> generic interface implementation of each element.</param>
        /// <typeparam name="T">The type of the elements of the array.</typeparam>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="array" /> is null.</exception>
        /// <exception cref="ArgumentOutOfRangeException">
        /// <paramref name="index" /> is less than the lower bound of <paramref name="array" />.-or-<paramref name="length" /> is less than zero.</exception>
        /// <exception cref="ArgumentException">
        /// <paramref name="index" /> and <paramref name="length" /> do not specify a valid range in <paramref name="array" />. -or-The implementation of <paramref name="comparer" /> caused an error during the sort. For example, <paramref name="comparer" /> might not return 0 when comparing an item with itself.</exception>
        /// <exception cref="InvalidOperationException">
        /// <paramref name="comparer" /> is null, and one or more elements in <paramref name="array" /> do not implement the <see cref="IComparable{T}" /> generic interface.</exception>
        public static T[] SortArray<T>([NotNull] this T[] array, Int32 index, Int32 length, IComparer<T> comparer)
        {
            Array.Sort(array, index, length, comparer);
            return array;
        }

        /// <summary>Determines whether every element in the array matches the conditions defined by the specified predicate.</summary>
        /// <returns>true if every element in <paramref name="array" /> matches the conditions defined by the specified predicate; otherwise, false. If there are no elements in the array, the return value is true.</returns>
        /// <param name="array">The one-dimensional, zero-based <see cref="Array" /> to check against the conditions</param>
        /// <param name="match">The <see cref="Predicate{T}" /> that defines the conditions to check against the elements.</param>
        /// <typeparam name="T">The type of the elements of the array.</typeparam>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="array" /> is null.-or-<paramref name="match" /> is null.</exception>
        [Pure]
        public static Boolean TrueForAll<T>([NotNull] this T[] array, [NotNull, InstantHandle] Predicate<T> match)
        {
            return Array.TrueForAll(array, match);
        }
        
        /// <summary>
        /// Converts to <see cref="ReadOnlyArray{T}"/>.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="source"></param>
        /// <returns></returns>
        public static ReadOnlyArray<T> ToReadOnlyArray<T>(this IEnumerable<T> source)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            return source is T[] array ? ToReadOnlyArray(array) : new ReadOnlyArray<T>(source.ToArray());
        }
        
        /// <summary>
        /// Converts to <see cref="ReadOnlyArray{T}"/>.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="source"></param>
        /// <returns></returns>
        public static ReadOnlyArray<T> ToReadOnlyArray<T>(this T[] source)
        {
            return new ReadOnlyArray<T>(source);
        }

        #region EqualsTo

        private const Int32 BoundLength = 5;
        
        /// <summary>
        /// Returns true, if length and content of <paramref name="first"/> equals <paramref name="second"/>.
		/// </summary>
		/// <param name="first">The first array to compare.</param>
		/// <param name="second">The second array to compare.</param>
		/// <returns>True, if length and content of <paramref name="first"/> equals <paramref name="second"/>.</returns>
		[Pure]
		public static unsafe Boolean EqualsTo([CanBeNull] this Char[] first, [CanBeNull] Char[] second)
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
			
			if (first.Length == 0)
			{
				return true;
			}

			if (first.Length < BoundLength)
			{
                return !first.Where((value, i) => value != second[i]).Any(); 
			}

			fixed (void* pa = &first[0], pb = &second[0])
            {
				return MemoryUtils.Compare((Byte*)pa, (Byte*)pb, first.Length * sizeof(Char));
            }
		}

		/// <summary>
        /// Returns true, if length and content of <paramref name="first"/> equals <paramref name="second"/>.
		/// </summary>
		/// <param name="first">The first array to compare.</param>
		/// <param name="second">The second array to compare.</param>
		/// <returns>True, if length and content of <paramref name="first"/> equals <paramref name="second"/>.</returns>
		[Pure]
		public static unsafe Boolean EqualsTo([CanBeNull] this SByte[] first, [CanBeNull] SByte[] second)
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

			if (first.Length == 0)
			{
				return true;
			}
			
			if (first.Length < BoundLength)
			{
                return !first.Where((value, i) => value != second[i]).Any(); 
			}

			fixed (void* pa = &first[0], pb = &second[0])
            {
				return MemoryUtils.Compare((Byte*)pa, (Byte*)pb, first.Length * sizeof(SByte));
            }
		}

		/// <summary>
        /// Returns true, if length and content of <paramref name="first"/> equals <paramref name="second"/>.
		/// </summary>
		/// <param name="first">The first array to compare.</param>
		/// <param name="second">The second array to compare.</param>
		/// <returns>True, if length and content of <paramref name="first"/> equals <paramref name="second"/>.</returns>
		[Pure]
		public static unsafe Boolean EqualsTo([CanBeNull] this Byte[] first, [CanBeNull] Byte[] second)
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
			
			if (first.Length == 0)
			{
				return true;
			}

			if (first.Length < BoundLength)
			{
                return !first.Where((value, i) => value != second[i]).Any(); 
			}

			fixed (void* pa = &first[0], pb = &second[0])
            {
				return MemoryUtils.Compare((Byte*)pa, (Byte*)pb, first.Length * sizeof(Byte));
            }
		}

		/// <summary>
        /// Returns true, if length and content of <paramref name="first"/> equals <paramref name="second"/>.
		/// </summary>
		/// <param name="first">The first array to compare.</param>
		/// <param name="second">The second array to compare.</param>
		/// <returns>True, if length and content of <paramref name="first"/> equals <paramref name="second"/>.</returns>
		[Pure]
		public static unsafe Boolean EqualsTo([CanBeNull] this Int16[] first, [CanBeNull] Int16[] second)
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
			
			if (first.Length == 0)
			{
				return true;
			}

			if (first.Length < BoundLength)
			{
                return !first.Where((value, i) => value != second[i]).Any(); 
			}

			fixed (void* pa = &first[0], pb = &second[0])
            {
				return MemoryUtils.Compare((Byte*)pa, (Byte*)pb, first.Length * sizeof(Int16));
            }
		}

		/// <summary>
        /// Returns true, if length and content of <paramref name="first"/> equals <paramref name="second"/>.
		/// </summary>
		/// <param name="first">The first array to compare.</param>
		/// <param name="second">The second array to compare.</param>
		/// <returns>True, if length and content of <paramref name="first"/> equals <paramref name="second"/>.</returns>
		[Pure]
		public static unsafe Boolean EqualsTo([CanBeNull] this UInt16[] first, [CanBeNull] UInt16[] second)
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
			
			if (first.Length == 0)
			{
				return true;
			}

			if (first.Length < BoundLength)
			{
                return !first.Where((value, i) => value != second[i]).Any(); 
			}

			fixed (void* pa = &first[0], pb = &second[0])
            {
				return MemoryUtils.Compare((Byte*)pa, (Byte*)pb, first.Length * sizeof(UInt16));
            }
		}

		/// <summary>
        /// Returns true, if length and content of <paramref name="first"/> equals <paramref name="second"/>.
		/// </summary>
		/// <param name="first">The first array to compare.</param>
		/// <param name="second">The second array to compare.</param>
		/// <returns>True, if length and content of <paramref name="first"/> equals <paramref name="second"/>.</returns>
		[Pure]
		public static unsafe Boolean EqualsTo([CanBeNull] this Int32[] first, [CanBeNull] Int32[] second)
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
			
			if (first.Length == 0)
			{
				return true;
			}

			if (first.Length < BoundLength)
			{
                return !first.Where((value, i) => value != second[i]).Any(); 
			}

			fixed (void* pa = &first[0], pb = &second[0])
            {
				return MemoryUtils.Compare((Byte*)pa, (Byte*)pb, first.Length * sizeof(Int32));
            }
		}

		/// <summary>
        /// Returns true, if length and content of <paramref name="first"/> equals <paramref name="second"/>.
		/// </summary>
		/// <param name="first">The first array to compare.</param>
		/// <param name="second">The second array to compare.</param>
		/// <returns>True, if length and content of <paramref name="first"/> equals <paramref name="second"/>.</returns>
		[Pure]
		public static unsafe Boolean EqualsTo([CanBeNull] this UInt32[] first, [CanBeNull] UInt32[] second)
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
			
			if (first.Length == 0)
			{
				return true;
			}

			if (first.Length < BoundLength)
			{
                return !first.Where((value, i) => value != second[i]).Any(); 
			}

			fixed (void* pa = &first[0], pb = &second[0])
            {
				return MemoryUtils.Compare((Byte*)pa, (Byte*)pb, first.Length * sizeof(UInt32));
            }
		}

		/// <summary>
        /// Returns true, if length and content of <paramref name="first"/> equals <paramref name="second"/>.
		/// </summary>
		/// <param name="first">The first array to compare.</param>
		/// <param name="second">The second array to compare.</param>
		/// <returns>True, if length and content of <paramref name="first"/> equals <paramref name="second"/>.</returns>
		[Pure]
		public static unsafe Boolean EqualsTo([CanBeNull] this Int64[] first, [CanBeNull] Int64[] second)
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
			
			if (first.Length == 0)
			{
				return true;
			}

			if (first.Length < BoundLength)
			{
                return !first.Where((value, i) => value != second[i]).Any(); 
			}

			fixed (void* pa = &first[0], pb = &second[0])
            {
				return MemoryUtils.Compare((Byte*)pa, (Byte*)pb, first.Length * sizeof(Int64));
            }
		}

		/// <summary>
        /// Returns true, if length and content of <paramref name="first"/> equals <paramref name="second"/>.
		/// </summary>
		/// <param name="first">The first array to compare.</param>
		/// <param name="second">The second array to compare.</param>
		/// <returns>True, if length and content of <paramref name="first"/> equals <paramref name="second"/>.</returns>
		[Pure]
		public static unsafe Boolean EqualsTo([CanBeNull] this UInt64[] first, [CanBeNull] UInt64[] second)
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
			
			if (first.Length == 0)
			{
				return true;
			}

			if (first.Length < BoundLength)
			{
                return !first.Where((value, i) => value != second[i]).Any(); 
			}

			fixed (void* pa = &first[0], pb = &second[0])
            {
				return MemoryUtils.Compare((Byte*)pa, (Byte*)pb, first.Length * sizeof(UInt64));
            }
		}

		/// <summary>
        /// Returns true, if length and content of <paramref name="first"/> equals <paramref name="second"/>.
		/// </summary>
		/// <param name="first">The first array to compare.</param>
		/// <param name="second">The second array to compare.</param>
		/// <returns>True, if length and content of <paramref name="first"/> equals <paramref name="second"/>.</returns>
		[Pure]
		public static unsafe Boolean EqualsTo([CanBeNull] this Single[] first, [CanBeNull] Single[] second)
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
			
			if (first.Length == 0)
			{
				return true;
			}

			if (first.Length < BoundLength)
			{
                 return !first.Where((value, i) => Math.Abs(value - second[i]) >= Single.Epsilon).Any();
			}

			fixed (void* pa = &first[0], pb = &second[0])
            {
				return MemoryUtils.Compare((Byte*)pa, (Byte*)pb, first.Length * sizeof(Single));
            }
		}

		/// <summary>
        /// Returns true, if length and content of <paramref name="first"/> equals <paramref name="second"/>.
		/// </summary>
		/// <param name="first">The first array to compare.</param>
		/// <param name="second">The second array to compare.</param>
		/// <returns>True, if length and content of <paramref name="first"/> equals <paramref name="second"/>.</returns>
		[Pure]
		public static unsafe Boolean EqualsTo([CanBeNull] this Double[] first, [CanBeNull] Double[] second)
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
			
			if (first.Length == 0)
			{
				return true;
			}

			if (first.Length < BoundLength)
			{
                 return !first.Where((value, i) => Math.Abs(value - second[i]) >= Double.Epsilon).Any();
			}

			fixed (void* pa = &first[0], pb = &second[0])
            {
				return MemoryUtils.Compare((Byte*)pa, (Byte*)pb, first.Length * sizeof(Double));
            }
		}

		/// <summary>
        /// Returns true, if length and content of <paramref name="first"/> equals <paramref name="second"/>.
		/// </summary>
		/// <param name="first">The first array to compare.</param>
		/// <param name="second">The second array to compare.</param>
		/// <returns>True, if length and content of <paramref name="first"/> equals <paramref name="second"/>.</returns>
		[Pure]
		public static unsafe Boolean EqualsTo([CanBeNull] this Decimal[] first, [CanBeNull] Decimal[] second)
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
			
			if (first.Length == 0)
			{
				return true;
			}

			if (first.Length < BoundLength)
			{
                return !first.Where((value, i) => value != second[i]).Any(); 
			}

			fixed (void* pa = &first[0], pb = &second[0])
            {
				return MemoryUtils.Compare((Byte*)pa, (Byte*)pb, first.Length * sizeof(Decimal));
            }
		}

		/// <summary>
        /// Returns true, if length and content of <paramref name="first"/> equals <paramref name="second"/>.
		/// </summary>
		/// <param name="first">The first array to compare.</param>
		/// <param name="second">The second array to compare.</param>
		/// <returns>True, if length and content of <paramref name="first"/> equals <paramref name="second"/>.</returns>
		[Pure]
		public static unsafe Boolean EqualsTo([CanBeNull] this TimeSpan[] first, [CanBeNull] TimeSpan[] second)
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
			
			if (first.Length == 0)
			{
				return true;
			}

			if (first.Length < BoundLength)
			{
                return !first.Where((value, i) => value != second[i]).Any(); 
			}

			fixed (void* pa = &first[0], pb = &second[0])
            {
				return MemoryUtils.Compare((Byte*)pa, (Byte*)pb, first.Length * sizeof(TimeSpan));
            }
		}

		/// <summary>
        /// Returns true, if length and content of <paramref name="first"/> equals <paramref name="second"/>.
		/// </summary>
		/// <param name="first">The first array to compare.</param>
		/// <param name="second">The second array to compare.</param>
		/// <returns>True, if length and content of <paramref name="first"/> equals <paramref name="second"/>.</returns>
		[Pure]
		public static unsafe Boolean EqualsTo([CanBeNull] this Guid[] first, [CanBeNull] Guid[] second)
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

			if (first.Length == 0)
            {
				return true;
            }

			fixed (void* pa = &first[0], pb = &second[0])
            {
				return MemoryUtils.Compare((Byte*)pa, (Byte*)pb, first.Length * sizeof(Guid));
            }
		}
		
		/// <summary>
		/// Returns true, if length and content of <paramref name="first"/> equals <paramref name="second"/>.
		/// </summary>
		/// <param name="first">The first array to compare.</param>
		/// <param name="second">The second array to compare.</param>
		/// <returns>True, if length and content of <paramref name="first"/> equals <paramref name="second"/>.</returns>
		[Pure]
		public static unsafe Boolean EqualsTo<T>([CanBeNull] this T[] first, [CanBeNull] T[] second) where T : unmanaged
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

			if (first.Length == 0)
			{
				return true;
			}

			fixed (void* pa = &first[0], pb = &second[0])
			{
				return MemoryUtils.Compare((Byte*)pa, (Byte*)pb, first.Length * sizeof(T));
			}
		}

        #endregion
    }
}