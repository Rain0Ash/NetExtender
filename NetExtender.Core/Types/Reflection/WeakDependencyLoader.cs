// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Reflection;
using NetExtender.Types.Reflection.Interfaces;
using NetExtender.Utilities.Core;
using NetExtender.Utilities.Types;

namespace NetExtender.Types.Reflection
{
    public enum WeakDependencyState : Byte
    {
        Unload,
        Successful,
        Failed
    }
    
    public class WeakDependencyLoader : WeakDependencyLoader<WeakDependency.Factory>
    {
        public WeakDependencyLoader(String assembly)
            : base(assembly)
        {
        }
        
        public WeakDependencyLoader(AssemblyName name)
            : base(name)
        {
        }
        
        public WeakDependencyLoader(Assembly assembly)
            : base(assembly)
        {
        }
    }

    public class WeakDependencyLoader<TFactory> : IWeakDependencyLoader<TFactory> where TFactory : class, IWeakDependencyFactory, new()
    {
        public SyncRoot SyncRoot { get; } = SyncRoot.Create();
        
        private Assembly? _assembly;
        public Assembly? Assembly
        {
            get
            {
                lock (SyncRoot)
                {
                    if (_assembly is not null && State == WeakDependencyState.Successful)
                    {
                        return _assembly;
                    }
                    
                    return Reload() ? _assembly : null;
                }
            }
            protected set
            {
                lock (SyncRoot)
                {
                    _assembly = value;
                }
            }
        }
        
        private readonly AssemblyName _name;
        public AssemblyName Name
        {
            get
            {
                lock (SyncRoot)
                {
                    return Assembly?.GetName() ?? _name;
                }
            }
        }
        
        public WeakDependencyState State { get; protected set; }
        
        private TFactory? _factory;
        public virtual TFactory Factory
        {
            get
            {
                return _factory ??= new TFactory
                {
                    Loader = this
                };
            }
            protected set
            {
                _factory = value;
            }
        }
        
        IWeakDependencyFactory IWeakDependencyLoader.Factory
        {
            get
            {
                return Factory;
            }
        }
        
        public WeakDependencyLoader(String assembly)
            : this(new AssemblyName(assembly))
        {
        }

        public WeakDependencyLoader(AssemblyName name)
        {
            _name = name ?? throw new ArgumentNullException(nameof(name));
        }
        
        public WeakDependencyLoader(Assembly assembly)
            : this(assembly is not null ? assembly.GetName() : throw new ArgumentNullException(nameof(assembly)))
        {
            _assembly = assembly;
        }
        
        protected virtual Assembly? Load()
        {
            try
            {
                AssemblyName name = Name;
                return ReflectionUtilities.Assemblies.FirstOrDefault(assembly => AssemblyName.ReferenceMatchesDefinition(assembly.GetName(), name)) ?? Assembly.Load(Name);
            }
            catch (Exception)
            {
                return null;
            }
        }
        
        public Boolean Reload()
        {
            lock (SyncRoot)
            {
                _assembly = Load();
                State = _assembly is not null ? WeakDependencyState.Successful : WeakDependencyState.Failed;
                return State == WeakDependencyState.Successful;
            }
        }
        
        public Boolean TryGet([MaybeNullWhen(false)] out Assembly assembly)
        {
            assembly = Assembly;
            return State == WeakDependencyState.Successful && assembly is not null;
        }
    }
}