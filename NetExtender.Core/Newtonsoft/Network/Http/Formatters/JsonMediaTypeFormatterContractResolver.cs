// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Reflection;
using NetExtender.Types.Network.Formatters;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace NetExtender.Newtonsoft.Types.Network.Formatters
{
    public class JsonMediaTypeFormatterContractResolver : DefaultContractResolver
    {
        private MediaTypeFormatter Formatter { get; }

        public JsonMediaTypeFormatterContractResolver(MediaTypeFormatter formatter)
        {
            Formatter = formatter ?? throw new ArgumentNullException(nameof(formatter));
            IgnoreSerializableAttribute = false;
        }

        private void ConfigureProperty(MemberInfo member, JsonProperty property)
        {
            if (member is null)
            {
                throw new ArgumentNullException(nameof(member));
            }

            if (property is null)
            {
                throw new ArgumentNullException(nameof(property));
            }

            if (Formatter.Selector is null || !Formatter.Selector.IsRequired(member))
            {
                property.Required = Required.Default;
                return;
            }

            property.Required = Required.AllowNull;
            property.DefaultValueHandling = DefaultValueHandling.Include;
            property.NullValueHandling = NullValueHandling.Include;
        }

        protected override JsonProperty CreateProperty(MemberInfo member, MemberSerialization serialization)
        {
            if (member is null)
            {
                throw new ArgumentNullException(nameof(member));
            }

            JsonProperty property = base.CreateProperty(member, serialization);
            ConfigureProperty(member, property);
            return property;
        }
    }
}