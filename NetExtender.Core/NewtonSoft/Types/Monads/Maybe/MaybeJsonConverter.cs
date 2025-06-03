using System;
using NetExtender.Types.Monads;
using Newtonsoft.Json;

namespace NetExtender.NewtonSoft.Types.Monads
{
    public class MaybeJsonConverter<T> : JsonConverter<Maybe<T>>
    {
        public override Maybe<T> ReadJson(JsonReader reader, Type objectType, Maybe<T> existingValue, Boolean hasExistingValue, JsonSerializer serializer)
        {
            if (reader.TokenType == JsonToken.Null)
            {
                return default;
            }

            T? value = serializer.Deserialize<T>(reader);
            return new Maybe<T>(value!);
        }

        public override void WriteJson(JsonWriter writer, Maybe<T> value, JsonSerializer serializer)
        {
            if (!value)
            {
                writer.WriteNull();
                return;
            }

            serializer.Serialize(writer, value.Value);
        }
    }
}