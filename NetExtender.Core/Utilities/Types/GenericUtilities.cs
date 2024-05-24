// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Reflection;
using System.Runtime.CompilerServices;
using NetExtender.Types.Exceptions;
using NetExtender.Types.Monads;
using NetExtender.Utilities.Core;

namespace NetExtender.Utilities.Types
{
    public static class GenericUtilities
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static Int32? ReferenceCompare(Object? first, Object? second)
        {
            if (first is null || second is null)
            {
                return second is null ? 1 : -1;
            }

            return ReferenceEquals(first, second) ? 0 : null;
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static Box<T> Box<T>(this T value)
        {
            return value;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static T? AsDefault<T>(this T? value, T? alternate)
        {
            return IsDefault(value) ? alternate : value;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static Boolean IsDefault<T>(this T? value)
        {
            return EqualityComparer<T?>.Default.Equals(value, default);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static Boolean IsNotDefault<T>(this T? value)
        {
            return !IsDefault(value);
        }

        /// <summary>
        /// Return equals
        /// default != default
        /// </summary>
        /// <param name="first"></param>
        /// <param name="second"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static Boolean IsEquals<T>(this T? first, T? second)
        {
            if (IsDefault(first) || IsDefault(second))
            {
                return false;
            }

            return EqualityComparer<T?>.Default.Equals(first, second);
        }

        public static Boolean IsDefaultOrEquals<T>(this T? first, T? second)
        {
            Boolean result = IsDefault(first);

            if (result != IsDefault(second))
            {
                return false;
            }

            return result || EqualityComparer<T?>.Default.Equals(first, second);
        }

        public static Boolean IsAnyDefaultOrEquals<T>(this T? first, T? second)
        {
            if (IsDefault(first) || IsDefault(second))
            {
                return true;
            }

            return EqualityComparer<T?>.Default.Equals(first, second);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static T If<T>(this T value, Func<T, T> selector, Boolean condition)
        {
            if (selector is null)
            {
                throw new ArgumentNullException(nameof(selector));
            }

            return condition ? selector(value) : value;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static T If<T>(this T value, Func<T, T> selector, Func<Boolean> condition)
        {
            if (selector is null)
            {
                throw new ArgumentNullException(nameof(selector));
            }

            if (condition is null)
            {
                throw new ArgumentNullException(nameof(condition));
            }

            return condition() ? selector(value) : value;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static T If<T>(this T value, Func<T, T> selector, Func<T, Boolean> condition)
        {
            if (selector is null)
            {
                throw new ArgumentNullException(nameof(selector));
            }

            if (condition is null)
            {
                throw new ArgumentNullException(nameof(condition));
            }

            return condition(value) ? selector(value) : value;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static T If<T, TArgument>(this T value, Func<T, TArgument, T> selector, TArgument argument, Boolean condition)
        {
            if (selector is null)
            {
                throw new ArgumentNullException(nameof(selector));
            }

            return condition ? selector(value, argument) : value;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static T If<T, TArgument>(this T value, Func<T, TArgument, T> selector, TArgument argument, Func<Boolean> condition)
        {
            if (selector is null)
            {
                throw new ArgumentNullException(nameof(selector));
            }

            if (condition is null)
            {
                throw new ArgumentNullException(nameof(condition));
            }

            return condition() ? selector(value, argument) : value;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static T If<T, TArgument>(this T value, Func<T, TArgument, T> selector, TArgument argument, Func<T, Boolean> condition)
        {
            if (selector is null)
            {
                throw new ArgumentNullException(nameof(selector));
            }

            if (condition is null)
            {
                throw new ArgumentNullException(nameof(condition));
            }

            return condition(value) ? selector(value, argument) : value;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static T If<T, T1, T2>(this T value, Func<T, T1, T2, T> selector, T1 first, T2 second, Boolean condition)
        {
            if (selector is null)
            {
                throw new ArgumentNullException(nameof(selector));
            }

            return condition ? selector(value, first, second) : value;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static T If<T, T1, T2>(this T value, Func<T, T1, T2, T> selector, T1 first, T2 second, Func<Boolean> condition)
        {
            if (selector is null)
            {
                throw new ArgumentNullException(nameof(selector));
            }

            if (condition is null)
            {
                throw new ArgumentNullException(nameof(condition));
            }

            return condition() ? selector(value, first, second) : value;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static T If<T, T1, T2>(this T value, Func<T, T1, T2, T> selector, T1 first, T2 second, Func<T, Boolean> condition)
        {
            if (selector is null)
            {
                throw new ArgumentNullException(nameof(selector));
            }

            if (condition is null)
            {
                throw new ArgumentNullException(nameof(condition));
            }

            return condition(value) ? selector(value, first, second) : value;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static T If<T, T1, T2, T3>(this T value, Func<T, T1, T2, T3, T> selector, T1 first, T2 second, T3 third, Boolean condition)
        {
            if (selector is null)
            {
                throw new ArgumentNullException(nameof(selector));
            }

            return condition ? selector(value, first, second, third) : value;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static T If<T, T1, T2, T3>(this T value, Func<T, T1, T2, T3, T> selector, T1 first, T2 second, T3 third, Func<Boolean> condition)
        {
            if (selector is null)
            {
                throw new ArgumentNullException(nameof(selector));
            }

            if (condition is null)
            {
                throw new ArgumentNullException(nameof(condition));
            }

            return condition() ? selector(value, first, second, third) : value;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static T If<T, T1, T2, T3>(this T value, Func<T, T1, T2, T3, T> selector, T1 first, T2 second, T3 third, Func<T, Boolean> condition)
        {
            if (selector is null)
            {
                throw new ArgumentNullException(nameof(selector));
            }

            if (condition is null)
            {
                throw new ArgumentNullException(nameof(condition));
            }

            return condition(value) ? selector(value, first, second, third) : value;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static T IfNot<T>(this T value, Func<T, T> selector, Boolean condition)
        {
            if (selector is null)
            {
                throw new ArgumentNullException(nameof(selector));
            }

            return !condition ? selector(value) : value;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static T IfNot<T>(this T value, Func<T, T> selector, Func<Boolean> condition)
        {
            if (selector is null)
            {
                throw new ArgumentNullException(nameof(selector));
            }

            if (condition is null)
            {
                throw new ArgumentNullException(nameof(condition));
            }

            return !condition() ? selector(value) : value;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static T IfNot<T>(this T value, Func<T, T> selector, Func<T, Boolean> condition)
        {
            if (selector is null)
            {
                throw new ArgumentNullException(nameof(selector));
            }

            if (condition is null)
            {
                throw new ArgumentNullException(nameof(condition));
            }

            return !condition(value) ? selector(value) : value;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static T IfNot<T, TArgument>(this T value, Func<T, TArgument, T> selector, TArgument argument, Boolean condition)
        {
            if (selector is null)
            {
                throw new ArgumentNullException(nameof(selector));
            }

            return !condition ? selector(value, argument) : value;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static T IfNot<T, TArgument>(this T value, Func<T, TArgument, T> selector, TArgument argument, Func<Boolean> condition)
        {
            if (selector is null)
            {
                throw new ArgumentNullException(nameof(selector));
            }

            if (condition is null)
            {
                throw new ArgumentNullException(nameof(condition));
            }

            return !condition() ? selector(value, argument) : value;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static T IfNot<T, TArgument>(this T value, Func<T, TArgument, T> selector, TArgument argument, Func<T, Boolean> condition)
        {
            if (selector is null)
            {
                throw new ArgumentNullException(nameof(selector));
            }

            if (condition is null)
            {
                throw new ArgumentNullException(nameof(condition));
            }

            return !condition(value) ? selector(value, argument) : value;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static T IfNot<T, T1, T2>(this T value, Func<T, T1, T2, T> selector, T1 first, T2 second, Boolean condition)
        {
            if (selector is null)
            {
                throw new ArgumentNullException(nameof(selector));
            }

            return !condition ? selector(value, first, second) : value;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static T IfNot<T, T1, T2>(this T value, Func<T, T1, T2, T> selector, T1 first, T2 second, Func<Boolean> condition)
        {
            if (selector is null)
            {
                throw new ArgumentNullException(nameof(selector));
            }

            if (condition is null)
            {
                throw new ArgumentNullException(nameof(condition));
            }

            return !condition() ? selector(value, first, second) : value;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static T IfNot<T, T1, T2>(this T value, Func<T, T1, T2, T> selector, T1 first, T2 second, Func<T, Boolean> condition)
        {
            if (selector is null)
            {
                throw new ArgumentNullException(nameof(selector));
            }

            if (condition is null)
            {
                throw new ArgumentNullException(nameof(condition));
            }

            return !condition(value) ? selector(value, first, second) : value;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static T IfNot<T, T1, T2, T3>(this T value, Func<T, T1, T2, T3, T> selector, T1 first, T2 second, T3 third, Boolean condition)
        {
            if (selector is null)
            {
                throw new ArgumentNullException(nameof(selector));
            }

            return !condition ? selector(value, first, second, third) : value;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static T IfNot<T, T1, T2, T3>(this T value, Func<T, T1, T2, T3, T> selector, T1 first, T2 second, T3 third, Func<Boolean> condition)
        {
            if (selector is null)
            {
                throw new ArgumentNullException(nameof(selector));
            }

            if (condition is null)
            {
                throw new ArgumentNullException(nameof(condition));
            }

            return !condition() ? selector(value, first, second, third) : value;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static T IfNot<T, T1, T2, T3>(this T value, Func<T, T1, T2, T3, T> selector, T1 first, T2 second, T3 third, Func<T, Boolean> condition)
        {
            if (selector is null)
            {
                throw new ArgumentNullException(nameof(selector));
            }

            if (condition is null)
            {
                throw new ArgumentNullException(nameof(condition));
            }

            return !condition(value) ? selector(value, first, second, third) : value;
        }

        private static MethodInfo MemberwiseCloneMethod { get; } = typeof(Object).GetMethod("MemberwiseClone", BindingFlags.Instance | BindingFlags.NonPublic)!;
        private static Converter<Object, Object> MemberwiseCloneDelegate { get; } = (Converter<Object, Object>) MemberwiseCloneMethod.CreateDelegate(typeof(Converter<Object, Object>));

        [return: NotNullIfNotNull("value")]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T? MemberwiseClone<T>(this T? value)
        {
            return value is not null ? (T) MemberwiseCloneDelegate.Invoke(value) : default;
        }

        [return: NotNullIfNotNull("value")]
        public static T? Clone<T>(this T? value)
        {
            if (value is null)
            {
                return default;
            }

            if (Equals(value, default))
            {
                return default;
            }

            return value switch
            {
                ICloneable cloneable => cloneable.Clone<T>(),
                _ => value.DeepCopy()
            };
        }

        [return: NotNullIfNotNull("cloneable")]
        public static T? Clone<T>(this ICloneable? cloneable)
        {
            if (cloneable is null)
            {
                return default;
            }

            if (cloneable.Clone() is T clone)
            {
                return clone;
            }

            throw new CloneException($"{cloneable.GetType()} is not {typeof(T)}");
        }

        /// <summary>
        /// Creates a deep copy (new instance with new instances of properties).
        /// </summary>
        /// <param name="original">The original object to copy.</param>
        [return: NotNullIfNotNull("original")]
        public static T? DeepCopy<T>(this T original)
        {
            if (original is null)
            {
                return default;
            }

            try
            {
                if (DeepCopyInternal(original, new Dictionary<Object, Object?>(ReferenceEqualityComparer.Instance)) is T generic)
                {
                    return generic;
                }

                throw new CloneException();
            }
            catch (Exception exception)
            {
                throw new CloneException(null, exception);
            }
        }

        private static Object? DeepCopyInternal(Object? original, IDictionary<Object, Object?> visited)
        {
            if (visited is null)
            {
                throw new ArgumentNullException(nameof(visited));
            }

            if (original is null)
            {
                return null;
            }

            Type type = original.GetType();

            if (type.IsPrimitive())
            {
                return original;
            }

            if (visited.TryGetValue(original, out Object? @internal))
            {
                return @internal;
            }

            if (typeof(Delegate).IsAssignableFrom(type))
            {
                return null;
            }

            Object? clone = MemberwiseCloneMethod.Invoke(original, null);

            if (clone is null)
            {
                return null;
            }

            if (type.IsArray)
            {
                if (type.GetElementType()?.IsPrimitive() is false)
                {
                    Array cloned = (Array) clone;
                    cloned.ForEach((arr, indices) => arr.SetValue(DeepCopyInternal(cloned?.GetValue(indices), visited), indices));
                }
            }

            visited.Add(original, clone);

            DeepCopyFields(original, visited, clone, type);

            RecursiveDeepCopyBaseTypePrivateFields(original, visited, clone, type);

            return clone;
        }

        private static void DeepCopyFields(Object original, IDictionary<Object, Object?> visited, Object clone, IReflect type, Func<FieldInfo, Boolean>? filter = null)
        {
            DeepCopyFields(original, visited, clone, type, BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.FlattenHierarchy, filter);
        }

        private static void DeepCopyFields(Object original, IDictionary<Object, Object?> visited, Object clone, IReflect type, BindingFlags binding, Func<FieldInfo, Boolean>? filter = null)
        {
            IEnumerable<FieldInfo> fields = type.GetFields(binding);

            if (filter is not null)
            {
                fields = fields.WhereNot(filter);
            }

            foreach (FieldInfo field in fields)
            {
                if (field.FieldType.IsPrimitive())
                {
                    continue;
                }

                Object? origfield = field.GetValue(original);
                Object? clonedfield = DeepCopyInternal(origfield, visited);

                field.SetValue(clone, clonedfield);
            }
        }

        private static void RecursiveDeepCopyBaseTypePrivateFields(Object original, IDictionary<Object, Object?> visited, Object clone, Type type)
        {
            if (type.BaseType is null)
            {
                return;
            }

            RecursiveDeepCopyBaseTypePrivateFields(original, visited, clone, type.BaseType);
            DeepCopyFields(original, visited, clone, type.BaseType, BindingFlags.Instance | BindingFlags.NonPublic, fieldInfo => fieldInfo.IsPrivate);
        }
    }
}