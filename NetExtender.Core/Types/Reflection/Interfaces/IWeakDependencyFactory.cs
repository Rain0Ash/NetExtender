// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;

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