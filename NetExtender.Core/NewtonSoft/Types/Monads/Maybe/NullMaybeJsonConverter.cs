using System;
using NetExtender.Types.Monads;
using Newtonsoft.Json;

namespace NetExtender.NewtonSoft.Types.Monads
{
    public class NullMaybeJsonConverter<T> : JsonConverter<NullMaybe<T>>
    {
        public override NullMaybe<T> ReadJson(JsonReader reader, Type objectType, NullMaybe<T> existingValue, Boolean hasExistingValue, JsonSerializer serializer)
        {
            if (reader.TokenType == JsonToken.Null)
            {
                return default;
            }

            T? value = serializer.Deserialize<T>(reader);
            return new NullMaybe<T>(value!);
        }

        public override void WriteJson(JsonWriter writer, NullMaybe<T> value, JsonSerializer serializer)
        {
            if (value.IsEmpty)
            {
                writer.WriteNull();
                return;
            }

            serializer.Serialize(writer, value.Value);
        }
    }
}