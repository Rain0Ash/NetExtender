// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Security.Cryptography;
using System.Threading;
using System.Threading.Tasks;
using NetExtender.Configuration.Behavior.Interfaces;
using NetExtender.Configuration.Common;
using NetExtender.Configuration.Cryptography.Behavior.Interfaces;
using NetExtender.Configuration.Cryptography.Common;
using NetExtender.Crypto.CryptKey.Interfaces;
using NetExtender.Utilities.Crypto;
using NetExtender.Utilities.Types;

namespace NetExtender.Configuration.Cryptography.Behavior
{
    public class CryptographyBehavior : ICryptographyConfigBehavior
    {
        private IConfigBehavior Behavior { get; }
        
        public IStringCryptor Cryptor { get; }
        private IStringEncryptor Encryptor { get; }
        private IStringDecryptor Decryptor { get; }
        public CryptographyConfigOptions CryptOptions { get; }

        public Boolean IsCryptDefault
        {
            get
            {
                return CryptOptions.HasFlag(CryptographyConfigOptions.CryptDefault);
            }
        }

        public Boolean IsCryptKey
        {
            get
            {
                return CryptOptions.HasFlag(CryptographyConfigOptions.CryptKey);
            }
        }

        public Boolean IsCryptValue
        {
            get
            {
                return CryptOptions.HasFlag(CryptographyConfigOptions.CryptValue);
            }
        }

        public Boolean IsCryptSections
        {
            get
            {
                return CryptOptions.HasFlag(CryptographyConfigOptions.CryptSections);
            }
        }

        public Boolean IsCryptConfig
        {
            get
            {
                return CryptOptions.HasFlag(CryptographyConfigOptions.CryptConfig);
            }
        }

        public Boolean IsCryptAll
        {
            get
            {
                return CryptOptions.HasFlag(CryptographyConfigOptions.CryptAll);
            }
        }

        public event EventHandler<ConfigurationEntry> Changed = null!;

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
        
        public CryptographyBehavior(IConfigBehavior behavior, IStringCryptor cryptor)
            : this(behavior, cryptor, CryptographyConfigOptions.All)
        {
        }
        
        public CryptographyBehavior(IConfigBehavior behavior, IStringCryptor cryptor, CryptographyConfigOptions options)
        {
            Behavior = behavior ?? throw new ArgumentNullException(nameof(behavior));
            Cryptor = cryptor ?? throw new ArgumentNullException(nameof(cryptor));
            CryptOptions = options;
            Encryptor = Cryptor.IsDeterministic || !IsCryptKey && !IsCryptSections ? Cryptor : Cryptor.CreateDeterministic();
            Decryptor = Cryptor;
        }
        
        protected virtual Boolean TryEncryptKey(String? key, IStringEncryptor? encryptor, out String? result)
        {
            if (key is null)
            {
                result = key;
                return true;
            }

            if (encryptor is null && !IsCryptDefault)
            {
                result = key;
                return true;
            }

            encryptor ??= Encryptor;

            try
            {
                if (!encryptor.IsEncrypt)
                {
                    result = default;
                    return false;
                }

                result = encryptor.Encrypt(key);
                return result is not null;
            }
            catch (Exception)
            {
                result = default;
                return false;
            }
        }

        protected virtual Boolean TryDecryptKey(String? key, IStringDecryptor? decryptor, out String? result)
        {
            if (key is null)
            {
                result = key;
                return true;
            }

            if (decryptor is null && !IsCryptDefault)
            {
                result = key;
                return true;
            }

            decryptor ??= Decryptor;

            try
            {
                if (!decryptor.IsDecrypt)
                {
                    result = default;
                    return false;
                }
                        
                result = decryptor.Decrypt(key);
                return result is not null;
            }
            catch (Exception)
            {
                result = default;
                return false;
            }
        }

        protected virtual Boolean TryEncryptValue(String? value, IStringEncryptor? encryptor, out String? result)
        {
            if (value is null)
            {
                result = value;
                return true;
            }
            
            if (encryptor is null && !IsCryptDefault)
            {
                result = value;
                return true;
            }

            encryptor ??= Encryptor;
            
            try
            {
                if (!encryptor.IsEncrypt)
                {
                    result = default;
                    return false;
                }
                        
                result = encryptor.Encrypt(value);
                return result is not null;
            }
            catch (Exception)
            {
                result = default;
                return false;
            }
        }

