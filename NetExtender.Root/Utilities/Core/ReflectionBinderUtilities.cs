using System;
using System.Reflection;
using System.Runtime.CompilerServices;

namespace NetExtender.Utilities.Core
{
    public static class ReflectionBinderUtilities
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static PropertyInfo? SelectProperty(this Binder binder, BindingFlags binding, PropertyInfo[] match)
        {
            if (binder is null)
            {
                throw new ArgumentNullException(nameof(binder));
            }

            return binder.SelectProperty(binding, match, null, null, null);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static PropertyInfo? SelectProperty(this Binder binder, BindingFlags binding, PropertyInfo[] match, Type? returnType)
        {
            if (binder is null)
            {
                throw new ArgumentNullException(nameof(binder));
            }

            return binder.SelectProperty(binding, match, returnType, null, null);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static PropertyInfo? SelectProperty(this Binder binder, BindingFlags binding, PropertyInfo[] match, Type[]? indexes)
        {
            if (binder is null)
            {
                throw new ArgumentNullException(nameof(binder));
            }

            return binder.SelectProperty(binding, match, null, indexes, null);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static PropertyInfo? SelectProperty(this Binder binder, BindingFlags binding, PropertyInfo[] match, ParameterModifier[]? modifiers)
        {
            if (binder is null)
            {
                throw new ArgumentNullException(nameof(binder));
            }

            return binder.SelectProperty(binding, match, null, null, modifiers);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static PropertyInfo? SelectProperty(this Binder binder, BindingFlags binding, PropertyInfo[] match, Type? returnType, Type[]? indexes)
        {
            if (binder is null)
            {
                throw new ArgumentNullException(nameof(binder));
            }

            return binder.SelectProperty(binding, match, returnType, indexes, null);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static PropertyInfo? SelectProperty(this Binder binder, BindingFlags binding, PropertyInfo[] match, Type? returnType, ParameterModifier[]? modifiers)
        {
            if (binder is null)
            {
                throw new ArgumentNullException(nameof(binder));
            }

            return binder.SelectProperty(binding, match, returnType, null, modifiers);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static MethodBase? SelectMethod(this Binder binder, BindingFlags binding, MethodBase[] match, Type[] types)
        {
            if (binder is null)
            {
                throw new ArgumentNullException(nameof(binder));
            }

            return binder.SelectMethod(binding, match, types, null);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static MethodInfo? SelectMethod(this Binder binder, BindingFlags binding, MethodInfo[] match, Type[] types)
        {
            return SelectMethod(binder, binding, match, types, null);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static MethodInfo? SelectMethod(this Binder binder, BindingFlags binding, MethodInfo[] match, Type[] types, ParameterModifier[]? modifiers)
        {
            if (binder is null)
            {
                throw new ArgumentNullException(nameof(binder));
            }

            if (match is null)
            {
                throw new ArgumentNullException(nameof(match));
            }

            if (types is null)
            {
                throw new ArgumentNullException(nameof(types));
            }

            // ReSharper disable once CoVariantArrayConversion
            return (MethodInfo?) binder.SelectMethod(binding, match, types, modifiers);
        }
    }
}