using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;

namespace NetExtender.Utilities.Core
{
    public static class AttributeUtilities
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean HasAttribute<T>(this MemberInfo member) where T : Attribute
        {
            if (member is null)
            {
                throw new ArgumentNullException(nameof(member));
            }

            return member.IsDefined(typeof(T));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean HasAttribute<T>(this MemberInfo member, Boolean inherit) where T : Attribute
        {
            if (member is null)
            {
                throw new ArgumentNullException(nameof(member));
            }

            return member.IsDefined(typeof(T), inherit);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean HasAttribute(this MemberInfo member, Type attribute)
        {
            if (member is null)
            {
                throw new ArgumentNullException(nameof(member));
            }

            if (attribute is null)
            {
                throw new ArgumentNullException(nameof(attribute));
            }

            return member.IsDefined(attribute);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean HasAttribute(this MemberInfo member, Type attribute, Boolean inherit)
        {
            if (member is null)
            {
                throw new ArgumentNullException(nameof(member));
            }

            if (attribute is null)
            {
                throw new ArgumentNullException(nameof(attribute));
            }

            return member.IsDefined(attribute, inherit);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T? GetCustomAttribute<T>(this MemberInfo member) where T : Attribute
        {
            if (member is null)
            {
                throw new ArgumentNullException(nameof(member));
            }

            return member.GetCustomAttribute(typeof(T)) as T;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T? GetCustomAttribute<T>(this MemberInfo member, Boolean inherit) where T : Attribute
        {
            if (member is null)
            {
                throw new ArgumentNullException(nameof(member));
            }

            return member.GetCustomAttribute(typeof(T), inherit) as T;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<T> GetCustomAttributes<T>(this MemberInfo member) where T : Attribute
        {
            if (member is null)
            {
                throw new ArgumentNullException(nameof(member));
            }

            return member.GetCustomAttributes(typeof(T)).OfType<T>();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<T> GetCustomAttributes<T>(this MemberInfo member, Boolean inherit) where T : Attribute
        {
            if (member is null)
            {
                throw new ArgumentNullException(nameof(member));
            }

            return member.GetCustomAttributes(typeof(T), inherit).OfType<T>();
        }
    }
}