// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using NetExtender.Configuration.Common;
using NetExtender.Configuration.Cryptography.Behavior.Interfaces;
using NetExtender.Configuration.Cryptography.Common;
using NetExtender.Configuration.Cryptography.Common.Interfaces;
using NetExtender.Configuration.Cryptography.Interfaces;
using NetExtender.Crypto;
using NetExtender.Crypto.CryptKey.Interfaces;

namespace NetExtender.Configuration.Cryptography
{
    public class CryptographyConfig : Config, ICryptographyConfig, IReadOnlyCryptographyConfig
    {
        protected new ICryptographyConfigBehavior Behavior { get; }

        public IConfigurationCryptor Cryptor
        {
            get
            {
                return Behavior.Cryptor;
            }
        }
        
        public CryptAction Crypt
        {
            get
            {
                return Behavior.Crypt;
            }
        }

        public CryptographyConfigOptions CryptographyOptions
        {
            get
            {
                return Behavior.CryptographyOptions;
            }
        }
        
        public Boolean IsCryptDefault
        {
            get
            {
                return Behavior.IsCryptDefault;
            }
        }
        
        public Boolean IsCryptKey
        {
            get
            {
                return Behavior.IsCryptKey;
            }
        }
        
        public Boolean IsCryptValue
        {
            get
            {
                return Behavior.IsCryptValue;
            }
        }
        
        public Boolean IsCryptSections
        {
            get
            {
                return Behavior.IsCryptSections;
            }
        }
        
        public Boolean IsCryptConfig
        {
            get
            {
                return Behavior.IsCryptConfig;
            }
        }
        
        public Boolean IsCryptAll
        {
            get
            {
                return Behavior.IsCryptAll;
            }
        }

        protected internal CryptographyConfig(ICryptographyConfigBehavior behavior)
            : base(behavior)
        {
            Behavior = behavior ?? throw new ArgumentNullException(nameof(behavior));
        }
        
        public String? GetRawValue(String? key, params String[]? sections)
        {
            return GetRawValue(key, (IEnumerable<String>?) sections);
        }

        public virtual String? GetRawValue(String? key, IEnumerable<String>? sections)
        {
            return Behavior.GetRaw(key, sections);
        }
        
        public String? GetRawValue(String? key, String? alternate, params String[]? sections)
        {
            return GetRawValue(key, alternate, (IEnumerable<String>?) sections);
        }

        public String? GetRawValue(String? key, String? alternate, IEnumerable<String>? sections)
        {
            return GetRawValue(key, sections) ?? alternate;
        }
        
        public String? GetValue(String? key, IStringCryptor? cryptor, params String[]? sections)
        {
            return GetValue(key, cryptor, (IEnumerable<String>?) sections);
        }

        public virtual String? GetValue(String? key, IStringCryptor? cryptor, IEnumerable<String>? sections)
        {
            return Behavior.Get(key, cryptor, sections);
        }

        public String? GetValue(String? key, String? alternate, IStringCryptor? cryptor, params String[]? sections)
        {
            return GetValue(key, alternate, cryptor, (IEnumerable<String>?) sections);
        }

        public String? GetValue(String? key, String? alternate, IStringCryptor? cryptor, IEnumerable<String>? sections)
        {
            return GetValue(key, cryptor, sections) ?? alternate;
        }
        
        public Task<String?> GetRawValueAsync(String? key, params String[]? sections)
        {
            return GetRawValueAsync(key, (IEnumerable<String>?) sections);
        }
        
        public Task<String?> GetRawValueAsync(String? key, IEnumerable<String>? sections)
        {
            return GetRawValueAsync(key, sections, CancellationToken.None);
        }
        
        public Task<String?> GetRawValueAsync(String? key, CancellationToken token, params String[]? sections)
        {
            return GetRawValueAsync(key, sections, token);
        }
        
        public virtual Task<String?> GetRawValueAsync(String? key, IEnumerable<String>? sections, CancellationToken token)
        {
            return Behavior.GetRawAsync(key, sections, token);
        }

        public Task<String?> GetRawValueAsync(String? key, String? alternate, params String[]? sections)
        {
            return GetRawValueAsync(key, alternate, (IEnumerable<String>?) sections);
        }

        public Task<String?> GetRawValueAsync(String? key, String? alternate, IEnumerable<String>? sections)
        {
            return GetRawValueAsync(key, alternate, sections, CancellationToken.None);
        }

        public Task<String?> GetRawValueAsync(String? key, String? alternate, CancellationToken token, params String[]? sections)
        {
            return GetRawValueAsync(key, alternate, sections, token);
        }

