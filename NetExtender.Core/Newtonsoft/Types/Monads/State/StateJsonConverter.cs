using System;
using NetExtender.Types.Monads;
using NetExtender.Utilities.Serialization;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace NetExtender.Newtonsoft.Types.Monads
{
    public sealed class NotifyStateJsonConverter<T> : NewtonsoftJsonConverter<NotifyState<T>>
    {
        protected internal override NotifyState<T>? Read(in JsonReader reader, Type type, Maybe<NotifyState<T>> exist, ref SerializerOptions options)
        {
            if (reader.TokenType is JsonToken.Null)
            {
                return null;
            }

            State<T> state = options.Deserialize<State<T>>(reader);
            return new NotifyState<T>(state);
        }

        protected internal override Boolean Write(in JsonWriter writer, NotifyState<T>? value, ref SerializerOptions options)
        {
            if (value is null)
            {
                writer.WriteNull();
                return true;
            }

            options.Serialize(writer, value.Internal);
            return true;
        }
    }
    
    public sealed class MutableStateJsonConverter<T> : NewtonsoftJsonConverter<MutableState<T>>
    {
        protected internal override MutableState<T>? Read(in JsonReader reader, Type type, Maybe<MutableState<T>> exist, ref SerializerOptions options)
        {
            if (reader.TokenType is JsonToken.Null)
            {
                return null;
            }

            State<T> state = options.Deserialize<State<T>>(reader);
            return new MutableState<T>(state);
        }

        protected internal override Boolean Write(in JsonWriter writer, MutableState<T>? value, ref SerializerOptions options)
        {
            if (value is null)
            {
                writer.WriteNull();
                return true;
            }

            options.Serialize(writer, value.Internal);
            return true;
        }
    }

    public sealed class StateJsonConverter<T> : NewtonsoftJsonConverter<State<T>>
    {
        protected internal override State<T> Read(in JsonReader reader, Type type, Maybe<State<T>> exist, ref SerializerOptions options)
        {
            switch (reader.TokenType)
            {
                case JsonToken.Null:
                {
                    return default;
                }
                case JsonToken.StartObject when JObject.Load(reader) is { } @object:
                {
                    if (!@object.HasValues)
                    {
                        return default(T)!;
                    }
                    
                    JToken jvalue = @object.GetValue(nameof(State<T>.Value), options) ?? throw new JsonSerializationException($"Missing required property '{nameof(State<T>.Value)}'.");
                    T? value = jvalue.ToObject<T>(options);

                    JToken? jnext = @object.GetValue(nameof(State<T>.Next), options);
                    return jnext is not null ? new State<T>(value!, jnext.ToObject<T>(options)!) : new State<T>(value!);
                }
                case JsonToken.StartArray when JArray.Load(reader) is { } array:
                {
                    return array.Count <= 0 ? default(T)! : throw new JsonSerializationException($"Unexpected non-empty array serialization to '{typeof(State<T>).Name}'.");
                }
                default:
                {
                    return new State<T>(options.Deserialize<T>(reader)!);
                }
            }
        }

        protected internal override Boolean Write(in JsonWriter writer, State<T> value, ref SerializerOptions options)
        {
            if (!value.HasNext)
            {
                if (value.Value is null)
                {
                    writer.WriteObject();
                    return true;
                }

                options.Serialize(writer, value.Value);
                return true;
            }

            writer.WriteStartObject();

            writer.WritePropertyName(nameof(State<T>.Value), options);
            options.Serialize(writer, value.Value);

            writer.WritePropertyName(nameof(State<T>.Next), options);
            options.Serialize(writer, value.Next.Value);

            writer.WriteEndObject();
            return true;
        }
    }
}

namespace NetExtender.Serialization.Json.Monads
{
    using System.Text.Json;

    public sealed class NotifyStateJsonConverter<T> : TextJsonConverter<NotifyState<T>>
    {
        protected internal override NotifyState<T>? Read(ref Utf8JsonReader reader, Type type, ref SerializerOptions options)
        {
            if (reader.TokenType is JsonTokenType.Null)
            {
                return null;
            }

            State<T> state = JsonSerializer.Deserialize<State<T>>(ref reader, options);
            return new NotifyState<T>(state);
        }

        protected internal override Boolean Write(in Utf8JsonWriter writer, NotifyState<T>? value, ref SerializerOptions options)
        {
            if (value is null)
            {
                writer.WriteNullValue();
                return true;
            }

            JsonSerializer.Serialize(writer, value.Internal, options);
            return true;
        }
    }

    public sealed class MutableStateJsonConverter<T> : TextJsonConverter<MutableState<T>>
    {
        protected internal override MutableState<T>? Read(ref Utf8JsonReader reader, Type type, ref SerializerOptions options)
        {
            if (reader.TokenType is JsonTokenType.Null)
            {
                return null;
            }

            State<T> state = JsonSerializer.Deserialize<State<T>>(ref reader, options);
            return new MutableState<T>(state);
        }

        protected internal override Boolean Write(in Utf8JsonWriter writer, MutableState<T>? value, ref SerializerOptions options)
        {
            if (value is null)
            {
                writer.WriteNullValue();
                return true;
            }

            JsonSerializer.Serialize(writer, value.Internal, options);
            return true;
        }
    }
    
    public sealed class StateJsonConverter<T> : TextJsonConverter<State<T>>
    {
        protected internal override State<T> Read(ref Utf8JsonReader reader, Type type, ref SerializerOptions options)
        {
            if (reader.TokenType is JsonTokenType.Null)
            {
                return default;
            }

            using JsonDocument document = JsonDocument.ParseValue(ref reader);
            JsonElement root = document.RootElement;

            switch (root.ValueKind)
            {
                case JsonValueKind.Object:
                {
                    if (root.IsEmpty())
                    {
                        return default(T)!;
                    }
            
                    if (!root.TryGetProperty(nameof(State<T>.Value), options, out JsonElement jvalue))
                    {
                        throw new JsonException($"Missing required property '{nameof(State<T>.Value)}'.");
                    }

                    T? value = jvalue.Deserialize<T>(options);
                    if (!root.TryGetProperty(nameof(State<T>.Next), options, out JsonElement jnext))
                    {
                        return new State<T>(value!);
                    }

                    T? next = jnext.Deserialize<T>(options);
                    return new State<T>(value!, next!);
                }
                case JsonValueKind.Array:
                {
                    return root.IsEmpty() ? default(T)! : throw new JsonException($"Unexpected non-empty array serialization to '{typeof(State<T>).Name}'.");
                }
                default:
                {
                    return new State<T>(root.Deserialize<T>(options)!);
                }
            }
        }

        protected internal override Boolean Write(in Utf8JsonWriter writer, State<T> value, ref SerializerOptions options)
        {
            if (!value.HasNext)
            {
                if (value.Value is null)
                {
                    writer.WriteObject();
                    return true;
                }
                
                JsonSerializer.Serialize(writer, value.Value, options);
                return true;
            }

            writer.WriteStartObject();

            writer.WritePropertyName(nameof(State<T>.Value), options);
            JsonSerializer.Serialize(writer, value.Value, options);

            writer.WritePropertyName(nameof(State<T>.Next), options);
            JsonSerializer.Serialize(writer, value.Next.Value, options);

            writer.WriteEndObject();
            return true;
        }
    }
}