// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Collections;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;
using NetExtender.Cecil;
using NetExtender.Utilities.Types;

namespace NetExtender.Utilities.Core
{
    public static partial class ReflectionUtilities
    {
        [SuppressMessage("ReSharper", "MemberHidesStaticFromOuterClass")]
        internal sealed class Container
        {
            private static Func<ConcurrentHashSet<MonoCecilType>> Create { get; } = static () => new ConcurrentHashSet<MonoCecilType>(4, 4);

            private ConcurrentHashSet<MonoCecilType>? _inherit;
            public ConcurrentHashSet<MonoCecilType> Inherit
            {
                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                get
                {
                    return LazyInitializer.EnsureInitialized(ref _inherit, Create);
                }
            }

            public ConcurrentHashSet<MonoCecilType>? InheritSafe
            {
                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                get
                {
                    return Volatile.Read(ref _inherit);
                }
            }

            private ConcurrentHashSet<MonoCecilType>? _attribute;
            public ConcurrentHashSet<MonoCecilType> AttributeInherit
            {
                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                get
                {
                    return LazyInitializer.EnsureInitialized(ref _attribute, Create);
                }
            }

            public ConcurrentHashSet<MonoCecilType>? AttributeInheritSafe
            {
                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                get
                {
                    return Volatile.Read(ref _attribute);
                }
            }

            private ConcurrentHashSet<MonoCecilType>? _direct;
            public ConcurrentHashSet<MonoCecilType> DirectAttributeInherit
            {
                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                get
                {
                    return LazyInitializer.EnsureInitialized(ref _direct, Create);
                }
            }

            public ConcurrentHashSet<MonoCecilType>? DirectAttributeInheritSafe
            {
                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                get
                {
                    return Volatile.Read(ref _direct);
                }
            }

            private ConcurrentHashSet<MonoCecilType>? _type;
            public ConcurrentHashSet<MonoCecilType> TypeToDirectAttributes
            {
                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                get
                {
                    return LazyInitializer.EnsureInitialized(ref _type, Create);
                }
            }

            public ConcurrentHashSet<MonoCecilType>? TypeToDirectAttributesSafe
            {
                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                get
                {
                    return Volatile.Read(ref _type);
                }
            }
        }

