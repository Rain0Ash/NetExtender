// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Security.Cryptography;
using System.Threading;
using System.Threading.Tasks;
using NetExtender.Configuration.Behavior.Interfaces;
using NetExtender.Configuration.Common;
using NetExtender.Configuration.Cryptography.Behavior.Interfaces;
using NetExtender.Configuration.Cryptography.Common;
using NetExtender.Configuration.Cryptography.Common.Interfaces;
using NetExtender.Crypto;
using NetExtender.Crypto.CryptKey.Interfaces;
using NetExtender.Utilities.Crypto;
using NetExtender.Utilities.Types;

namespace NetExtender.Configuration.Cryptography.Behavior
{
    public class CryptographyBehavior : ICryptographyConfigBehavior
    {
        private IConfigBehavior Behavior { get; }
        
        public IConfigurationCryptor Cryptor { get; }

        public event ConfigurationChangedEventHandler Changed = null!;

        public String Path
        {
            get
            {
                return Behavior.Path;
            }
        }

        public ConfigOptions Options
        {
            get
            {
                return Behavior.Options;
            }
        }

        public Boolean IsReadOnly
        {
            get
            {
                return Behavior.IsReadOnly;
            }
        }
        
        public Boolean IsIgnoreEvent
        {
            get
            {
                return Behavior.IsIgnoreEvent;
            }
        }

        public Boolean IsLazyWrite
        {
            get
            {
                return Behavior.IsLazyWrite;
            }
        }

        public String Joiner
        {
            get
            {
                return Behavior.Joiner;
            }
        }

        public CryptAction Crypt
        {
            get
            {
                return Cryptor.Crypt;
            }
        }

        public CryptographyConfigOptions CryptographyOptions
        {
            get
            {
                return Cryptor.CryptographyOptions;
            }
        }

        public Boolean IsCryptDefault
        {
            get
            {
                return Cryptor.IsCryptDefault;
            }
        }

        public Boolean IsCryptKey
        {
            get
            {
                return Cryptor.IsCryptKey;
            }
        }

        public Boolean IsCryptValue
        {
            get
            {
                return Cryptor.IsCryptValue;
            }
        }

        public Boolean IsCryptSections
        {
            get
            {
                return Cryptor.IsCryptSections;
            }
        }

        public Boolean IsCryptConfig
        {
            get
            {
                return Cryptor.IsCryptConfig;
            }
        }

        public Boolean IsCryptAll
        {
            get
            {
                return Cryptor.IsCryptAll;
            }
        }

        public CryptographyBehavior(IConfigBehavior behavior, IConfigurationCryptor cryptor)
        {
            Behavior = behavior ?? throw new ArgumentNullException(nameof(behavior));
            Cryptor = cryptor ?? throw new ArgumentNullException(nameof(cryptor));
        }
        
        protected virtual Boolean TryEncryptKey(String? key, IStringEncryptor? encryptor, out String? result)
        {
            if (encryptor is null && !IsCryptDefault)
            {
                result = key;
                return true;
            }

            encryptor ??= Cryptor;
            return encryptor.TryEncrypt(key, out result);
        }

        protected virtual Boolean TryDecryptKey(String? key, IStringDecryptor? decryptor, out String? result)
        {
            if (decryptor is null && !IsCryptDefault)
            {
                result = key;
                return true;
            }

            decryptor ??= Cryptor;
            return decryptor.TryDecrypt(key, out result);
        }

        protected virtual Boolean TryEncryptValue(String? value, IStringEncryptor? encryptor, out String? result)
        {
            if (encryptor is null && !IsCryptDefault)
            {
                result = value;
                return true;
            }

            encryptor ??= Cryptor;
            return encryptor.TryEncrypt(value, out result);
        }

        protected virtual Boolean TryDecryptValue(String? value, IStringDecryptor? decryptor, out String? result)
        {
            if (decryptor is null && !IsCryptDefault)
            {
                result = value;
                return true;
            }

            decryptor ??= Cryptor;
            return decryptor.TryDecrypt(value, out result);
        }

        protected virtual Boolean TryEncryptSections(IEnumerable<String>? sections, IStringEncryptor? encryptor, out IEnumerable<String>? result)
        {
            if (encryptor is null && !IsCryptDefault)
            {
                result = sections;
                return true;
            }

            encryptor ??= Cryptor;
            return encryptor.TryEncrypt(sections, out result);
        }

        protected virtual Boolean TryDecryptSections(IEnumerable<String>? sections, IStringDecryptor? decryptor, out IEnumerable<String>? result)
        {
            if (decryptor is null && !IsCryptDefault)
            {
                result = sections;
                return true;
            }

            decryptor ??= Cryptor;
            return decryptor.TryDecrypt(sections, out result);
        }

