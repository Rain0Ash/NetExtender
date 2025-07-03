using System;
using NetExtender.Types.Monads;
using NetExtender.Utilities.Serialization;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace NetExtender.Newtonsoft.Types.Monads
{
    public sealed class NotifyMutableValueDefaultJsonConverter<T> : DefaultJsonConverter<T, NotifyMutableValueDefault<T>>
    {
        protected override NotifyMutableValueDefault<T> Create(T @default, Maybe<T> value)
        {
            return value ? new NotifyMutableValueDefault<T>(@default, value.Internal) : new NotifyMutableValueDefault<T>(@default);
        }

        protected override T Default(NotifyMutableValueDefault<T> @default)
        {
            return @default.Default;
        }
    }

    public sealed class MutableValueDefaultJsonConverter<T> : DefaultJsonConverter<T, MutableValueDefault<T>>
    {
        protected override MutableValueDefault<T> Create(T @default, Maybe<T> value)
        {
            return value ? new MutableValueDefault<T>(@default, value.Internal) : new MutableValueDefault<T>(@default);
        }

        protected override T Default(MutableValueDefault<T> @default)
        {
            return @default.Default;
        }
    }

    public sealed class NotifyMutableDynamicDefaultJsonConverter<T> : DefaultJsonConverter<T, NotifyMutableDynamicDefault<T>>
    {
        protected override NotifyMutableDynamicDefault<T> Create(T @default, Maybe<T> value)
        {
            return value ? new NotifyMutableDynamicDefault<T>(() => @default, value.Internal) : new NotifyMutableDynamicDefault<T>(() => @default);
        }

        protected override T Default(NotifyMutableDynamicDefault<T> @default)
        {
            return @default.Default();
        }
    }

    public sealed class MutableDynamicDefaultJsonConverter<T> : DefaultJsonConverter<T, MutableDynamicDefault<T>>
    {
        protected override MutableDynamicDefault<T> Create(T @default, Maybe<T> value)
        {
            return value ? new MutableDynamicDefault<T>(() => @default, value.Internal) : new MutableDynamicDefault<T>(() => @default);
        }

        protected override T Default(MutableDynamicDefault<T> @default)
        {
            return @default.Default();
        }
    }

    public sealed class MutableDefaultJsonConverter<T> : DefaultJsonConverter<T, MutableDefault<T>>
    {
        protected override MutableDefault<T> Create(T @default, Maybe<T> value)
        {
            return value ? new MutableValueDefault<T>(@default, value.Internal) : new MutableValueDefault<T>(@default);
        }

        protected override T Default(MutableDefault<T> @default)
        {
            return @default.Default;
        }
    }

    public abstract class DefaultJsonConverter<T, TDefault> : NewtonsoftJsonConverter<TDefault> where TDefault : MutableDefault<T>
    {
        protected TDefault Create(T @default)
        {
            return Create(@default, default);
        }
        
        protected abstract TDefault Create(T @default, Maybe<T> value);
        protected abstract T Default(TDefault @default);

        protected internal override TDefault? Read(in JsonReader reader, Type type, Maybe<TDefault> exist, ref SerializerOptions options)
        {
            switch (reader.TokenType)
            {
                case JsonToken.Null:
                {
                    return null;
                }
                case JsonToken.StartObject when JObject.Load(reader) is { } @object:
                {
                    if (!@object.HasValues)
                    {
                        return Create(default!);
                    }
                    
                    JToken jdefault = @object.GetValue(nameof(MutableDefault<T>.Default), options) ?? throw new JsonSerializationException($"Missing required property '{nameof(MutableDefault<T>.Default)}'.");
                    T? @default = jdefault.ToObject<T>(options);

                    JToken? jvalue = @object.GetValue(nameof(MutableDefault<T>.Value), options);
                    return jvalue is not null ? Create(@default!, jvalue.ToObject<T>(options)!) : Create(@default!);
                }
                case JsonToken.StartArray when JArray.Load(reader) is { } array:
                {
                    return array.Count <= 0 ? Create(default!) : throw new JsonSerializationException($"Unexpected non-empty array serialization to '{typeof(TDefault).Name}'.");
                }
                default:
                {
                    return Create(options.Deserialize<T>(reader)!);
                }
            }
        }

        protected internal override Boolean Write(in JsonWriter writer, TDefault? value, ref SerializerOptions options)
        {
            if (value is null)
            {
                writer.WriteNull();
                return true;
            }

            T @default = Default(value);
            if (value.IsDefault)
            {
                if (@default is not null)
                {
                    options.Serialize(writer, @default);
                    return true;
                }

                writer.WriteStartObject();
                writer.WriteEndObject();
                return true;
            }

            writer.WriteStartObject();
            
            writer.WritePropertyName(nameof(MutableDefault<T>.Default), options);
            options.Serialize(writer, @default);

            writer.WritePropertyName(nameof(MutableDefault<T>.Value), options);
            options.Serialize(writer, value.Value);

            writer.WriteEndObject();
            return true;
        }
    }
}