        [SuppressMessage("ReSharper", "MemberHidesStaticFromOuterClass")]
        private readonly struct InheritEvaluator : IStruct<InheritEvaluator>
        {
            public static implicit operator Inherit.Result(InheritEvaluator value)
            {
                return new Inherit.Result(value.Type, value.Attribute);
            }

            private static ConcurrentDictionary<MonoCecilType, Container> Storage { get; } = new ConcurrentDictionary<MonoCecilType, Container>(Environment.ProcessorCount, 100000);
            private static ConcurrentHashSet<MonoCecilType> Traverse { get; } = new ConcurrentHashSet<MonoCecilType>();

            private Inherit Type { get; }
            private AttributeInherit Attribute { get; }

            public Boolean IsEmpty
            {
                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                get
                {
                    return Type is null;
                }
            }

            private InheritEvaluator(Inherit type, AttributeInherit attribute)
            {
                Type = type ?? throw new ArgumentNullException(nameof(type));
                Attribute = attribute ?? throw new ArgumentNullException(nameof(attribute));
            }

            public static InheritEvaluator Create()
            {
                ScanProcess.Wait();
                (TypeMap<MonoCecilType, ReflectionInheritResult> Inherit, TypeMap<MonoCecilType, ReflectionInheritResult> AttributeInherit, TypeMap<MonoCecilType, ReflectionInheritResult> DirectAttributeInherit) result = Builder(Storage);
                return new InheritEvaluator(new Inherit(result.Inherit), new AttributeInherit(result.AttributeInherit, result.DirectAttributeInherit));
            }

            [MethodImpl(MethodImplOptions.AggressiveOptimization)]
            private static (TypeMap<MonoCecilType, ReflectionInheritResult> Inherit, TypeMap<MonoCecilType, ReflectionInheritResult> AttributeInherit, TypeMap<MonoCecilType, ReflectionInheritResult> DirectAttributeInherit) Builder(ConcurrentDictionary<MonoCecilType, Container> storage)
            {
                KeyValuePair<MonoCecilType, Container>[] snapshot = storage.ToArray();
                Dictionary<MonoCecilType, ReflectionInheritResult> inherit = new Dictionary<MonoCecilType, ReflectionInheritResult>(snapshot.Length);
                Dictionary<MonoCecilType, ReflectionInheritResult> attributes = new Dictionary<MonoCecilType, ReflectionInheritResult>(snapshot.Length);
                Dictionary<MonoCecilType, ReflectionInheritResult> direct = new Dictionary<MonoCecilType, ReflectionInheritResult>(snapshot.Length);

                foreach ((MonoCecilType type, Container container) in snapshot)
                {
                    ConcurrentHashSet<MonoCecilType>? set = container.InheritSafe;

                    if (set is not null)
                    {
                        inherit[type] = ReflectionInheritResult.Create(set);
                    }

                    set = container.AttributeInheritSafe;

                    if (set is not null)
                    {
                        attributes[type] = ReflectionInheritResult.Create(set);
                    }

                    set = container.DirectAttributeInheritSafe;

                    if (set is not null)
                    {
                        direct[type] = ReflectionInheritResult.Create(set);
                    }
                }

                return (TypeMap<MonoCecilType, ReflectionInheritResult>.Create(inherit), TypeMap<MonoCecilType, ReflectionInheritResult>.Create(attributes), TypeMap<MonoCecilType, ReflectionInheritResult>.Create(direct));
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            private static void ScanHandler(MonoCecilType type)
            {
                switch (type)
                {
                    case null:
                        return;
                    case { IsInterface: true }:
                        InterfaceScanHandler(type);
                        return;
                    default:
                        TypeScanHandler(type);
                        return;
                }
            }

            [MethodImpl(MethodImplOptions.AggressiveOptimization)]
            [SuppressMessage("ReSharper", "CognitiveComplexity")]
            private static Exception? AttributeScanHandler(MonoCecilType type)
            {
                try
                {
                    if (type.Attributes is not { Count: > 0 } data)
                    {
                        goto ancestors;
                    }

                    ConcurrentHashSet<MonoCecilType> set = Storage.GetOrNew(type).TypeToDirectAttributes;

                    foreach (MonoCecilType attribute in data)
                    {
                        if (attribute == typeof(SuppressMessageAttribute))
                        {
                            continue;
                        }

                        set.Add(attribute);
                        Storage.GetOrNew(attribute).AttributeInherit.Add(type);
                        Storage.GetOrNew(attribute).DirectAttributeInherit.Add(type);

                        if (!attribute.IsGenericType)
                        {
                            continue;
                        }

                        if (attribute.GetGenericTypeDefinition() is not { } generic)
                        {
                            continue;
                        }

                        set.Add(generic);
                        Storage.GetOrNew(generic).AttributeInherit.Add(type);
                        Storage.GetOrNew(generic).DirectAttributeInherit.Add(type);
                    }

                    ancestors:
                    MonoCecilType? ancestor = type.BaseType;

                    while (ancestor is not null && ancestor != typeof(Object))
                    {
                        if (Storage.TryGetValue(ancestor, out Container? container) && container.TypeToDirectAttributesSafe is { } attributes)
                        {
                            foreach (MonoCecilType attribute in attributes)
                            {
                                Storage.GetOrNew(attribute).AttributeInherit.Add(type);
                            }
                        }

                        ancestor = ancestor.BaseType;
                    }

                    return null;
                }
                catch (Exception exception)
                {
                    return exception;
                }
            }

            [MethodImpl(MethodImplOptions.AggressiveOptimization)]
            [SuppressMessage("ReSharper", "CognitiveComplexity")]
            private static void TypeScanHandler(MonoCecilType type)
            {
                MonoCecilType real = type;

                start:
                if (!type.IsScan || !Traverse.Add(type))
                {
                    return;
                }

                foreach (MonoCecilType @interface in type.Interfaces)
                {
                    Storage.GetOrNew(@interface).Inherit.Add(type);

                    if (@interface.IsGenericType)
                    {
                        Storage.GetOrNew(@interface.GetGenericTypeDefinition()).Inherit.Add(type);
                    }

                    InterfaceScanHandler(@interface);
                }

                AttributeScanHandler(type);

                if (type.BaseType is not { } @base || @base == typeof(Object))
                {
                    return;
                }

                MonoCecilType? current = real;
                while (current is not null && current != typeof(Object))
                {
                    Storage.GetOrNew(current).Inherit.Add(real);

                    if (current.IsGenericType)
                    {
                        Storage.GetOrNew(current.GetGenericTypeDefinition()).Inherit.Add(real);
                    }

                    current = current.BaseType;
                }

                type = @base;
                goto start;
            }

            [MethodImpl(MethodImplOptions.AggressiveOptimization)]
            private static void InterfaceScanHandler(MonoCecilType type)
            {
                if (!type.IsScan || !Traverse.Add(type))
                {
                    return;
                }

                AttributeScanHandler(type);

                foreach (MonoCecilType @interface in type.Interfaces)
                {
                    Storage.GetOrNew(@interface).Inherit.Add(type);

                    if (@interface.IsGenericType)
                    {
                        Storage.GetOrNew(@interface.GetGenericTypeDefinition()).Inherit.Add(type);
                    }

                    InterfaceScanHandler(@interface);
                }
            }

            [MethodImpl(MethodImplOptions.AggressiveOptimization)]
            public static void Scan(IEnumerable<MonoCecilType> source)
            {
                if (source is null)
                {
                    throw new ArgumentNullException(nameof(source));
                }

                Parallel.ForEach(source, new ParallelOptions { MaxDegreeOfParallelism = Environment.ProcessorCount }, ScanHandler);
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static Boolean Scan()
        {
            return Scan(AppDomain.CurrentDomain);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static Boolean Scan(AppDomain domain)
        {
            if (domain is null)
            {
                throw new ArgumentNullException(nameof(domain));
            }

            return Scan(domain.GetCecilTypes());
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static Boolean Scan(Assembly assembly)
        {
            return Scan(assembly, out _);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static Boolean Scan(Assembly assembly, out TypeSet set)
        {
            if (assembly is null)
            {
                throw new ArgumentNullException(nameof(assembly));
            }

            return Scan(set = assembly.GetCecilTypes());
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static Boolean Scan(IEnumerable<Assembly> source)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            return Scan(source.GetCecilTypes());
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static Boolean Scan(IEnumerable<MonoCecilType> source)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            try
            {
                InheritEvaluator.Scan(source);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }

    public sealed class ReflectionInheritResult : IImmutableSet<MonoCecilType>
    {
        [return: NotNullIfNotNull("value")]
        public static implicit operator TypeSet?(ReflectionInheritResult? value)
        {
            return value?.All;
        }

        public static ReflectionInheritResult Empty { get; } = new ReflectionInheritResult(ReflectionInherit.Empty, ReflectionInherit.Empty);

        private ReflectionInherit _all;
        public ReflectionInherit All
        {
            get
            {
                if (!_all.IsEmpty)
                {
                    return _all;
                }

                ReflectionInherit.Builder builder = new ReflectionInherit.Builder();

                builder.Types.UnionWith(Inherit.Types);
                builder.Types.UnionWith(Generic.Types);

                builder.Interfaces.UnionWith(Inherit.Interfaces);
                builder.Interfaces.UnionWith(Generic.Interfaces);

                return _all = builder.ToImmutable();
            }
        }

        public readonly ReflectionInherit Inherit;
        public readonly ReflectionInherit Generic;

        public TypeSet Types
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                return Inherit.Types;
            }
        }

        public TypeSet Interfaces
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                return Inherit.Interfaces;
            }
        }

        public Int32 Count
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                return All.Count;
            }
        }

        public Int32 MaximumCount
        {
            get
            {
                return Inherit.Count + Generic.Count;
            }
        }

        private ReflectionInheritResult(ReflectionInherit inherit, ReflectionInherit generic)
        {
            Inherit = inherit;
            Generic = generic;
        }

        [MethodImpl(MethodImplOptions.AggressiveOptimization)]
        internal static ReflectionInheritResult Create(ConcurrentHashSet<MonoCecilType> set)
        {
            if (set is null)
            {
                throw new ArgumentNullException(nameof(set));
            }

            Builder builder = new Builder();

            lock (set)
            {
                foreach (MonoCecilType item in set)
                {
                    builder.Add(item);
                }
            }

            return builder.ToImmutable();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private TypeSet Target(MonoCecilType item)
        {
            return item switch
            {
                { IsInterface: true, IsGenericTypeDefinition: true } => Generic.Interfaces,
                { IsInterface: true } => Inherit.Interfaces,
                { IsGenericTypeDefinition: true } => Generic.Types,
                _ => Inherit.Types
            };
        }

        public Boolean Contains(MonoCecilType? item)
        {
            if (item is null)
            {
                return false;
            }

            TypeSet target = Target(item);
            return target.Count > 0 && target.Contains(item);
        }

        public Boolean TryGetValue(MonoCecilType? equalValue, out MonoCecilType actualValue)
        {
            if (equalValue is not null)
            {
                return Target(equalValue).TryGetValue(equalValue, out actualValue);
            }

            actualValue = null!;
            return false;
        }

        public Boolean IsSubsetOf(IEnumerable<MonoCecilType> other)
        {
            return All.IsSubsetOf(other);
        }

        public Boolean IsProperSubsetOf(IEnumerable<MonoCecilType> other)
        {
            return All.IsProperSubsetOf(other);
        }

        public Boolean IsSupersetOf(IEnumerable<MonoCecilType> other)
        {
            return All.IsSupersetOf(other);
        }

        public Boolean IsProperSupersetOf(IEnumerable<MonoCecilType> other)
        {
            return All.IsProperSupersetOf(other);
        }

        public Boolean Overlaps(IEnumerable<MonoCecilType> other)
        {
            return All.Overlaps(other);
        }

        public Boolean SetEquals(IEnumerable<MonoCecilType> other)
        {
            return All.SetEquals(other);
        }

        public IImmutableSet<MonoCecilType> Add(MonoCecilType value)
        {
            return All.Add(value);
        }

        public IImmutableSet<MonoCecilType> Union(IEnumerable<MonoCecilType> other)
        {
            return All.Union(other);
        }

        public IImmutableSet<MonoCecilType> Intersect(IEnumerable<MonoCecilType> other)
        {
            return All.Intersect(other);
        }

        public IImmutableSet<MonoCecilType> Except(IEnumerable<MonoCecilType> other)
        {
            return All.Except(other);
        }

        public IImmutableSet<MonoCecilType> SymmetricExcept(IEnumerable<MonoCecilType> other)
        {
            return All.SymmetricExcept(other);
        }

        public IImmutableSet<MonoCecilType> Remove(MonoCecilType value)
        {
            return All.Remove(value);
        }

        public IImmutableSet<MonoCecilType> Clear()
        {
            return All.Clear();
        }

        public IEnumerator<MonoCecilType> GetEnumerator()
        {
            return All.GetEnumerator();
        }

        IEnumerator<MonoCecilType> IEnumerable<MonoCecilType>.GetEnumerator()
        {
            return Inherit.Types.Concat<MonoCecilType>(Generic.Types).Concat(Inherit.Interfaces).Concat(Generic.Interfaces).GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        [SuppressMessage("ReSharper", "UnassignedGetOnlyAutoProperty")]
        private struct Builder
        {
            private ReflectionInherit.Builder Inherit;
            public ReflectionInherit.Builder Generic;

            public HashSet<MonoCecilType> Types
            {
                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                get
                {
                    return Inherit.Types;
                }
            }

            public HashSet<MonoCecilType> Interfaces
            {
                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                get
                {
                    return Inherit.Interfaces;
                }
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public HashSet<MonoCecilType> Target(MonoCecilType item)
            {
                return item switch
                {
                    { IsInterface: true, IsGenericTypeDefinition: true } => Generic.Interfaces,
                    { IsInterface: true } => Inherit.Interfaces,
                    { IsGenericTypeDefinition: true } => Generic.Types,
                    _ => Inherit.Types
                };
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public Boolean Add(MonoCecilType item)
            {
                return Target(item).Add(item);
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public ReflectionInheritResult ToImmutable()
            {
                return new ReflectionInheritResult(Inherit.ToImmutable(), Generic.ToImmutable());
            }
        }
    }

    [SuppressMessage("ReSharper", "LoopCanBeConvertedToQuery")]
    [SuppressMessage("ReSharper", "ForeachCanBeConvertedToQueryUsingAnotherGetEnumerator")]
    [SuppressMessage("ReSharper", "ForeachCanBePartlyConvertedToQueryUsingAnotherGetEnumerator")]
    public readonly struct ReflectionInherit : IStruct<ReflectionInherit>, IImmutableSet<MonoCecilType>
    {
        public static implicit operator TypeSet(ReflectionInherit value)
        {
            return value.Types.Union(value.Interfaces);
        }

        public static ReflectionInherit Empty { get; } = new ReflectionInherit();

        private readonly TypeSet _types;
        public TypeSet Types
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                return _types ?? TypeSet.Empty;
            }
        }

        private readonly TypeSet _interfaces;
        public TypeSet Interfaces
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                return _interfaces ?? TypeSet.Empty;
            }
        }

        public Int32 Count
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                return Types.Count + Interfaces.Count;
            }
        }

        public Boolean IsEmpty
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                return _types is null || _interfaces is null;
            }
        }

