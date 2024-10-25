using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using Microsoft.Extensions.DependencyInjection;
using NetExtender.DependencyInjection.Events;
using NetExtender.DependencyInjection.Exceptions;
using NetExtender.DependencyInjection.Interfaces;
using NetExtender.Types.Collections;
using NetExtender.Types.Collections.Interfaces;
using NetExtender.Utilities.Types;

namespace NetExtender.DependencyInjection
{
    public class DynamicServiceProvider : IObservableServiceProvider, ISuppressObservableCollection<ServiceDescriptor>
    {
        protected ISuppressObservableCollection<ServiceDescriptor> Collection { get; }
        
        public event NotifyCollectionChangedEventHandler? CollectionChanged;
        public event PropertyChangingEventHandler? PropertyChanging;
        public event PropertyChangedEventHandler? PropertyChanged;
        public event ServiceProviderChangedEventHandler? Changed;
        
        private ServiceProvider? _provider;
        public IServiceProvider Provider
        {
            get
            {
                lock (SyncRoot)
                {
                    return _provider ?? this.RaiseAndSetIfChanged(ref _provider, new ServiceProvider(Build(Build())) { IsStable = IsStable } )!;
                }
            }
        }

        public ServiceProviderOptions? Options { get; init; }
        
        private Boolean _stable;
        public Boolean IsStable
        {
            get
            {
                lock (SyncRoot)
                {
                    return _provider?.IsStable ?? _stable;
                }
            }
            set
            {
                lock (SyncRoot)
                {
                    if (_provider is null)
                    {
                        this.RaiseAndSetIfChanged(ref _stable, value);
                        return;
                    }
                    
                    _provider.IsStable = value;
                    this.RaiseAndSetIfChanged(ref _stable, _provider.IsStable);
                }
            }
        }
        
        private Boolean _final;
        public Boolean IsFinal
        {
            get
            {
                lock (SyncRoot)
                {
                    return _final;
                }
            }
            set
            {
                lock (SyncRoot)
                {
                    this.RaiseAndSetIfChanged(ref _final, value);
                }
            }
        }
        
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
                return _final || Collection.IsReadOnly;
            }
        }

        public DynamicServiceProvider()
        {
            Collection = new SuppressObservableCollection<ServiceDescriptor>();
            Collection.CollectionChanged += OnCollectionChanged;
            Collection.PropertyChanging += OnPropertyChanging;
            Collection.PropertyChanged += OnPropertyChanged;
            
            PropertyChanged += OnProviderChanged;
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
            
            if (_provider is not null && !_final)
            {
                Reset();
            }
        }
        
        private void OnPropertyChanging(Object? sender, PropertyChangingEventArgs args)
        {
            PropertyChanging?.Invoke(this, args);
        }
        
        private void OnPropertyChanged(Object? sender, PropertyChangedEventArgs args)
        {
            PropertyChanged?.Invoke(this, args);
        }
        
        private void OnProviderChanged(Object? sender, PropertyChangedEventArgs args)
        {
            if (args.PropertyName == nameof(Provider))
            {
                Changed?.Invoke(this, new ServiceProviderEventArgs(() => Provider));
            }
        }
        
        protected virtual ServiceCollection Build()
        {
            ServiceCollection collection = new ServiceCollection();
            collection.AddRange(Collection);
            return collection;
        }
        
        protected virtual IServiceProvider Build(ServiceCollection collection)
        {
            if (collection is null)
            {
                throw new ArgumentNullException(nameof(collection));
            }
            
            return Options is not null ? collection.BuildServiceProvider(Options) : collection.BuildServiceProvider();
        }
        
        public IServiceProvider Rebuild()
        {
            if (_provider is not null && _final)
            {
                throw new ServiceProviderFinalException();
            }
            
            lock (SyncRoot)
            {
                if (_provider is { } provider)
                {
                    provider.Dispose();
                }

                return this.RaiseAndSetIfChanged(ref _provider, new ServiceProvider(Build(Build())), nameof(Provider))!;
            }
        }

        public void Reset()
        {
            if (_provider is not null && _final)
            {
                throw new ServiceProviderFinalException();
            }
            
            lock (SyncRoot)
            {
                if (_provider is { } provider)
                {
                    provider.Dispose();
                }

                this.RaiseAndSetIfChanged(ref _provider, (ServiceProvider?) null, nameof(Provider));
            }
        }
        
        public Object? GetService(Type service)
        {
            if (service is null)
            {
                throw new ArgumentNullException(nameof(service));
            }
            
            return Provider.GetService(service);
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
            
            if (_provider is not null && _final)
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
            
            if (_provider is not null && _final)
            {
                throw new ServiceProviderFinalException();
            }
            
            Collection.Insert(index, item);
        }
        
        public void Move(Int32 oldIndex, Int32 newIndex)
        {
            if (_provider is not null && _final)
            {
                throw new ServiceProviderFinalException();
            }
            
            Collection.Move(oldIndex, newIndex);
        }
        
        public Boolean Remove(ServiceDescriptor item)
        {
            if (item is null)
            {
                throw new ArgumentNullException(nameof(item));
            }
            
            if (_provider is not null && _final)
            {
                throw new ServiceProviderFinalException();
            }
            
            return Collection.Remove(item);
        }
        
        public void RemoveAt(Int32 index)
        {
            if (_provider is not null && _final)
            {
                throw new ServiceProviderFinalException();
            }
            
            Collection.RemoveAt(index);
        }
        
        public void Clear()
        {
            if (_provider is not null && _final)
            {
                throw new ServiceProviderFinalException();
            }
            
            Collection.Clear();
        }
        
        public void CopyTo(Array array, Int32 index)
        {
            if (array is not ServiceDescriptor[] services)
            {
                throw new ArgumentException($"Array is not {nameof(ServiceDescriptor)} array.", nameof(array));
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
        
        void IChangeableServiceProvider.Final()
        {
            IsFinal = true;
        }
        
        public ServiceDescriptor this[Int32 index]
        {
            get
            {
                return Collection[index];
            }
            set
            {
                if (_provider is not null && _final)
                {
                    throw new ServiceProviderFinalException();
                }
                
                Collection[index] = value;
            }
        }
        
        protected sealed class ServiceProvider : IServiceProvider, IDisposable
        {
            public IServiceProvider Provider { get; }
            public Boolean IsStable { get; set; }
            
            private Boolean _disposed;
            
            public ServiceProvider(IServiceProvider provider)
            {
                Provider = provider ?? throw new ArgumentNullException(nameof(provider));
            }
            
            public Object? GetService(Type service)
            {
                if (!IsStable && _disposed)
                {
                    throw new ObjectDisposedException(nameof(ServiceProvider));
                }
                
                return Provider.GetService(service);
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