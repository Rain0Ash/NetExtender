// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Collections;
using System.Collections.Generic;
using NetExtender.Utilities.Types;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace NetExtender.Newtonsoft
{
    public class OverrideConvertContractResolver : NewtonsoftContractResolverWrapper, IReadOnlyDictionary<Type, JsonConverter?>
    {
        private Dictionary<Type, JsonConverter?> Override { get; }

        public IEnumerable<Type> Keys
        {
            get
            {
                return Override.Keys;
            }
        }

        public IEnumerable<JsonConverter?> Values
        {
            get
            {
                return Override.Values;
            }
        }

        public Int32 Count
        {
            get
            {
                return Override.Count;
            }
        }

        public OverrideConvertContractResolver()
            : this((DefaultContractResolver?) null)
        {
        }
        
        public OverrideConvertContractResolver(DefaultContractResolver? resolver)
            : base(resolver)
        {
            Override = new Dictionary<Type, JsonConverter?>(4);
        }

        public OverrideConvertContractResolver(IEnumerable<KeyValuePair<Type, JsonConverter?>> converters)
            : this(null, converters)
        {
        }

        public OverrideConvertContractResolver(DefaultContractResolver? resolver, IEnumerable<KeyValuePair<Type, JsonConverter?>> converters)
            : base(resolver)
        {
            if (converters is null)
            {
                throw new ArgumentNullException(nameof(converters));
            }

            Override = converters.ToDictionary();
        }

        protected override JsonConverter? ResolveContractConverter(Type type)
        {
            return Override.TryGetValue(type, out JsonConverter? converter) ? converter : base.ResolveContractConverter(type);
        }

        public Boolean ContainsKey(Type type)
        {
            return Override.ContainsKey(type);
        }

        public Boolean TryGetValue(Type type, out JsonConverter? value)
        {
            return Override.TryGetValue(type, out value);
        }

        public virtual OverrideConvertContractResolver Add(Type type, JsonConverter? converter)
        {
            if (type is null)
            {
                throw new ArgumentNullException(nameof(type));
            }

            Override[type] = converter;
            return this;
        }

        public OverrideConvertContractResolver Add<T>(JsonConverter? converter)
        {
            return Add(typeof(T), converter);
        }

        public OverrideConvertContractResolver Without(Type type)
        {
            return Add(type, null);
        }

        public OverrideConvertContractResolver Without<T>()
        {
            return Add<T>(null);
        }

        public OverrideConvertContractResolver Remove(Type type)
        {
            return Remove(type, out _);
        }

        public OverrideConvertContractResolver Remove(Type type, out JsonConverter? converter)
        {
            if (type is null)
            {
                throw new ArgumentNullException(nameof(type));
            }

            Override.Remove(type, out converter);
            return this;
        }

        public OverrideConvertContractResolver Remove<T>()
        {
            return Remove<T>(out _);
        }

        public OverrideConvertContractResolver Remove<T>(out JsonConverter? converter)
        {
            return Remove(typeof(T), out converter);
        }

        public IEnumerator<KeyValuePair<Type, JsonConverter?>> GetEnumerator()
        {
            return Override.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public JsonConverter? this[Type type]
        {
            get
            {
                return Override[type];
            }
        }
    }
}