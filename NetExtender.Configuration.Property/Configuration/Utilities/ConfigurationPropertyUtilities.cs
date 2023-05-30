// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using NetExtender.Configuration.Common;
using NetExtender.Configuration.Interfaces;
using NetExtender.Configuration.Properties;
using NetExtender.Configuration.Properties.Interfaces;
using NetExtender.Configuration.Synchronizers.Interfaces;
using NetExtender.Utilities.Types;

namespace NetExtender.Configuration.Utilities
{
    public static class ConfigurationPropertyUtilities
    {
        public static IReadOnlyConfigProperty AsReadOnly(this IConfigProperty property)
        {
            if (property is null)
            {
                throw new ArgumentNullException(nameof(property));
            }

            return new ReadOnlyConfigPropertyWrapper(property);
        }

        public static IReadOnlyConfigProperty<T> AsReadOnly<T>(this IConfigProperty<T> property)
        {
            if (property is null)
            {
                throw new ArgumentNullException(nameof(property));
            }

            return new ReadOnlyConfigPropertyWrapper<T>(property);
        }

        public static IReadOnlyConfigProperty<T?> Converter<T>(this IReadOnlyConfigProperty property)
        {
            return Converter(property, default(T));
        }

        public static IReadOnlyConfigProperty<T?> Converter<T>(this IReadOnlyConfigProperty property, Func<T, Boolean>? validate)
        {
            return Converter<T>(property, validate, null);
        }

        public static IReadOnlyConfigProperty<T?> Converter<T>(this IReadOnlyConfigProperty property, TryConverter<String?, T>? converter)
        {
            return Converter(property, null, converter);
        }

        public static IReadOnlyConfigProperty<T?> Converter<T>(this IReadOnlyConfigProperty property, Func<T, Boolean>? validate, TryConverter<String?, T>? converter)
        {
            return Converter(property, default, validate!, converter!);
        }

        public static IReadOnlyConfigProperty<T> Converter<T>(this IReadOnlyConfigProperty property, T alternate)
        {
            return Converter(property, alternate, null, null);
        }

        public static IReadOnlyConfigProperty<T> Converter<T>(this IReadOnlyConfigProperty property, T alternate, Func<T, Boolean>? validate)
        {
            return Converter(property, alternate, validate, null);
        }

        public static IReadOnlyConfigProperty<T> Converter<T>(this IReadOnlyConfigProperty property, T alternate, TryConverter<String?, T>? converter)
        {
            return Converter(property, alternate, null, converter);
        }

        public static IReadOnlyConfigProperty<T> Converter<T>(this IReadOnlyConfigProperty property, T alternate, Func<T, Boolean>? validate, TryConverter<String?, T>? converter)
        {
            if (property is null)
            {
                throw new ArgumentNullException(nameof(property));
            }

            return new ReadOnlyConfigProperty<T>(property, alternate, validate, converter);
        }

        public static IConfigProperty<T?> Converter<T>(this IConfigProperty property)
        {
            return Converter(property, default(T));
        }

        public static IConfigProperty<T?> Converter<T>(this IConfigProperty property, Func<T, Boolean>? validate)
        {
            return Converter<T>(property, validate, null);
        }

        public static IConfigProperty<T?> Converter<T>(this IConfigProperty property, TryConverter<String?, T>? converter)
        {
            return Converter(property, null, converter);
        }

        public static IConfigProperty<T?> Converter<T>(this IConfigProperty property, Func<T, Boolean>? validate, TryConverter<String?, T>? converter)
        {
            return Converter(property, default, validate!, converter!);
        }

        public static IConfigProperty<T> Converter<T>(this IConfigProperty property, T alternate)
        {
            return Converter(property, alternate, null, null);
        }

        public static IConfigProperty<T> Converter<T>(this IConfigProperty property, T alternate, Func<T, Boolean>? validate)
        {
            return Converter(property, alternate, validate, null);
        }

