// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Security.Cryptography;
using System.Threading;
using System.Threading.Tasks;
using NetExtender.Configuration.Behavior.Interfaces;
using NetExtender.Configuration.Behavior.Transactions.Interfaces;
using NetExtender.Configuration.Common;
using NetExtender.Configuration.Cryptography.Behavior.Interfaces;
using NetExtender.Configuration.Cryptography.Behavior.Transactions;
using NetExtender.Configuration.Cryptography.Behavior.Transactions.Interfaces;
using NetExtender.Configuration.Cryptography.Common;
using NetExtender.Configuration.Cryptography.Common.Interfaces;
using NetExtender.Configuration.Cryptography.Utilities;
using NetExtender.Configuration.Memory;
using NetExtender.Crypto;
using NetExtender.Crypto.CryptKey.Interfaces;
using NetExtender.Utilities.Crypto;
using NetExtender.Utilities.Types;

namespace NetExtender.Configuration.Cryptography.Behavior
{
    public class CryptographyConfigBehavior : ICryptographyConfigBehavior
    {
        private IConfigBehavior Behavior { get; }
        
        public IConfigurationCryptor Cryptor { get; }

        public event ConfigurationChangedEventHandler? Changed;

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

        public Boolean IsThreadSafe
        {
            get
            {
                return Behavior.IsThreadSafe;
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

        public CryptographyConfigBehavior(IConfigBehavior behavior, IStringCryptor cryptor)
        {
            if (cryptor is null)
            {
                throw new ArgumentNullException(nameof(cryptor));
            }

            Behavior = behavior ?? throw new ArgumentNullException(nameof(behavior));
            Cryptor = cryptor.AsCryptor();
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

        [return: NotNullIfNotNull("entries")]
        // ReSharper disable once CognitiveComplexity
        protected virtual IEnumerable<ConfigurationValueEntry>? Encrypt(IStringCryptor? cryptor, IEnumerable<ConfigurationValueEntry>? entries)
        {
            if (entries is null)
            {
                return null;
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            IEnumerable<ConfigurationValueEntry> Internal()
            {
                IConfigurationCryptor? configuration = cryptor?.AsCryptor(Cryptor.CryptographyOptions);
                
                foreach (ConfigurationValueEntry entry in entries)
                {
                    (String? key, String? value, ImmutableArray<String> array) = entry;
                    IEnumerable<String>? sections = array;
                    
                    if ((configuration?.IsCryptKey ?? IsCryptKey) && !TryEncryptKey(key, configuration, out key))
                    {
                        throw new CryptographicException();
                    }
                
                    if ((configuration?.IsCryptValue ?? IsCryptValue) && !TryEncryptValue(value, configuration, out value))
                    {
                        throw new CryptographicException();
                    }

                    if ((configuration?.IsCryptSections ?? IsCryptSections) && !TryEncryptSections(sections, configuration, out sections))
                    {
                        throw new CryptographicException();
                    }

                    yield return new ConfigurationValueEntry(key, value, sections);
                }
            }

            return Internal();
        }
        
        [return: NotNullIfNotNull("entries")]
        // ReSharper disable once CognitiveComplexity
        protected virtual IEnumerable<ConfigurationValueEntry>? Decrypt(IStringCryptor? cryptor, IEnumerable<ConfigurationValueEntry>? entries)
        {
            if (entries is null)
            {
                return null;
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            IEnumerable<ConfigurationValueEntry> Internal()
            {
                IConfigurationCryptor? configuration = cryptor?.AsCryptor(Cryptor.CryptographyOptions);
                
                foreach (ConfigurationValueEntry entry in entries)
                {
                    (String? key, String? value, ImmutableArray<String> array) = entry;
                    IEnumerable<String>? sections = array;
                    
                    if ((configuration?.IsCryptKey ?? IsCryptKey) && !TryDecryptKey(key, configuration, out key))
                    {
                        throw new CryptographicException();
                    }
                
                    if ((configuration?.IsCryptValue ?? IsCryptValue) && !TryDecryptValue(value, configuration, out value))
                    {
                        throw new CryptographicException();
                    }

                    if ((configuration?.IsCryptSections ?? IsCryptSections) && !TryDecryptSections(sections, configuration, out sections))
                    {
                        throw new CryptographicException();
                    }

                    yield return new ConfigurationValueEntry(key, value, sections);
                }
            }

            return Internal();
        }

        public Boolean Contains(String? key, IEnumerable<String>? sections)
        {
            return Contains(key, null, sections);
        }
        
        public virtual Boolean Contains(String? key, IStringCryptor? cryptor, IEnumerable<String>? sections)
        {
            IConfigurationCryptor? configuration = cryptor?.AsCryptor(Cryptor.CryptographyOptions);
            
            if ((configuration?.IsCryptKey ?? IsCryptKey) && !TryEncryptKey(key, configuration, out key))
            {
                throw new CryptographicException();
            }

            if ((configuration?.IsCryptSections ?? IsCryptSections) && !TryEncryptSections(sections, configuration, out sections))
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
            IConfigurationCryptor? configuration = cryptor?.AsCryptor(Cryptor.CryptographyOptions);
            
            if ((configuration?.IsCryptKey ?? IsCryptKey) && !TryEncryptKey(key, configuration, out key))
            {
                throw new CryptographicException();
            }

            if ((configuration?.IsCryptSections ?? IsCryptSections) && !TryEncryptSections(sections, configuration, out sections))
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
            IConfigurationCryptor? configuration = cryptor?.AsCryptor(Cryptor.CryptographyOptions);
            
            if ((configuration?.IsCryptKey ?? IsCryptKey) && !TryEncryptKey(key, configuration, out key))
            {
                throw new CryptographicException();
            }
            
            if ((configuration?.IsCryptSections ?? IsCryptSections) && !TryEncryptSections(sections, configuration, out sections))
            {
                throw new CryptographicException();
            }

            String? value = GetRaw(key, sections);

            if (value is not null && (configuration?.IsCryptValue ?? IsCryptValue))
            {
                return TryDecryptValue(value, configuration, out String? result) ? result : value;
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
            IConfigurationCryptor? configuration = cryptor?.AsCryptor(Cryptor.CryptographyOptions);
            
            if ((configuration?.IsCryptKey ?? IsCryptKey) && !TryEncryptKey(key, configuration, out key))
            {
                throw new CryptographicException();
            }
            
            if ((configuration?.IsCryptSections ?? IsCryptSections) && !TryEncryptSections(sections, configuration, out sections))
            {
                throw new CryptographicException();
            }

            String? value = await GetRawAsync(key, sections, token);

            if (value is not null && (configuration?.IsCryptValue ?? IsCryptValue))
            {
                return TryDecryptValue(value, configuration, out String? result) ? result : value;
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
            IConfigurationCryptor? configuration = cryptor?.AsCryptor(Cryptor.CryptographyOptions);
            
            if ((configuration?.IsCryptKey ?? IsCryptKey) && !TryEncryptKey(key, configuration, out key))
            {
                throw new CryptographicException();
            }
            
            if ((configuration?.IsCryptValue ?? IsCryptValue) && !TryEncryptValue(value, configuration, out value))
            {
                throw new CryptographicException();
            }
            
            if ((configuration?.IsCryptSections ?? IsCryptSections) && !TryEncryptSections(sections, configuration, out sections))
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
            IConfigurationCryptor? configuration = cryptor?.AsCryptor(Cryptor.CryptographyOptions);
            
            if ((configuration?.IsCryptKey ?? IsCryptKey) && !TryEncryptKey(key, configuration, out key))
            {
                throw new CryptographicException();
            }
            
            if ((configuration?.IsCryptValue ?? IsCryptValue) && !TryEncryptValue(value, configuration, out value))
            {
                throw new CryptographicException();
            }
            
            if ((configuration?.IsCryptSections ?? IsCryptSections) && !TryEncryptSections(sections, configuration, out sections))
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
            IConfigurationCryptor? configuration = cryptor?.AsCryptor(Cryptor.CryptographyOptions);
            
            if ((configuration?.IsCryptKey ?? IsCryptKey) && !TryEncryptKey(key, configuration, out key))
            {
                throw new CryptographicException();
            }
            
            if ((configuration?.IsCryptValue ?? IsCryptValue) && !TryEncryptValue(value, configuration, out value))
            {
                throw new CryptographicException();
            }
            
            if ((configuration?.IsCryptSections ?? IsCryptSections) && !TryEncryptSections(sections, configuration, out sections))
            {
                throw new CryptographicException();
            }

            String? raw = GetOrSetRaw(key, value, sections);

            if (raw is not null && (configuration?.IsCryptValue ?? IsCryptValue) && TryDecryptValue(raw, configuration, out value))
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
            IConfigurationCryptor? configuration = cryptor?.AsCryptor(Cryptor.CryptographyOptions);
            
            if ((configuration?.IsCryptKey ?? IsCryptKey) && !TryEncryptKey(key, configuration, out key))
            {
                throw new CryptographicException();
            }
            
            if ((configuration?.IsCryptValue ?? IsCryptValue) && !TryEncryptValue(value, configuration, out value))
            {
                throw new CryptographicException();
            }
            
            if ((configuration?.IsCryptSections ?? IsCryptSections) && !TryEncryptSections(sections, configuration, out sections))
            {
                throw new CryptographicException();
            }

            String? raw = await GetOrSetRawAsync(key, value, sections, token);

            if (raw is not null && (configuration?.IsCryptValue ?? IsCryptValue) && TryDecryptValue(raw, configuration, out value))
            {
                return value;
            }

            return raw;
        }

        public Task<String?> GetOrSetRawAsync(String? key, String? value, IEnumerable<String>? sections, CancellationToken token)
        {
            return Behavior.GetOrSetAsync(key, value, sections, token);
        }

        public ConfigurationEntry[]? GetExists(IEnumerable<String>? sections)
        {
            return GetExists(null, sections);
        }
        
        // ReSharper disable once CognitiveComplexity
        public virtual ConfigurationEntry[]? GetExists(IStringCryptor? cryptor, IEnumerable<String>? sections)
        {
            IConfigurationCryptor? configuration = cryptor?.AsCryptor(Cryptor.CryptographyOptions);
            
            if (sections is not null)
            {
                if ((configuration?.IsCryptSections ?? IsCryptSections) && !TryEncryptSections(sections, configuration, out sections))
                {
                    throw new CryptographicException();
                }
            }
            
            ConfigurationEntry[]? entries = GetExistsRaw(sections);

            if (entries is null)
            {
                return null;
            }
            
            for (Int32 i = 0; i < entries.Length; i++)
            {
                (String? key, ImmutableArray<String> array) = entries[i];

                if ((configuration?.IsCryptKey ?? IsCryptKey) && !TryDecryptKey(key, configuration, out key))
                {
                    throw new CryptographicException();
                }

                sections = array;
                if ((configuration?.IsCryptSections ?? IsCryptSections) && !TryDecryptSections(sections, configuration, out sections))
                {
                    throw new CryptographicException();
                }
                
                entries[i] = new ConfigurationEntry(key, sections.AsImmutableArray());
            }

            return entries;
        }

        public ConfigurationEntry[]? GetExistsRaw(IEnumerable<String>? sections)
        {
            return Behavior.GetExists(sections);
        }
        
        public ConfigurationValueEntry[]? GetExistsValues(IEnumerable<String>? sections)
        {
            return GetExistsValues(null, sections);
        }
        
        // ReSharper disable once CognitiveComplexity
        public virtual ConfigurationValueEntry[]? GetExistsValues(IStringCryptor? cryptor, IEnumerable<String>? sections)
        {
            IConfigurationCryptor? configuration = cryptor?.AsCryptor(Cryptor.CryptographyOptions);
            
            if (sections is not null)
            {
                if ((configuration?.IsCryptSections ?? IsCryptSections) && !TryEncryptSections(sections, configuration, out sections))
                {
                    throw new CryptographicException();
                }
            }
            
            ConfigurationValueEntry[]? entries = GetExistsValuesRaw(sections);

            if (entries is null)
            {
                return null;
            }
            
            for (Int32 i = 0; i < entries.Length; i++)
            {
                (String? key, String? value, ImmutableArray<String> array) = entries[i];

                if ((configuration?.IsCryptKey ?? IsCryptKey) && !TryDecryptKey(key, configuration, out key))
                {
                    throw new CryptographicException();
                }
                
                if ((configuration?.IsCryptValue ?? IsCryptValue) && !TryDecryptValue(value, configuration, out value))
                {
                    throw new CryptographicException();
                }

                sections = array;
                if ((configuration?.IsCryptSections ?? IsCryptSections) && !TryDecryptSections(sections, configuration, out sections))
                {
                    throw new CryptographicException();
                }
                
                entries[i] = new ConfigurationValueEntry(key, value, sections.AsImmutableArray());
            }

            return entries;
        }

        public ConfigurationValueEntry[]? GetExistsValuesRaw(IEnumerable<String>? sections)
        {
            return Behavior.GetExistsValues(sections);
        }

        public Task<ConfigurationEntry[]?> GetExistsAsync(IEnumerable<String>? sections, CancellationToken token)
        {
            return GetExistsAsync(null, sections, token);
        }

        // ReSharper disable once CognitiveComplexity
        public async virtual Task<ConfigurationEntry[]?> GetExistsAsync(IStringCryptor? cryptor, IEnumerable<String>? sections, CancellationToken token)
        {
            IConfigurationCryptor? configuration = cryptor?.AsCryptor(Cryptor.CryptographyOptions);
            
            if (sections is not null)
            {
                if ((configuration?.IsCryptSections ?? IsCryptSections) && !TryEncryptSections(sections, configuration, out sections))
                {
                    throw new CryptographicException();
                }
            }
            
            ConfigurationEntry[]? entries = await GetExistsRawAsync(sections, token);

            if (entries is null)
            {
                return null;
            }

            for (Int32 i = 0; i < entries.Length; i++)
            {
                (String? key, ImmutableArray<String> array) = entries[i];

                if ((configuration?.IsCryptKey ?? IsCryptKey) && !TryDecryptKey(key, configuration, out key))
                {
                    throw new CryptographicException();
                }

                sections = array;
                if ((configuration?.IsCryptSections ?? IsCryptSections) && !TryDecryptSections(sections, configuration, out sections))
                {
                    throw new CryptographicException();
                }
                
                entries[i] = new ConfigurationEntry(key, sections.AsImmutableArray());
            }

            return entries;
        }

        public Task<ConfigurationEntry[]?> GetExistsRawAsync(IEnumerable<String>? sections, CancellationToken token)
        {
            return Behavior.GetExistsAsync(sections, token);
        }
        
        public Task<ConfigurationValueEntry[]?> GetExistsValuesAsync(IEnumerable<String>? sections, CancellationToken token)
        {
            return GetExistsValuesAsync(null, sections, token);
        }

        // ReSharper disable once CognitiveComplexity
        public virtual async Task<ConfigurationValueEntry[]?> GetExistsValuesAsync(IStringCryptor? cryptor, IEnumerable<String>? sections, CancellationToken token)
        {
            IConfigurationCryptor? configuration = cryptor?.AsCryptor(Cryptor.CryptographyOptions);
            
            if (sections is not null)
            {
                if ((configuration?.IsCryptSections ?? IsCryptSections) && !TryEncryptSections(sections, configuration, out sections))
                {
                    throw new CryptographicException();
                }
            }
            
            ConfigurationValueEntry[]? entries = await GetExistsValuesRawAsync(sections, token);

            if (entries is null)
            {
                return null;
            }

            for (Int32 i = 0; i < entries.Length; i++)
            {
                (String? key, String? value, ImmutableArray<String> array) = entries[i];

                if ((configuration?.IsCryptKey ?? IsCryptKey) && !TryDecryptKey(key, configuration, out key))
                {
                    throw new CryptographicException();
                }
                
                if ((configuration?.IsCryptValue ?? IsCryptValue) && !TryDecryptValue(value, configuration, out value))
                {
                    throw new CryptographicException();
                }

                sections = array;
                if ((configuration?.IsCryptSections ?? IsCryptSections) && !TryDecryptSections(sections, configuration, out sections))
                {
                    throw new CryptographicException();
                }
                
                entries[i] = new ConfigurationValueEntry(key, value, sections.AsImmutableArray());
            }

            return entries;
        }

        public Task<ConfigurationValueEntry[]?> GetExistsValuesRawAsync(IEnumerable<String>? sections, CancellationToken token)
        {
            return Behavior.GetExistsValuesAsync(sections, token);
        }

        public Boolean Clear(IEnumerable<String>? sections)
        {
            return Clear(null, sections);
        }
        
        public virtual Boolean Clear(IStringCryptor? cryptor, IEnumerable<String>? sections)
        {
            if (sections is null)
            {
                return Behavior.Clear(sections);
            }

            IConfigurationCryptor? configuration = cryptor?.AsCryptor(Cryptor.CryptographyOptions);
            
            if ((configuration?.IsCryptSections ?? IsCryptSections) && !TryEncryptSections(sections, configuration, out sections))
            {
                throw new CryptographicException();
            }

            return Behavior.Clear(sections);
        }
        
        public Boolean ClearRaw(IEnumerable<String>? sections)
        {
            return Behavior.Clear(sections);
        }

        public Task<Boolean> ClearAsync(IEnumerable<String>? sections, CancellationToken token)
        {
            return ClearAsync(null, sections, token);
        }

        public virtual Task<Boolean> ClearAsync(IStringCryptor? cryptor, IEnumerable<String>? sections, CancellationToken token)
        {
            if (sections is null)
            {
                return Behavior.ClearAsync(sections, token);
            }

            IConfigurationCryptor? configuration = cryptor?.AsCryptor(Cryptor.CryptographyOptions);
            
            if ((configuration?.IsCryptSections ?? IsCryptSections) && !TryEncryptSections(sections, configuration, out sections))
            {
                throw new CryptographicException();
            }

            return Behavior.ClearAsync(sections, token);
        }

        public Task<Boolean> ClearRawAsync(IEnumerable<String>? sections, CancellationToken token)
        {
            return Behavior.ClearAsync(sections, token);
        }

        public Boolean Reload()
        {
            return Behavior.Reload();
        }

        public Task<Boolean> ReloadAsync(CancellationToken token)
        {
            return Behavior.ReloadAsync(token);
        }
        
        public Boolean Reset()
        {
            return Behavior.Reset();
        }

        public Task<Boolean> ResetAsync(CancellationToken token)
        {
            return Behavior.ResetAsync(token);
        }

        public Boolean Merge(IEnumerable<ConfigurationValueEntry>? entries)
        {
            return Merge(null, entries);
        }
        
        public Boolean Merge(IStringCryptor? cryptor, IEnumerable<ConfigurationValueEntry>? entries)
        {
            return Behavior.Merge(Encrypt(cryptor, entries));
        }
        
        public Boolean MergeRaw(IEnumerable<ConfigurationValueEntry>? entries)
        {
            return Behavior.Merge(entries);
        }

        public Task<Boolean> MergeAsync(IEnumerable<ConfigurationValueEntry>? entries, CancellationToken token)
        {
            return MergeAsync(null, entries, token);
        }
        
        public Task<Boolean> MergeAsync(IStringCryptor? cryptor, IEnumerable<ConfigurationValueEntry>? entries, CancellationToken token)
        {
            return Behavior.MergeAsync(Encrypt(cryptor, entries), token);
        }
        
        public Task<Boolean> MergeRawAsync(IEnumerable<ConfigurationValueEntry>? entries, CancellationToken token)
        {
            return Behavior.MergeAsync(entries, token);
        }

        public Boolean Replace(IEnumerable<ConfigurationValueEntry>? entries)
        {
            return Replace(null, entries);
        }

        public Boolean Replace(IStringCryptor? cryptor, IEnumerable<ConfigurationValueEntry>? entries)
        {
            return Behavior.Replace(Encrypt(cryptor, entries));
        }

        public Boolean ReplaceRaw(IEnumerable<ConfigurationValueEntry>? entries)
        {
            return Behavior.Replace(entries);
        }
        
        public Task<Boolean> ReplaceAsync(IEnumerable<ConfigurationValueEntry>? entries, CancellationToken token)
        {
            return ReplaceAsync(null, entries, token);
        }
        
        public Task<Boolean> ReplaceAsync(IStringCryptor? cryptor, IEnumerable<ConfigurationValueEntry>? entries, CancellationToken token)
        {
            return Behavior.ReplaceAsync(Encrypt(cryptor, entries), token);
        }
        
        public Task<Boolean> ReplaceRawAsync(IEnumerable<ConfigurationValueEntry>? entries, CancellationToken token)
        {
            return Behavior.ReplaceAsync(entries, token);
        }
        
        public ConfigurationValueEntry[]? Difference(IEnumerable<ConfigurationValueEntry>? entries)
        {
            return Difference(null, entries);
        }
        
        public ConfigurationValueEntry[]? Difference(IStringCryptor? cryptor, IEnumerable<ConfigurationValueEntry>? entries)
        {
            return Decrypt(cryptor, Behavior.Difference(Encrypt(cryptor, entries)))?.ToArray();
        }
        
        public ConfigurationValueEntry[]? DifferenceRaw(IEnumerable<ConfigurationValueEntry>? entries)
        {
            return Behavior.Difference(entries);
        }
        
        public Task<ConfigurationValueEntry[]?> DifferenceAsync(IEnumerable<ConfigurationValueEntry>? entries, CancellationToken token)
        {
            return DifferenceAsync(null, entries, token);
        }
        
        public async Task<ConfigurationValueEntry[]?> DifferenceAsync(IStringCryptor? cryptor, IEnumerable<ConfigurationValueEntry>? entries, CancellationToken token)
        {
            return Decrypt(cryptor, await Behavior.DifferenceAsync(Encrypt(cryptor, entries), token))?.ToArray();
        }
        
        public Task<ConfigurationValueEntry[]?> DifferenceRawAsync(IEnumerable<ConfigurationValueEntry>? entries, CancellationToken token)
        {
            return Behavior.DifferenceAsync(entries, token);
        }
        
        IConfigBehaviorTransaction? IConfigBehavior.Transaction()
        {
            return Transaction();
        }

        async Task<IConfigBehaviorTransaction?> IConfigBehavior.TransactionAsync(CancellationToken token)
        {
            return await TransactionAsync(token);
        }

        public ICryptographyConfigBehaviorTransaction? Transaction()
        {
            return Transaction(null);
        }

        public virtual ICryptographyConfigBehaviorTransaction? Transaction(IStringCryptor? cryptor)
        {
            if (IsReadOnly)
            {
                return null;
            }
            
            ConfigurationValueEntry[]? entries = GetExistsValues(null);
            
            ICryptographyConfigBehavior transaction = new MemoryConfigBehavior(ConfigOptions.IgnoreEvent).Cryptography(cryptor ?? Cryptor);

            transaction.Merge(entries);
            return new CryptographyConfigBehaviorTransaction(this, transaction);
        }

        public Task<ICryptographyConfigBehaviorTransaction?> TransactionAsync(CancellationToken token)
        {
            return TransactionAsync(null, token);
        }

        public virtual async Task<ICryptographyConfigBehaviorTransaction?> TransactionAsync(IStringCryptor? cryptor, CancellationToken token)
        {
            if (IsReadOnly)
            {
                return null;
            }
            
            ConfigurationValueEntry[]? entries = await GetExistsValuesAsync(null, token);
            
            ICryptographyConfigBehavior transaction = new MemoryConfigBehavior(ConfigOptions.IgnoreEvent).Cryptography(cryptor ?? Cryptor);

            await transaction.MergeAsync(entries, token);
            return new CryptographyConfigBehaviorTransaction(this, transaction);
        }

        protected void InvokeChanged(ConfigurationValueEntry entry)
        {
            Changed?.Invoke(this, new ConfigurationChangedEventArgs(entry));
        }
        
        public IEnumerator<ConfigurationValueEntry> GetEnumerator()
        {
            return Decrypt(null, Behavior).GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
        
        public void Dispose()
        {
            Behavior.Dispose();
            GC.SuppressFinalize(this);
        }

        public async ValueTask DisposeAsync()
        {
            await Behavior.DisposeAsync();
            GC.SuppressFinalize(this);
        }
    }
}