// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using NetExtender.Configuration.Convert.Interfaces;
using NetExtender.Configuration.Interfaces;
using NetExtender.Crypto.CryptKey.Interfaces;

namespace NetExtender.Configuration.Cryptography.Interfaces
{
    public interface IReadOnlyCryptographyConverterConfig : IReadOnlyConverterConfig, IReadOnlyCryptographyConfig
    {
        public T? GetValue<T>(String? key, IStringCryptor? cryptor, params String[]? sections);
        public T? GetValue<T>(String? key, IStringCryptor? cryptor, IEnumerable<String>? sections);
        public T GetValue<T>(String? key, T alternate, IStringCryptor? cryptor, params String[]? sections);
        public T GetValue<T>(String? key, T alternate, IStringCryptor? cryptor, IEnumerable<String>? sections);
        public T GetValue<T>(String? key, T alternate, IStringCryptor? cryptor, TryConverter<String, T>? converter, params String[]? sections);
        public T GetValue<T>(String? key, T alternate, IStringCryptor? cryptor, TryConverter<String, T>? converter, IEnumerable<String>? sections);
        public T? GetRawValue<T>(String? key, params String[]? sections);
        public T? GetRawValue<T>(String? key, IEnumerable<String>? sections);
        public T? GetRawValue<T>(String? key, TryConverter<String, T>? converter, params String[]? sections);
        public T? GetRawValue<T>(String? key, TryConverter<String, T>? converter, IEnumerable<String>? sections);
        public T GetRawValue<T>(String? key, T alternate, params String[]? sections);
        public T GetRawValue<T>(String? key, T alternate, IEnumerable<String>? sections);
        public T GetRawValue<T>(String? key, T alternate, TryConverter<String, T>? converter, params String[]? sections);
        public T GetRawValue<T>(String? key, T alternate, TryConverter<String, T>? converter, IEnumerable<String>? sections);
        public Task<T> GetValueAsync<T>(String? key, IStringCryptor? cryptor, params String[]? sections);
        public Task<T> GetValueAsync<T>(String? key, IStringCryptor? cryptor, IEnumerable<String>? sections);
        public Task<T> GetValueAsync<T>(String? key, IStringCryptor? cryptor, CancellationToken token, params String[]? sections);
        public Task<T> GetValueAsync<T>(String? key, IStringCryptor? cryptor, IEnumerable<String>? sections, CancellationToken token);
        public Task<T> GetValueAsync<T>(String? key, T alternate, IStringCryptor? cryptor, params String[]? sections);
        public Task<T> GetValueAsync<T>(String? key, T alternate, IStringCryptor? cryptor, IEnumerable<String>? sections);
        public Task<T> GetValueAsync<T>(String? key, T alternate, IStringCryptor? cryptor, CancellationToken token, params String[]? sections);
        public Task<T> GetValueAsync<T>(String? key, T alternate, IStringCryptor? cryptor, IEnumerable<String>? sections, CancellationToken token);
        public Task<T> GetValueAsync<T>(String? key, T alternate, IStringCryptor? cryptor, TryConverter<String, T>? converter, params String[]? sections);
        public Task<T> GetValueAsync<T>(String? key, T alternate, IStringCryptor? cryptor, TryConverter<String, T>? converter, IEnumerable<String>? sections);
        public Task<T> GetValueAsync<T>(String? key, T alternate, IStringCryptor? cryptor, TryConverter<String, T>? converter, CancellationToken token, params String[]? sections);
        public Task<T> GetValueAsync<T>(String? key, T alternate, IStringCryptor? cryptor, TryConverter<String, T>? converter, IEnumerable<String>? sections, CancellationToken token);
        public Task<T?> GetRawValueAsync<T>(String? key, params String[]? sections);
        public Task<T?> GetRawValueAsync<T>(String? key, IEnumerable<String>? sections);
        public Task<T?> GetRawValueAsync<T>(String? key, CancellationToken token, params String[]? sections);
        public Task<T?> GetRawValueAsync<T>(String? key, IEnumerable<String>? sections, CancellationToken token);
        public Task<T?> GetRawValueAsync<T>(String? key, TryConverter<String, T>? converter, params String[]? sections);
        public Task<T?> GetRawValueAsync<T>(String? key, TryConverter<String, T>? converter, IEnumerable<String>? sections);
        public Task<T?> GetRawValueAsync<T>(String? key, TryConverter<String, T>? converter, CancellationToken token, params String[]? sections);
        public Task<T?> GetRawValueAsync<T>(String? key, TryConverter<String, T>? converter, IEnumerable<String>? sections, CancellationToken token);
        public Task<T> GetRawValueAsync<T>(String? key, T alternate, params String[]? sections);
        public Task<T> GetRawValueAsync<T>(String? key, T alternate, IEnumerable<String>? sections);
        public Task<T> GetRawValueAsync<T>(String? key, T alternate, CancellationToken token, params String[]? sections);
        public Task<T> GetRawValueAsync<T>(String? key, T alternate, IEnumerable<String>? sections, CancellationToken token);
        public Task<T> GetRawValueAsync<T>(String? key, T alternate, TryConverter<String, T>? converter, params String[]? sections);
        public Task<T> GetRawValueAsync<T>(String? key, T alternate, TryConverter<String, T>? converter, IEnumerable<String>? sections);
        public Task<T> GetRawValueAsync<T>(String? key, T alternate, TryConverter<String, T>? converter, CancellationToken token, params String[]? sections);
        public Task<T> GetRawValueAsync<T>(String? key, T alternate, TryConverter<String, T>? converter, IEnumerable<String>? sections, CancellationToken token);
    }
}