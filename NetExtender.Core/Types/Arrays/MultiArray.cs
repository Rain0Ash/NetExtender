// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace NetExtender.Types.Arrays
{
    public class MultiArray<T> : IEnumerable<T> where T : new()
    {
        public static implicit operator T[](MultiArray<T> array)
        {
            return array.ToArray();
        }

        public readonly T[] Matrix;

        public Int32 Width { get; }
        public Int32 Height { get; }

        public MultiArray(Int32 width, Int32 height)
        {
            Width = width;
            Height = height;

            Matrix = new T[Width * Height];
        }

        public T[] ToArray()
        {
            return Matrix;
        }

        public Span<T> GetAxisX(Int32 y)
        {
            return Matrix.AsSpan(y * Width);
        }

        public Span<T> GetAxisY(Int32 x)
        {
            return Matrix.AsSpan(x * Height);
        }

        public T this[Int32 index]
        {
            get
            {
                return Matrix[index];
            }
            set
            {
                Matrix[index] = value;
            }
        }

        public T this[Int32 x, Int32 y]
        {
            get
            {
                return this[x % Width * y * Width];
            }
            set
            {
                this[x % Width * y * Width] = value;
            }
        }

        public IEnumerator<T> GetEnumerator()
        {
            return Matrix.Cast<T>().GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}