        public Boolean Contains(String? key, IEnumerable<String>? sections)
        {
            return Contains(key, null, sections);
        }
        
        public virtual Boolean Contains(String? key, IConfigurationCryptor? cryptor, IEnumerable<String>? sections)
        {
            if ((cryptor?.IsCryptKey ?? IsCryptKey) && !TryEncryptKey(key, cryptor, out key))
            {
                throw new CryptographicException();
            }

            if ((cryptor?.IsCryptSections ?? IsCryptSections) && !TryEncryptSections(sections, cryptor, out sections))
            {
                throw new CryptographicException();
            }

            return ContainsRaw(key, sections);
        }
        
        public Boolean ContainsRaw(String? key, IEnumerable<String>? sections)
        {
            return Behavior.Contains(key, sections);
        }

        public Task<Boolean> ContainsAsync(String? key, IEnumerable<String>? sections, CancellationToken token)
        {
            return ContainsAsync(key, null, sections, token);
        }
        
        public virtual Task<Boolean> ContainsAsync(String? key, IConfigurationCryptor? cryptor, IEnumerable<String>? sections, CancellationToken token)
        {
            if ((cryptor?.IsCryptKey ?? IsCryptKey) && !TryEncryptKey(key, cryptor, out key))
            {
                throw new CryptographicException();
            }

            if ((cryptor?.IsCryptSections ?? IsCryptSections) && !TryEncryptSections(sections, cryptor, out sections))
            {
                throw new CryptographicException();
            }

            return ContainsRawAsync(key, sections, token);
        }

        public Task<Boolean> ContainsRawAsync(String? key, IEnumerable<String>? sections, CancellationToken token)
        {
            return Behavior.ContainsAsync(key, sections, token);
        }

        public String? Get(String? key, IEnumerable<String>? sections)
        {
            return Get(key, null, sections);
        }
        
        public virtual String? Get(String? key, IConfigurationCryptor? cryptor, IEnumerable<String>? sections)
        {
            if ((cryptor?.IsCryptKey ?? IsCryptKey) && !TryEncryptKey(key, cryptor, out key))
            {
                throw new CryptographicException();
            }
            
            if ((cryptor?.IsCryptSections ?? IsCryptSections) && !TryEncryptSections(sections, cryptor, out sections))
            {
                throw new CryptographicException();
            }

            String? value = GetRaw(key, sections);

            if (value is not null && (cryptor?.IsCryptValue ?? IsCryptValue))
            {
                return TryDecryptValue(value, cryptor, out String? result) ? result : value;
            }

            return value;
        }
        
        public String? GetRaw(String? key, IEnumerable<String>? sections)
        {
            return Behavior.Get(key, sections);
        }

        public Task<String?> GetAsync(String? key, IEnumerable<String>? sections, CancellationToken token)
        {
            return GetAsync(key, null, sections, token);
        }
        
        public virtual async Task<String?> GetAsync(String? key, IConfigurationCryptor? cryptor, IEnumerable<String>? sections, CancellationToken token)
        {
            if ((cryptor?.IsCryptKey ?? IsCryptKey) && !TryEncryptKey(key, cryptor, out key))
            {
                throw new CryptographicException();
            }
            
            if ((cryptor?.IsCryptSections ?? IsCryptSections) && !TryEncryptSections(sections, cryptor, out sections))
            {
                throw new CryptographicException();
            }

            String? value = await GetRawAsync(key, sections, token);

            if (value is not null && (cryptor?.IsCryptValue ?? IsCryptValue))
            {
                return TryDecryptValue(value, cryptor, out String? result) ? result : value;
            }

            return value;
        }

        public Task<String?> GetRawAsync(String? key, IEnumerable<String>? sections, CancellationToken token)
        {
            return Behavior.GetAsync(key, sections, token);
        }

        public Boolean Set(String? key, String? value, IEnumerable<String>? sections)
        {
            return Set(key, value, null, sections);
        }
        
        public virtual Boolean Set(String? key, String? value, IConfigurationCryptor? cryptor, IEnumerable<String>? sections)
        {
            if ((cryptor?.IsCryptKey ?? IsCryptKey) && !TryEncryptKey(key, cryptor, out key))
            {
                throw new CryptographicException();
            }
            
            if ((cryptor?.IsCryptValue ?? IsCryptValue) && !TryEncryptValue(value, cryptor, out value))
            {
                throw new CryptographicException();
            }
            
            if ((cryptor?.IsCryptSections ?? IsCryptSections) && !TryEncryptSections(sections, cryptor, out sections))
            {
                throw new CryptographicException();
            }

            return SetRaw(key, value, sections);
        }

        public Boolean SetRaw(String? key, String? value, IEnumerable<String>? sections)
        {
            return Behavior.Set(key, value, sections);
        }

