// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Collections.Generic;
using NetExtender.Configuration.Common;
using NetExtender.Crypto.CryptKey.Interfaces;
using NetExtender.Types.Trees;

namespace NetExtender.Configuration.Ram
{
    public class RamConfigBehavior : ConfigBehavior
    {
        protected DictionaryTree<String, String> Config;

        public RamConfigBehavior(String? path = null, ConfigOptions options = ConfigOptions.None)
            : this(path, null, options)
        {
        }
        
        public RamConfigBehavior(String? path, ICryptKey? crypt, ConfigOptions options = ConfigOptions.None)
            : this(new DictionaryTree<String, String>(), path, crypt, options)
        {
        }

        public RamConfigBehavior(DictionaryTree<String, String> config, String? path = null, ConfigOptions options = ConfigOptions.None)
            : this(config, path, null, options)
        {
        }
        
        public RamConfigBehavior(DictionaryTree<String, String> config, String? path, ICryptKey? crypt, ConfigOptions options = ConfigOptions.None)
            : base(path ?? "RAM", crypt, options)
        {
            Config = config;
        }

        public override String Get(String key, IEnumerable<String>? sections)
        {
            return Config[key, sections].Value;
        }

        public override Boolean Set(String key, String? value, IEnumerable<String>? sections)
        {
            if (value is null)
            {
                return Config.Remove(key);
            }

            Config[key, sections].Value = value;
            return true;
        }
    }
}