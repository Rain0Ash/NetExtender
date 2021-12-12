// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using NetExtender.Configuration.Behavior.Interfaces;
using NetExtender.Configuration.Common;
using NetExtender.Configuration.Cryptography.Common.Interfaces;

namespace NetExtender.Configuration.Cryptography.Behavior.Interfaces
{
    public interface ICryptographyConfigBehavior : IConfigBehavior, IConfigurationCryptographyInfo
    {
        public IConfigurationCryptor Cryptor { get; }
        public Boolean Contains(String? key, IConfigurationCryptor? cryptor, IEnumerable<String>? sections);
        public Task<Boolean> ContainsAsync(String? key, IConfigurationCryptor? cryptor, IEnumerable<String>? sections, CancellationToken token);
        public Boolean ContainsRaw(String? key, IEnumerable<String>? sections);
        public Task<Boolean> ContainsRawAsync(String? key, IEnumerable<String>? sections, CancellationToken token);
        public String? Get(String? key, IConfigurationCryptor? cryptor, IEnumerable<String>? sections);
        public Task<String?> GetAsync(String? key, IConfigurationCryptor? cryptor, IEnumerable<String>? sections, CancellationToken token);
        public String? GetRaw(String? key, IEnumerable<String>? sections);
        public Task<String?> GetRawAsync(String? key, IEnumerable<String>? sections, CancellationToken token);
        public Boolean Set(String? key, String? value, IConfigurationCryptor? cryptor, IEnumerable<String>? sections);
        public Task<Boolean> SetAsync(String? key, String? value, IConfigurationCryptor? cryptor, IEnumerable<String>? sections, CancellationToken token);
        public Boolean SetRaw(String? key, String? value, IEnumerable<String>? sections);
        public Task<Boolean> SetRawAsync(String? key, String? value, IEnumerable<String>? sections, CancellationToken token);
        public String? GetOrSet(String? key, String? value, IConfigurationCryptor? cryptor, IEnumerable<String>? sections);
        public Task<String?> GetOrSetAsync(String? key, String? value, IConfigurationCryptor? cryptor, IEnumerable<String>? sections, CancellationToken token);
        public String? GetOrSetRaw(String? key, String? value, IEnumerable<String>? sections);
        public Task<String?> GetOrSetRawAsync(String? key, String? value, IEnumerable<String>? sections, CancellationToken token);
        public ConfigurationEntry[]? GetExists(IConfigurationCryptor? cryptor);
        public ConfigurationEntry[]? GetExistsRaw();
        public ConfigurationValueEntry[]? GetExistsValues(IConfigurationCryptor? cryptor);
        public ConfigurationValueEntry[]? GetExistsValuesRaw();
        public Task<ConfigurationEntry[]?> GetExistsAsync(IConfigurationCryptor? cryptor, CancellationToken token);
        public Task<ConfigurationEntry[]?> GetExistsRawAsync(CancellationToken token);
        public Task<ConfigurationValueEntry[]?> GetExistsValuesAsync(IConfigurationCryptor? cryptor, CancellationToken token);
        public Task<ConfigurationValueEntry[]?> GetExistsValuesRawAsync(CancellationToken token);
    }
}