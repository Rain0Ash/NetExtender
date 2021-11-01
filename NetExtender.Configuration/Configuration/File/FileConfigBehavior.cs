// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using NetExtender.Configuration.Common;
using NetExtender.Configuration.Ram;
using NetExtender.Crypto.CryptKey.Interfaces;
using NetExtender.Types.Trees;
using NetExtender.Utilities.Types;

namespace NetExtender.Configuration.File
{
    public abstract class FileConfigBehavior : RamConfigBehavior
    {
        public Encoding? Encoding { get; init; }
        
        protected FileConfigBehavior(String? path, ConfigOptions options)
            : this(path, null, options)
        {
        }

        protected FileConfigBehavior(String? path, ICryptKey? crypt, ConfigOptions options)
            : base(ValidatePathOrGetDefault(path, "txt"), crypt, options | ConfigOptions.LazyWrite)
        {
            Config = ReadConfig();
        }

        protected String? ReadConfigText()
        {
            try
            {
                return System.IO.File.ReadAllText(Path, Encoding ?? Encoding.UTF8);
            }
            catch (Exception)
            {
                return null;
            }
        }

        protected abstract DictionaryTree<String, String>? DeserializeConfig(String config);

        protected DictionaryTree<String, String> ReadConfig()
        {
            return ReadConfig(out DictionaryTree<String, String>? result) ? result : new DictionaryTree<String, String>();
        }

        protected Boolean ReadConfig([MaybeNullWhen(false)] out DictionaryTree<String, String> result)
        {
            try
            {
                String? config = ReadConfigText();

                if (config is null)
                {
                    result = default;
                    return false;
                }
                
                if (!IsCryptConfig)
                {
                    result = DeserializeConfig(config);
                    return result is not null;
                }

                String? decrypted = Crypt.Decrypt(config);
                
                result = String.IsNullOrEmpty(decrypted) ? DeserializeConfig(config) : DeserializeConfig(decrypted);
                return result is not null;
            }
            catch (Exception)
            {
                result = default;
                return false;
            }
        }

        protected Boolean WriteConfigText(String? config)
        {
            if (String.IsNullOrEmpty(config))
            {
                return false;
            }

            try
            {
                System.IO.File.WriteAllText(Path, config, Encoding ?? Encoding.UTF8);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        protected async Task<Boolean> WriteConfigTextAsync(String? config, CancellationToken token)
        {
            if (String.IsNullOrEmpty(config))
            {
                return false;
            }

            try
            {
                await System.IO.File.WriteAllTextAsync(Path, config, token);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        protected abstract String? SerializeConfig();

        protected Task<String?> SerializeConfigAsync()
        {
            return SerializeConfigAsync(CancellationToken.None);
        }
        
        protected virtual Task<String?> SerializeConfigAsync(CancellationToken token)
        {
            return !token.IsCancellationRequested ? Task.FromResult(SerializeConfig()) : StringUtilities.Null;
        }

        protected Boolean WriteConfig()
        {
            String? config = SerializeConfig();

            if (config is not null && IsCryptConfig)
            {
                config = Crypt.Encrypt(config);
            }
            
            return WriteConfigText(config);
        }

        protected Task<Boolean> WriteConfigAsync()
        {
            return WriteConfigAsync(CancellationToken.None);
        }

        protected Task<Boolean> WriteConfigAsync(CancellationToken token)
        {
            if (token.IsCancellationRequested)
            {
                return TaskUtilities.False;
            }
            
            String? config = SerializeConfig();

            if (config is not null && IsCryptConfig)
            {
                config = Crypt.Encrypt(config);
            }
            
            return WriteConfigTextAsync(config, token);
        }

        public override Boolean Set(String? key, String? value, IEnumerable<String>? sections)
        {
            return base.Set(key, value, sections) && WriteConfig();
        }

        public override async Task<Boolean> SetAsync(String? key, String? value, IEnumerable<String>? sections, CancellationToken token)
        {
            return await base.SetAsync(key, value, sections, token) && await WriteConfigAsync(token);
        }

        public override Boolean Reload()
        {
            if (!ReadConfig(out DictionaryTree<String, String>? config))
            {
                return false;
            }

            Config = config;
            return true;
        }
    }
}