using System;
using System.Collections.Generic;
using NetExtender.Types.Monads;

namespace NetExtender.Types.Comparers
{
    public sealed class NullableEqualityComparerUnwrapper<T> : IEqualityComparer<T>, IEqualityComparer<NullMaybe<T>>
    {
        public IEqualityComparer<NullMaybe<T>> Comparer { get; }

        public NullableEqualityComparerUnwrapper(IEqualityComparer<NullMaybe<T>> comparer)
        {
            Comparer = comparer ?? throw new ArgumentNullException(nameof(comparer));
        }

        public Boolean Equals(T? x, T? y)
        {
            return Comparer.Equals(x!, y!);
        }

        public Int32 GetHashCode(T other)
        {
            return Comparer.GetHashCode(other);
        }

        public Boolean Equals(NullMaybe<T> x, NullMaybe<T> y)
        {
            return Comparer.Equals(x, y);
        }

        public Int32 GetHashCode(NullMaybe<T> other)
        {
            return Comparer.GetHashCode(other);
        }

        public override Int32 GetHashCode()
        {
            return Comparer.GetHashCode();
        }

        public override Boolean Equals(Object? obj)
        {
            return Comparer.Equals(obj);
        }

        public override String? ToString()
        {
            return Comparer.ToString();
        }
    }
    
    public sealed class NullableEqualityComparerUnwrapper<TSource, T> : IEqualityComparer<T>, IEqualityComparer<NullMaybe<T>> where TSource : notnull
    {
        public TSource Source { get; }
        private Func<TSource, IEqualityComparer<NullMaybe<T>>> Selector { get; }

        public IEqualityComparer<NullMaybe<T>> Comparer
        {
            get
            {
                return Selector(Source);
            }
        }

        public NullableEqualityComparerUnwrapper(TSource source, Func<TSource, IEqualityComparer<NullMaybe<T>>> selector)
        {
            Source = source ?? throw new ArgumentNullException(nameof(source));
            Selector = selector ?? throw new ArgumentNullException(nameof(selector));
        }

        public Boolean Equals(T? x, T? y)
        {
            return Comparer.Equals(x!, y!);
        }

        public Int32 GetHashCode(T other)
        {
            return Comparer.GetHashCode(other);
        }

        public Boolean Equals(NullMaybe<T> x, NullMaybe<T> y)
        {
            return Comparer.Equals(x, y);
        }

        public Int32 GetHashCode(NullMaybe<T> other)
        {
            return Comparer.GetHashCode(other);
        }

        public override Int32 GetHashCode()
        {
            return Comparer.GetHashCode();
        }

        public override Boolean Equals(Object? obj)
        {
            return Comparer.Equals(obj);
        }

        public override String? ToString()
        {
            return Comparer.ToString();
        }
    }
}