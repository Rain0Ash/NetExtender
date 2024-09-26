using System;
using System.Diagnostics.CodeAnalysis;
using System.Reflection;
using NetExtender.Utilities.Types;

namespace NetExtender.Types.Reflection.Interfaces
{
    public interface IWeakDependencyLoader<out TFactory> : IWeakDependencyLoader where TFactory : class, IWeakDependencyFactory
    {
        public new TFactory Factory { get; }
    }

    public interface IWeakDependencyLoader
    {
        public SyncRoot SyncRoot { get; }
        public Assembly? Assembly { get; }
        public AssemblyName Name { get; }
        public WeakDependencyState State { get; }
        public IWeakDependencyFactory Factory { get; }

        public Boolean Reload();
        public Boolean TryGet([MaybeNullWhen(false)] out Assembly assembly);
    }
}