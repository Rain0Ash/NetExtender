// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using NetExtender.Configuration.Common;
using NetExtender.Crypto;
using NetExtender.Crypto.CryptKey.Interfaces;

namespace NetExtender.Configuration.Interfaces
{
    public interface IConfig : IDisposable
    {
        public String Path { get; }
        public ConfigOptions Options { get; }
        public Boolean IsReadOnly { get; }
        public Boolean IsLazyWrite { get; }
        public Boolean ThrowOnReadOnly { get; }
        public Boolean CryptByDefault { get; }
        public ConfigPropertyOptions DefaultOptions { get; }

        public String? GetValue(String? key, params String[]? sections);
        public String? GetValue(String? key, IEnumerable<String>? sections);
        public String? GetValue(String? key, String? alternate, params String[]? sections);
        public String? GetValue(String? key, String? alternate, IEnumerable<String>? sections);
        public String? GetValue(String? key, String? alternate, Boolean decrypt, params String[]? sections);
        public String? GetValue(String? key, String? alternate, Boolean decrypt, IEnumerable<String>? sections);
        public String? GetValue(String? key, String? alternate, Boolean decrypt, ICryptKey? crypt, params String[]? sections);
        public String? GetValue(String? key, String? alternate, Boolean decrypt, ICryptKey? crypt, IEnumerable<String>? sections);
        public Task<String?> GetValueAsync(String? key, params String[]? sections);
        public Task<String?> GetValueAsync(String? key, IEnumerable<String>? sections);
        public Task<String?> GetValueAsync(String? key, CancellationToken token, params String[]? sections);
        public Task<String?> GetValueAsync(String? key, IEnumerable<String>? sections, CancellationToken token);
        public Task<String?> GetValueAsync(String? key, String? alternate, params String[]? sections);
        public Task<String?> GetValueAsync(String? key, String? alternate, IEnumerable<String>? sections);
        public Task<String?> GetValueAsync(String? key, String? alternate, CancellationToken token, params String[]? sections);
        public Task<String?> GetValueAsync(String? key, String? alternate, IEnumerable<String>? sections, CancellationToken token);
        public Task<String?> GetValueAsync(String? key, String? alternate, Boolean decrypt, params String[]? sections);
        public Task<String?> GetValueAsync(String? key, String? alternate, Boolean decrypt, IEnumerable<String>? sections);
        public Task<String?> GetValueAsync(String? key, String? alternate, Boolean decrypt, CancellationToken token, params String[]? sections);
        public Task<String?> GetValueAsync(String? key, String? alternate, Boolean decrypt, IEnumerable<String>? sections, CancellationToken token);
        public Task<String?> GetValueAsync(String? key, String? alternate, Boolean decrypt, ICryptKey? crypt, params String[]? sections);
        public Task<String?> GetValueAsync(String? key, String? alternate, Boolean decrypt, ICryptKey? crypt, IEnumerable<String>? sections);
        public Task<String?> GetValueAsync(String? key, String? alternate, Boolean decrypt, ICryptKey? crypt, CancellationToken token, params String[]? sections);
        public Task<String?> GetValueAsync(String? key, String? alternate, Boolean decrypt, ICryptKey? crypt, IEnumerable<String>? sections, CancellationToken token);
        public T? GetValue<T>(String? key, params String[]? sections);
        public T? GetValue<T>(String? key, IEnumerable<String>? sections);
        public T? GetValue<T>(String? key, T alternate, params String[]? sections);
        public T? GetValue<T>(String? key, T alternate, IEnumerable<String>? sections);
        public T? GetValue<T>(String? key, T alternate, ICryptKey? crypt, params String[]? sections);
        public T? GetValue<T>(String? key, T alternate, ICryptKey? crypt, IEnumerable<String>? sections);
        public T? GetValue<T>(String? key, T alternate, TryConverter<String?, T>? converter, params String[]? sections);
        public T? GetValue<T>(String? key, T alternate, TryConverter<String?, T>? converter, IEnumerable<String>? sections);
        public T? GetValue<T>(String? key, T alternate, ICryptKey? crypt, TryConverter<String?, T>? converter, params String[]? sections);
        public T? GetValue<T>(String? key, T alternate, ICryptKey? crypt, TryConverter<String?, T>? converter, IEnumerable<String>? sections);
        public Task<T?> GetValueAsync<T>(String key, params String[]? sections);
        public Task<T?> GetValueAsync<T>(String? key, IEnumerable<String>? sections);
        public Task<T?> GetValueAsync<T>(String? key, CancellationToken token, params String[]? sections);
        public Task<T?> GetValueAsync<T>(String? key, IEnumerable<String>? sections, CancellationToken token);
        public Task<T?> GetValueAsync<T>(String? key, T alternate, params String[]? sections);
        public Task<T?> GetValueAsync<T>(String? key, T alternate, IEnumerable<String>? sections);
        public Task<T?> GetValueAsync<T>(String? key, T alternate, CancellationToken token, params String[]? sections);
        public Task<T?> GetValueAsync<T>(String? key, T alternate, IEnumerable<String>? sections, CancellationToken token);
        public Task<T?> GetValueAsync<T>(String? key, T alternate, ICryptKey? crypt, params String[]? sections);
        public Task<T?> GetValueAsync<T>(String? key, T alternate, ICryptKey? crypt, IEnumerable<String>? sections);
        public Task<T?> GetValueAsync<T>(String? key, T alternate, ICryptKey? crypt, CancellationToken token, params String[]? sections);
        public Task<T?> GetValueAsync<T>(String? key, T alternate, ICryptKey? crypt, IEnumerable<String>? sections, CancellationToken token);
        public Task<T?> GetValueAsync<T>(String? key, T alternate, TryConverter<String?, T>? converter, params String[]? sections);
        public Task<T?> GetValueAsync<T>(String? key, T alternate, TryConverter<String?, T>? converter, IEnumerable<String>? sections);
        public Task<T?> GetValueAsync<T>(String? key, T alternate, TryConverter<String?, T>? converter, CancellationToken token, params String[]? sections);
        public Task<T?> GetValueAsync<T>(String? key, T alternate, TryConverter<String?, T>? converter, IEnumerable<String>? sections, CancellationToken token);
        public Task<T?> GetValueAsync<T>(String? key, T alternate, ICryptKey? crypt, TryConverter<String?, T>? converter, params String[]? sections);
        public Task<T?> GetValueAsync<T>(String? key, T alternate, ICryptKey? crypt, TryConverter<String?, T>? converter, IEnumerable<String>? sections);
        public Task<T?> GetValueAsync<T>(String? key, T alternate, ICryptKey? crypt, TryConverter<String?, T>? converter, CancellationToken token, params String[]? sections);
        public Task<T?> GetValueAsync<T>(String? key, T alternate, ICryptKey? crypt, TryConverter<String?, T>? converter, IEnumerable<String>? sections, CancellationToken token);
        public Boolean KeyExist(String? key, params String[]? sections);
        public Boolean KeyExist(String? key, IEnumerable<String>? sections);
        public Task<Boolean> KeyExistAsync(String? key, params String[]? sections);
        public Task<Boolean> KeyExistAsync(String? key, IEnumerable<String>? sections);
        public Task<Boolean> KeyExistAsync(String? key, CancellationToken token, params String[]? sections);
        public Task<Boolean> KeyExistAsync(String? key, IEnumerable<String>? sections, CancellationToken token);
        public String?[]? GetExistKeys();
        public Task<String?[]?> GetExistKeysAsync();
        public Task<String?[]?> GetExistKeysAsync(CancellationToken token);

