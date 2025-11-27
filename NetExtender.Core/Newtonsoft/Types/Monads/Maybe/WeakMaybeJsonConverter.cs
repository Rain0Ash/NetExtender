using System;
using NetExtender.Types.Monads;
using NetExtender.Utilities.Serialization;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace NetExtender.Newtonsoft.Types.Monads
{
    public sealed class WeakMaybeJsonConverter<T> : NewtonsoftJsonConverter<WeakMaybe<T>> where T : class
    {
        protected internal override WeakMaybe<T> Read(in JsonReader reader, Type type, Maybe<WeakMaybe<T>> exist, ref SerializerOptions options)
        {
            return reader.TokenType switch
            {
                JsonToken.Null => default,
                JsonToken.StartObject when JObject.Load(reader) is { } @object => !@object.HasValues ? default(T)! : throw new JsonSerializationException($"Unexpected non-empty object serialization to '{typeof(WeakMaybe<T>).Name}'."),
                JsonToken.StartArray when JArray.Load(reader) is { } array => array.Count <= 0 ? default(T)! : throw new JsonSerializationException($"Unexpected non-empty array serialization to '{typeof(WeakMaybe<T>).Name}'."),
                _ => new WeakMaybe<T>(options.Deserialize<T>(reader)!)
            };
        }

        protected internal override Boolean Write(in JsonWriter writer, WeakMaybe<T> value, ref SerializerOptions options)
        {
            Maybe<T> maybe = value.Maybe;
            
            if (!maybe.HasValue)
            {
                writer.WriteNull();
                return true;
            }

            if (maybe.Internal is { } @object)
            {
                options.Serialize(writer, @object);
                return true;
            }

            writer.WriteObject();
            return true;
        }
    }
}

namespace NetExtender.Serialization.Json.Monads
{
    using System.Text.Json;

    public sealed class WeakMaybeJsonConverter<T> : TextJsonConverter<WeakMaybe<T>> where T : class
    {
        protected internal override WeakMaybe<T> Read(ref Utf8JsonReader reader, Type type, ref SerializerOptions options)
        {
            return reader.TokenType switch
            {
                JsonTokenType.Null => default,
                JsonTokenType.StartObject => reader.IsEmptyObject() ? default(T)! : throw new JsonException($"Unexpected non-empty object serialization to '{typeof(WeakMaybe<T>).Name}'."),
                JsonTokenType.StartArray => reader.IsEmptyArray() ? default(T)! : throw new JsonException($"Unexpected non-empty array serialization to '{typeof(WeakMaybe<T>).Name}'."),
                _ => new WeakMaybe<T>(JsonSerializer.Deserialize<T>(ref reader, options)!)
            };
        }

        protected internal override Boolean Write(in Utf8JsonWriter writer, WeakMaybe<T> value, ref SerializerOptions options)
        {
            Maybe<T> maybe = value.Maybe;
            
            if (!maybe.HasValue)
            {
                writer.WriteNullValue();
                return true;
            }

            if (maybe.Internal is { } @object)
            {
                JsonSerializer.Serialize(writer, @object, options);
                return true;
            }

            writer.WriteObject();
            return true;
        }
    }
}