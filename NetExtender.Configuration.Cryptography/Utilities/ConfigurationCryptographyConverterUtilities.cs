// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using NetExtender.Configuration.Cryptography.Interfaces;
using NetExtender.Crypto.CryptKey.Interfaces;
using NetExtender.Utilities.Types;

namespace NetExtender.Configuration.Cryptography.Convert.Utilities
{
    public static class ConfigurationCryptographyConverterUtilities
    {
        public static T? GetValue<T>(this IReadOnlyCryptographyConfig config, String? key, params String[]? sections)
        {
            return GetValue<T>(config, key, (IEnumerable<String>?) sections);
        }

        public static T? GetValue<T>(this IReadOnlyCryptographyConfig config, String? key, IEnumerable<String>? sections)
        {
            return GetValue(config, key, default(T), sections);
        }

        public static T? GetValue<T>(this IReadOnlyCryptographyConfig config, String? key, TryConverter<String, T>? converter, params String[]? sections)
        {
            return GetValue(config, key, converter, (IEnumerable<String>?) sections);
        }

        public static T? GetValue<T>(this IReadOnlyCryptographyConfig config, String? key, TryConverter<String, T>? converter, IEnumerable<String>? sections)
        {
            return GetValue(config, key, default(T), converter!, sections);
        }

        public static T GetValue<T>(this IReadOnlyCryptographyConfig config, String? key, T alternate, params String[]? sections)
        {
            return GetValue(config, key, alternate, (IEnumerable<String>?) sections);
        }

        public static T GetValue<T>(this IReadOnlyCryptographyConfig config, String? key, T alternate, IEnumerable<String>? sections)
        {
            return GetValue(config, key, alternate, (TryConverter<String, T>?) null, sections);
        }

        public static T GetValue<T>(this IReadOnlyCryptographyConfig config, String? key, T alternate, TryConverter<String, T>? converter, params String[]? sections)
        {
            return GetValue(config, key, alternate, converter, (IEnumerable<String>?) sections);
        }

