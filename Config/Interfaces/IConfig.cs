// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Threading;
using System.Threading.Tasks;
using NetExtender.Converters;
using NetExtender.Crypto;
using NetExtender.Crypto.CryptKey.Interfaces;

namespace NetExtender.Config.Interfaces
{
    public interface IConfig : IReadOnlyConfig
    {
        public new Boolean ThrowOnReadOnly { get; set; }
        public new Boolean CryptByDefault { get; set; }
        public new ConfigPropertyOptions DefaultOptions { get; set; }
        public Boolean SetValue<T>(String key, T value, params String[] sections);
        public Boolean SetValue<T>(String key, T value, ICryptKey crypt, params String[] sections);
        public Task<Boolean> SetValueAsync<T>(String key, T value, params String[] sections);
        public Task<Boolean> SetValueAsync<T>(String key, T value, CancellationToken token, params String[] sections);
        public Task<Boolean> SetValueAsync<T>(String key, T value, ICryptKey crypt, params String[] sections);
        public Task<Boolean> SetValueAsync<T>(String key, T value, ICryptKey crypt, CancellationToken token, params String[] sections);
        public String GetOrSetValue(String key, String defaultValue, params String[] sections);
        public String GetOrSetValue(String key, String defaultValue, CryptAction crypt, params String[] sections);
        public String GetOrSetValue(String key, String defaultValue, CryptAction crypt, ICryptKey cryptKey, params String[] sections);
        public Task<String> GetOrSetValueAsync(String key, String defaultValue, params String[] sections);
        public Task<String> GetOrSetValueAsync(String key, String defaultValue, CancellationToken token, params String[] sections);
        public Task<String> GetOrSetValueAsync(String key, String defaultValue, CryptAction crypt, params String[] sections);
        public Task<String> GetOrSetValueAsync(String key, String defaultValue, CryptAction crypt, CancellationToken token, params String[] sections);
        public Task<String> GetOrSetValueAsync(String key, String defaultValue, CryptAction crypt, ICryptKey cryptKey, params String[] sections);
        public Task<String> GetOrSetValueAsync(String key, String defaultValue, CryptAction crypt, ICryptKey cryptKey, CancellationToken token, params String[] sections);
        public T GetOrSetValue<T>(String key, T defaultValue, params String[] sections);
        public T GetOrSetValue<T>(String key, T defaultValue, ICryptKey crypt, params String[] sections);
        public T GetOrSetValue<T>(String key, T defaultValue, TryConverter<String, T> converter, params String[] sections);
        public T GetOrSetValue<T>(String key, T defaultValue, ICryptKey crypt, TryConverter<String, T> converter, params String[] sections);
        public Task<T> GetOrSetValueAsync<T>(String key, T defaultValue, params String[] sections);
        public Task<T> GetOrSetValueAsync<T>(String key, T defaultValue, CancellationToken token, params String[] sections);
        public Task<T> GetOrSetValueAsync<T>(String key, T defaultValue, ICryptKey crypt, params String[] sections);
        public Task<T> GetOrSetValueAsync<T>(String key, T defaultValue, ICryptKey crypt, CancellationToken token, params String[] sections);
        public Task<T> GetOrSetValueAsync<T>(String key, T defaultValue, TryConverter<String, T> converter, params String[] sections);
        public Task<T> GetOrSetValueAsync<T>(String key, T defaultValue, TryConverter<String, T> converter, CancellationToken token, params String[] sections);
        public Task<T> GetOrSetValueAsync<T>(String key, T defaultValue, ICryptKey crypt, TryConverter<String, T> converter, params String[] sections);
        public Task<T> GetOrSetValueAsync<T>(String key, T defaultValue, ICryptKey crypt, TryConverter<String, T> converter, CancellationToken token, params String[] sections);
        public Boolean RemoveValue(String key, params String[] sections);
        public Task<Boolean> RemoveValueAsync(String key, params String[] sections);
        public Task<Boolean> RemoveValueAsync(String key, CancellationToken token, params String[] sections);
    }
}