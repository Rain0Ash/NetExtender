// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Collections.Generic;
using System.Reflection;

namespace NetExtender.Utils.Types
{
    public static partial class GenericUtils
    {
        public static T AsDefault<T>(this T value, T alternate)
        {
            return IsDefault(value) ? alternate : value;
        }

        public static Boolean IsDefault<T>(this T value)
        {
            return EqualityComparer<T>.Default.Equals(value, default);
        }

        public static Boolean IsNotDefault<T>(this T value)
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
        public static Boolean IsEquals<T>(this T first, T second)
        {
            if (IsDefault(first) || IsDefault(second))
            {
                return false;
            }

            return EqualityComparer<T>.Default.Equals(first, second);
        }

        public static Boolean IsDefaultOrEquals<T>(this T first, T second)
        {
            Boolean df = IsDefault(first);

            if (df != IsDefault(second))
            {
                return false;
            }

            return df || EqualityComparer<T>.Default.Equals(first, second);
        }

        public static Boolean IsAnyDefaultOrEquals<T>(this T first, T second)
        {
            if (IsDefault(first) || IsDefault(second))
            {
                return true;
            }

            return EqualityComparer<T>.Default.Equals(first, second);
        }

        private static readonly MethodInfo MemberwiseCloneMethod = typeof(Object).GetMethod("MemberwiseClone", BindingFlags.NonPublic | BindingFlags.Instance);

        /// <summary>
        /// Creates a deep copy (new instance with new instances of properties).
        /// </summary>
        /// <param name="original">The original object to copy.</param>
        public static T DeepCopy<T>(this T original)
        {
            try
            {
                if (DeepCopyInternal(original, new Dictionary<Object, Object>(ReferenceEqualityComparer.Instance)) is T generic)
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
    }
}