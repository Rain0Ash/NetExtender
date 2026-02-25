// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Collections;
using System.Collections.Generic;
using NetExtender.Utilities.Types;
using Newtonsoft.Json.Serialization;

namespace NetExtender.Newtonsoft
{
    public class SingletonCompositeContractResolver : NewtonsoftContractResolverWrapper, IEnumerable<IContractResolver>
    {
        private List<IContractResolver> Resolvers { get; } = new List<IContractResolver>();
        private Dictionary<Type, IContractResolver> Map { get; } = new Dictionary<Type, IContractResolver>();

        public Int32 Count
        {
            get
            {
                return Resolvers.Count;
            }
        }

        public SingletonCompositeContractResolver()
        {
        }

        public SingletonCompositeContractResolver(DefaultContractResolver? resolver)
            : base(resolver)
        {
        }

        public override JsonContract ResolveContract(Type type)
        {
            for (Int32 i = Resolvers.Count - 1; i >= 0; i--)
            {
                if (Resolvers[i].ResolveContract(type) is { } contract)
                {
                    return contract;
                }
            }

            return Resolver?.ResolveContract(type) ?? base.ResolveContract(type);
        }

        public Boolean Add(IContractResolver resolver)
        {
            if (resolver is null)
            {
                throw new ArgumentNullException(nameof(resolver));
            }

            Type type = resolver.GetType();
            if (!Map.TryAdd(type, resolver))
            {
                return false;
            }

            Resolvers.Add(resolver);
            return true;
        }

        public Boolean Add<T>() where T : class, IContractResolver, new()
        {
            if (Map.ContainsKey(typeof(T)))
            {
                return false;
            }

            T resolver = new T();
            Map.Add(typeof(T), resolver);
            Resolvers.Add(resolver);
            return true;
        }

        public Boolean Add<T>(T resolver) where T : class, IContractResolver
        {
            return Add((IContractResolver) resolver);
        }

        public IContractResolver GetOrAdd(IContractResolver resolver)
        {
            if (resolver is null)
            {
                throw new ArgumentNullException(nameof(resolver));
            }

            Type type = resolver.GetType();
            if (Map.TryGetValue(type, out IContractResolver? result))
            {
                return result;
            }

            Map.Add(type, resolver);
            Resolvers.Add(resolver);
            return resolver;
        }

        public T GetOrAdd<T>() where T : class, IContractResolver, new()
        {
            if (Map.TryGetValue(typeof(T), out IContractResolver? result))
            {
                return (T) result;
            }

            T resolver = new T();
            Map.Add(typeof(T), resolver);
            Resolvers.Add(resolver);
            return resolver;
        }

        public T GetOrAdd<T>(T resolver) where T : class, IContractResolver
        {
            return (T) GetOrAdd((IContractResolver) resolver);
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

            Type type = resolver.GetType();
            if (!Map.TryGetValue(type, out resolver!))
            {
                Map.Add(type, resolver);
            }
            else
            {
                Resolvers.Remove(resolver);
            }

            if (index < count - 1)
            {
                Resolvers.Insert(index, resolver);
            }
            else
            {
                Resolvers.Add(resolver);
            }

            return this;
        }

        public SingletonCompositeContractResolver SetPosition<T>(Int32 index) where T : class, IContractResolver
        {
            return Map.TryGetValue(typeof(T), out IContractResolver? resolver) ? SetPosition(resolver, index) : this;
        }

        public List<IContractResolver>.Enumerator GetEnumerator()
        {
            return Resolvers.GetEnumerator();
        }

        IEnumerator<IContractResolver> IEnumerable<IContractResolver>.GetEnumerator()
        {
            return GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}