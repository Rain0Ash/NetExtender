// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Collections;
using System.Collections.Generic;

namespace NetExtender.Types.Arrays
{
    /// <summary>
    /// Provides the readonly array.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public sealed class ReadOnlyArray<T> : ReadOnlyArray, IReadOnlyList<T>
    {
        public static implicit operator ReadOnlySpan<T>(ReadOnlyArray<T> array)
        {
            return array._source;
        }

        private readonly T[] _source;

        /// <summary>
        /// Gets the number of elements in the collection.
        /// </summary>
        public Int32 Count
        {
            get
            {
                return _source.Length;
            }
        }
        
        /// <summary>
        /// Creates instance.
        /// </summary>
        /// <param name="source"></param>
        public ReadOnlyArray(T[] source)
        {
            _source = source ?? throw new ArgumentNullException(nameof(source));
        }

        /// <summary>
        /// Gets the element at the specified index in the read-only list.
        /// </summary>
        /// <param name="index">The zero-based index of the element to get.</param>
        /// <returns>The element at the specified index in the read-only list.</returns>
        public T this[Int32 index]
        {
            get
            {
                return _source[index];
            }
        }

        /// <summary>
        /// Returns an enumerator that iterates through the collection.
        /// </summary>
        /// <returns>An enumerator that can be used to iterate through the collection.</returns>
        public Enumerator GetEnumerator()
        {
            return new Enumerator(_source);
        }
        
        /// <summary>
        /// Returns an enumerator that iterates through the collection.
        /// </summary>
        /// <returns>An enumerator that can be used to iterate through the collection.</returns>
        IEnumerator<T> IEnumerable<T>.GetEnumerator()
        {
            return new RefEnumerator(_source);
        }
        
        /// <summary>
        /// Returns an enumerator that iterates through a collection.
        /// </summary>
        /// <returns>An <see cref="IEnumerator"/> object that can be used to iterate through the collection.</returns>
        IEnumerator IEnumerable.GetEnumerator()
        {
            return _source.GetEnumerator();
        }
        
        /// <summary>
        /// Provides an enumerator as value type that iterates through the collection.
        /// </summary>
        public struct Enumerator : IEnumerator<T>
        {
            private readonly T[] _source;
            private Int32 _index;

            public readonly T Current
            {
                get
                {
                    return _source[_index];
                }
            }

            readonly Object? IEnumerator.Current
            {
                get
                {
                    return Current;
                }
            }
            
            public Enumerator(T[] source)
            {
                _source = source;
                _index = -1;
            }

            public Boolean MoveNext()
            {
                _index++;
                return (UInt32) _index < (UInt32) _source.Length;
            }

            public void Reset()
            {
                _index = -1;
            }
            
            public void Dispose()
            {
            }
        }

        /// <summary>
        /// Provides an enumerator as reference type that iterates through the collection.
        /// </summary>
        public class RefEnumerator : IEnumerator<T>
        {
            private readonly T[] _source;
            private Int32 _index;
            
            public T Current
            {
                get
                {
                    return _source[_index];
                }
            }

            Object? IEnumerator.Current
            {
                get
                {
                    return Current;
                }
            }

            public RefEnumerator(T[] source)
            {
                _source = source;
                _index = -1;
            }

            public Boolean MoveNext()
            {
                _index++;
                return (UInt32) _index < (UInt32) _source.Length;
            }
            
            public void Reset()
            {
                _index = -1;
            }
            
            public void Dispose()
            {
            }
        }
    }

    public abstract class ReadOnlyArray
    {
        private static class EmptyArray<T>
        {
            public static readonly ReadOnlyArray<T> Value = new ReadOnlyArray<T>(Array.Empty<T>());
        }

        public static ReadOnlyArray<T> Empty<T>()
        {
            return EmptyArray<T>.Value;
        }
    }
}