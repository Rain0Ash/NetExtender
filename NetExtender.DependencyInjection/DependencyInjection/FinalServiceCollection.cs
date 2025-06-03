// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

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
        protected IServiceCollection Services { get; }
        
        public Int32 Count
        {
            get
            {
                return Services.Count;
            }
        }
        
        private Func<Boolean> _final;
        public Boolean IsFinal
        {
            get
            {
                return _final() || Services is IFinalServiceCollection { IsFinal: true } || Services is IChangeableServiceProvider { IsFinal: true };
            }
        }
        
        private readonly SyncRoot _sync = Utilities.Types.SyncRoot.Create();
        public Object SyncRoot
        {
            get
            {
                return (Services as ICollection)?.SyncRoot ?? _sync;
            }
        }
        
        public Boolean IsSynchronized
        {
            get
            {
                return (Services as ICollection)?.IsSynchronized ?? false;
            }
        }
        
        public Boolean IsReadOnly
        {
            get
            {
                return IsFinal || Services.IsReadOnly;
            }
        }
        
        public FinalServiceCollection()
            : this(static () => false)
        {
        }
        
        public FinalServiceCollection(Func<Boolean> final)
        {
            _final = final ?? throw new ArgumentNullException(nameof(final));
            Services = new ServiceCollection();
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
            Services = new ServiceCollection();
            Services.AddRange(source.WhereNotNull());
        }
        
        public FinalServiceCollection(IServiceCollection services)
            : this(services, false)
        {
        }
        
        public FinalServiceCollection(IServiceCollection services, Boolean final)
            : this(services is not null ? services : throw new ArgumentNullException(nameof(services)), final ? static () => true : static () => false)
        {
        }
        
        public FinalServiceCollection(IServiceCollection services, Func<Boolean> final)
        {
            Services = services ?? throw new ArgumentNullException(nameof(services));
            _final = final ?? throw new ArgumentNullException(nameof(final));
        }
        
        public Boolean Contains(ServiceDescriptor item)
        {
            if (item is null)
            {
                throw new ArgumentNullException(nameof(item));
            }
            
            return Services.Contains(item);
        }
        
        public Int32 IndexOf(ServiceDescriptor item)
        {
            if (item is null)
            {
                throw new ArgumentNullException(nameof(item));
            }
            
            return Services.IndexOf(item);
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
            
            Services.Add(item);
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
            
            Services.Insert(index, item);
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
            
            return Services.Remove(item);
        }
        
        public void RemoveAt(Int32 index)
        {
            if (IsFinal)
            {
                throw new ServiceCollectionFinalException();
            }
            
            Services.RemoveAt(index);
        }
        
        public void Clear()
        {
            if (IsFinal)
            {
                throw new ServiceCollectionFinalException();
            }
            
            Services.Clear();
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
            Services.CopyTo(array, index);
        }
        
        public void Final()
        {
            _final = static () => true;
        }
        
        public IEnumerator<ServiceDescriptor> GetEnumerator()
        {
            return Services.GetEnumerator();
        }
        
        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
        
        public ServiceDescriptor this[Int32 index]
        {
            get
            {
                return Services[index];
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
                
                Services[index] = value;
            }
        }
    }
}