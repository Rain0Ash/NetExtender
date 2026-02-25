using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using NetExtender.Cecil;
using NetExtender.Types.Comparers;
using NetExtender.Utilities.Types;

#if NET8_0_OR_GREATER
using System.Collections.Frozen;
#endif

namespace NetExtender.Utilities.Core
{
    public static class AssemblyUtilities
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static String Name(this Assembly assembly)
        {
            if (assembly is null)
            {
                throw new ArgumentNullException(nameof(assembly));
            }

            AssemblyName name = assembly.GetName();
            return name.Name ?? name.FullName;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<String> Name(this IEnumerable<Assembly?> source)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            return EnumerableBaseUtilities.WhereNotNull(source).Select(Name);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<String> FullName(this IEnumerable<Assembly?> source)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            return EnumerableBaseUtilities.WhereNotNull(source).Select(static assembly => assembly.GetName().FullName);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<String?> GetNamespaces(this Assembly assembly)
        {
            if (assembly is null)
            {
                throw new ArgumentNullException(nameof(assembly));
            }

            return assembly.GetSafeTypesUnsafe().Select(static type => type.Namespace);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<Type> GetTypes(this AppDomain domain)
        {
            if (domain is null)
            {
                throw new ArgumentNullException(nameof(domain));
            }

            return domain.GetAssemblies().GetTypes();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<MonoCecilType> GetCecilTypes(this AppDomain domain)
        {
            if (domain is null)
            {
                throw new ArgumentNullException(nameof(domain));
            }

            return domain.GetAssemblies().GetCecilTypes();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<Type> GetTypes(this IEnumerable<Assembly?> source)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            return EnumerableBaseUtilities.WhereNotNull(source).SelectMany(static assembly => assembly.GetSafeTypesUnsafe());
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<MonoCecilType> GetCecilTypes(this IEnumerable<Assembly?> source)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            return EnumerableBaseUtilities.WhereNotNull(source).SelectMany<Assembly, MonoCecilType>(GetCecilTypes);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static TypeSet GetCecilTypes(this Assembly assembly)
        {
            return MonoCecilType.From(assembly);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Type[] GetSafeTypes(this Assembly assembly)
        {
            return GetSafeTypesUnsafe(assembly).ToArray();
        }

        private static readonly ConcurrentDictionary<Assembly, IReadOnlySet<Type>> _safe = new ConcurrentDictionary<Assembly, IReadOnlySet<Type>>();

        [MethodImpl(MethodImplOptions.AggressiveOptimization)]
        internal static IReadOnlySet<Type> GetSafeTypesUnsafe(this Assembly assembly)
        {
            if (assembly is null)
            {
                throw new ArgumentNullException(nameof(assembly));
            }

            return _safe.GetOrAdd(assembly, static assembly =>
            {
                IEnumerable<Type> result;

                try
                {
                    result = assembly.GetTypes();
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

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<Assembly> OrderBy(this IEnumerable<Assembly> source, AssemblyComparison comparison)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            AssemblyComparer comparer = comparison;
            return source.OrderBy(static assembly => assembly, comparer);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<Assembly> OrderByDescending(this IEnumerable<Assembly> source, AssemblyComparison comparison)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            AssemblyComparer comparer = comparison;
            return source.OrderByDescending(static assembly => assembly, comparer);
        }
    }
}