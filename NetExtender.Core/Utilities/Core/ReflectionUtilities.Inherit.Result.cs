// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Collections;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Diagnostics.CodeAnalysis;
using System.Reflection;
using System.Runtime.CompilerServices;
using NetExtender.Cecil;
using NetExtender.Utilities.Types;

namespace NetExtender.Utilities.Core
{
    public static partial class ReflectionUtilities
    {
        public static ReflectionInheritResult Get(this ImmutableDictionary<MonoCecilType, ReflectionInheritResult> source, MonoCecilType type)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            if (type is null)
            {
                throw new ArgumentNullException(nameof(type));
            }

            return source.TryGetValue(type, out ReflectionInheritResult? result) ? result : ReflectionInheritResult.Empty;
        }

        public static ReflectionInheritResult Get(this ImmutableDictionary<Assembly, ReflectionInheritResult> source, Assembly assembly)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            if (assembly is null)
            {
                throw new ArgumentNullException(nameof(assembly));
            }

            return source.TryGetValue(assembly, out ReflectionInheritResult? result) ? result : ReflectionInheritResult.Empty;
        }

        public static ReflectionInheritResult Get(this ImmutableDictionary<MonoCecilType, ImmutableDictionary<Assembly, ReflectionInheritResult>> source, Assembly assembly, MonoCecilType type)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            if (assembly is null)
            {
                throw new ArgumentNullException(nameof(assembly));
            }

            if (type is null)
            {
                throw new ArgumentNullException(nameof(type));
            }

            return source.TryGetValue(type, out ImmutableDictionary<Assembly, ReflectionInheritResult>? result) ? result.Get(assembly) : ReflectionInheritResult.Empty;
        }
    }

    public sealed class Inherit : IReadOnlyDictionary<MonoCecilType, ReflectionInheritResult>
    {
        public readonly struct Result : IReadOnlyDictionary<MonoCecilType, ReflectionInheritResult>
        {
            public static implicit operator Inherit(Result value)
            {
                return value.Type;
            }

            public static implicit operator AttributeInherit(Result value)
            {
                return value.Attributes;
            }

            public Inherit Type { get; }
            public AttributeInherit Attributes { get; }

            public TypeMap<MonoCecilType, TypeMap<Assembly, ReflectionInheritResult>> Assembly
            {
                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                get
                {
                    return Type.Assembly;
                }
            }

            public Int32 Count
            {
                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                get
                {
                    return Type.Count;
                }
            }

            public IEnumerable<MonoCecilType> Keys
            {
                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                get
                {
                    return Type.Keys;
                }
            }

            public IEnumerable<ReflectionInheritResult> Values
            {
                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                get
                {
                    return Type.Values;
                }
            }

            public Result(Inherit type, AttributeInherit attribute)
            {
                Type = type ?? throw new ArgumentNullException(nameof(type));
                Attributes = attribute ?? throw new ArgumentNullException(nameof(attribute));
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public Boolean Contains(MonoCecilType key)
            {
                return Type.Contains(key);
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            Boolean IReadOnlyDictionary<MonoCecilType, ReflectionInheritResult>.ContainsKey(MonoCecilType key)
            {
                return Contains(key);
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public Boolean TryGetValue(MonoCecilType key, [MaybeNullWhen(false)] out ReflectionInheritResult value)
            {
                return Type.TryGetValue(key, out value);
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public TypeMap<MonoCecilType, ReflectionInheritResult>.Enumerator GetEnumerator()
            {
                return Type.GetEnumerator();
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            IEnumerator<KeyValuePair<MonoCecilType, ReflectionInheritResult>> IEnumerable<KeyValuePair<MonoCecilType, ReflectionInheritResult>>.GetEnumerator()
            {
                return GetEnumerator();
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            IEnumerator IEnumerable.GetEnumerator()
            {
                return GetEnumerator();
            }

            public ReflectionInheritResult this[MonoCecilType key]
            {
                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                get
                {
                    return Type[key];
                }
            }

            public ReflectionInheritResult this[Assembly assembly, MonoCecilType key]
            {
                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                get
                {
                    return Type[assembly, key];
                }
            }
        }

        private TypeMap<MonoCecilType, ReflectionInheritResult> Type { get; }
        public TypeMap<MonoCecilType, TypeMap<Assembly, ReflectionInheritResult>> Assembly { get; }

        public Int32 Count
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                return Type.Count;
            }
        }

        public IEnumerable<MonoCecilType> Keys
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                return Type.Keys;
            }
        }

        public IEnumerable<ReflectionInheritResult> Values
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                return Type.Values;
            }
        }

        internal Inherit(TypeMap<MonoCecilType, ReflectionInheritResult> inherit)
        {
            Type = inherit ?? throw new ArgumentNullException(nameof(inherit));
            Assembly = TypeMap<MonoCecilType, TypeMap<Assembly, ReflectionInheritResult>>.Create<ReflectionInheritResult, TypeMap<MonoCecilType, ReflectionInheritResult>.Enumerator>(Type.GetEnumerator(), Assemblies);
        }

        [MethodImpl(MethodImplOptions.AggressiveOptimization)]
        private static TypeMap<Assembly, ReflectionInheritResult> Assemblies(ReflectionInheritResult result)
        {
            Dictionary<Assembly, ConcurrentHashSet<MonoCecilType>> assemblies = new Dictionary<Assembly, ConcurrentHashSet<MonoCecilType>>(500);

            foreach (MonoCecilType type in result)
            {
                if (type.Assembly is { } assembly)
                {
                    DictionaryBaseUtilities.GetOrNew(assemblies, assembly).Add(type);
                }
            }

            return TypeMap<Assembly, ReflectionInheritResult>.Create<ConcurrentHashSet<MonoCecilType>, Dictionary<Assembly, ConcurrentHashSet<MonoCecilType>>.Enumerator>(assemblies.GetEnumerator(), ReflectionInheritResult.Create);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Boolean Contains(MonoCecilType key)
        {
            return Type.Contains(key);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        Boolean IReadOnlyDictionary<MonoCecilType, ReflectionInheritResult>.ContainsKey(MonoCecilType key)
        {
            return Contains(key);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Boolean TryGetValue(MonoCecilType key, [MaybeNullWhen(false)] out ReflectionInheritResult value)
        {
            return Type.TryGetValue(key, out value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public TypeMap<MonoCecilType, ReflectionInheritResult>.Enumerator GetEnumerator()
        {
            return Type.GetEnumerator();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        IEnumerator<KeyValuePair<MonoCecilType, ReflectionInheritResult>> IEnumerable<KeyValuePair<MonoCecilType, ReflectionInheritResult>>.GetEnumerator()
        {
            return GetEnumerator();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public ReflectionInheritResult this[MonoCecilType key]
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                return Type.TryGetValue(key, out ReflectionInheritResult? result) ? result : ReflectionInheritResult.Empty;
            }
        }

        public ReflectionInheritResult this[Assembly assembly, MonoCecilType key]
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                return Assembly.TryGetValue(key, out TypeMap<Assembly, ReflectionInheritResult>? immutable) && immutable.TryGetValue(assembly, out ReflectionInheritResult? result) ? result : ReflectionInheritResult.Empty;
            }
        }
    }

    public sealed class AttributeInherit : IReadOnlyDictionary<MonoCecilType, ReflectionInheritResult>
    {
        private TypeMap<MonoCecilType, ReflectionInheritResult> Type { get; }
        private TypeMap<MonoCecilType, ReflectionInheritResult> Direct { get; }

        public AttributeAssemblyInherit Assembly { get; }

        public Int32 Count
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                return Type.Count;
            }
        }

        public IEnumerable<MonoCecilType> Keys
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                return Type.Keys;
            }
        }

        public IEnumerable<ReflectionInheritResult> Values
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                return Type.Values;
            }
        }

        internal AttributeInherit(TypeMap<MonoCecilType, ReflectionInheritResult> type, TypeMap<MonoCecilType, ReflectionInheritResult> direct)
        {
            Type = type ?? throw new ArgumentNullException(nameof(type));
            Direct = direct ?? throw new ArgumentNullException(nameof(direct));
            Assembly = new AttributeAssemblyInherit(Type, Direct);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Boolean Contains(MonoCecilType key)
        {
            return Type.Contains(key);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        Boolean IReadOnlyDictionary<MonoCecilType, ReflectionInheritResult>.ContainsKey(MonoCecilType key)
        {
            return Contains(key);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Boolean TryGetValue(MonoCecilType key, [MaybeNullWhen(false)] out ReflectionInheritResult value)
        {
            return Type.TryGetValue(key, out value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public TypeMap<MonoCecilType, ReflectionInheritResult>.Enumerator GetEnumerator()
        {
            return Type.GetEnumerator();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        IEnumerator<KeyValuePair<MonoCecilType, ReflectionInheritResult>> IEnumerable<KeyValuePair<MonoCecilType, ReflectionInheritResult>>.GetEnumerator()
        {
            return GetEnumerator();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public ReflectionInheritResult this[MonoCecilType key]
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                return Type.TryGetValue(key, out ReflectionInheritResult? result) ? result : ReflectionInheritResult.Empty;
            }
        }
    }

    public sealed class AttributeAssemblyInherit : IReadOnlyDictionary<MonoCecilType, TypeMap<Assembly, ReflectionInheritResult>>
    {
        private TypeMap<MonoCecilType, TypeMap<Assembly, ReflectionInheritResult>> Type { get; }
        private TypeMap<MonoCecilType, TypeMap<Assembly, ReflectionInheritResult>> Direct { get; }

        public Int32 Count
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                return Type.Count;
            }
        }

        public IEnumerable<MonoCecilType> Keys
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                return Type.Keys;
            }
        }

        public IEnumerable<TypeMap<Assembly, ReflectionInheritResult>> Values
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                return Type.Values;
            }
        }

        internal AttributeAssemblyInherit(TypeMap<MonoCecilType, ReflectionInheritResult> type, TypeMap<MonoCecilType, ReflectionInheritResult> direct)
        {
            if (type is null)
            {
                throw new ArgumentNullException(nameof(type));
            }

            if (direct is null)
            {
                throw new ArgumentNullException(nameof(direct));
            }

            Type = TypeMap<MonoCecilType, TypeMap<Assembly, ReflectionInheritResult>>.Create<ReflectionInheritResult, TypeMap<MonoCecilType, ReflectionInheritResult>.Enumerator>(type.GetEnumerator(), Assemblies);
            Direct = TypeMap<MonoCecilType, TypeMap<Assembly, ReflectionInheritResult>>.Create<ReflectionInheritResult, TypeMap<MonoCecilType, ReflectionInheritResult>.Enumerator>(direct.GetEnumerator(), Assemblies);
        }

        [MethodImpl(MethodImplOptions.AggressiveOptimization)]
        private static TypeMap<Assembly, ReflectionInheritResult> Assemblies(ReflectionInheritResult result)
        {
            Dictionary<Assembly, ConcurrentHashSet<MonoCecilType>> assemblies = new Dictionary<Assembly, ConcurrentHashSet<MonoCecilType>>();

            foreach (MonoCecilType type in result)
            {
                if (type.Assembly is { } assembly)
                {
                    DictionaryBaseUtilities.GetOrNew(assemblies, assembly).Add(type);
                }
            }

            return TypeMap<Assembly, ReflectionInheritResult>.Create<ConcurrentHashSet<MonoCecilType>, Dictionary<Assembly, ConcurrentHashSet<MonoCecilType>>.Enumerator>(assemblies.GetEnumerator(), ReflectionInheritResult.Create);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Boolean Contains(MonoCecilType key)
        {
            return Type.Contains(key);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        Boolean IReadOnlyDictionary<MonoCecilType, TypeMap<Assembly, ReflectionInheritResult>>.ContainsKey(MonoCecilType key)
        {
            return Contains(key);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Boolean TryGetValue(MonoCecilType key, [MaybeNullWhen(false)] out TypeMap<Assembly, ReflectionInheritResult> value)
        {
            return Type.TryGetValue(key, out value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IEnumerator<KeyValuePair<MonoCecilType, TypeMap<Assembly, ReflectionInheritResult>>> GetEnumerator()
        {
            return Type.GetEnumerator();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public TypeMap<Assembly, ReflectionInheritResult> this[MonoCecilType key]
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                return Type.TryGetValue(key, out TypeMap<Assembly, ReflectionInheritResult>? result) ? result : TypeMap<Assembly, ReflectionInheritResult>.Empty;
            }
        }

        public ReflectionInheritResult this[Assembly assembly, MonoCecilType key]
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                return Type.TryGetValue(key, out TypeMap<Assembly, ReflectionInheritResult>? immutable) && immutable.TryGetValue(assembly, out ReflectionInheritResult? result) ? result : ReflectionInheritResult.Empty;
            }
        }
    }
}