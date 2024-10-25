using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Runtime.CompilerServices;
using NetExtender.Types.Counters;
using NetExtender.Types.Counters.Interfaces;
using NetExtender.Types.Exceptions;
using NetExtender.Types.Immutable.Counters.Interfaces;

namespace NetExtender.Types.Immutable.Counters
{
    public static class ImmutableSortedCounter
    {
        public static ImmutableSortedCounter<T, TCount> Create<T, TCount>() where T : notnull where TCount : unmanaged, IConvertible
        {
            if (typeof(TCount) == typeof(Int32))
            {
                ImmutableSortedCounter<T, Int32> result = ImmutableSortedCounter<T>.Empty;
                return Unsafe.As<ImmutableSortedCounter<T, Int32>, ImmutableSortedCounter<T, TCount>>(ref result);
            }
            
            if (typeof(TCount) == typeof(Int64))
            {
                ImmutableSortedCounter<T, Int64> result = ImmutableSortedCounter64<T>.Empty;
                return Unsafe.As<ImmutableSortedCounter<T, Int64>, ImmutableSortedCounter<T, TCount>>(ref result);
            }
            
            if (typeof(TCount) == typeof(Decimal))
            {
                ImmutableSortedCounter<T, Decimal> result = ImmutableSortedDecimalCounter<T>.Empty;
                return Unsafe.As<ImmutableSortedCounter<T, Decimal>, ImmutableSortedCounter<T, TCount>>(ref result);
            }
            
            return ImmutableSortedCounter<T, TCount>.Empty;
        }
        
