// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Threading;
using System.Threading.Tasks;
using NetExtender.Configuration.Interfaces;
using NetExtender.Utilities.Types;

namespace NetExtender.Configuration.Utilities
{
    [SuppressMessage("ReSharper", "ConvertIfStatementToSwitchStatement")]
    public static class ConfigurationConverterUtilities
    {
        public static T? GetValue<T>(this IReadOnlyConfig config, String? key, params String[]? sections)
        {
            return GetValue<T>(config, key, (IEnumerable<String>?) sections);
        }

        public static T? GetValue<T>(this IReadOnlyConfig config, String? key, IEnumerable<String>? sections)
        {
            return GetValue(config, key, default(T), sections);
        }

        public static T? GetValue<T>(this IReadOnlyConfig config, String? key, TryConverter<String, T>? converter, params String[]? sections)
        {
            return GetValue(config, key, converter, (IEnumerable<String>?) sections);
        }

        public static T? GetValue<T>(this IReadOnlyConfig config, String? key, TryConverter<String, T>? converter, IEnumerable<String>? sections)
        {
            return GetValue(config, key, default, converter!, sections);
        }

        public static T GetValue<T>(this IReadOnlyConfig config, String? key, T alternate, params String[]? sections)
        {
            return GetValue(config, key, alternate, (IEnumerable<String>?) sections);
        }

        public static T GetValue<T>(this IReadOnlyConfig config, String? key, T alternate, IEnumerable<String>? sections)
        {
            return GetValue(config, key, alternate, null, sections);
        }

        public static T GetValue<T>(this IReadOnlyConfig config, String? key, T alternate, TryConverter<String, T>? converter, params String[]? sections)
        {
            return GetValue(config, key, alternate, converter, (IEnumerable<String>?) sections);
        }

        public static T GetValue<T>(this IReadOnlyConfig config, String? key, T alternate, TryConverter<String, T>? converter, IEnumerable<String>? sections)
        {
            if (config is null)
            {
                throw new ArgumentNullException(nameof(config));
            }

            if (config is IReadOnlyConverterConfig configuration)
            {
                return configuration.GetValue(key, alternate, converter, sections);
            }

            String? value = config.GetValue(key, sections);

            if (value is null)
            {
                return alternate;
            }

            converter ??= ConvertUtilities.TryConvert;
            return converter(value, out T? result) ? result : alternate;
        }

        public static Task<T?> GetValueAsync<T>(this IReadOnlyConfig config, String? key, params String[]? sections)
        {
            return GetValueAsync<T>(config, key, (IEnumerable<String>?) sections);
        }

        public static Task<T?> GetValueAsync<T>(this IReadOnlyConfig config, String? key, IEnumerable<String>? sections)
        {
            return GetValueAsync<T>(config, key, sections, CancellationToken.None);
        }

        public static Task<T?> GetValueAsync<T>(this IReadOnlyConfig config, String? key, CancellationToken token, params String[]? sections)
        {
            return GetValueAsync<T>(config, key, sections, token);
        }

        public static Task<T?> GetValueAsync<T>(this IReadOnlyConfig config, String? key, IEnumerable<String>? sections, CancellationToken token)
        {
            return GetValueAsync(config, key, default(T), sections, token);
        }

        public static Task<T?> GetValueAsync<T>(this IReadOnlyConfig config, String? key, TryConverter<String, T>? converter, params String[]? sections)
        {
            return GetValueAsync(config, key, converter, (IEnumerable<String>?) sections);
        }

        public static Task<T?> GetValueAsync<T>(this IReadOnlyConfig config, String? key, TryConverter<String, T>? converter, IEnumerable<String>? sections)
        {
            return GetValueAsync(config, key, converter, sections, CancellationToken.None);
        }

        public static Task<T?> GetValueAsync<T>(this IReadOnlyConfig config, String? key, TryConverter<String, T>? converter, CancellationToken token, params String[]? sections)
        {
            return GetValueAsync(config, key, converter, sections, token);
        }

        public static Task<T?> GetValueAsync<T>(this IReadOnlyConfig config, String? key, TryConverter<String, T>? converter, IEnumerable<String>? sections, CancellationToken token)
        {
            return GetValueAsync(config, key, default, converter!, sections, token);
        }

        public static Task<T> GetValueAsync<T>(this IReadOnlyConfig config, String? key, T alternate, params String[]? sections)
        {
            return GetValueAsync(config, key, alternate, (IEnumerable<String>?) sections);
        }

