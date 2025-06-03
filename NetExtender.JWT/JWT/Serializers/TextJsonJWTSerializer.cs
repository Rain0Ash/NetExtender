// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;
using NetExtender.Types.Exceptions;

namespace NetExtender.JWT
{
    public class TextJsonJWTSerializer : JWTSerializer
    {
        private static JsonSerializerOptions DefaultSerializeOptions { get; } = new JsonSerializerOptions();
        private static JsonSerializerOptions DefaultDeserializeOptions { get; } = new JsonSerializerOptions
        {
            Converters =
            {
                new DictionaryStringObjectJsonConverterCustomWrite()
            }
        };

        protected JsonSerializerOptions SerializeOptions { get; }
        protected JsonSerializerOptions DeserializeOptions { get; }

        public sealed override JWTSerializerType Type
        {
            get
            {
                return JWTSerializerType.TextJson;
            }
        }

        public TextJsonJWTSerializer()
            : this(DefaultSerializeOptions, DefaultDeserializeOptions)
        {
        }

        protected TextJsonJWTSerializer(JsonSerializerOptions serialize, JsonSerializerOptions deserialize)
        {
            SerializeOptions = serialize ?? throw new ArgumentNullException(nameof(serialize));
            DeserializeOptions = deserialize ?? throw new ArgumentNullException(nameof(deserialize));
        }

        public override String Serialize<T>(T value)
        {
            if (value is null)
            {
                throw new ArgumentNullException(nameof(value));
            }

            return JsonSerializer.Serialize(value, SerializeOptions);
        }

        public override Object Deserialize(Type type, String json)
        {
            if (type is null)
            {
                throw new ArgumentNullException(nameof(type));
            }

            if (String.IsNullOrEmpty(json))
            {
                throw new ArgumentNullOrEmptyStringException(json, nameof(json));
            }

            return JsonSerializer.Deserialize(json, type, DeserializeOptions) ?? throw new ArgumentException($"Can't deserialize json to '{type}'.");
        }

        public override T Deserialize<T>(String json)
        {
            if (String.IsNullOrEmpty(json))
            {
                throw new ArgumentNullOrEmptyStringException(json, nameof(json));
            }

            return JsonSerializer.Deserialize<T>(json, DeserializeOptions) ?? throw new ArgumentException($"Can't deserialize json to '{typeof(T).Name}'.");
        }

        public class Serializer : TextJsonJWTSerializer
        {
            public Serializer()
            {
            }

            protected Serializer(JsonSerializerOptions serialize, JsonSerializerOptions deserialize)
                : base(serialize, deserialize)
            {
            }

            public sealed override String Serialize<T>(T value)
            {
                return base.Serialize(value);
            }

            public sealed override Object Deserialize(Type type, String json)
            {
                return base.Deserialize(type, json);
            }

            public sealed override T Deserialize<T>(String json)
            {
                return base.Deserialize<T>(json);
            }
        }

        private sealed class DictionaryStringObjectJsonConverterCustomWrite : JsonConverter<Dictionary<String, Object?>>
        {
            public override Dictionary<String, Object?> Read(ref Utf8JsonReader reader, Type? type, JsonSerializerOptions options)
            {
                if (reader.TokenType != JsonTokenType.StartObject)
                {
                    throw new JsonException($"{nameof(JsonTokenType)} was of type '{reader.TokenType}', only objects are supported.");
                }

                Dictionary<String, Object?> result = new Dictionary<String, Object?>(8);
                
                while (reader.Read())
                {
                    if (reader.TokenType == JsonTokenType.EndObject)
                    {
                        return result;
                    }

                    if (reader.TokenType != JsonTokenType.PropertyName)
                    {
                        throw new JsonException($"{nameof(JsonTokenType)} was not {nameof(JsonTokenType.PropertyName)}.");
                    }

                    String? property = reader.GetString();
                    if (String.IsNullOrWhiteSpace(property))
                    {
                        throw new JsonException("Can't get property name.");
                    }

                    reader.Read();
                    result.Add(property, Get(ref reader, options));
                }

                return result;
            }

            public override void Write(Utf8JsonWriter writer, Dictionary<String, Object?> value, JsonSerializerOptions options)
            {
                throw new NotSupportedException();
            }

            private Object? Get(ref Utf8JsonReader reader, JsonSerializerOptions options)
            {
                switch (reader.TokenType)
                {
                    case JsonTokenType.String:
                    {
                        return reader.TryGetDateTime(out DateTime date) ? date : reader.GetString();
                    }
                    case JsonTokenType.False:
                    {
                        return false;
                    }
                    case JsonTokenType.True:
                    {
                        return true;
                    }
                    case JsonTokenType.Null:
                    {
                        return null;
                    }
                    case JsonTokenType.Number:
                    {
                        return reader.TryGetInt64(out Int64 result) ? result : reader.GetDecimal();
                    }
                    case JsonTokenType.StartObject:
                    {
                        return Read(ref reader, null, options);
                    }
                    case JsonTokenType.StartArray:
                    {
                        List<Object?> list = new List<Object?>(16);
                        while (reader.Read() && reader.TokenType != JsonTokenType.EndArray)
                        {
                            list.Add(Get(ref reader, options));
                        }

                        return list;
                    }
                    default:
                    {
                        throw new JsonException($"Token '{reader.TokenType}' is not supported.");
                    }
                }
            }
        }
    }
}
