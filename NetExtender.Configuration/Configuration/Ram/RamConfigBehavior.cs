// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using NetExtender.Configuration.Behavior;
using NetExtender.Configuration.Common;
using NetExtender.Crypto.CryptKey.Interfaces;
using NetExtender.Types.Trees;
using NetExtender.Utilities.Types;

namespace NetExtender.Configuration.Ram
{
    public class RamConfigBehavior : ConfigBehavior
    {
        protected DictionaryTree<String, String> Config { get; set; }

        public RamConfigBehavior()
            : this(ConfigOptions.None)
        {
        }
        
        public RamConfigBehavior(ConfigOptions options)
            : this(new DictionaryTree<String, String>(), null, null, options)
        {
        }
        
        public RamConfigBehavior(ICryptKey? crypt)
            : this(crypt, ConfigOptions.None)
        {
        }
        
        public RamConfigBehavior(ICryptKey? crypt, ConfigOptions options)
            : this(new DictionaryTree<String, String>(), null, crypt, options)
        {
        }

        public RamConfigBehavior(String? path)
            : this(path, ConfigOptions.None)
        {
        }

        public RamConfigBehavior(String? path, ConfigOptions options)
            : this(path, null, options)
        {
        }
        
        public RamConfigBehavior(String? path, ICryptKey? crypt)
            : this(path, crypt, ConfigOptions.None)
        {
        }
        
        public RamConfigBehavior(String? path, ICryptKey? crypt, ConfigOptions options)
            : this(new DictionaryTree<String, String>(), path, crypt, options)
        {
        }
        
        public RamConfigBehavior(DictionaryTree<String, String> config)
            : this(config, ConfigOptions.None)
        {
        }
        
        public RamConfigBehavior(DictionaryTree<String, String> config, ConfigOptions options)
            : this(config, null, null, options)
        {
        }
        
        public RamConfigBehavior(DictionaryTree<String, String> config, ICryptKey? crypt)
            : this(config, crypt, ConfigOptions.None)
        {
        }
        
        public RamConfigBehavior(DictionaryTree<String, String> config, ICryptKey? crypt, ConfigOptions options)
            : this(config, null, crypt, options)
        {
        }

        public RamConfigBehavior(DictionaryTree<String, String> config, String? path)
            : this(config, path, ConfigOptions.None)
        {
        }

        public RamConfigBehavior(DictionaryTree<String, String> config, String? path, ConfigOptions options)
            : this(config, path, null, options)
        {
        }
        
        public RamConfigBehavior(DictionaryTree<String, String> config, String? path, ICryptKey? crypt)
            : this(config, path, crypt, ConfigOptions.None)
        {
        }
        
        public RamConfigBehavior(DictionaryTree<String, String> config, String? path, ICryptKey? crypt, ConfigOptions options)
            : base(path ?? "RAM", crypt, options)
        {
            Config = config ?? throw new ArgumentNullException(nameof(config));
        }

        public override String? Get(String? key, IEnumerable<String>? sections)
        {
            return key is not null ? Config[key, sections].Value : null;
        }

        public override Boolean Set(String? key, String? value, IEnumerable<String>? sections)
        {
            if (key is null)
            {
                return false;
            }
            
            if (value is null)
            {
                return Config.Remove(key);
            }

            if (!IsLazyWrite)
            {
                Config[key, sections].Value = value;
                return true;
            }

            IImmutableList<String> immutable = sections.AsIImmutableList();

            if (Get(key, immutable) == value)
            {
                return false;
            }
            
            Config[key, immutable].Value = value;
            return true;
        }

        public override String?[] GetExistKeys()
        {
            return Config.Keys.ToArray(); //TODO: full path
        }

        public override Boolean Reload()
        {
            return false;
        }
    }
}