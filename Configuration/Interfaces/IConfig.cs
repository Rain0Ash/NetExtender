// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using NetExtender.Converters;
using NetExtender.Crypto;
using NetExtender.Crypto.CryptKey.Interfaces;

namespace NetExtender.Configuration.Interfaces
{
    public interface IConfig : IReadOnlyConfig
    {
        public new Boolean ThrowOnReadOnly { get; set; }
        public new Boolean CryptByDefault { get; set; }
        public new ConfigPropertyOptions DefaultOptions { get; set; }

        public Boolean SetValue<T>(String key, T value, params String[] sections)
        {
            return SetValue(key, value, (IEnumerable<String>) sections);
        }

        public Boolean SetValue<T>(String key, T value, IEnumerable<String> sections);

        public Boolean SetValue<T>(String key, T value, ICryptKey crypt, params String[] sections)
        {
            return SetValue(key, value, crypt, (IEnumerable<String>) sections);
        }

        public Boolean SetValue<T>(String key, T value, ICryptKey crypt, IEnumerable<String> sections);

        public Task<Boolean> SetValueAsync<T>(String key, T value, params String[] sections)
        {
            return SetValueAsync(key, value, (IEnumerable<String>) sections);
        }

        public Task<Boolean> SetValueAsync<T>(String key, T value, IEnumerable<String> sections);

        public Task<Boolean> SetValueAsync<T>(String key, T value, CancellationToken token, params String[] sections)
        {
            return SetValueAsync(key, value, sections, token);
        }

        public Task<Boolean> SetValueAsync<T>(String key, T value, IEnumerable<String> sections, CancellationToken token);

        public Task<Boolean> SetValueAsync<T>(String key, T value, ICryptKey crypt, params String[] sections)
        {
            return SetValueAsync(key, value, crypt, (IEnumerable<String>) sections);
        }

        public Task<Boolean> SetValueAsync<T>(String key, T value, ICryptKey crypt, IEnumerable<String> sections);

        public Task<Boolean> SetValueAsync<T>(String key, T value, ICryptKey crypt, CancellationToken token, params String[] sections)
        {
            return SetValueAsync(key, value, crypt, sections, token);
        }

        public Task<Boolean> SetValueAsync<T>(String key, T value, ICryptKey crypt, IEnumerable<String> sections, CancellationToken token);

        public String GetOrSetValue(String key, String defaultValue, params String[] sections)
        {
            return GetOrSetValue(key, defaultValue, (IEnumerable<String>) sections);
        }

        public String GetOrSetValue(String key, String defaultValue, IEnumerable<String> sections);

        public String GetOrSetValue(String key, String defaultValue, CryptAction crypt, params String[] sections)
        {
            return GetOrSetValue(key, defaultValue, crypt, (IEnumerable<String>) sections);
        }

        public String GetOrSetValue(String key, String defaultValue, CryptAction crypt, IEnumerable<String> sections);

        public String GetOrSetValue(String key, String defaultValue, CryptAction crypt, ICryptKey cryptKey, params String[] sections)
        {
            return GetOrSetValue(key, defaultValue, crypt, cryptKey, (IEnumerable<String>) sections);
        }

        public String GetOrSetValue(String key, String defaultValue, CryptAction crypt, ICryptKey cryptKey, IEnumerable<String> sections);

        public Task<String> GetOrSetValueAsync(String key, String defaultValue, params String[] sections)
        {
            return GetOrSetValueAsync(key, defaultValue, (IEnumerable<String>) sections);
        }

        public Task<String> GetOrSetValueAsync(String key, String defaultValue, IEnumerable<String> sections);

        public Task<String> GetOrSetValueAsync(String key, String defaultValue, CancellationToken token, params String[] sections)
        {
            return GetOrSetValueAsync(key, defaultValue, sections, token);
        }

        public Task<String> GetOrSetValueAsync(String key, String defaultValue, IEnumerable<String> sections, CancellationToken token);

        public Task<String> GetOrSetValueAsync(String key, String defaultValue, CryptAction crypt, params String[] sections)
        {
            return GetOrSetValueAsync(key, defaultValue, crypt, (IEnumerable<String>) sections);
        }

        public Task<String> GetOrSetValueAsync(String key, String defaultValue, CryptAction crypt, IEnumerable<String> sections);

        public Task<String> GetOrSetValueAsync(String key, String defaultValue, CryptAction crypt, CancellationToken token, params String[] sections)
        {
            return GetOrSetValueAsync(key, defaultValue, crypt, sections, token);
        }

        public Task<String> GetOrSetValueAsync(String key, String defaultValue, CryptAction crypt, IEnumerable<String> sections, CancellationToken token);

        public Task<String> GetOrSetValueAsync(String key, String defaultValue, CryptAction crypt, ICryptKey cryptKey, params String[] sections)
        {
            return GetOrSetValueAsync(key, defaultValue, crypt, cryptKey, (IEnumerable<String>) sections);
        }

        public Task<String> GetOrSetValueAsync(String key, String defaultValue, CryptAction crypt, ICryptKey cryptKey, IEnumerable<String> sections);

        public Task<String> GetOrSetValueAsync(String key, String defaultValue, CryptAction crypt, ICryptKey cryptKey, CancellationToken token, params String[] sections)
        {
            return GetOrSetValueAsync(key, defaultValue, crypt, cryptKey, sections, token);
        }