        public static IConfigProperty<T> Converter<T>(this IConfigProperty property, T alternate, TryConverter<String?, T>? converter)
        {
            return Converter(property, alternate, null, converter);
        }

        public static IConfigProperty<T> Converter<T>(this IConfigProperty property, T alternate, Func<T, Boolean>? validate, TryConverter<String?, T>? converter)
        {
            if (property is null)
            {
                throw new ArgumentNullException(nameof(property));
            }

            return new ConfigProperty<T>(property, alternate, validate, converter);
        }

        public static T? GetValue<T>(this IReadOnlyConfigProperty property, TryConverter<String?, T>? converter)
        {
            if (property is null)
            {
                throw new ArgumentNullException(nameof(property));
            }

            return GetValue(property, converter?.Invoke(property.Alternate, out T? result) == true ? result : default, converter!);
        }

        public static T GetValue<T>(this IReadOnlyConfigProperty property, T alternate)
        {
            return GetValue(property, alternate, null);
        }

        public static T GetValue<T>(this IReadOnlyConfigProperty property, T alternate, TryConverter<String, T>? converter)
        {
            if (property is null)
            {
                throw new ArgumentNullException(nameof(property));
            }

            String? value = property.GetValue();

            if (value is null)
            {
                return alternate;
            }

            converter ??= ConvertUtilities.TryConvert;
            return converter(value, out T? result) ? result : alternate;
        }

        public static Task<T?> GetValueAsync<T>(this IReadOnlyConfigProperty property, TryConverter<String?, T>? converter)
        {
            return GetValueAsync(property, converter, CancellationToken.None);
        }

        public static Task<T?> GetValueAsync<T>(this IReadOnlyConfigProperty property, TryConverter<String?, T>? converter, CancellationToken token)
        {
            if (property is null)
            {
                throw new ArgumentNullException(nameof(property));
            }

            return GetValueAsync(property, converter?.Invoke(property.Alternate, out T? result) == true ? result : default, converter!, token);
        }

        public static Task<T> GetValueAsync<T>(this IReadOnlyConfigProperty property, T alternate)
        {
            return GetValueAsync(property, alternate, CancellationToken.None);
        }

        public static Task<T> GetValueAsync<T>(this IReadOnlyConfigProperty property, T alternate, CancellationToken token)
        {
            return GetValueAsync(property, alternate, null, token);
        }

        public static Task<T> GetValueAsync<T>(this IReadOnlyConfigProperty property, T alternate, TryConverter<String, T>? converter)
        {
            return GetValueAsync(property, alternate, converter, CancellationToken.None);
        }

        public static async Task<T> GetValueAsync<T>(this IReadOnlyConfigProperty property, T alternate, TryConverter<String, T>? converter, CancellationToken token)
        {
            if (property is null)
            {
                throw new ArgumentNullException(nameof(property));
            }

            String? value = await property.GetValueAsync(token).ConfigureAwait(false);

            if (value is null)
            {
                return alternate;
            }

            converter ??= ConvertUtilities.TryConvert;
            return converter(value, out T? result) ? result : alternate;
        }

        public static T? GetValue<T>(this IConfigProperty property, TryConverter<String?, T>? converter)
        {
            if (property is null)
            {
                throw new ArgumentNullException(nameof(property));
            }

            return GetValue(property, converter?.Invoke(property.Alternate, out T? result) == true ? result : default, converter!);
        }

        public static T GetValue<T>(this IConfigProperty property, T alternate)
        {
            return GetValue(property, alternate, null);
        }

        public static T GetValue<T>(this IConfigProperty property, T alternate, TryConverter<String, T>? converter)
        {
            if (property is null)
            {
                throw new ArgumentNullException(nameof(property));
            }

            String? value = property.GetValue();

            if (value is null)
            {
                return alternate;
            }

            converter ??= ConvertUtilities.TryConvert;
            return converter(value, out T? result) ? result : alternate;
        }

        public static Task<T?> GetValueAsync<T>(this IConfigProperty property, TryConverter<String?, T>? converter)
        {
            return GetValueAsync(property, converter, CancellationToken.None);
        }

