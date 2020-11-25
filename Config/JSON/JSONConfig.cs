// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using NetExtender.Config.Common;
using NetExtender.Config.File;
using NetExtender.Crypto.CryptKey.Interfaces;
using NetExtender.Types.Trees;
using Newtonsoft.Json;

namespace NetExtender.Config.JSON
{
    public class JSONConfig : FileConfig
    {
        private static readonly JsonSerializerSettings Settings = new JsonSerializerSettings
        {
            Formatting = Formatting.Indented,
            NullValueHandling = NullValueHandling.Ignore
        };
        
        public JSONConfig(String path = null, ConfigOptions options = ConfigOptions.None)
            : this(path, null, options)
        {
        }
        
        public JSONConfig(String path, ICryptKey crypt, ConfigOptions options = ConfigOptions.None)
            : base(ValidatePathOrGetDefault(path, "json"), crypt, options)
        {
        }

        protected override DictionaryTree<String, String> DeserializeConfig(String config)
        {
            return String.IsNullOrWhiteSpace(config) ? new DictionaryTree<String, String>() : 
                JsonConvert.DeserializeObject<DictionaryTree<String, String>>(config, Settings);
        }

        protected override String SerializeConfig()
        {
            return JsonConvert.SerializeObject(Config, Settings);
        }
    }
}