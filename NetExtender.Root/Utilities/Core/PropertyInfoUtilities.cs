using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Reflection.Emit;
using System.Runtime.CompilerServices;

namespace NetExtender.Utilities.Core
{
    public static class PropertyInfoUtilities
    {
        [MethodImpl(MethodImplOptions.AggressiveOptimization)]
        public static Boolean IsAbstract(this PropertyInfo info)
        {
            if (info is null)
            {
                throw new ArgumentNullException(nameof(info));
            }

            return info.GetMethod is { IsAbstract: true } || info.SetMethod is { IsAbstract: true };
        }

        public static (Func<TInstance, T>? Get, Action<TInstance, T>? Set) GetParentProperty<TInstance, T>(this PropertyInfo property) where TInstance : notnull
        {
            if (property is null)
            {
                throw new ArgumentNullException(nameof(property));
            }

            return (property.GetMethod is not null ? instance => property.GetParentGetMethod<T>(instance)!.Invoke() : null, property.SetMethod is not null ? (instance, value) => property.GetParentSetMethod<T>(instance)!.Invoke(value) : null);
        }

        public static (Func<T>? Get, Action<T>? Set) GetParentProperty<T>(this PropertyInfo property, Object instance)
        {
            if (property is null)
            {
                throw new ArgumentNullException(nameof(property));
            }

            if (instance is null)
            {
                throw new ArgumentNullException(nameof(instance));
            }

            return (property.GetParentGetMethod<T>(instance), property.GetParentSetMethod<T>(instance));
        }

        public static Func<T>? GetParentGetMethod<T>(this PropertyInfo property, Object instance)
        {
            if (property is null)
            {
                throw new ArgumentNullException(nameof(property));
            }

            if (instance is null)
            {
                throw new ArgumentNullException(nameof(instance));
            }

            return property.GetMethod?.GetParentMethod<Func<T>>(instance);
        }

        public static Action<T>? GetParentSetMethod<T>(this PropertyInfo property, Object instance)
        {
            if (property is null)
            {
                throw new ArgumentNullException(nameof(property));
            }

            if (instance is null)
            {
                throw new ArgumentNullException(nameof(instance));
            }

            return property.SetMethod?.GetParentMethod<Action<T>>(instance);
        }

        public static IEnumerable<PropertyInfo> GetProperties(this Type type, Type attribute)
        {
            if (type is null)
            {
                throw new ArgumentNullException(nameof(type));
            }

            if (attribute is null)
            {
                throw new ArgumentNullException(nameof(attribute));
            }

            foreach (PropertyInfo property in type.GetProperties())
            {
                if (property.HasAttribute(attribute))
                {
                    yield return property;
                }
            }
        }

        public static IEnumerable<PropertyInfo> GetProperties(this Type type, Type attribute, Boolean inherit)
        {
            if (type is null)
            {
                throw new ArgumentNullException(nameof(type));
            }

            if (attribute is null)
            {
                throw new ArgumentNullException(nameof(attribute));
            }

            foreach (PropertyInfo property in type.GetProperties())
            {
                if (property.HasAttribute(attribute, inherit))
                {
                    yield return property;
                }
            }
        }

        public static IEnumerable<PropertyInfo> GetProperties(this Type type, Type attribute, BindingFlags binding)
        {
            if (type is null)
            {
                throw new ArgumentNullException(nameof(type));
            }

            if (attribute is null)
            {
                throw new ArgumentNullException(nameof(attribute));
            }

            foreach (PropertyInfo property in type.GetProperties(binding))
            {
                if (property.HasAttribute(attribute))
                {
                    yield return property;
                }
            }
        }