        public static Task<T?> GetValueAsync<T>(this IConfigProperty property, TryConverter<String?, T>? converter, CancellationToken token)
        {
            if (property is null)
            {
                throw new ArgumentNullException(nameof(property));
            }

            return GetValueAsync(property, converter?.Invoke(property.Alternate, out T? result) == true ? result : default, converter!, token);
        }

        public static Task<T> GetValueAsync<T>(this IConfigProperty property, T alternate)
        {
            return GetValueAsync(property, alternate, CancellationToken.None);
        }

        public static Task<T> GetValueAsync<T>(this IConfigProperty property, T alternate, CancellationToken token)
        {
            return GetValueAsync(property, alternate, null, token);
        }

        public static Task<T> GetValueAsync<T>(this IConfigProperty property, T alternate, TryConverter<String, T>? converter)
        {
            return GetValueAsync(property, alternate, converter, CancellationToken.None);
        }

        public static async Task<T> GetValueAsync<T>(this IConfigProperty property, T alternate, TryConverter<String, T>? converter, CancellationToken token)
        {
            if (property is null)
            {
                throw new ArgumentNullException(nameof(property));
            }

            String? value = await property.GetValueAsync(token).ConfigureAwait(false);

            if (value is null)
            {
                return alternate;
            }

            converter ??= ConvertUtilities.TryConvert;
            return converter(value, out T? result) ? result : alternate;
        }

        public static Boolean SetValue<T>(this IConfigProperty property, T value)
        {
            if (property is null)
            {
                throw new ArgumentNullException(nameof(property));
            }

            if (value is null)
            {
                return property.SetValue(null);
            }

            String? convert = value.GetString();
            return convert is not null && property.SetValue(convert);
        }

        public static Task<Boolean> SetValueAsync<T>(this IConfigProperty property, T value)
        {
            return SetValueAsync(property, value, CancellationToken.None);
        }

        public static async Task<Boolean> SetValueAsync<T>(this IConfigProperty property, T value, CancellationToken token)
        {
            if (property is null)
            {
                throw new ArgumentNullException(nameof(property));
            }

            if (value is null)
            {
                return await property.SetValueAsync(null, token).ConfigureAwait(false);
            }

            String? convert = value.GetString();
            return convert is not null && await property.SetValueAsync(convert, token).ConfigureAwait(false);
        }

        public static IReadOnlyConfigProperty GetConfigurationProperty(this IReadOnlyConfig config, String? key, params String[]? sections)
        {
            return GetConfigurationProperty(config, key, (IEnumerable<String>?) sections);
        }

        public static IReadOnlyConfigProperty GetConfigurationProperty(this IReadOnlyConfig config, String? key, IEnumerable<String>? sections)
        {
            return GetConfigurationProperty(config, key, null, sections);
        }

        public static IReadOnlyConfigProperty GetConfigurationProperty(this IReadOnlyConfig config, String? key, ConfigPropertyOptions options, params String[]? sections)
        {
            return GetConfigurationProperty(config, key, options, (IEnumerable<String>?) sections);
        }

        public static IReadOnlyConfigProperty GetConfigurationProperty(this IReadOnlyConfig config, String? key, ConfigPropertyOptions options, IEnumerable<String>? sections)
        {
            return GetConfigurationProperty(config, key, null, options, sections);
        }

        public static IReadOnlyConfigProperty GetConfigurationProperty(this IReadOnlyConfig config, String? key, String? alternate, params String[]? sections)
        {
            return GetConfigurationProperty(config, key, alternate, (IEnumerable<String>?) sections);
        }

        public static IReadOnlyConfigProperty GetConfigurationProperty(this IReadOnlyConfig config, String? key, String? alternate, IEnumerable<String>? sections)
        {
            return GetConfigurationProperty(config, key, alternate, ConfigPropertyOptions.None, sections);
        }

