using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;

namespace NetExtender.Utilities.Core
{
    public static class FieldInfoUtilities
    {
        public static IEnumerable<FieldInfo> GetFields(this Type type, Type attribute)
        {
            if (type is null)
            {
                throw new ArgumentNullException(nameof(type));
            }

            if (attribute is null)
            {
                throw new ArgumentNullException(nameof(attribute));
            }

            foreach (FieldInfo field in type.GetFields())
            {
                if (field.HasAttribute(attribute))
                {
                    yield return field;
                }
            }
        }

        public static IEnumerable<FieldInfo> GetFields(this Type type, Type attribute, Boolean inherit)
        {
            if (type is null)
            {
                throw new ArgumentNullException(nameof(type));
            }

            if (attribute is null)
            {
                throw new ArgumentNullException(nameof(attribute));
            }

            foreach (FieldInfo field in type.GetFields())
            {
                if (field.HasAttribute(attribute, inherit))
                {
                    yield return field;
                }
            }
        }

        public static IEnumerable<FieldInfo> GetFields(this Type type, Type attribute, BindingFlags binding)
        {
            if (type is null)
            {
                throw new ArgumentNullException(nameof(type));
            }

            if (attribute is null)
            {
                throw new ArgumentNullException(nameof(attribute));
            }

            foreach (FieldInfo field in type.GetFields(binding))
            {
                if (field.HasAttribute(attribute))
                {
                    yield return field;
                }
            }
        }

        public static IEnumerable<FieldInfo> GetFields(this Type type, Type attribute, BindingFlags binding, Boolean inherit)
        {
            if (type is null)
            {
                throw new ArgumentNullException(nameof(type));
            }

            if (attribute is null)
            {
                throw new ArgumentNullException(nameof(attribute));
            }

            foreach (FieldInfo field in type.GetFields(binding))
            {
                if (field.HasAttribute(attribute, inherit))
                {
                    yield return field;
                }
            }
        }

        public static IEnumerable<FieldInfo> GetFields<TAttribute>(this Type type) where TAttribute : Attribute
        {
            if (type is null)
            {
                throw new ArgumentNullException(nameof(type));
            }

            FieldInfo[] fields = type.GetFields();
            return fields.Where(static field => field.HasAttribute<TAttribute>());
        }

        public static IEnumerable<FieldInfo> GetFields<TAttribute>(this Type type, Boolean inherit) where TAttribute : Attribute
        {
            if (type is null)
            {
                throw new ArgumentNullException(nameof(type));
            }

            FieldInfo[] fields = type.GetFields();
            return inherit ? fields.Where(static field => field.HasAttribute<TAttribute>(false)) : fields.Where(static field => field.HasAttribute<TAttribute>(true));
        }

        public static IEnumerable<FieldInfo> GetFields<TAttribute>(this Type type, BindingFlags binding) where TAttribute : Attribute
        {
            if (type is null)
            {
                throw new ArgumentNullException(nameof(type));
            }

            FieldInfo[] fields = type.GetFields(binding);
            return fields.Where(static field => field.HasAttribute<TAttribute>());
        }

        public static IEnumerable<FieldInfo> GetFields<TAttribute>(this Type type, BindingFlags binding, Boolean inherit) where TAttribute : Attribute
        {
            if (type is null)
            {
                throw new ArgumentNullException(nameof(type));
            }

            FieldInfo[] fields = type.GetFields(binding);
            return inherit ? fields.Where(static field => field.HasAttribute<TAttribute>(false)) : fields.Where(static field => field.HasAttribute<TAttribute>(true));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<FieldInfo> Where<T>(this IEnumerable<FieldInfo> source)
        {
            return Where(source, typeof(T));
        }

        public static IEnumerable<FieldInfo> Where(this IEnumerable<FieldInfo> source, Type type)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            if (type is null)
            {
                throw new ArgumentNullException(nameof(type));
            }

            foreach (FieldInfo field in source)
            {
                if (field.FieldType == type)
                {
                    yield return field;
                }
            }
        }
    }
}