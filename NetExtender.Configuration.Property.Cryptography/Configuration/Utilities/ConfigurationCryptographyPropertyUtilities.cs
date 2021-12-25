// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Collections.Generic;
using NetExtender.Configuration.Common;
using NetExtender.Configuration.Cryptography.Interfaces;
using NetExtender.Configuration.Cryptography.Properties;
using NetExtender.Configuration.Cryptography.Properties.Interfaces;
using NetExtender.Configuration.Interfaces;
using NetExtender.Crypto.CryptKey.Interfaces;

namespace NetExtender.Configuration.Cryptography.Utilities
{
    public static class ConfigurationCryptographyPropertyUtilities
    {
        public static IReadOnlyCryptographyConfigProperty<T?> Converter<T>(this IReadOnlyCryptographyConfigProperty property)
        {
            return Converter(property, default(T));
        }

        public static IReadOnlyCryptographyConfigProperty<T?> Converter<T>(this IReadOnlyCryptographyConfigProperty property, Func<T, Boolean>? validate)
        {
            return Converter<T>(property, validate, null);
        }

        public static IReadOnlyCryptographyConfigProperty<T?> Converter<T>(this IReadOnlyCryptographyConfigProperty property, TryConverter<String?, T>? converter)
        {
            return Converter(property, null, converter);
        }

        public static IReadOnlyCryptographyConfigProperty<T?> Converter<T>(this IReadOnlyCryptographyConfigProperty property, Func<T, Boolean>? validate,
            TryConverter<String?, T>? converter)
        {
            return Converter(property, default, validate!, converter!);
        }

        public static IReadOnlyCryptographyConfigProperty<T> Converter<T>(this IReadOnlyCryptographyConfigProperty property, T alternate)
        {
            return Converter(property, alternate, null, null);
        }

        public static IReadOnlyCryptographyConfigProperty<T> Converter<T>(this IReadOnlyCryptographyConfigProperty property, T alternate, Func<T, Boolean>? validate)
        {
            return Converter(property, alternate, validate, null);
        }

        public static IReadOnlyCryptographyConfigProperty<T> Converter<T>(this IReadOnlyCryptographyConfigProperty property, T alternate, TryConverter<String?, T>? converter)
        {
            return Converter(property, alternate, null, converter);
        }

        public static IReadOnlyCryptographyConfigProperty<T> Converter<T>(this IReadOnlyCryptographyConfigProperty property, T alternate, Func<T, Boolean>? validate,
            TryConverter<String?, T>? converter)
        {
            if (property is null)
            {
                throw new ArgumentNullException(nameof(property));
            }

            return new ReadOnlyCryptographyConfigProperty<T>(property, alternate, validate, converter);
        }

        public static ICryptographyConfigProperty<T?> Converter<T>(this ICryptographyConfigProperty property)
        {
            return Converter(property, default(T));
        }

        public static ICryptographyConfigProperty<T?> Converter<T>(this ICryptographyConfigProperty property, Func<T, Boolean>? validate)
        {
            return Converter<T>(property, validate, null);
        }

        public static ICryptographyConfigProperty<T?> Converter<T>(this ICryptographyConfigProperty property, TryConverter<String?, T>? converter)
        {
            return Converter(property, null, converter);
        }

        public static ICryptographyConfigProperty<T?> Converter<T>(this ICryptographyConfigProperty property, Func<T, Boolean>? validate, TryConverter<String?, T>? converter)
        {
            return Converter(property, default, validate!, converter!);
        }

        public static ICryptographyConfigProperty<T> Converter<T>(this ICryptographyConfigProperty property, T alternate)
        {
            return Converter(property, alternate, null, null);
        }

        public static ICryptographyConfigProperty<T> Converter<T>(this ICryptographyConfigProperty property, T alternate, Func<T, Boolean>? validate)
        {
            return Converter(property, alternate, validate, null);
        }

        public static ICryptographyConfigProperty<T> Converter<T>(this ICryptographyConfigProperty property, T alternate, TryConverter<String?, T>? converter)
        {
            return Converter(property, alternate, null, converter);
        }