        private ReflectionInherit(TypeSet? types, TypeSet? interfaces)
        {
            _types = types ?? TypeSet.Empty;
            _interfaces = interfaces ?? TypeSet.Empty;
        }

        private TypeSet Target(MonoCecilType item)
        {
            return item.IsInterface ? Interfaces : Types;
        }

        public Boolean Contains(MonoCecilType? item)
        {
            if (item is null)
            {
                return false;
            }

            TypeSet target = Target(item);
            return target.Count > 0 && target.Contains(item);
        }

        public Boolean TryGetValue(MonoCecilType? equalValue, out MonoCecilType actualValue)
        {
            if (equalValue is not null)
            {
                return Target(equalValue).TryGetValue(equalValue, out actualValue);
            }

            actualValue = null!;
            return false;
        }

        public Boolean IsSubsetOf(IEnumerable<MonoCecilType> other)
        {
            if (other is null)
            {
                throw new ArgumentNullException(nameof(other));
            }

            IReadOnlySet<MonoCecilType> set = other as IReadOnlySet<MonoCecilType> ?? TypeSet.Create(other);

            foreach (MonoCecilType item in Types)
            {
                if (!set.Contains(item))
                {
                    return false;
                }
            }

            foreach (MonoCecilType item in Interfaces)
            {
                if (!set.Contains(item))
                {
                    return false;
                }
            }

            return true;
        }