        public static Task<T> GetValueAsync<T>(this IReadOnlyConfig config, String? key, T alternate, IEnumerable<String>? sections)
        {
            return GetValueAsync(config, key, alternate, sections, CancellationToken.None);
        }

        public static Task<T> GetValueAsync<T>(this IReadOnlyConfig config, String? key, T alternate, CancellationToken token, params String[]? sections)
        {
            return GetValueAsync(config, key, alternate, sections, token);
        }

        public static Task<T> GetValueAsync<T>(this IReadOnlyConfig config, String? key, T alternate, IEnumerable<String>? sections, CancellationToken token)
        {
            return GetValueAsync(config, key, alternate, null, sections, token);
        }

        public static Task<T> GetValueAsync<T>(this IReadOnlyConfig config, String? key, T alternate, TryConverter<String, T>? converter, params String[]? sections)
        {
            return GetValueAsync(config, key, alternate, converter, (IEnumerable<String>?) sections);
        }

        public static Task<T> GetValueAsync<T>(this IReadOnlyConfig config, String? key, T alternate, TryConverter<String, T>? converter, IEnumerable<String>? sections)
        {
            return GetValueAsync(config, key, alternate, converter, sections, CancellationToken.None);
        }

        public static Task<T> GetValueAsync<T>(this IReadOnlyConfig config, String? key, T alternate, TryConverter<String, T>? converter, CancellationToken token, params String[]? sections)
        {
            return GetValueAsync(config, key, alternate, converter, sections, token);
        }

        public static async Task<T> GetValueAsync<T>(this IReadOnlyConfig config, String? key, T alternate, TryConverter<String, T>? converter, IEnumerable<String>? sections, CancellationToken token)
        {
            if (config is null)
            {
                throw new ArgumentNullException(nameof(config));
            }

            if (config is IReadOnlyConverterConfig configuration)
            {
                return await configuration.GetValueAsync(key, alternate, converter, sections, token).ConfigureAwait(false);
            }

            String? value = await config.GetValueAsync(key, sections, token).ConfigureAwait(false);

            if (value is null)
            {
                return alternate;
            }

            converter ??= ConvertUtilities.TryConvert;
            return converter(value, out T? result) ? result : alternate;
        }

        public static T? GetValue<T>(this IConfig config, String? key, params String[]? sections)
        {
            return GetValue<T>(config, key, (IEnumerable<String>?) sections);
        }

        public static T? GetValue<T>(this IConfig config, String? key, IEnumerable<String>? sections)
        {
            return GetValue(config, key, default(T), sections);
        }

        public static T? GetValue<T>(this IConfig config, String? key, TryConverter<String, T>? converter, params String[]? sections)
        {
            return GetValue(config, key, converter, (IEnumerable<String>?) sections);
        }

        public static T? GetValue<T>(this IConfig config, String? key, TryConverter<String, T>? converter, IEnumerable<String>? sections)
        {
            return GetValue(config, key, default, converter!, sections);
        }

        public static T GetValue<T>(this IConfig config, String? key, T alternate, params String[]? sections)
        {
            return GetValue(config, key, alternate, (IEnumerable<String>?) sections);
        }

        public static T GetValue<T>(this IConfig config, String? key, T alternate, IEnumerable<String>? sections)
        {
            return GetValue(config, key, alternate, null, sections);
        }

        public static T GetValue<T>(this IConfig config, String? key, T alternate, TryConverter<String, T>? converter, params String[]? sections)
        {
            return GetValue(config, key, alternate, converter, (IEnumerable<String>?) sections);
        }

        public static T GetValue<T>(this IConfig config, String? key, T alternate, TryConverter<String, T>? converter, IEnumerable<String>? sections)
        {
            if (config is null)
            {
                throw new ArgumentNullException(nameof(config));
            }

            if (config is IConverterConfig configuration)
            {
                return configuration.GetValue(key, alternate, converter, sections);
            }

            String? value = config.GetValue(key, sections);

            if (value is null)
            {
                return alternate;
            }

            converter ??= ConvertUtilities.TryConvert;
            return converter(value, out T? result) ? result : alternate;
        }

        public static Task<T?> GetValueAsync<T>(this IConfig config, String? key, params String[]? sections)
        {
            return GetValueAsync<T>(config, key, (IEnumerable<String>?) sections);
        }

        public static Task<T?> GetValueAsync<T>(this IConfig config, String? key, IEnumerable<String>? sections)
        {
            return GetValueAsync<T>(config, key, sections, CancellationToken.None);
        }

        public static Task<T?> GetValueAsync<T>(this IConfig config, String? key, CancellationToken token, params String[]? sections)
        {
            return GetValueAsync<T>(config, key, sections, token);
        }