        public static ICryptographyConfigProperty<T> Converter<T>(this ICryptographyConfigProperty property, T alternate, Func<T, Boolean>? validate,
            TryConverter<String?, T>? converter)
        {
            if (property is null)
            {
                throw new ArgumentNullException(nameof(property));
            }

            return new CryptographyConfigProperty<T>(property, alternate, validate, converter);
        }

        public static IReadOnlyCryptographyConfigProperty GetProperty(this IReadOnlyConfig config, String? key, IStringCryptor cryptor, params String[]? sections)
        {
            return GetProperty(config, key, cryptor, (IEnumerable<String>?) sections);
        }

        public static IReadOnlyCryptographyConfigProperty GetProperty(this IReadOnlyConfig config, String? key, IStringCryptor cryptor, IEnumerable<String>? sections)
        {
            return GetProperty(config, key, null, cryptor, sections);
        }

        public static IReadOnlyCryptographyConfigProperty GetProperty(this IReadOnlyConfig config, String? key, IStringCryptor cryptor, ConfigPropertyOptions options,
            params String[]? sections)
        {
            return GetProperty(config, key, cryptor, options, (IEnumerable<String>?) sections);
        }

        public static IReadOnlyCryptographyConfigProperty GetProperty(this IReadOnlyConfig config, String? key, IStringCryptor cryptor, ConfigPropertyOptions options,
            IEnumerable<String>? sections)
        {
            return GetProperty(config, key, null, cryptor, options, sections);
        }

        public static IReadOnlyCryptographyConfigProperty GetProperty(this IReadOnlyConfig config, String? key, String? alternate, IStringCryptor cryptor, params String[]? sections)
        {
            return GetProperty(config, key, alternate, cryptor, (IEnumerable<String>?) sections);
        }

        public static IReadOnlyCryptographyConfigProperty GetProperty(this IReadOnlyConfig config, String? key, String? alternate, IStringCryptor cryptor, IEnumerable<String>? sections)
        {
            return GetProperty(config, key, alternate, cryptor, ConfigPropertyOptions.None, sections);
        }

        public static IReadOnlyCryptographyConfigProperty GetProperty(this IReadOnlyConfig config, String? key, String? alternate, IStringCryptor cryptor, ConfigPropertyOptions options,
            params String[]? sections)
        {
            return GetProperty(config, key, alternate, cryptor, options, (IEnumerable<String>?) sections);
        }

        public static IReadOnlyCryptographyConfigProperty GetProperty(this IReadOnlyConfig config, String? key, String? alternate, IStringCryptor cryptor, ConfigPropertyOptions options,
            IEnumerable<String>? sections)
        {
            return new ReadOnlyCryptographyConfigPropertyWrapper(config, key, alternate, cryptor, options, sections);
        }

        public static ICryptographyConfigProperty GetProperty(this IConfig config, String? key, IStringCryptor cryptor, params String[]? sections)
        {
            return GetProperty(config, key, cryptor, (IEnumerable<String>?) sections);
        }

        public static ICryptographyConfigProperty GetProperty(this IConfig config, String? key, IStringCryptor cryptor, IEnumerable<String>? sections)
        {
            return GetProperty(config, key, null, cryptor, sections);
        }

        public static ICryptographyConfigProperty GetProperty(this IConfig config, String? key, IStringCryptor cryptor, ConfigPropertyOptions options, params String[]? sections)
        {
            return GetProperty(config, key, cryptor, options, (IEnumerable<String>?) sections);
        }

        public static ICryptographyConfigProperty GetProperty(this IConfig config, String? key, IStringCryptor cryptor, ConfigPropertyOptions options, IEnumerable<String>? sections)
        {
            return GetProperty(config, key, null, cryptor, options, sections);
        }

        public static ICryptographyConfigProperty GetProperty(this IConfig config, String? key, String? alternate, IStringCryptor cryptor, params String[]? sections)
        {
            return GetProperty(config, key, alternate, cryptor, (IEnumerable<String>?) sections);
        }

        public static ICryptographyConfigProperty GetProperty(this IConfig config, String? key, String? alternate, IStringCryptor cryptor, IEnumerable<String>? sections)
        {
            return GetProperty(config, key, alternate, cryptor, ConfigPropertyOptions.None, sections);
        }

