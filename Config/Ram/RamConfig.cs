// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using NetExtender.Config.Common;
using NetExtender.Crypto.CryptKey.Interfaces;
using NetExtender.Types.Trees;

namespace NetExtender.Config.Ram
{
    public class RamConfig : Config
    {
        protected DictionaryTree<String, String> Config;

        public RamConfig(String path = null, ConfigOptions options = ConfigOptions.None)
            : this(path, null, options)
        {
        }
        
        public RamConfig(String path, ICryptKey crypt, ConfigOptions options = ConfigOptions.None)
            : this(new DictionaryTree<String, String>(), path, crypt, options)
        {
        }

        public RamConfig(DictionaryTree<String, String> config, String path = null, ConfigOptions options = ConfigOptions.None)
            : this(config, path, null, options)
        {
        }
        
        public RamConfig(DictionaryTree<String, String> config, String path, ICryptKey crypt, ConfigOptions options = ConfigOptions.None)
            : base(path ?? "RAM", crypt, options)
        {
            Config = config;
        }

        protected override String Get(String key, params String[] sections)
        {
            return Config[key, sections].Value;
        }

        protected override Boolean Set(String key, String value, params String[] sections)
        {
            if (value is null)
            {
                return Config.Remove(key);
            }

            if (Get(key, sections) == value)
            {
                return false;
            }

            Config[key, sections].Value = value;
            return true;
        }
    }
}