using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;

namespace NetExtender.Utilities.Core
{
    public static class MemberInfoUtilities
    {
        [MethodImpl(MethodImplOptions.AggressiveOptimization)]
        public static Boolean IsAbstract(this MemberInfo info)
        {
            if (info is null)
            {
                throw new ArgumentNullException(nameof(info));
            }

            return info switch
            {
                Type type => type.IsAbstract,
                PropertyInfo property => property.IsAbstract(),
                MethodBase method => method.IsAbstract,
                EventInfo @event => @event.IsAbstract(),
                _ => false
            };
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<String> Name(this IEnumerable<MemberInfo> source)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            return source.Select(static member => member.Name);
        }

        [MethodImpl(MethodImplOptions.AggressiveOptimization)]
        public static Boolean HasName(this MemberInfo member, String? name)
        {
            if (member is null)
            {
                throw new ArgumentNullException(nameof(member));
            }

            if (String.IsNullOrEmpty(name))
            {
                return false;
            }

            if (member.Name == name)
            {
                return true;
            }

            if (member.Name.Length < name.Length)
            {
                return false;
            }

            Int32 index = member.Name.Length - name.Length - 1;

            if (index >= 0 && member.Name[index] == '.')
            {
                return member.Name.EndsWith(name, StringComparison.Ordinal);
            }

            return false;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ImmutableHashSet<String> ToNameSet(this IEnumerable<MemberInfo> source)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            return source.Name().ToImmutableHashSet();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Type MemberType(this MemberInfo member)
        {
            if (member is null)
            {
                throw new ArgumentNullException(nameof(member));
            }

            return member switch
            {
                FieldInfo field => field.FieldType,
                PropertyInfo property => property.PropertyType,
                _ => throw new ArgumentException($"Member '{member.GetType().Name}' is not '{nameof(FieldInfo)}' or '{nameof(PropertyInfo)}'.")
            };
        }

        public static IEnumerable<MemberInfo> GetMembers(this Type type, Type attribute)
        {
            if (type is null)
            {
                throw new ArgumentNullException(nameof(type));
            }

            if (attribute is null)
            {
                throw new ArgumentNullException(nameof(attribute));
            }

            foreach (MemberInfo member in type.GetMembers())
            {
                if (member.HasAttribute(attribute))
                {
                    yield return member;
                }
            }
        }

        public static IEnumerable<MemberInfo> GetMembers(this Type type, Type attribute, Boolean inherit)
        {
            if (type is null)
            {
                throw new ArgumentNullException(nameof(type));
            }

            if (attribute is null)
            {
                throw new ArgumentNullException(nameof(attribute));
            }

            foreach (MemberInfo member in type.GetMembers())
            {
                if (member.HasAttribute(attribute, inherit))
                {
                    yield return member;
                }
            }
        }

        public static IEnumerable<MemberInfo> GetMembers(this Type type, Type attribute, BindingFlags binding)
        {
            if (type is null)
            {
                throw new ArgumentNullException(nameof(type));
            }

            if (attribute is null)
            {
                throw new ArgumentNullException(nameof(attribute));
            }

            foreach (MemberInfo member in type.GetMembers(binding))
            {
                if (member.HasAttribute(attribute))
                {
                    yield return member;
                }
            }
        }

        public static IEnumerable<MemberInfo> GetMembers(this Type type, Type attribute, BindingFlags binding, Boolean inherit)
        {
            if (type is null)
            {
                throw new ArgumentNullException(nameof(type));
            }

            if (attribute is null)
            {
                throw new ArgumentNullException(nameof(attribute));
            }

            foreach (MemberInfo member in type.GetMembers(binding))
            {
                if (member.HasAttribute(attribute, inherit))
                {
                    yield return member;
                }
            }
        }

        public static IEnumerable<MemberInfo> GetMembers<TAttribute>(this Type type) where TAttribute : Attribute
        {
            if (type is null)
            {
                throw new ArgumentNullException(nameof(type));
            }

            MemberInfo[] members = type.GetMembers();
            return members.Where(static member => member.HasAttribute<TAttribute>());
        }

        public static IEnumerable<MemberInfo> GetMembers<TAttribute>(this Type type, Boolean inherit) where TAttribute : Attribute
        {
            if (type is null)
            {
                throw new ArgumentNullException(nameof(type));
            }

            MemberInfo[] members = type.GetMembers();
            return inherit ? members.Where(static member => member.HasAttribute<TAttribute>(false)) : members.Where(static member => member.HasAttribute<TAttribute>(true));
        }

        public static IEnumerable<MemberInfo> GetMembers<TAttribute>(this Type type, BindingFlags binding) where TAttribute : Attribute
        {
            if (type is null)
            {
                throw new ArgumentNullException(nameof(type));
            }

            MemberInfo[] members = type.GetMembers(binding);
            return members.Where(static member => member.HasAttribute<TAttribute>());
        }

        public static IEnumerable<MemberInfo> GetMembers<TAttribute>(this Type type, BindingFlags binding, Boolean inherit) where TAttribute : Attribute
        {
            if (type is null)
            {
                throw new ArgumentNullException(nameof(type));
            }

            MemberInfo[] members = type.GetMembers(binding);
            return inherit ? members.Where(static member => member.HasAttribute<TAttribute>(false)) : members.Where(static member => member.HasAttribute<TAttribute>(true));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<MemberInfo> Where<T>(this IEnumerable<MemberInfo> source)
        {
            return Where(source, typeof(T));
        }

        public static IEnumerable<MemberInfo> Where(this IEnumerable<MemberInfo> source, Type type)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            if (type is null)
            {
                throw new ArgumentNullException(nameof(type));
            }

            foreach (MemberInfo member in source)
            {
                if (member.MemberType() == type)
                {
                    yield return member;
                }
            }
        }
    }
}