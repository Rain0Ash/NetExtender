using System;
using System.Collections.Concurrent;
using System.Diagnostics.CodeAnalysis;
using System.Reflection.Emit;
using NetExtender.Types.Assemblies.Interfaces;

namespace NetExtender.Types.Assemblies
{
    public class DynamicAssemblyStorage : DynamicAssembly, IDynamicAssemblyUnsafeStorage
    {
        protected ConcurrentDictionary<Type, Type> Storage { get; } = new ConcurrentDictionary<Type, Type>();
        
        ConcurrentDictionary<Type, Type> IDynamicAssemblyUnsafeStorage.Storage
        {
            get
            {
                return Storage;
            }
        }
        
        public DynamicAssemblyStorage()
        {
        }
        
        public DynamicAssemblyStorage(AssemblyBuilderAccess access)
            : base(access)
        {
        }
        
        public DynamicAssemblyStorage(String name, AssemblyBuilderAccess access)
            : base(name, access)
        {
        }
        
        public Boolean Contains(Type type)
        {
            if (type is null)
            {
                throw new ArgumentNullException(nameof(type));
            }
            
            return Storage.ContainsKey(type);
        }
        
        public Boolean TryGetValue(Type type, [MaybeNullWhen(false)] out Type result)
        {
            if (type is null)
            {
                throw new ArgumentNullException(nameof(type));
            }
            
            return Storage.TryGetValue(type, out result);
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
            
            return Storage.GetOrAdd(type, factory);
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
            
            return Storage.GetOrAdd(type, factory, this);
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
            
            return Storage.TryRemove(type, out result);
        }
    }
}