        public static ImmutableSortedCounter<T, TCount> Create<T, TCount>(IComparer<T>? comparer) where T : notnull where TCount : unmanaged, IConvertible
        {
            if (typeof(TCount) == typeof(Int32))
            {
                ImmutableSortedCounter<T, Int32> result = new ImmutableSortedCounter<T>(ImmutableSortedDictionary.Create<T, Int32>(comparer));
                return Unsafe.As<ImmutableSortedCounter<T, Int32>, ImmutableSortedCounter<T, TCount>>(ref result);
            }
            
            if (typeof(TCount) == typeof(Int64))
            {
                ImmutableSortedCounter<T, Int64> result = new ImmutableSortedCounter64<T>(ImmutableSortedDictionary.Create<T, Int64>(comparer));
                return Unsafe.As<ImmutableSortedCounter<T, Int64>, ImmutableSortedCounter<T, TCount>>(ref result);
            }
            
            if (typeof(TCount) == typeof(Decimal))
            {
                ImmutableSortedCounter<T, Decimal> result = new ImmutableSortedDecimalCounter<T>(ImmutableSortedDictionary.Create<T, Decimal>(comparer));
                return Unsafe.As<ImmutableSortedCounter<T, Decimal>, ImmutableSortedCounter<T, TCount>>(ref result);
            }
            
            return new ImmutableSortedCounter<T, TCount>(ImmutableSortedDictionary.Create<T, TCount>(comparer));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ImmutableSortedCounter<T, TCount> CreateRange<T, TCount>(IEnumerable<T> items) where T : notnull where TCount : unmanaged, IConvertible
        {
            if (items is null)
            {
                throw new ArgumentNullException(nameof(items));
            }

            ImmutableSortedCounter<T, TCount> counter = Create<T, TCount>();
            return counter.AddRange(items);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ImmutableSortedCounter<T, TCount> CreateRange<T, TCount>(IEnumerable<KeyValuePair<T, TCount>> items) where T : notnull where TCount : unmanaged, IConvertible
        {
            if (items is null)
            {
                throw new ArgumentNullException(nameof(items));
            }

            ImmutableSortedCounter<T, TCount> counter = Create<T, TCount>();
            return counter.AddRange(items);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ImmutableSortedCounter<T, TCount> CreateRange<T, TCount>(IComparer<T>? comparer, IEnumerable<T> items) where T : notnull where TCount : unmanaged, IConvertible
        {
            if (items is null)
            {
                throw new ArgumentNullException(nameof(items));
            }

            ImmutableSortedCounter<T, TCount> counter = Create<T, TCount>(comparer);
            return counter.AddRange(items);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ImmutableSortedCounter<T, TCount> CreateRange<T, TCount>(IComparer<T>? comparer, IEnumerable<KeyValuePair<T, TCount>> items) where T : notnull where TCount : unmanaged, IConvertible
        {
            if (items is null)
            {
                throw new ArgumentNullException(nameof(items));
            }

            ImmutableSortedCounter<T, TCount> counter = Create<T, TCount>(comparer);
            return counter.AddRange(items);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ImmutableSortedCounter<TKey, TValue> ToImmutableSortedCounter<TKey, TValue>(this IEnumerable<KeyValuePair<TKey, TValue>> source) where TKey : notnull where TValue : unmanaged, IConvertible
        {
            return ToImmutableSortedCounter(source, null);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ImmutableSortedCounter<TKey, TValue> ToImmutableSortedCounter<TKey, TValue>(this IEnumerable<KeyValuePair<TKey, TValue>> source, IComparer<TKey>? comparer) where TKey : notnull where TValue : unmanaged, IConvertible
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            return source is ImmutableSortedCounter<TKey, TValue> counter ? counter.WithComparers(comparer) : ImmutableSortedCounter<TKey, TValue>.Empty.WithComparers(comparer).AddRange(source);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ImmutableSortedCounter<TKey, TValue> ToImmutableSortedCounter<TSource, TKey, TValue>(this IEnumerable<TSource> source, Func<TSource, TKey> keySelector, Func<TSource, TValue> elementSelector) where TKey : notnull where TValue : unmanaged, IConvertible
        {
            return ToImmutableSortedCounter(source, keySelector, elementSelector, null);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ImmutableSortedCounter<TKey, TValue> ToImmutableSortedCounter<TSource, TKey, TValue>(this IEnumerable<TSource> source, Func<TSource, TKey> keySelector, Func<TSource, TValue> elementSelector, IComparer<TKey>? comparer) where TKey : notnull where TValue : unmanaged, IConvertible
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            if (keySelector is null)
            {
                throw new ArgumentNullException(nameof(keySelector));
            }

            if (elementSelector is null)
            {
                throw new ArgumentNullException(nameof(elementSelector));
            }

            return ImmutableSortedCounter<TKey, TValue>.Empty.WithComparers(comparer).AddRange(source.Select(element => new KeyValuePair<TKey, TValue>(keySelector(element), elementSelector(element))));
        }
    }
    
    public class ImmutableSortedCounter<T> : ImmutableSortedCounter<T, Int32> where T : notnull
    {
        public new static ImmutableSortedCounter<T, Int32> Empty { get; } = new ImmutableSortedCounter<T>(ImmutableSortedDictionary<T, Int32>.Empty);
        
        internal ImmutableSortedCounter(ImmutableSortedDictionary<T, Int32> @internal)
            : base(@internal)
        {
        }

        [return: NotNullIfNotNull("internal")]
        protected override ImmutableSortedCounter<T>? Convert(IImmutableDictionary<T, Int32>? @internal)
        {
            return @internal switch
            {
                null => null,
                ImmutableSortedDictionary<T, Int32> dictionary => new ImmutableSortedCounter<T>(dictionary),
                _ => throw new NeverOperationException(@internal.GetType().Name)
            };
        }

        [return: NotNullIfNotNull("internal")]
        protected override ImmutableSortedCounter<T>? Convert(IImmutableCounter<T, Int32>? @internal)
        {
            return @internal switch
            {
                null => null,
                ImmutableSortedCounter<T> counter => counter,
                _ => throw new NeverOperationException(@internal.GetType().Name)
            };
        }

        protected override Counter<T> ToCounter()
        {
            return new Counter<T>(Internal);
        }
        
        protected sealed override Boolean Less(Int32 value, Int32 count)
        {
            return value < count;
        }
        
        protected sealed override Boolean LessOrEquals(Int32 value, Int32 count)
        {
            return value <= count;
        }

        protected sealed override Boolean Greater(Int32 value, Int32 count)
        {
            return value > count;
        }

        protected sealed override Boolean GreaterOrEquals(Int32 value, Int32 count)
        {
            return value >= count;
        }

        protected sealed override Int32 Increment(Int32 value)
        {
            return ++value;
        }

        protected sealed override Int32 Decrement(Int32 value)
        {
            return --value;
        }

        protected sealed override Int32 Add(Int32 left, Int32 right)
        {
            return left + right;
        }

        protected sealed override Int32 Subtract(Int32 left, Int32 right)
        {
            return left - right;
        }
    }
    
    public class ImmutableSortedCounter64<T> : ImmutableSortedCounter<T, Int64> where T : notnull
    {
        public new static ImmutableSortedCounter<T, Int64> Empty { get; } = new ImmutableSortedCounter64<T>(ImmutableSortedDictionary<T, Int64>.Empty);
        
        internal ImmutableSortedCounter64(ImmutableSortedDictionary<T, Int64> @internal)
            : base(@internal)
        {
        }
        
        [return: NotNullIfNotNull("internal")]
        protected override ImmutableSortedCounter64<T>? Convert(IImmutableDictionary<T, Int64>? @internal)
        {
            return @internal switch
            {
                null => null,
                ImmutableSortedDictionary<T, Int64> dictionary => new ImmutableSortedCounter64<T>(dictionary),
                _ => throw new NeverOperationException(@internal.GetType().Name)
            };
        }

        [return: NotNullIfNotNull("internal")]
        protected override ImmutableSortedCounter64<T>? Convert(IImmutableCounter<T, Int64>? @internal)
        {
            return @internal switch
            {
                null => null,
                ImmutableSortedCounter64<T> counter => counter,
                _ => throw new NeverOperationException(@internal.GetType().Name)
            };
        }

        protected override Counter64<T> ToCounter()
        {
            return new Counter64<T>(Internal);
        }
        
        protected sealed override Boolean Less(Int64 value, Int64 count)
        {
            return value < count;
        }
        
        protected sealed override Boolean LessOrEquals(Int64 value, Int64 count)
        {
            return value <= count;
        }

        protected sealed override Boolean Greater(Int64 value, Int64 count)
        {
            return value > count;
        }

        protected sealed override Boolean GreaterOrEquals(Int64 value, Int64 count)
        {
            return value >= count;
        }

        protected sealed override Int64 Increment(Int64 value)
        {
            return ++value;
        }

        protected sealed override Int64 Decrement(Int64 value)
        {
            return --value;
        }

        protected sealed override Int64 Add(Int64 left, Int64 right)
        {
            return left + right;
        }

        protected sealed override Int64 Subtract(Int64 left, Int64 right)
        {
            return left - right;
        }
    }
    
    public class ImmutableSortedDecimalCounter<T> : ImmutableSortedCounter<T, Decimal> where T : notnull
    {
        public new static ImmutableSortedCounter<T, Decimal> Empty { get; } = new ImmutableSortedDecimalCounter<T>(ImmutableSortedDictionary<T, Decimal>.Empty);
        
        internal ImmutableSortedDecimalCounter(ImmutableSortedDictionary<T, Decimal> @internal)
            : base(@internal)
        {
        }
        
        [return: NotNullIfNotNull("internal")]
        protected override ImmutableSortedDecimalCounter<T>? Convert(IImmutableDictionary<T, Decimal>? @internal)
        {
            return @internal switch
            {
                null => null,
                ImmutableSortedDictionary<T, Decimal> dictionary => new ImmutableSortedDecimalCounter<T>(dictionary),
                _ => throw new NeverOperationException(@internal.GetType().Name)
            };
        }

        [return: NotNullIfNotNull("internal")]
        protected override ImmutableSortedDecimalCounter<T>? Convert(IImmutableCounter<T, Decimal>? @internal)
        {
            return @internal switch
            {
                null => null,
                ImmutableSortedDecimalCounter<T> counter => counter,
                _ => throw new NeverOperationException(@internal.GetType().Name)
            };
        }

        protected override DecimalCounter<T> ToCounter()
        {
            return new DecimalCounter<T>(Internal);
        }
        
        protected sealed override Boolean Less(Decimal value, Decimal count)
        {
            return value < count;
        }
        
        protected sealed override Boolean LessOrEquals(Decimal value, Decimal count)
        {
            return value <= count;
        }

        protected sealed override Boolean Greater(Decimal value, Decimal count)
        {
            return value > count;
        }

        protected sealed override Boolean GreaterOrEquals(Decimal value, Decimal count)
        {
            return value >= count;
        }

        protected sealed override Decimal Increment(Decimal value)
        {
            return ++value;
        }

        protected sealed override Decimal Decrement(Decimal value)
        {
            return --value;
        }

        protected sealed override Decimal Add(Decimal left, Decimal right)
        {
            return left + right;
        }

        protected sealed override Decimal Subtract(Decimal left, Decimal right)
        {
            return left - right;
        }
    }
    
    public class ImmutableSortedCounter<T, TCount> : ImmutableSortedCounter<T, TCount, ImmutableSortedCounter<T, TCount>> where T : notnull where TCount : unmanaged, IConvertible
    {
        public static ImmutableSortedCounter<T, TCount> Empty { get; } = new ImmutableSortedCounter<T, TCount>(ImmutableSortedDictionary<T, TCount>.Empty);
        
        internal ImmutableSortedCounter(ImmutableSortedDictionary<T, TCount> @internal)
            : base(@internal)
        {
        }
        
        [return: NotNullIfNotNull("internal")]
        protected override ImmutableSortedCounter<T, TCount>? Convert(IImmutableDictionary<T, TCount>? @internal)
        {
            return @internal switch
            {
                null => null,
                ImmutableSortedDictionary<T, TCount> dictionary => new ImmutableSortedCounter<T, TCount>(dictionary),
                _ => throw new NeverOperationException(@internal.GetType().Name)
            };
        }

        [return: NotNullIfNotNull("internal")]
        protected override ImmutableSortedCounter<T, TCount>? Convert(IImmutableCounter<T, TCount>? @internal)
        {
            return @internal switch
            {
                null => null,
                ImmutableSortedCounter<T, TCount> counter => counter,
                _ => throw new NeverOperationException(@internal.GetType().Name)
            };
        }

        protected override ICounter<T, TCount> ToCounter()
        {
            return new Counter<T, TCount>(Internal);
        }
    }
    
    public abstract class ImmutableSortedCounter<T, TCount, TCounter> : ImmutableCounterAbstraction<T, TCount, TCounter> where T : notnull where TCount : unmanaged, IConvertible where TCounter : class, IImmutableCounter<T, TCount>
    {
        protected sealed override ImmutableSortedDictionary<T, TCount> Internal { get; }
        
        public IComparer<T> KeyComparer
        {
            get
            {
                return Internal.KeyComparer;
            }
        }
        
        public IEqualityComparer<TCount> ValueComparer
        {
            get
            {
                return Internal.ValueComparer;
            }
        }

        protected ImmutableSortedCounter(ImmutableSortedDictionary<T, TCount> @internal)
        {
            Internal = @internal ?? throw new ArgumentNullException(nameof(@internal));
        }

        public TCounter WithComparers(IComparer<T>? comparer)
        {
            return Convert(Internal.WithComparers(comparer));
        }

        public sealed override KeyValuePair<T, TCount>[] ToArray()
        {
            return base.ToArray();
        }
    }
}