// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using NetExtender.Configuration.Behavior.Interfaces;
using NetExtender.Configuration.Common;
using NetExtender.Configuration.Cryptography.Behavior.Transactions.Interfaces;
using NetExtender.Configuration.Cryptography.Common.Interfaces;
using NetExtender.Cryptography.Keys.Interfaces;

namespace NetExtender.Configuration.Cryptography.Behavior.Interfaces
{
    public interface ICryptographyConfigBehavior : IConfigBehavior, IConfigurationCryptographyInfo
    {
        public IConfigurationCryptor Cryptor { get; }
        public Boolean Contains(String? key, IStringCryptor? cryptor, IEnumerable<String>? sections);
        public Task<Boolean> ContainsAsync(String? key, IStringCryptor? cryptor, IEnumerable<String>? sections, CancellationToken token);
        public Boolean ContainsRaw(String? key, IEnumerable<String>? sections);
        public Task<Boolean> ContainsRawAsync(String? key, IEnumerable<String>? sections, CancellationToken token);
        public String? Get(String? key, IStringCryptor? cryptor, IEnumerable<String>? sections);
        public Task<String?> GetAsync(String? key, IStringCryptor? cryptor, IEnumerable<String>? sections, CancellationToken token);
        public String? GetRaw(String? key, IEnumerable<String>? sections);
        public Task<String?> GetRawAsync(String? key, IEnumerable<String>? sections, CancellationToken token);
        public Boolean Set(String? key, String? value, IStringCryptor? cryptor, IEnumerable<String>? sections);
        public Task<Boolean> SetAsync(String? key, String? value, IStringCryptor? cryptor, IEnumerable<String>? sections, CancellationToken token);
        public Boolean SetRaw(String? key, String? value, IEnumerable<String>? sections);
        public Task<Boolean> SetRawAsync(String? key, String? value, IEnumerable<String>? sections, CancellationToken token);
        public String? GetOrSet(String? key, String? value, IStringCryptor? cryptor, IEnumerable<String>? sections);
        public Task<String?> GetOrSetAsync(String? key, String? value, IStringCryptor? cryptor, IEnumerable<String>? sections, CancellationToken token);
        public String? GetOrSetRaw(String? key, String? value, IEnumerable<String>? sections);
        public Task<String?> GetOrSetRawAsync(String? key, String? value, IEnumerable<String>? sections, CancellationToken token);
        public ConfigurationEntry[]? GetExists(IStringCryptor? cryptor, IEnumerable<String>? sections);
        public ConfigurationEntry[]? GetExistsRaw(IEnumerable<String>? sections);
        public ConfigurationValueEntry[]? GetExistsValues(IStringCryptor? cryptor, IEnumerable<String>? sections);
        public ConfigurationValueEntry[]? GetExistsValuesRaw(IEnumerable<String>? sections);
        public Task<ConfigurationEntry[]?> GetExistsAsync(IStringCryptor? cryptor, IEnumerable<String>? sections, CancellationToken token);
        public Task<ConfigurationEntry[]?> GetExistsRawAsync(IEnumerable<String>? sections, CancellationToken token);
        public Task<ConfigurationValueEntry[]?> GetExistsValuesAsync(IStringCryptor? cryptor, IEnumerable<String>? sections, CancellationToken token);
        public Task<ConfigurationValueEntry[]?> GetExistsValuesRawAsync(IEnumerable<String>? sections, CancellationToken token);
        public Boolean Clear(IStringCryptor? cryptor, IEnumerable<String>? sections);
        public Boolean ClearRaw(IEnumerable<String>? sections);
        public Task<Boolean> ClearAsync(IStringCryptor? cryptor, IEnumerable<String>? sections, CancellationToken token);
        public Task<Boolean> ClearRawAsync(IEnumerable<String>? sections, CancellationToken token);
        public Boolean Merge(IStringCryptor? cryptor, IEnumerable<ConfigurationValueEntry>? entries);
        public Boolean MergeRaw(IEnumerable<ConfigurationValueEntry>? entries);
        public Task<Boolean> MergeAsync(IStringCryptor? cryptor, IEnumerable<ConfigurationValueEntry>? entries, CancellationToken token);
        public Task<Boolean> MergeRawAsync(IEnumerable<ConfigurationValueEntry>? entries, CancellationToken token);
        public Boolean Replace(IStringCryptor? cryptor, IEnumerable<ConfigurationValueEntry>? entries);
        public Boolean ReplaceRaw(IEnumerable<ConfigurationValueEntry>? entries);
        public Task<Boolean> ReplaceAsync(IStringCryptor? cryptor, IEnumerable<ConfigurationValueEntry>? entries, CancellationToken token);
        public Task<Boolean> ReplaceRawAsync(IEnumerable<ConfigurationValueEntry>? entries, CancellationToken token);
        public ConfigurationValueEntry[]? Difference(IStringCryptor? cryptor, IEnumerable<ConfigurationValueEntry>? entries);
        public ConfigurationValueEntry[]? DifferenceRaw(IEnumerable<ConfigurationValueEntry>? entries);
        public Task<ConfigurationValueEntry[]?> DifferenceAsync(IStringCryptor? cryptor, IEnumerable<ConfigurationValueEntry>? entries, CancellationToken token);
        public Task<ConfigurationValueEntry[]?> DifferenceRawAsync(IEnumerable<ConfigurationValueEntry>? entries, CancellationToken token);
        public new ICryptographyConfigBehaviorTransaction? Transaction();
        public ICryptographyConfigBehaviorTransaction? Transaction(IStringCryptor? cryptor);
        public new Task<ICryptographyConfigBehaviorTransaction?> TransactionAsync(CancellationToken token);
        public Task<ICryptographyConfigBehaviorTransaction?> TransactionAsync(IStringCryptor? cryptor, CancellationToken token);
    }
}