        public Task<Boolean> SetAsync(String? key, String? value, IEnumerable<String>? sections, CancellationToken token)
        {
            return SetAsync(key, value, null, sections, token);
        }
        
        public virtual Task<Boolean> SetAsync(String? key, String? value, IConfigurationCryptor? cryptor, IEnumerable<String>? sections, CancellationToken token)
        {
            if ((cryptor?.IsCryptKey ?? IsCryptKey) && !TryEncryptKey(key, cryptor, out key))
            {
                throw new CryptographicException();
            }
            
            if ((cryptor?.IsCryptValue ?? IsCryptValue) && !TryEncryptValue(value, cryptor, out value))
            {
                throw new CryptographicException();
            }
            
            if ((cryptor?.IsCryptSections ?? IsCryptSections) && !TryEncryptSections(sections, cryptor, out sections))
            {
                throw new CryptographicException();
            }

            return SetRawAsync(key, value, sections, token);
        }
        
        public Task<Boolean> SetRawAsync(String? key, String? value, IEnumerable<String>? sections, CancellationToken token)
        {
            return Behavior.SetAsync(key, value, sections, token);
        }

        public String? GetOrSet(String? key, String? value, IEnumerable<String>? sections)
        {
            return GetOrSet(key, value, null, sections);
        }

        public virtual String? GetOrSet(String? key, String? value, IConfigurationCryptor? cryptor, IEnumerable<String>? sections)
        {
            if ((cryptor?.IsCryptKey ?? IsCryptKey) && !TryEncryptKey(key, cryptor, out key))
            {
                throw new CryptographicException();
            }
            
            if ((cryptor?.IsCryptValue ?? IsCryptValue) && !TryEncryptValue(value, cryptor, out value))
            {
                throw new CryptographicException();
            }
            
            if ((cryptor?.IsCryptSections ?? IsCryptSections) && !TryEncryptSections(sections, cryptor, out sections))
            {
                throw new CryptographicException();
            }

            String? raw = GetOrSetRaw(key, value, sections);

            if (raw is not null && (cryptor?.IsCryptValue ?? IsCryptValue) && TryDecryptValue(raw, cryptor, out value))
            {
                return value;
            }

            return raw;
        }
        
        public String? GetOrSetRaw(String? key, String? value, IEnumerable<String>? sections)
        {
            return Behavior.GetOrSet(key, value, sections);
        }

        public Task<String?> GetOrSetAsync(String? key, String? value, IEnumerable<String>? sections, CancellationToken token)
        {
            return GetOrSetAsync(key, value, null, sections, token);
        }

        public virtual async Task<String?> GetOrSetAsync(String? key, String? value, IConfigurationCryptor? cryptor, IEnumerable<String>? sections, CancellationToken token)
        {
            if ((cryptor?.IsCryptKey ?? IsCryptKey) && !TryEncryptKey(key, cryptor, out key))
            {
                throw new CryptographicException();
            }
            
            if ((cryptor?.IsCryptValue ?? IsCryptValue) && !TryEncryptValue(value, cryptor, out value))
            {
                throw new CryptographicException();
            }
            
            if ((cryptor?.IsCryptSections ?? IsCryptSections) && !TryEncryptSections(sections, cryptor, out sections))
            {
                throw new CryptographicException();
            }

            String? raw = await GetOrSetRawAsync(key, value, sections, token);

            if (raw is not null && (cryptor?.IsCryptValue ?? IsCryptValue) && TryDecryptValue(raw, cryptor, out value))
            {
                return value;
            }

            return raw;
        }

        public Task<String?> GetOrSetRawAsync(String? key, String? value, IEnumerable<String>? sections, CancellationToken token)
        {
            return Behavior.GetOrSetAsync(key, value, sections, token);
        }

        public ConfigurationEntry[]? GetExists()
        {
            return GetExists(null);
        }
        
        public ConfigurationEntry[]? GetExists(IConfigurationCryptor? cryptor)
        {
            ConfigurationEntry[]? entries = GetExistsRaw();

            if (entries is null)
            {
                return null;
            }

            for (Int32 i = 0; i < entries.Length; i++)
            {
                (String? key, ImmutableArray<String> array) = entries[i];

                if ((cryptor?.IsCryptKey ?? IsCryptKey) && !TryDecryptKey(key, cryptor, out key))
                {
                    throw new CryptographicException();
                }

                IEnumerable<String>? sections = array;
                if ((cryptor?.IsCryptSections ?? IsCryptSections) && !TryDecryptSections(sections, cryptor, out sections))
                {
                    throw new CryptographicException();
                }
                
                entries[i] = new ConfigurationEntry(key, sections.AsImmutableArray());
            }

            return entries;
        }

