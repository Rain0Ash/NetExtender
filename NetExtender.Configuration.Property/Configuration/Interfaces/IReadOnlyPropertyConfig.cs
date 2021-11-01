// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Collections.Generic;
using System.Linq;
using NetExtender.Configuration.Common;
using NetExtender.Configuration.Interfaces;
using NetExtender.Configuration.Property.Interfaces.Common;
using NetExtender.Crypto;
using NetExtender.Crypto.CryptKey;
using NetExtender.Crypto.CryptKey.Interfaces;
using NetExtender.Utilities.Types;

namespace NetExtender.Configuration.Property.Interfaces
{
    public interface IReadOnlyPropertyConfig : IReadOnlyConfig, IReadOnlyPropertyConfigBase
    {
        public IReadOnlyConfigProperty<T?> GetProperty<T>(String? key, IEnumerable<String>? sections)
        {
            return GetProperty(key, default(T), sections);
        }

        public IReadOnlyConfigProperty<T?> GetProperty<T>(String? key, params String[]? sections)
        {
            return GetProperty<T>(key, (IEnumerable<String>?) sections);
        }

        public IReadOnlyConfigProperty<T> GetProperty<T>(String? key, T value, IEnumerable<String>? sections)
        {
            return GetProperty(key, value, null, sections);
        }

        public IReadOnlyConfigProperty<T> GetProperty<T>(String? key, T value, params String[]? sections)
        {
            return GetProperty(key, value, (IEnumerable<String>?) sections);
        }

        public IReadOnlyConfigProperty<T> GetProperty<T>(String? key, T value, Func<T, Boolean>? validate, IEnumerable<String>? sections)
        {
            return GetProperty(key, value, validate, CryptAction.Decrypt, sections);
        }

        public IReadOnlyConfigProperty<T> GetProperty<T>(String? key, T value, Func<T, Boolean>? validate, params String[]? sections)
        {
            return GetProperty(key, value, validate, (IEnumerable<String>?) sections);
        }

        public IReadOnlyConfigProperty<T> GetProperty<T>(String? key, T value, Func<T, Boolean>? validate, CryptAction crypt, IEnumerable<String>? sections)
        {
            return GetProperty(key, value, validate, CryptKey.Create(crypt), sections);
        }

        public IReadOnlyConfigProperty<T> GetProperty<T>(String? key, T value, Func<T, Boolean>? validate, CryptAction crypt, params String[]? sections)
        {
            return GetProperty(key, value, validate, crypt, (IEnumerable<String>?) sections);
        }

        public IReadOnlyConfigProperty<T> GetProperty<T>(String? key, T value, Func<T, Boolean>? validate, ICryptKey? crypt, IEnumerable<String>? sections)
        {
            return GetProperty(key, value, validate, crypt, DefaultOptions, sections);
        }

        public IReadOnlyConfigProperty<T> GetProperty<T>(String? key, T value, Func<T, Boolean>? validate, ICryptKey? crypt, params String[]? sections)
        {
            return GetProperty(key, value, validate, crypt, (IEnumerable<String>?) sections);
        }

        public IReadOnlyConfigProperty<T> GetProperty<T>(String? key, T value, Func<T, Boolean>? validate, Func<ICryptKey?>? crypt, IEnumerable<String>? sections)
        {
            return GetProperty(key, value, validate, crypt?.Invoke(), sections);
        }

        public IReadOnlyConfigProperty<T> GetProperty<T>(String? key, T value, Func<T, Boolean>? validate, Func<ICryptKey?>? crypt, params String[]? sections)
        {
            return GetProperty(key, value, validate, crypt, (IEnumerable<String>?) sections);
        }

        public IReadOnlyConfigProperty<T> GetProperty<T>(String? key, T value, Func<T, Boolean>? validate, Func<String?, ICryptKey?>? crypt, IEnumerable<String>? sections)
        {
            return GetProperty(key, value, validate, crypt?.Invoke(key), sections);
        }

