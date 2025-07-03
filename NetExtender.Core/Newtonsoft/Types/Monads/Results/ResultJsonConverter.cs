using System;
using NetExtender.Types.Monads;
using NetExtender.Types.Monads.Result;
using Newtonsoft.Json;

namespace NetExtender.Newtonsoft.Types.Monads.Results
{
    public sealed class ResultJsonConverter<T> : NewtonsoftJsonConverter<Result<T>>
    {
        protected internal override Result<T> Read(in JsonReader reader, Type type, Maybe<Result<T>> exist, ref SerializerOptions options)
        {
            if (reader.TokenType is JsonToken.Null)
            {
                try
                {
                    T? value = options.Deserialize<T>(reader);
                    return new Result<T>(value!);
                }
                catch (Exception)
                {
                    return default;
                }
            }

            try
            {
                if (options.Deserialize<Exception>(reader) is { } exception)
                {
                    return new Result<T>(exception);
                }
            }
            catch (Exception)
            {
            }

            try
            {
                T? value = options.Deserialize<T>(reader);
                return new Result<T>(value!);
            }
            catch (Exception)
            {
                throw new JsonSerializationException($"Can't deserialize as '{nameof(Exception)}', nor '{typeof(T).Name}' for '{typeof(Result<T>).Name}'.");
            }
        }

        protected internal override Boolean Write(in JsonWriter writer, Result<T> value, ref SerializerOptions options)
        {
            if (value.Exception is { } exception)
            {
                options.Serialize(writer, exception);
                return true;
            }

            options.Serialize(writer, (T) value);
            return true;
        }
    }
    
    public sealed class ResultJsonConverter<T, TException> : NewtonsoftJsonConverter<Result<T, TException>> where TException : Exception
    {
        protected internal override Result<T, TException> Read(in JsonReader reader, Type type, Maybe<Result<T, TException>> exist, ref SerializerOptions options)
        {
            if (reader.TokenType is JsonToken.Null)
            {
                try
                {
                    T? value = options.Deserialize<T>(reader);
                    return new Result<T, TException>(value!);
                }
                catch (Exception)
                {
                    return default;
                }
            }

            try
            {
                if (options.Deserialize<TException>(reader) is { } exception)
                {
                    return new Result<T, TException>(exception);
                }
            }
            catch (Exception)
            {
            }

            try
            {
                T? value = options.Deserialize<T>(reader);
                return new Result<T, TException>(value!);
            }
            catch (Exception)
            {
                throw new JsonSerializationException($"Can't deserialize as '{typeof(TException).Name}', nor '{typeof(T).Name}' for '{typeof(Result<T, TException>).Name}'.");
            }
        }

        protected internal override Boolean Write(in JsonWriter writer, Result<T, TException> value, ref SerializerOptions options)
        {
            if (value.Exception is { } exception)
            {
                options.Serialize(writer, exception);
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
    
    public sealed class ResultJsonConverter<T> : TextJsonConverter<Result<T>>
    {
        protected internal override Result<T> Read(ref Utf8JsonReader reader, Type type, ref SerializerOptions options)
        {
            if (reader.TokenType is JsonTokenType.Null)
            {
                try
                {
                    T? value = JsonSerializer.Deserialize<T>(ref reader, options);
                    return new Result<T>(value!);
                }
                catch (Exception)
                {
                    return default;
                }
            }

            Utf8JsonReader clone = reader;

            try
            {
                if (JsonSerializer.Deserialize<Exception>(ref reader, options) is { } exception)
                {
                    return new Result<T>(exception);
                }
            }
            catch (Exception)
            {
                reader = clone;
            }

            try
            {
                T? value = JsonSerializer.Deserialize<T>(ref reader, options);
                return new Result<T>(value!);
            }
            catch (Exception)
            {
                throw new JsonException($"Can't deserialize as '{nameof(Exception)}', nor '{typeof(T).Name}' for '{typeof(Result<T>).Name}'.");
            }
        }

        protected internal override Boolean Write(in Utf8JsonWriter writer, Result<T> value, ref SerializerOptions options)
        {
            if (value.Exception is { } exception)
            {
                JsonSerializer.Serialize(writer, exception, options);
                return true;
            }

            JsonSerializer.Serialize(writer, (T) value, options);
            return true;
        }
    }

    public sealed class ResultJsonConverter<T, TException> : TextJsonConverter<Result<T, TException>> where TException : Exception
    {
        protected internal override Result<T, TException> Read(ref Utf8JsonReader reader, Type type, ref SerializerOptions options)
        {
            if (reader.TokenType is JsonTokenType.Null)
            {
                try
                {
                    T? value = JsonSerializer.Deserialize<T>(ref reader, options);
                    return new Result<T, TException>(value!);
                }
                catch (Exception)
                {
                    return default;
                }
            }

            Utf8JsonReader clone = reader;

            try
            {
                if (JsonSerializer.Deserialize<TException>(ref reader, options) is { } exception)
                {
                    return new Result<T, TException>(exception);
                }
            }
            catch (Exception)
            {
                reader = clone;
            }

            try
            {
                T? value = JsonSerializer.Deserialize<T>(ref reader, options);
                return new Result<T, TException>(value!);
            }
            catch (Exception)
            {
                throw new JsonException($"Can't deserialize as '{typeof(TException).Name}', nor '{typeof(T).Name}' for '{typeof(Result<T, TException>).Name}'.");
            }
        }

        protected internal override Boolean Write(in Utf8JsonWriter writer, Result<T, TException> value, ref SerializerOptions options)
        {
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