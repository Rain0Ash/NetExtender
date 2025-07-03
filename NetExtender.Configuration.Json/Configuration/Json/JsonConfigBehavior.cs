// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using NetExtender.Configuration.Common;
using NetExtender.Configuration.File;
using NetExtender.Newtonsoft;
using NetExtender.Types.Trees;
using Newtonsoft.Json;

namespace NetExtender.Configuration.Json
{
    public class JsonConfigBehavior : FileConfigBehavior
    {
        protected static JsonSerializerSettings Default { get; } = new DefaultJsonSerializerSettings();

        public JsonSerializerSettings? Settings { get; init; } = Default;

        public JsonConfigBehavior()
            : this(ConfigOptions.None)
        {
        }

        public JsonConfigBehavior(ConfigOptions options)
            : this(null, options)
        {
        }

        public JsonConfigBehavior(String? path)
            : this(path, ConfigOptions.None)
        {
        }

        public JsonConfigBehavior(String? path, ConfigOptions options)
            : base(ValidatePathOrGetDefault(path, "json"), options)
        {
        }

        protected override DictionaryTree<String, String>? DeserializeConfig(String config)
        {
            return String.IsNullOrWhiteSpace(config) ? new DictionaryTree<String, String>() : 
                JsonConvert.DeserializeObject<DictionaryTree<String, String>>(config, Settings ?? Default);
        }

        protected override String? SerializeConfig()
        {
            return JsonConvert.SerializeObject(Config, Settings ?? Default);
        }
    }
}