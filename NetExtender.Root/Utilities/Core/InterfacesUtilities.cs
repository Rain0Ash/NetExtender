using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using NetExtender.Utilities.Types;

#if NET8_0_OR_GREATER
using System.Collections.Frozen;
#endif

namespace NetExtender.Utilities.Core
{
    public static class InterfacesUtilities
    {
        private static readonly ConcurrentDictionary<Assembly, IReadOnlySet<Type>> _interfaces = new ConcurrentDictionary<Assembly, IReadOnlySet<Type>>();
        private static readonly ConcurrentDictionary<Type, IReadOnlySet<Type>> _safe = new ConcurrentDictionary<Type, IReadOnlySet<Type>>();

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<Type> GetInterfaces(this AppDomain domain)
        {
            if (domain is null)
            {
                throw new ArgumentNullException(nameof(domain));
            }

            return domain.GetAssemblies().GetInterfaces();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<Type> GetInterfaces(this IEnumerable<Assembly?> source)
        {
            return EnumerableBaseUtilities.WhereNotNull(source).SelectMany(static assembly => assembly.GetInterfacesUnsafe());
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Type[] GetInterfaces(this Assembly assembly)
        {
            return GetInterfacesUnsafe(assembly).ToArray();
        }

        [MethodImpl(MethodImplOptions.AggressiveOptimization)]
        public static IReadOnlySet<Type> GetInterfacesUnsafe(this Assembly assembly)
        {
            if (assembly is null)
            {
                throw new ArgumentNullException(nameof(assembly));
            }

            return _interfaces.GetOrAdd(assembly, static assembly =>
            {
                IEnumerable<Type> result = assembly.GetSafeTypesUnsafe().Where(static type => type.IsInterface);
#if NET8_0_OR_GREATER
                return result.ToFrozenSet();
#else
                HashSet<Type> set = new HashSet<Type>(result);
                set.TrimExcess();
                return set;
#endif
            });
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Type[] GetSafeInterfaces(this Type type)
        {
            return GetSafeInterfacesUnsafe(type).ToArray();
        }

        [MethodImpl(MethodImplOptions.AggressiveOptimization)]
        internal static IReadOnlySet<Type> GetSafeInterfacesUnsafe(this Type type)
        {
            if (type is null)
            {
                throw new ArgumentNullException(nameof(type));
            }

            return _safe.GetOrAdd(type, static type =>
            {
                IEnumerable<Type> result;

                try
                {
                    result = type.GetInterfaces();
                }
                catch (ReflectionTypeLoadException exception)
                {
                    result = EnumerableBaseUtilities.WhereNotNull(exception.Types);
                }
#if NET8_0_OR_GREATER
                return result.ToFrozenSet();
#else
                HashSet<Type> set = new HashSet<Type>(result);
                set.TrimExcess();
                return set;
#endif
            });
        }
    }
}