        public static ICryptographyConfigProperty GetProperty(this IConfig config, String? key, String? alternate, IStringCryptor cryptor, ConfigPropertyOptions options,
            params String[]? sections)
        {
            return GetProperty(config, key, alternate, cryptor, options, (IEnumerable<String>?) sections);
        }

        public static ICryptographyConfigProperty GetProperty(this IConfig config, String? key, String? alternate, IStringCryptor cryptor, ConfigPropertyOptions options,
            IEnumerable<String>? sections)
        {
            return new CryptographyConfigPropertyWrapper(config, key, alternate, cryptor, options, sections);
        }

        public static IReadOnlyCryptographyConfigProperty GetProperty(this IReadOnlyCryptographyConfig config, String? key, params String[]? sections)
        {
            return GetProperty(config, key, (IEnumerable<String>?) sections);
        }

        public static IReadOnlyCryptographyConfigProperty GetProperty(this IReadOnlyCryptographyConfig config, String? key, IEnumerable<String>? sections)
        {
            return GetProperty(config, key, (String?) null, sections);
        }

        public static IReadOnlyCryptographyConfigProperty GetProperty(this IReadOnlyCryptographyConfig config, String? key, ConfigPropertyOptions options, params String[]? sections)
        {
            return GetProperty(config, key, options, (IEnumerable<String>?) sections);
        }

        public static IReadOnlyCryptographyConfigProperty GetProperty(this IReadOnlyCryptographyConfig config, String? key, ConfigPropertyOptions options, IEnumerable<String>? sections)
        {
            return GetProperty(config, key, (String?) null, options, sections);
        }

        public static IReadOnlyCryptographyConfigProperty GetProperty(this IReadOnlyCryptographyConfig config, String? key, IStringCryptor? cryptor, params String[]? sections)
        {
            return GetProperty(config, key, cryptor, (IEnumerable<String>?) sections);
        }

        public static IReadOnlyCryptographyConfigProperty GetProperty(this IReadOnlyCryptographyConfig config, String? key, IStringCryptor? cryptor, IEnumerable<String>? sections)
        {
            return GetProperty(config, key, null, cryptor, ConfigPropertyOptions.None, sections);
        }

        public static IReadOnlyCryptographyConfigProperty GetProperty(this IReadOnlyCryptographyConfig config, String? key, IStringCryptor? cryptor, ConfigPropertyOptions options,
            params String[]? sections)
        {
            return GetProperty(config, key, cryptor, options, (IEnumerable<String>?) sections);
        }

        public static IReadOnlyCryptographyConfigProperty GetProperty(this IReadOnlyCryptographyConfig config, String? key, IStringCryptor? cryptor, ConfigPropertyOptions options,
            IEnumerable<String>? sections)
        {
            return GetProperty(config, key, null, cryptor, options, sections);
        }

        public static IReadOnlyCryptographyConfigProperty GetProperty(this IReadOnlyCryptographyConfig config, String? key, String? alternate, params String[]? sections)
        {
            return GetProperty(config, key, alternate, (IEnumerable<String>?) sections);
        }

        public static IReadOnlyCryptographyConfigProperty GetProperty(this IReadOnlyCryptographyConfig config, String? key, String? alternate, IEnumerable<String>? sections)
        {
            return GetProperty(config, key, alternate, null, sections);
        }

        public static IReadOnlyCryptographyConfigProperty GetProperty(this IReadOnlyCryptographyConfig config, String? key, String? alternate, ConfigPropertyOptions options,
            params String[]? sections)
        {
            return GetProperty(config, key, alternate, options, (IEnumerable<String>?) sections);
        }

        public static IReadOnlyCryptographyConfigProperty GetProperty(this IReadOnlyCryptographyConfig config, String? key, String? alternate, ConfigPropertyOptions options,
            IEnumerable<String>? sections)
        {
            return GetProperty(config, key, alternate, null, options, sections);
        }

        public static IReadOnlyCryptographyConfigProperty GetProperty(this IReadOnlyCryptographyConfig config, String? key, String? alternate, IStringCryptor? cryptor,
            params String[]? sections)
        {
            return GetProperty(config, key, alternate, cryptor, (IEnumerable<String>?) sections);
        }

        public static IReadOnlyCryptographyConfigProperty GetProperty(this IReadOnlyCryptographyConfig config, String? key, String? alternate, IStringCryptor? cryptor,
            IEnumerable<String>? sections)
        {
            return GetProperty(config, key, alternate, cryptor, ConfigPropertyOptions.None, sections);
        }

