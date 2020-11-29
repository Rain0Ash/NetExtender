// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Threading;
using System.Threading.Tasks;
using NetExtender.Config.Common;
using NetExtender.Converters;
using NetExtender.Crypto.CryptKey.Interfaces;

namespace NetExtender.Config.Interfaces
{
    public interface IReadOnlyConfig : IDisposable
    {
        public String Path { get; }
        public ConfigOptions Options { get; }
        public Boolean IsReadOnly { get; }
        public Boolean ThrowOnReadOnly { get; set; }
        public Boolean CryptByDefault { get; set; }
        public ConfigPropertyOptions DefaultOptions { get; set; }
        public String GetValue(String key, params String[] sections);
        public String GetValue(String key, String defaultValue, params String[] sections);
        public String GetValue(String key, String defaultValue, Boolean decrypt, params String[] sections);
        public String GetValue(String key, String defaultValue, Boolean decrypt, ICryptKey crypt, params String[] sections);
        public Task<String> GetValueAsync(String key, params String[] sections);
        public Task<String> GetValueAsync(String key, CancellationToken token, params String[] sections);
        public Task<String> GetValueAsync(String key, String defaultValue, params String[] sections);
        public Task<String> GetValueAsync(String key, String defaultValue, CancellationToken token, params String[] sections);
        public Task<String> GetValueAsync(String key, String defaultValue, Boolean decrypt, params String[] sections);
        public Task<String> GetValueAsync(String key, String defaultValue, Boolean decrypt, CancellationToken token, params String[] sections);
        public Task<String> GetValueAsync(String key, String defaultValue, Boolean decrypt, ICryptKey crypt, params String[] sections);
        public Task<String> GetValueAsync(String key, String defaultValue, Boolean decrypt, ICryptKey crypt, CancellationToken token, params String[] sections);
        public T GetValue<T>(String key, params String[] sections);
        public T GetValue<T>(String key, T defaultValue, params String[] sections);
        public T GetValue<T>(String key, T defaultValue, ICryptKey crypt, params String[] sections);
        public T GetValue<T>(String key, T defaultValue, TryConverter<String, T> converter, params String[] sections);
        public T GetValue<T>(String key, T defaultValue, ICryptKey crypt, TryConverter<String, T> converter, params String[] sections);
        public Task<T> GetValueAsync<T>(String key, params String[] sections);
        public Task<T> GetValueAsync<T>(String key, CancellationToken token, params String[] sections);
        public Task<T> GetValueAsync<T>(String key, T defaultValue, params String[] sections);
        public Task<T> GetValueAsync<T>(String key, T defaultValue, CancellationToken token, params String[] sections);
        public Task<T> GetValueAsync<T>(String key, T defaultValue, ICryptKey crypt, params String[] sections);
        public Task<T> GetValueAsync<T>(String key, T defaultValue, ICryptKey crypt, CancellationToken token, params String[] sections);
        public Task<T> GetValueAsync<T>(String key, T defaultValue, TryConverter<String, T> converter, params String[] sections);
        public Task<T> GetValueAsync<T>(String key, T defaultValue, TryConverter<String, T> converter, CancellationToken token, params String[] sections);
        public Task<T> GetValueAsync<T>(String key, T defaultValue, ICryptKey crypt, TryConverter<String, T> converter, params String[] sections);
        public Task<T> GetValueAsync<T>(String key, T defaultValue, ICryptKey crypt, TryConverter<String, T> converter, CancellationToken token, params String[] sections);
        public Boolean KeyExist(String key, params String[] sections);
        public Task<Boolean> KeyExistAsync(String key, params String[] sections);
        public Task<Boolean> KeyExistAsync(String key, CancellationToken token, params String[] sections);
    }
}