        public Boolean SetValue<T>(String? key, T value, params String[]? sections);
        public Boolean SetValue<T>(String? key, T value, IEnumerable<String>? sections);
        public Boolean SetValue<T>(String? key, T value, ICryptKey? crypt, params String[]? sections);
        public Boolean SetValue<T>(String? key, T value, ICryptKey? crypt, IEnumerable<String>? sections);
        public Task<Boolean> SetValueAsync<T>(String? key, T value, params String[]? sections);
        public Task<Boolean> SetValueAsync<T>(String? key, T value, IEnumerable<String>? sections);
        public Task<Boolean> SetValueAsync<T>(String? key, T value, CancellationToken token, params String[]? sections);
        public Task<Boolean> SetValueAsync<T>(String? key, T value, IEnumerable<String>? sections, CancellationToken token);
        public Task<Boolean> SetValueAsync<T>(String? key, T value, ICryptKey? crypt, params String[]? sections);
        public Task<Boolean> SetValueAsync<T>(String? key, T value, ICryptKey? crypt, IEnumerable<String>? sections);
        public Task<Boolean> SetValueAsync<T>(String? key, T value, ICryptKey? crypt, CancellationToken token, params String[]? sections);
        public Task<Boolean> SetValueAsync<T>(String? key, T value, ICryptKey? crypt, IEnumerable<String>? sections, CancellationToken token);
        public String? GetOrSetValue(String? key, String? value, params String[]? sections);
        public String? GetOrSetValue(String? key, String? value, IEnumerable<String>? sections);
        public String? GetOrSetValue(String? key, String? value, CryptAction crypt, params String[]? sections);
        public String? GetOrSetValue(String? key, String? value, CryptAction crypt, IEnumerable<String>? sections);
        public String? GetOrSetValue(String? key, String? value, CryptAction crypt, ICryptKey? cryptkey, params String[]? sections);
        public String? GetOrSetValue(String? key, String? value, CryptAction crypt, ICryptKey? cryptkey, IEnumerable<String>? sections);
        public Task<String?> GetOrSetValueAsync(String? key, String? value, params String[]? sections);
        public Task<String?> GetOrSetValueAsync(String? key, String? value, IEnumerable<String>? sections);
        public Task<String?> GetOrSetValueAsync(String? key, String? value, CancellationToken token, params String[]? sections);
        public Task<String?> GetOrSetValueAsync(String? key, String? value, IEnumerable<String>? sections, CancellationToken token);
        public Task<String?> GetOrSetValueAsync(String? key, String? value, CryptAction crypt, params String[]? sections);
        public Task<String?> GetOrSetValueAsync(String? key, String? value, CryptAction crypt, IEnumerable<String>? sections);
        public Task<String?> GetOrSetValueAsync(String? key, String? value, CryptAction crypt, CancellationToken token, params String[]? sections);
        public Task<String?> GetOrSetValueAsync(String? key, String? value, CryptAction crypt, IEnumerable<String>? sections, CancellationToken token);
        public Task<String?> GetOrSetValueAsync(String? key, String? value, CryptAction crypt, ICryptKey? cryptkey, params String[]? sections);
        public Task<String?> GetOrSetValueAsync(String? key, String? value, CryptAction crypt, ICryptKey? cryptkey, IEnumerable<String>? sections);
        public Task<String?> GetOrSetValueAsync(String? key, String? value, CryptAction crypt, ICryptKey? cryptkey, CancellationToken token, params String[]? sections);
        public Task<String?> GetOrSetValueAsync(String? key, String? value, CryptAction crypt, ICryptKey? cryptkey, IEnumerable<String>? sections, CancellationToken token);
        public T? GetOrSetValue<T>(String? key, T value, params String[]? sections);
        public T? GetOrSetValue<T>(String? key, T value, IEnumerable<String>? sections);
        public T? GetOrSetValue<T>(String? key, T value, ICryptKey? crypt, params String[]? sections);
        public T? GetOrSetValue<T>(String? key, T value, ICryptKey? crypt, IEnumerable<String>? sections);
        public T? GetOrSetValue<T>(String? key, T value, TryConverter<String?, T>? converter, params String[]? sections);
        public T? GetOrSetValue<T>(String? key, T value, TryConverter<String?, T>? converter, IEnumerable<String>? sections);
        public T? GetOrSetValue<T>(String? key, T value, ICryptKey? crypt, TryConverter<String?, T>? converter, params String[]? sections);
        public T? GetOrSetValue<T>(String? key, T value, ICryptKey? crypt, TryConverter<String?, T>? converter, IEnumerable<String>? sections);
        public Task<T?> GetOrSetValueAsync<T>(String? key, T value, params String[]? sections);
        public Task<T?> GetOrSetValueAsync<T>(String? key, T value, IEnumerable<String>? sections);
        public Task<T?> GetOrSetValueAsync<T>(String? key, T value, CancellationToken token, params String[]? sections);
        public Task<T?> GetOrSetValueAsync<T>(String? key, T value, IEnumerable<String>? sections, CancellationToken token);
        public Task<T?> GetOrSetValueAsync<T>(String? key, T value, ICryptKey? crypt, params String[]? sections);
        public Task<T?> GetOrSetValueAsync<T>(String? key, T value, ICryptKey? crypt, IEnumerable<String>? sections);
        public Task<T?> GetOrSetValueAsync<T>(String? key, T value, ICryptKey? crypt, CancellationToken token, params String[]? sections);
        public Task<T?> GetOrSetValueAsync<T>(String? key, T value, ICryptKey? crypt, IEnumerable<String>? sections, CancellationToken token);
        public Task<T?> GetOrSetValueAsync<T>(String? key, T value, TryConverter<String?, T>? converter, params String[]? sections);
        public Task<T?> GetOrSetValueAsync<T>(String? key, T value, TryConverter<String?, T>? converter, IEnumerable<String>? sections);
        public Task<T?> GetOrSetValueAsync<T>(String? key, T value, TryConverter<String?, T>? converter, CancellationToken token, params String[]? sections);
        public Task<T?> GetOrSetValueAsync<T>(String? key, T value, TryConverter<String?, T>? converter, IEnumerable<String>? sections, CancellationToken token);
        public Task<T?> GetOrSetValueAsync<T>(String? key, T value, ICryptKey? crypt, TryConverter<String?, T>? converter, params String[]? sections);
        public Task<T?> GetOrSetValueAsync<T>(String? key, T value, ICryptKey? crypt, TryConverter<String?, T>? converter, IEnumerable<String>? sections);
        public Task<T?> GetOrSetValueAsync<T>(String? key, T value, ICryptKey? crypt, TryConverter<String?, T>? converter, CancellationToken token, params String[]? sections);
        public Task<T?> GetOrSetValueAsync<T>(String? key, T value, ICryptKey? crypt, TryConverter<String?, T>? converter, IEnumerable<String>? sections, CancellationToken token);
        public Boolean RemoveValue(String? key, params String[]? sections);
        public Boolean RemoveValue(String? key, IEnumerable<String>? sections);
        public Task<Boolean> RemoveValueAsync(String? key, params String[]? sections);
        public Task<Boolean> RemoveValueAsync(String? key, IEnumerable<String>? sections);
        public Task<Boolean> RemoveValueAsync(String? key, CancellationToken token, params String[]? sections);
        public Task<Boolean> RemoveValueAsync(String? key, IEnumerable<String>? sections, CancellationToken token);
    }
}