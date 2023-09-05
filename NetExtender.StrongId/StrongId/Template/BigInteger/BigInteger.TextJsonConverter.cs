
        public class STRONGIDTextJsonConverter : System.Text.Json.Serialization.JsonConverter<STRONGID>
        {
            public override STRONGID Read(ref System.Text.Json.Utf8JsonReader reader, System.Type type, System.Text.Json.JsonSerializerOptions options)
            {
                if (reader.TokenType != System.Text.Json.JsonTokenType.Number)
                {
                    throw new System.Text.Json.JsonException($"Found token {reader.TokenType} but expected token {System.Text.Json.JsonTokenType.Number}");
                }

                using System.Text.Json.JsonDocument document = System.Text.Json.JsonDocument.ParseValue(ref reader);
                System.Numerics.BigInteger result = System.Numerics.BigInteger.Parse(document.RootElement.GetRawText(), System.Globalization.NumberFormatInfo.InvariantInfo);
                return new STRONGID(result);
            }

            public override void Write(System.Text.Json.Utf8JsonWriter writer, STRONGID value, System.Text.Json.JsonSerializerOptions options)
            {
                writer.WriteRawValue(value.ToString(System.Globalization.NumberFormatInfo.InvariantInfo));
            }
        }