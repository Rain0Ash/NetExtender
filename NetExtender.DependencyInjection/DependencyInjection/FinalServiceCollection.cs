using System;
using System.Collections;
using System.Collections.Generic;
using Microsoft.Extensions.DependencyInjection;
using NetExtender.DependencyInjection.Exceptions;
using NetExtender.DependencyInjection.Interfaces;
using NetExtender.Utilities.Types;

namespace NetExtender.DependencyInjection
{
    public class FinalServiceCollection : IFinalServiceCollection
    {
        protected IServiceCollection Collection { get; }
        
        public Int32 Count
        {
            get
            {
                return Collection.Count;
            }
        }
        
        private Func<Boolean> _final;
        public Boolean IsFinal
        {
            get
            {
                return _final() || Collection is IFinalServiceCollection { IsFinal: true } || Collection is IChangeableServiceProvider { IsFinal: true };
            }
        }
        
        private readonly Object _sync = ConcurrentUtilities.SyncRoot;
        public Object SyncRoot
        {
            get
            {
                return (Collection as ICollection)?.SyncRoot ?? _sync;
            }
        }
        
        public Boolean IsSynchronized
        {
            get
            {
                return (Collection as ICollection)?.IsSynchronized ?? false;
            }
        }
        
        public Boolean IsReadOnly
        {
            get
            {
                return IsFinal || Collection.IsReadOnly;
            }
        }
        
        public FinalServiceCollection()
            : this(static () => false)
        {
        }
        
        public FinalServiceCollection(Func<Boolean> final)
        {
            _final = final ?? throw new ArgumentNullException(nameof(final));
            Collection = new ServiceCollection();
        }
        
        public FinalServiceCollection(IEnumerable<ServiceDescriptor> source)
            : this(source, false)
        {
        }
        
        public FinalServiceCollection(IEnumerable<ServiceDescriptor> source, Boolean final)
            : this(source is not null ? source : throw new ArgumentNullException(nameof(source)), final ? static () => true : static () => false)
        {
        }
        
        public FinalServiceCollection(IEnumerable<ServiceDescriptor> source, Func<Boolean> final)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }
            
            _final = final;
            Collection = new ServiceCollection();
            Collection.AddRange(source.WhereNotNull());
        }
        
        public FinalServiceCollection(IServiceCollection collection)
            : this(collection, false)
        {
        }
        
        public FinalServiceCollection(IServiceCollection collection, Boolean final)
            : this(collection is not null ? collection : throw new ArgumentNullException(nameof(collection)), final ? static () => true : static () => false)
        {
        }
        
        public FinalServiceCollection(IServiceCollection collection, Func<Boolean> final)
        {
            Collection = collection ?? throw new ArgumentNullException(nameof(collection));
            _final = final ?? throw new ArgumentNullException(nameof(final));
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
            
            if (IsFinal)
            {
                throw new ServiceCollectionFinalException();
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
                throw new ServiceCollectionFinalException();
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
                throw new ServiceCollectionFinalException();
            }
            
            return Collection.Remove(item);
        }
        
        public void RemoveAt(Int32 index)
        {
            if (IsFinal)
            {
                throw new ServiceCollectionFinalException();
            }
            
            Collection.RemoveAt(index);
        }
        
        public void Clear()
        {
            if (IsFinal)
            {
                throw new ServiceCollectionFinalException();
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
        
        public void CopyTo(ServiceDescriptor[] array, Int32 index)
        {
            Collection.CopyTo(array, index);
        }
        
        public void Final()
        {
            _final = static () => true;
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
                    throw new ServiceCollectionFinalException();
                }
                
                Collection[index] = value;
            }
        }
    }
}