// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using NetExtender.Types.Dictionaries;
using NetExtender.Utilities.Types;
using Newtonsoft.Json.Serialization;

namespace NetExtender.NewtonSoft
{
    public class SingletonCompositeContractResolver : DefaultContractResolver, IEnumerable<IContractResolver>
    {
        private IndexDictionary<Type, IContractResolver> Resolvers { get; } = new IndexDictionary<Type, IContractResolver>();

        public Int32 Count
        {
            get
            {
                return Resolvers.Count;
            }
        }

        public override JsonContract ResolveContract(Type type)
        {
            return Resolvers.Values.Reverse()
                    .Select(x => x.ResolveContract(type))
                    .WhereNotNull()
                    .FirstOrDefault() ?? base.ResolveContract(type);
        }

        public void Add(IContractResolver resolver)
        {
            if (resolver is null)
            {
                throw new ArgumentNullException(nameof(resolver));
            }
            
            Resolvers.Add(resolver.GetType(), resolver);
        }
        
        public void Add<T>() where T : class, IContractResolver, new()
        {
            if (Resolvers.ContainsKey(typeof(T)))
            {
                return;
            }
            
            Resolvers.Add(typeof(T), new T());
        }

        public void Add<T>(T resolver) where T : class, IContractResolver
        {
            if (resolver is null)
            {
                throw new ArgumentNullException(nameof(resolver));
            }

            Resolvers.Add(typeof(T), resolver);
        }

        public IContractResolver GetOrAdd(IContractResolver resolver)
        {
            if (resolver is null)
            {
                throw new ArgumentNullException(nameof(resolver));
            }

            return Resolvers.GetOrAdd(resolver.GetType(), resolver);
        }

        public T GetOrAdd<T>() where T : class, IContractResolver, new()
        {
            return (T) Resolvers.GetOrAdd(typeof(T), () => new T());
        }

        public T GetOrAdd<T>(T resolver) where T : class, IContractResolver
        {
            if (resolver is null)
            {
                throw new ArgumentNullException(nameof(resolver));
            }

            return (T) Resolvers.GetOrAdd(typeof(T), resolver);
        }
        
        public SingletonCompositeContractResolver SetPosition(IContractResolver resolver, Int32 index)
        {
            if (resolver is null)
            {
                throw new ArgumentNullException(nameof(resolver));
            }

            Int32 count = Resolvers.Count;
            if (index < 0 || index >= count)
            {
                throw new ArgumentOutOfRangeException(nameof(index), index, null);
            }
            
            if (!Resolvers.Remove(resolver.GetType(), out resolver!))
            {
                return this;
            }
            
            if (index < count - 1)
            {
                Resolvers.Insert(index, resolver.GetType(), resolver);
                return this;
            }

            Resolvers.Add(resolver.GetType(), resolver);
            return this;
        }

        public SingletonCompositeContractResolver SetPosition<T>(Int32 index) where T : class, IContractResolver
        {
            return Resolvers.TryGetValue(typeof(T), out IContractResolver? resolver) ? SetPosition(resolver, index) : this;
        }

        public IEnumerator<IContractResolver> GetEnumerator()
        {
            return Resolvers.Values.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}