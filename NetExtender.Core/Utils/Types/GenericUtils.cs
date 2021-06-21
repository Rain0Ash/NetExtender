// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Collections.Generic;
using System.Reflection;
using NetExtender.Utils.Core;

namespace NetExtender.Utils.Types
{
    public static class GenericUtils
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

        private static readonly MethodInfo MemberwiseCloneMethod = typeof(Object).GetMethod("MemberwiseClone", BindingFlags.NonPublic | BindingFlags.Instance)!;

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
                Type? array = type.GetElementType();

                if (array is not null && !array.IsPrimitive())
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