        public static T GetValue<T>(this IReadOnlyCryptographyConfig config, String? key, T alternate, TryConverter<String, T>? converter, IEnumerable<String>? sections)
        {
            if (config is null)
            {
                throw new ArgumentNullException(nameof(config));
            }

            if (config is IReadOnlyCryptographyConverterConfig configuration)
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
        
        public static T? GetValue<T>(this IReadOnlyCryptographyConfig config, String? key, IStringCryptor? cryptor, params String[]? sections)
        {
            return GetValue<T>(config, key, cryptor, (IEnumerable<String>?) sections);
        }

        public static T? GetValue<T>(this IReadOnlyCryptographyConfig config, String? key, IStringCryptor? cryptor, IEnumerable<String>? sections)
        {
            return GetValue<T>(config, key, cryptor, null, sections);
        }

        public static T? GetValue<T>(this IReadOnlyCryptographyConfig config, String? key, IStringCryptor? cryptor, TryConverter<String, T>? converter, params String[]? sections)
        {
            return GetValue(config, key, cryptor, converter, (IEnumerable<String>?) sections);
        }

        public static T? GetValue<T>(this IReadOnlyCryptographyConfig config, String? key, IStringCryptor? cryptor, TryConverter<String, T>? converter, IEnumerable<String>? sections)
        {
            return GetValue(config, key, default, cryptor, converter!, sections);
        }
        
        public static T GetValue<T>(this IReadOnlyCryptographyConfig config, String? key, T alternate, IStringCryptor? cryptor, params String[]? sections)
        {
            return GetValue(config, key, alternate, cryptor, (IEnumerable<String>?) sections);
        }

        public static T GetValue<T>(this IReadOnlyCryptographyConfig config, String? key, T alternate, IStringCryptor? cryptor, IEnumerable<String>? sections)
        {
            return GetValue(config, key, alternate, cryptor, null, sections);
        }

        public static T GetValue<T>(this IReadOnlyCryptographyConfig config, String? key, T alternate, IStringCryptor? cryptor, TryConverter<String, T>? converter, params String[]? sections)
        {
            return GetValue(config, key, alternate, cryptor, converter, (IEnumerable<String>?) sections);
        }

        public static T GetValue<T>(this IReadOnlyCryptographyConfig config, String? key, T alternate, IStringCryptor? cryptor, TryConverter<String, T>? converter, IEnumerable<String>? sections)
        {
            if (config is null)
            {
                throw new ArgumentNullException(nameof(config));
            }
            
            if (config is IReadOnlyCryptographyConverterConfig configuration)
            {
                return configuration.GetValue(key, alternate, cryptor, converter, sections);
            }

            String? value = config.GetValue(key, cryptor, sections);

            if (value is null)
            {
                return alternate;
            }

            converter ??= ConvertUtilities.TryConvert;
            return converter(value, out T? result) ? result : alternate;
        }
        
        public static T? GetRawValue<T>(this IReadOnlyCryptographyConfig config, String? key, params String[]? sections)
        {
            return GetRawValue<T>(config, key, (IEnumerable<String>?) sections);
        }

        public static T? GetRawValue<T>(this IReadOnlyCryptographyConfig config, String? key, IEnumerable<String>? sections)
        {
            return GetRawValue(config, key, default(T), sections);
        }

        public static T? GetRawValue<T>(this IReadOnlyCryptographyConfig config, String? key, TryConverter<String, T>? converter, params String[]? sections)
        {
            return GetRawValue(config, key, converter, (IEnumerable<String>?) sections);
        }

        public static T? GetRawValue<T>(this IReadOnlyCryptographyConfig config, String? key, TryConverter<String, T>? converter, IEnumerable<String>? sections)
        {
            return GetRawValue(config, key, default, converter!, sections);
        }

        public static T GetRawValue<T>(this IReadOnlyCryptographyConfig config, String? key, T alternate, params String[]? sections)
        {
            return GetRawValue(config, key, alternate, (IEnumerable<String>?) sections);
        }

        public static T GetRawValue<T>(this IReadOnlyCryptographyConfig config, String? key, T alternate, IEnumerable<String>? sections)
        {
            return GetRawValue(config, key, alternate, null, sections);
        }

        public static T GetRawValue<T>(this IReadOnlyCryptographyConfig config, String? key, T alternate, TryConverter<String, T>? converter, params String[]? sections)
        {
            return GetRawValue(config, key, alternate, converter, (IEnumerable<String>?) sections);
        }

        public static T GetRawValue<T>(this IReadOnlyCryptographyConfig config, String? key, T alternate, TryConverter<String, T>? converter, IEnumerable<String>? sections)
        {
            if (config is null)
            {
                throw new ArgumentNullException(nameof(config));
            }
            
            if (config is IReadOnlyCryptographyConverterConfig configuration)
            {
                return configuration.GetRawValue(key, alternate, converter, sections);
            }

            String? value = config.GetRawValue(key, sections);

            if (value is null)
            {
                return alternate;
            }

            converter ??= ConvertUtilities.TryConvert;
            return converter(value, out T? result) ? result : alternate;
        }
        
        public static Task<T?> GetValueAsync<T>(this IReadOnlyCryptographyConfig config, String? key, params String[]? sections)
        {
            return GetValueAsync<T>(config, key, (IEnumerable<String>?) sections);
        }

        public static Task<T?> GetValueAsync<T>(this IReadOnlyCryptographyConfig config, String? key, IEnumerable<String>? sections)
        {
            return GetValueAsync<T>(config, key, sections, CancellationToken.None);
        }

        public static Task<T?> GetValueAsync<T>(this IReadOnlyCryptographyConfig config, String? key, CancellationToken token, params String[]? sections)
        {
            return GetValueAsync<T>(config, key, sections, token);
        }

        public static Task<T?> GetValueAsync<T>(this IReadOnlyCryptographyConfig config, String? key, IEnumerable<String>? sections, CancellationToken token)
        {
            return GetValueAsync(config, key, default(T), sections, token);
        }

        public static Task<T?> GetValueAsync<T>(this IReadOnlyCryptographyConfig config, String? key, TryConverter<String, T>? converter, params String[]? sections)
        {
            return GetValueAsync(config, key, converter, (IEnumerable<String>?) sections);
        }

        public static Task<T?> GetValueAsync<T>(this IReadOnlyCryptographyConfig config, String? key, TryConverter<String, T>? converter, IEnumerable<String>? sections)
        {
            return GetValueAsync(config, key, converter, sections, CancellationToken.None);
        }

        public static Task<T?> GetValueAsync<T>(this IReadOnlyCryptographyConfig config, String? key, TryConverter<String, T>? converter, CancellationToken token, params String[]? sections)
        {
            return GetValueAsync(config, key, converter, sections, token);
        }

        public static Task<T?> GetValueAsync<T>(this IReadOnlyCryptographyConfig config, String? key, TryConverter<String, T>? converter, IEnumerable<String>? sections, CancellationToken token)
        {
            return GetValueAsync(config, key, default, converter!, sections, token);
        }

        public static Task<T> GetValueAsync<T>(this IReadOnlyCryptographyConfig config, String? key, T alternate, params String[]? sections)
        {
            return GetValueAsync(config, key, alternate, (IEnumerable<String>?) sections);
        }

        public static Task<T> GetValueAsync<T>(this IReadOnlyCryptographyConfig config, String? key, T alternate, IEnumerable<String>? sections)
        {
            return GetValueAsync(config, key, alternate, sections, CancellationToken.None);
        }

        public static Task<T> GetValueAsync<T>(this IReadOnlyCryptographyConfig config, String? key, T alternate, CancellationToken token, params String[]? sections)
        {
            return GetValueAsync(config, key, alternate, sections, token);
        }

        public static Task<T> GetValueAsync<T>(this IReadOnlyCryptographyConfig config, String? key, T alternate, IEnumerable<String>? sections, CancellationToken token)
        {
            return GetValueAsync(config, key, alternate, (TryConverter<String, T>?) null, sections, token);
        }

        public static Task<T> GetValueAsync<T>(this IReadOnlyCryptographyConfig config, String? key, T alternate, TryConverter<String, T>? converter, params String[]? sections)
        {
            return GetValueAsync(config, key, alternate, converter, (IEnumerable<String>?) sections);
        }

        public static Task<T> GetValueAsync<T>(this IReadOnlyCryptographyConfig config, String? key, T alternate, TryConverter<String, T>? converter, IEnumerable<String>? sections)
        {
            return GetValueAsync(config, key, alternate, converter, sections, CancellationToken.None);
        }

        public static Task<T> GetValueAsync<T>(this IReadOnlyCryptographyConfig config, String? key, T alternate, TryConverter<String, T>? converter, CancellationToken token, params String[]? sections)
        {
            return GetValueAsync(config, key, alternate, converter, sections, token);
        }

        public static async Task<T> GetValueAsync<T>(this IReadOnlyCryptographyConfig config, String? key, T alternate, TryConverter<String, T>? converter, IEnumerable<String>? sections, CancellationToken token)
        {
            if (config is null)
            {
                throw new ArgumentNullException(nameof(config));
            }
            
            if (config is IReadOnlyCryptographyConverterConfig configuration)
            {
                return await configuration.GetValueAsync(key, alternate, converter, sections, token);
            }

            String? value = await config.GetValueAsync(key, sections, token);

            if (value is null)
            {
                return alternate;
            }

            converter ??= ConvertUtilities.TryConvert;
            return converter(value, out T? result) ? result : alternate;
        }

        public static Task<T?> GetValueAsync<T>(this IReadOnlyCryptographyConfig config, String? key, IStringCryptor? cryptor, params String[]? sections)
        {
            return GetValueAsync<T>(config, key, cryptor, (IEnumerable<String>?) sections);
        }

        public static Task<T?> GetValueAsync<T>(this IReadOnlyCryptographyConfig config, String? key, IStringCryptor? cryptor, IEnumerable<String>? sections)
        {
            return GetValueAsync<T>(config, key, cryptor, sections, CancellationToken.None);
        }

        public static Task<T?> GetValueAsync<T>(this IReadOnlyCryptographyConfig config, String? key, IStringCryptor? cryptor, CancellationToken token, params String[]? sections)
        {
            return GetValueAsync<T>(config, key, cryptor, sections, token);
        }

        public static Task<T?> GetValueAsync<T>(this IReadOnlyCryptographyConfig config, String? key, IStringCryptor? cryptor, IEnumerable<String>? sections, CancellationToken token)
        {
            return GetValueAsync<T?>(config, key, default, cryptor, sections, token);
        }

        public static Task<T> GetValueAsync<T>(this IReadOnlyCryptographyConfig config, String? key, T alternate, IStringCryptor? cryptor, params String[]? sections)
        {
            return GetValueAsync(config, key, alternate, cryptor, (IEnumerable<String>?) sections);
        }

        public static Task<T> GetValueAsync<T>(this IReadOnlyCryptographyConfig config, String? key, T alternate, IStringCryptor? cryptor, IEnumerable<String>? sections)
        {
            return GetValueAsync(config, key, alternate, cryptor, sections, CancellationToken.None);
        }

        public static Task<T> GetValueAsync<T>(this IReadOnlyCryptographyConfig config, String? key, T alternate, IStringCryptor? cryptor, CancellationToken token, params String[]? sections)
        {
            return GetValueAsync(config, key, alternate, cryptor, sections, token);
        }

        public static Task<T> GetValueAsync<T>(this IReadOnlyCryptographyConfig config, String? key, T alternate, IStringCryptor? cryptor, IEnumerable<String>? sections, CancellationToken token)
        {
            return GetValueAsync(config, key, alternate, cryptor, null, sections, token);
        }

        public static Task<T> GetValueAsync<T>(this IReadOnlyCryptographyConfig config, String? key, T alternate, IStringCryptor? cryptor, TryConverter<String, T>? converter, params String[]? sections)
        {
            return GetValueAsync(config, key, alternate, cryptor, converter, (IEnumerable<String>?) sections);
        }

        public static Task<T> GetValueAsync<T>(this IReadOnlyCryptographyConfig config, String? key, T alternate, IStringCryptor? cryptor, TryConverter<String, T>? converter, IEnumerable<String>? sections)
        {
            return GetValueAsync(config, key, alternate, cryptor, converter, sections, CancellationToken.None);
        }

        public static Task<T> GetValueAsync<T>(this IReadOnlyCryptographyConfig config, String? key, T alternate, IStringCryptor? cryptor, TryConverter<String, T>? converter, CancellationToken token, params String[]? sections)
        {
            return GetValueAsync(config, key, alternate, cryptor, converter, sections, token);
        }

        public static async Task<T> GetValueAsync<T>(this IReadOnlyCryptographyConfig config, String? key, T alternate, IStringCryptor? cryptor, TryConverter<String, T>? converter, IEnumerable<String>? sections, CancellationToken token)
        {
            if (config is null)
            {
                throw new ArgumentNullException(nameof(config));
            }
            
            if (config is IReadOnlyCryptographyConverterConfig configuration)
            {
                return await configuration.GetValueAsync(key, alternate, cryptor, converter, sections, token);
            }

            String? value = await config.GetValueAsync(key, cryptor, sections, token);

            if (value is null)
            {
                return alternate;
            }

            converter ??= ConvertUtilities.TryConvert;
            return converter(value, out T? result) ? result : alternate;
        }

        public static Task<T?> GetRawValueAsync<T>(this IReadOnlyCryptographyConfig config, String? key, params String[]? sections)
        {
            return GetRawValueAsync<T>(config, key, (IEnumerable<String>?) sections);
        }

        public static Task<T?> GetRawValueAsync<T>(this IReadOnlyCryptographyConfig config, String? key, IEnumerable<String>? sections)
        {
            return GetRawValueAsync<T>(config, key, sections, CancellationToken.None);
        }

        public static Task<T?> GetRawValueAsync<T>(this IReadOnlyCryptographyConfig config, String? key, CancellationToken token, params String[]? sections)
        {
            return GetRawValueAsync<T>(config, key, sections, token);
        }

        public static Task<T?> GetRawValueAsync<T>(this IReadOnlyCryptographyConfig config, String? key, IEnumerable<String>? sections, CancellationToken token)
        {
            return GetRawValueAsync(config, key, default(T), sections, token);
        }

        public static Task<T?> GetRawValueAsync<T>(this IReadOnlyCryptographyConfig config, String? key, TryConverter<String, T>? converter, params String[]? sections)
        {
            return GetRawValueAsync(config, key, converter, (IEnumerable<String>?) sections);
        }

        public static Task<T?> GetRawValueAsync<T>(this IReadOnlyCryptographyConfig config, String? key, TryConverter<String, T>? converter, IEnumerable<String>? sections)
        {
            return GetRawValueAsync(config, key, converter, sections, CancellationToken.None);
        }

        public static Task<T?> GetRawValueAsync<T>(this IReadOnlyCryptographyConfig config, String? key, TryConverter<String, T>? converter, CancellationToken token, params String[]? sections)
        {
            return GetRawValueAsync(config, key, converter, sections, token);
        }

        public static Task<T?> GetRawValueAsync<T>(this IReadOnlyCryptographyConfig config, String? key, TryConverter<String, T>? converter, IEnumerable<String>? sections, CancellationToken token)
        {
            return GetRawValueAsync(config, key, default, converter!, sections, token);
        }

        public static Task<T> GetRawValueAsync<T>(this IReadOnlyCryptographyConfig config, String? key, T alternate, params String[]? sections)
        {
            return GetRawValueAsync(config, key, alternate, (IEnumerable<String>?) sections);
        }

        public static Task<T> GetRawValueAsync<T>(this IReadOnlyCryptographyConfig config, String? key, T alternate, IEnumerable<String>? sections)
        {
            return GetRawValueAsync(config, key, alternate, sections, CancellationToken.None);
        }

        public static Task<T> GetRawValueAsync<T>(this IReadOnlyCryptographyConfig config, String? key, T alternate, CancellationToken token, params String[]? sections)
        {
            return GetRawValueAsync(config, key, alternate, sections, token);
        }

        public static Task<T> GetRawValueAsync<T>(this IReadOnlyCryptographyConfig config, String? key, T alternate, IEnumerable<String>? sections, CancellationToken token)
        {
            return GetRawValueAsync(config, key, alternate, null, sections, token);
        }

        public static Task<T> GetRawValueAsync<T>(this IReadOnlyCryptographyConfig config, String? key, T alternate, TryConverter<String, T>? converter, params String[]? sections)
        {
            return GetRawValueAsync(config, key, alternate, converter, (IEnumerable<String>?) sections);
        }

        public static Task<T> GetRawValueAsync<T>(this IReadOnlyCryptographyConfig config, String? key, T alternate, TryConverter<String, T>? converter, IEnumerable<String>? sections)
        {
            return GetRawValueAsync(config, key, alternate, converter, sections, CancellationToken.None);
        }

        public static Task<T> GetRawValueAsync<T>(this IReadOnlyCryptographyConfig config, String? key, T alternate, TryConverter<String, T>? converter, CancellationToken token, params String[]? sections)
        {
            return GetRawValueAsync(config, key, alternate, converter, sections, token);
        }

        public static async Task<T> GetRawValueAsync<T>(this IReadOnlyCryptographyConfig config, String? key, T alternate, TryConverter<String, T>? converter, IEnumerable<String>? sections, CancellationToken token)
        {
            if (config is null)
            {
                throw new ArgumentNullException(nameof(config));
            }
            
            if (config is IReadOnlyCryptographyConverterConfig configuration)
            {
                return await configuration.GetRawValueAsync(key, alternate, converter, sections, token);
            }

            String? value = await config.GetRawValueAsync(key, sections, token);

            if (value is null)
            {
                return alternate;
            }

            converter ??= ConvertUtilities.TryConvert;
            return converter(value, out T? result) ? result : alternate;
        }
        
        public static T? GetValue<T>(this ICryptographyConfig config, String? key, params String[]? sections)
        {
            return GetValue<T>(config, key, (IEnumerable<String>?) sections);
        }

        public static T? GetValue<T>(this ICryptographyConfig config, String? key, IEnumerable<String>? sections)
        {
            return GetValue(config, key, default(T), sections);
        }

        public static T? GetValue<T>(this ICryptographyConfig config, String? key, TryConverter<String, T>? converter, params String[]? sections)
        {
            return GetValue(config, key, converter, (IEnumerable<String>?) sections);
        }

        public static T? GetValue<T>(this ICryptographyConfig config, String? key, TryConverter<String, T>? converter, IEnumerable<String>? sections)
        {
            return GetValue(config, key, default(T), converter!, sections);
        }

        public static T GetValue<T>(this ICryptographyConfig config, String? key, T alternate, params String[]? sections)
        {
            return GetValue(config, key, alternate, (IEnumerable<String>?) sections);
        }

        public static T GetValue<T>(this ICryptographyConfig config, String? key, T alternate, IEnumerable<String>? sections)
        {
            return GetValue(config, key, alternate, (TryConverter<String, T>?) null, sections);
        }

        public static T GetValue<T>(this ICryptographyConfig config, String? key, T alternate, TryConverter<String, T>? converter, params String[]? sections)
        {
            return GetValue(config, key, alternate, converter, (IEnumerable<String>?) sections);
        }

        public static T GetValue<T>(this ICryptographyConfig config, String? key, T alternate, TryConverter<String, T>? converter, IEnumerable<String>? sections)
        {
            if (config is null)
            {
                throw new ArgumentNullException(nameof(config));
            }
            
            if (config is ICryptographyConverterConfig configuration)
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
        
        public static T? GetValue<T>(this ICryptographyConfig config, String? key, IStringCryptor? cryptor, params String[]? sections)
        {
            return GetValue<T>(config, key, cryptor, (IEnumerable<String>?) sections);
        }

        public static T? GetValue<T>(this ICryptographyConfig config, String? key, IStringCryptor? cryptor, IEnumerable<String>? sections)
        {
            return GetValue<T>(config, key, cryptor, null, sections);
        }

        public static T? GetValue<T>(this ICryptographyConfig config, String? key, IStringCryptor? cryptor, TryConverter<String, T>? converter, params String[]? sections)
        {
            return GetValue(config, key, cryptor, converter, (IEnumerable<String>?) sections);
        }

        public static T? GetValue<T>(this ICryptographyConfig config, String? key, IStringCryptor? cryptor, TryConverter<String, T>? converter, IEnumerable<String>? sections)
        {
            return GetValue(config, key, default, cryptor, converter!, sections);
        }
        
        public static T GetValue<T>(this ICryptographyConfig config, String? key, T alternate, IStringCryptor? cryptor, params String[]? sections)
        {
            return GetValue(config, key, alternate, cryptor, (IEnumerable<String>?) sections);
        }

        public static T GetValue<T>(this ICryptographyConfig config, String? key, T alternate, IStringCryptor? cryptor, IEnumerable<String>? sections)
        {
            return GetValue(config, key, alternate, cryptor, null, sections);
        }

        public static T GetValue<T>(this ICryptographyConfig config, String? key, T alternate, IStringCryptor? cryptor, TryConverter<String, T>? converter, params String[]? sections)
        {
            return GetValue(config, key, alternate, cryptor, converter, (IEnumerable<String>?) sections);
        }

        public static T GetValue<T>(this ICryptographyConfig config, String? key, T alternate, IStringCryptor? cryptor, TryConverter<String, T>? converter, IEnumerable<String>? sections)
        {
            if (config is null)
            {
                throw new ArgumentNullException(nameof(config));
            }
            
            if (config is ICryptographyConverterConfig configuration)
            {
                return configuration.GetValue(key, alternate, cryptor, converter, sections);
            }

            String? value = config.GetValue(key, cryptor, sections);

            if (value is null)
            {
                return alternate;
            }

            converter ??= ConvertUtilities.TryConvert;
            return converter(value, out T? result) ? result : alternate;
        }
        
        public static T? GetRawValue<T>(this ICryptographyConfig config, String? key, params String[]? sections)
        {
            return GetRawValue<T>(config, key, (IEnumerable<String>?) sections);
        }

        public static T? GetRawValue<T>(this ICryptographyConfig config, String? key, IEnumerable<String>? sections)
        {
            return GetRawValue(config, key, default(T), sections);
        }

        public static T? GetRawValue<T>(this ICryptographyConfig config, String? key, TryConverter<String, T>? converter, params String[]? sections)
        {
            return GetRawValue(config, key, converter, (IEnumerable<String>?) sections);
        }

        public static T? GetRawValue<T>(this ICryptographyConfig config, String? key, TryConverter<String, T>? converter, IEnumerable<String>? sections)
        {
            return GetRawValue(config, key, default, converter!, sections);
        }

        public static T GetRawValue<T>(this ICryptographyConfig config, String? key, T alternate, params String[]? sections)
        {
            return GetRawValue(config, key, alternate, (IEnumerable<String>?) sections);
        }

        public static T GetRawValue<T>(this ICryptographyConfig config, String? key, T alternate, IEnumerable<String>? sections)
        {
            return GetRawValue(config, key, alternate, null, sections);
        }

        public static T GetRawValue<T>(this ICryptographyConfig config, String? key, T alternate, TryConverter<String, T>? converter, params String[]? sections)
        {
            return GetRawValue(config, key, alternate, converter, (IEnumerable<String>?) sections);
        }

        public static T GetRawValue<T>(this ICryptographyConfig config, String? key, T alternate, TryConverter<String, T>? converter, IEnumerable<String>? sections)
        {
            if (config is null)
            {
                throw new ArgumentNullException(nameof(config));
            }
            
            if (config is ICryptographyConverterConfig configuration)
            {
                return configuration.GetRawValue(key, alternate, converter, sections);
            }

            String? value = config.GetRawValue(key, sections);

            if (value is null)
            {
                return alternate;
            }

            converter ??= ConvertUtilities.TryConvert;
            return converter(value, out T? result) ? result : alternate;
        }
        
        public static Task<T?> GetValueAsync<T>(this ICryptographyConfig config, String? key, params String[]? sections)
        {
            return GetValueAsync<T>(config, key, (IEnumerable<String>?) sections);
        }

        public static Task<T?> GetValueAsync<T>(this ICryptographyConfig config, String? key, IEnumerable<String>? sections)
        {
            return GetValueAsync<T>(config, key, sections, CancellationToken.None);
        }

        public static Task<T?> GetValueAsync<T>(this ICryptographyConfig config, String? key, CancellationToken token, params String[]? sections)
        {
            return GetValueAsync<T>(config, key, sections, token);
        }

        public static Task<T?> GetValueAsync<T>(this ICryptographyConfig config, String? key, IEnumerable<String>? sections, CancellationToken token)
        {
            return GetValueAsync(config, key, default(T), sections, token);
        }

        public static Task<T?> GetValueAsync<T>(this ICryptographyConfig config, String? key, TryConverter<String, T>? converter, params String[]? sections)
        {
            return GetValueAsync(config, key, converter, (IEnumerable<String>?) sections);
        }

        public static Task<T?> GetValueAsync<T>(this ICryptographyConfig config, String? key, TryConverter<String, T>? converter, IEnumerable<String>? sections)
        {
            return GetValueAsync(config, key, converter, sections, CancellationToken.None);
        }

        public static Task<T?> GetValueAsync<T>(this ICryptographyConfig config, String? key, TryConverter<String, T>? converter, CancellationToken token, params String[]? sections)
        {
            return GetValueAsync(config, key, converter, sections, token);
        }

        public static Task<T?> GetValueAsync<T>(this ICryptographyConfig config, String? key, TryConverter<String, T>? converter, IEnumerable<String>? sections, CancellationToken token)
        {
            return GetValueAsync(config, key, default, converter!, sections, token);
        }

        public static Task<T> GetValueAsync<T>(this ICryptographyConfig config, String? key, T alternate, params String[]? sections)
        {
            return GetValueAsync(config, key, alternate, (IEnumerable<String>?) sections);
        }

        public static Task<T> GetValueAsync<T>(this ICryptographyConfig config, String? key, T alternate, IEnumerable<String>? sections)
        {
            return GetValueAsync(config, key, alternate, sections, CancellationToken.None);
        }

        public static Task<T> GetValueAsync<T>(this ICryptographyConfig config, String? key, T alternate, CancellationToken token, params String[]? sections)
        {
            return GetValueAsync(config, key, alternate, sections, token);
        }

        public static Task<T> GetValueAsync<T>(this ICryptographyConfig config, String? key, T alternate, IEnumerable<String>? sections, CancellationToken token)
        {
            return GetValueAsync(config, key, alternate, (TryConverter<String, T>?) null, sections, token);
        }

        public static Task<T> GetValueAsync<T>(this ICryptographyConfig config, String? key, T alternate, TryConverter<String, T>? converter, params String[]? sections)
        {
            return GetValueAsync(config, key, alternate, converter, (IEnumerable<String>?) sections);
        }

        public static Task<T> GetValueAsync<T>(this ICryptographyConfig config, String? key, T alternate, TryConverter<String, T>? converter, IEnumerable<String>? sections)
        {
            return GetValueAsync(config, key, alternate, converter, sections, CancellationToken.None);
        }

        public static Task<T> GetValueAsync<T>(this ICryptographyConfig config, String? key, T alternate, TryConverter<String, T>? converter, CancellationToken token, params String[]? sections)
        {
            return GetValueAsync(config, key, alternate, converter, sections, token);
        }

        public static async Task<T> GetValueAsync<T>(this ICryptographyConfig config, String? key, T alternate, TryConverter<String, T>? converter, IEnumerable<String>? sections, CancellationToken token)
        {
            if (config is null)
            {
                throw new ArgumentNullException(nameof(config));
            }
            
            if (config is ICryptographyConverterConfig configuration)
            {
                return await configuration.GetValueAsync(key, alternate, converter, sections, token);
            }

            String? value = await config.GetValueAsync(key, sections, token);

            if (value is null)
            {
                return alternate;
            }

            converter ??= ConvertUtilities.TryConvert;
            return converter(value, out T? result) ? result : alternate;
        }

        public static Task<T?> GetValueAsync<T>(this ICryptographyConfig config, String? key, IStringCryptor? cryptor, params String[]? sections)
        {
            return GetValueAsync<T>(config, key, cryptor, (IEnumerable<String>?) sections);
        }

        public static Task<T?> GetValueAsync<T>(this ICryptographyConfig config, String? key, IStringCryptor? cryptor, IEnumerable<String>? sections)
        {
            return GetValueAsync<T>(config, key, cryptor, sections, CancellationToken.None);
        }

        public static Task<T?> GetValueAsync<T>(this ICryptographyConfig config, String? key, IStringCryptor? cryptor, CancellationToken token, params String[]? sections)
        {
            return GetValueAsync<T>(config, key, cryptor, sections, token);
        }

        public static Task<T?> GetValueAsync<T>(this ICryptographyConfig config, String? key, IStringCryptor? cryptor, IEnumerable<String>? sections, CancellationToken token)
        {
            return GetValueAsync<T?>(config, key, default, cryptor, sections, token);
        }

        public static Task<T> GetValueAsync<T>(this ICryptographyConfig config, String? key, T alternate, IStringCryptor? cryptor, params String[]? sections)
        {
            return GetValueAsync(config, key, alternate, cryptor, (IEnumerable<String>?) sections);
        }

        public static Task<T> GetValueAsync<T>(this ICryptographyConfig config, String? key, T alternate, IStringCryptor? cryptor, IEnumerable<String>? sections)
        {
            return GetValueAsync(config, key, alternate, cryptor, sections, CancellationToken.None);
        }

        public static Task<T> GetValueAsync<T>(this ICryptographyConfig config, String? key, T alternate, IStringCryptor? cryptor, CancellationToken token, params String[]? sections)
        {
            return GetValueAsync(config, key, alternate, cryptor, sections, token);
        }

        public static Task<T> GetValueAsync<T>(this ICryptographyConfig config, String? key, T alternate, IStringCryptor? cryptor, IEnumerable<String>? sections, CancellationToken token)
        {
            return GetValueAsync(config, key, alternate, cryptor, null, sections, token);
        }

        public static Task<T> GetValueAsync<T>(this ICryptographyConfig config, String? key, T alternate, IStringCryptor? cryptor, TryConverter<String, T>? converter, params String[]? sections)
        {
            return GetValueAsync(config, key, alternate, cryptor, converter, (IEnumerable<String>?) sections);
        }

        public static Task<T> GetValueAsync<T>(this ICryptographyConfig config, String? key, T alternate, IStringCryptor? cryptor, TryConverter<String, T>? converter, IEnumerable<String>? sections)
        {
            return GetValueAsync(config, key, alternate, cryptor, converter, sections, CancellationToken.None);
        }

        public static Task<T> GetValueAsync<T>(this ICryptographyConfig config, String? key, T alternate, IStringCryptor? cryptor, TryConverter<String, T>? converter, CancellationToken token, params String[]? sections)
        {
            return GetValueAsync(config, key, alternate, cryptor, converter, sections, token);
        }

        public static async Task<T> GetValueAsync<T>(this ICryptographyConfig config, String? key, T alternate, IStringCryptor? cryptor, TryConverter<String, T>? converter, IEnumerable<String>? sections, CancellationToken token)
        {
            if (config is null)
            {
                throw new ArgumentNullException(nameof(config));
            }
            
            if (config is ICryptographyConverterConfig configuration)
            {
                return await configuration.GetValueAsync(key, alternate, cryptor, converter, sections, token);
            }

            String? value = await config.GetValueAsync(key, cryptor, sections, token);

            if (value is null)
            {
                return alternate;
            }

            converter ??= ConvertUtilities.TryConvert;
            return converter(value, out T? result) ? result : alternate;
        }

        public static Task<T?> GetRawValueAsync<T>(this ICryptographyConfig config, String? key, params String[]? sections)
        {
            return GetRawValueAsync<T>(config, key, (IEnumerable<String>?) sections);
        }

        public static Task<T?> GetRawValueAsync<T>(this ICryptographyConfig config, String? key, IEnumerable<String>? sections)
        {
            return GetRawValueAsync<T>(config, key, sections, CancellationToken.None);
        }

        public static Task<T?> GetRawValueAsync<T>(this ICryptographyConfig config, String? key, CancellationToken token, params String[]? sections)
        {
            return GetRawValueAsync<T>(config, key, sections, token);
        }

        public static Task<T?> GetRawValueAsync<T>(this ICryptographyConfig config, String? key, IEnumerable<String>? sections, CancellationToken token)
        {
            return GetRawValueAsync(config, key, default(T), sections, token);
        }

        public static Task<T?> GetRawValueAsync<T>(this ICryptographyConfig config, String? key, TryConverter<String, T>? converter, params String[]? sections)
        {
            return GetRawValueAsync(config, key, converter, (IEnumerable<String>?) sections);
        }

        public static Task<T?> GetRawValueAsync<T>(this ICryptographyConfig config, String? key, TryConverter<String, T>? converter, IEnumerable<String>? sections)
        {
            return GetRawValueAsync(config, key, converter, sections, CancellationToken.None);
        }

        public static Task<T?> GetRawValueAsync<T>(this ICryptographyConfig config, String? key, TryConverter<String, T>? converter, CancellationToken token, params String[]? sections)
        {
            return GetRawValueAsync(config, key, converter, sections, token);
        }

        public static Task<T?> GetRawValueAsync<T>(this ICryptographyConfig config, String? key, TryConverter<String, T>? converter, IEnumerable<String>? sections, CancellationToken token)
        {
            return GetRawValueAsync(config, key, default, converter!, sections, token);
        }

        public static Task<T> GetRawValueAsync<T>(this ICryptographyConfig config, String? key, T alternate, params String[]? sections)
        {
            return GetRawValueAsync(config, key, alternate, (IEnumerable<String>?) sections);
        }

        public static Task<T> GetRawValueAsync<T>(this ICryptographyConfig config, String? key, T alternate, IEnumerable<String>? sections)
        {
            return GetRawValueAsync(config, key, alternate, sections, CancellationToken.None);
        }

        public static Task<T> GetRawValueAsync<T>(this ICryptographyConfig config, String? key, T alternate, CancellationToken token, params String[]? sections)
        {
            return GetRawValueAsync(config, key, alternate, sections, token);
        }

        public static Task<T> GetRawValueAsync<T>(this ICryptographyConfig config, String? key, T alternate, IEnumerable<String>? sections, CancellationToken token)
        {
            return GetRawValueAsync(config, key, alternate, null, sections, token);
        }

        public static Task<T> GetRawValueAsync<T>(this ICryptographyConfig config, String? key, T alternate, TryConverter<String, T>? converter, params String[]? sections)
        {
            return GetRawValueAsync(config, key, alternate, converter, (IEnumerable<String>?) sections);
        }

        public static Task<T> GetRawValueAsync<T>(this ICryptographyConfig config, String? key, T alternate, TryConverter<String, T>? converter, IEnumerable<String>? sections)
        {
            return GetRawValueAsync(config, key, alternate, converter, sections, CancellationToken.None);
        }

        public static Task<T> GetRawValueAsync<T>(this ICryptographyConfig config, String? key, T alternate, TryConverter<String, T>? converter, CancellationToken token, params String[]? sections)
        {
            return GetRawValueAsync(config, key, alternate, converter, sections, token);
        }

        public static async Task<T> GetRawValueAsync<T>(this ICryptographyConfig config, String? key, T alternate, TryConverter<String, T>? converter, IEnumerable<String>? sections, CancellationToken token)
        {
            if (config is null)
            {
                throw new ArgumentNullException(nameof(config));
            }
            
            if (config is ICryptographyConverterConfig configuration)
            {
                return await configuration.GetRawValueAsync(key, alternate, converter, sections, token);
            }

            String? value = await config.GetRawValueAsync(key, sections, token);

            if (value is null)
            {
                return alternate;
            }

            converter ??= ConvertUtilities.TryConvert;
            return converter(value, out T? result) ? result : alternate;
        }
        
        public static Boolean SetValue<T>(this ICryptographyConfig config, String? key, T value, params String[]? sections)
        {
            return SetValue(config, key, value, (IEnumerable<String>?) sections);
        }

        public static Boolean SetValue<T>(this ICryptographyConfig config, String? key, T value, IEnumerable<String>? sections)
        {
            if (config is null)
            {
                throw new ArgumentNullException(nameof(config));
            }
            
            if (config is ICryptographyConverterConfig configuration)
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
        
        public static Boolean SetValue<T>(this ICryptographyConfig config, String? key, T value, IStringCryptor? cryptor, params String[]? sections)
        {
            return SetValue(config, key, value, cryptor, (IEnumerable<String>?) sections);
        }

        public static Boolean SetValue<T>(this ICryptographyConfig config, String? key, T value, IStringCryptor? cryptor, IEnumerable<String>? sections)
        {
            if (config is null)
            {
                throw new ArgumentNullException(nameof(config));
            }
            
            if (config is ICryptographyConverterConfig configuration)
            {
                return configuration.SetValue(key, value, cryptor, sections);
            }

            if (value is null)
            {
                return config.SetValue(key, null, cryptor, sections);
            }
            
            String? convert = value.GetString();
            return convert is not null && config.SetValue(key, convert, cryptor, sections);
        }
        
        public static Boolean SetRawValue<T>(this ICryptographyConfig config, String? key, T value, params String[]? sections)
        {
            return SetRawValue(config, key, value, (IEnumerable<String>?) sections);
        }

        public static Boolean SetRawValue<T>(this ICryptographyConfig config, String? key, T value, IEnumerable<String>? sections)
        {
            if (config is null)
            {
                throw new ArgumentNullException(nameof(config));
            }
            
            if (config is ICryptographyConverterConfig configuration)
            {
                return configuration.SetRawValue(key, value, sections);
            }

            if (value is null)
            {
                return config.SetRawValue(key, null, sections);
            }
            
            String? convert = value.GetString();
            return convert is not null && config.SetRawValue(key, convert, sections);
        }

        public static Task<Boolean> SetValueAsync<T>(this ICryptographyConfig config, String? key, T value, params String[]? sections)
        {
            return SetValueAsync(config, key, value, (IEnumerable<String>?) sections);
        }

        public static Task<Boolean> SetValueAsync<T>(this ICryptographyConfig config, String? key, T value, IEnumerable<String>? sections)
        {
            return SetValueAsync(config, key, value, sections, CancellationToken.None);
        }

        public static Task<Boolean> SetValueAsync<T>(this ICryptographyConfig config, String? key, T value, CancellationToken token, params String[]? sections)
        {
            return SetValueAsync(config, key, value, sections, token);
        }

        public static async Task<Boolean> SetValueAsync<T>(this ICryptographyConfig config, String? key, T value, IEnumerable<String>? sections, CancellationToken token)
        {
            if (config is null)
            {
                throw new ArgumentNullException(nameof(config));
            }
            
            if (config is ICryptographyConverterConfig configuration)
            {
                return await configuration.SetValueAsync(key, value, sections, token);
            }

            if (value is null)
            {
                return await config.SetValueAsync(key, null, sections, token);
            }
            
            String? convert = value.GetString();
            return convert is not null && await config.SetValueAsync(key, convert, sections, token);
        }

        public static Task<Boolean> SetValueAsync<T>(this ICryptographyConfig config, String? key, T value, IStringCryptor? cryptor, params String[]? sections)
        {
            return SetValueAsync(config, key, value, cryptor, (IEnumerable<String>?) sections);
        }

        public static Task<Boolean> SetValueAsync<T>(this ICryptographyConfig config, String? key, T value, IStringCryptor? cryptor, IEnumerable<String>? sections)
        {
            return SetValueAsync(config, key, value, cryptor, sections, CancellationToken.None);
        }

        public static Task<Boolean> SetValueAsync<T>(this ICryptographyConfig config, String? key, T value, IStringCryptor? cryptor, CancellationToken token, params String[]? sections)
        {
            return SetValueAsync(config, key, value, cryptor, sections, CancellationToken.None);
        }

        public static async Task<Boolean> SetValueAsync<T>(this ICryptographyConfig config, String? key, T value, IStringCryptor? cryptor, IEnumerable<String>? sections, CancellationToken token)
        {
            if (config is null)
            {
                throw new ArgumentNullException(nameof(config));
            }
            
            if (config is ICryptographyConverterConfig configuration)
            {
                return await configuration.SetValueAsync(key, value, cryptor, sections, token);
            }

            if (value is null)
            {
                return await config.SetValueAsync(key, null, cryptor, sections, token);
            }
            
            String? convert = value.GetString();
            return convert is not null && await config.SetValueAsync(key, convert, cryptor, sections, token);
        }

        public static Task<Boolean> SetRawValueAsync<T>(this ICryptographyConfig config, String? key, T value, params String[]? sections)
        {
            return SetRawValueAsync(config, key, value, (IEnumerable<String>?) sections);
        }

        public static Task<Boolean> SetRawValueAsync<T>(this ICryptographyConfig config, String? key, T value, IEnumerable<String>? sections)
        {
            return SetRawValueAsync(config, key, value, sections, CancellationToken.None);
        }

        public static Task<Boolean> SetRawValueAsync<T>(this ICryptographyConfig config, String? key, T value, CancellationToken token, params String[]? sections)
        {
            return SetRawValueAsync(config, key, value, sections, token);
        }

        public static async Task<Boolean> SetRawValueAsync<T>(this ICryptographyConfig config, String? key, T value, IEnumerable<String>? sections, CancellationToken token)
        {
            if (config is null)
            {
                throw new ArgumentNullException(nameof(config));
            }
            
            if (config is ICryptographyConverterConfig configuration)
            {
                return await configuration.SetRawValueAsync(key, value, sections, token);
            }

            if (value is null)
            {
                return await config.SetRawValueAsync(key, null, sections, token);
            }
            
            String? convert = value.GetString();
            return convert is not null && await config.SetRawValueAsync(key, convert, sections, token);
        }
        
        public static T? GetOrSetValue<T>(this ICryptographyConfig config, String? key, T value, params String[]? sections)
        {
            return GetOrSetValue(config, key, value, (IEnumerable<String>?) sections);
        }

        public static T? GetOrSetValue<T>(this ICryptographyConfig config, String? key, T value, IEnumerable<String>? sections)
        {
            return GetOrSetValue(config, key, value, (TryConverter<String, T>?) null, sections);
        }

        public static T? GetOrSetValue<T>(this ICryptographyConfig config, String? key, T value, TryConverter<String, T>? converter, params String[]? sections)
        {
            return GetOrSetValue(config, key, value, converter, (IEnumerable<String>?) sections);
        }

        public static T? GetOrSetValue<T>(this ICryptographyConfig config, String? key, T value, TryConverter<String, T>? converter, IEnumerable<String>? sections)
        {
            if (config is null)
            {
                throw new ArgumentNullException(nameof(config));
            }
            
            if (config is ICryptographyConverterConfig configuration)
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
        
        public static T? GetOrSetValue<T>(this ICryptographyConfig config, String? key, T value, IStringCryptor? cryptor, params String[]? sections)
        {
            return GetOrSetValue(config, key, value, cryptor, (IEnumerable<String>?) sections);
        }

        public static T? GetOrSetValue<T>(this ICryptographyConfig config, String? key, T value, IStringCryptor? cryptor, IEnumerable<String>? sections)
        {
            return GetOrSetValue(config, key, value, cryptor, null, sections);
        }

        public static T? GetOrSetValue<T>(this ICryptographyConfig config, String? key, T value, IStringCryptor? cryptor, TryConverter<String, T>? converter, params String[]? sections)
        {
            return GetOrSetValue(config, key, value, cryptor, converter, (IEnumerable<String>?) sections);
        }

        public static T? GetOrSetValue<T>(this ICryptographyConfig config, String? key, T value, IStringCryptor? cryptor, TryConverter<String, T>? converter, IEnumerable<String>? sections)
        {
            if (config is null)
            {
                throw new ArgumentNullException(nameof(config));
            }
            
            if (config is ICryptographyConverterConfig configuration)
            {
                return configuration.GetOrSetValue(key, value, cryptor, converter, sections);
            }

            String? convert = value?.GetString();
            String? get = config.GetOrSetValue(key, convert, cryptor, sections);

            if (get is null)
            {
                return default;
            }
            
            converter ??= ConvertUtilities.TryConvert;
            return converter(get, out T? result) ? result : default;
        }
        
        public static T? GetOrSetRawValue<T>(this ICryptographyConfig config, String? key, T value, params String[]? sections)
        {
            return GetOrSetRawValue(config, key, value, (IEnumerable<String>?) sections);
        }

        public static T? GetOrSetRawValue<T>(this ICryptographyConfig config, String? key, T value, IEnumerable<String>? sections)
        {
            return GetOrSetRawValue(config, key, value, null, sections);
        }

        public static T? GetOrSetRawValue<T>(this ICryptographyConfig config, String? key, T value, TryConverter<String, T>? converter, params String[]? sections)
        {
            return GetOrSetRawValue(config, key, value, converter, (IEnumerable<String>?) sections);
        }

        public static T? GetOrSetRawValue<T>(this ICryptographyConfig config, String? key, T value, TryConverter<String, T>? converter, IEnumerable<String>? sections)
        {
            if (config is null)
            {
                throw new ArgumentNullException(nameof(config));
            }
            
            if (config is ICryptographyConverterConfig configuration)
            {
                return configuration.GetOrSetRawValue(key, value, converter, sections);
            }

            String? convert = value?.GetString();
            String? get = config.GetOrSetRawValue(key, convert, sections);

            if (get is null)
            {
                return default;
            }
            
            converter ??= ConvertUtilities.TryConvert;
            return converter(get, out T? result) ? result : default;
        }

        public static Task<T?> GetOrSetValueAsync<T>(this ICryptographyConfig config, String? key, T value, params String[]? sections)
        {
            return GetOrSetValueAsync(config, key, value, (IEnumerable<String>?) sections);
        }

        public static Task<T?> GetOrSetValueAsync<T>(this ICryptographyConfig config, String? key, T value, IEnumerable<String>? sections)
        {
            return GetOrSetValueAsync(config, key, value, sections, CancellationToken.None);
        }

        public static Task<T?> GetOrSetValueAsync<T>(this ICryptographyConfig config, String? key, T value, CancellationToken token, params String[]? sections)
        {
            return GetOrSetValueAsync(config, key, value, sections, token);
        }

        public static Task<T?> GetOrSetValueAsync<T>(this ICryptographyConfig config, String? key, T value, IEnumerable<String>? sections, CancellationToken token)
        {
            return GetOrSetValueAsync(config, key, value, (TryConverter<String, T>?) null, sections, token);
        }

        public static Task<T?> GetOrSetValueAsync<T>(this ICryptographyConfig config, String? key, T value, TryConverter<String, T>? converter, params String[]? sections)
        {
            return GetOrSetValueAsync(config, key, value, converter, (IEnumerable<String>?) sections);
        }

        public static Task<T?> GetOrSetValueAsync<T>(this ICryptographyConfig config, String? key, T value, TryConverter<String, T>? converter, IEnumerable<String>? sections)
        {
            return GetOrSetValueAsync(config, key, value, converter, sections, CancellationToken.None);
        }

        public static Task<T?> GetOrSetValueAsync<T>(this ICryptographyConfig config, String? key, T value, TryConverter<String, T>? converter, CancellationToken token, params String[]? sections)
        {
            return GetOrSetValueAsync(config, key, value, converter, sections, token);
        }

        public static async Task<T?> GetOrSetValueAsync<T>(this ICryptographyConfig config, String? key, T value, TryConverter<String, T>? converter, IEnumerable<String>? sections, CancellationToken token)
        {
            if (config is null)
            {
                throw new ArgumentNullException(nameof(config));
            }
            
            if (config is ICryptographyConverterConfig configuration)
            {
                return await configuration.GetOrSetValueAsync(key, value, converter, sections, token);
            }

            String? convert = value?.GetString();
            String? get = await config.GetOrSetValueAsync(key, convert, sections, token);

            if (get is null)
            {
                return default;
            }
            
            converter ??= ConvertUtilities.TryConvert;
            return converter(get, out T? result) ? result : default;
        }

        public static Task<T?> GetOrSetValueAsync<T>(this ICryptographyConfig config, String? key, T value, IStringCryptor? cryptor, params String[]? sections)
        {
            return GetOrSetValueAsync(config, key, value, cryptor, (IEnumerable<String>?) sections);
        }

        public static Task<T?> GetOrSetValueAsync<T>(this ICryptographyConfig config, String? key, T value, IStringCryptor? cryptor, IEnumerable<String>? sections)
        {
            return GetOrSetValueAsync(config, key, value, cryptor, sections, CancellationToken.None);
        }

        public static Task<T?> GetOrSetValueAsync<T>(this ICryptographyConfig config, String? key, T value, IStringCryptor? cryptor, CancellationToken token, params String[]? sections)
        {
            return GetOrSetValueAsync(config, key, value, cryptor, sections, token);
        }

        public static Task<T?> GetOrSetValueAsync<T>(this ICryptographyConfig config, String? key, T value, IStringCryptor? cryptor, IEnumerable<String>? sections, CancellationToken token)
        {
            return GetOrSetValueAsync(config, key, value, cryptor, null, sections, token);
        }

        public static Task<T?> GetOrSetValueAsync<T>(this ICryptographyConfig config, String? key, T value, IStringCryptor? cryptor, TryConverter<String, T>? converter, params String[]? sections)
        {
            return GetOrSetValueAsync(config, key, value, cryptor, converter, (IEnumerable<String>?) sections);
        }

        public static Task<T?> GetOrSetValueAsync<T>(this ICryptographyConfig config, String? key, T value, IStringCryptor? cryptor, TryConverter<String, T>? converter, IEnumerable<String>? sections)
        {
            return GetOrSetValueAsync(config, key, value, cryptor, converter, sections, CancellationToken.None);
        }

        public static Task<T?> GetOrSetValueAsync<T>(this ICryptographyConfig config, String? key, T value, IStringCryptor? cryptor, TryConverter<String, T>? converter, CancellationToken token, params String[]? sections)
        {
            return GetOrSetValueAsync(config, key, value, cryptor, converter, sections, token);
        }

        public static async Task<T?> GetOrSetValueAsync<T>(this ICryptographyConfig config, String? key, T value, IStringCryptor? cryptor, TryConverter<String, T>? converter, IEnumerable<String>? sections, CancellationToken token)
        {
            if (config is null)
            {
                throw new ArgumentNullException(nameof(config));
            }
            
            if (config is ICryptographyConverterConfig configuration)
            {
                return await configuration.GetOrSetValueAsync(key, value, cryptor, converter, sections, token);
            }

            String? convert = value?.GetString();
            String? get = await config.GetOrSetValueAsync(key, convert, cryptor, sections, token);

            if (get is null)
            {
                return default;
            }
            
            converter ??= ConvertUtilities.TryConvert;
            return converter(get, out T? result) ? result : default;
        }

        public static Task<T?> GetOrSetRawValueAsync<T>(this ICryptographyConfig config, String? key, T value, params String[]? sections)
        {
            return GetOrSetRawValueAsync(config, key, value, (IEnumerable<String>?) sections);
        }

        public static Task<T?> GetOrSetRawValueAsync<T>(this ICryptographyConfig config, String? key, T value, IEnumerable<String>? sections)
        {
            return GetOrSetRawValueAsync(config, key, value, sections, CancellationToken.None);
        }

        public static Task<T?> GetOrSetRawValueAsync<T>(this ICryptographyConfig config, String? key, T value, CancellationToken token, params String[]? sections)
        {
            return GetOrSetRawValueAsync(config, key, value, sections, token);
        }

        public static Task<T?> GetOrSetRawValueAsync<T>(this ICryptographyConfig config, String? key, T value, IEnumerable<String>? sections, CancellationToken token)
        {
            return GetOrSetRawValueAsync(config, key, value, null, sections, token);
        }

        public static Task<T?> GetOrSetRawValueAsync<T>(this ICryptographyConfig config, String? key, T value, TryConverter<String, T>? converter, params String[]? sections)
        {
            return GetOrSetRawValueAsync(config, key, value, converter, (IEnumerable<String>?) sections);
        }

        public static Task<T?> GetOrSetRawValueAsync<T>(this ICryptographyConfig config, String? key, T value, TryConverter<String, T>? converter, IEnumerable<String>? sections)
        {
            return GetOrSetRawValueAsync(config, key, value, converter, sections, CancellationToken.None);
        }

        public static Task<T?> GetOrSetRawValueAsync<T>(this ICryptographyConfig config, String? key, T value, TryConverter<String, T>? converter, CancellationToken token, params String[]? sections)
        {
            return GetOrSetRawValueAsync(config, key, value, converter, sections, token);
        }

        public static async Task<T?> GetOrSetRawValueAsync<T>(this ICryptographyConfig config, String? key, T value, TryConverter<String, T>? converter, IEnumerable<String>? sections, CancellationToken token)
        {
            if (config is null)
            {
                throw new ArgumentNullException(nameof(config));
            }
            
            if (config is ICryptographyConverterConfig configuration)
            {
                return await configuration.GetOrSetRawValueAsync(key, value, converter, sections, token);
            }

            String? convert = value?.GetString();
            String? get = await config.GetOrSetRawValueAsync(key, convert, sections, token);

            if (get is null)
            {
                return default;
            }
            
            converter ??= ConvertUtilities.TryConvert;
            return converter(get, out T? result) ? result : default;
        }
    }
}