        public static IReadOnlyCryptographyConfigProperty GetProperty(this IReadOnlyCryptographyConfig config, String? key, String? alternate, IStringCryptor? cryptor,
            ConfigPropertyOptions options, params String[]? sections)
        {
            return GetProperty(config, key, alternate, cryptor, options, (IEnumerable<String>?) sections);
        }

        public static IReadOnlyCryptographyConfigProperty GetProperty(this IReadOnlyCryptographyConfig config, String? key, String? alternate, IStringCryptor? cryptor,
            ConfigPropertyOptions options, IEnumerable<String>? sections)
        {
            return new ReadOnlyCryptographyConfigProperty(config, key, alternate, cryptor, options, sections);
        }

        public static ICryptographyConfigProperty GetProperty(this ICryptographyConfig config, String? key, params String[]? sections)
        {
            return GetProperty(config, key, (IEnumerable<String>?) sections);
        }

        public static ICryptographyConfigProperty GetProperty(this ICryptographyConfig config, String? key, IEnumerable<String>? sections)
        {
            return GetProperty(config, key, (String?) null, sections);
        }

        public static ICryptographyConfigProperty GetProperty(this ICryptographyConfig config, String? key, ConfigPropertyOptions options, params String[]? sections)
        {
            return GetProperty(config, key, options, (IEnumerable<String>?) sections);
        }

        public static ICryptographyConfigProperty GetProperty(this ICryptographyConfig config, String? key, ConfigPropertyOptions options, IEnumerable<String>? sections)
        {
            return GetProperty(config, key, (String?) null, options, sections);
        }

        public static ICryptographyConfigProperty GetProperty(this ICryptographyConfig config, String? key, IStringCryptor? cryptor, params String[]? sections)
        {
            return GetProperty(config, key, cryptor, (IEnumerable<String>?) sections);
        }

        public static ICryptographyConfigProperty GetProperty(this ICryptographyConfig config, String? key, IStringCryptor? cryptor, IEnumerable<String>? sections)
        {
            return GetProperty(config, key, null, cryptor, ConfigPropertyOptions.None, sections);
        }

        public static ICryptographyConfigProperty GetProperty(this ICryptographyConfig config, String? key, IStringCryptor? cryptor, ConfigPropertyOptions options,
            params String[]? sections)
        {
            return GetProperty(config, key, cryptor, options, (IEnumerable<String>?) sections);
        }

        public static ICryptographyConfigProperty GetProperty(this ICryptographyConfig config, String? key, IStringCryptor? cryptor, ConfigPropertyOptions options,
            IEnumerable<String>? sections)
        {
            return GetProperty(config, key, null, cryptor, options, sections);
        }

        public static ICryptographyConfigProperty GetProperty(this ICryptographyConfig config, String? key, String? alternate, params String[]? sections)
        {
            return GetProperty(config, key, alternate, (IEnumerable<String>?) sections);
        }

        public static ICryptographyConfigProperty GetProperty(this ICryptographyConfig config, String? key, String? alternate, IEnumerable<String>? sections)
        {
            return GetProperty(config, key, alternate, null, sections);
        }

        public static ICryptographyConfigProperty GetProperty(this ICryptographyConfig config, String? key, String? alternate, ConfigPropertyOptions options, params String[]? sections)
        {
            return GetProperty(config, key, alternate, options, (IEnumerable<String>?) sections);
        }

        public static ICryptographyConfigProperty GetProperty(this ICryptographyConfig config, String? key, String? alternate, ConfigPropertyOptions options,
            IEnumerable<String>? sections)
        {
            return GetProperty(config, key, alternate, null, options, sections);
        }

        public static ICryptographyConfigProperty GetProperty(this ICryptographyConfig config, String? key, String? alternate, IStringCryptor? cryptor, params String[]? sections)
        {
            return GetProperty(config, key, alternate, cryptor, (IEnumerable<String>?) sections);
        }

        public static ICryptographyConfigProperty GetProperty(this ICryptographyConfig config, String? key, String? alternate, IStringCryptor? cryptor, IEnumerable<String>? sections)
        {
            return GetProperty(config, key, alternate, cryptor, ConfigPropertyOptions.None, sections);
        }

