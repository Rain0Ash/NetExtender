// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Reflection;
using NetExtender.Types.Attributes;
using NetExtender.Utilities.Core;
using Newtonsoft.Json;

namespace NetExtender.Newtonsoft
{
    [StaticInitializerRequired]
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

        static DefaultJsonSerializerSettings()
        {
            JsonConvert.DefaultSettings = static () => Settings;
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
    
    [StaticInitializerRequired]
    public static class DefaultJsonSerializerOptions
    {
        [ReflectionSignature(typeof(JsonSerializerOptions))]
        private static readonly JsonSerializerOptions? s_defaultOptions;
        public static JsonSerializerOptions Options
        {
            get
            {
                return s_defaultOptions ?? throw new NotSupportedException($"Field '{nameof(s_defaultOptions)}' not found in class '{typeof(DefaultJsonSerializerOptions)}'. Can't set default options.");
            }
        }

        static DefaultJsonSerializerOptions()
        {
            const BindingFlags binding = BindingFlags.Static | BindingFlags.NonPublic;
            if (typeof(JsonSerializerOptions).GetField(nameof(s_defaultOptions), binding) is not { } @default)
            {
                return;
            }

            s_defaultOptions = Create();
            @default.SetValue(null, s_defaultOptions);
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