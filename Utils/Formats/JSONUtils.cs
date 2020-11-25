// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Xml;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace NetExtender.Utils.Formats
{
    public static class JSONUtils
    {
        public static Boolean IsValidJson(String json)
        {
            json = json.Trim();
            
            if (String.IsNullOrEmpty(json) || (!json.StartsWith("{") || !json.EndsWith("}")) && (!json.StartsWith("[") || !json.EndsWith("]")))
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

        public static T DeserializeObject<T>(String json, JsonSerializerSettings settings = null)
        {
            return JsonConvert.DeserializeObject<T>(json, settings);
        }

        public static Boolean TryDeserializeObject<T>(String json, out T result)
        {
            return TryDeserializeObject(json, null, out result);
        }

        public static Boolean TryDeserializeObject<T>(String json, JsonSerializerSettings settings, out T result)
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

        public static String ToJSON(String xml)
        {
            return XMLUtils.Parse(xml).ToJSON();
        }

        public static String ToJSON(this XmlDocument document)
        {
            return JsonConvert.SerializeXmlNode(document, Newtonsoft.Json.Formatting.Indented, true);
        }
    }
}