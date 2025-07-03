// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Diagnostics.CodeAnalysis;
using System.Reflection;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace NetExtender.Newtonsoft
{
    public sealed class GenericContractResolver : NewtonsoftContractResolverWrapper
    {
        public static IContractResolver Instance { get; } = new GenericContractResolver();

        public GenericContractResolver()
        {
        }

        public GenericContractResolver(DefaultContractResolver? resolver)
            : base(resolver)
        {
        }
        
        protected override JsonConverter? ResolveContractConverter(Type type)
        {
            return TryResolveGenericContractConverter(type, out JsonConverter? converter) ? converter : base.ResolveContractConverter(type);
        }

        private static Boolean TryResolveGenericContractConverter(Type? type, [MaybeNullWhen(false)] out JsonConverter converter)
        {
            if (type is not { IsGenericType: true, IsGenericTypeDefinition: false })
            {
                converter = null;
                return false;
            }

            JsonConverterAttribute? attribute = type.GetCustomAttribute<JsonConverterAttribute>();

            if (attribute is not null && attribute.ConverterType.IsGenericTypeDefinition)
            {
                 converter = Activator.CreateInstance(attribute.ConverterType.MakeGenericType(type.GenericTypeArguments), attribute.ConverterParameters) as JsonConverter;
                 return converter is not null;
            }

            converter = null;
            return false;
        }
    }
}

namespace NetExtender.Serialization.Json
{
    using System.Text.Json;
    using System.Text.Json.Serialization;
    
    public sealed class GenericJsonConverterFactory : TextJsonConverterFactoryWrapper
    {
        public GenericJsonConverterFactory()
        {
        }

        public GenericJsonConverterFactory(JsonConverterFactory factory)
            : base(factory)
        {
        }
        
        public override Boolean CanConvert(Type type)
        {
            return type is { IsGenericType: true, IsGenericTypeDefinition: false };
        }

        public override JsonConverter? CreateConverter(Type type, JsonSerializerOptions options)
        {
            return TryCreateGenericConverter(type, options, out JsonConverter? converter) ? converter : null;
        }

        internal static Boolean TryCreateGenericConverter(Type type, JsonSerializerOptions options, [MaybeNullWhen(false)] out JsonConverter result)
        {
            if (type is null)
            {
                throw new ArgumentNullException(nameof(type));
            }

            if (type.GetCustomAttribute<JsonConverterAttribute>() is not { ConverterType: { IsGenericType: true, IsGenericTypeDefinition: true } converter })
            {
                result = null;
                return false;
            }

            converter = converter.MakeGenericType(type.GenericTypeArguments);
            result = Activator.CreateInstance(converter, Array.Empty<Object>()) as JsonConverter;
            return result is not null;
        }
    }
}