        public static IReadOnlyConfigProperty GetConfigurationProperty(this IReadOnlyConfig config, String? key, String? alternate, ConfigPropertyOptions options, params String[]? sections)
        {
            return GetConfigurationProperty(config, key, alternate, options, (IEnumerable<String>?) sections);
        }

        public static IReadOnlyConfigProperty GetConfigurationProperty(this IReadOnlyConfig config, String? key, String? alternate, ConfigPropertyOptions options, IEnumerable<String>? sections)
        {
            return new ReadOnlyConfigProperty(config, key, alternate, options, sections);
        }

        public static IConfigProperty GetConfigurationProperty(this IConfig config, String? key, params String[]? sections)
        {
            return GetConfigurationProperty(config, key, (IEnumerable<String>?) sections);
        }

        public static IConfigProperty GetConfigurationProperty(this IConfig config, String? key, IEnumerable<String>? sections)
        {
            return GetConfigurationProperty(config, key, null, sections);
        }

        public static IConfigProperty GetConfigurationProperty(this IConfig config, String? key, ConfigPropertyOptions options, params String[]? sections)
        {
            return GetConfigurationProperty(config, key, options, (IEnumerable<String>?) sections);
        }

        public static IConfigProperty GetConfigurationProperty(this IConfig config, String? key, ConfigPropertyOptions options, IEnumerable<String>? sections)
        {
            return GetConfigurationProperty(config, key, null, options, sections);
        }

        public static IConfigProperty GetConfigurationProperty(this IConfig config, String? key, String? alternate, params String[]? sections)
        {
            return GetConfigurationProperty(config, key, alternate, (IEnumerable<String>?) sections);
        }

        public static IConfigProperty GetConfigurationProperty(this IConfig config, String? key, String? alternate, IEnumerable<String>? sections)
        {
            return GetConfigurationProperty(config, key, alternate, ConfigPropertyOptions.None, sections);
        }

        public static IConfigProperty GetConfigurationProperty(this IConfig config, String? key, String? alternate, ConfigPropertyOptions options, params String[]? sections)
        {
            return GetConfigurationProperty(config, key, alternate, options, (IEnumerable<String>?) sections);
        }

        public static IConfigProperty GetConfigurationProperty(this IConfig config, String? key, String? alternate, ConfigPropertyOptions options, IEnumerable<String>? sections)
        {
            return new ConfigProperty(config, key, alternate, options, sections);
        }

        public static IReadOnlyConfigProperty GetReadOnlyConfigurationProperty(this IConfig config, String? key, params String[]? sections)
        {
            return GetReadOnlyConfigurationProperty(config, key, (IEnumerable<String>?) sections);
        }

        public static IReadOnlyConfigProperty GetReadOnlyConfigurationProperty(this IConfig config, String? key, IEnumerable<String>? sections)
        {
            return GetReadOnlyConfigurationProperty(config, key, null, sections);
        }

        public static IReadOnlyConfigProperty GetReadOnlyConfigurationProperty(this IConfig config, String? key, ConfigPropertyOptions options, params String[]? sections)
        {
            return GetReadOnlyConfigurationProperty(config, key, options, (IEnumerable<String>?) sections);
        }

        public static IReadOnlyConfigProperty GetReadOnlyConfigurationProperty(this IConfig config, String? key, ConfigPropertyOptions options, IEnumerable<String>? sections)
        {
            return GetReadOnlyConfigurationProperty(config, key, null, options, sections);
        }

        public static IReadOnlyConfigProperty GetReadOnlyConfigurationProperty(this IConfig config, String? key, String? alternate, params String[]? sections)
        {
            return GetReadOnlyConfigurationProperty(config, key, alternate, (IEnumerable<String>?) sections);
        }

        public static IReadOnlyConfigProperty GetReadOnlyConfigurationProperty(this IConfig config, String? key, String? alternate, IEnumerable<String>? sections)
        {
            return GetReadOnlyConfigurationProperty(config, key, alternate, ConfigPropertyOptions.None, sections);
        }

