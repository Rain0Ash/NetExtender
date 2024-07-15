using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using Microsoft.Extensions.DependencyInjection;
using NetExtender.AspNetCore.Types.DependencyInjection.Interfaces;
using NetExtender.Types.Collections;
using NetExtender.Types.Collections.Interfaces;
using NetExtender.Utilities.Types;

namespace NetExtender.AspNetCore.Types.DependencyInjection
{
    public class DynamicServiceProvider : IObservableServiceProvider, ISuppressObservableCollection<ServiceDescriptor>
    {
        protected ISuppressObservableCollection<ServiceDescriptor> Collection { get; }
        
        public event NotifyCollectionChangedEventHandler? CollectionChanged;
        public event PropertyChangingEventHandler? PropertyChanging;
        public event PropertyChangedEventHandler? PropertyChanged;
        
        private IServiceProvider? _provider;
        public IServiceProvider Provider
        {
            get
            {
                lock (SyncRoot)
                {
                    return _provider ?? this.RaiseAndSetIfChanged(ref _provider, new ServiceProvider(Build().BuildServiceProvider(Options)))!;
                }
            }
        }

        public ServiceProviderOptions? Options { get; init; }
        
        public Boolean IsAllowSuppress
        {
            get
            {
                return Collection.IsAllowSuppress;
            }
        }
        
        public Boolean IsSuppressed
        {
            get
            {
                return Collection.IsSuppressed;
            }
        }
        
        public Int32 SuppressDepth
        {
            get
            {
                return Collection.SuppressDepth;
            }
        }
        
        public Int32 Count
        {
            get
            {
                return Collection.Count;
            }
        }
        
        public Object SyncRoot { get; } = ConcurrentUtilities.SyncRoot;
        
        public Boolean IsSynchronized
        {
            get
            {
                return false;
            }
        }
        
        Boolean ICollection<ServiceDescriptor>.IsReadOnly
        {
            get
            {
                return Collection.IsReadOnly;
            }
        }

        public DynamicServiceProvider()
        {
            Collection = new SuppressObservableCollection<ServiceDescriptor>();
            Collection.CollectionChanged += OnCollectionChanged;
            Collection.PropertyChanging += OnPropertyChanging;
            Collection.PropertyChanged += OnPropertyChanged;
        }
        
        public DynamicServiceProvider(IEnumerable<ServiceDescriptor> services)
        {
            Collection = new SuppressObservableCollection<ServiceDescriptor>(services);
        }

        public DynamicServiceProvider(List<ServiceDescriptor> services)
        {
            Collection = new SuppressObservableCollection<ServiceDescriptor>(services);
        }
        
        private void OnCollectionChanged(Object? sender, NotifyCollectionChangedEventArgs args)
        {
            CollectionChanged?.Invoke(this, args);
            Reset();
        }
        
        private void OnPropertyChanging(Object? sender, PropertyChangingEventArgs args)
        {
            PropertyChanging?.Invoke(this, args);
        }
        
        private void OnPropertyChanged(Object? sender, PropertyChangedEventArgs args)
        {
            PropertyChanged?.Invoke(this, args);
        }
        
        private ServiceCollection Build()
        {
            ServiceCollection collection = new ServiceCollection();
            collection.AddRange(Collection);
            return collection;
        }
        
        public IServiceProvider Rebuild()
        {
            lock (SyncRoot)
            {
                if (_provider is ServiceProvider provider)
                {
                    provider.Dispose();
                }

                return this.RaiseAndSetIfChanged(ref _provider, new ServiceProvider(Build().BuildServiceProvider(Options)), nameof(Provider))!;
            }
        }

        public void Reset()
        {
            lock (SyncRoot)
            {
                if (_provider is ServiceProvider provider)
                {
                    provider.Dispose();
                }

                this.RaiseAndSetIfChanged(ref _provider, (IServiceProvider?) null, nameof(Provider));
            }
        }
        
        public Object? GetService(Type serviceType)
        {
            return Provider.GetService(serviceType);
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
        
        public void Add(ServiceDescriptor item)
        {
            if (item is null)
            {
                throw new ArgumentNullException(nameof(item));
            }
            
            Collection.Add(item);
        }
        
        public void Insert(Int32 index, ServiceDescriptor item)
        {
            if (item is null)
            {
                throw new ArgumentNullException(nameof(item));
            }
            
            Collection.Insert(index, item);
        }
        
        public void Move(Int32 oldIndex, Int32 newIndex)
        {
            Collection.Move(oldIndex, newIndex);
        }
        
        public Boolean Remove(ServiceDescriptor item)
        {
            if (item is null)
            {
                throw new ArgumentNullException(nameof(item));
            }
            
            return Collection.Remove(item);
        }
        
        public void RemoveAt(Int32 index)
        {
            Collection.RemoveAt(index);
        }
        
        public void Clear()
        {
            Collection.Clear();
        }
        
        public void CopyTo(Array array, Int32 index)
        {
            if (array is not ServiceDescriptor[] services)
            {
                throw new ArgumentException($"Array is not {nameof(ServiceDescriptor)} array", nameof(array));
            }
            
            CopyTo(services, index);
        }
        
        public void CopyTo(ServiceDescriptor[] array, Int32 arrayIndex)
        {
            Collection.CopyTo(array, arrayIndex);
        }
        
        public IEnumerator<ServiceDescriptor> GetEnumerator()
        {
            return Collection.GetEnumerator();
        }
        
        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
        
        public IDisposable? Suppress()
        {
            return Collection.Suppress();
        }
        
        public ServiceDescriptor this[Int32 index]
        {
            get
            {
                return Collection[index];
            }
            set
            {
                Collection[index] = value;
            }
        }
        
        protected sealed class ServiceProvider : IServiceProvider, IDisposable
        {
            public IServiceProvider Provider { get; }
            private Boolean _disposed;
            
            public ServiceProvider(IServiceProvider provider)
            {
                Provider = provider ?? throw new ArgumentNullException(nameof(provider));
            }
            
            public Object? GetService(Type serviceType)
            {
                if (_disposed)
                {
                    throw new ObjectDisposedException(nameof(ServiceProvider));
                }
                
                return Provider.GetService(serviceType);
            }
            
            public override Int32 GetHashCode()
            {
                return Provider.GetHashCode();
            }
            
            public override Boolean Equals(Object? other)
            {
                return Provider.Equals(other);
            }
            
            public override String? ToString()
            {
                return Provider.ToString();
            }
            
            public void Dispose()
            {
                Dispose(true);
                GC.SuppressFinalize(this);
            }
            
            // ReSharper disable once UnusedParameter.Local
            private void Dispose(Boolean disposing)
            {
                _disposed = true;
            }
            
            ~ServiceProvider()
            {
                Dispose(false);
            }
        }
    }
}