        public Boolean IsProperSubsetOf(IEnumerable<MonoCecilType> other)
        {
            if (other is null)
            {
                throw new ArgumentNullException(nameof(other));
            }

            IReadOnlySet<MonoCecilType> set = other as IReadOnlySet<MonoCecilType> ?? TypeSet.Create(other);
            return Count < set.Count && IsSubsetOf(set);
        }

        public Boolean IsSupersetOf(IEnumerable<MonoCecilType> other)
        {
            if (other is null)
            {
                throw new ArgumentNullException(nameof(other));
            }

            foreach (MonoCecilType item in other)
            {
                if (!Contains(item))
                {
                    return false;
                }
            }

            return true;
        }

        public Boolean IsProperSupersetOf(IEnumerable<MonoCecilType> other)
        {
            if (other is null)
            {
                throw new ArgumentNullException(nameof(other));
            }

            IReadOnlySet<MonoCecilType> set = other as IReadOnlySet<MonoCecilType> ?? TypeSet.Create(other);
            return Count > set.Count && IsSupersetOf(set);
        }

        public Boolean Overlaps(IEnumerable<MonoCecilType> other)
        {
            if (other is null)
            {
                throw new ArgumentNullException(nameof(other));
            }

            foreach (MonoCecilType item in other)
            {
                if (Contains(item))
                {
                    return true;
                }
            }

            return false;
        }

