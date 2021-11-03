// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Threading;
using System.Threading.Tasks;
using NetExtender.Configuration.Behavior.Interfaces;
using NetExtender.Configuration.Common;
using NetExtender.Configuration.Convert.Interfaces;
using NetExtender.Configuration.Interfaces;
using NetExtender.Utilities.Types;

namespace NetExtender.Configuration.Convert
{
    public class ConverterConfig : IConverterConfig, IReadOnlyConverterConfig
    {
        private IConfig Config { get; }

        public event EventHandler<ConfigurationEntry> Changed
        {
            add
            {
                Config.Changed += value;
            }
            remove
            {
                Config.Changed -= value;
            }
        }

        public String Path
        {
            get
            {
                return Config.Path;
            }
        }

        public ConfigOptions Options
        {
            get
            {
                return Config.Options;
            }
        }

        public Boolean IsReadOnly
        {
            get
            {
                return Config.IsReadOnly;
            }
        }

        public Boolean IsLazyWrite
        {
            get
            {
                return Config.IsLazyWrite;
            }
        }
        
        public ConverterConfig(IConfigBehavior behavior)
        {
            if (behavior is null)
            {
                throw new ArgumentNullException(nameof(behavior));
            }

            Config = new Config(behavior);
        }

        private static Boolean TryConvert<T>(String value, [MaybeNullWhen(false)] out T result)
        {
            return value.TryConvert(out result);
        }

        public String? GetValue(String? key, params String[]? sections)
        {
            return Config.GetValue(key, sections);
        }

        public String? GetValue(String? key, IEnumerable<String>? sections)
        {
            return Config.GetValue(key, sections);
        }

        public String? GetValue(String? key, String? alternate, params String[]? sections)
        {
            return Config.GetValue(key, alternate, sections);
        }

        public String? GetValue(String? key, String? alternate, IEnumerable<String>? sections)
        {
            return Config.GetValue(key, alternate, sections);
        }

        public Task<String?> GetValueAsync(String? key, params String[]? sections)
        {
            return Config.GetValueAsync(key, sections);
        }

        public Task<String?> GetValueAsync(String? key, IEnumerable<String>? sections)
        {
            return Config.GetValueAsync(key, sections);
        }

        public Task<String?> GetValueAsync(String? key, CancellationToken token, params String[]? sections)
        {
            return Config.GetValueAsync(key, token, sections);
        }

        public Task<String?> GetValueAsync(String? key, IEnumerable<String>? sections, CancellationToken token)
        {
            return Config.GetValueAsync(key, sections, token);
        }

        public Task<String?> GetValueAsync(String? key, String? alternate, params String[]? sections)
        {
            return Config.GetValueAsync(key, alternate, sections);
        }

        public Task<String?> GetValueAsync(String? key, String? alternate, IEnumerable<String>? sections)
        {
            return Config.GetValueAsync(key, alternate, sections);
        }

        public Task<String?> GetValueAsync(String? key, String? alternate, CancellationToken token, params String[]? sections)
        {
            return Config.GetValueAsync(key, alternate, token, sections);
        }

        public Task<String?> GetValueAsync(String? key, String? alternate, IEnumerable<String>? sections, CancellationToken token)
        {
            return Config.GetValueAsync(key, alternate, sections, token);
        }
        
        public T? GetValue<T>(String? key, params String[]? sections)
        {
            return GetValue<T>(key, (IEnumerable<String>?) sections);
        }

        public T? GetValue<T>(String? key, IEnumerable<String>? sections)
        {
            return GetValue(key, default(T), sections);
        }
        
        public T? GetValue<T>(String? key, TryConverter<String, T>? converter, params String[]? sections)
        {
            return GetValue(key, converter, (IEnumerable<String>?) sections);
        }

        public T? GetValue<T>(String? key, TryConverter<String, T>? converter, IEnumerable<String>? sections)
        {
            return GetValue(key, default, converter!, sections);
        }

        public T GetValue<T>(String? key, T alternate, params String[]? sections)
        {
            return GetValue(key, alternate, (IEnumerable<String>?) sections);
        }

        public T GetValue<T>(String? key, T alternate, IEnumerable<String>? sections)
        {
            return GetValue(key, alternate, TryConvert, sections);
        }