        public async Task<String?> GetRawValueAsync(String? key, String? alternate, IEnumerable<String>? sections, CancellationToken token)
        {
            return await GetRawValueAsync(key, sections, token) ?? alternate;
        }

        public Task<String?> GetValueAsync(String? key, IStringCryptor? cryptor, params String[]? sections)
        {
            return GetValueAsync(key, cryptor, (IEnumerable<String>?) sections);
        }

        public Task<String?> GetValueAsync(String? key, IStringCryptor? cryptor, IEnumerable<String>? sections)
        {
            return GetValueAsync(key, cryptor, sections, CancellationToken.None);
        }

        public Task<String?> GetValueAsync(String? key, IStringCryptor? cryptor, CancellationToken token, params String[]? sections)
        {
            return GetValueAsync(key, cryptor, sections, token);
        }

        public virtual async Task<String?> GetValueAsync(String? key, IStringCryptor? cryptor, IEnumerable<String>? sections, CancellationToken token)
        {
            return await Behavior.GetAsync(key, cryptor, sections, token);
        }

        public Task<String?> GetValueAsync(String? key, String? alternate, IStringCryptor? cryptor, params String[]? sections)
        {
            return GetValueAsync(key, alternate, cryptor, (IEnumerable<String>?) sections);
        }

        public Task<String?> GetValueAsync(String? key, String? alternate, IStringCryptor? cryptor, IEnumerable<String>? sections)
        {
            return GetValueAsync(key, alternate, cryptor, sections, CancellationToken.None);
        }

        public Task<String?> GetValueAsync(String? key, String? alternate, IStringCryptor? cryptor, CancellationToken token, params String[]? sections)
        {
            return GetValueAsync(key, alternate, cryptor, sections, token);
        }

        public async Task<String?> GetValueAsync(String? key, String? alternate, IStringCryptor? cryptor, IEnumerable<String>? sections, CancellationToken token)
        {
            return await GetValueAsync(key, cryptor, sections, token) ?? alternate;
        }

        public Boolean SetRawValue(String? key, String? value, params String[]? sections)
        {
            return SetRawValue(key, value, (IEnumerable<String>?) sections);
        }

        public virtual Boolean SetRawValue(String? key, String? value, IEnumerable<String>? sections)
        {
            return Behavior.SetRaw(key, value, sections);
        }
        
        public Boolean SetValue(String? key, String? value, IStringCryptor? cryptor, params String[]? sections)
        {
            return SetValue(key, value, cryptor, (IEnumerable<String>?) sections);
        }

        public virtual Boolean SetValue(String? key, String? value, IStringCryptor? cryptor, IEnumerable<String>? sections)
        {
            return Behavior.Set(key, value, cryptor, sections);
        }
        
        public Task<Boolean> SetRawValueAsync(String? key, String? value, params String[]? sections)
        {
            return SetRawValueAsync(key, value, (IEnumerable<String>?) sections);
        }
        
        public Task<Boolean> SetRawValueAsync(String? key, String? value, IEnumerable<String>? sections)
        {
            return SetRawValueAsync(key, value, sections, CancellationToken.None);
        }
        
        public Task<Boolean> SetRawValueAsync(String? key, String? value, CancellationToken token, params String[]? sections)
        {
            return SetRawValueAsync(key, value, sections, token);
        }
        
        public virtual Task<Boolean> SetRawValueAsync(String? key, String? value, IEnumerable<String>? sections, CancellationToken token)
        {
            return Behavior.SetRawAsync(key, value, sections, token);
        }
        
        public Task<Boolean> SetValueAsync(String? key, String? value, IStringCryptor? cryptor, params String[]? sections)
        {
            return SetValueAsync(key, value, cryptor, (IEnumerable<String>?) sections);
        }
        
        public Task<Boolean> SetValueAsync(String? key, String? value, IStringCryptor? cryptor, IEnumerable<String>? sections)
        {
            return SetValueAsync(key, value, cryptor, sections, CancellationToken.None);
        }
        
        public Task<Boolean> SetValueAsync(String? key, String? value, IStringCryptor? cryptor, CancellationToken token, params String[]? sections)
        {
            return SetValueAsync(key, value, cryptor, sections, token);
        }
        
        public virtual Task<Boolean> SetValueAsync(String? key, String? value, IStringCryptor? cryptor, IEnumerable<String>? sections, CancellationToken token)
        {
            return Behavior.SetAsync(key, value, cryptor, sections, token);
        }
        
        public String? GetOrSetRawValue(String? key, String? value, params String[]? sections)
        {
            return GetOrSetRawValue(key, value, (IEnumerable<String>?) sections);
        }

