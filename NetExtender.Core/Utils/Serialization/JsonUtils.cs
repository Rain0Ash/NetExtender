// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Xml;
using NetExtender.NewtonSoft;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Serialization;

namespace NetExtender.Utils.Serialization
{
    public static class JsonUtils
    {
        public static Boolean IsValidJson(String? json)
        {
            if (String.IsNullOrEmpty(json))
            {
                return false;
            }

            json = json.Trim();

            if (json.Length <= 0 || (!json.StartsWith("{") || !json.EndsWith("}")) && (!json.StartsWith("[") || !json.EndsWith("]")))
            {
                return false;
            }

            try
            {
                _ = JToken.Parse(json);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public static String JsonSerializeObject(this Object? value)
        {
            return JsonConvert.SerializeObject(value);
        }

        public static String JsonSerializeObject(this Object? value, Newtonsoft.Json.Formatting formatting)
        {
            return JsonConvert.SerializeObject(value, formatting);
        }

        public static String JsonSerializeObject(this Object? value, params JsonConverter[] converters)
        {
            return JsonConvert.SerializeObject(value, converters);
        }

        public static String JsonSerializeObject(this Object? value, Newtonsoft.Json.Formatting formatting, params JsonConverter[] converters)
        {
            return JsonConvert.SerializeObject(value, formatting, converters);
        }

        public static String JsonSerializeObject(this Object? value, JsonSerializerSettings? settings)
        {
            return JsonConvert.SerializeObject(value, settings);
        }

        public static String JsonSerializeObject(this Object? value, Type? type, JsonSerializerSettings? settings)
        {
            return JsonConvert.SerializeObject(value, type, settings);
        }

        public static String JsonSerializeObject(this Object? value, Newtonsoft.Json.Formatting formatting, JsonSerializerSettings? settings)
        {
            return JsonConvert.SerializeObject(value, formatting, settings);
        }

        public static String JsonSerializeObject(this Object? value, Type? type, Newtonsoft.Json.Formatting formatting, JsonSerializerSettings? settings)
        {
            return JsonConvert.SerializeObject(value, type, formatting, settings);
        }

        public static T? JsonDeserializeObject<T>(this String json)
        {
            return JsonConvert.DeserializeObject<T>(json);
        }
        
        public static T? JsonDeserializeObject<T>(this String json, JsonSerializerSettings? settings)
        {
            return JsonConvert.DeserializeObject<T>(json, settings);
        }

        public static Boolean TryJsonDeserializeObject<T>(this String? json, out T? result)
        {
            return TryJsonDeserializeObject(json, null, out result);
        }

        public static Boolean TryJsonDeserializeObject<T>(this String? json, JsonSerializerSettings? settings, out T? result)
        {
            if (String.IsNullOrWhiteSpace(json))
            {
                result = default;
                return false;
            }

            try
            {
                result = JsonConvert.DeserializeObject<T>(json, settings);
                return true;
            }
            catch (Exception)
            {
                result = default;
                return false;
            }
        }

        public static String ToJson(String xml)
        {
            return XmlUtils.Parse(xml).ToJson();
        }

        public static String ToJson(this XmlDocument document)
        {
            return JsonConvert.SerializeXmlNode(document, Newtonsoft.Json.Formatting.Indented, true);
        }

        private static PropertyContractResolver InitResolver(JsonSerializerSettings settings)
        {
            if (settings is null)
            {
                throw new ArgumentNullException(nameof(settings));
            }

            switch (settings.ContractResolver)
            {
                case null:
                {
                    PropertyContractResolver resolver = new PropertyContractResolver();
                    settings.ContractResolver = resolver;
                    return resolver;
                }
                case PropertyContractResolver resolver:
                {
                    return resolver;
                }
                case DefaultContractResolver other:
                {
                    PropertyContractResolver resolver = new PropertyContractResolver(other);
                    settings.ContractResolver = resolver;
                    return resolver;
                }
                default:
                    throw new NotSupportedException(
                        $"Invalid {nameof(settings.ContractResolver)}. Expected {nameof(DefaultContractResolver)}, received {settings.ContractResolver.GetType().FullName}.");
            }
        }

        public static JsonSerializerSettings RenameProperty(this JsonSerializerSettings settings, Type type, String property, String name)
        {
            InitResolver(settings).RenameProperty(type, property, name);
            return settings;
        }

        public static JsonSerializerSettings IgnoreProperty(this JsonSerializerSettings settings, Type type, String property)
        {
            InitResolver(settings).IgnoreProperty(type, property);
            return settings;
        }

        public static JsonSerializerSettings IgnoreProperty(this JsonSerializerSettings settings, Type type, params String[] properties)
        {
            InitResolver(settings).IgnoreProperty(type, properties);
            return settings;
        }

        public static JsonSerializerSettings OrderProperty(this JsonSerializerSettings settings, Type type, String property, Int32 order)
        {
            InitResolver(settings).OrderProperty(type, property, order);
            return settings;
        }
    }
}