        public T GetValue<T>(String? key, T alternate, TryConverter<String, T>? converter, params String[]? sections)
        {
            return GetValue(key, alternate, converter, (IEnumerable<String>?) sections);
        }

        public T GetValue<T>(String? key, T alternate, TryConverter<String, T>? converter, IEnumerable<String>? sections)
        {
            String? value = GetValue(key, sections);

            if (value is null)
            {
                return alternate;
            }

            converter ??= TryConvert;
            return converter(value, out T? result) ? result : alternate;
        }

        public Task<T?> GetValueAsync<T>(String? key, params String[]? sections)
        {
            return GetValueAsync<T>(key, (IEnumerable<String>?) sections);
        }

        public Task<T?> GetValueAsync<T>(String? key, IEnumerable<String>? sections)
        {
            return GetValueAsync<T>(key, sections, CancellationToken.None);
        }

        public Task<T?> GetValueAsync<T>(String? key, CancellationToken token, params String[]? sections)
        {
            return GetValueAsync<T>(key, sections, token);
        }

        public Task<T?> GetValueAsync<T>(String? key, IEnumerable<String>? sections, CancellationToken token)
        {
            return GetValueAsync(key, default(T), sections, token);
        }
        
        public Task<T?> GetValueAsync<T>(String? key, TryConverter<String, T>? converter, params String[]? sections)
        {
            return GetValueAsync(key, converter, (IEnumerable<String>?) sections);
        }

        public Task<T?> GetValueAsync<T>(String? key, TryConverter<String, T>? converter, IEnumerable<String>? sections)
        {
            return GetValueAsync(key, converter, sections, CancellationToken.None);
        }

        public Task<T?> GetValueAsync<T>(String? key, TryConverter<String, T>? converter, CancellationToken token, params String[]? sections)
        {
            return GetValueAsync(key, converter, sections, token);
        }

        public Task<T?> GetValueAsync<T>(String? key, TryConverter<String, T>? converter, IEnumerable<String>? sections, CancellationToken token)
        {
            return GetValueAsync(key, default, converter!, sections, token);
        }

        public Task<T> GetValueAsync<T>(String? key, T alternate, params String[]? sections)
        {
            return GetValueAsync(key, alternate, (IEnumerable<String>?) sections);
        }

        public Task<T> GetValueAsync<T>(String? key, T alternate, IEnumerable<String>? sections)
        {
            return GetValueAsync(key, alternate, sections, CancellationToken.None);
        }

        public Task<T> GetValueAsync<T>(String? key, T alternate, CancellationToken token, params String[]? sections)
        {
            return GetValueAsync(key, alternate, sections, token);
        }

        public Task<T> GetValueAsync<T>(String? key, T alternate, IEnumerable<String>? sections, CancellationToken token)
        {
            return GetValueAsync(key, alternate, TryConvert, sections, token);
        }

        public Task<T> GetValueAsync<T>(String? key, T alternate, TryConverter<String, T>? converter, params String[]? sections)
        {
            return GetValueAsync(key, alternate, converter, (IEnumerable<String>?) sections);
        }

        public Task<T> GetValueAsync<T>(String? key, T alternate, TryConverter<String, T>? converter, IEnumerable<String>? sections)
        {
            return GetValueAsync(key, alternate, converter, sections, CancellationToken.None);
        }

        public Task<T> GetValueAsync<T>(String? key, T alternate, TryConverter<String, T>? converter, CancellationToken token, params String[]? sections)
        {
            return GetValueAsync(key, alternate, converter, sections, token);
        }

        public async Task<T> GetValueAsync<T>(String? key, T alternate, TryConverter<String, T>? converter, IEnumerable<String>? sections, CancellationToken token)
        {
            String? value = await GetValueAsync(key, sections, token);

            if (value is null)
            {
                return alternate;
            }

            converter ??= TryConvert;
            return converter(value, out T? result) ? result : alternate;
        }

        public Boolean SetValue(String? key, String? value, params String[]? sections)
        {
            return Config.SetValue(key, value, sections);
        }

        public Boolean SetValue(String? key, String? value, IEnumerable<String>? sections)
        {
            return Config.SetValue(key, value, sections);
        }

        public Task<Boolean> SetValueAsync(String? key, String? value, params String[]? sections)
        {
            return Config.SetValueAsync(key, value, sections);
        }

