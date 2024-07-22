using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using Microsoft.Extensions.DependencyInjection;
using NetExtender.DependencyInjection.Events;
using NetExtender.DependencyInjection.Exceptions;
using NetExtender.DependencyInjection.Interfaces;
using NetExtender.Types.Collections.Interfaces;
using NetExtender.Utilities.Types;

namespace NetExtender.DependencyInjection
{
    public class DynamicServiceProviderWrapper : IObservableServiceProvider, ISuppressObservableCollection<ServiceDescriptor>
    {
        public event ServiceProviderChangedEventHandler? Changed;
        public event NotifyCollectionChangedEventHandler? CollectionChanged;
        public event PropertyChangingEventHandler? PropertyChanging;
        public event PropertyChangedEventHandler? PropertyChanged;
        
        protected IServiceProvider Provider { get; }
        protected IFinalServiceCollection Collection { get; }
        
        public Int32 Count
        {
            get
            {
                return Collection.Count;
            }
        }
        
        public Boolean IsStable
        {
            get
            {
                return Provider is not IChangeableServiceProvider provider || provider.IsStable;
            }
        }
        
        public Boolean IsFinal
        {
            get
            {
                return Provider is not IChangeableServiceProvider provider || provider.IsFinal;
            }
        }
        
        public Boolean IsAllowSuppress
        {
            get
            {
                return (Provider as ISuppressObservableCollection<ServiceDescriptor>)?.IsAllowSuppress ?? false;
            }
        }
        
        public Boolean IsSuppressed
        {
            get
            {
                return (Provider as ISuppressObservableCollection<ServiceDescriptor>)?.IsSuppressed ?? false;
            }
        }
        
        public Int32 SuppressDepth
        {
            get
            {
                return (Provider as ISuppressObservableCollection<ServiceDescriptor>)?.SuppressDepth ?? 0;
            }
        }
        
        private readonly Object _sync = ConcurrentUtilities.SyncRoot;
        public Object SyncRoot
        {
            get
            {
                return (Provider as ICollection)?.SyncRoot ?? _sync;
            }
        }
        
        public Boolean IsSynchronized
        {
            get
            {
                return (Provider as ICollection)?.IsSynchronized ?? false;
            }
        }
        
        public Boolean IsReadOnly
        {
            get
            {
                return (Provider as ICollection<ServiceDescriptor>)?.IsReadOnly ?? true;
            }
        }
        
        public DynamicServiceProviderWrapper(IServiceProvider provider)
            : this(provider, null)
        {
        }
        
        public DynamicServiceProviderWrapper(IServiceProvider provider, IServiceCollection? collection)
        {
            Provider = provider ?? throw new ArgumentNullException(nameof(provider));
            
            Collection = provider switch
            {
                IServiceCollection value => new FinalServiceCollection(value, () => IsFinal),
                _ when collection is not null => new FinalServiceCollection((IEnumerable<ServiceDescriptor>) collection, () => IsFinal),
                _ => new FinalServiceCollection(() => IsFinal)
            };
        }
        
        public Object? GetService(Type service)
        {
            if (service is null)
            {
                throw new ArgumentNullException(nameof(service));
            }
            
            return Provider.GetService(service);
        }
        
        public IServiceProvider? Rebuild()
        {
            if (IsFinal)
            {
                throw new ServiceProviderFinalException();
            }
            
            return (Provider as IChangeableServiceProvider)?.Rebuild();
        }
        
        public void Reset()
        {
            if (IsFinal)
            {
                throw new ServiceProviderFinalException();
            }
            
            (Provider as IDynamicServiceProvider)?.Reset();
        }
        
        public Boolean Contains(ServiceDescriptor item)
        {
            if (item is null)
            {
                throw new ArgumentNullException(nameof(item));
            }
            
            return Collection.Contains(item);
        }
        
        public Int32 IndexOf(ServiceDescriptor item)
        {
            if (item is null)
            {
                throw new ArgumentNullException(nameof(item));
            }
            
            return Collection.IndexOf(item);
        }
        
        public void Move(Int32 oldIndex, Int32 newIndex)
        {
            if (IsFinal)
            {
                throw new ServiceProviderFinalException();
            }
            
            if (Collection.IsFinal)
            {
                throw new ServiceCollectionFinalException();
            }
            
            if (Provider is ISuppressObservableCollection<ServiceDescriptor> collection)
            {
                collection.Move(oldIndex, newIndex);
                return;
            }
            
            (Collection[oldIndex], Collection[newIndex]) = (Collection[newIndex], Collection[oldIndex]);
        }
        
        public void Add(ServiceDescriptor item)
        {
            if (item is null)
            {
                throw new ArgumentNullException(nameof(item));
            }
            
            if (IsFinal)
            {
                throw new ServiceProviderFinalException();
            }
            
            Collection.Add(item);
        }
        
        public void Insert(Int32 index, ServiceDescriptor item)
        {
            if (item is null)
            {
                throw new ArgumentNullException(nameof(item));
            }
            
            if (IsFinal)
            {
                throw new ServiceProviderFinalException();
            }
            
            Collection.Insert(index, item);
        }
        
        public Boolean Remove(ServiceDescriptor item)
        {
            if (item is null)
            {
                throw new ArgumentNullException(nameof(item));
            }
            
            if (IsFinal)
            {
                throw new ServiceProviderFinalException();
            }
            
            return Collection.Remove(item);
        }
        
        public void RemoveAt(Int32 index)
        {
            if (IsFinal)
            {
                throw new ServiceProviderFinalException();
            }
            
            Collection.RemoveAt(index);
        }
        
        public void Clear()
        {
            if (IsFinal)
            {
                throw new ServiceProviderFinalException();
            }
            
            Collection.Clear();
        }
        
        public void CopyTo(Array array, Int32 index)
        {
            Collection.CopyTo(array, index);
        }
        
        public void CopyTo(ServiceDescriptor[] array, Int32 index)
        {
            Collection.CopyTo(array, index);
        }
        
        public void Final()
        {
            Collection.Final();
        }
        
        public IDisposable? Suppress()
        {
            return null;
        }
        
        public IEnumerator<ServiceDescriptor> GetEnumerator()
        {
            return Collection.GetEnumerator();
        }
        
        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
        
        public ServiceDescriptor this[Int32 index]
        {
            get
            {
                return Collection[index];
            }
            set
            {
                // ReSharper disable once JoinNullCheckWithUsage
                if (value is null)
                {
                    throw new ArgumentNullException(nameof(value));
                }
                
                if (IsFinal)
                {
                    throw new ServiceProviderFinalException();
                }
                
                Collection[index] = value;
            }
        }
    }
}