        public static IReadOnlyConfigProperty GetReadOnlyConfigurationProperty(this IConfig config, String? key, String? alternate, ConfigPropertyOptions options, params String[]? sections)
        {
            return GetReadOnlyConfigurationProperty(config, key, alternate, options, (IEnumerable<String>?) sections);
        }

        public static IReadOnlyConfigProperty GetReadOnlyConfigurationProperty(this IConfig config, String? key, String? alternate, ConfigPropertyOptions options, IEnumerable<String>? sections)
        {
            return new ReadOnlyConfigPropertyWrapper(config, key, alternate, options, sections);
        }

        public static IReadOnlyConfigProperty<T?> GetConfigurationProperty<T>(this IReadOnlyConfig config, String? key, params String[]? sections)
        {
            return GetConfigurationProperty<T>(config, key, (IEnumerable<String>?) sections);
        }

        public static IReadOnlyConfigProperty<T?> GetConfigurationProperty<T>(this IReadOnlyConfig config, String? key, IEnumerable<String>? sections)
        {
            return GetConfigurationProperty(config, key, default(T), sections);
        }

        public static IReadOnlyConfigProperty<T> GetConfigurationProperty<T>(this IReadOnlyConfig config, String? key, T alternate, params String[]? sections)
        {
            return GetConfigurationProperty(config, key, alternate, (IEnumerable<String>?) sections);
        }

        public static IReadOnlyConfigProperty<T> GetConfigurationProperty<T>(this IReadOnlyConfig config, String? key, T alternate, IEnumerable<String>? sections)
        {
            return GetConfigurationProperty(config, key, alternate, null, sections);
        }

        public static IReadOnlyConfigProperty<T> GetConfigurationProperty<T>(this IReadOnlyConfig config, String? key, T alternate, ConfigPropertyOptions options, params String[]? sections)
        {
            return GetConfigurationProperty(config, key, alternate, options, (IEnumerable<String>?) sections);
        }

        public static IReadOnlyConfigProperty<T> GetConfigurationProperty<T>(this IReadOnlyConfig config, String? key, T alternate, ConfigPropertyOptions options, IEnumerable<String>? sections)
        {
            return GetConfigurationProperty(config, key, alternate, null, options, sections);
        }

        public static IReadOnlyConfigProperty<T> GetConfigurationProperty<T>(this IReadOnlyConfig config, String? key, T alternate, Func<T, Boolean>? validate, params String[]? sections)
        {
            return GetConfigurationProperty(config, key, alternate, validate, (IEnumerable<String>?) sections);
        }

        public static IReadOnlyConfigProperty<T> GetConfigurationProperty<T>(this IReadOnlyConfig config, String? key, T alternate, Func<T, Boolean>? validate, IEnumerable<String>? sections)
        {
            return GetConfigurationProperty(config, key, alternate, validate, ConfigPropertyOptions.None, sections);
        }

        public static IReadOnlyConfigProperty<T> GetConfigurationProperty<T>(this IReadOnlyConfig config, String? key, T alternate, Func<T, Boolean>? validate, ConfigPropertyOptions options, params String[]? sections)
        {
            return GetConfigurationProperty(config, key, alternate, validate, options, (IEnumerable<String>?) sections);
        }

        public static IReadOnlyConfigProperty<T> GetConfigurationProperty<T>(this IReadOnlyConfig config, String? key, T alternate, Func<T, Boolean>? validate, ConfigPropertyOptions options, IEnumerable<String>? sections)
        {
            return GetConfigurationProperty(config, key, alternate, validate, null, options, sections);
        }

        public static IReadOnlyConfigProperty<T> GetConfigurationProperty<T>(this IReadOnlyConfig config, String? key, T alternate, Func<T, Boolean>? validate, TryConverter<String?, T>? converter, ConfigPropertyOptions options, params String[]? sections)
        {
            return GetConfigurationProperty(config, key, alternate, validate, converter, options, (IEnumerable<String>?) sections);
        }

