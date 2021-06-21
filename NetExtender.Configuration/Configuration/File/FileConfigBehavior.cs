// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Collections.Generic;
using NetExtender.Configuration.Common;
using NetExtender.Configuration.Ram;
using NetExtender.Crypto.CryptKey.Interfaces;
using NetExtender.Types.Trees;

namespace NetExtender.Configuration.File
{
    public abstract class FileConfigBehavior : LazyWriteRamConfigBehavior
    {
        protected FileConfigBehavior(String path, ConfigOptions options)
            : this(path, null, options)
        {
        }

        protected FileConfigBehavior(String path, ICryptKey? crypt, ConfigOptions options)
            : base(ValidatePathOrGetDefault(path, "txt"), crypt, options)
        {
            Config = ReadConfig();
        }

        private String ReadConfigText()
        {
            try
            {
                return System.IO.File.ReadAllText(Path);
            }
            catch (Exception)
            {
                return null;
            }
        }

        protected abstract DictionaryTree<String, String>? DeserializeConfig(String config);

        private DictionaryTree<String, String> ReadConfig()
        {
            try
            {
                String config = ReadConfigText();

                if (!IsCryptConfig)
                {
                    return DeserializeConfig(config) ?? new DictionaryTree<String, String>();
                }

                String decrypted = Crypt.Decrypt(config);

                return !String.IsNullOrWhiteSpace(decrypted) ? DeserializeConfig(decrypted) : DeserializeConfig(config);
            }
            catch (Exception)
            {
                return new DictionaryTree<String, String>();
            }
        }

        private void WriteConfigText(String? config)
        {
            if (String.IsNullOrWhiteSpace(config))
            {
                return;
            }
            
            System.IO.File.WriteAllText(Path, config);
        }

        protected abstract String SerializeConfig();

        private void WriteConfig()
        {
            String config = SerializeConfig();

            if (IsCryptConfig)
            {
                config = Crypt.Encrypt(config);
            }
            
            WriteConfigText(config);
        }

        public override Boolean Set(String key, String? value, IEnumerable<String>? sections)
        {
            if (!base.Set(key, value, sections))
            {
                return false;
            }

            WriteConfig();
            return true;
        }
    }
}