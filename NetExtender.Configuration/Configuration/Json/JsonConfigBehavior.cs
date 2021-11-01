// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using NetExtender.Configuration.Common;
using NetExtender.Configuration.File;
using NetExtender.Crypto.CryptKey.Interfaces;
using NetExtender.NewtonSoft;
using NetExtender.Types.Trees;
using NetExtender.Utilities.Serialization;
using Newtonsoft.Json;

namespace NetExtender.Configuration.Json
{
    public class JsonConfigBehavior : FileConfigBehavior
    {
        protected static JsonSerializerSettings Default { get; } = new DefaultJsonSerializerSettings().RenameProperty(typeof(DictionaryTreeNode<String, String>), "Tree", "Config");

        public JsonSerializerSettings? Settings { get; init; } = Default;
        
        public JsonConfigBehavior()
            : this(ConfigOptions.None)
        {
        }
        
        public JsonConfigBehavior(ConfigOptions options)
            : this(null, null, options)
        {
        }
        
        public JsonConfigBehavior(ICryptKey? crypt)
            : this(crypt, ConfigOptions.None)
        {
        }
        
        public JsonConfigBehavior(ICryptKey? crypt, ConfigOptions options)
            : this(null, crypt, options)
        {
        }

        public JsonConfigBehavior(String? path)
            : this(path, ConfigOptions.None)
        {
        }

        public JsonConfigBehavior(String? path, ConfigOptions options)
            : this(path, null, options)
        {
        }
        
        public JsonConfigBehavior(String? path, ICryptKey? crypt)
            : this(path, crypt, ConfigOptions.None)
        {
        }
        
        public JsonConfigBehavior(String? path, ICryptKey? crypt, ConfigOptions options)
            : base(ValidatePathOrGetDefault(path, "json"), crypt, options)
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