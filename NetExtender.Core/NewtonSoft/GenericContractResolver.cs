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
            TypeInfo info = type.GetTypeInfo();
            
            if (!info.IsGenericType || info.IsGenericTypeDefinition)
            {
                return base.ResolveContractConverter(type);
            }

            JsonConverterAttribute? attribute = info.GetCustomAttribute<JsonConverterAttribute>();
            
            if (attribute is not null && attribute.ConverterType.GetTypeInfo().IsGenericTypeDefinition)
            {
                return (JsonConverter?) Activator.CreateInstance(attribute.ConverterType.MakeGenericType(info.GenericTypeArguments), attribute.ConverterParameters);
            }
            
            return base.ResolveContractConverter(type);
        }
    }
}