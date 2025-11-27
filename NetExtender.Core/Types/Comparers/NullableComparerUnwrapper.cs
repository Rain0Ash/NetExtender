using System;
using System.Collections.Generic;
using NetExtender.Types.Monads;

namespace NetExtender.Types.Comparers
{
    public sealed class NullableComparerUnwrapper<T> : IComparer<T>, IComparer<NullMaybe<T>>
    {
        public IComparer<NullMaybe<T>> Comparer { get; }

        public NullableComparerUnwrapper(IComparer<NullMaybe<T>> comparer)
        {
            Comparer = comparer ?? throw new ArgumentNullException(nameof(comparer));
        }

        public Int32 Compare(T? x, T? y)
        {
            return Comparer.Compare(x!, y!);
        }

        public Int32 Compare(NullMaybe<T> x, NullMaybe<T> y)
        {
            return Comparer.Compare(x, y);
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
    
    public sealed class NullableComparerUnwrapper<TSource, T> : IComparer<T>, IComparer<NullMaybe<T>> where TSource : notnull
    {
        private TSource Source { get; }
        private Func<TSource, IComparer<NullMaybe<T>>> Selector { get; }

        public IComparer<NullMaybe<T>> Comparer
        {
            get
            {
                return Selector(Source);
            }
        }

        public NullableComparerUnwrapper(TSource source, Func<TSource, IComparer<NullMaybe<T>>> selector)
        {
            Source = source ?? throw new ArgumentNullException(nameof(source));
            Selector = selector ?? throw new ArgumentNullException(nameof(selector));
        }

        public Int32 Compare(T? x, T? y)
        {
            return Comparer.Compare(x!, y!);
        }

        public Int32 Compare(NullMaybe<T> x, NullMaybe<T> y)
        {
            return Comparer.Compare(x, y);
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