        public Task<Boolean> SetValueAsync(String? key, String? value, IEnumerable<String>? sections)
        {
            return Config.SetValueAsync(key, value, sections);
        }

        public Task<Boolean> SetValueAsync(String? key, String? value, CancellationToken token, params String[]? sections)
        {
            return Config.SetValueAsync(key, value, token, sections);
        }

        public Task<Boolean> SetValueAsync(String? key, String? value, IEnumerable<String>? sections, CancellationToken token)
        {
            return Config.SetValueAsync(key, value, sections, token);
        }
        
        public Boolean SetValue<T>(String? key, T value, params String[]? sections)
        {
            return SetValue(key, value, (IEnumerable<String>?) sections);
        }

        public Boolean SetValue<T>(String? key, T value, IEnumerable<String>? sections)
        {
            if (value is null)
            {
                return SetValue(key, null, sections);
            }
            
            String? convert = value.GetString();
            return convert is not null && SetValue(key, convert, sections);
        }

        public Task<Boolean> SetValueAsync<T>(String? key, T value, params String[]? sections)
        {
            return SetValueAsync(key, value, (IEnumerable<String>?) sections);
        }

        public Task<Boolean> SetValueAsync<T>(String? key, T value, IEnumerable<String>? sections)
        {
            return SetValueAsync(key, value, sections, CancellationToken.None);
        }

        public Task<Boolean> SetValueAsync<T>(String? key, T value, CancellationToken token, params String[]? sections)
        {
            return SetValueAsync(key, value, sections, token);
        }

        public async Task<Boolean> SetValueAsync<T>(String? key, T value, IEnumerable<String>? sections, CancellationToken token)
        {
            if (value is null)
            {
                return await SetValueAsync(key, null, sections, token);
            }
            
            String? convert = value.GetString();
            return convert is not null && await SetValueAsync(key, convert, sections, token);
        }

        public String? GetOrSetValue(String? key, String? value, params String[]? sections)
        {
            return Config.GetOrSetValue(key, value, sections);
        }

        public String? GetOrSetValue(String? key, String? value, IEnumerable<String>? sections)
        {
            return Config.GetOrSetValue(key, value, sections);
        }

        public Task<String?> GetOrSetValueAsync(String? key, String? value, params String[]? sections)
        {
            return Config.GetOrSetValueAsync(key, value, sections);
        }

        public Task<String?> GetOrSetValueAsync(String? key, String? value, IEnumerable<String>? sections)
        {
            return Config.GetOrSetValueAsync(key, value, sections);
        }

        public Task<String?> GetOrSetValueAsync(String? key, String? value, CancellationToken token, params String[]? sections)
        {
            return Config.GetOrSetValueAsync(key, value, token, sections);
        }

        public Task<String?> GetOrSetValueAsync(String? key, String? value, IEnumerable<String>? sections, CancellationToken token)
        {
            return Config.GetOrSetValueAsync(key, value, sections, token);
        }
        
        public T? GetOrSetValue<T>(String? key, T value, params String[]? sections)
        {
            return GetOrSetValue(key, value, (IEnumerable<String>?) sections);
        }

        public T? GetOrSetValue<T>(String? key, T value, IEnumerable<String>? sections)
        {
            return GetOrSetValue(key, value, TryConvert, sections);
        }

        public T? GetOrSetValue<T>(String? key, T value, TryConverter<String, T>? converter, params String[]? sections)
        {
            return GetOrSetValue(key, value, TryConvert, (IEnumerable<String>?) sections);
        }

        public T? GetOrSetValue<T>(String? key, T value, TryConverter<String, T>? converter, IEnumerable<String>? sections)
        {
            String? convert = value?.GetString();
            String? get = GetOrSetValue(key, convert, sections);

            if (get is null)
            {
                return default;
            }
            
            converter ??= TryConvert;
            return converter(get, out T? result) ? result : default;
        }

        public Task<T?> GetOrSetValueAsync<T>(String? key, T value, params String[]? sections)
        {
            return GetOrSetValueAsync(key, value, (IEnumerable<String>?) sections);
        }

        public Task<T?> GetOrSetValueAsync<T>(String? key, T value, IEnumerable<String>? sections)
        {
            return GetOrSetValueAsync(key, value, sections, CancellationToken.None);
        }