        public Boolean SetEquals(IEnumerable<MonoCecilType> other)
        {
            if (other is null)
            {
                throw new ArgumentNullException(nameof(other));
            }

            IReadOnlySet<MonoCecilType> set = other as IReadOnlySet<MonoCecilType> ?? TypeSet.Create(other);
            return Count == set.Count && IsSubsetOf(set);
        }

        public IImmutableSet<MonoCecilType> Add(MonoCecilType? value)
        {
            if (value is null)
            {
                return this;
            }

            return value.IsInterface ? new ReflectionInherit(Types, Interfaces.Add(value)) : new ReflectionInherit(Types.Add(value), Interfaces);
        }

        public IImmutableSet<MonoCecilType> Union(IEnumerable<MonoCecilType> other)
        {
            if (other is null)
            {
                throw new ArgumentNullException(nameof(other));
            }

            HashSet<MonoCecilType?> builder = new HashSet<MonoCecilType?>(Types.Count + Interfaces.Count + (other.CountIfMaterialized() ?? 32));

            builder.UnionWith(Types);
            builder.UnionWith(Interfaces);
            builder.UnionWith(other);

            return TypeSet.Create(builder);
        }

        public IImmutableSet<MonoCecilType> Intersect(IEnumerable<MonoCecilType> other)
        {
            if (other is null)
            {
                throw new ArgumentNullException(nameof(other));
            }

            HashSet<MonoCecilType?> builder = new HashSet<MonoCecilType?>(Types.Count + Interfaces.Count);
            IReadOnlySet<MonoCecilType> set = other as IReadOnlySet<MonoCecilType> ?? TypeSet.Create(other);

            foreach (MonoCecilType item in Types)
            {
                if (set.Contains(item))
                {
                    builder.Add(item);
                }
            }

            foreach (MonoCecilType item in Interfaces)
            {
                if (set.Contains(item))
                {
                    builder.Add(item);
                }
            }

            return TypeSet.Create(builder);
        }

