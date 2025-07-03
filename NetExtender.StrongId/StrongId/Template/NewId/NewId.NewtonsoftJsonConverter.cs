
        public class STRONGIDNewtonsoftJsonConverter : Newtonsoft.Json.JsonConverter
        {
            public override System.Boolean CanConvert(System.Type type)
            {
                return type == typeof(STRONGID);
            }

            public override System.Object? ReadJson(Newtonsoft.Json.JsonReader reader, System.Type type, System.Object? existingValue, Newtonsoft.Json.JsonSerializer serializer)
            {
                System.Guid? result = serializer.Deserialize<System.Guid?>(reader);
                return result.HasValue ? new STRONGID(result.Value) : null;
            }

            public override void WriteJson(Newtonsoft.Json.JsonWriter writer, System.Object? value, Newtonsoft.Json.JsonSerializer serializer)
            {
                STRONGID? id = (STRONGID?) value;
                serializer.Serialize(writer, id?.Value);
            }
        }