// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using NetExtender.Configuration.Interfaces;

namespace NetExtender.Configuration.Convert.Interfaces
{
    public interface IConverterConfig : IConfig
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
        public Boolean SetValue<T>(String? key, T value, params String[]? sections);
        public Boolean SetValue<T>(String? key, T value, IEnumerable<String>? sections);
        public Task<Boolean> SetValueAsync<T>(String? key, T value, params String[]? sections);
        public Task<Boolean> SetValueAsync<T>(String? key, T value, IEnumerable<String>? sections);
        public Task<Boolean> SetValueAsync<T>(String? key, T value, CancellationToken token, params String[]? sections);
        public Task<Boolean> SetValueAsync<T>(String? key, T value, IEnumerable<String>? sections, CancellationToken token);
        public T? GetOrSetValue<T>(String? key, T value, params String[]? sections);
        public T? GetOrSetValue<T>(String? key, T value, IEnumerable<String>? sections);
        public T? GetOrSetValue<T>(String? key, T value, TryConverter<String, T>? converter, params String[]? sections);
        public T? GetOrSetValue<T>(String? key, T value, TryConverter<String, T>? converter, IEnumerable<String>? sections);
        public Task<T?> GetOrSetValueAsync<T>(String? key, T value, params String[]? sections);
        public Task<T?> GetOrSetValueAsync<T>(String? key, T value, IEnumerable<String>? sections);
        public Task<T?> GetOrSetValueAsync<T>(String? key, T value, CancellationToken token, params String[]? sections);
        public Task<T?> GetOrSetValueAsync<T>(String? key, T value, IEnumerable<String>? sections, CancellationToken token);
        public Task<T?> GetOrSetValueAsync<T>(String? key, T value, TryConverter<String, T>? converter, params String[]? sections);
        public Task<T?> GetOrSetValueAsync<T>(String? key, T value, TryConverter<String, T>? converter, IEnumerable<String>? sections);
        public Task<T?> GetOrSetValueAsync<T>(String? key, T value, TryConverter<String, T>? converter, CancellationToken token, params String[]? sections);
        public Task<T?> GetOrSetValueAsync<T>(String? key, T value, TryConverter<String, T>? converter, IEnumerable<String>? sections, CancellationToken token);
    }
}