        public static Task<T?> GetValueAsync<T>(this IConfig config, String? key, IEnumerable<String>? sections, CancellationToken token)
        {
            return GetValueAsync(config, key, default(T), sections, token);
        }

        public static Task<T?> GetValueAsync<T>(this IConfig config, String? key, TryConverter<String, T>? converter, params String[]? sections)
        {
            return GetValueAsync(config, key, converter, (IEnumerable<String>?) sections);
        }

        public static Task<T?> GetValueAsync<T>(this IConfig config, String? key, TryConverter<String, T>? converter, IEnumerable<String>? sections)
        {
            return GetValueAsync(config, key, converter, sections, CancellationToken.None);
        }

        public static Task<T?> GetValueAsync<T>(this IConfig config, String? key, TryConverter<String, T>? converter, CancellationToken token, params String[]? sections)
        {
            return GetValueAsync(config, key, converter, sections, token);
        }

        public static Task<T?> GetValueAsync<T>(this IConfig config, String? key, TryConverter<String, T>? converter, IEnumerable<String>? sections, CancellationToken token)
        {
            return GetValueAsync(config, key, default, converter!, sections, token);
        }

        public static Task<T> GetValueAsync<T>(this IConfig config, String? key, T alternate, params String[]? sections)
        {
            return GetValueAsync(config, key, alternate, (IEnumerable<String>?) sections);
        }

        public static Task<T> GetValueAsync<T>(this IConfig config, String? key, T alternate, IEnumerable<String>? sections)
        {
            return GetValueAsync(config, key, alternate, sections, CancellationToken.None);
        }

        public static Task<T> GetValueAsync<T>(this IConfig config, String? key, T alternate, CancellationToken token, params String[]? sections)
        {
            return GetValueAsync(config, key, alternate, sections, token);
        }

        public static Task<T> GetValueAsync<T>(this IConfig config, String? key, T alternate, IEnumerable<String>? sections, CancellationToken token)
        {
            return GetValueAsync(config, key, alternate, null, sections, token);
        }

        public static Task<T> GetValueAsync<T>(this IConfig config, String? key, T alternate, TryConverter<String, T>? converter, params String[]? sections)
        {
            return GetValueAsync(config, key, alternate, converter, (IEnumerable<String>?) sections);
        }

        public static Task<T> GetValueAsync<T>(this IConfig config, String? key, T alternate, TryConverter<String, T>? converter, IEnumerable<String>? sections)
        {
            return GetValueAsync(config, key, alternate, converter, sections, CancellationToken.None);
        }

        public static Task<T> GetValueAsync<T>(this IConfig config, String? key, T alternate, TryConverter<String, T>? converter, CancellationToken token, params String[]? sections)
        {
            return GetValueAsync(config, key, alternate, converter, sections, token);
        }

        public static async Task<T> GetValueAsync<T>(this IConfig config, String? key, T alternate, TryConverter<String, T>? converter, IEnumerable<String>? sections, CancellationToken token)
        {
            if (config is null)
            {
                throw new ArgumentNullException(nameof(config));
            }

            if (config is IConverterConfig configuration)
            {
                return await configuration.GetValueAsync(key, alternate, converter, sections, token).ConfigureAwait(false);
            }

            String? value = await config.GetValueAsync(key, sections, token).ConfigureAwait(false);

            if (value is null)
            {
                return alternate;
            }

            converter ??= ConvertUtilities.TryConvert;
            return converter(value, out T? result) ? result : alternate;
        }

        public static Boolean SetValue<T>(this IConfig config, String? key, T value, params String[]? sections)
        {
            return SetValue(config, key, value, (IEnumerable<String>?) sections);
        }

        public static Boolean SetValue<T>(this IConfig config, String? key, T value, IEnumerable<String>? sections)
        {
            if (config is null)
            {
                throw new ArgumentNullException(nameof(config));
            }

            if (config is IConverterConfig configuration)
            {
                return configuration.SetValue(key, value, sections);
            }

            if (value is null)
            {
                return config.SetValue(key, null, sections);
            }

            String? convert = value.GetString();
            return convert is not null && config.SetValue(key, convert, sections);
        }

        public static Task<Boolean> SetValueAsync<T>(this IConfig config, String? key, T value, params String[]? sections)
        {
            return SetValueAsync(config, key, value, (IEnumerable<String>?) sections);
        }

        public static Task<Boolean> SetValueAsync<T>(this IConfig config, String? key, T value, IEnumerable<String>? sections)
        {
            return SetValueAsync(config, key, value, sections, CancellationToken.None);
        }