        public IReadOnlyConfigProperty<T> GetProperty<T>(String? key, T value, Func<T, Boolean>? validate, Func<String?, ICryptKey?>? crypt, params String[]? sections)
        {
            return GetProperty(key, value, validate, crypt, (IEnumerable<String>?) sections);
        }

        public IReadOnlyConfigProperty<T> GetProperty<T>(String? key, T value, Func<T, Boolean>? validate, Func<String?, String[]?, ICryptKey?>? crypt, IEnumerable<String>? sections)
        {
            String[]? array = sections?.ToArray();
            return GetProperty(key, value, validate, crypt?.Invoke(key, array), array);
        }

        public IReadOnlyConfigProperty<T> GetProperty<T>(String? key, T value, Func<T, Boolean>? validate, Func<String?, String[]?, ICryptKey?>? crypt, params String[]? sections)
        {
            return GetProperty(key, value, validate, crypt, (IEnumerable<String>?) sections);
        }

        public IReadOnlyConfigProperty<T> GetProperty<T>(String? key, T value, Func<T, Boolean>? validate, CryptAction crypt, ConfigPropertyOptions options, IEnumerable<String>? sections)
        {
            return GetProperty(key, value, validate, CryptKey.Create(crypt), options, sections);
        }

        public IReadOnlyConfigProperty<T> GetProperty<T>(String? key, T value, Func<T, Boolean>? validate, CryptAction crypt, ConfigPropertyOptions options, params String[]? sections)
        {
            return GetProperty(key, value, validate, crypt, options, (IEnumerable<String>?) sections);
        }

        public IReadOnlyConfigProperty<T> GetProperty<T>(String? key, T value, Func<T, Boolean>? validate, ICryptKey? crypt, ConfigPropertyOptions options, IEnumerable<String>? sections)
        {
            return GetProperty(key, value, validate, crypt, options, ConvertUtilities.TryConvert, sections);
        }

        public IReadOnlyConfigProperty<T> GetProperty<T>(String? key, T value, Func<T, Boolean>? validate, ICryptKey? crypt, ConfigPropertyOptions options, params String[]? sections)
        {
            return GetProperty(key, value, validate, crypt, options, (IEnumerable<String>?) sections);
        }

        public IReadOnlyConfigProperty<T> GetProperty<T>(String? key, T value, Func<T, Boolean>? validate, Func<ICryptKey?>? crypt, ConfigPropertyOptions options,
            IEnumerable<String>? sections)
        {
            return GetProperty(key, value, validate, crypt?.Invoke(), options, sections);
        }

        public IReadOnlyConfigProperty<T> GetProperty<T>(String? key, T value, Func<T, Boolean>? validate, Func<ICryptKey?>? crypt, ConfigPropertyOptions options, params String[]? sections)
        {
            return GetProperty(key, value, validate, crypt, options, (IEnumerable<String>?) sections);
        }

        public IReadOnlyConfigProperty<T> GetProperty<T>(String? key, T value, Func<T, Boolean>? validate, Func<String?, ICryptKey?>? crypt, ConfigPropertyOptions options,
            IEnumerable<String>? sections)
        {
            return GetProperty(key, value, validate, crypt?.Invoke(key), options, sections);
        }

        public IReadOnlyConfigProperty<T> GetProperty<T>(String? key, T value, Func<T, Boolean>? validate, Func<String?, ICryptKey?>? crypt, ConfigPropertyOptions options,
            params String[]? sections)
        {
            return GetProperty(key, value, validate, crypt, options, (IEnumerable<String>?) sections);
        }

        public IReadOnlyConfigProperty<T> GetProperty<T>(String? key, T value, Func<T, Boolean>? validate, Func<String?, String[]?, ICryptKey?>? crypt, ConfigPropertyOptions options,
            IEnumerable<String>? sections)
        {
            String[]? array = sections?.ToArray();
            return GetProperty(key, value, validate, crypt?.Invoke(key, array), options, array);
        }