        public static ICryptographyConfigProperty GetProperty(this ICryptographyConfig config, String? key, String? alternate, IStringCryptor? cryptor, ConfigPropertyOptions options,
            params String[]? sections)
        {
            return GetProperty(config, key, alternate, cryptor, options, (IEnumerable<String>?) sections);
        }

        public static ICryptographyConfigProperty GetProperty(this ICryptographyConfig config, String? key, String? alternate, IStringCryptor? cryptor, ConfigPropertyOptions options,
            IEnumerable<String>? sections)
        {
            return new CryptographyConfigProperty(config, key, alternate, cryptor, options, sections);
        }

        public static IReadOnlyCryptographyConfigProperty<T?> GetProperty<T>(this IReadOnlyConfig config, String? key, IStringCryptor cryptor, params String[]? sections)
        {
            return GetProperty<T>(config, key, cryptor, (IEnumerable<String>?) sections);
        }

        public static IReadOnlyCryptographyConfigProperty<T?> GetProperty<T>(this IReadOnlyConfig config, String? key, IStringCryptor cryptor, IEnumerable<String>? sections)
        {
            return GetProperty(config, key, default(T), cryptor, sections);
        }

        public static IReadOnlyCryptographyConfigProperty<T> GetProperty<T>(this IReadOnlyConfig config, String? key, T alternate, IStringCryptor cryptor, params String[]? sections)
        {
            return GetProperty(config, key, alternate, cryptor, (IEnumerable<String>?) sections);
        }

        public static IReadOnlyCryptographyConfigProperty<T> GetProperty<T>(this IReadOnlyConfig config, String? key, T alternate, IStringCryptor cryptor, IEnumerable<String>? sections)
        {
            return GetProperty(config, key, alternate, cryptor, null, sections);
        }

        public static IReadOnlyCryptographyConfigProperty<T> GetProperty<T>(this IReadOnlyConfig config, String? key, T alternate, IStringCryptor cryptor, ConfigPropertyOptions options,
            params String[]? sections)
        {
            return GetProperty(config, key, alternate, cryptor, options, (IEnumerable<String>?) sections);
        }

        public static IReadOnlyCryptographyConfigProperty<T> GetProperty<T>(this IReadOnlyConfig config, String? key, T alternate, IStringCryptor cryptor, ConfigPropertyOptions options,
            IEnumerable<String>? sections)
        {
            return GetProperty(config, key, alternate, cryptor, null, options, sections);
        }

        public static IReadOnlyCryptographyConfigProperty<T> GetProperty<T>(this IReadOnlyConfig config, String? key, T alternate, IStringCryptor cryptor, Func<T, Boolean>? validate,
            params String[]? sections)
        {
            return GetProperty(config, key, alternate, cryptor, validate, (IEnumerable<String>?) sections);
        }

        public static IReadOnlyCryptographyConfigProperty<T> GetProperty<T>(this IReadOnlyConfig config, String? key, T alternate, IStringCryptor cryptor, Func<T, Boolean>? validate,
            IEnumerable<String>? sections)
        {
            return GetProperty(config, key, alternate, cryptor, validate, ConfigPropertyOptions.None, sections);
        }

        public static IReadOnlyCryptographyConfigProperty<T> GetProperty<T>(this IReadOnlyConfig config, String? key, T alternate, IStringCryptor cryptor, Func<T, Boolean>? validate,
            ConfigPropertyOptions options, params String[]? sections)
        {
            return GetProperty(config, key, alternate, cryptor, validate, options, (IEnumerable<String>?) sections);
        }

        public static IReadOnlyCryptographyConfigProperty<T> GetProperty<T>(this IReadOnlyConfig config, String? key, T alternate, IStringCryptor cryptor, Func<T, Boolean>? validate,
            ConfigPropertyOptions options, IEnumerable<String>? sections)
        {
            return GetProperty(config, key, alternate, cryptor, validate, null, options, sections);
        }

        public static IReadOnlyCryptographyConfigProperty<T> GetProperty<T>(this IReadOnlyConfig config, String? key, T alternate, IStringCryptor cryptor, Func<T, Boolean>? validate,
            TryConverter<String?, T>? converter, ConfigPropertyOptions options, params String[]? sections)
        {
            return GetProperty(config, key, alternate, cryptor, validate, converter, options, (IEnumerable<String>?) sections);
        }

