using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;
using NetExtender.Types.Exceptions;
using NetExtender.Types.Sorters.Interfaces;

namespace NetExtender.Types.Sorters
{
    public enum BinarySorterAlternatePolicy : Byte
    {
        StringIgnoreCase,
        String,
        Throw
    }

    [SuppressMessage("ReSharper", "StaticMemberInGenericType")]
    public sealed class BinarySorter<T> : BinarySorterBase<T>
    {
        private static BinarySorterAlternatePolicy? _policy;
        public static BinarySorterAlternatePolicy Policy
        {
            get
            {
                return _policy ?? BinarySorter.Policy;
            }
            set
            {
                _policy = value;
            }
        }

        public static IBinarySorter<T> Default
        {
            get
            {
                return Policy switch
                {
                    BinarySorterAlternatePolicy.StringIgnoreCase => StringIgnoreCase.Default,
                    BinarySorterAlternatePolicy.String => String.Default,
                    BinarySorterAlternatePolicy.Throw => Throw.Default,
                    _ => throw new EnumUndefinedOrNotSupportedException<BinarySorterAlternatePolicy>(Policy, nameof(Policy), null)
                };
            }
        }

        private IComparer<T> Comparer { get; }

        public BinarySorter()
            : this(null)
        {
        }

        public BinarySorter(IComparer<T>? comparer)
        {
            Comparer = comparer ?? _policy switch
            {
                null => Comparer<T>.Default,
                BinarySorterAlternatePolicy.StringIgnoreCase => StringIgnoreCase.Default,
                BinarySorterAlternatePolicy.String => String.Default,
                BinarySorterAlternatePolicy.Throw => Throw.Default,
                _ => throw new EnumUndefinedOrNotSupportedException<BinarySorterAlternatePolicy>(Policy, nameof(Policy), null)
            };
        }

        public override Int32 Compare(T? x, T? y)
        {
            return Comparer.Compare(x, y);
        }
    }

    public abstract class BinarySorterBase<T> : IBinarySorter<T>
    {
        public abstract Int32 Compare(T? x, T? y);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Int32 GetInsertIndex<TSource>(TSource source, T item, Int32 count, Func<TSource, Int32, T> search)
        {
            return BinarySearchForIndex(source, item, 0, count - 1, search);
        }

        [MethodImpl(MethodImplOptions.AggressiveOptimization)]
        protected virtual Int32 BinarySearchForIndex<TSource>(TSource source, T item, Int32 low, Int32 high, Func<TSource, Int32, T> search)
        {
            if (search is null)
            {
                throw new ArgumentNullException(nameof(search));
            }

            while (high >= low)
            {
                Int32 middle = low + ((high - low) >> 1);
                Int32 result = Compare(search(source, middle), item);

                switch (result)
                {
                    case 0:
                        return middle;
                    case < 0:
                        low = middle + 1;
                        continue;
                    default:
                        high = middle - 1;
                        continue;
                }
            }

            return low;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Int32 GetMatchIndex<TSource>(TSource source, T item, Int32 count, Func<TSource, Int32, T> search)
        {
            return BinarySearchForMatch(source, item, 0, count - 1, search);
        }

        [MethodImpl(MethodImplOptions.AggressiveOptimization)]
        protected virtual Int32 BinarySearchForMatch<TSource>(TSource source, T item, Int32 low, Int32 high, Func<TSource, Int32, T> search)
        {
            if (search is null)
            {
                throw new ArgumentNullException(nameof(search));
            }

            while (high >= low)
            {
                Int32 middle = low + ((high - low) >> 1);
                Int32 result = Compare(search(source, middle), item);

                switch (result)
                {
                    case 0:
                        return middle;
                    case < 0:
                        low = middle + 1;
                        continue;
                    default:
                        high = middle - 1;
                        continue;
                }
            }

            return -1;
        }

        [SuppressMessage("ReSharper", "MemberHidesStaticFromOuterClass")]
        protected sealed class StringIgnoreCase : BinarySorterBase<T>
        {
            public static IBinarySorter<T> Default { get; } = new StringIgnoreCase();

            public override Int32 Compare(T? x, T? y)
            {
                return StringComparer.InvariantCultureIgnoreCase.Compare(x?.ToString(), y?.ToString());
            }
        }

        [SuppressMessage("ReSharper", "MemberHidesStaticFromOuterClass")]
        protected sealed class String : BinarySorterBase<T>
        {
            public static IBinarySorter<T> Default { get; } = new String();

            public override Int32 Compare(T? x, T? y)
            {
                return StringComparer.InvariantCulture.Compare(x?.ToString(), y?.ToString());
            }
        }

        [SuppressMessage("ReSharper", "MemberHidesStaticFromOuterClass")]
        protected sealed class Throw : BinarySorterBase<T>
        {
            public static IBinarySorter<T> Default { get; } = new Throw();

            public override Int32 Compare(T? x, T? y)
            {
                return Comparer<T>.Default.Compare(x, y);
            }
        }
    }

    public abstract class BinarySorter
    {
        public static BinarySorterAlternatePolicy Policy { get; set; } = BinarySorterAlternatePolicy.StringIgnoreCase;
    }
}