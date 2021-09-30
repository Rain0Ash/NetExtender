// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using NetExtender.Configuration.Common;
using NetExtender.Crypto.CryptKey.Interfaces;

namespace NetExtender.Configuration.Interfaces
{
    public interface IReadOnlyConfig : IDisposable
    {
        public String Path { get; }
        public ConfigOptions Options { get; }
        public Boolean IsReadOnly { get; }
        public Boolean ThrowOnReadOnly { get; }
        public Boolean CryptByDefault { get; }
        public ConfigPropertyOptions DefaultOptions { get; }

        public String? GetValue(String? key, params String[]? sections)
        {
            return GetValue(key, (IEnumerable<String>?) sections);
        }

        public String? GetValue(String? key, IEnumerable<String>? sections);

        public String? GetValue(String? key, String? alternate, params String[]? sections)
        {
            return GetValue(key, alternate, (IEnumerable<String>?) sections);
        }

        public String? GetValue(String? key, String? alternate, IEnumerable<String>? sections);

        public String? GetValue(String? key, String? alternate, Boolean decrypt, params String[]? sections)
        {
            return GetValue(key, alternate, decrypt, (IEnumerable<String>?) sections);
        }

        public String? GetValue(String? key, String? alternate, Boolean decrypt, IEnumerable<String>? sections);

        public String? GetValue(String? key, String? alternate, Boolean decrypt, ICryptKey? crypt, params String[]? sections)
        {
            return GetValue(key, alternate, decrypt, crypt, (IEnumerable<String>?) sections);
        }

        public String? GetValue(String? key, String? alternate, Boolean decrypt, ICryptKey? crypt, IEnumerable<String>? sections);

        public Task<String?> GetValueAsync(String? key, params String[]? sections)
        {
            return GetValueAsync(key, (IEnumerable<String>?) sections);
        }

        public Task<String?> GetValueAsync(String? key, IEnumerable<String>? sections);

        public Task<String?> GetValueAsync(String? key, CancellationToken token, params String[]? sections)
        {
            return GetValueAsync(key, (IEnumerable<String>?) sections, token);
        }

        public Task<String?> GetValueAsync(String? key, IEnumerable<String>? sections, CancellationToken token);

        public Task<String?> GetValueAsync(String? key, String? alternate, params String[]? sections)
        {
            return GetValueAsync(key, alternate, (IEnumerable<String>?) sections);
        }

        public Task<String?> GetValueAsync(String? key, String? alternate, IEnumerable<String>? sections);

        public Task<String?> GetValueAsync(String? key, String? alternate, CancellationToken token, params String[]? sections)
        {
            return GetValueAsync(key, alternate, sections, token);
        }

        public Task<String?> GetValueAsync(String? key, String? alternate, IEnumerable<String>? sections, CancellationToken token);

        public Task<String?> GetValueAsync(String? key, String? alternate, Boolean decrypt, params String[]? sections)
        {
            return GetValueAsync(key, alternate, decrypt, (IEnumerable<String>?) sections);
        }

        public Task<String?> GetValueAsync(String? key, String? alternate, Boolean decrypt, IEnumerable<String>? sections);

        public Task<String?> GetValueAsync(String? key, String? alternate, Boolean decrypt, CancellationToken token, params String[]? sections)
        {
            return GetValueAsync(key, alternate, decrypt, sections, token);
        }

        public Task<String?> GetValueAsync(String? key, String? alternate, Boolean decrypt, IEnumerable<String>? sections, CancellationToken token);

        public Task<String?> GetValueAsync(String? key, String? alternate, Boolean decrypt, ICryptKey? crypt, params String[]? sections)
        {
            return GetValueAsync(key, alternate, decrypt, crypt, (IEnumerable<String>?) sections);
        }

        public Task<String?> GetValueAsync(String? key, String? alternate, Boolean decrypt, ICryptKey? crypt, IEnumerable<String>? sections);

        public Task<String?> GetValueAsync(String? key, String? alternate, Boolean decrypt, ICryptKey? crypt, CancellationToken token, params String[]? sections)
        {
            return GetValueAsync(key, alternate, decrypt, crypt, sections, token);
        }

        public Task<String?> GetValueAsync(String? key, String? alternate, Boolean decrypt, ICryptKey? crypt, IEnumerable<String>? sections, CancellationToken token);

        public T? GetValue<T>(String? key, params String[]? sections)
        {
            return GetValue<T>(key, (IEnumerable<String>?) sections);
        }

        public T? GetValue<T>(String? key, IEnumerable<String>? sections);

        public T? GetValue<T>(String? key, T alternate, params String[]? sections)
        {
            return GetValue(key, alternate, (IEnumerable<String>?) sections);
        }

        public T? GetValue<T>(String? key, T alternate, IEnumerable<String>? sections);

        public T? GetValue<T>(String? key, T alternate, ICryptKey? crypt, params String[]? sections)
        {
            return GetValue(key, alternate, crypt, (IEnumerable<String>?) sections);
        }

        public T? GetValue<T>(String? key, T alternate, ICryptKey? crypt, IEnumerable<String>? sections);