        public static IReadOnlyCryptographyConfigProperty<T> GetProperty<T>(this IReadOnlyConfig config, String? key, T alternate, IStringCryptor cryptor, Func<T, Boolean>? validate,
            TryConverter<String?, T>? converter, ConfigPropertyOptions options, IEnumerable<String>? sections)
        {
            return new ReadOnlyCryptographyConfigProperty<T>(config, key, alternate, cryptor, validate, converter, options, sections);
        }

        public static ICryptographyConfigProperty<T?> GetProperty<T>(this IConfig config, String? key, IStringCryptor cryptor, params String[]? sections)
        {
            return GetProperty<T>(config, key, cryptor, (IEnumerable<String>?) sections);
        }

        public static ICryptographyConfigProperty<T?> GetProperty<T>(this IConfig config, String? key, IStringCryptor cryptor, IEnumerable<String>? sections)
        {
            return GetProperty(config, key, default(T), cryptor, sections);
        }

        public static ICryptographyConfigProperty<T> GetProperty<T>(this IConfig config, String? key, T alternate, IStringCryptor cryptor, params String[]? sections)
        {
            return GetProperty(config, key, alternate, cryptor, (IEnumerable<String>?) sections);
        }

        public static ICryptographyConfigProperty<T> GetProperty<T>(this IConfig config, String? key, T alternate, IStringCryptor cryptor, IEnumerable<String>? sections)
        {
            return GetProperty(config, key, alternate, cryptor, null, sections);
        }

        public static ICryptographyConfigProperty<T> GetProperty<T>(this IConfig config, String? key, T alternate, IStringCryptor cryptor, ConfigPropertyOptions options,
            params String[]? sections)
        {
            return GetProperty(config, key, alternate, cryptor, options, (IEnumerable<String>?) sections);
        }

        public static ICryptographyConfigProperty<T> GetProperty<T>(this IConfig config, String? key, T alternate, IStringCryptor cryptor, ConfigPropertyOptions options,
            IEnumerable<String>? sections)
        {
            return GetProperty(config, key, alternate, cryptor, null, options, sections);
        }

        public static ICryptographyConfigProperty<T> GetProperty<T>(this IConfig config, String? key, T alternate, IStringCryptor cryptor, Func<T, Boolean>? validate,
            params String[]? sections)
        {
            return GetProperty(config, key, alternate, cryptor, validate, (IEnumerable<String>?) sections);
        }

        public static ICryptographyConfigProperty<T> GetProperty<T>(this IConfig config, String? key, T alternate, IStringCryptor cryptor, Func<T, Boolean>? validate,
            IEnumerable<String>? sections)
        {
            return GetProperty(config, key, alternate, cryptor, validate, ConfigPropertyOptions.None, sections);
        }

        public static ICryptographyConfigProperty<T> GetProperty<T>(this IConfig config, String? key, T alternate, IStringCryptor cryptor, Func<T, Boolean>? validate,
            ConfigPropertyOptions options, params String[]? sections)
        {
            return GetProperty(config, key, alternate, cryptor, validate, options, (IEnumerable<String>?) sections);
        }

        public static ICryptographyConfigProperty<T> GetProperty<T>(this IConfig config, String? key, T alternate, IStringCryptor cryptor, Func<T, Boolean>? validate,
            ConfigPropertyOptions options, IEnumerable<String>? sections)
        {
            return GetProperty(config, key, alternate, cryptor, validate, null, options, sections);
        }

        public static ICryptographyConfigProperty<T> GetProperty<T>(this IConfig config, String? key, T alternate, IStringCryptor cryptor, Func<T, Boolean>? validate,
            TryConverter<String?, T>? converter, ConfigPropertyOptions options, params String[]? sections)
        {
            return GetProperty(config, key, alternate, cryptor, validate, converter, options, (IEnumerable<String>?) sections);
        }

