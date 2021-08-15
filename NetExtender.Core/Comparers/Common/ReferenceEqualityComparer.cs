// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

#if !NET5_0
namespace NetExtender.Comparers.Common
{
    /// <summary>
    /// Equality comparer that uses the <see cref="object.ReferenceEquals(object, object)"/> to compare values.
    /// </summary>
    public class ReferenceEqualityComparer : ReferenceEqualityComparer<Object>
    {
        private static Lazy<ReferenceEqualityComparer> Lazy { get; } = new Lazy<ReferenceEqualityComparer>();

        public new static ReferenceEqualityComparer Default
        {
            get
            {
                return Lazy.Value;
            }
        }

        public new static ReferenceEqualityComparer Instance
        {
            get
            {
                return Default;
            }
        }
    }

    /// <summary>
    /// Equality comparer that uses the <see cref="object.ReferenceEquals(object, object)"/> to compare values.
    /// </summary>
    /// <typeparam name="T">The type of objects to compare.</typeparam>
    public class ReferenceEqualityComparer<T> : EqualityComparer<T> where T : class
    {
        private static Lazy<ReferenceEqualityComparer<T>> Lazy { get; } = new Lazy<ReferenceEqualityComparer<T>>();

        public new static ReferenceEqualityComparer<T> Default
        {
            get
            {
                return Lazy.Value;
            }
        }

        public static ReferenceEqualityComparer<T> Instance
        {
            get
            {
                return Default;
            }
        }

        protected ReferenceEqualityComparer()
        {
        }

        /// <summary>
        /// Determines whether the provided objects are the same reference.
        /// </summary>
        /// <param name="x">First object.</param>
        /// <param name="y">Second object.</param>
        public override Boolean Equals(T x, T y)
        {
            return ReferenceEquals(x, y);
        }

        /// <summary>
        /// Returns the hash code of the provided object.
        /// </summary>
        /// <param name="obj">The object.</param>
        public override Int32 GetHashCode(T obj)
        {
            return obj.GetHashCode();
        }
    }
}
#endif