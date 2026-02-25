using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace NetExtender.Utilities.Core
{
    public static class ConstructorInfoUtilities
    {
        public static IEnumerable<ConstructorInfo> GetConstructors(this Type type, Type attribute)
        {
            if (type is null)
            {
                throw new ArgumentNullException(nameof(type));
            }

            if (attribute is null)
            {
                throw new ArgumentNullException(nameof(attribute));
            }

            foreach (ConstructorInfo constructor in type.GetConstructors())
            {
                if (constructor.HasAttribute(attribute))
                {
                    yield return constructor;
                }
            }
        }

        public static IEnumerable<ConstructorInfo> GetConstructors(this Type type, Type attribute, Boolean inherit)
        {
            if (type is null)
            {
                throw new ArgumentNullException(nameof(type));
            }

            if (attribute is null)
            {
                throw new ArgumentNullException(nameof(attribute));
            }

            foreach (ConstructorInfo constructor in type.GetConstructors())
            {
                if (constructor.HasAttribute(attribute, inherit))
                {
                    yield return constructor;
                }
            }
        }

        public static IEnumerable<ConstructorInfo> GetConstructors(this Type type, Type attribute, BindingFlags binding)
        {
            if (type is null)
            {
                throw new ArgumentNullException(nameof(type));
            }

            if (attribute is null)
            {
                throw new ArgumentNullException(nameof(attribute));
            }

            foreach (ConstructorInfo constructor in type.GetConstructors(binding))
            {
                if (constructor.HasAttribute(attribute))
                {
                    yield return constructor;
                }
            }
        }

        public static IEnumerable<ConstructorInfo> GetConstructors(this Type type, Type attribute, BindingFlags binding, Boolean inherit)
        {
            if (type is null)
            {
                throw new ArgumentNullException(nameof(type));
            }

            if (attribute is null)
            {
                throw new ArgumentNullException(nameof(attribute));
            }

            foreach (ConstructorInfo constructor in type.GetConstructors(binding))
            {
                if (constructor.HasAttribute(attribute, inherit))
                {
                    yield return constructor;
                }
            }
        }

        public static IEnumerable<ConstructorInfo> GetConstructors<TAttribute>(this Type type) where TAttribute : Attribute
        {
            if (type is null)
            {
                throw new ArgumentNullException(nameof(type));
            }

            ConstructorInfo[] constructors = type.GetConstructors();
            return constructors.Where(static constructor => constructor.HasAttribute<TAttribute>());
        }

        public static IEnumerable<ConstructorInfo> GetConstructors<TAttribute>(this Type type, Boolean inherit) where TAttribute : Attribute
        {
            if (type is null)
            {
                throw new ArgumentNullException(nameof(type));
            }

            ConstructorInfo[] constructors = type.GetConstructors();
            return inherit ? constructors.Where(static constructor => constructor.HasAttribute<TAttribute>(false)) : constructors.Where(static constructor => constructor.HasAttribute<TAttribute>(true));
        }

        public static IEnumerable<ConstructorInfo> GetConstructors<TAttribute>(this Type type, BindingFlags binding) where TAttribute : Attribute
        {
            if (type is null)
            {
                throw new ArgumentNullException(nameof(type));
            }

            ConstructorInfo[] constructors = type.GetConstructors(binding);
            return constructors.Where(static constructor => constructor.HasAttribute<TAttribute>());
        }

        public static IEnumerable<ConstructorInfo> GetConstructors<TAttribute>(this Type type, BindingFlags binding, Boolean inherit) where TAttribute : Attribute
        {
            if (type is null)
            {
                throw new ArgumentNullException(nameof(type));
            }

            ConstructorInfo[] constructors = type.GetConstructors(binding);
            return inherit ? constructors.Where(static constructor => constructor.HasAttribute<TAttribute>(false)) : constructors.Where(static constructor => constructor.HasAttribute<TAttribute>(true));
        }
    }
}