        public static ICryptographyConfigProperty<T> GetProperty<T>(this IConfig config, String? key, T alternate, IStringCryptor cryptor, Func<T, Boolean>? validate,
            TryConverter<String?, T>? converter, ConfigPropertyOptions options, IEnumerable<String>? sections)
        {
            return new CryptographyConfigProperty<T>(config, key, alternate, cryptor, validate, converter, options, sections);
        }
        
        public static IReadOnlyCryptographyConfigProperty<T?> GetProperty<T>(this IReadOnlyCryptographyConfig config, String? key, IStringCryptor cryptor, params String[]? sections)
        {
            return GetProperty<T>(config, key, cryptor, (IEnumerable<String>?) sections);
        }

        public static IReadOnlyCryptographyConfigProperty<T?> GetProperty<T>(this IReadOnlyCryptographyConfig config, String? key, IStringCryptor cryptor, IEnumerable<String>? sections)
        {
            return GetProperty(config, key, default(T), cryptor, sections);
        }

        public static IReadOnlyCryptographyConfigProperty<T> GetProperty<T>(this IReadOnlyCryptographyConfig config, String? key, T alternate, IStringCryptor cryptor, params String[]? sections)
        {
            return GetProperty(config, key, alternate, cryptor, (IEnumerable<String>?) sections);
        }

        public static IReadOnlyCryptographyConfigProperty<T> GetProperty<T>(this IReadOnlyCryptographyConfig config, String? key, T alternate, IStringCryptor cryptor, IEnumerable<String>? sections)
        {
            return GetProperty(config, key, alternate, cryptor, null, sections);
        }

        public static IReadOnlyCryptographyConfigProperty<T> GetProperty<T>(this IReadOnlyCryptographyConfig config, String? key, T alternate, IStringCryptor cryptor, ConfigPropertyOptions options,
            params String[]? sections)
        {
            return GetProperty(config, key, alternate, cryptor, options, (IEnumerable<String>?) sections);
        }

        public static IReadOnlyCryptographyConfigProperty<T> GetProperty<T>(this IReadOnlyCryptographyConfig config, String? key, T alternate, IStringCryptor cryptor, ConfigPropertyOptions options,
            IEnumerable<String>? sections)
        {
            return GetProperty(config, key, alternate, cryptor, null, options, sections);
        }

        public static IReadOnlyCryptographyConfigProperty<T> GetProperty<T>(this IReadOnlyCryptographyConfig config, String? key, T alternate, IStringCryptor cryptor, Func<T, Boolean>? validate,
            params String[]? sections)
        {
            return GetProperty(config, key, alternate, cryptor, validate, (IEnumerable<String>?) sections);
        }

        public static IReadOnlyCryptographyConfigProperty<T> GetProperty<T>(this IReadOnlyCryptographyConfig config, String? key, T alternate, IStringCryptor cryptor, Func<T, Boolean>? validate,
            IEnumerable<String>? sections)
        {
            return GetProperty(config, key, alternate, cryptor, validate, ConfigPropertyOptions.None, sections);
        }

        public static IReadOnlyCryptographyConfigProperty<T> GetProperty<T>(this IReadOnlyCryptographyConfig config, String? key, T alternate, IStringCryptor cryptor, Func<T, Boolean>? validate,
            ConfigPropertyOptions options, params String[]? sections)
        {
            return GetProperty(config, key, alternate, cryptor, validate, options, (IEnumerable<String>?) sections);
        }

        public static IReadOnlyCryptographyConfigProperty<T> GetProperty<T>(this IReadOnlyCryptographyConfig config, String? key, T alternate, IStringCryptor cryptor, Func<T, Boolean>? validate,
            ConfigPropertyOptions options, IEnumerable<String>? sections)
        {
            return GetProperty(config, key, alternate, cryptor, validate, null, options, sections);
        }

        public static IReadOnlyCryptographyConfigProperty<T> GetProperty<T>(this IReadOnlyCryptographyConfig config, String? key, T alternate, IStringCryptor cryptor, Func<T, Boolean>? validate,
            TryConverter<String?, T>? converter, ConfigPropertyOptions options, params String[]? sections)
        {
            return GetProperty(config, key, alternate, cryptor, validate, converter, options, (IEnumerable<String>?) sections);
        }

