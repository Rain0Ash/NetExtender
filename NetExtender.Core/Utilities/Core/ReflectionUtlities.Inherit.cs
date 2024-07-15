using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;
using NetExtender.Types.Multithreading;

namespace NetExtender.Utilities.Core
{
    public static partial class ReflectionUtilities
    {
        private readonly struct InheritResult
        {
            private static MutexSlim Mutex { get; } = new MutexSlim();
            private static SemaphoreSlim Semaphore { get; } = new SemaphoreSlim(1, 1);
            
            // ReSharper disable once MemberHidesStaticFromOuterClass
            private static ConcurrentDictionary<Type, ConcurrentHashSet<Type>> Inherit { get; } = new ConcurrentDictionary<Type, ConcurrentHashSet<Type>>();
            public ImmutableDictionary<Type, ImmutableHashSet<Type>> Type { get; private init; }
            public ImmutableDictionary<Type, ImmutableDictionary<Assembly, ImmutableHashSet<Type>>> Assembly { get; private init; }
            
            public Boolean IsEmpty
            {
                get
                {
                    return Type is null;
                }
            }
            
            public static InheritResult Create()
            {
                static ImmutableHashSet<Type> Set(KeyValuePair<Type, ConcurrentHashSet<Type>> pair)
                {
                    return pair.Value.ToImmutableHashSet();
                }
                
                static ImmutableDictionary<Assembly, ImmutableHashSet<Type>> Assemblies(KeyValuePair<Type, ImmutableHashSet<Type>> pair)
                {
                    static ImmutableHashSet<Type> Set(KeyValuePair<Assembly, ConcurrentHashSet<Type>> pair)
                    {
                        return pair.Value.ToImmutableHashSet();
                    }
                    
                    ConcurrentDictionary<Assembly, ConcurrentHashSet<Type>> assemblies = new ConcurrentDictionary<Assembly, ConcurrentHashSet<Type>>();
                    
                    foreach (Type type in pair.Value)
                    {
                        assemblies.GetOrAdd(type.Assembly, static _ => new ConcurrentHashSet<Type>()).Add(type);
                    }
                    
                    return assemblies.ToImmutableDictionary(static pair => pair.Key, Set);
                }
                
                Semaphore.Wait();
                
                try
                {
                    Mutex.Wait();
                    
                    ImmutableDictionary<Type, ImmutableHashSet<Type>> immutable = Inherit.ToImmutableDictionary(static pair => pair.Key, Set);
                    
                    return new InheritResult
                    {
                        Type = immutable,
                        Assembly = immutable.ToImmutableDictionary(static pair => pair.Key, Assemblies)
                    };
                }
                finally
                {
                    Mutex.Release();
                    Semaphore.Release();
                }
            }
            
            [MethodImpl(MethodImplOptions.AggressiveOptimization)]
            public static async ValueTask Handle(IEnumerable<Type> types)
            {
                if (types is null)
                {
                    throw new ArgumentNullException(nameof(types));
                }
                
                static ValueTask Handler(Type type, CancellationToken token)
                {
                    if (token.IsCancellationRequested)
                    {
                        return ValueTask.CompletedTask;
                    }
                    
                    if (type.BaseType is not { } @base)
                    {
                        return ValueTask.CompletedTask;
                    }
                    
                    Inherit.GetOrAdd(@base, static _ => new ConcurrentHashSet<Type>()).Add(type);
                    
                    if (@base.IsGenericType)
                    {
                        Inherit.GetOrAdd(@base.GetGenericTypeDefinition(), static _ => new ConcurrentHashSet<Type>()).Add(type);
                    }
                    
                    return ValueTask.CompletedTask;
                }
                
                await Mutex.WaitAsync();
                
                try
                {
                    await Parallel.ForEachAsync(types, Handler);
                }
                finally
                {
                    Mutex.Release();
                }
            }
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static Task<Boolean> HandleAssemblyInherit()
        {
            return HandleAssemblyInherit(AppDomain.CurrentDomain);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static Task<Boolean> HandleAssemblyInherit(AppDomain domain)
        {
            if (domain is null)
            {
                throw new ArgumentNullException(nameof(domain));
            }
            
            return HandleAssemblyInherit(domain.GetTypes());
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static Task<Boolean> HandleAssemblyInherit(Assembly assembly)
        {
            if (assembly is null)
            {
                throw new ArgumentNullException(nameof(assembly));
            }
            
            return HandleAssemblyInherit(assembly.GetTypes());
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static Task<Boolean> HandleAssemblyInherit(IEnumerable<Assembly> assemblies)
        {
            if (assemblies is null)
            {
                throw new ArgumentNullException(nameof(assemblies));
            }
            
            return HandleAssemblyInherit(assemblies.GetTypes());
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static async Task<Boolean> HandleAssemblyInherit(IEnumerable<Type> types)
        {
            if (types is null)
            {
                throw new ArgumentNullException(nameof(types));
            }
            
            try
            {
                await InheritResult.Handle(types);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}