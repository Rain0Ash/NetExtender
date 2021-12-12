// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using NetExtender.Configuration.Common;
using NetExtender.Configuration.Cryptography.Common.Interfaces;
using NetExtender.Configuration.Interfaces;

namespace NetExtender.Configuration.Cryptography.Interfaces
{
    public interface ICryptographyConfig : IConfig, IConfigurationCryptographyInfo
    {
        public IConfigurationCryptor Cryptor { get; }

        public String? GetValue(String? key, IConfigurationCryptor? cryptor, params String[]? sections);
        public String? GetValue(String? key, IConfigurationCryptor? cryptor, IEnumerable<String>? sections);
        public String? GetValue(String? key, String? alternate, IConfigurationCryptor? cryptor, params String[]? sections);
        public String? GetValue(String? key, String? alternate, IConfigurationCryptor? cryptor, IEnumerable<String>? sections);
        public String? GetRawValue(String? key, params String[]? sections);
        public String? GetRawValue(String? key, IEnumerable<String>? sections);
        public String? GetRawValue(String? key, String? alternate, params String[]? sections);
        public String? GetRawValue(String? key, String? alternate, IEnumerable<String>? sections);
        public Task<String?> GetValueAsync(String? key, IConfigurationCryptor? cryptor, params String[]? sections);
        public Task<String?> GetValueAsync(String? key, IConfigurationCryptor? cryptor, IEnumerable<String>? sections);
        public Task<String?> GetValueAsync(String? key, IConfigurationCryptor? cryptor, CancellationToken token, params String[]? sections);
        public Task<String?> GetValueAsync(String? key, IConfigurationCryptor? cryptor, IEnumerable<String>? sections, CancellationToken token);
        public Task<String?> GetValueAsync(String? key, String? alternate, IConfigurationCryptor? cryptor, params String[]? sections);
        public Task<String?> GetValueAsync(String? key, String? alternate, IConfigurationCryptor? cryptor, IEnumerable<String>? sections);
        public Task<String?> GetValueAsync(String? key, String? alternate, IConfigurationCryptor? cryptor, CancellationToken token, params String[]? sections);
        public Task<String?> GetValueAsync(String? key, String? alternate, IConfigurationCryptor? cryptor, IEnumerable<String>? sections, CancellationToken token);
        public Task<String?> GetRawValueAsync(String? key, params String[]? sections);
        public Task<String?> GetRawValueAsync(String? key, IEnumerable<String>? sections);
        public Task<String?> GetRawValueAsync(String? key, CancellationToken token, params String[]? sections);
        public Task<String?> GetRawValueAsync(String? key, IEnumerable<String>? sections, CancellationToken token);
        public Task<String?> GetRawValueAsync(String? key, String? alternate, params String[]? sections);
        public Task<String?> GetRawValueAsync(String? key, String? alternate, IEnumerable<String>? sections);
        public Task<String?> GetRawValueAsync(String? key, String? alternate, CancellationToken token, params String[]? sections);
        public Task<String?> GetRawValueAsync(String? key, String? alternate, IEnumerable<String>? sections, CancellationToken token);
        public Boolean SetValue(String? key, String? value, IConfigurationCryptor? cryptor, params String[]? sections);
        public Boolean SetValue(String? key, String? value, IConfigurationCryptor? cryptor, IEnumerable<String>? sections);
        public Boolean SetRawValue(String? key, String? value, params String[]? sections);
        public Boolean SetRawValue(String? key, String? value, IEnumerable<String>? sections);
        public Task<Boolean> SetValueAsync(String? key, String? value, IConfigurationCryptor? cryptor, params String[]? sections);
        public Task<Boolean> SetValueAsync(String? key, String? value, IConfigurationCryptor? cryptor, IEnumerable<String>? sections);
        public Task<Boolean> SetValueAsync(String? key, String? value, IConfigurationCryptor? cryptor, CancellationToken token, params String[]? sections);
        public Task<Boolean> SetValueAsync(String? key, String? value, IConfigurationCryptor? cryptor, IEnumerable<String>? sections, CancellationToken token);
        public Task<Boolean> SetRawValueAsync(String? key, String? value, params String[]? sections);
        public Task<Boolean> SetRawValueAsync(String? key, String? value, IEnumerable<String>? sections);
        public Task<Boolean> SetRawValueAsync(String? key, String? value, CancellationToken token, params String[]? sections);
        public Task<Boolean> SetRawValueAsync(String? key, String? value, IEnumerable<String>? sections, CancellationToken token);
        public String? GetOrSetValue(String? key, String? value, IConfigurationCryptor? cryptor, params String[]? sections);
        public String? GetOrSetValue(String? key, String? value, IConfigurationCryptor? cryptor, IEnumerable<String>? sections);
        public String? GetOrSetRawValue(String? key, String? value, params String[]? sections);
        public String? GetOrSetRawValue(String? key, String? value, IEnumerable<String>? sections);
        public Task<String?> GetOrSetValueAsync(String? key, String? value, IConfigurationCryptor? cryptor, params String[]? sections);
        public Task<String?> GetOrSetValueAsync(String? key, String? value, IConfigurationCryptor? cryptor, IEnumerable<String>? sections);
        public Task<String?> GetOrSetValueAsync(String? key, String? value, IConfigurationCryptor? cryptor, CancellationToken token, params String[]? sections);
        public Task<String?> GetOrSetValueAsync(String? key, String? value, IConfigurationCryptor? cryptor, IEnumerable<String>? sections, CancellationToken token);
        public Task<String?> GetOrSetRawValueAsync(String? key, String? value, params String[]? sections);
        public Task<String?> GetOrSetRawValueAsync(String? key, String? value, IEnumerable<String>? sections);
        public Task<String?> GetOrSetRawValueAsync(String? key, String? value, CancellationToken token, params String[]? sections);
        public Task<String?> GetOrSetRawValueAsync(String? key, String? value, IEnumerable<String>? sections, CancellationToken token);
        public Boolean RemoveValue(String? key, IConfigurationCryptor? cryptor, params String[]? sections);
        public Boolean RemoveValue(String? key, IConfigurationCryptor? cryptor, IEnumerable<String>? sections);
        public Boolean RemoveRawValue(String? key, params String[]? sections);
        public Boolean RemoveRawValue(String? key, IEnumerable<String>? sections);
        public Task<Boolean> RemoveValueAsync(String? key, IConfigurationCryptor? cryptor, params String[]? sections);
        public Task<Boolean> RemoveValueAsync(String? key, IConfigurationCryptor? cryptor, IEnumerable<String>? sections);
        public Task<Boolean> RemoveValueAsync(String? key, IConfigurationCryptor? cryptor, CancellationToken token, params String[]? sections);
        public Task<Boolean> RemoveValueAsync(String? key, IConfigurationCryptor? cryptor, IEnumerable<String>? sections, CancellationToken token);
        public Task<Boolean> RemoveRawValueAsync(String? key, params String[]? sections);
        public Task<Boolean> RemoveRawValueAsync(String? key, IEnumerable<String>? sections);
        public Task<Boolean> RemoveRawValueAsync(String? key, CancellationToken token, params String[]? sections);
        public Task<Boolean> RemoveRawValueAsync(String? key, IEnumerable<String>? sections, CancellationToken token);
        public Boolean KeyExist(String? key, IConfigurationCryptor? cryptor, params String[]? sections);
        public Boolean KeyExist(String? key, IConfigurationCryptor? cryptor, IEnumerable<String>? sections);
        public Boolean KeyExistRaw(String? key, params String[]? sections);
        public Boolean KeyExistRaw(String? key, IEnumerable<String>? sections);
        public Task<Boolean> KeyExistAsync(String? key, IConfigurationCryptor? cryptor, params String[]? sections);
        public Task<Boolean> KeyExistAsync(String? key, IConfigurationCryptor? cryptor, IEnumerable<String>? sections);
        public Task<Boolean> KeyExistAsync(String? key, IConfigurationCryptor? cryptor, CancellationToken token, params String[]? sections);
        public Task<Boolean> KeyExistAsync(String? key, IConfigurationCryptor? cryptor, IEnumerable<String>? sections, CancellationToken token);
        public Task<Boolean> KeyExistRawAsync(String? key, params String[]? sections);
        public Task<Boolean> KeyExistRawAsync(String? key, IEnumerable<String>? sections);
        public Task<Boolean> KeyExistRawAsync(String? key, CancellationToken token, params String[]? sections);
        public Task<Boolean> KeyExistRawAsync(String? key, IEnumerable<String>? sections, CancellationToken token);
        public ConfigurationEntry[]? GetExists(IConfigurationCryptor? cryptor);
        public ConfigurationEntry[]? GetExistsRaw();
        public ConfigurationValueEntry[]? GetExistsValues(IConfigurationCryptor? cryptor);
        public ConfigurationValueEntry[]? GetExistsValuesRaw();
        public Task<ConfigurationEntry[]?> GetExistsAsync(IConfigurationCryptor? cryptor);
        public Task<ConfigurationEntry[]?> GetExistsAsync(IConfigurationCryptor? cryptor, CancellationToken token);
        public Task<ConfigurationEntry[]?> GetExistsRawAsync();
        public Task<ConfigurationEntry[]?> GetExistsRawAsync(CancellationToken token);
        public Task<ConfigurationValueEntry[]?> GetExistsValuesAsync(IConfigurationCryptor? cryptor);
        public Task<ConfigurationValueEntry[]?> GetExistsValuesAsync(IConfigurationCryptor? cryptor, CancellationToken token);
        public Task<ConfigurationValueEntry[]?> GetExistsValuesRawAsync();
        public Task<ConfigurationValueEntry[]?> GetExistsValuesRawAsync(CancellationToken token);
    }
}