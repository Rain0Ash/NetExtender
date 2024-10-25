using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Reflection;
using Microsoft.Extensions.DependencyInjection;
using NetExtender.DependencyInjection.Events;
using NetExtender.DependencyInjection.Interfaces;
using NetExtender.Types.Monads.Interfaces;
using NetExtender.Types.Storages;
using NetExtender.Types.Storages.Interfaces;

namespace NetExtender.Utilities.Types
{
    public interface IAssemblyServiceProvider : IChangeableServiceProvider, ILazy<IServiceProvider>, IScannable
    {
        public IReadOnlyCollection<Assembly>? Assemblies { get; }
    }
    
    public static partial class ServiceProviderUtilities
    {
        private static IStorage<IServiceProvider, ConcurrentHashSet<Assembly>> Storage { get; } = new WeakStorage<IServiceProvider, ConcurrentHashSet<Assembly>>();
        
        public static IAssemblyServiceProvider Provider { get; private set; }
        
        private static IReadOnlyCollection<ServiceDescriptor>? _services;
        public static IReadOnlyCollection<ServiceDescriptor> Services
        {
            get
            {
                return _services ??= new ReadOnlyCollection<ServiceDescriptor>(((ICustomServiceProvider) Provider).Services);
            }
        }
        
        public static event ServiceProviderChangedEventHandler? Changed;

        static ServiceProviderUtilities()
        {
            Provider = new ServiceProvider();
            Provider.Changed += OnChanged;
            AppDomain.CurrentDomain.AssemblyLoad += OnAssemblyLoad;
        }
        
        private static void OnAssemblyLoad(Object? sender, AssemblyLoadEventArgs args)
        {
            if (!Provider.IsScan)
            {
                return;
            }
            
            if (!Provider.IsValueCreated || Provider is not IDynamicServiceProvider { IsFinal: false } provider || args.LoadedAssembly is not { } assembly)
            {
                return;
            }

            provider.Scan(assembly);
            Storage.GetOrAdd(provider, static _ => new ConcurrentHashSet<Assembly>()).Add(assembly);
        }
        
        private static void OnChanged(Object? sender, ServiceProviderEventArgs args)
        {
            Changed?.Invoke(sender, args);
        }
        
        public static void Reset(IServiceProvider provider)
        {
            if (provider is null)
            {
                throw new ArgumentNullException(nameof(provider));
            }
            
            Reset(provider as IAssemblyServiceProvider ?? new ServiceProviderWrapper(provider));
        }
        
        private static void Reset(IAssemblyServiceProvider provider)
        {
            Provider = provider ?? throw new ArgumentNullException(nameof(provider));
            provider.Changed -= OnChanged;
            provider.Changed += OnChanged;
        }
    }
}