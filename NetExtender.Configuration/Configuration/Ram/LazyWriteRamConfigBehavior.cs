// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using NetExtender.Configuration.Common;
using NetExtender.Crypto.CryptKey.Interfaces;
using NetExtender.Types.Trees;
using NetExtender.Utils.Types;

namespace NetExtender.Configuration.Ram
{
    public class LazyWriteRamConfigBehavior : RamConfigBehavior
    {
        public LazyWriteRamConfigBehavior(String? path = null, ConfigOptions options = ConfigOptions.None)
            : base(path, options)
        {
        }

        public LazyWriteRamConfigBehavior(String? path, ICryptKey? crypt, ConfigOptions options = ConfigOptions.None)
            : base(path, crypt, options)
        {
        }

        public LazyWriteRamConfigBehavior(DictionaryTree<String, String> config, String? path = null, ConfigOptions options = ConfigOptions.None)
            : base(config, path, options)
        {
        }

        public LazyWriteRamConfigBehavior(DictionaryTree<String, String> config, String? path, ICryptKey? crypt, ConfigOptions options = ConfigOptions.None)
            : base(config, path, crypt, options)
        {
        }

        public override Boolean Set(String key, String? value, IEnumerable<String>? sections)
        {
            if (value is null)
            {
                return Config.Remove(key);
            }

            IImmutableList<String> imsections = sections.AsIImmutableList();

            if (Get(key, imsections) == value)
            {
                return false;
            }

            Config[key, imsections].Value = value;
            return true;
        }
    }
}