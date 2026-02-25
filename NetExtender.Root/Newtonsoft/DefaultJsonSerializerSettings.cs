// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Reflection;
using NetExtender.Utilities.Core;
using NetExtender.Utilities.Types;
using Newtonsoft.Json;

namespace NetExtender.Newtonsoft
{
    public class DefaultJsonSerializerSettings : JsonSerializerSettings
    {
        public static JsonSerializerSettings Settings { get; } = new DefaultJsonSerializerSettings();

        private static JsonSerializer? _serializer;
        internal static JsonSerializer Serializer
        {
            get
            {
                return _serializer ??= JsonSerializer.CreateDefault(Settings);
            }
        }

        public DefaultJsonSerializerSettings()
        {
            Formatting = Formatting.Indented;
            NullValueHandling = NullValueHandling.Ignore;
            ContractResolver = GenericContractResolver.Instance;
        }
    }
}

namespace NetExtender.Serialization.Json
{
    using System.Text.Json;
    using System.Text.Json.Serialization;

    public static class DefaultJsonSerializerOptions
    {
        [ReflectionSignature(typeof(JsonSerializerOptions))]
        private static JsonSerializerOptions? s_defaultOptions;
        public static JsonSerializerOptions Options
        {
            get
            {
                return s_defaultOptions ?? throw new NotSupportedException($"Field '{nameof(s_defaultOptions)}' not found in class '{typeof(DefaultJsonSerializerOptions)}'. Can't set default options.");
            }
            set
            {
                if (value is null)
                {
                    throw new ArgumentNullException(nameof(value));
                }

                const BindingFlags binding = BindingFlags.Static | BindingFlags.NonPublic;
                if (typeof(JsonSerializerOptions).GetField(nameof(s_defaultOptions), binding) is { } info)
                {
                    info.CreateSetDelegate<Action<JsonSerializerOptions?>>(null).Invoke(s_defaultOptions = value);
                }
            }
        }

        public static JsonSerializerOptions Create()
        {
            return new JsonSerializerOptions
            {
                WriteIndented = true,
                DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull,
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                Converters =
                {
                    new GenericJsonConverterFactory()
                }
            };
        }
    }
}