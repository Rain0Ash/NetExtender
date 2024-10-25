using System;
using System.Collections;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Reflection;
using System.Threading;
using Microsoft.Extensions.DependencyInjection;
using NetExtender.DependencyInjection;
using NetExtender.DependencyInjection.Events;
using NetExtender.DependencyInjection.Interfaces;
using NetExtender.Types.Monads;
using NetExtender.Types.Monads.Interfaces;
using NetExtender.Utilities.Core;

namespace NetExtender.Utilities.Types
{
    public static partial class ServiceProviderUtilities
    {
        internal interface ICustomServiceProvider : IAssemblyServiceProvider, IReadOnlyCollection<ServiceDescriptor>
        {
            public IServiceCollection Services { get; }
        }
        
        [SuppressMessage("ReSharper", "MemberHidesStaticFromOuterClass")]
        private sealed class ServiceProvider : ICustomServiceProvider
        {
            private readonly ILazy<IServiceProvider> _provider;
            
            public event ServiceProviderChangedEventHandler? Changed;
            
            private Maybe<IServiceProvider> Provider
            {
                get
                {
                    return _provider.IsValueCreated ? new Maybe<IServiceProvider>(_provider.Value) : default;
                }
            }
            
            IServiceCollection ICustomServiceProvider.Services
            {
                get
                {
                    return (IServiceCollection) _provider.Value;
                }
            }
            
            IReadOnlyCollection<Assembly>? IAssemblyServiceProvider.Assemblies
            {
                get
                {
                    return Storage.TryGetValue(this, out ConcurrentHashSet<Assembly>? result) ? result : null;
                }
            }
            
            public IServiceProvider Value
            {
                get
                {
                    return _provider.Value;
                }
            }
            
            public Boolean IsValueCreated
            {
                get
                {
                    return _provider.IsValueCreated;
                }
            }
            
            public Int32 Count
            {
                get
                {
                    return ((IServiceCollection) _provider.Value).Count;
                }
            }
            
            public Boolean IsStable
            {
                get
                {
                    return ((IChangeableServiceProvider?) Provider.Unwrap())?.IsStable ?? true;
                }
            }
            
            private volatile Boolean _final;
            public Boolean IsFinal
            {
                get
                {
                    return ((IChangeableServiceProvider?) Provider.Unwrap())?.IsFinal ?? _final;
                }
            }
            
            public Boolean IsScan
            {
                get
                {
                    return true;
                }
            }
            
            public ServiceProvider()
            {
                IServiceProvider Factory()
                {
                    IObservableServiceProvider provider = (IObservableServiceProvider) new DynamicServiceProvider { IsStable = true }.Scan();
                    
                    if (_final)
                    {
                        provider.Final();
                    }

                    Storage.GetOrAdd(provider, static _ => new ConcurrentHashSet<Assembly>()).AddRange(ReflectionUtilities.Assemblies);
                    provider.Changed += OnChanged;
                    return provider;
                }
                
                _provider = new LazyWrapper<IServiceProvider>(Factory, LazyThreadSafetyMode.ExecutionAndPublication);
            }
            
            private void OnChanged(Object? sender, ServiceProviderEventArgs args)
            {
                Changed?.Invoke(this, args);
            }
            
            public Object? GetService(Type service)
            {
                return _provider.Value.GetService(service);
            }
            
            IServiceProvider? IChangeableServiceProvider.Rebuild()
            {
                return (Provider.Unwrap() as IChangeableServiceProvider)?.Rebuild();
            }
            
            void IChangeableServiceProvider.Final()
            {
                _final = true;
                (Provider.Unwrap() as IChangeableServiceProvider)?.Final();
            }
            
            public IEnumerator<ServiceDescriptor> GetEnumerator()
            {
                return ((IEnumerable<ServiceDescriptor>) ((IServiceCollection) _provider.Value).ToArray()).GetEnumerator();
            }
            
            IEnumerator IEnumerable.GetEnumerator()
            {
                return GetEnumerator();
            }
        }
        
        private sealed class ServiceProviderWrapper : DynamicServiceProviderWrapper, ICustomServiceProvider
        {
            IServiceCollection ICustomServiceProvider.Services
            {
                get
                {
                    return Collection;
                }
            }
            
            IReadOnlyCollection<Assembly>? IAssemblyServiceProvider.Assemblies
            {
                get
                {
                    return Storage.TryGetValue(this, out ConcurrentHashSet<Assembly>? result) ? result : null;
                }
            }
            
            IServiceProvider ILazy<IServiceProvider>.Value
            {
                get
                {
                    return Provider;
                }
            }
            
            public Boolean IsValueCreated
            {
                get
                {
                    return true;
                }
            }
            
            public Boolean IsScan
            {
                get
                {
                    return (Provider as IAssemblyServiceProvider)?.IsScan ?? true;
                }
            }
            
            public ServiceProviderWrapper(IServiceProvider provider)
                : base(provider)
            {
            }
            
            public ServiceProviderWrapper(IServiceProvider provider, IServiceCollection? collection)
                : base(provider, collection)
            {
            }
        }
    }
}