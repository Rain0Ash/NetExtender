using System;
using System.Reactive;
using NetExtender.Types.Exceptions;
using NetExtender.Types.Monads;
using NetExtender.Utilities.Serialization;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace NetExtender.Newtonsoft.Types.Monads.Results
{
    public sealed class BusinessResultJsonConverter : NewtonsoftJsonConverter<BusinessResult>
    {
        protected internal override BusinessResult Read(in JsonReader reader, Type type, Maybe<BusinessResult> exist, ref SerializerOptions options)
        {
            if (reader.TokenType is JsonToken.Null)
            {
                return default;
            }

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
                        return default;
                    }

                    try
                    {
                        return @object.ToObject<BusinessException>(options);
                    }
                    catch (Exception exception)
                    {
                        throw new JsonSerializationException($"Unexpected non-empty object serialization to '{nameof(BusinessResult)}'.", exception);
                    }
                }
                case JsonToken.StartArray when JArray.Load(reader) is { } array:
                {
                    return array.Count <= 0 ? default : throw new JsonSerializationException($"Unexpected non-empty array serialization to '{nameof(BusinessResult)}'.");
                }
                default:
                {
                    return options.Deserialize<BusinessException>(reader) ?? throw new JsonSerializationException($"Can't deserialize as '{nameof(BusinessException)}', nor '{nameof(Unit)}' for '{nameof(BusinessResult)}'.");
                }
            }
        }

        protected internal override Boolean Write(in JsonWriter writer, BusinessResult value, ref SerializerOptions options)
        {
            if (value.IsEmpty)
            {
                writer.WriteNull();
                return true;
            }

            if (value.Exception is { } exception)
            {
                options.Serialize(writer, exception.Business);
                return true;
            }

            writer.WriteNull();
            return true;
        }
    }

    public sealed class BusinessResultJsonConverter<T> : NewtonsoftJsonConverter<BusinessResult<T>>
    {
        protected internal override BusinessResult<T> Read(in JsonReader reader, Type type, Maybe<BusinessResult<T>> exist, ref SerializerOptions options)
        {
            if (reader.TokenType is JsonToken.Null)
            {
                try
                {
                    T? value = options.Deserialize<T>(reader);
                    return new BusinessResult<T>(value!);
                }
                catch (Exception)
                {
                    return default;
                }
            }

            try
            {
                if (options.Deserialize<BusinessException>(reader) is { } exception)
                {
                    return new BusinessResult<T>(exception);
                }
            }
            catch (Exception)
            {
            }

            try
            {
                T? value = options.Deserialize<T>(reader);
                return new BusinessResult<T>(value!);
            }
            catch (Exception)
            {
                throw new JsonSerializationException($"Can't deserialize as '{nameof(BusinessException)}', nor '{typeof(T).Name}' for '{typeof(BusinessResult<T>).Name}'.");
            }
        }

        protected internal override Boolean Write(in JsonWriter writer, BusinessResult<T> value, ref SerializerOptions options)
        {
            if (value.IsEmpty)
            {
                writer.WriteNull();
                return true;
            }

            if (value.Exception is { } exception)
            {
                options.Serialize(writer, exception.Business);
                return true;
            }

            options.Serialize(writer, (T) value);
            return true;
        }
    }

    public sealed class BusinessResultJsonConverter<T, TBusiness> : NewtonsoftJsonConverter<BusinessResult<T, TBusiness>>
    {
        protected internal override BusinessResult<T, TBusiness> Read(in JsonReader reader, Type type, Maybe<BusinessResult<T, TBusiness>> exist, ref SerializerOptions options)
        {
            if (reader.TokenType is JsonToken.Null)
            {
                try
                {
                    T? value = options.Deserialize<T>(reader);
                    return new BusinessResult<T, TBusiness>(value!);
                }
                catch (Exception)
                {
                    return default;
                }
            }

            try
            {
                if (options.Deserialize<BusinessException<TBusiness>>(reader) is { } exception)
                {
                    return new BusinessResult<T, TBusiness>(exception);
                }
            }
            catch (Exception)
            {
            }

            try
            {
                T? value = options.Deserialize<T>(reader);
                return new BusinessResult<T, TBusiness>(value!);
            }
            catch (Exception)
            {
                throw new JsonSerializationException($"Can't deserialize as '{typeof(BusinessException<TBusiness>).Name}', nor '{typeof(T).Name}' for '{typeof(BusinessResult<T, TBusiness>).Name}'.");
            }
        }

        protected internal override Boolean Write(in JsonWriter writer, BusinessResult<T, TBusiness> value, ref SerializerOptions options)
        {
            if (value.IsEmpty)
            {
                writer.WriteNull();
                return true;
            }

            if (value.Exception is { } exception)
            {
                options.Serialize(writer, exception.Business);
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

    public sealed class BusinessResultJsonConverter : TextJsonConverter<BusinessResult>
    {
        protected internal override BusinessResult Read(ref Utf8JsonReader reader, Type type, ref SerializerOptions options)
        {
            switch (reader.TokenType)
            {
                case JsonTokenType.Null:
                {
                    return default;
                }
                case JsonTokenType.StartObject:
                {
                    if (reader.IsEmptyObject())
                    {
                        return default;
                    }

                    try
                    {
                        return JsonSerializer.Deserialize<BusinessException>(ref reader, options);
                    }
                    catch (Exception exception)
                    {
                        throw new JsonException($"Unexpected non-empty object serialization to '{nameof(BusinessResult)}'.", exception);
                    }
                }
                case JsonTokenType.StartArray:
                {
                    return reader.IsEmptyArray() ? default : throw new JsonException($"Unexpected non-empty array serialization to '{nameof(BusinessResult)}'.");
                }
                default:
                {
                    return JsonSerializer.Deserialize<BusinessException>(ref reader, options) ?? throw new JsonException($"Can't deserialize as '{nameof(BusinessException)}', nor '{nameof(Unit)}' for '{nameof(BusinessResult)}'.");
                }
            }
        }

        protected internal override Boolean Write(in Utf8JsonWriter writer, BusinessResult value, ref SerializerOptions options)
        {
            if (value.IsEmpty)
            {
                writer.WriteNullValue();
                return true;
            }

            if (value.Exception is { } exception)
            {
                JsonSerializer.Serialize(writer, exception, options);
                return true;
            }

            JsonSerializer.Serialize(writer, default(Object), options);
            return true;
        }
    }

    public sealed class BusinessResultJsonConverter<T> : TextJsonConverter<BusinessResult<T>>
    {
        protected internal override BusinessResult<T> Read(ref Utf8JsonReader reader, Type type, ref SerializerOptions options)
        {
            if (reader.TokenType is JsonTokenType.Null)
            {
                try
                {
                    T? value = JsonSerializer.Deserialize<T>(ref reader, options);
                    return new BusinessResult<T>(value!);
                }
                catch (Exception)
                {
                    return default;
                }
            }

            Utf8JsonReader clone = reader;

            try
            {
                if (JsonSerializer.Deserialize<BusinessException>(ref reader, options) is { } exception)
                {
                    return new BusinessResult<T>(exception);
                }
            }
            catch (Exception)
            {
                reader = clone;
            }

            try
            {
                T? value = JsonSerializer.Deserialize<T>(ref reader, options);
                return new BusinessResult<T>(value!);
            }
            catch (Exception)
            {
                throw new JsonException($"Can't deserialize as '{nameof(BusinessException)}', nor '{typeof(T).Name}' for '{typeof(BusinessResult<T>).Name}'.");
            }
        }

        protected internal override Boolean Write(in Utf8JsonWriter writer, BusinessResult<T> value, ref SerializerOptions options)
        {
            if (value.IsEmpty)
            {
                writer.WriteNullValue();
                return true;
            }

            if (value.Exception is { } exception)
            {
                JsonSerializer.Serialize(writer, exception.Business, options);
                return true;
            }

            JsonSerializer.Serialize(writer, (T) value, options);
            return true;
        }
    }

    public sealed class BusinessResultJsonConverter<T, TBusiness> : TextJsonConverter<BusinessResult<T, TBusiness>>
    {
        protected internal override BusinessResult<T, TBusiness> Read(ref Utf8JsonReader reader, Type type, ref SerializerOptions options)
        {
            if (reader.TokenType is JsonTokenType.Null)
            {
                try
                {
                    T? value = JsonSerializer.Deserialize<T>(ref reader, options);
                    return new BusinessResult<T, TBusiness>(value!);
                }
                catch (Exception)
                {
                    return default;
                }
            }

            Utf8JsonReader clone = reader;

            try
            {
                if (JsonSerializer.Deserialize<BusinessException<TBusiness>>(ref reader, options) is { } exception)
                {
                    return new BusinessResult<T, TBusiness>(exception);
                }
            }
            catch (Exception)
            {
                reader = clone;
            }

            try
            {
                T? value = JsonSerializer.Deserialize<T>(ref reader, options);
                return new BusinessResult<T, TBusiness>(value!);
            }
            catch (Exception)
            {
                throw new JsonException($"Can't deserialize as '{typeof(BusinessException<TBusiness>).Name}', nor '{typeof(T).Name}' for '{typeof(BusinessResult<T, TBusiness>).Name}'.");
            }
        }

        protected internal override Boolean Write(in Utf8JsonWriter writer, BusinessResult<T, TBusiness> value, ref SerializerOptions options)
        {
            if (value.IsEmpty)
            {
                writer.WriteNullValue();
                return true;
            }

            if (value.Exception is { } exception)
            {
                JsonSerializer.Serialize(writer, exception, options);
                return true;
            }

            JsonSerializer.Serialize(writer, (T) value, options);
            return true;
        }
    }
}