        public Task<String> GetOrSetValueAsync(String key, String defaultValue, CryptAction crypt, ICryptKey cryptKey, IEnumerable<String> sections, CancellationToken token);

        public T GetOrSetValue<T>(String key, T defaultValue, params String[] sections)
        {
            return GetOrSetValue(key, defaultValue, (IEnumerable<String>) sections);
        }

        public T GetOrSetValue<T>(String key, T defaultValue, IEnumerable<String> sections);

        public T GetOrSetValue<T>(String key, T defaultValue, ICryptKey crypt, params String[] sections)
        {
            return GetOrSetValue(key, defaultValue, crypt, (IEnumerable<String>) sections);
        }

        public T GetOrSetValue<T>(String key, T defaultValue, ICryptKey crypt, IEnumerable<String> sections);

        public T GetOrSetValue<T>(String key, T defaultValue, TryConverter<String, T> converter, params String[] sections)
        {
            return GetOrSetValue(key, defaultValue, converter, (IEnumerable<String>) sections);
        }

        public T GetOrSetValue<T>(String key, T defaultValue, TryConverter<String, T> converter, IEnumerable<String> sections);

        public T GetOrSetValue<T>(String key, T defaultValue, ICryptKey crypt, TryConverter<String, T> converter, params String[] sections)
        {
            return GetOrSetValue(key, defaultValue, crypt, converter, (IEnumerable<String>) sections);
        }

        public T GetOrSetValue<T>(String key, T defaultValue, ICryptKey crypt, TryConverter<String, T> converter, IEnumerable<String> sections);

        public Task<T> GetOrSetValueAsync<T>(String key, T defaultValue, params String[] sections)
        {
            return GetOrSetValueAsync(key, defaultValue, (IEnumerable<String>) sections);
        }

        public Task<T> GetOrSetValueAsync<T>(String key, T defaultValue, IEnumerable<String> sections);

        public Task<T> GetOrSetValueAsync<T>(String key, T defaultValue, CancellationToken token, params String[] sections)
        {
            return GetOrSetValueAsync(key, defaultValue, sections, token);
        }

        public Task<T> GetOrSetValueAsync<T>(String key, T defaultValue, IEnumerable<String> sections, CancellationToken token);

        public Task<T> GetOrSetValueAsync<T>(String key, T defaultValue, ICryptKey crypt, params String[] sections)
        {
            return GetOrSetValueAsync(key, defaultValue, crypt, (IEnumerable<String>) sections);
        }

        public Task<T> GetOrSetValueAsync<T>(String key, T defaultValue, ICryptKey crypt, IEnumerable<String> sections);

        public Task<T> GetOrSetValueAsync<T>(String key, T defaultValue, ICryptKey crypt, CancellationToken token, params String[] sections)
        {
            return GetOrSetValueAsync(key, defaultValue, crypt, sections, token);
        }

        public Task<T> GetOrSetValueAsync<T>(String key, T defaultValue, ICryptKey crypt, IEnumerable<String> sections, CancellationToken token);

        public Task<T> GetOrSetValueAsync<T>(String key, T defaultValue, TryConverter<String, T> converter, params String[] sections)
        {
            return GetOrSetValueAsync(key, defaultValue, converter, (IEnumerable<String>) sections);
        }

        public Task<T> GetOrSetValueAsync<T>(String key, T defaultValue, TryConverter<String, T> converter, IEnumerable<String> sections);

        public Task<T> GetOrSetValueAsync<T>(String key, T defaultValue, TryConverter<String, T> converter, CancellationToken token, params String[] sections)
        {
            return GetOrSetValueAsync(key, defaultValue, converter, sections, token);
        }

        public Task<T> GetOrSetValueAsync<T>(String key, T defaultValue, TryConverter<String, T> converter, IEnumerable<String> sections, CancellationToken token);

        public Task<T> GetOrSetValueAsync<T>(String key, T defaultValue, ICryptKey crypt, TryConverter<String, T> converter, params String[] sections)
        {
            return GetOrSetValueAsync(key, defaultValue, crypt, converter, (IEnumerable<String>) sections);
        }

        public Task<T> GetOrSetValueAsync<T>(String key, T defaultValue, ICryptKey crypt, TryConverter<String, T> converter, IEnumerable<String> sections);

        public Task<T> GetOrSetValueAsync<T>(String key, T defaultValue, ICryptKey crypt, TryConverter<String, T> converter, CancellationToken token, params String[] sections)
        {
            return GetOrSetValueAsync(key, defaultValue, crypt, converter, sections, token);
        }

        public Task<T> GetOrSetValueAsync<T>(String key, T defaultValue, ICryptKey crypt, TryConverter<String, T> converter, IEnumerable<String> sections, CancellationToken token);

        public Boolean RemoveValue(String key, params String[] sections)
        {
            return RemoveValue(key, (IEnumerable<String>) sections);
        }

        public Boolean RemoveValue(String key, IEnumerable<String> sections);

        public Task<Boolean> RemoveValueAsync(String key, params String[] sections)
        {
            return RemoveValueAsync(key, (IEnumerable<String>) sections);
        }

        public Task<Boolean> RemoveValueAsync(String key, IEnumerable<String> sections);

        public Task<Boolean> RemoveValueAsync(String key, CancellationToken token, params String[] sections)
        {
            return RemoveValueAsync(key, sections, token);
        }

        public Task<Boolean> RemoveValueAsync(String key, IEnumerable<String> sections, CancellationToken token);
    }
}