        public virtual String? GetOrSetRawValue(String? key, String? value, IEnumerable<String>? sections)
        {
            return Behavior.GetOrSetRaw(key, value, sections);
        }
        
        public String? GetOrSetValue(String? key, String? value, IStringCryptor? cryptor, params String[]? sections)
        {
            return GetOrSetValue(key, value, cryptor, (IEnumerable<String>?) sections);
        }

        public virtual String? GetOrSetValue(String? key, String? value, IStringCryptor? cryptor, IEnumerable<String>? sections)
        {
            return Behavior.GetOrSet(key, value, cryptor, sections);
        }
        
        public Task<String?> GetOrSetRawValueAsync(String? key, String? value, params String[]? sections)
        {
            return GetOrSetRawValueAsync(key, value, (IEnumerable<String>?) sections);
        }
        
        public Task<String?> GetOrSetRawValueAsync(String? key, String? value, IEnumerable<String>? sections)
        {
            return GetOrSetRawValueAsync(key, value, sections, CancellationToken.None);
        }
        
        public Task<String?> GetOrSetRawValueAsync(String? key, String? value, CancellationToken token, params String[]? sections)
        {
            return GetOrSetRawValueAsync(key, value, sections, token);
        }
        
        public virtual Task<String?> GetOrSetRawValueAsync(String? key, String? value, IEnumerable<String>? sections, CancellationToken token)
        {
            return Behavior.GetOrSetRawAsync(key, value, sections, token);
        }
        
        public Task<String?> GetOrSetValueAsync(String? key, String? value, IStringCryptor? cryptor, params String[]? sections)
        {
            return GetOrSetValueAsync(key, value, cryptor, (IEnumerable<String>?) sections);
        }

        public Task<String?> GetOrSetValueAsync(String? key, String? value, IStringCryptor? cryptor, IEnumerable<String>? sections)
        {
            return GetOrSetValueAsync(key, value, cryptor, sections, CancellationToken.None);
        }

        public Task<String?> GetOrSetValueAsync(String? key, String? value, IStringCryptor? cryptor, CancellationToken token, params String[]? sections)
        {
            return GetOrSetValueAsync(key, value, cryptor, sections, token);
        }

        public virtual Task<String?> GetOrSetValueAsync(String? key, String? value, IStringCryptor? cryptor, IEnumerable<String>? sections, CancellationToken token)
        {
            return Behavior.GetOrSetAsync(key, value, cryptor, sections, token);
        }

        public Boolean RemoveRawValue(String? key, params String[]? sections)
        {
            return RemoveRawValue(key, (IEnumerable<String>?) sections);
        }

        public Boolean RemoveRawValue(String? key, IEnumerable<String>? sections)
        {
            return SetRawValue(key, null, sections);
        }
        
        public Boolean RemoveValue(String? key, IStringCryptor? cryptor, params String[]? sections)
        {
            return RemoveValue(key, cryptor, (IEnumerable<String>?) sections);
        }

        public Boolean RemoveValue(String? key, IStringCryptor? cryptor, IEnumerable<String>? sections)
        {
            return SetValue(key, null, cryptor, sections);
        }
        
        public Task<Boolean> RemoveRawValueAsync(String? key, params String[]? sections)
        {
            return RemoveRawValueAsync(key, (IEnumerable<String>?) sections);
        }
        
        public Task<Boolean> RemoveRawValueAsync(String? key, IEnumerable<String>? sections)
        {
            return RemoveRawValueAsync(key, sections, CancellationToken.None);
        }
        
        public Task<Boolean> RemoveRawValueAsync(String? key, CancellationToken token, params String[]? sections)
        {
            return RemoveRawValueAsync(key, sections, token);
        }
        
        public Task<Boolean> RemoveRawValueAsync(String? key, IEnumerable<String>? sections, CancellationToken token)
        {
            return SetRawValueAsync(key, null, sections, token);
        }
        
        public Task<Boolean> RemoveValueAsync(String? key, IStringCryptor? cryptor, params String[]? sections)
        {
            return RemoveValueAsync(key, cryptor, (IEnumerable<String>?) sections);
        }

        public Task<Boolean> RemoveValueAsync(String? key, IStringCryptor? cryptor, IEnumerable<String>? sections)
        {
            return RemoveValueAsync(key, cryptor, sections, CancellationToken.None);
        }

        public Task<Boolean> RemoveValueAsync(String? key, IStringCryptor? cryptor, CancellationToken token, params String[]? sections)
        {
            return RemoveValueAsync(key, cryptor, sections, token);
        }