        public static IReadOnlyConfigProperty<T> GetConfigurationProperty<T>(this IReadOnlyConfig config, String? key, T alternate, Func<T, Boolean>? validate, TryConverter<String?, T>? converter, ConfigPropertyOptions options, IEnumerable<String>? sections)
        {
            return new ReadOnlyConfigProperty<T>(config, key, alternate, validate, converter, options, sections);
        }

        public static IConfigProperty<T?> GetConfigurationProperty<T>(this IConfig config, String? key, params String[]? sections)
        {
            return GetConfigurationProperty<T>(config, key, (IEnumerable<String>?) sections);
        }

        public static IConfigProperty<T?> GetConfigurationProperty<T>(this IConfig config, String? key, IEnumerable<String>? sections)
        {
            return GetConfigurationProperty(config, key, default(T), sections);
        }

        public static IConfigProperty<T> GetConfigurationProperty<T>(this IConfig config, String? key, T alternate, params String[]? sections)
        {
            return GetConfigurationProperty(config, key, alternate, (IEnumerable<String>?) sections);
        }

        public static IConfigProperty<T> GetConfigurationProperty<T>(this IConfig config, String? key, T alternate, IEnumerable<String>? sections)
        {
            return GetConfigurationProperty(config, key, alternate, null, sections);
        }

        public static IConfigProperty<T> GetConfigurationProperty<T>(this IConfig config, String? key, T alternate, ConfigPropertyOptions options, params String[]? sections)
        {
            return GetConfigurationProperty(config, key, alternate, options, (IEnumerable<String>?) sections);
        }

        public static IConfigProperty<T> GetConfigurationProperty<T>(this IConfig config, String? key, T alternate, ConfigPropertyOptions options, IEnumerable<String>? sections)
        {
            return GetConfigurationProperty(config, key, alternate, null, options, sections);
        }

        public static IConfigProperty<T> GetConfigurationProperty<T>(this IConfig config, String? key, T alternate, Func<T, Boolean>? validate, params String[]? sections)
        {
            return GetConfigurationProperty(config, key, alternate, validate, (IEnumerable<String>?) sections);
        }

        public static IConfigProperty<T> GetConfigurationProperty<T>(this IConfig config, String? key, T alternate, Func<T, Boolean>? validate, IEnumerable<String>? sections)
        {
            return GetConfigurationProperty(config, key, alternate, validate, ConfigPropertyOptions.None, sections);
        }

        public static IConfigProperty<T> GetConfigurationProperty<T>(this IConfig config, String? key, T alternate, Func<T, Boolean>? validate, ConfigPropertyOptions options, params String[]? sections)
        {
            return GetConfigurationProperty(config, key, alternate, validate, options, (IEnumerable<String>?) sections);
        }

        public static IConfigProperty<T> GetConfigurationProperty<T>(this IConfig config, String? key, T alternate, Func<T, Boolean>? validate, ConfigPropertyOptions options, IEnumerable<String>? sections)
        {
            return GetConfigurationProperty(config, key, alternate, validate, null, options, sections);
        }

        public static IConfigProperty<T> GetConfigurationProperty<T>(this IConfig config, String? key, T alternate, Func<T, Boolean>? validate, TryConverter<String?, T>? converter, ConfigPropertyOptions options, params String[]? sections)
        {
            return GetConfigurationProperty(config, key, alternate, validate, converter, options, (IEnumerable<String>?) sections);
        }

        public static IConfigProperty<T> GetConfigurationProperty<T>(this IConfig config, String? key, T alternate, Func<T, Boolean>? validate, TryConverter<String?, T>? converter, ConfigPropertyOptions options, IEnumerable<String>? sections)
        {
            return new ConfigProperty<T>(config, key, alternate, validate, converter, options, sections);
        }

        public static IReadOnlyConfigProperty<T?> GetReadOnlyConfigurationProperty<T>(this IConfig config, String? key, params String[]? sections)
        {
            return GetReadOnlyConfigurationProperty<T>(config, key, (IEnumerable<String>?) sections);
        }