        public T? GetValue<T>(String? key, T alternate, TryConverter<String?, T>? converter, params String[]? sections)
        {
            return GetValue(key, alternate, converter, (IEnumerable<String>?) sections);
        }

        public T? GetValue<T>(String? key, T alternate, TryConverter<String?, T>? converter, IEnumerable<String>? sections);

        public T? GetValue<T>(String? key, T alternate, ICryptKey? crypt, TryConverter<String?, T>? converter, params String[]? sections)
        {
            return GetValue(key, alternate, crypt, converter, (IEnumerable<String>?) sections);
        }

        public T? GetValue<T>(String? key, T alternate, ICryptKey? crypt, TryConverter<String?, T>? converter, IEnumerable<String>? sections);

        public Task<T?> GetValueAsync<T>(String key, params String[]? sections)
        {
            return GetValueAsync<T>(key, (IEnumerable<String>?) sections);
        }

        public Task<T?> GetValueAsync<T>(String? key, IEnumerable<String>? sections);

        public Task<T?> GetValueAsync<T>(String? key, CancellationToken token, params String[]? sections)
        {
            return GetValueAsync<T>(key, sections, token);
        }

        public Task<T?> GetValueAsync<T>(String? key, IEnumerable<String>? sections, CancellationToken token);

        public Task<T?> GetValueAsync<T>(String? key, T alternate, params String[]? sections)
        {
            return GetValueAsync(key, alternate, (IEnumerable<String>?) sections);
        }

        public Task<T?> GetValueAsync<T>(String? key, T alternate, IEnumerable<String>? sections);

        public Task<T?> GetValueAsync<T>(String? key, T alternate, CancellationToken token, params String[]? sections)
        {
            return GetValueAsync(key, alternate, sections, token);
        }

        public Task<T?> GetValueAsync<T>(String? key, T alternate, IEnumerable<String>? sections, CancellationToken token);

        public Task<T?> GetValueAsync<T>(String? key, T alternate, ICryptKey? crypt, params String[]? sections)
        {
            return GetValueAsync(key, alternate, crypt, (IEnumerable<String>?) sections);
        }

        public Task<T?> GetValueAsync<T>(String? key, T alternate, ICryptKey? crypt, IEnumerable<String>? sections);

        public Task<T?> GetValueAsync<T>(String? key, T alternate, ICryptKey? crypt, CancellationToken token, params String[]? sections)
        {
            return GetValueAsync(key, alternate, crypt, sections, token);
        }

        public Task<T?> GetValueAsync<T>(String? key, T alternate, ICryptKey? crypt, IEnumerable<String>? sections, CancellationToken token);

        public Task<T?> GetValueAsync<T>(String? key, T alternate, TryConverter<String?, T>? converter, params String[]? sections)
        {
            return GetValueAsync(key, alternate, converter, (IEnumerable<String>?) sections);
        }

        public Task<T?> GetValueAsync<T>(String? key, T alternate, TryConverter<String?, T>? converter, IEnumerable<String>? sections);

        public Task<T?> GetValueAsync<T>(String? key, T alternate, TryConverter<String?, T>? converter, CancellationToken token, params String[]? sections)
        {
            return GetValueAsync(key, alternate, converter, sections, token);
        }

        public Task<T?> GetValueAsync<T>(String? key, T alternate, TryConverter<String?, T>? converter, IEnumerable<String>? sections, CancellationToken token);

        public Task<T?> GetValueAsync<T>(String? key, T alternate, ICryptKey? crypt, TryConverter<String?, T>? converter, params String[]? sections)
        {
            return GetValueAsync(key, alternate, crypt, converter, (IEnumerable<String>?) sections);
        }

        public Task<T?> GetValueAsync<T>(String? key, T alternate, ICryptKey? crypt, TryConverter<String?, T>? converter, IEnumerable<String>? sections);

        public Task<T?> GetValueAsync<T>(String? key, T alternate, ICryptKey? crypt, TryConverter<String?, T>? converter, CancellationToken token, params String[]? sections)
        {
            return GetValueAsync(key, alternate, crypt, converter, sections, token);
        }

        public Task<T?> GetValueAsync<T>(String? key, T alternate, ICryptKey? crypt, TryConverter<String?, T>? converter, IEnumerable<String>? sections, CancellationToken token);

        public Boolean KeyExist(String? key, params String[]? sections)
        {
            return KeyExist(key, (IEnumerable<String>?) sections);
        }

        public Boolean KeyExist(String? key, IEnumerable<String>? sections);

        public Task<Boolean> KeyExistAsync(String? key, params String[]? sections)
        {
            return KeyExistAsync(key, (IEnumerable<String>?) sections);
        }

        public Task<Boolean> KeyExistAsync(String? key, IEnumerable<String>? sections);

        public Task<Boolean> KeyExistAsync(String? key, CancellationToken token, params String[]? sections)
        {
            return KeyExistAsync(key, sections, token);
        }

        public Task<Boolean> KeyExistAsync(String? key, IEnumerable<String>? sections, CancellationToken token);
    }
}