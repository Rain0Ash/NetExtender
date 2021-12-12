// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace NetExtender.Configuration.Interfaces
{
    public interface IReadOnlyConverterConfig : IReadOnlyConfig
    {
        public T? GetValue<T>(String? key, params String[]? sections);
        public T? GetValue<T>(String? key, IEnumerable<String>? sections);
        public T? GetValue<T>(String? key, TryConverter<String, T>? converter, params String[]? sections);
        public T? GetValue<T>(String? key, TryConverter<String, T>? converter, IEnumerable<String>? sections);
        public T GetValue<T>(String? key, T alternate, params String[]? sections);
        public T GetValue<T>(String? key, T alternate, IEnumerable<String>? sections);
        public T GetValue<T>(String? key, T alternate, TryConverter<String, T>? converter, params String[]? sections);
        public T GetValue<T>(String? key, T alternate, TryConverter<String, T>? converter, IEnumerable<String>? sections);
        public Task<T?> GetValueAsync<T>(String? key, params String[]? sections);
        public Task<T?> GetValueAsync<T>(String? key, IEnumerable<String>? sections);
        public Task<T?> GetValueAsync<T>(String? key, CancellationToken token, params String[]? sections);
        public Task<T?> GetValueAsync<T>(String? key, IEnumerable<String>? sections, CancellationToken token);
        public Task<T?> GetValueAsync<T>(String? key, TryConverter<String, T>? converter, params String[]? sections);
        public Task<T?> GetValueAsync<T>(String? key, TryConverter<String, T>? converter, IEnumerable<String>? sections);
        public Task<T?> GetValueAsync<T>(String? key, TryConverter<String, T>? converter, CancellationToken token, params String[]? sections);
        public Task<T?> GetValueAsync<T>(String? key, TryConverter<String, T>? converter, IEnumerable<String>? sections, CancellationToken token);
        public Task<T> GetValueAsync<T>(String? key, T alternate, params String[]? sections);
        public Task<T> GetValueAsync<T>(String? key, T alternate, IEnumerable<String>? sections);
        public Task<T> GetValueAsync<T>(String? key, T alternate, CancellationToken token, params String[]? sections);
        public Task<T> GetValueAsync<T>(String? key, T alternate, IEnumerable<String>? sections, CancellationToken token);
        public Task<T> GetValueAsync<T>(String? key, T alternate, TryConverter<String, T>? converter, params String[]? sections);
        public Task<T> GetValueAsync<T>(String? key, T alternate, TryConverter<String, T>? converter, IEnumerable<String>? sections);
        public Task<T> GetValueAsync<T>(String? key, T alternate, TryConverter<String, T>? converter, CancellationToken token, params String[]? sections);
        public Task<T> GetValueAsync<T>(String? key, T alternate, TryConverter<String, T>? converter, IEnumerable<String>? sections, CancellationToken token);
    }
}