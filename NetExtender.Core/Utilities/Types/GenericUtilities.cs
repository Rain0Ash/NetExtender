// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Reflection;
using System.Runtime.CompilerServices;
using NetExtender.Utilities.Core;

namespace NetExtender.Utilities.Types
{
    public static class GenericUtilities
    {
        public static T? AsDefault<T>(this T? value, T? alternate)
        {
            return IsDefault(value) ? alternate : value;
        }

        public static Boolean IsDefault<T>(this T? value)
        {
            return EqualityComparer<T?>.Default.Equals(value, default);
        }

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
            Boolean df = IsDefault(first);

            if (df != IsDefault(second))
            {
                return false;
            }

            return df || EqualityComparer<T?>.Default.Equals(first, second);
        }

        public static Boolean IsAnyDefaultOrEquals<T>(this T? first, T? second)
        {
            if (IsDefault(first) || IsDefault(second))
            {
                return true;
            }

            return EqualityComparer<T?>.Default.Equals(first, second);
        }

        public static T If<T>(this T value, Func<T, T> selector, Boolean condition)
        {
            if (selector is null)
            {
                throw new ArgumentNullException(nameof(selector));
            }

            return condition ? selector(value) : value;
        }
        
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
        
        public static T If<T, TArgument>(this T value, Func<T, TArgument, T> selector, TArgument argument, Boolean condition)
        {
            if (selector is null)
            {
                throw new ArgumentNullException(nameof(selector));
            }

            return condition ? selector(value, argument) : value;
        }
        
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
        
        public static T If<T, TArgument1, TArgument2>(this T value, Func<T, TArgument1, TArgument2, T> selector, TArgument1 first, TArgument2 second, Boolean condition)
        {
            if (selector is null)
            {
                throw new ArgumentNullException(nameof(selector));
            }

            return condition ? selector(value, first, second) : value;
        }
        
        public static T If<T, TArgument1, TArgument2>(this T value, Func<T, TArgument1, TArgument2, T> selector, TArgument1 first, TArgument2 second, Func<Boolean> condition)
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
        
        public static T If<T, TArgument1, TArgument2>(this T value, Func<T, TArgument1, TArgument2, T> selector, TArgument1 first, TArgument2 second, Func<T, Boolean> condition)
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
        
        public static T If<T, TArgument1, TArgument2, TArgument3>(this T value, Func<T, TArgument1, TArgument2, TArgument3, T> selector, TArgument1 first, TArgument2 second, TArgument3 third, Boolean condition)
        {
            if (selector is null)
            {
                throw new ArgumentNullException(nameof(selector));
            }

            return condition ? selector(value, first, second, third) : value;
        }
        
        public static T If<T, TArgument1, TArgument2, TArgument3>(this T value, Func<T, TArgument1, TArgument2, TArgument3, T> selector, TArgument1 first, TArgument2 second, TArgument3 third, Func<Boolean> condition)
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
        
        public static T If<T, TArgument1, TArgument2, TArgument3>(this T value, Func<T, TArgument1, TArgument2, TArgument3, T> selector, TArgument1 first, TArgument2 second, TArgument3 third, Func<T, Boolean> condition)
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

        public static T IfNot<T>(this T value, Func<T, T> selector, Boolean condition)
        {
            if (selector is null)
            {
                throw new ArgumentNullException(nameof(selector));
            }

            return !condition ? selector(value) : value;
        }
        
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
        
        public static T IfNot<T, TArgument>(this T value, Func<T, TArgument, T> selector, TArgument argument, Boolean condition)
        {
            if (selector is null)
            {
                throw new ArgumentNullException(nameof(selector));
            }

            return !condition ? selector(value, argument) : value;
        }
        
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
        
        public static T IfNot<T, TArgument1, TArgument2>(this T value, Func<T, TArgument1, TArgument2, T> selector, TArgument1 first, TArgument2 second, Boolean condition)
        {
            if (selector is null)
            {
                throw new ArgumentNullException(nameof(selector));
            }

            return !condition ? selector(value, first, second) : value;
        }
        
        public static T IfNot<T, TArgument1, TArgument2>(this T value, Func<T, TArgument1, TArgument2, T> selector, TArgument1 first, TArgument2 second, Func<Boolean> condition)
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
        
        public static T IfNot<T, TArgument1, TArgument2>(this T value, Func<T, TArgument1, TArgument2, T> selector, TArgument1 first, TArgument2 second, Func<T, Boolean> condition)
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
        
        public static T IfNot<T, TArgument1, TArgument2, TArgument3>(this T value, Func<T, TArgument1, TArgument2, TArgument3, T> selector, TArgument1 first, TArgument2 second, TArgument3 third, Boolean condition)
        {
            if (selector is null)
            {
                throw new ArgumentNullException(nameof(selector));
            }

            return !condition ? selector(value, first, second, third) : value;
        }
        
        public static T IfNot<T, TArgument1, TArgument2, TArgument3>(this T value, Func<T, TArgument1, TArgument2, TArgument3, T> selector, TArgument1 first, TArgument2 second, TArgument3 third, Func<Boolean> condition)
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
        
        public static T IfNot<T, TArgument1, TArgument2, TArgument3>(this T value, Func<T, TArgument1, TArgument2, TArgument3, T> selector, TArgument1 first, TArgument2 second, TArgument3 third, Func<T, Boolean> condition)
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

        private static MethodInfo MemberwiseCloneMethod { get; } = typeof(Object).GetMethod("MemberwiseClone", BindingFlags.NonPublic | BindingFlags.Instance)!;
        private static Converter<Object, Object> MemberwiseCloneDelegate { get; } = (Converter<Object, Object>) MemberwiseCloneMethod.CreateDelegate(typeof(Converter<Object, Object>));
        
        [return: NotNullIfNotNull("value")]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T? MemberwiseClone<T>(this T? value)
        {
            return value is not null ? (T) MemberwiseCloneDelegate.Invoke(value) : default;
        }

        /// <summary>
        /// Creates a deep copy (new instance with new instances of properties).
        /// </summary>
        /// <param name="original">The original object to copy.</param>
        public static T? DeepCopy<T>(this T? original)
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

                return default;
            }
            catch (Exception)
            {
                return default;
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

            if (visited.ContainsKey(original))
            {
                return visited[original];
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
                if (type.GetElementType()?.IsPrimitive() == false)
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

        private const BindingFlags DeepCopyFieldsBinding = BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.FlattenHierarchy;
        private static void DeepCopyFields(Object original, IDictionary<Object, Object?> visited, Object clone, IReflect type,
            BindingFlags binding = DeepCopyFieldsBinding, Func<FieldInfo, Boolean>? filter = null)
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