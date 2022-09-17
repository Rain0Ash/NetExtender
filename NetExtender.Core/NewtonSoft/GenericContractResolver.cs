// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Reflection;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace NetExtender.NewtonSoft
{
    public sealed class GenericContractResolver : DefaultContractResolver
    {
        public static IContractResolver Instance { get; } = new GenericContractResolver();
        
        private GenericContractResolver()
        {
        }
        
        protected override JsonConverter? ResolveContractConverter(Type type)
        {
            if (!type.IsGenericType || type.IsGenericTypeDefinition)
            {
                return base.ResolveContractConverter(type);
            }

            JsonConverterAttribute? attribute = type.GetCustomAttribute<JsonConverterAttribute>();
            
            if (attribute is not null && attribute.ConverterType.IsGenericTypeDefinition)
            {
                return (JsonConverter?) Activator.CreateInstance(attribute.ConverterType.MakeGenericType(type.GenericTypeArguments), attribute.ConverterParameters);
            }
            
            return base.ResolveContractConverter(type);
        }
    }
}