using System;
using System.Reflection;

namespace NetExtender.Types.Reflection.Interfaces
{
    public interface IWeakDependencyFactory<out T> where T : IWeakDependency
    {
        public IWeakDependencyLoader Loader { get; init; }
        
        public T Create();
    }
    
    public interface IWeakDependencyFactory
    {
        public IWeakDependencyLoader Loader { get; init; }
        
        public Type Create(Type type);
    }
}