namespace NetExtender.Serialization.Json.Monads
{
    using System.Text.Json;
    
    public sealed class NotifyMutableValueDefaultJsonConverter<T> : DefaultJsonConverter<T, NotifyMutableValueDefault<T>>
    {
        protected override NotifyMutableValueDefault<T> Create(T @default, Maybe<T> value)
        {
            return value ? new NotifyMutableValueDefault<T>(@default, value.Internal) : new NotifyMutableValueDefault<T>(@default);
        }

        protected override T Default(NotifyMutableValueDefault<T> @default)
        {
            return @default.Default;
        }
    }

    public sealed class MutableValueDefaultJsonConverter<T> : DefaultJsonConverter<T, MutableValueDefault<T>>
    {
        protected override MutableValueDefault<T> Create(T @default, Maybe<T> value)
        {
            return value ? new MutableValueDefault<T>(@default, value.Internal) : new MutableValueDefault<T>(@default);
        }

        protected override T Default(MutableValueDefault<T> @default)
        {
            return @default.Default;
        }
    }

    public sealed class NotifyMutableDynamicDefaultJsonConverter<T> : DefaultJsonConverter<T, NotifyMutableDynamicDefault<T>>
    {
        protected override NotifyMutableDynamicDefault<T> Create(T @default, Maybe<T> value)
        {
            return value ? new NotifyMutableDynamicDefault<T>(() => @default, value.Internal) : new NotifyMutableDynamicDefault<T>(() => @default);
        }

        protected override T Default(NotifyMutableDynamicDefault<T> @default)
        {
            return @default.Default();
        }
    }

    public sealed class MutableDynamicDefaultJsonConverter<T> : DefaultJsonConverter<T, MutableDynamicDefault<T>>
    {
        protected override MutableDynamicDefault<T> Create(T @default, Maybe<T> value)
        {
            return value ? new MutableDynamicDefault<T>(() => @default, value.Internal) : new MutableDynamicDefault<T>(() => @default);
        }

        protected override T Default(MutableDynamicDefault<T> @default)
        {
            return @default.Default();
        }
    }

    public sealed class MutableDefaultJsonConverter<T> : DefaultJsonConverter<T, MutableDefault<T>>
    {
        protected override MutableDefault<T> Create(T @default, Maybe<T> value)
        {
            return value ? new MutableValueDefault<T>(@default, value.Internal) : new MutableValueDefault<T>(@default);
        }

        protected override T Default(MutableDefault<T> @default)
        {
            return @default.Default;
        }
    }

    public abstract class DefaultJsonConverter<T, TDefault> : TextJsonConverter<TDefault> where TDefault : MutableDefault<T>
    {
        protected TDefault Create(T @default)
        {
            return Create(@default, default);
        }
        
        protected abstract TDefault Create(T @default, Maybe<T> value);
        protected abstract T Default(TDefault @default);

        protected internal override TDefault? Read(ref Utf8JsonReader reader, Type type, ref SerializerOptions options)
        {
            if (reader.TokenType is JsonTokenType.Null)
            {
                return null;
            }

            using JsonDocument document = JsonDocument.ParseValue(ref reader);
            JsonElement root = document.RootElement;
            
            switch (root.ValueKind)
            {
                case JsonValueKind.Object:
                {
                    if (root.IsEmpty())
                    {
                        return Create(default!);
                    }
                    
                    if (!root.TryGetProperty(nameof(MutableDefault<T>.Default), options, out JsonElement jdefault))
                    {
                        throw new JsonException($"Missing required property '{nameof(MutableDefault<T>.Default)}'.");
                    }

                    T? @default = jdefault.Deserialize<T>(options);

                    if (!root.TryGetProperty(nameof(MutableDefault<T>.Value), options, out JsonElement jvalue))
                    {
                        return Create(@default!);
                    }

                    T? value = jvalue.Deserialize<T>(options);
                    return Create(@default!, value!);
                }
                case JsonValueKind.Array:
                {
                    return root.IsEmpty() ? Create(default!) : throw new JsonException($"Unexpected non-empty array serialization to '{typeof(TDefault).Name}'.");
                }
                default:
                {
                    return Create(root.Deserialize<T>(options)!);
                }
            }
        }

        protected internal override Boolean Write(in Utf8JsonWriter writer, TDefault? value, ref SerializerOptions options)
        {
            if (value is null)
            {
                writer.WriteNullValue();
                return true;
            }

            T @default = Default(value);
            if (value.IsDefault)
            {
                if (@default is not null)
                {
                    JsonSerializer.Serialize(writer, @default, options);
                    return true;
                }

                writer.WriteStartObject();
                writer.WriteEndObject();
                return true;
            }
            
            writer.WriteStartObject();
            
            writer.WritePropertyName(nameof(MutableDefault<T>.Default), options);
            JsonSerializer.Serialize(writer, @default, options);

            writer.WritePropertyName(nameof(MutableDefault<T>.Value), options);
            JsonSerializer.Serialize(writer, value.Value, options);
            
            writer.WriteEndObject();
            return true;
        }
    }
}