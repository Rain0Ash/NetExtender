using System;
using NetExtender.Types.Monads;
using NetExtender.Utilities.Serialization;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace NetExtender.Newtonsoft.Types.Monads
{
    public sealed class NotifyDebounceJsonConverter<T> : NewtonsoftJsonConverter<NotifyDebounce<T>>
    {
        protected internal override NotifyDebounce<T>? Read(in JsonReader reader, Type type, Maybe<NotifyDebounce<T>> exist, ref SerializerOptions options)
        {
            if (reader.TokenType is JsonToken.Null)
            {
                return null;
            }

            Debounce<T> debounce = options.Deserialize<Debounce<T>>(reader);
            return new NotifyDebounce<T>(debounce);
        }

        protected internal override Boolean Write(in JsonWriter writer, NotifyDebounce<T>? value, ref SerializerOptions options)
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
    
    public sealed class MutableDebounceJsonConverter<T> : NewtonsoftJsonConverter<MutableDebounce<T>>
    {
        protected internal override MutableDebounce<T>? Read(in JsonReader reader, Type type, Maybe<MutableDebounce<T>> exist, ref SerializerOptions options)
        {
            if (reader.TokenType is JsonToken.Null)
            {
                return null;
            }

            Debounce<T> debounce = options.Deserialize<Debounce<T>>(reader);
            return new MutableDebounce<T>(debounce);
        }

        protected internal override Boolean Write(in JsonWriter writer, MutableDebounce<T>? value, ref SerializerOptions options)
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

    public sealed class DebounceJsonConverter<T> : NewtonsoftJsonConverter<Debounce<T>>
    {
        protected internal override Debounce<T> Read(in JsonReader reader, Type type, Maybe<Debounce<T>> exist, ref SerializerOptions options)
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
                    
                    JToken jvalue = @object.GetValue(nameof(Debounce<T>.Value), options) ?? throw new JsonSerializationException($"Missing required property '{nameof(Debounce<T>.Value)}'.");
                    T? value = jvalue.ToObject<T>(options);

                    DateTime settime = default;
                    if (@object.GetValue(nameof(Debounce<T>.SetTime), options) is { } jsettime)
                    {
                        settime = jsettime.ToObject<DateTime>(options);
                    }

                    TimeSpan delay = TimeSpan.Zero;
                    if (@object.GetValue(nameof(Debounce<T>.Delay), options) is { } jdelay)
                    {
                        delay = jdelay.ToObject<TimeSpan>(options);
                    }
                    
                    DateTimeKind kind = Debounce<T>.DefaultDateTimeKind;
                    if (@object.GetValue(nameof(Debounce<T>.TimeKind), options) is { } jkind)
                    {
                        kind = jkind.ToObject<DateTimeKind>(options);
                    }

                    return new Debounce<T>(value!, delay, kind) { SetTime = settime };
                }
                case JsonToken.StartArray when JArray.Load(reader) is { } array:
                {
                    return array.Count <= 0 ? default(T)! : throw new JsonSerializationException($"Unexpected non-empty array serialization to '{typeof(Debounce<T>).Name}'.");
                }
                default:
                {
                    return new Debounce<T>(options.Deserialize<T>(reader)!);
                }
            }
        }

        protected internal override Boolean Write(in JsonWriter writer, Debounce<T> value, ref SerializerOptions options)
        {
            if (value.IsEmpty)
            {
                writer.WriteNull();
            }

            if (value.Delay == TimeSpan.Zero && value.SetTime == default && value.TimeKind is Debounce<T>.DefaultDateTimeKind)
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

            writer.WritePropertyName(nameof(Debounce<T>.Value), options);
            options.Serialize(writer, value.Value);

            if (value.SetTime != default)
            {
                writer.WritePropertyName(nameof(Debounce<T>.SetTime), options);
                options.Serialize(writer, value.SetTime);
            }

            if (value.Delay != TimeSpan.Zero)
            {
                writer.WritePropertyName(nameof(Debounce<T>.Delay), options);
                options.Serialize(writer, value.Delay);
            }

            if (value.TimeKind is not Debounce<T>.DefaultDateTimeKind)
            {
                writer.WritePropertyName(nameof(Debounce<T>.TimeKind), options);
                options.Serialize(writer, value.TimeKind);
            }

            writer.WriteEndObject();
            return true;
        }
    }
}