        public static IReadOnlyConfigProperty<T?> GetReadOnlyConfigurationProperty<T>(this IConfig config, String? key, IEnumerable<String>? sections)
        {
            return GetReadOnlyConfigurationProperty(config, key, default(T), sections);
        }

        public static IReadOnlyConfigProperty<T> GetReadOnlyConfigurationProperty<T>(this IConfig config, String? key, T alternate, params String[]? sections)
        {
            return GetReadOnlyConfigurationProperty(config, key, alternate, (IEnumerable<String>?) sections);
        }

        public static IReadOnlyConfigProperty<T> GetReadOnlyConfigurationProperty<T>(this IConfig config, String? key, T alternate, IEnumerable<String>? sections)
        {
            return GetReadOnlyConfigurationProperty(config, key, alternate, null, sections);
        }

        public static IReadOnlyConfigProperty<T> GetReadOnlyConfigurationProperty<T>(this IConfig config, String? key, T alternate, ConfigPropertyOptions options, params String[]? sections)
        {
            return GetReadOnlyConfigurationProperty(config, key, alternate, options, (IEnumerable<String>?) sections);
        }

        public static IReadOnlyConfigProperty<T> GetReadOnlyConfigurationProperty<T>(this IConfig config, String? key, T alternate, ConfigPropertyOptions options, IEnumerable<String>? sections)
        {
            return GetReadOnlyConfigurationProperty(config, key, alternate, null, options, sections);
        }

        public static IReadOnlyConfigProperty<T> GetReadOnlyConfigurationProperty<T>(this IConfig config, String? key, T alternate, Func<T, Boolean>? validate, params String[]? sections)
        {
            return GetReadOnlyConfigurationProperty(config, key, alternate, validate, (IEnumerable<String>?) sections);
        }

        public static IReadOnlyConfigProperty<T> GetReadOnlyConfigurationProperty<T>(this IConfig config, String? key, T alternate, Func<T, Boolean>? validate, IEnumerable<String>? sections)
        {
            return GetReadOnlyConfigurationProperty(config, key, alternate, validate, ConfigPropertyOptions.None, sections);
        }

        public static IReadOnlyConfigProperty<T> GetReadOnlyConfigurationProperty<T>(this IConfig config, String? key, T alternate, Func<T, Boolean>? validate, ConfigPropertyOptions options, params String[]? sections)
        {
            return GetReadOnlyConfigurationProperty(config, key, alternate, validate, options, (IEnumerable<String>?) sections);
        }

        public static IReadOnlyConfigProperty<T> GetReadOnlyConfigurationProperty<T>(this IConfig config, String? key, T alternate, Func<T, Boolean>? validate, ConfigPropertyOptions options, IEnumerable<String>? sections)
        {
            return GetReadOnlyConfigurationProperty(config, key, alternate, validate, null, options, sections);
        }

        public static IReadOnlyConfigProperty<T> GetReadOnlyConfigurationProperty<T>(this IConfig config, String? key, T alternate, Func<T, Boolean>? validate, TryConverter<String?, T>? converter, ConfigPropertyOptions options, params String[]? sections)
        {
            return GetReadOnlyConfigurationProperty(config, key, alternate, validate, converter, options, (IEnumerable<String>?) sections);
        }

        public static IReadOnlyConfigProperty<T> GetReadOnlyConfigurationProperty<T>(this IConfig config, String? key, T alternate, Func<T, Boolean>? validate, TryConverter<String?, T>? converter, ConfigPropertyOptions options, IEnumerable<String>? sections)
        {
            return new ReadOnlyConfigPropertyWrapper<T>(config, key, alternate, validate, converter, options, sections);
        }

        public static TProperty Synchronize<TProperty>(this TProperty property, IConfigPropertySynchronizer synchronizer) where TProperty : IConfigPropertyInfo
        {
            if (property is null)
            {
                throw new ArgumentNullException(nameof(property));
            }

            if (synchronizer is null)
            {
                throw new ArgumentNullException(nameof(synchronizer));
            }
            
            synchronizer.Add(property);
            return property;
        }
    }
}