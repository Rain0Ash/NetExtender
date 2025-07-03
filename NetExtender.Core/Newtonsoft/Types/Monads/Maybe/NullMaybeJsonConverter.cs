using System;
using NetExtender.Types.Monads;
using NetExtender.Utilities.Serialization;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace NetExtender.Newtonsoft.Types.Monads
{
    public sealed class NullMaybeJsonConverter<T> : NewtonsoftJsonConverter<NullMaybe<T>>
    {
        protected internal override NullMaybe<T> Read(in JsonReader reader, Type type, Maybe<NullMaybe<T>> exist, ref SerializerOptions options)
        {
            return reader.TokenType switch
            {
                JsonToken.Null => default,
                JsonToken.StartObject when JObject.Load(reader) is { } @object => !@object.HasValues ? default(T)! : throw new JsonSerializationException($"Unexpected non-empty object serialization to '{typeof(NullMaybe<T>).Name}'."),
                JsonToken.StartArray when JArray.Load(reader) is { } array => array.Count <= 0 ? default(T)! : throw new JsonSerializationException($"Unexpected non-empty array serialization to '{typeof(NullMaybe<T>).Name}'."),
                _ => new NullMaybe<T>(options.Deserialize<T>(reader)!)
            };
        }

        protected internal override Boolean Write(in JsonWriter writer, NullMaybe<T> value, ref SerializerOptions options)
        {
            if (value.IsEmpty)
            {
                writer.WriteNull();
                return true;
            }

            if (value.Value is null)
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

    public sealed class NullMaybeJsonConverter<T> : TextJsonConverter<NullMaybe<T>>
    {
        protected internal override NullMaybe<T> Read(ref Utf8JsonReader reader, Type type, ref SerializerOptions options)
        {
            return reader.TokenType switch
            {
                JsonTokenType.Null => default,
                JsonTokenType.StartObject => reader.IsEmptyObject() ? default(T)! : throw new JsonException($"Unexpected non-empty object serialization to '{typeof(NullMaybe<T>).Name}'."),
                JsonTokenType.StartArray => reader.IsEmptyArray() ? default(T)! : throw new JsonException($"Unexpected non-empty array serialization to '{typeof(NullMaybe<T>).Name}'."),
                _ => new NullMaybe<T>(JsonSerializer.Deserialize<T>(ref reader, options)!)
            };
        }

        protected internal override Boolean Write(in Utf8JsonWriter writer, NullMaybe<T> value, ref SerializerOptions options)
        {
            if (value.IsEmpty)
            {
                writer.WriteNullValue();
                return true;
            }

            if (value.Value is null)
            {
                writer.WriteObject();
                return true;
            }

            JsonSerializer.Serialize(writer, (T) value, options);
            return true;
        }
    }
}