        public ConfigurationEntry[]? GetExistsRaw()
        {
            return Behavior.GetExists();
        }
        
        public ConfigurationValueEntry[]? GetExistsValues()
        {
            return GetExistsValues(null);
        }
        
        // ReSharper disable once CognitiveComplexity
        public ConfigurationValueEntry[]? GetExistsValues(IConfigurationCryptor? cryptor)
        {
            ConfigurationValueEntry[]? entries = GetExistsValuesRaw();

            if (entries is null)
            {
                return null;
            }

            for (Int32 i = 0; i < entries.Length; i++)
            {
                (String? key, String? value, ImmutableArray<String> array) = entries[i];

                if ((cryptor?.IsCryptKey ?? IsCryptKey) && !TryDecryptKey(key, cryptor, out key))
                {
                    throw new CryptographicException();
                }
                
                if ((cryptor?.IsCryptValue ?? IsCryptValue) && !TryDecryptValue(value, cryptor, out value))
                {
                    throw new CryptographicException();
                }

                IEnumerable<String>? sections = array;
                if ((cryptor?.IsCryptSections ?? IsCryptSections) && !TryDecryptSections(sections, cryptor, out sections))
                {
                    throw new CryptographicException();
                }
                
                entries[i] = new ConfigurationValueEntry(key, value, sections.AsImmutableArray());
            }

            return entries;
        }

        public ConfigurationValueEntry[]? GetExistsValuesRaw()
        {
            return Behavior.GetExistsValues();
        }

        public Task<ConfigurationEntry[]?> GetExistsAsync(CancellationToken token)
        {
            return GetExistsAsync(null, token);
        }

        public async Task<ConfigurationEntry[]?> GetExistsAsync(IConfigurationCryptor? cryptor, CancellationToken token)
        {
            ConfigurationEntry[]? entries = await GetExistsRawAsync(token);

            if (entries is null)
            {
                return null;
            }

            for (Int32 i = 0; i < entries.Length; i++)
            {
                (String? key, ImmutableArray<String> array) = entries[i];

                if ((cryptor?.IsCryptKey ?? IsCryptKey) && !TryDecryptKey(key, cryptor, out key))
                {
                    throw new CryptographicException();
                }

                IEnumerable<String>? sections = array;
                if ((cryptor?.IsCryptSections ?? IsCryptSections) && !TryDecryptSections(sections, cryptor, out sections))
                {
                    throw new CryptographicException();
                }
                
                entries[i] = new ConfigurationEntry(key, sections.AsImmutableArray());
            }

            return entries;
        }

        public Task<ConfigurationEntry[]?> GetExistsRawAsync(CancellationToken token)
        {
            return Behavior.GetExistsAsync(token);
        }
        
        public Task<ConfigurationValueEntry[]?> GetExistsValuesAsync(CancellationToken token)
        {
            return GetExistsValuesAsync(null, token);
        }

        // ReSharper disable once CognitiveComplexity
        public async Task<ConfigurationValueEntry[]?> GetExistsValuesAsync(IConfigurationCryptor? cryptor, CancellationToken token)
        {
            ConfigurationValueEntry[]? entries = await GetExistsValuesRawAsync(token);

            if (entries is null)
            {
                return null;
            }

            for (Int32 i = 0; i < entries.Length; i++)
            {
                (String? key, String? value, ImmutableArray<String> array) = entries[i];

                if ((cryptor?.IsCryptKey ?? IsCryptKey) && !TryDecryptKey(key, cryptor, out key))
                {
                    throw new CryptographicException();
                }
                
                if ((cryptor?.IsCryptValue ?? IsCryptValue) && !TryDecryptValue(value, cryptor, out value))
                {
                    throw new CryptographicException();
                }

                IEnumerable<String>? sections = array;
                if ((cryptor?.IsCryptSections ?? IsCryptSections) && !TryDecryptSections(sections, cryptor, out sections))
                {
                    throw new CryptographicException();
                }
                
                entries[i] = new ConfigurationValueEntry(key, value, sections.AsImmutableArray());
            }

            return entries;
        }

        public Task<ConfigurationValueEntry[]?> GetExistsValuesRawAsync(CancellationToken token)
        {
            return Behavior.GetExistsValuesAsync(token);
        }

        public Boolean Reload()
        {
            return Behavior.Reload();
        }

        public Task<Boolean> ReloadAsync(CancellationToken token)
        {
            return Behavior.ReloadAsync(token);
        }
        
        protected void InvokeChanged(ConfigurationValueEntry entry)
        {
            Changed?.Invoke(this, new ConfigurationChangedEventArgs(entry));
        }
        
        public void Dispose()
        {
            Behavior.Dispose();
            GC.SuppressFinalize(this);
        }
    }
}