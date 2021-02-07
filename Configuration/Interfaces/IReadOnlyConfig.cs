// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using NetExtender.Configuration.Common;
using NetExtender.Converters;
using NetExtender.Crypto.CryptKey.Interfaces;

namespace NetExtender.Configuration.Interfaces
{
    public interface IReadOnlyConfig : IDisposable
    {
        public String Path { get; }
        public ConfigOptions Options { get; }
        public Boolean IsReadOnly { get; }
        public Boolean ThrowOnReadOnly { get; set; }
        public Boolean CryptByDefault { get; set; }
        public ConfigPropertyOptions DefaultOptions { get; set; }

        public String GetValue(String key, params String[] sections)
        {
            return GetValue(key, (IEnumerable<String>) sections);
        }

        public String GetValue(String key, IEnumerable<String> sections);

        public String GetValue(String key, String defaultValue, params String[] sections)
        {
            return GetValue(key, defaultValue, (IEnumerable<String>) sections);
        }

        public String GetValue(String key, String defaultValue, IEnumerable<String> sections);

        public String GetValue(String key, String defaultValue, Boolean decrypt, params String[] sections)
        {
            return GetValue(key, defaultValue, decrypt, (IEnumerable<String>) sections);
        }

        public String GetValue(String key, String defaultValue, Boolean decrypt, IEnumerable<String> sections);

        public String GetValue(String key, String defaultValue, Boolean decrypt, ICryptKey crypt, params String[] sections)
        {
            return GetValue(key, defaultValue, decrypt, crypt, (IEnumerable<String>) sections);
        }

        public String GetValue(String key, String defaultValue, Boolean decrypt, ICryptKey crypt, IEnumerable<String> sections);

        public Task<String> GetValueAsync(String key, params String[] sections)
        {
            return GetValueAsync(key, (IEnumerable<String>) sections);
        }

        public Task<String> GetValueAsync(String key, IEnumerable<String> sections);

        public Task<String> GetValueAsync(String key, CancellationToken token, params String[] sections)
        {
            return GetValueAsync(key, (IEnumerable<String>) sections, token);
        }

        public Task<String> GetValueAsync(String key, IEnumerable<String> sections, CancellationToken token);

        public Task<String> GetValueAsync(String key, String defaultValue, params String[] sections)
        {
            return GetValueAsync(key, defaultValue, (IEnumerable<String>) sections);
        }

        public Task<String> GetValueAsync(String key, String defaultValue, IEnumerable<String> sections);

        public Task<String> GetValueAsync(String key, String defaultValue, CancellationToken token, params String[] sections)
        {
            return GetValueAsync(key, defaultValue, sections, token);
        }

        public Task<String> GetValueAsync(String key, String defaultValue, IEnumerable<String> sections, CancellationToken token);

        public Task<String> GetValueAsync(String key, String defaultValue, Boolean decrypt, params String[] sections)
        {
            return GetValueAsync(key, defaultValue, decrypt, (IEnumerable<String>) sections);
        }

        public Task<String> GetValueAsync(String key, String defaultValue, Boolean decrypt, IEnumerable<String> sections);

        public Task<String> GetValueAsync(String key, String defaultValue, Boolean decrypt, CancellationToken token, params String[] sections)
        {
            return GetValueAsync(key, defaultValue, decrypt, sections, token);
        }

        public Task<String> GetValueAsync(String key, String defaultValue, Boolean decrypt, IEnumerable<String> sections, CancellationToken token);

        public Task<String> GetValueAsync(String key, String defaultValue, Boolean decrypt, ICryptKey crypt, params String[] sections)
        {
            return GetValueAsync(key, defaultValue, decrypt, crypt, (IEnumerable<String>) sections);
        }

        public Task<String> GetValueAsync(String key, String defaultValue, Boolean decrypt, ICryptKey crypt, IEnumerable<String> sections);

        public Task<String> GetValueAsync(String key, String defaultValue, Boolean decrypt, ICryptKey crypt, CancellationToken token, params String[] sections)
        {
            return GetValueAsync(key, defaultValue, decrypt, crypt, sections, token);
        }

        public Task<String> GetValueAsync(String key, String defaultValue, Boolean decrypt, ICryptKey crypt, IEnumerable<String> sections, CancellationToken token);

        public T GetValue<T>(String key, params String[] sections)
        {
            return GetValue<T>(key, (IEnumerable<String>) sections);
        }

        public T GetValue<T>(String key, IEnumerable<String> sections);

        public T GetValue<T>(String key, T defaultValue, params String[] sections)
        {
            return GetValue(key, defaultValue, (IEnumerable<String>) sections);
        }

        public T GetValue<T>(String key, T defaultValue, IEnumerable<String> sections);

        public T GetValue<T>(String key, T defaultValue, ICryptKey crypt, params String[] sections)
        {
            return GetValue(key, defaultValue, crypt, (IEnumerable<String>) sections);
        }