        public IImmutableSet<MonoCecilType> Except(IEnumerable<MonoCecilType> other)
        {
            if (other is null)
            {
                throw new ArgumentNullException(nameof(other));
            }

            HashSet<MonoCecilType?> builder = new HashSet<MonoCecilType?>(Types.Count + Interfaces.Count);
            IReadOnlySet<MonoCecilType> set = other as IReadOnlySet<MonoCecilType> ?? TypeSet.Create(other);

            foreach (MonoCecilType item in Types)
            {
                if (!set.Contains(item))
                {
                    builder.Add(item);
                }
            }

            foreach (MonoCecilType item in Interfaces)
            {
                if (!set.Contains(item))
                {
                    builder.Add(item);
                }
            }

            return TypeSet.Create(builder);
        }

        public IImmutableSet<MonoCecilType> SymmetricExcept(IEnumerable<MonoCecilType> other)
        {
            if (other is null)
            {
                throw new ArgumentNullException(nameof(other));
            }

            HashSet<MonoCecilType?> builder = new HashSet<MonoCecilType?>(Types.Count + Interfaces.Count + (other.CountIfMaterialized() ?? 32));

            builder.UnionWith(Types);
            builder.UnionWith(Interfaces);
            builder.SymmetricExceptWith(other);

            return TypeSet.Create(builder);
        }

        public IImmutableSet<MonoCecilType> Remove(MonoCecilType? value)
        {
            if (value is null)
            {
                return this;
            }

            return value.IsInterface ? new ReflectionInherit(Types, Interfaces.Remove(value)) : new ReflectionInherit(Types.Remove(value), Interfaces);
        }

        public IImmutableSet<MonoCecilType> Clear()
        {
            return Empty;
        }

        public IEnumerator<MonoCecilType> GetEnumerator()
        {
            foreach (MonoCecilType item in Types)
            {
                yield return item;
            }

            foreach (MonoCecilType item in Interfaces)
            {
                yield return item;
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        internal struct Builder
        {
            private HashSet<MonoCecilType?>? _types;
            public HashSet<MonoCecilType> Types
            {
                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                get
                {
                    return (_types ??= new HashSet<MonoCecilType?>())!;
                }
            }

            private HashSet<MonoCecilType?>? _interfaces;
            public HashSet<MonoCecilType> Interfaces
            {
                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                get
                {
                    return (_interfaces ??= new HashSet<MonoCecilType?>())!;
                }
            }

            public Builder(Int32 types, Int32 interfaces)
            {
                _types = new HashSet<MonoCecilType?>(types);
                _interfaces = new HashSet<MonoCecilType?>(interfaces);
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public ReflectionInherit ToImmutable()
            {
                return new ReflectionInherit(TypeSet.Create(_types), TypeSet.Create(_interfaces));
            }
        }
    }
}