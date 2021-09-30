// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using NetExtender.Configuration.Common;
using NetExtender.Crypto.CryptKey.Interfaces;

namespace NetExtender.Configuration.Behavior.Interfaces
{
    public interface IConfigBehavior : IDisposable
    {
        public String Path { get; }
        public ICryptKey Crypt { get; }
        public ConfigOptions Options { get; }
        public Boolean IsReadOnly { get; }
        public Boolean IsCryptData { get; }
        public Boolean IsCryptConfig { get; }
        public Boolean IsCryptAll { get; }
        public Boolean ThrowOnReadOnly { get; set; }
        public Boolean CryptByDefault { get; set; }
        public ConfigPropertyOptions DefaultOptions { get; set; }
        public String? ConvertToValue<T>(T value);
        public T? ConvertFromValue<T>(String? value);
        public String? Get(String? key, IEnumerable<String>? sections);
        public Task<String?> GetAsync(String? key, IEnumerable<String>? sections, CancellationToken token);
        public Boolean Set(String? key, String? value, IEnumerable<String>? sections);
        public Task<Boolean> SetAsync(String? key, String? value, IEnumerable<String>? sections, CancellationToken token);
    }
}