        public Task<Boolean> RemoveValueAsync(String? key, IStringCryptor? cryptor, IEnumerable<String>? sections, CancellationToken token)
        {
            return SetValueAsync(key, null, cryptor, sections, token);
        }

        public Boolean KeyExistRaw(String? key, params String[]? sections)
        {
            return KeyExistRaw(key, (IEnumerable<String>?) sections);
        }

        public virtual Boolean KeyExistRaw(String? key, IEnumerable<String>? sections)
        {
            return Behavior.ContainsRaw(key, sections);
        }
        
        public Boolean KeyExist(String? key, IStringCryptor? cryptor, params String[]? sections)
        {
            return KeyExist(key, cryptor, (IEnumerable<String>?) sections);
        }

        public virtual Boolean KeyExist(String? key, IStringCryptor? cryptor, IEnumerable<String>? sections)
        {
            return Behavior.Contains(key, cryptor, sections);
        }
        
        public Task<Boolean> KeyExistRawAsync(String? key, params String[]? sections)
        {
            return KeyExistRawAsync(key, (IEnumerable<String>?) sections);
        }
        
        public Task<Boolean> KeyExistRawAsync(String? key, IEnumerable<String>? sections)
        {
            return KeyExistRawAsync(key, sections, CancellationToken.None);
        }
        
        public Task<Boolean> KeyExistRawAsync(String? key, CancellationToken token, params String[]? sections)
        {
            return KeyExistRawAsync(key, sections, token);
        }
        
        public virtual Task<Boolean> KeyExistRawAsync(String? key, IEnumerable<String>? sections, CancellationToken token)
        {
            return Behavior.ContainsRawAsync(key, sections, token);
        }

        public Task<Boolean> KeyExistAsync(String? key, IStringCryptor? cryptor, params String[]? sections)
        {
            return KeyExistAsync(key, cryptor, (IEnumerable<String>?) sections);
        }

        public Task<Boolean> KeyExistAsync(String? key, IStringCryptor? cryptor, IEnumerable<String>? sections)
        {
            return KeyExistAsync(key, cryptor, sections, CancellationToken.None);
        }

        public Task<Boolean> KeyExistAsync(String? key, IStringCryptor? cryptor, CancellationToken token, params String[]? sections)
        {
            return KeyExistAsync(key, cryptor, sections, token);
        }

        public virtual Task<Boolean> KeyExistAsync(String? key, IStringCryptor? cryptor, IEnumerable<String>? sections, CancellationToken token)
        {
            return Behavior.ContainsAsync(key, cryptor, sections, token);
        }
        
        public virtual ConfigurationEntry[]? GetExists(IStringCryptor? cryptor)
        {
            return Behavior.GetExists(cryptor);
        }

        public virtual ConfigurationEntry[]? GetExistsRaw()
        {
            return Behavior.GetExistsRaw();
        }
        
        public virtual ConfigurationValueEntry[]? GetExistsValues(IStringCryptor? cryptor)
        {
            return Behavior.GetExistsValues(cryptor);
        }

        public virtual ConfigurationValueEntry[]? GetExistsValuesRaw()
        {
            return Behavior.GetExistsValuesRaw();
        }

        public Task<ConfigurationEntry[]?> GetExistsRawAsync()
        {
            return GetExistsRawAsync(CancellationToken.None);
        }

        public virtual Task<ConfigurationEntry[]?> GetExistsRawAsync(CancellationToken token)
        {
            return Behavior.GetExistsRawAsync(token);
        }

        public Task<ConfigurationEntry[]?> GetExistsAsync(IStringCryptor? cryptor)
        {
            return GetExistsAsync(cryptor, CancellationToken.None);
        }

        public virtual Task<ConfigurationEntry[]?> GetExistsAsync(IStringCryptor? cryptor, CancellationToken token)
        {
            return Behavior.GetExistsAsync(cryptor, token);
        }
        
        public Task<ConfigurationValueEntry[]?> GetExistsValuesRawAsync()
        {
            return GetExistsValuesRawAsync(CancellationToken.None);
        }

        public virtual Task<ConfigurationValueEntry[]?> GetExistsValuesRawAsync(CancellationToken token)
        {
            return Behavior.GetExistsValuesRawAsync(token);
        }

        public Task<ConfigurationValueEntry[]?> GetExistsValuesAsync(IStringCryptor? cryptor)
        {
            return GetExistsValuesAsync(cryptor, CancellationToken.None);
        }

        public virtual Task<ConfigurationValueEntry[]?> GetExistsValuesAsync(IStringCryptor? cryptor, CancellationToken token)
        {
            return Behavior.GetExistsValuesAsync(cryptor, token);
        }
    }
}