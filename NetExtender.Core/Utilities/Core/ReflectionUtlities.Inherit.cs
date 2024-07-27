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
using NetExtender.Types.Threading;
using NetExtender.Utilities.Types;

namespace NetExtender.Utilities.Core
{
    public static partial class ReflectionUtilities
    {
        [SuppressMessage("ReSharper", "MemberHidesStaticFromOuterClass")]
        private readonly struct InheritEvaluator
        {
            public static implicit operator Inherit.Result(InheritEvaluator value)
            {
                return new Inherit.Result(value.Type, value.Attribute);
            }
            
            private static MutexSlim Mutex { get; } = new MutexSlim();
            private static SemaphoreSlim Semaphore { get; } = new SemaphoreSlim(1, 1);
            
            private static ConcurrentDictionary<Type, ConcurrentHashSet<Type>> Inherit { get; } = new ConcurrentDictionary<Type, ConcurrentHashSet<Type>>();
            private static ConcurrentDictionary<Attribute, ConcurrentHashSet<Type>> AttributeInherit { get; } = new ConcurrentDictionary<Attribute, ConcurrentHashSet<Type>>();
            private static ConcurrentDictionary<Type, ConcurrentHashSet<Type>> AttributeTypeInherit { get; } = new ConcurrentDictionary<Type, ConcurrentHashSet<Type>>();
            
            private Inherit Type { get; }
            private AttributeInherit Attribute { get; }
            
            public Boolean IsEmpty
            {
                get
                {
                    return Type is null;
                }
            }
            
            public InheritEvaluator(Inherit type, AttributeInherit attribute)
            {
                Type = type ?? throw new ArgumentNullException(nameof(type));
                Attribute = attribute ?? throw new ArgumentNullException(nameof(attribute));
            }
            
            public static InheritEvaluator Create()
            {
                Semaphore.Wait();
                
                try
                {
                    Mutex.Wait();
                    return new InheritEvaluator(new Inherit(Inherit), new AttributeInherit(AttributeTypeInherit, AttributeInherit));
                }
                finally
                {
                    Mutex.Release();
                    Semaphore.Release();
                }
            }
            
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            private static ValueTask ScanHandler(Type type, CancellationToken token)
            {
                return type.IsInterface ? InterfaceScanHandler(type, token) : TypeScanHandler(type, token);
            }
            
            [MethodImpl(MethodImplOptions.AggressiveOptimization)]
            private static ValueTask<Exception?> AttributeScanHandler(Type type, CancellationToken token)
            {
                if (token.IsCancellationRequested)
                {
                    return ValueTask.FromResult<Exception?>(null);
                }
                
                static IEnumerable<(Type Type, Attribute Attribute)> Get(Type type)
                {
                    return type.GetCustomAttributes(true).OfType<Attribute>().Select(static attribute => (attribute.GetType(), attribute));
                }
                
                try
                {
                    foreach ((Type Type, Attribute Attribute) attribute in Get(type))
                    {
                        AttributeInherit.GetOrAdd(attribute.Attribute, static _ => new ConcurrentHashSet<Type>()).Add(type);
                        AttributeTypeInherit.GetOrAdd(attribute.Type, static _ => new ConcurrentHashSet<Type>()).Add(type);

                        if (attribute.Type.IsGenericType)
                        {
                            AttributeTypeInherit.GetOrAdd(attribute.Type.GetGenericTypeDefinition(), static _ => new ConcurrentHashSet<Type>()).Add(type);
                        }
                    }
                    
                    return ValueTask.FromResult<Exception?>(null);
                }
                catch (Exception exception)
                {
                    return ValueTask.FromResult<Exception?>(exception);
                }
            }
            
            [MethodImpl(MethodImplOptions.AggressiveOptimization)]
            private static async ValueTask TypeScanHandler(Type type, CancellationToken token)
            {
                if (token.IsCancellationRequested || type.IsInterface)
                {
                    return;
                }
                
                foreach (Type @interface in type.GetInterfaces())
                {
                    Inherit.GetOrAdd(@interface, static _ => new ConcurrentHashSet<Type>()).Add(type);
                    
                    if (@interface.IsGenericType)
                    {
                        Inherit.GetOrAdd(@interface.GetGenericTypeDefinition(), static _ => new ConcurrentHashSet<Type>()).Add(type);
                    }
                }
                
                await AttributeScanHandler(type, token);
                
                if (type.BaseType is not { } @base)
                {
                    return;
                }

                Inherit.GetOrAdd(@base, static _ => new ConcurrentHashSet<Type>()).Add(type);
                
                if (@base.IsGenericType)
                {
                    Inherit.GetOrAdd(@base.GetGenericTypeDefinition(), static _ => new ConcurrentHashSet<Type>()).Add(type);
                }
            }
            
            [MethodImpl(MethodImplOptions.AggressiveOptimization)]
            private static async ValueTask InterfaceScanHandler(Type type, CancellationToken token)
            {
                if (token.IsCancellationRequested || !type.IsInterface)
                {
                    return;
                }
                
                if (type.GetInterfaces() is not { Length: > 0 } interfaces)
                {
                    return;
                }
                
                foreach (Type @interface in interfaces)
                {
                    Inherit.GetOrAdd(@interface, static _ => new ConcurrentHashSet<Type>()).Add(type);
                    
                    if (@interface.IsGenericType)
                    {
                        Inherit.GetOrAdd(@interface.GetGenericTypeDefinition(), static _ => new ConcurrentHashSet<Type>()).Add(type);
                    }
                    
                    await AttributeScanHandler(@interface, token);
                    await InterfaceScanHandler(@interface, token);
                }
            }
            
            [MethodImpl(MethodImplOptions.AggressiveOptimization)]
            // ReSharper disable once CognitiveComplexity
            public static async ValueTask Scan(IEnumerable<Type> source)
            {
                if (source is null)
                {
                    throw new ArgumentNullException(nameof(source));
                }
                
                await Mutex.WaitAsync();
                
                try
                {
                    await Parallel.ForEachAsync(source, ScanHandler).ConfigureAwait(false);
                }
                finally
                {
                    await Mutex.ReleaseAsync();
                }
            }
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static async Task<Boolean> Scan()
        {
            return await Scan(AppDomain.CurrentDomain).ConfigureAwait(false);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static async Task<Boolean> Scan(AppDomain domain)
        {
            if (domain is null)
            {
                throw new ArgumentNullException(nameof(domain));
            }
            
            return await Scan(domain.GetTypes()).ConfigureAwait(false);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static async Task<Boolean> Scan(Assembly assembly)
        {
            if (assembly is null)
            {
                throw new ArgumentNullException(nameof(assembly));
            }
            
            return await Scan(assembly.GetTypes()).ConfigureAwait(false);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static async Task<Boolean> Scan(IEnumerable<Assembly> source)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }
            
            return await Scan(source.GetTypes()).ConfigureAwait(false);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static async Task<Boolean> Scan(IEnumerable<Type> source)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }
            
            try
            {
                await InheritEvaluator.Scan(source).ConfigureAwait(false);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
    
    public sealed class ReflectionInheritResult : IImmutableSet<Type>
    {
        [return: NotNullIfNotNull("value")]
        public static implicit operator ImmutableHashSet<Type>?(ReflectionInheritResult? value)
        {
            return value?.All;
        }
        
        public static ReflectionInheritResult Empty { get; } = new ReflectionInheritResult(ReflectionInherit.Empty, ReflectionInherit.Empty, ReflectionInherit.Empty);
        
        public ReflectionInherit All { get; }
        public ReflectionInherit Inherit { get; }
        public ReflectionInherit Generic { get; }
        
        public ImmutableHashSet<Type> Types
        {
            get
            {
                return Inherit.Types;
            }
        }
        
        public ImmutableHashSet<Type> Interfaces
        {
            get
            {
                return Inherit.Interfaces;
            }
        }
        
        public Int32 Count
        {
            get
            {
                return All.Count;
            }
        }
        
        private ReflectionInheritResult(ReflectionInherit? all, ReflectionInherit? @internal, ReflectionInherit? generic)
        {
            Inherit = @internal ?? ReflectionInherit.Empty;
            Generic = generic ?? ReflectionInherit.Empty;
            
            if (all is null)
            {
                ReflectionInherit.Builder builder = new ReflectionInherit.Builder();
                
                builder.Types.UnionWith(Inherit.Types);
                builder.Types.UnionWith(Generic.Types);
                
                builder.Interfaces.UnionWith(Inherit.Interfaces);
                builder.Interfaces.UnionWith(Generic.Interfaces);
                
                all = builder.ToImmutable();
            }
            
            All = all;
        }
        
        [MethodImpl(MethodImplOptions.AggressiveOptimization)]
        internal static ReflectionInheritResult Create(ConcurrentHashSet<Type> set)
        {
            if (set is null)
            {
                throw new ArgumentNullException(nameof(set));
            }
            
            Builder builder = new Builder();
            
            foreach (Type type in set)
            {
                (type switch
                {
                    { IsInterface: true, IsGenericTypeDefinition: true } => builder.Generic.Interfaces,
                    { IsInterface: true } => builder.Interfaces,
                    { IsInterface: false, IsGenericTypeDefinition: true } => builder.Generic.Types,
                    _ => builder.Types
                }).Add(type);
            }

            return builder.ToImmutable();
        }
        
        public Boolean Contains(Type item)
        {
            return All.Contains(item);
        }
        
        public Boolean TryGetValue(Type equalValue, out Type actualValue)
        {
            return All.TryGetValue(equalValue, out actualValue);
        }
        
        public Boolean IsProperSubsetOf(IEnumerable<Type> other)
        {
            return All.IsProperSubsetOf(other);
        }
        
        public Boolean IsProperSupersetOf(IEnumerable<Type> other)
        {
            return All.IsProperSupersetOf(other);
        }
        
        public Boolean IsSubsetOf(IEnumerable<Type> other)
        {
            return All.IsSubsetOf(other);
        }
        
        public Boolean IsSupersetOf(IEnumerable<Type> other)
        {
            return All.IsSupersetOf(other);
        }
        
        public Boolean Overlaps(IEnumerable<Type> other)
        {
            return All.Overlaps(other);
        }
        
        public Boolean SetEquals(IEnumerable<Type> other)
        {
            return All.SetEquals(other);
        }
        
        public ImmutableHashSet<Type> Add(Type value)
        {
            return All.Add(value);
        }
        
        IImmutableSet<Type> IImmutableSet<Type>.Add(Type value)
        {
            return All.Add(value);
        }
        
        public ImmutableHashSet<Type> Union(IEnumerable<Type> other)
        {
            return All.Union(other);
        }
        
        IImmutableSet<Type> IImmutableSet<Type>.Union(IEnumerable<Type> other)
        {
            return All.Union(other);
        }
        
        public ImmutableHashSet<Type> Intersect(IEnumerable<Type> other)
        {
            return All.Intersect(other);
        }
        
        IImmutableSet<Type> IImmutableSet<Type>.Intersect(IEnumerable<Type> other)
        {
            return All.Intersect(other);
        }
        
        public ImmutableHashSet<Type> Except(IEnumerable<Type> other)
        {
            return All.Except(other);
        }
        
        IImmutableSet<Type> IImmutableSet<Type>.Except(IEnumerable<Type> other)
        {
            return All.Except(other);
        }
        
        public ImmutableHashSet<Type> SymmetricExcept(IEnumerable<Type> other)
        {
            return All.SymmetricExcept(other);
        }
        
        IImmutableSet<Type> IImmutableSet<Type>.SymmetricExcept(IEnumerable<Type> other)
        {
            return All.SymmetricExcept(other);
        }
        
        public ImmutableHashSet<Type> Remove(Type value)
        {
            return All.Remove(value);
        }
        
        IImmutableSet<Type> IImmutableSet<Type>.Remove(Type value)
        {
            return All.Remove(value);
        }
        
        public ImmutableHashSet<Type> Clear()
        {
            return All.Clear();
        }
        
        IImmutableSet<Type> IImmutableSet<Type>.Clear()
        {
            return All.Clear();
        }
        
        public ImmutableHashSet<Type>.Enumerator GetEnumerator()
        {
            return All.GetEnumerator();
        }
        
        IEnumerator<Type> IEnumerable<Type>.GetEnumerator()
        {
            return Inherit.Types.Concat(Generic.Types).Concat(Inherit.Interfaces).Concat(Generic.Interfaces).GetEnumerator();
        }
        
        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
        
        [SuppressMessage("ReSharper", "UnassignedGetOnlyAutoProperty")]
        private struct Builder
        {
            private ReflectionInherit.Builder Internal;
            public ReflectionInherit.Builder Generic;
            
            public ImmutableHashSet<Type>.Builder Types
            {
                get
                {
                    return Internal.Types;
                }
            }
            
            public ImmutableHashSet<Type>.Builder Interfaces
            {
                get
                {
                    return Internal.Interfaces;
                }
            }
            
            public ReflectionInheritResult ToImmutable()
            {
                return new ReflectionInheritResult(null, Internal.ToImmutable(), Generic.ToImmutable());
            }
        }
    }
    
    public sealed class ReflectionInherit : IImmutableSet<Type>
    {
        [return: NotNullIfNotNull("value")]
        public static implicit operator ImmutableHashSet<Type>?(ReflectionInherit? value)
        {
            return value?.All;
        }
        
        public static ReflectionInherit Empty { get; } = new ReflectionInherit();
        
        public ImmutableHashSet<Type> All { get; }
        public ImmutableHashSet<Type> Types { get; }
        public ImmutableHashSet<Type> Interfaces { get; }
        
        public Int32 Count
        {
            get
            {
                return Types.Count + Interfaces.Count;
            }
        }
        
        private ReflectionInherit()
            : this(null, null)
        {
        }
        
        private ReflectionInherit(ImmutableHashSet<Type>? types, ImmutableHashSet<Type>? interfaces)
        {
            Types = types ?? ImmutableHashSet<Type>.Empty;
            Interfaces = interfaces ?? ImmutableHashSet<Type>.Empty;
            All = ImmutableHashSet<Type>.Empty.Union(Types).Union(Interfaces);
        }
        
        public Boolean Contains(Type item)
        {
            return All.Contains(item);
        }
        
        public Boolean TryGetValue(Type equalValue, out Type actualValue)
        {
            return All.TryGetValue(equalValue, out actualValue);
        }
        
        public Boolean IsProperSubsetOf(IEnumerable<Type> other)
        {
            return All.IsProperSubsetOf(other);
        }
        
        public Boolean IsProperSupersetOf(IEnumerable<Type> other)
        {
            return All.IsProperSupersetOf(other);
        }
        
        public Boolean IsSubsetOf(IEnumerable<Type> other)
        {
            return All.IsSubsetOf(other);
        }
        
        public Boolean IsSupersetOf(IEnumerable<Type> other)
        {
            return All.IsSupersetOf(other);
        }
        
        public Boolean Overlaps(IEnumerable<Type> other)
        {
            return All.Overlaps(other);
        }
        
        public Boolean SetEquals(IEnumerable<Type> other)
        {
            return All.SetEquals(other);
        }
        
        public ImmutableHashSet<Type> Add(Type value)
        {
            return All.Add(value);
        }
        
        IImmutableSet<Type> IImmutableSet<Type>.Add(Type value)
        {
            return All.Add(value);
        }
        
        public ImmutableHashSet<Type> Union(IEnumerable<Type> other)
        {
            return All.Union(other);
        }
        
        IImmutableSet<Type> IImmutableSet<Type>.Union(IEnumerable<Type> other)
        {
            return All.Union(other);
        }
        
        public ImmutableHashSet<Type> Intersect(IEnumerable<Type> other)
        {
            return All.Intersect(other);
        }
        
        IImmutableSet<Type> IImmutableSet<Type>.Intersect(IEnumerable<Type> other)
        {
            return All.Intersect(other);
        }
        
        public ImmutableHashSet<Type> Except(IEnumerable<Type> other)
        {
            return All.Except(other);
        }
        
        IImmutableSet<Type> IImmutableSet<Type>.Except(IEnumerable<Type> other)
        {
            return All.Except(other);
        }
        
        public ImmutableHashSet<Type> SymmetricExcept(IEnumerable<Type> other)
        {
            return All.SymmetricExcept(other);
        }
        
        IImmutableSet<Type> IImmutableSet<Type>.SymmetricExcept(IEnumerable<Type> other)
        {
            return All.SymmetricExcept(other);
        }
        
        public ImmutableHashSet<Type> Remove(Type value)
        {
            return All.Remove(value);
        }
        
        IImmutableSet<Type> IImmutableSet<Type>.Remove(Type value)
        {
            return All.Remove(value);
        }
        
        public ImmutableHashSet<Type> Clear()
        {
            return All.Clear();
        }
        
        IImmutableSet<Type> IImmutableSet<Type>.Clear()
        {
            return All.Clear();
        }
        
        public ImmutableHashSet<Type>.Enumerator GetEnumerator()
        {
            return All.GetEnumerator();
        }
        
        IEnumerator<Type> IEnumerable<Type>.GetEnumerator()
        {
            return Types.Concat(Interfaces).GetEnumerator();
        }
        
        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
        
        internal struct Builder
        {
            private ImmutableHashSet<Type>.Builder? _types;
            public ImmutableHashSet<Type>.Builder Types
            {
                get
                {
                    return _types ??= ImmutableHashSet.CreateBuilder<Type>();
                }
            }
            
            private ImmutableHashSet<Type>.Builder? _interfaces;
            public ImmutableHashSet<Type>.Builder Interfaces
            {
                get
                {
                    return _interfaces ??= ImmutableHashSet.CreateBuilder<Type>();
                }
            }
            
            public ReflectionInherit ToImmutable()
            {
                return new ReflectionInherit(_types?.ToImmutable(), _interfaces?.ToImmutable());
            }
        }
    }
}