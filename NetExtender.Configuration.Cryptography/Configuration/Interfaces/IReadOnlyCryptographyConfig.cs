// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using NetExtender.Configuration.Common;
using NetExtender.Configuration.Cryptography.Common.Interfaces;
using NetExtender.Configuration.Interfaces;
using NetExtender.Crypto.CryptKey.Interfaces;

namespace NetExtender.Configuration.Cryptography.Interfaces
{
    public interface IReadOnlyCryptographyConfig : IReadOnlyConfig, IConfigurationCryptographyInfo
    {
        public IConfigurationCryptor Cryptor { get; }

        public String? GetValue(String? key, IStringCryptor? cryptor, params String[]? sections);
        public String? GetValue(String? key, IStringCryptor? cryptor, IEnumerable<String>? sections);
        public String? GetValue(String? key, String? alternate, IStringCryptor? cryptor, params String[]? sections);
        public String? GetValue(String? key, String? alternate, IStringCryptor? cryptor, IEnumerable<String>? sections);
        public String? GetRawValue(String? key, params String[]? sections);
        public String? GetRawValue(String? key, IEnumerable<String>? sections);
        public String? GetRawValue(String? key, String? alternate, params String[]? sections);
        public String? GetRawValue(String? key, String? alternate, IEnumerable<String>? sections);
        public Task<String?> GetValueAsync(String? key, IStringCryptor? cryptor, params String[]? sections);
        public Task<String?> GetValueAsync(String? key, IStringCryptor? cryptor, IEnumerable<String>? sections);
        public Task<String?> GetValueAsync(String? key, IStringCryptor? cryptor, CancellationToken token, params String[]? sections);
        public Task<String?> GetValueAsync(String? key, IStringCryptor? cryptor, IEnumerable<String>? sections, CancellationToken token);
        public Task<String?> GetValueAsync(String? key, String? alternate, IStringCryptor? cryptor, params String[]? sections);
        public Task<String?> GetValueAsync(String? key, String? alternate, IStringCryptor? cryptor, IEnumerable<String>? sections);
        public Task<String?> GetValueAsync(String? key, String? alternate, IStringCryptor? cryptor, CancellationToken token, params String[]? sections);
        public Task<String?> GetValueAsync(String? key, String? alternate, IStringCryptor? cryptor, IEnumerable<String>? sections, CancellationToken token);
        public Task<String?> GetRawValueAsync(String? key, params String[]? sections);
        public Task<String?> GetRawValueAsync(String? key, IEnumerable<String>? sections);
        public Task<String?> GetRawValueAsync(String? key, CancellationToken token, params String[]? sections);
        public Task<String?> GetRawValueAsync(String? key, IEnumerable<String>? sections, CancellationToken token);
        public Task<String?> GetRawValueAsync(String? key, String? alternate, params String[]? sections);
        public Task<String?> GetRawValueAsync(String? key, String? alternate, IEnumerable<String>? sections);
        public Task<String?> GetRawValueAsync(String? key, String? alternate, CancellationToken token, params String[]? sections);
        public Task<String?> GetRawValueAsync(String? key, String? alternate, IEnumerable<String>? sections, CancellationToken token);
        public Boolean KeyExist(String? key, IStringCryptor? cryptor, params String[]? sections);
        public Boolean KeyExist(String? key, IStringCryptor? cryptor, IEnumerable<String>? sections);
        public Boolean KeyExistRaw(String? key, params String[]? sections);
        public Boolean KeyExistRaw(String? key, IEnumerable<String>? sections);
        public Task<Boolean> KeyExistAsync(String? key, IStringCryptor? cryptor, params String[]? sections);
        public Task<Boolean> KeyExistAsync(String? key, IStringCryptor? cryptor, IEnumerable<String>? sections);
        public Task<Boolean> KeyExistAsync(String? key, IStringCryptor? cryptor, CancellationToken token, params String[]? sections);
        public Task<Boolean> KeyExistAsync(String? key, IStringCryptor? cryptor, IEnumerable<String>? sections, CancellationToken token);
        public Task<Boolean> KeyExistRawAsync(String? key, params String[]? sections);
        public Task<Boolean> KeyExistRawAsync(String? key, IEnumerable<String>? sections);
        public Task<Boolean> KeyExistRawAsync(String? key, CancellationToken token, params String[]? sections);
        public Task<Boolean> KeyExistRawAsync(String? key, IEnumerable<String>? sections, CancellationToken token);
        public ConfigurationEntry[]? GetExists(IStringCryptor? cryptor);
        public ConfigurationEntry[]? GetExistsRaw();
        public Task<ConfigurationEntry[]?> GetExistsAsync(IStringCryptor? cryptor);
        public Task<ConfigurationEntry[]?> GetExistsAsync(IStringCryptor? cryptor, CancellationToken token);
        public Task<ConfigurationEntry[]?> GetExistsRawAsync();
        public Task<ConfigurationEntry[]?> GetExistsRawAsync(CancellationToken token);
    }
}