        public static IReadOnlyCryptographyConfigProperty<T> GetProperty<T>(this IReadOnlyCryptographyConfig config, String? key, T alternate, IStringCryptor cryptor, Func<T, Boolean>? validate,
            TryConverter<String?, T>? converter, ConfigPropertyOptions options, IEnumerable<String>? sections)
        {
            return new ReadOnlyCryptographyConfigProperty<T>(config, key, alternate, cryptor, validate, converter, options, sections);
        }

        public static ICryptographyConfigProperty<T?> GetProperty<T>(this ICryptographyConfig config, String? key, IStringCryptor cryptor, params String[]? sections)
        {
            return GetProperty<T>(config, key, cryptor, (IEnumerable<String>?) sections);
        }

        public static ICryptographyConfigProperty<T?> GetProperty<T>(this ICryptographyConfig config, String? key, IStringCryptor cryptor, IEnumerable<String>? sections)
        {
            return GetProperty(config, key, default(T), cryptor, sections);
        }

        public static ICryptographyConfigProperty<T> GetProperty<T>(this ICryptographyConfig config, String? key, T alternate, IStringCryptor cryptor, params String[]? sections)
        {
            return GetProperty(config, key, alternate, cryptor, (IEnumerable<String>?) sections);
        }

        public static ICryptographyConfigProperty<T> GetProperty<T>(this ICryptographyConfig config, String? key, T alternate, IStringCryptor cryptor, IEnumerable<String>? sections)
        {
            return GetProperty(config, key, alternate, cryptor, null, sections);
        }

        public static ICryptographyConfigProperty<T> GetProperty<T>(this ICryptographyConfig config, String? key, T alternate, IStringCryptor cryptor, ConfigPropertyOptions options,
            params String[]? sections)
        {
            return GetProperty(config, key, alternate, cryptor, options, (IEnumerable<String>?) sections);
        }

        public static ICryptographyConfigProperty<T> GetProperty<T>(this ICryptographyConfig config, String? key, T alternate, IStringCryptor cryptor, ConfigPropertyOptions options,
            IEnumerable<String>? sections)
        {
            return GetProperty(config, key, alternate, cryptor, null, options, sections);
        }

        public static ICryptographyConfigProperty<T> GetProperty<T>(this ICryptographyConfig config, String? key, T alternate, IStringCryptor cryptor, Func<T, Boolean>? validate,
            params String[]? sections)
        {
            return GetProperty(config, key, alternate, cryptor, validate, (IEnumerable<String>?) sections);
        }

        public static ICryptographyConfigProperty<T> GetProperty<T>(this ICryptographyConfig config, String? key, T alternate, IStringCryptor cryptor, Func<T, Boolean>? validate,
            IEnumerable<String>? sections)
        {
            return GetProperty(config, key, alternate, cryptor, validate, ConfigPropertyOptions.None, sections);
        }

        public static ICryptographyConfigProperty<T> GetProperty<T>(this ICryptographyConfig config, String? key, T alternate, IStringCryptor cryptor, Func<T, Boolean>? validate,
            ConfigPropertyOptions options, params String[]? sections)
        {
            return GetProperty(config, key, alternate, cryptor, validate, options, (IEnumerable<String>?) sections);
        }

        public static ICryptographyConfigProperty<T> GetProperty<T>(this ICryptographyConfig config, String? key, T alternate, IStringCryptor cryptor, Func<T, Boolean>? validate,
            ConfigPropertyOptions options, IEnumerable<String>? sections)
        {
            return GetProperty(config, key, alternate, cryptor, validate, null, options, sections);
        }

        public static ICryptographyConfigProperty<T> GetProperty<T>(this ICryptographyConfig config, String? key, T alternate, IStringCryptor cryptor, Func<T, Boolean>? validate,
            TryConverter<String?, T>? converter, ConfigPropertyOptions options, params String[]? sections)
        {
            return GetProperty(config, key, alternate, cryptor, validate, converter, options, (IEnumerable<String>?) sections);
        }

        public static ICryptographyConfigProperty<T> GetProperty<T>(this ICryptographyConfig config, String? key, T alternate, IStringCryptor cryptor, Func<T, Boolean>? validate,
            TryConverter<String?, T>? converter, ConfigPropertyOptions options, IEnumerable<String>? sections)
        {
            return new CryptographyConfigProperty<T>(config, key, alternate, cryptor, validate, converter, options, sections);
        }
    }
}