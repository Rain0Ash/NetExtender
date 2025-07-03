using System;
using NetExtender.Types.Monads;
using NetExtender.Utilities.Serialization;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace NetExtender.Newtonsoft.Types.Monads
{
    public sealed class MaybeJsonConverter<T> : NewtonsoftJsonConverter<Maybe<T>>
    {
        protected internal override Maybe<T> Read(in JsonReader reader, Type type, Maybe<Maybe<T>> exist, ref SerializerOptions options)
        {
            return reader.TokenType switch
            {
                JsonToken.Null => default,
                JsonToken.StartObject when JObject.Load(reader) is { } @object => !@object.HasValues ? default(T)! : throw new JsonSerializationException($"Unexpected non-empty object serialization to '{typeof(Maybe<T>).Name}'."),
                JsonToken.StartArray when JArray.Load(reader) is { } array => array.Count <= 0 ? default(T)! : throw new JsonSerializationException($"Unexpected non-empty array serialization to '{typeof(Maybe<T>).Name}'."),
                _ => new Maybe<T>(options.Deserialize<T>(reader)!)
            };
        }

        protected internal override Boolean Write(in JsonWriter writer, Maybe<T> value, ref SerializerOptions options)
        {
            if (!value.HasValue)
            {
                writer.WriteNull();
                return true;
            }

            if (value.Internal is null)
            {
                writer.WriteObject();
                return true;
            }

            options.Serialize(writer, (T) value);
            return true;
        }
    }
}

namespace NetExtender.Serialization.Json.Monads
{
    using System.Text.Json;

    public sealed class MaybeJsonConverter<T> : TextJsonConverter<Maybe<T>>
    {
        protected internal override Maybe<T> Read(ref Utf8JsonReader reader, Type type, ref SerializerOptions options)
        {
            return reader.TokenType switch
            {
                JsonTokenType.Null => default,
                JsonTokenType.StartObject => reader.IsEmptyObject() ? default(T)! : throw new JsonException($"Unexpected non-empty object serialization to '{typeof(Maybe<T>).Name}'."),
                JsonTokenType.StartArray => reader.IsEmptyArray() ? default(T)! : throw new JsonException($"Unexpected non-empty array serialization to '{typeof(Maybe<T>).Name}'."),
                _ => new Maybe<T>(JsonSerializer.Deserialize<T>(ref reader, options)!)
            };
        }

        protected internal override Boolean Write(in Utf8JsonWriter writer, Maybe<T> value, ref SerializerOptions options)
        {
            if (!value.HasValue)
            {
                writer.WriteNullValue();
                return true;
            }

            if (value.Internal is null)
            {
                writer.WriteObject();
                return true;
            }

            JsonSerializer.Serialize(writer, (T) value, options);
            return true;
        }
    }
}