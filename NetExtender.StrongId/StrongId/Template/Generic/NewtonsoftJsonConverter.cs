
        public class STRONGIDNewtonsoftJsonConverter : Newtonsoft.Json.JsonConverter
        {
            public override System.Boolean CanConvert(System.Type objectType)
            {
                return objectType == typeof(STRONGID);
            }

            public override void WriteJson(Newtonsoft.Json.JsonWriter writer, System.Object? value, Newtonsoft.Json.JsonSerializer serializer)
            {
                STRONGID? id = (STRONGID?) value;
                serializer.Serialize(writer, id?.Value);
            }

            public override System.Object? ReadJson(Newtonsoft.Json.JsonReader reader, System.Type objectType, System.Object? existingValue, Newtonsoft.Json.JsonSerializer serializer)
            {
                UNDERLYING? result = serializer.Deserialize<UNDERLYING?>(reader);
                return result.HasValue ? new STRONGID(result.Value) : null;
            }
        }