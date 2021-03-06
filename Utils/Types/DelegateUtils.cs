// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Linq;
using System.Runtime.CompilerServices;
using JetBrains.Annotations;

namespace NetExtender.Utils.Types
{
    [System.Diagnostics.CodeAnalysis.SuppressMessage("ReSharper", "CoVariantArrayConversion")]
    public static class DelegateUtils
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean Has([NotNull] this Delegate @delegate, Delegate value)
        {
            if (@delegate is null)
            {
                throw new ArgumentNullException(nameof(@delegate));
            }

            return @delegate.GetInvocationList().Contains(value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean Contains(this Delegate? @delegate, Delegate value)
        {
            return @delegate is not null && Has(@delegate, value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T Combine<T>(this T @delegate, T value) where T : Delegate
        {
            return (T) Delegate.Combine(@delegate, value);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T Combine<T>([NotNull] params T[] delegates) where T : Delegate
        {
            if (delegates is null)
            {
                throw new ArgumentNullException(nameof(delegates));
            }

            return delegates.Length switch
            {
                0 => null,
                1 => delegates[1],
                _ => (T) Delegate.Combine(delegates)
            };
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T Combine<T>([NotNull] this T @delegate, params T[] delegates) where T : Delegate
        {
            if (@delegate is null)
            {
                throw new ArgumentNullException(nameof(@delegate));
            }

            if (delegates is null || delegates.Length <= 0)
            {
                return @delegate;
            }

            return (T) Delegate.Combine(@delegate.ParamsAppend(delegates));
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T Remove<T>(this T @delegate, T value) where T : Delegate
        {
            return (T) Delegate.Remove(@delegate, value);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T RemoveAll<T>(this T @delegate, T value) where T : Delegate
        {
            return (T) Delegate.RemoveAll(@delegate, value);
        }
    }
}