        public Task<T?> GetOrSetValueAsync<T>(String? key, T value, CancellationToken token, params String[]? sections)
        {
            return GetOrSetValueAsync(key, value, sections, token);
        }

        public Task<T?> GetOrSetValueAsync<T>(String? key, T value, IEnumerable<String>? sections, CancellationToken token)
        {
            return GetOrSetValueAsync(key, value, TryConvert, sections, token);
        }

        public Task<T?> GetOrSetValueAsync<T>(String? key, T value, TryConverter<String, T>? converter, params String[]? sections)
        {
            return GetOrSetValueAsync(key, value, converter, (IEnumerable<String>?) sections);
        }

        public Task<T?> GetOrSetValueAsync<T>(String? key, T value, TryConverter<String, T>? converter, IEnumerable<String>? sections)
        {
            return GetOrSetValueAsync(key, value, converter, sections, CancellationToken.None);
        }

        public Task<T?> GetOrSetValueAsync<T>(String? key, T value, TryConverter<String, T>? converter, CancellationToken token, params String[]? sections)
        {
            return GetOrSetValueAsync(key, value, converter, sections, token);
        }

        public async Task<T?> GetOrSetValueAsync<T>(String? key, T value, TryConverter<String, T>? converter, IEnumerable<String>? sections, CancellationToken token)
        {
            String? convert = value?.GetString();
            String? get = await GetOrSetValueAsync(key, convert, sections, token);

            if (get is null)
            {
                return default;
            }
            
            converter ??= TryConvert;
            return converter(get, out T? result) ? result : default;
        }

        public Boolean RemoveValue(String? key, params String[]? sections)
        {
            return Config.RemoveValue(key, sections);
        }

        public Boolean RemoveValue(String? key, IEnumerable<String>? sections)
        {
            return Config.RemoveValue(key, sections);
        }

        public Task<Boolean> RemoveValueAsync(String? key, params String[]? sections)
        {
            return Config.RemoveValueAsync(key, sections);
        }

        public Task<Boolean> RemoveValueAsync(String? key, IEnumerable<String>? sections)
        {
            return Config.RemoveValueAsync(key, sections);
        }

        public Task<Boolean> RemoveValueAsync(String? key, CancellationToken token, params String[]? sections)
        {
            return Config.RemoveValueAsync(key, token, sections);
        }

        public Task<Boolean> RemoveValueAsync(String? key, IEnumerable<String>? sections, CancellationToken token)
        {
            return Config.RemoveValueAsync(key, sections, token);
        }

        public Boolean KeyExist(String? key, params String[]? sections)
        {
            return Config.KeyExist(key, sections);
        }

        public Boolean KeyExist(String? key, IEnumerable<String>? sections)
        {
            return Config.KeyExist(key, sections);
        }

        public Task<Boolean> KeyExistAsync(String? key, params String[]? sections)
        {
            return Config.KeyExistAsync(key, sections);
        }

        public Task<Boolean> KeyExistAsync(String? key, IEnumerable<String>? sections)
        {
            return Config.KeyExistAsync(key, sections);
        }

        public Task<Boolean> KeyExistAsync(String? key, CancellationToken token, params String[]? sections)
        {
            return Config.KeyExistAsync(key, token, sections);
        }

        public Task<Boolean> KeyExistAsync(String? key, IEnumerable<String>? sections, CancellationToken token)
        {
            return Config.KeyExistAsync(key, sections, token);
        }

        public ConfigurationEntry[]? GetExists()
        {
            return Config.GetExists();
        }

        public Task<ConfigurationEntry[]?> GetExistsAsync()
        {
            return Config.GetExistsAsync();
        }

        public Task<ConfigurationEntry[]?> GetExistsAsync(CancellationToken token)
        {
            return Config.GetExistsAsync(token);
        }

        public Boolean Reload()
        {
            return Config.Reload();
        }

        public Task<Boolean> ReloadAsync()
        {
            return Config.ReloadAsync();
        }

        public Task<Boolean> ReloadAsync(CancellationToken token)
        {
            return Config.ReloadAsync(token);
        }

        public override String? ToString()
        {
            return Config.ToString();
        }

        public void Dispose()
        {
            Config.Dispose();
            GC.SuppressFinalize(this);
        }
    }
}