        public static IEnumerable<PropertyInfo> GetProperties(this Type type, Type attribute, BindingFlags binding, Boolean inherit)
        {
            if (type is null)
            {
                throw new ArgumentNullException(nameof(type));
            }

            if (attribute is null)
            {
                throw new ArgumentNullException(nameof(attribute));
            }

            foreach (PropertyInfo property in type.GetProperties(binding))
            {
                if (property.HasAttribute(attribute, inherit))
                {
                    yield return property;
                }
            }
        }

        public static IEnumerable<PropertyInfo> GetProperties<TAttribute>(this Type type) where TAttribute : Attribute
        {
            if (type is null)
            {
                throw new ArgumentNullException(nameof(type));
            }

            PropertyInfo[] properties = type.GetProperties();
            return properties.Where(static property => property.HasAttribute<TAttribute>());
        }

        public static IEnumerable<PropertyInfo> GetProperties<TAttribute>(this Type type, Boolean inherit) where TAttribute : Attribute
        {
            if (type is null)
            {
                throw new ArgumentNullException(nameof(type));
            }

            PropertyInfo[] properties = type.GetProperties();
            return inherit ? properties.Where(static property => property.HasAttribute<TAttribute>(false)) : properties.Where(static property => property.HasAttribute<TAttribute>(true));
        }

        public static IEnumerable<PropertyInfo> GetProperties<TAttribute>(this Type type, BindingFlags binding) where TAttribute : Attribute
        {
            if (type is null)
            {
                throw new ArgumentNullException(nameof(type));
            }

            PropertyInfo[] properties = type.GetProperties(binding);
            return properties.Where(static property => property.HasAttribute<TAttribute>());
        }

        public static IEnumerable<PropertyInfo> GetProperties<TAttribute>(this Type type, BindingFlags binding, Boolean inherit) where TAttribute : Attribute
        {
            if (type is null)
            {
                throw new ArgumentNullException(nameof(type));
            }

            PropertyInfo[] properties = type.GetProperties(binding);
            return inherit ? properties.Where(static property => property.HasAttribute<TAttribute>(false)) : properties.Where(static property => property.HasAttribute<TAttribute>(true));
        }

        public static ParameterInfo[]? GetSafeIndexParameters(this PropertyInfo property)
        {
            if (property is null)
            {
                throw new ArgumentNullException(nameof(property));
            }

            try
            {
                return property.GetIndexParameters();
            }
            catch (TypeLoadException)
            {
                return null;
            }
            catch (NotSupportedException) when (property is PropertyBuilder builder)
            {
                return CodeGeneratorStorageUtilities.Parameters.PropertyBuilder.TryGetValue(builder, out ParameterInfo[]? parameters) ? parameters : null;
            }
            catch (NotSupportedException)
            {
                return null;
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Type[] GetIndexParameterTypes(this PropertyInfo property)
        {
            if (property is null)
            {
                throw new ArgumentNullException(nameof(property));
            }

            return property.GetIndexParameters().Types();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Type[]? GetSafeIndexParameterTypes(this PropertyInfo property)
        {
            if (property is null)
            {
                throw new ArgumentNullException(nameof(property));
            }

            return property.GetSafeIndexParameters()?.Types();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static String[] GetIndexParameterNames(this PropertyInfo property)
        {
            if (property is null)
            {
                throw new ArgumentNullException(nameof(property));
            }

            return property.GetIndexParameters().Names();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static String[]? GetSafeIndexParameterNames(this PropertyInfo property)
        {
            if (property is null)
            {
                throw new ArgumentNullException(nameof(property));
            }

            return property.GetSafeIndexParameters()?.Names();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<PropertyInfo> Where<T>(this IEnumerable<PropertyInfo> source)
        {
            return Where(source, typeof(T));
        }

        public static IEnumerable<PropertyInfo> Where(this IEnumerable<PropertyInfo> source, Type type)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            if (type is null)
            {
                throw new ArgumentNullException(nameof(type));
            }

            foreach (PropertyInfo property in source)
            {
                if (property.PropertyType == type)
                {
                    yield return property;
                }
            }
        }
    }
}