using System;
using System.Collections.Concurrent;
using System.Diagnostics.CodeAnalysis;
using System.Reflection.Emit;
using NetExtender.Types.Assemblies.Interfaces;

namespace NetExtender.Types.Assemblies
{
    public class DynamicAssemblyStore : DynamicAssembly, IDynamicAssemblyUnsafeStore
    {
        protected ConcurrentDictionary<Type, Type> Store { get; } = new ConcurrentDictionary<Type, Type>();
        
        ConcurrentDictionary<Type, Type> IDynamicAssemblyUnsafeStore.Store
        {
            get
            {
                return Store;
            }
        }
        
        public DynamicAssemblyStore()
        {
        }
        
        public DynamicAssemblyStore(AssemblyBuilderAccess access)
            : base(access)
        {
        }
        
        public DynamicAssemblyStore(String name, AssemblyBuilderAccess access)
            : base(name, access)
        {
        }
        
        public Boolean Contains(Type type)
        {
            if (type is null)
            {
                throw new ArgumentNullException(nameof(type));
            }
            
            return Store.ContainsKey(type);
        }
        
        public Boolean TryGetValue(Type type, [MaybeNullWhen(false)] out Type result)
        {
            if (type is null)
            {
                throw new ArgumentNullException(nameof(type));
            }
            
            return Store.TryGetValue(type, out result);
        }
        
        public Type GetOrAdd(Type type, Func<Type, Type> factory)
        {
            if (type is null)
            {
                throw new ArgumentNullException(nameof(type));
            }
            
            if (factory is null)
            {
                throw new ArgumentNullException(nameof(factory));
            }
            
            return Store.GetOrAdd(type, factory);
        }
        
        public Type GetOrAdd(Type type, Func<Type, IDynamicAssembly, Type> factory)
        {
            if (type is null)
            {
                throw new ArgumentNullException(nameof(type));
            }
            
            if (factory is null)
            {
                throw new ArgumentNullException(nameof(factory));
            }
            
            return Store.GetOrAdd(type, factory, this);
        }
        
        public Boolean Remove(Type type)
        {
            return Remove(type, out _);
        }
        
        public Boolean Remove(Type type, [MaybeNullWhen(false)] out Type result)
        {
            if (type is null)
            {
                throw new ArgumentNullException(nameof(type));
            }
            
            return Store.TryRemove(type, out result);
        }
    }
}