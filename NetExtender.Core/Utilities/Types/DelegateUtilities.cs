// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Runtime.CompilerServices;

namespace NetExtender.Utilities.Types
{
    [SuppressMessage("ReSharper", "CoVariantArrayConversion")]
    public static class DelegateUtilities
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Predicate<T> AsPredicate<T>(this Func<T, Boolean> function)
        {
            if (function is null)
            {
                throw new ArgumentNullException(nameof(function));
            }

            return As<Predicate<T>>(function);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Comparison<T> AsComparison<T>(this Func<T, T, Int32> function)
        {
            if (function is null)
            {
                throw new ArgumentNullException(nameof(function));
            }

            return As<Comparison<T>>(function);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Func<T, Boolean> AsFunc<T>(this Predicate<T> predicate)
        {
            if (predicate is null)
            {
                throw new ArgumentNullException(nameof(predicate));
            }

            return As<Func<T, Boolean>>(predicate);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Func<T, T, Int32> AsFunc<T>(this Comparison<T> comparison)
        {
            if (comparison is null)
            {
                throw new ArgumentNullException(nameof(comparison));
            }

            return As<Func<T, T, Int32>>(comparison);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static TDelegate As<TDelegate>(Delegate @delegate)
        {
            if (@delegate is null)
            {
                throw new ArgumentNullException(nameof(@delegate));
            }

            return (TDelegate) (Object) Delegate.CreateDelegate(typeof(TDelegate), @delegate.Target, @delegate.Method);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean Has(this Delegate @delegate, Delegate? value)
        {
            if (@delegate is null)
            {
                throw new ArgumentNullException(nameof(@delegate));
            }

            return @delegate.GetInvocationList().Contains(value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean Contains(this Delegate? @delegate, Delegate? value)
        {
            return @delegate is not null && Has(@delegate, value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T Combine<T>(this T @delegate, T value) where T : Delegate
        {
            return (T) Delegate.Combine(@delegate, value);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T? Combine<T>(params T[] delegates) where T : Delegate
        {
            if (delegates is null)
            {
                throw new ArgumentNullException(nameof(delegates));
            }

            return delegates.Length switch
            {
                0 => null,
                1 => delegates[1],
                _ => (T?) Delegate.Combine(delegates)
            };
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T? Combine<T>(this T @delegate, params T[]? delegates) where T : Delegate
        {
            if (@delegate is null)
            {
                throw new ArgumentNullException(nameof(@delegate));
            }

            if (delegates is null || delegates.Length <= 0)
            {
                return @delegate;
            }

            return (T?) Delegate.Combine(delegates.Prepend(@delegate).ToArray());
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T? Remove<T>(this T @delegate, T value) where T : Delegate
        {
            return (T?) Delegate.Remove(@delegate, value);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T? RemoveAll<T>(this T @delegate, T value) where T : Delegate
        {
            return (T?) Delegate.RemoveAll(@delegate, value);
        }
        
        public static Object? ParallelDynamicInvoke(this Delegate @delegate, params Object?[]? args)
        {
            if (@delegate is null)
            {
                throw new ArgumentNullException(nameof(@delegate));
            }

            if (args is null)
            {
                throw new ArgumentNullException(nameof(args));
            }

            return @delegate.GetInvocationList()
                .AsParallel().AsOrdered()
                .Select(item => item.DynamicInvoke(args))
                .LastOrDefault();
        }
    }
}