namespace NetExtender.Serialization.Json.Monads
{
    using System.Text.Json;

    public sealed class NotifyDebounceJsonConverter<T> : TextJsonConverter<NotifyDebounce<T>>
    {
        protected internal override NotifyDebounce<T>? Read(ref Utf8JsonReader reader, Type type, ref SerializerOptions options)
        {
            if (reader.TokenType is JsonTokenType.Null)
            {
                return null;
            }

            Debounce<T> debounce = JsonSerializer.Deserialize<Debounce<T>>(ref reader, options);
            return new NotifyDebounce<T>(debounce);
        }

        protected internal override Boolean Write(in Utf8JsonWriter writer, NotifyDebounce<T>? value, ref SerializerOptions options)
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

    public sealed class MutableDebounceJsonConverter<T> : TextJsonConverter<MutableDebounce<T>>
    {
        protected internal override MutableDebounce<T>? Read(ref Utf8JsonReader reader, Type type, ref SerializerOptions options)
        {
            if (reader.TokenType is JsonTokenType.Null)
            {
                return null;
            }

            Debounce<T> debounce = JsonSerializer.Deserialize<Debounce<T>>(ref reader, options);
            return new MutableDebounce<T>(debounce);
        }

        protected internal override Boolean Write(in Utf8JsonWriter writer, MutableDebounce<T>? value, ref SerializerOptions options)
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
    
    public sealed class DebounceJsonConverter<T> : TextJsonConverter<Debounce<T>>
    {
        // ReSharper disable once CognitiveComplexity
        protected internal override Debounce<T> Read(ref Utf8JsonReader reader, Type type, ref SerializerOptions options)
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
                    
                    if (!root.TryGetProperty(nameof(Debounce<T>.Value), options, out JsonElement jvalue))
                    {
                        throw new JsonException($"Missing required property '{nameof(Debounce<T>.Value)}'.");
                    }

                    T? value = jvalue.Deserialize<T>(options);

                    DateTime settime = default;
                    if (root.TryGetProperty(nameof(Debounce<T>.SetTime), options, out JsonElement jsettime))
                    {
                        settime = jsettime.GetDateTime();
                    }

                    TimeSpan delay = TimeSpan.Zero;
                    if (root.TryGetProperty(nameof(Debounce<T>.Delay), options, out JsonElement jdelay))
                    {
                        delay = jdelay.Deserialize<TimeSpan>();
                    }
                    
                    DateTimeKind kind = Debounce<T>.DefaultDateTimeKind;
                    if (root.TryGetProperty(nameof(Debounce<T>.TimeKind), options, out JsonElement jkind))
                    {
                        kind = jkind.Deserialize<DateTimeKind>();
                    }

                    return new Debounce<T>(value!, delay, kind) { SetTime = settime };
                }
                case JsonValueKind.Array:
                {
                    return root.IsEmpty() ? default(T)! : throw new JsonException($"Unexpected non-empty array serialization to '{typeof(Debounce<T>).Name}'.");
                }
                default:
                {
                    return new Debounce<T>(root.Deserialize<T>(options)!);
                }
            }
        }

        protected internal override Boolean Write(in Utf8JsonWriter writer, Debounce<T> value, ref SerializerOptions options)
        {
            if (value.IsEmpty)
            {
                writer.WriteNullValue();
            }

            if (value.Delay == TimeSpan.Zero && value.SetTime == default && value.TimeKind is Debounce<T>.DefaultDateTimeKind)
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

            writer.WritePropertyName(nameof(Debounce<T>.Value), options);
            JsonSerializer.Serialize(writer, value.Value, options);

            if (value.SetTime != default)
            {
                writer.WritePropertyName(nameof(Debounce<T>.SetTime), options);
                JsonSerializer.Serialize(writer, value.SetTime, options);
            }

            if (value.Delay != TimeSpan.Zero)
            {
                writer.WritePropertyName(nameof(Debounce<T>.Delay), options);
                JsonSerializer.Serialize(writer, value.Delay, options);
            }

            if (value.TimeKind is not Debounce<T>.DefaultDateTimeKind)
            {
                writer.WritePropertyName(nameof(Debounce<T>.TimeKind), options);
                JsonSerializer.Serialize(writer, value.TimeKind, options);
            }
            
            writer.WriteEndObject();
            return true;
        }
    }
}