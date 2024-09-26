using System;
using System.Collections.Concurrent;
using System.Reflection;
using NetExtender.Types.Reflection.Interfaces;
using NetExtender.Utilities.Core;

namespace NetExtender.Types.Reflection
{
    public partial class WeakDependency
    {
        public sealed class Factory : WeakDependencyFactory
        {
            private static ConcurrentDictionary<Type, Type> Storage { get; } = new ConcurrentDictionary<Type, Type>();
            
            public override Type Create(Type type)
            {
                if (type is null)
                {
                    throw new ArgumentNullException(nameof(type));
                }
                
                return Storage.GetOrAdd(type, Initialize);
            }
            
            private Type Initialize(Type type)
            {
                if (type is null)
                {
                    throw new ArgumentNullException(nameof(type));
                }
                
                if (!type.HasInterface(typeof(IWeakDependency)))
                {
                    throw new ArgumentException($"Type '{type}' must implements '{typeof(IWeakDependency)}' method.");
                }
                
                Assembly assembly = Loader.Assembly ?? throw new InvalidOperationException();
                throw new NotImplementedException();
            }
        }
    }
    
    public abstract class WeakDependencyFactory : IWeakDependencyFactory
    {
        private readonly IWeakDependencyLoader? _loader;
        public IWeakDependencyLoader Loader
        {
            get
            {
                return _loader ?? throw new NotSupportedException();
            }
            init
            {
                _loader = value ?? throw new ArgumentNullException(nameof(value));
            }
        }
        
        protected WeakDependencyFactory()
        {
            Loader = default!;
        }
        
        public abstract Type Create(Type type);
        
        public Type Create<T>() where T : IWeakDependency
        {
            return Create(typeof(T));
        }
    }
}