        public static Task<Boolean> SetValueAsync<T>(this IConfig config, String? key, T value, CancellationToken token, params String[]? sections)
        {
            return SetValueAsync(config, key, value, sections, token);
        }

        public static async Task<Boolean> SetValueAsync<T>(this IConfig config, String? key, T value, IEnumerable<String>? sections, CancellationToken token)
        {
            if (config is null)
            {
                throw new ArgumentNullException(nameof(config));
            }

            if (config is IConverterConfig configuration)
            {
                return await configuration.SetValueAsync(key, value, sections, token).ConfigureAwait(false);
            }

            if (value is null)
            {
                return await config.SetValueAsync(key, null, sections, token).ConfigureAwait(false);
            }

            String? convert = value.GetString();
            return convert is not null && await config.SetValueAsync(key, convert, sections, token).ConfigureAwait(false);
        }

        public static T? GetOrSetValue<T>(this IConfig config, String? key, T value, params String[]? sections)
        {
            return GetOrSetValue(config, key, value, (IEnumerable<String>?) sections);
        }

        public static T? GetOrSetValue<T>(this IConfig config, String? key, T value, IEnumerable<String>? sections)
        {
            return GetOrSetValue(config, key, value, null, sections);
        }

        public static T? GetOrSetValue<T>(this IConfig config, String? key, T value, TryConverter<String, T>? converter, params String[]? sections)
        {
            return GetOrSetValue(config, key, value, converter, (IEnumerable<String>?) sections);
        }

        public static T? GetOrSetValue<T>(this IConfig config, String? key, T value, TryConverter<String, T>? converter, IEnumerable<String>? sections)
        {
            if (config is null)
            {
                throw new ArgumentNullException(nameof(config));
            }

            if (config is IConverterConfig configuration)
            {
                return configuration.GetOrSetValue(key, value, converter, sections);
            }

            String? convert = value?.GetString();
            String? get = config.GetOrSetValue(key, convert, sections);

            if (get is null)
            {
                return default;
            }

            converter ??= ConvertUtilities.TryConvert;
            return converter(get, out T? result) ? result : default;
        }

        public static Task<T?> GetOrSetValueAsync<T>(this IConfig config, String? key, T value, params String[]? sections)
        {
            return GetOrSetValueAsync(config, key, value, (IEnumerable<String>?) sections);
        }

        public static Task<T?> GetOrSetValueAsync<T>(this IConfig config, String? key, T value, IEnumerable<String>? sections)
        {
            return GetOrSetValueAsync(config, key, value, sections, CancellationToken.None);
        }

        public static Task<T?> GetOrSetValueAsync<T>(this IConfig config, String? key, T value, CancellationToken token, params String[]? sections)
        {
            return GetOrSetValueAsync(config, key, value, sections, token);
        }

        public static Task<T?> GetOrSetValueAsync<T>(this IConfig config, String? key, T value, IEnumerable<String>? sections, CancellationToken token)
        {
            return GetOrSetValueAsync(config, key, value, null, sections, token);
        }

        public static Task<T?> GetOrSetValueAsync<T>(this IConfig config, String? key, T value, TryConverter<String, T>? converter, params String[]? sections)
        {
            return GetOrSetValueAsync(config, key, value, converter, (IEnumerable<String>?) sections);
        }

        public static Task<T?> GetOrSetValueAsync<T>(this IConfig config, String? key, T value, TryConverter<String, T>? converter, IEnumerable<String>? sections)
        {
            return GetOrSetValueAsync(config, key, value, converter, sections, CancellationToken.None);
        }

        public static Task<T?> GetOrSetValueAsync<T>(this IConfig config, String? key, T value, TryConverter<String, T>? converter, CancellationToken token, params String[]? sections)
        {
            return GetOrSetValueAsync(config, key, value, converter, sections, token);
        }

        public static async Task<T?> GetOrSetValueAsync<T>(this IConfig config, String? key, T value, TryConverter<String, T>? converter, IEnumerable<String>? sections, CancellationToken token)
        {
            if (config is null)
            {
                throw new ArgumentNullException(nameof(config));
            }

            if (config is IConverterConfig configuration)
            {
                return await configuration.GetOrSetValueAsync(key, value, converter, sections, token).ConfigureAwait(false);
            }

            String? convert = value?.GetString();
            String? get = await config.GetOrSetValueAsync(key, convert, sections, token).ConfigureAwait(false);

            if (get is null)
            {
                return default;
            }

            converter ??= ConvertUtilities.TryConvert;
            return converter(get, out T? result) ? result : default;
        }
    }
}