        protected virtual Boolean TryDecryptValue(String? value, IStringDecryptor? decryptor, out String? result)
        {
            if (value is null)
            {
                result = value;
                return true;
            }
            
            if (decryptor is null && !IsCryptDefault)
            {
                result = value;
                return true;
            }

            decryptor ??= Decryptor;
            
            try
            {
                if (!decryptor.IsDecrypt)
                {
                    result = default;
                    return false;
                }
                        
                result = decryptor.Decrypt(value);
                return result is not null;
            }
            catch (Exception)
            {
                result = default;
                return false;
            }
        }

        protected virtual Boolean TryEncryptSections(IEnumerable<String>? sections, IStringEncryptor? encryptor, out IEnumerable<String>? result)
        {
            if (sections is null)
            {
                result = sections;
                return true;
            }
            
            if (encryptor is null && !IsCryptDefault)
            {
                result = sections;
                return true;
            }

            encryptor ??= Encryptor;
            
            try
            {
                if (!encryptor.IsEncrypt)
                {
                    result = default;
                    return false;
                }
                        
                result = encryptor.Encrypt(sections).ThrowIfNull<String, CryptographicException>().ToArray();
                return true;
            }
            catch (Exception)
            {
                result = default;
                return false;
            }
        }

        protected virtual Boolean TryDecryptSections(IEnumerable<String>? sections, IStringDecryptor? decryptor, out IEnumerable<String>? result)
        {
            if (sections is null)
            {
                result = sections;
                return true;
            }
            
            if (decryptor is null && !IsCryptDefault)
            {
                result = sections;
                return true;
            }

            decryptor ??= Cryptor;
            
            try
            {
                if (!decryptor.IsDecrypt)
                {
                    result = default;
                    return false;
                }
                        
                result = decryptor.Decrypt(sections).ThrowIfNull<String, CryptographicException>().ToArray();
                return true;
            }
            catch (Exception)
            {
                result = default;
                return false;
            }
        }

        public Boolean Contains(String? key, IEnumerable<String>? sections)
        {
            return Contains(key, null, sections);
        }
        
