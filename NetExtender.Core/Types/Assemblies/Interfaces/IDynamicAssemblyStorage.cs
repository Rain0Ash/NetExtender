using System;
using System.Collections.Concurrent;
using System.Diagnostics.CodeAnalysis;

namespace NetExtender.Types.Assemblies.Interfaces
{
    internal interface IDynamicAssemblyUnsafeStorage : IDynamicAssemblyStorage
    {
        public ConcurrentDictionary<Type, Type> Storage { get; }
    }
    
    public interface IDynamicAssemblyStorage : IDynamicAssembly
    {
        public Boolean Contains(Type type);
        public Boolean TryGetValue(Type type, [MaybeNullWhen(false)] out Type result);
        public Type GetOrAdd(Type type, Func<Type, Type> factory);
        public Type GetOrAdd(Type type, Func<Type, IDynamicAssembly, Type> factory);
        public Boolean Remove(Type type);
        public Boolean Remove(Type type, [MaybeNullWhen(false)] out Type result);
    }
}