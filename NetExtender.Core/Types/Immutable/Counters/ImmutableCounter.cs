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
    public static class ImmutableCounter
    {
        public static ImmutableCounter<T, TCount> Create<T, TCount>() where T : notnull where TCount : unmanaged, IConvertible
        {
            if (typeof(TCount) == typeof(Int32))
            {
                ImmutableCounter<T, Int32> result = ImmutableCounter<T>.Empty;
                return Unsafe.As<ImmutableCounter<T, Int32>, ImmutableCounter<T, TCount>>(ref result);
            }
            
            if (typeof(TCount) == typeof(Int64))
            {
                ImmutableCounter<T, Int64> result = ImmutableCounter64<T>.Empty;
                return Unsafe.As<ImmutableCounter<T, Int64>, ImmutableCounter<T, TCount>>(ref result);
            }
            
            if (typeof(TCount) == typeof(Decimal))
            {
                ImmutableCounter<T, Decimal> result = ImmutableDecimalCounter<T>.Empty;
                return Unsafe.As<ImmutableCounter<T, Decimal>, ImmutableCounter<T, TCount>>(ref result);
            }
            
            return ImmutableCounter<T, TCount>.Empty;
        }
        
        public static ImmutableCounter<T, TCount> Create<T, TCount>(IEqualityComparer<T>? comparer) where T : notnull where TCount : unmanaged, IConvertible
        {
            if (typeof(TCount) == typeof(Int32))
            {
                ImmutableCounter<T, Int32> result = new ImmutableCounter<T>(ImmutableDictionary.Create<T, Int32>(comparer));
                return Unsafe.As<ImmutableCounter<T, Int32>, ImmutableCounter<T, TCount>>(ref result);
            }
            
            if (typeof(TCount) == typeof(Int64))
            {
                ImmutableCounter<T, Int64> result = new ImmutableCounter64<T>(ImmutableDictionary.Create<T, Int64>(comparer));
                return Unsafe.As<ImmutableCounter<T, Int64>, ImmutableCounter<T, TCount>>(ref result);
            }
            
            if (typeof(TCount) == typeof(Decimal))
            {
                ImmutableCounter<T, Decimal> result = new ImmutableDecimalCounter<T>(ImmutableDictionary.Create<T, Decimal>(comparer));
                return Unsafe.As<ImmutableCounter<T, Decimal>, ImmutableCounter<T, TCount>>(ref result);
            }
            
            return new ImmutableCounter<T, TCount>(ImmutableDictionary.Create<T, TCount>(comparer));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ImmutableCounter<T, TCount> CreateRange<T, TCount>(IEnumerable<T> items) where T : notnull where TCount : unmanaged, IConvertible
        {
            if (items is null)
            {
                throw new ArgumentNullException(nameof(items));
            }

            ImmutableCounter<T, TCount> counter = Create<T, TCount>();
            return counter.AddRange(items);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ImmutableCounter<T, TCount> CreateRange<T, TCount>(IEnumerable<KeyValuePair<T, TCount>> items) where T : notnull where TCount : unmanaged, IConvertible
        {
            if (items is null)
            {
                throw new ArgumentNullException(nameof(items));
            }

            ImmutableCounter<T, TCount> counter = Create<T, TCount>();
            return counter.AddRange(items);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ImmutableCounter<T, TCount> CreateRange<T, TCount>(IEqualityComparer<T>? comparer, IEnumerable<T> items) where T : notnull where TCount : unmanaged, IConvertible
        {
            if (items is null)
            {
                throw new ArgumentNullException(nameof(items));
            }

            ImmutableCounter<T, TCount> counter = Create<T, TCount>(comparer);
            return counter.AddRange(items);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ImmutableCounter<T, TCount> CreateRange<T, TCount>(IEqualityComparer<T>? comparer, IEnumerable<KeyValuePair<T, TCount>> items) where T : notnull where TCount : unmanaged, IConvertible
        {
            if (items is null)
            {
                throw new ArgumentNullException(nameof(items));
            }

            ImmutableCounter<T, TCount> counter = Create<T, TCount>(comparer);
            return counter.AddRange(items);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ImmutableCounter<TKey, TValue> ToImmutableCounter<TKey, TValue>(this IEnumerable<KeyValuePair<TKey, TValue>> source) where TKey : notnull where TValue : unmanaged, IConvertible
        {
            return ToImmutableCounter(source, null);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ImmutableCounter<TKey, TValue> ToImmutableCounter<TKey, TValue>(this IEnumerable<KeyValuePair<TKey, TValue>> source, IEqualityComparer<TKey>? comparer) where TKey : notnull where TValue : unmanaged, IConvertible
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            return source is ImmutableCounter<TKey, TValue> counter ? counter.WithComparers(comparer) : ImmutableCounter<TKey, TValue>.Empty.WithComparers(comparer).AddRange(source);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ImmutableCounter<T, TSource> ToImmutableCounter<TSource, T>(this IEnumerable<TSource> source, Func<TSource, T> selector) where T : notnull where TSource : unmanaged, IConvertible
        {
            return ToImmutableCounter(source, selector, static item => item, null);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ImmutableCounter<TKey, TSource> ToImmutableCounter<TSource, TKey>(this IEnumerable<TSource> source, Func<TSource, TKey> selector, IEqualityComparer<TKey>? comparer) where TKey : notnull where TSource : unmanaged, IConvertible
        {
            return ToImmutableCounter(source, selector, static item => item, comparer);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ImmutableCounter<TKey, TValue> ToImmutableCounter<TSource, TKey, TValue>(this IEnumerable<TSource> source, Func<TSource, TKey> keySelector, Func<TSource, TValue> elementSelector) where TKey : notnull where TValue : unmanaged, IConvertible
        {
            return ToImmutableCounter(source, keySelector, elementSelector, null);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ImmutableCounter<TKey, TCount> ToImmutableCounter<TSource, TKey, TCount>(this IEnumerable<TSource> source, Func<TSource, TKey> keySelector, Func<TSource, TCount> elementSelector, IEqualityComparer<TKey>? comparer) where TKey : notnull where TCount : unmanaged, IConvertible
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

            return source is ImmutableCounter<TKey, TCount> counter ? counter.WithComparers(comparer) : ImmutableCounter<TKey, TCount>.Empty.WithComparers(comparer).AddRange(source.Select(item => new KeyValuePair<TKey, TCount>(keySelector(item), elementSelector(item))));
        }
    }
    
    public class ImmutableCounter<T> : ImmutableCounter<T, Int32> where T : notnull
    {
        public new static ImmutableCounter<T, Int32> Empty { get; } = new ImmutableCounter<T>(ImmutableDictionary<T, Int32>.Empty);
        
        internal ImmutableCounter(ImmutableDictionary<T, Int32> @internal)
            : base(@internal)
        {
        }

        [return: NotNullIfNotNull("internal")]
        protected override ImmutableCounter<T>? Convert(IImmutableDictionary<T, Int32>? @internal)
        {
            return @internal switch
            {
                null => null,
                ImmutableDictionary<T, Int32> dictionary => new ImmutableCounter<T>(dictionary),
                _ => throw new NeverOperationException(@internal.GetType().Name)
            };
        }

        [return: NotNullIfNotNull("internal")]
        protected override ImmutableCounter<T>? Convert(IImmutableCounter<T, Int32>? @internal)
        {
            return @internal switch
            {
                null => null,
                ImmutableCounter<T> counter => counter,
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
    
    public class ImmutableCounter64<T> : ImmutableCounter<T, Int64> where T : notnull
    {
        public new static ImmutableCounter<T, Int64> Empty { get; } = new ImmutableCounter64<T>(ImmutableDictionary<T, Int64>.Empty);
        
        internal ImmutableCounter64(ImmutableDictionary<T, Int64> @internal)
            : base(@internal)
        {
        }

        [return: NotNullIfNotNull("internal")]
        protected override ImmutableCounter64<T>? Convert(IImmutableDictionary<T, Int64>? @internal)
        {
            return @internal switch
            {
                null => null,
                ImmutableDictionary<T, Int64> dictionary => new ImmutableCounter64<T>(dictionary),
                _ => throw new NeverOperationException(@internal.GetType().Name)
            };
        }

        [return: NotNullIfNotNull("internal")]
        protected override ImmutableCounter64<T>? Convert(IImmutableCounter<T, Int64>? @internal)
        {
            return @internal switch
            {
                null => null,
                ImmutableCounter64<T> counter => counter,
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
    
    public class ImmutableDecimalCounter<T> : ImmutableCounter<T, Decimal> where T : notnull
    {
        public new static ImmutableCounter<T, Decimal> Empty { get; } = new ImmutableDecimalCounter<T>(ImmutableDictionary<T, Decimal>.Empty);
        
        internal ImmutableDecimalCounter(ImmutableDictionary<T, Decimal> @internal)
            : base(@internal)
        {
        }

        [return: NotNullIfNotNull("internal")]
        protected override ImmutableDecimalCounter<T>? Convert(IImmutableDictionary<T, Decimal>? @internal)
        {
            return @internal switch
            {
                null => null,
                ImmutableDictionary<T, Decimal> dictionary => new ImmutableDecimalCounter<T>(dictionary),
                _ => throw new NeverOperationException(@internal.GetType().Name)
            };
        }

        [return: NotNullIfNotNull("internal")]
        protected override ImmutableDecimalCounter<T>? Convert(IImmutableCounter<T, Decimal>? @internal)
        {
            return @internal switch
            {
                null => null,
                ImmutableDecimalCounter<T> counter => counter,
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
    
    public class ImmutableCounter<T, TCount> : ImmutableCounter<T, TCount, ImmutableCounter<T, TCount>> where T : notnull where TCount : unmanaged, IConvertible
    {
        public static ImmutableCounter<T, TCount> Empty { get; } = new ImmutableCounter<T, TCount>(ImmutableDictionary<T, TCount>.Empty);
        
        internal ImmutableCounter(ImmutableDictionary<T, TCount> @internal)
            : base(@internal)
        {
        }
        
        [return: NotNullIfNotNull("internal")]
        protected override ImmutableCounter<T, TCount>? Convert(IImmutableDictionary<T, TCount>? @internal)
        {
            return @internal switch
            {
                null => null,
                ImmutableDictionary<T, TCount> dictionary => new ImmutableCounter<T, TCount>(dictionary),
                _ => throw new NeverOperationException(@internal.GetType().Name)
            };
        }

        [return: NotNullIfNotNull("internal")]
        protected override ImmutableCounter<T, TCount>? Convert(IImmutableCounter<T, TCount>? @internal)
        {
            return @internal switch
            {
                null => null,
                ImmutableCounter<T, TCount> counter => counter,
                _ => throw new NeverOperationException(@internal.GetType().Name)
            };
        }

        protected override ICounter<T, TCount> ToCounter()
        {
            return new Counter<T, TCount>(Internal);
        }
    }
    
    public abstract class ImmutableCounter<T, TCount, TCounter> : ImmutableCounterAbstraction<T, TCount, TCounter> where T : notnull where TCount : unmanaged, IConvertible where TCounter : class, IImmutableCounter<T, TCount>
    {
        protected sealed override ImmutableDictionary<T, TCount> Internal { get; }
        
        public IEqualityComparer<T> KeyComparer
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

        protected ImmutableCounter(ImmutableDictionary<T, TCount> @internal)
        {
            Internal = @internal ?? throw new ArgumentNullException(nameof(@internal));
        }

        public TCounter WithComparers(IEqualityComparer<T>? comparer)
        {
            return Convert(Internal.WithComparers(comparer));
        }

        public sealed override KeyValuePair<T, TCount>[] ToArray()
        {
            return base.ToArray();
        }
    }
}