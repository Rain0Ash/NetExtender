// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Collections;
using System.Collections.Generic;
using NetExtender.Utilities.Types;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace NetExtender.NewtonSoft
{
    public class OverrideConvertContractResolver : DefaultContractResolver, IReadOnlyDictionary<Type, JsonConverter?>
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
        {
            Override = new Dictionary<Type, JsonConverter?>(4);
        }

        public OverrideConvertContractResolver(IEnumerable<KeyValuePair<Type, JsonConverter?>> converters)
        {
            if (converters is null)
            {
                throw new ArgumentNullException(nameof(converters));
            }

            Override = converters.ToDictionary();
        }

        protected override JsonConverter? ResolveContractConverter(Type objectType)
        {
            return Override.TryGetValue(objectType, out JsonConverter? converter) ? converter : base.ResolveContractConverter(objectType);
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