        public virtual Boolean Contains(String? key, IStringCryptor? cryptor, IEnumerable<String>? sections)
        {
            if (IsCryptKey && !TryEncryptKey(key, cryptor, out key))
            {
                throw new CryptographicException();
            }

            if (IsCryptSections && !TryEncryptSections(sections, cryptor, out sections))
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
        
        public virtual Task<Boolean> ContainsAsync(String? key, IStringCryptor? cryptor, IEnumerable<String>? sections, CancellationToken token)
        {
            if (IsCryptKey && !TryEncryptKey(key, cryptor, out key))
            {
                throw new CryptographicException();
            }

            if (IsCryptSections && !TryEncryptSections(sections, cryptor, out sections))
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
        
        public virtual String? Get(String? key, IStringCryptor? cryptor, IEnumerable<String>? sections)
        {
            if (IsCryptKey && !TryEncryptKey(key, cryptor, out key))
            {
                throw new CryptographicException();
            }
            
            if (IsCryptSections && !TryEncryptSections(sections, cryptor, out sections))
            {
                throw new CryptographicException();
            }

            String? value = GetRaw(key, sections);

            if (value is not null && IsCryptValue)
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
        
        public virtual async Task<String?> GetAsync(String? key, IStringCryptor? cryptor, IEnumerable<String>? sections, CancellationToken token)
        {
            if (IsCryptKey && !TryEncryptKey(key, cryptor, out key))
            {
                throw new CryptographicException();
            }
            
            if (IsCryptSections && !TryEncryptSections(sections, cryptor, out sections))
            {
                throw new CryptographicException();
            }

            String? value = await GetRawAsync(key, sections, token);

            if (value is not null && IsCryptValue)
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
        
        public virtual Boolean Set(String? key, String? value, IStringCryptor? cryptor, IEnumerable<String>? sections)
        {
            if (IsCryptKey && !TryEncryptKey(key, cryptor, out key))
            {
                throw new CryptographicException();
            }
            
            if (IsCryptValue && !TryEncryptValue(value, cryptor, out value))
            {
                throw new CryptographicException();
            }
            
            if (IsCryptSections && !TryEncryptSections(sections, cryptor, out sections))
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
        
        public virtual Task<Boolean> SetAsync(String? key, String? value, IStringCryptor? cryptor, IEnumerable<String>? sections, CancellationToken token)
        {
            if (IsCryptKey && !TryEncryptKey(key, cryptor, out key))
            {
                throw new CryptographicException();
            }
            
            if (IsCryptValue && !TryEncryptValue(value, cryptor, out value))
            {
                throw new CryptographicException();
            }
            
            if (IsCryptSections && !TryEncryptSections(sections, cryptor, out sections))
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

        public virtual String? GetOrSet(String? key, String? value, IStringCryptor? cryptor, IEnumerable<String>? sections)
        {
            if (IsCryptKey && !TryEncryptKey(key, cryptor, out key))
            {
                throw new CryptographicException();
            }
            
            if (IsCryptValue && !TryEncryptValue(value, cryptor, out value))
            {
                throw new CryptographicException();
            }
            
            if (IsCryptSections && !TryEncryptSections(sections, cryptor, out sections))
            {
                throw new CryptographicException();
            }

            String? raw = GetOrSetRaw(key, value, sections);

            if (raw is not null && IsCryptValue && TryDecryptValue(raw, cryptor, out value))
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

        public virtual async Task<String?> GetOrSetAsync(String? key, String? value, IStringCryptor? cryptor, IEnumerable<String>? sections, CancellationToken token)
        {
            if (IsCryptKey && !TryEncryptKey(key, cryptor, out key))
            {
                throw new CryptographicException();
            }
            
            if (IsCryptValue && !TryEncryptValue(value, cryptor, out value))
            {
                throw new CryptographicException();
            }
            
            if (IsCryptSections && !TryEncryptSections(sections, cryptor, out sections))
            {
                throw new CryptographicException();
            }

            String? raw = await GetOrSetRawAsync(key, value, sections, token);

            if (raw is not null && IsCryptValue && TryDecryptValue(raw, cryptor, out value))
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
        
        public ConfigurationEntry[]? GetExists(IStringCryptor? cryptor)
        {
            ConfigurationEntry[]? entries = GetExistsRaw();

            if (entries is null)
            {
                return null;
            }

            for (Int32 i = 0; i < entries.Length; i++)
            {
                (String? key, ImmutableArray<String> array) = entries[i];

                if (IsCryptKey && !TryDecryptKey(key, cryptor, out key))
                {
                    throw new CryptographicException();
                }

                IEnumerable<String>? sections = array;
                if (IsCryptSections && !TryDecryptSections(sections, cryptor, out sections))
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

        public Task<ConfigurationEntry[]?> GetExistsAsync(CancellationToken token)
        {
            return GetExistsAsync(null, token);
        }

        public async Task<ConfigurationEntry[]?> GetExistsAsync(IStringCryptor? cryptor, CancellationToken token)
        {
            ConfigurationEntry[]? entries = await GetExistsRawAsync(token);

            if (entries is null)
            {
                return null;
            }

            for (Int32 i = 0; i < entries.Length; i++)
            {
                (String? key, ImmutableArray<String> array) = entries[i];

                if (IsCryptKey && !TryDecryptKey(key, cryptor, out key))
                {
                    throw new CryptographicException();
                }

                IEnumerable<String>? sections = array;
                if (IsCryptSections && !TryDecryptSections(sections, cryptor, out sections))
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

        public Boolean Reload()
        {
            return Behavior.Reload();
        }

        public Task<Boolean> ReloadAsync(CancellationToken token)
        {
            return Behavior.ReloadAsync(token);
        }
        
        protected void InvokeChanged(ConfigurationEntry entry)
        {
            Changed?.Invoke(this, entry);
        }
        
        public void Dispose()
        {
            Behavior.Dispose();
            GC.SuppressFinalize(this);
        }
    }
}