
        public class STRONGIDTextJsonConverter : System.Text.Json.Serialization.JsonConverter<STRONGID>
        {
            public override STRONGID Read(ref System.Text.Json.Utf8JsonReader reader, System.Type type, System.Text.Json.JsonSerializerOptions options)
            {
                return new STRONGID(reader.GetTYPENAME());
            }

            public override void Write(System.Text.Json.Utf8JsonWriter writer, STRONGID value, System.Text.Json.JsonSerializerOptions options)
            {
                writer.WriteNumberValue(value.Value);
            }
        }