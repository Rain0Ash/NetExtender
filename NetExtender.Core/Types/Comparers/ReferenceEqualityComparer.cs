// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Collections.Generic;

namespace NetExtender.Types.Comparers
{
    /// <summary>
    /// Equality comparer that uses the <see cref="object.ReferenceEquals(object, object)"/> to compare values.
    /// </summary>
    /// <typeparam name="T">The type of objects to compare.</typeparam>
    public sealed class ReferenceEqualityComparer<T> : EqualityComparer<T>
    {
        public new static ReferenceEqualityComparer<T> Default { get; } = new ReferenceEqualityComparer<T>();

        private ReferenceEqualityComparer()
        {
        }

        /// <summary>
        /// Determines whether the provided objects are the same reference.
        /// </summary>
        /// <param name="x">First object.</param>
        /// <param name="y">Second object.</param>
        public override Boolean Equals(T? x, T? y)
        {
            return ReferenceEquals(x, y);
        }

        /// <summary>
        /// Returns the hash code of the provided object.
        /// </summary>
        /// <param name="obj">The object.</param>
        public override Int32 GetHashCode(T obj)
        {
            return obj?.GetHashCode() ?? 0;
        }
    }
}