        public IReadOnlyConfigProperty<T> GetProperty<T>(String? key, T value, Func<T, Boolean>? validate, Func<String?, String[]?, ICryptKey?>? crypt, ConfigPropertyOptions options,
            params String[]? sections)
        {
            return GetProperty(key, value, validate, crypt, options, (IEnumerable<String>?) sections);
        }

        public IReadOnlyConfigProperty<T> GetProperty<T>(String? key, T value, Func<T, Boolean>? validate, CryptAction crypt, ConfigPropertyOptions options,
            TryConverter<String?, T>? converter, IEnumerable<String>? sections)
        {
            return GetProperty(key, value, validate, CryptKey.Create(crypt), options, converter, sections);
        }

        public IReadOnlyConfigProperty<T> GetProperty<T>(String? key, T value, Func<T, Boolean>? validate, CryptAction crypt, ConfigPropertyOptions options,
            TryConverter<String?, T>? converter, params String[]? sections)
        {
            return GetProperty(key, value, validate, crypt, options, converter, (IEnumerable<String>?) sections);
        }

        public IReadOnlyConfigProperty<T> GetProperty<T>(String? key, T value, Func<T, Boolean>? validate, ICryptKey? crypt, ConfigPropertyOptions options,
            TryConverter<String?, T>? converter, IEnumerable<String>? sections);

        public IReadOnlyConfigProperty<T> GetProperty<T>(String? key, T value, Func<T, Boolean>? validate, ICryptKey? crypt, ConfigPropertyOptions options,
            TryConverter<String?, T>? converter, params String[]? sections)
        {
            return GetProperty(key, value, validate, crypt, options, converter, (IEnumerable<String>?) sections);
        }

        public IReadOnlyConfigProperty<T> GetProperty<T>(String? key, T value, Func<T, Boolean>? validate, Func<ICryptKey?>? crypt, ConfigPropertyOptions options,
            TryConverter<String?, T>? converter, IEnumerable<String>? sections)
        {
            return GetProperty(key, value, validate, crypt?.Invoke(), options, converter, sections);
        }

        public IReadOnlyConfigProperty<T> GetProperty<T>(String? key, T value, Func<T, Boolean>? validate, Func<ICryptKey?>? crypt, ConfigPropertyOptions options,
            TryConverter<String?, T>? converter, params String[]? sections)
        {
            return GetProperty(key, value, validate, crypt, options, converter, (IEnumerable<String>?) sections);
        }

        public IReadOnlyConfigProperty<T> GetProperty<T>(String? key, T value, Func<T, Boolean>? validate, Func<String?, ICryptKey?>? crypt, ConfigPropertyOptions options,
            TryConverter<String?, T>? converter, IEnumerable<String>? sections)
        {
            return GetProperty(key, value, validate, crypt?.Invoke(key), options, converter, sections);
        }

        public IReadOnlyConfigProperty<T> GetProperty<T>(String? key, T value, Func<T, Boolean>? validate, Func<String?, ICryptKey?>? crypt, ConfigPropertyOptions options,
            TryConverter<String?, T>? converter, params String[]? sections)
        {
            return GetProperty(key, value, validate, crypt, options, converter, (IEnumerable<String>?) sections);
        }

        public IReadOnlyConfigProperty<T> GetProperty<T>(String? key, T value, Func<T, Boolean>? validate, Func<String?, String[]?, ICryptKey?>? crypt, ConfigPropertyOptions options,
            TryConverter<String?, T>? converter, IEnumerable<String>? sections)
        {
            String[]? array = sections?.ToArray();
            return GetProperty(key, value, validate, crypt?.Invoke(key, array), options, converter, array);
        }

        public IReadOnlyConfigProperty<T> GetProperty<T>(String? key, T value, Func<T, Boolean>? validate, Func<String?, String[]?, ICryptKey?>? crypt, ConfigPropertyOptions options,
            TryConverter<String?, T>? converter, params String[]? sections)
        {
            return GetProperty(key, value, validate, crypt, options, converter, (IEnumerable<String>?) sections);
        }

        public IEnumerable<IReadOnlyConfigPropertyBase>? GetProperties();
    }
}