        public T GetValue<T>(String key, T defaultValue, ICryptKey crypt, IEnumerable<String> sections);

        public T GetValue<T>(String key, T defaultValue, TryConverter<String, T> converter, params String[] sections)
        {
            return GetValue(key, defaultValue, converter, (IEnumerable<String>) sections);
        }

        public T GetValue<T>(String key, T defaultValue, TryConverter<String, T> converter, IEnumerable<String> sections);

        public T GetValue<T>(String key, T defaultValue, ICryptKey crypt, TryConverter<String, T> converter, params String[] sections)
        {
            return GetValue(key, defaultValue, crypt, converter, (IEnumerable<String>) sections);
        }

        public T GetValue<T>(String key, T defaultValue, ICryptKey crypt, TryConverter<String, T> converter, IEnumerable<String> sections);

        public Task<T> GetValueAsync<T>(String key, params String[] sections)
        {
            return GetValueAsync<T>(key, (IEnumerable<String>) sections);
        }

        public Task<T> GetValueAsync<T>(String key, IEnumerable<String> sections);

        public Task<T> GetValueAsync<T>(String key, CancellationToken token, params String[] sections)
        {
            return GetValueAsync<T>(key, sections, token);
        }

        public Task<T> GetValueAsync<T>(String key, IEnumerable<String> sections, CancellationToken token);

        public Task<T> GetValueAsync<T>(String key, T defaultValue, params String[] sections)
        {
            return GetValueAsync(key, defaultValue, (IEnumerable<String>) sections);
        }

        public Task<T> GetValueAsync<T>(String key, T defaultValue, IEnumerable<String> sections);

        public Task<T> GetValueAsync<T>(String key, T defaultValue, CancellationToken token, params String[] sections)
        {
            return GetValueAsync(key, defaultValue, sections, token);
        }

        public Task<T> GetValueAsync<T>(String key, T defaultValue, IEnumerable<String> sections, CancellationToken token);

        public Task<T> GetValueAsync<T>(String key, T defaultValue, ICryptKey crypt, params String[] sections)
        {
            return GetValueAsync(key, defaultValue, crypt, (IEnumerable<String>) sections);
        }

        public Task<T> GetValueAsync<T>(String key, T defaultValue, ICryptKey crypt, IEnumerable<String> sections);

        public Task<T> GetValueAsync<T>(String key, T defaultValue, ICryptKey crypt, CancellationToken token, params String[] sections)
        {
            return GetValueAsync(key, defaultValue, crypt, sections, token);
        }

        public Task<T> GetValueAsync<T>(String key, T defaultValue, ICryptKey crypt, IEnumerable<String> sections, CancellationToken token);

        public Task<T> GetValueAsync<T>(String key, T defaultValue, TryConverter<String, T> converter, params String[] sections)
        {
            return GetValueAsync(key, defaultValue, converter, (IEnumerable<String>) sections);
        }

        public Task<T> GetValueAsync<T>(String key, T defaultValue, TryConverter<String, T> converter, IEnumerable<String> sections);

        public Task<T> GetValueAsync<T>(String key, T defaultValue, TryConverter<String, T> converter, CancellationToken token, params String[] sections)
        {
            return GetValueAsync(key, defaultValue, converter, sections, token);
        }

        public Task<T> GetValueAsync<T>(String key, T defaultValue, TryConverter<String, T> converter, IEnumerable<String> sections, CancellationToken token);

        public Task<T> GetValueAsync<T>(String key, T defaultValue, ICryptKey crypt, TryConverter<String, T> converter, params String[] sections)
        {
            return GetValueAsync(key, defaultValue, crypt, converter, (IEnumerable<String>) sections);
        }

        public Task<T> GetValueAsync<T>(String key, T defaultValue, ICryptKey crypt, TryConverter<String, T> converter, IEnumerable<String> sections);

        public Task<T> GetValueAsync<T>(String key, T defaultValue, ICryptKey crypt, TryConverter<String, T> converter, CancellationToken token, params String[] sections)
        {
            return GetValueAsync(key, defaultValue, crypt, converter, sections, token);
        }

        public Task<T> GetValueAsync<T>(String key, T defaultValue, ICryptKey crypt, TryConverter<String, T> converter, IEnumerable<String> sections, CancellationToken token);

        public Boolean KeyExist(String key, params String[] sections)
        {
            return KeyExist(key, (IEnumerable<String>) sections);
        }

        public Boolean KeyExist(String key, IEnumerable<String> sections);

        public Task<Boolean> KeyExistAsync(String key, params String[] sections)
        {
            return KeyExistAsync(key, (IEnumerable<String>) sections);
        }

        public Task<Boolean> KeyExistAsync(String key, IEnumerable<String> sections);

        public Task<Boolean> KeyExistAsync(String key, CancellationToken token, params String[] sections)
        {
            return KeyExistAsync(key, sections, token);
        }

        public Task<Boolean> KeyExistAsync(String key, IEnumerable<String> sections, CancellationToken token);
    }
}