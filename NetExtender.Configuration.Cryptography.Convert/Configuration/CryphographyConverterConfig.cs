// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Threading;
using System.Threading.Tasks;
using NetExtender.Configuration.Behavior.Interfaces;
using NetExtender.Configuration.Common;
using NetExtender.Configuration.Convert.Interfaces;
using NetExtender.Configuration.Cryptography.Behavior.Interfaces;
using NetExtender.Configuration.Cryptography.Common;
using NetExtender.Configuration.Cryptography.Interfaces;
using NetExtender.Configuration.Interfaces;
using NetExtender.Crypto.CryptKey.Interfaces;
using NetExtender.Utilities.Types;

namespace NetExtender.Configuration.Cryptography.Convert
{
    public class CryphographyConverterConfig : ICryptographyConverterConfig, IReadOnlyCryptographyConverterConfig
    {
        private ICryptographyConfig Config { get; }
        
        public event EventHandler<ConfigurationEntry>? Changed
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
        
        public IStringCryptor Cryptor
        {
            get
            {
                return Config.Cryptor;
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
        
        public Boolean IsCryptDefault
        {
            get
            {
                return Config.IsCryptDefault;
            }
        }

        public Boolean IsCryptKey
        {
            get
            {
                return Config.IsCryptKey;
            }
        }

        public Boolean IsCryptValue
        {
            get
            {
                return Config.IsCryptValue;
            }
        }

        public Boolean IsCryptSections
        {
            get
            {
                return Config.IsCryptSections;
            }
        }
        
        public Boolean IsCryptConfig
        {
            get
            {
                return Config.IsCryptConfig;
            }
        }
        
        public Boolean IsCryptAll
        {
            get
            {
                return Config.IsCryptAll;
            }
        }

        public CryptographyConfigOptions CryptOptions
        {
            get
            {
                return Config.CryptOptions;
            }
        }
        
        public CryphographyConverterConfig(ICryptographyConfigBehavior behavior)
        {
            if (behavior is null)
            {
                throw new ArgumentNullException(nameof(behavior));
            }

            Config = new CryptographyConfig(behavior);
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
        
        public String? GetValue(String? key, IStringCryptor? cryptor, params String[]? sections)
        {
            return Config.GetValue(key, cryptor, sections);
        }

        public String? GetValue(String? key, IStringCryptor? cryptor, IEnumerable<String>? sections)
        {
            return Config.GetValue(key, cryptor, sections);
        }

        public String? GetValue(String? key, String? alternate, IStringCryptor? cryptor, params String[]? sections)
        {
            return Config.GetValue(key, alternate, cryptor, sections);
        }

        public String? GetValue(String? key, String? alternate, IStringCryptor? cryptor, IEnumerable<String>? sections)
        {
            return Config.GetValue(key, alternate, cryptor, sections);
        }
        
        public String? GetRawValue(String? key, params String[]? sections)
        {
            return Config.GetRawValue(key, sections);
        }

        public String? GetRawValue(String? key, IEnumerable<String>? sections)
        {
            return Config.GetRawValue(key, sections);
        }

        public String? GetRawValue(String? key, String? alternate, params String[]? sections)
        {
            return Config.GetRawValue(key, alternate, sections);
        }

        public String? GetRawValue(String? key, String? alternate, IEnumerable<String>? sections)
        {
            return Config.GetRawValue(key, alternate, sections);
        }
        
        public T? GetValue<T>(String? key, params String[]? sections)
        {
            return GetValue<T>(key, (IEnumerable<String>?) sections);
        }

        public T? GetValue<T>(String? key, IEnumerable<String>? sections)
        {
            return GetValue(key, default(T), sections);
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
        
        public T? GetValue<T>(String? key, IStringCryptor? cryptor, params String[]? sections)
        {
            return GetValue<T>(key, cryptor, (IEnumerable<String>?) sections);
        }

        public T? GetValue<T>(String? key, IStringCryptor? cryptor, IEnumerable<String>? sections)
        {
            return GetValue<T>(key, cryptor, TryConvert, sections);
        }

        public T? GetValue<T>(String? key, IStringCryptor? cryptor, TryConverter<String, T>? converter, params String[]? sections)
        {
            return GetValue(key, cryptor, converter, (IEnumerable<String>?) sections);
        }

        public T? GetValue<T>(String? key, IStringCryptor? cryptor, TryConverter<String, T>? converter, IEnumerable<String>? sections)
        {
            return GetValue(key, default, cryptor, converter!, sections);
        }
        
        public T GetValue<T>(String? key, T alternate, IStringCryptor? cryptor, params String[]? sections)
        {
            return GetValue(key, alternate, cryptor, (IEnumerable<String>?) sections);
        }

        public T GetValue<T>(String? key, T alternate, IStringCryptor? cryptor, IEnumerable<String>? sections)
        {
            return GetValue(key, alternate, cryptor, TryConvert, sections);
        }

        public T GetValue<T>(String? key, T alternate, IStringCryptor? cryptor, TryConverter<String, T>? converter, params String[]? sections)
        {
            return GetValue(key, alternate, cryptor, converter, (IEnumerable<String>?) sections);
        }

        public T GetValue<T>(String? key, T alternate, IStringCryptor? cryptor, TryConverter<String, T>? converter, IEnumerable<String>? sections)
        {
            String? value = GetValue(key, cryptor, sections);

            if (value is null)
            {
                return alternate;
            }

            converter ??= TryConvert;
            return converter(value, out T? result) ? result : alternate;
        }
        
        public T? GetRawValue<T>(String? key, params String[]? sections)
        {
            return GetRawValue<T>(key, (IEnumerable<String>?) sections);
        }

        public T? GetRawValue<T>(String? key, IEnumerable<String>? sections)
        {
            return GetRawValue<T?>(key, default, sections);
        }

        public T GetRawValue<T>(String? key, T alternate, params String[]? sections)
        {
            return GetRawValue(key, alternate, (IEnumerable<String>?) sections);
        }

        public T GetRawValue<T>(String? key, T alternate, IEnumerable<String>? sections)
        {
            return GetRawValue(key, alternate, TryConvert, sections);
        }

        public T GetRawValue<T>(String? key, T alternate, TryConverter<String, T>? converter, params String[]? sections)
        {
            return GetRawValue(key, alternate, converter, (IEnumerable<String>?) sections);
        }

        public T GetRawValue<T>(String? key, T alternate, TryConverter<String, T>? converter, IEnumerable<String>? sections)
        {
            String? value = GetRawValue(key, sections);

            if (value is null)
            {
                return alternate;
            }

            converter ??= TryConvert;
            return converter(value, out T? result) ? result : alternate;
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
        
        public Task<String?> GetValueAsync(String? key, IStringCryptor? cryptor, params String[]? sections)
        {
            return Config.GetValueAsync(key, cryptor, sections);
        }

        public Task<String?> GetValueAsync(String? key, IStringCryptor? cryptor, IEnumerable<String>? sections)
        {
            return Config.GetValueAsync(key, cryptor, sections);
        }

        public Task<String?> GetValueAsync(String? key, IStringCryptor? cryptor, CancellationToken token, params String[]? sections)
        {
            return Config.GetValueAsync(key, cryptor, token, sections);
        }

        public Task<String?> GetValueAsync(String? key, IStringCryptor? cryptor, IEnumerable<String>? sections, CancellationToken token)
        {
            return Config.GetValueAsync(key, cryptor, sections, token);
        }

        public Task<String?> GetValueAsync(String? key, String? alternate, IStringCryptor? cryptor, params String[]? sections)
        {
            return Config.GetValueAsync(key, alternate, cryptor, sections);
        }

        public Task<String?> GetValueAsync(String? key, String? alternate, IStringCryptor? cryptor, IEnumerable<String>? sections)
        {
            return Config.GetValueAsync(key, alternate, cryptor, sections);
        }

        public Task<String?> GetValueAsync(String? key, String? alternate, IStringCryptor? cryptor, CancellationToken token, params String[]? sections)
        {
            return Config.GetValueAsync(key, alternate, cryptor, token, sections);
        }

        public Task<String?> GetValueAsync(String? key, String? alternate, IStringCryptor? cryptor, IEnumerable<String>? sections, CancellationToken token)
        {
            return Config.GetValueAsync(key, alternate, cryptor, sections, token);
        }

        public Task<String?> GetRawValueAsync(String? key, params String[]? sections)
        {
            return Config.GetRawValueAsync(key, sections);
        }

        public Task<String?> GetRawValueAsync(String? key, IEnumerable<String>? sections)
        {
            return Config.GetRawValueAsync(key, sections);
        }

        public Task<String?> GetRawValueAsync(String? key, CancellationToken token, params String[]? sections)
        {
            return Config.GetRawValueAsync(key, token, sections);
        }

        public Task<String?> GetRawValueAsync(String? key, IEnumerable<String>? sections, CancellationToken token)
        {
            return Config.GetRawValueAsync(key, sections, token);
        }

        public Task<String?> GetRawValueAsync(String? key, String? alternate, params String[]? sections)
        {
            return Config.GetRawValueAsync(key, alternate, sections);
        }

        public Task<String?> GetRawValueAsync(String? key, String? alternate, IEnumerable<String>? sections)
        {
            return Config.GetRawValueAsync(key, alternate, sections);
        }

        public Task<String?> GetRawValueAsync(String? key, String? alternate, CancellationToken token, params String[]? sections)
        {
            return Config.GetRawValueAsync(key, alternate, token, sections);
        }

        public Task<String?> GetRawValueAsync(String? key, String? alternate, IEnumerable<String>? sections, CancellationToken token)
        {
            return Config.GetRawValueAsync(key, alternate, sections, token);
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
            return GetValueAsync<T?>(key, default, sections, token);
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
        
        public Task<String?> GetValueAsync(String? key, IStringCryptor? cryptor, params String[]? sections)
        {
            return Config.GetValueAsync(key, cryptor, sections);
        }

        public Task<String?> GetValueAsync(String? key, IStringCryptor? cryptor, IEnumerable<String>? sections)
        {
            return Config.GetValueAsync(key, cryptor, sections);
        }

        public Task<String?> GetValueAsync(String? key, IStringCryptor? cryptor, CancellationToken token, params String[]? sections)
        {
            return Config.GetValueAsync(key, cryptor, token, sections);
        }

        public Task<String?> GetValueAsync(String? key, IStringCryptor? cryptor, IEnumerable<String>? sections, CancellationToken token)
        {
            return Config.GetValueAsync(key, cryptor, sections, token);
        }

        public Task<String?> GetValueAsync(String? key, String? alternate, IStringCryptor? cryptor, params String[]? sections)
        {
            return Config.GetValueAsync(key, alternate, cryptor, sections);
        }

        public Task<String?> GetValueAsync(String? key, String? alternate, IStringCryptor? cryptor, IEnumerable<String>? sections)
        {
            return Config.GetValueAsync(key, alternate, cryptor, sections);
        }

        public Task<String?> GetValueAsync(String? key, String? alternate, IStringCryptor? cryptor, CancellationToken token, params String[]? sections)
        {
            return Config.GetValueAsync(key, alternate, cryptor, token, sections);
        }

        public Task<String?> GetValueAsync(String? key, String? alternate, IStringCryptor? cryptor, IEnumerable<String>? sections, CancellationToken token)
        {
            return Config.GetValueAsync(key, alternate, cryptor, sections, token);
        }

        public Task<T?> GetRawValueAsync<T>(String? key, params String[]? sections)
        {
            return GetRawValueAsync<T>(key, (IEnumerable<String>?) sections);
        }

        public Task<T?> GetRawValueAsync<T>(String? key, IEnumerable<String>? sections)
        {
            return GetRawValueAsync<T>(key, sections, CancellationToken.None);
        }

        public Task<T?> GetRawValueAsync<T>(String? key, CancellationToken token, params String[]? sections)
        {
            return GetRawValueAsync<T>(key, sections, token);
        }

        public Task<T?> GetRawValueAsync<T>(String? key, IEnumerable<String>? sections, CancellationToken token)
        {
            return GetRawValueAsync<T?>(key, default, sections, token);
        }

        public Task<T> GetRawValueAsync<T>(String? key, T alternate, params String[]? sections)
        {
            return GetRawValueAsync(key, alternate, (IEnumerable<String>?) sections);
        }

        public Task<T> GetRawValueAsync<T>(String? key, T alternate, IEnumerable<String>? sections)
        {
            return GetRawValueAsync(key, alternate, sections, CancellationToken.None);
        }

        public Task<T> GetRawValueAsync<T>(String? key, T alternate, CancellationToken token, params String[]? sections)
        {
            return GetRawValueAsync(key, alternate, sections, token);
        }

        public Task<T> GetRawValueAsync<T>(String? key, T alternate, IEnumerable<String>? sections, CancellationToken token)
        {
            return GetRawValueAsync(key, alternate, TryConvert, sections, token);
        }

        public Task<T> GetRawValueAsync<T>(String? key, T alternate, TryConverter<String, T>? converter, params String[]? sections)
        {
            return GetRawValueAsync(key, alternate, converter, (IEnumerable<String>?) sections);
        }

        public Task<T> GetRawValueAsync<T>(String? key, T alternate, TryConverter<String, T>? converter, IEnumerable<String>? sections)
        {
            return GetRawValueAsync(key, alternate, converter, sections, CancellationToken.None);
        }

        public Task<T> GetRawValueAsync<T>(String? key, T alternate, TryConverter<String, T>? converter, CancellationToken token, params String[]? sections)
        {
            return GetRawValueAsync(key, alternate, converter, sections, token);
        }

        public async Task<T> GetRawValueAsync<T>(String? key, T alternate, TryConverter<String, T>? converter, IEnumerable<String>? sections, CancellationToken token)
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

        public Boolean SetValue(String? key, String? value, IStringCryptor? cryptor, params String[]? sections)
        {
            return Config.SetValue(key, value, cryptor, sections);
        }

        public Boolean SetValue(String? key, String? value, IStringCryptor? cryptor, IEnumerable<String>? sections)
        {
            return Config.SetValue(key, value, cryptor, sections);
        }

        public Boolean SetRawValue(String? key, String? value, params String[]? sections)
        {
            return Config.SetRawValue(key, value, sections);
        }

        public Boolean SetRawValue(String? key, String? value, IEnumerable<String>? sections)
        {
            return Config.SetRawValue(key, value, sections);
        }

        public Task<Boolean> SetValueAsync(String? key, String? value, IStringCryptor? cryptor, params String[]? sections)
        {
            return Config.SetValueAsync(key, value, cryptor, sections);
        }

        public Task<Boolean> SetValueAsync(String? key, String? value, IStringCryptor? cryptor, IEnumerable<String>? sections)
        {
            return Config.SetValueAsync(key, value, cryptor, sections);
        }

        public Task<Boolean> SetValueAsync(String? key, String? value, IStringCryptor? cryptor, CancellationToken token, params String[]? sections)
        {
            return Config.SetValueAsync(key, value, cryptor, token, sections);
        }

        public Task<Boolean> SetValueAsync(String? key, String? value, IStringCryptor? cryptor, IEnumerable<String>? sections, CancellationToken token)
        {
            return Config.SetValueAsync(key, value, cryptor, sections, token);
        }

        public Task<Boolean> SetRawValueAsync(String? key, String? value, params String[]? sections)
        {
            return Config.SetRawValueAsync(key, value, sections);
        }

        public Task<Boolean> SetRawValueAsync(String? key, String? value, IEnumerable<String>? sections)
        {
            return Config.SetRawValueAsync(key, value, sections);
        }

        public Task<Boolean> SetRawValueAsync(String? key, String? value, CancellationToken token, params String[]? sections)
        {
            return Config.SetRawValueAsync(key, value, token, sections);
        }

        public Task<Boolean> SetRawValueAsync(String? key, String? value, IEnumerable<String>? sections, CancellationToken token)
        {
            return Config.SetRawValueAsync(key, value, sections, token);
        }

        public String? GetOrSetValue(String? key, String? value, IStringCryptor? cryptor, params String[]? sections)
        {
            return Config.GetOrSetValue(key, value, cryptor, sections);
        }

        public String? GetOrSetValue(String? key, String? value, IStringCryptor? cryptor, IEnumerable<String>? sections)
        {
            return Config.GetOrSetValue(key, value, cryptor, sections);
        }

        public String? GetOrSetRawValue(String? key, String? value, params String[]? sections)
        {
            return Config.GetOrSetRawValue(key, value, sections);
        }

        public String? GetOrSetRawValue(String? key, String? value, IEnumerable<String>? sections)
        {
            return Config.GetOrSetRawValue(key, value, sections);
        }

        public Task<String?> GetOrSetValueAsync(String? key, String? value, IStringCryptor? cryptor, params String[]? sections)
        {
            return Config.GetOrSetValueAsync(key, value, cryptor, sections);
        }

        public Task<String?> GetOrSetValueAsync(String? key, String? value, IStringCryptor? cryptor, IEnumerable<String>? sections)
        {
            return Config.GetOrSetValueAsync(key, value, cryptor, sections);
        }

        public Task<String?> GetOrSetValueAsync(String? key, String? value, IStringCryptor? cryptor, CancellationToken token, params String[]? sections)
        {
            return Config.GetOrSetValueAsync(key, value, cryptor, token, sections);
        }

        public Task<String?> GetOrSetValueAsync(String? key, String? value, IStringCryptor? cryptor, IEnumerable<String>? sections, CancellationToken token)
        {
            return Config.GetOrSetValueAsync(key, value, cryptor, sections, token);
        }

        public Task<String?> GetOrSetRawValueAsync(String? key, String? value, params String[]? sections)
        {
            return Config.GetOrSetRawValueAsync(key, value, sections);
        }

        public Task<String?> GetOrSetRawValueAsync(String? key, String? value, IEnumerable<String>? sections)
        {
            return Config.GetOrSetRawValueAsync(key, value, sections);
        }

        public Task<String?> GetOrSetRawValueAsync(String? key, String? value, CancellationToken token, params String[]? sections)
        {
            return Config.GetOrSetRawValueAsync(key, value, token, sections);
        }

        public Task<String?> GetOrSetRawValueAsync(String? key, String? value, IEnumerable<String>? sections, CancellationToken token)
        {
            return Config.GetOrSetRawValueAsync(key, value, sections, token);
        }

        public Boolean RemoveValue(String? key, IStringCryptor? cryptor, params String[]? sections)
        {
            return Config.RemoveValue(key, cryptor, sections);
        }

        public Boolean RemoveValue(String? key, IStringCryptor? cryptor, IEnumerable<String>? sections)
        {
            return Config.RemoveValue(key, cryptor, sections);
        }

        public Boolean RemoveRawValue(String? key, params String[]? sections)
        {
            return Config.RemoveRawValue(key, sections);
        }

        public Boolean RemoveRawValue(String? key, IEnumerable<String>? sections)
        {
            return Config.RemoveRawValue(key, sections);
        }

        public Task<Boolean> RemoveValueAsync(String? key, IStringCryptor? cryptor, params String[]? sections)
        {
            return Config.RemoveValueAsync(key, cryptor, sections);
        }

        public Task<Boolean> RemoveValueAsync(String? key, IStringCryptor? cryptor, IEnumerable<String>? sections)
        {
            return Config.RemoveValueAsync(key, cryptor, sections);
        }

        public Task<Boolean> RemoveValueAsync(String? key, IStringCryptor? cryptor, CancellationToken token, params String[]? sections)
        {
            return Config.RemoveValueAsync(key, cryptor, token, sections);
        }

        public Task<Boolean> RemoveValueAsync(String? key, IStringCryptor? cryptor, IEnumerable<String>? sections, CancellationToken token)
        {
            return Config.RemoveValueAsync(key, cryptor, sections, token);
        }

        public Task<Boolean> RemoveRawValueAsync(String? key, params String[]? sections)
        {
            return Config.RemoveRawValueAsync(key, sections);
        }

        public Task<Boolean> RemoveRawValueAsync(String? key, IEnumerable<String>? sections)
        {
            return Config.RemoveRawValueAsync(key, sections);
        }

        public Task<Boolean> RemoveRawValueAsync(String? key, CancellationToken token, params String[]? sections)
        {
            return Config.RemoveRawValueAsync(key, token, sections);
        }

        public Task<Boolean> RemoveRawValueAsync(String? key, IEnumerable<String>? sections, CancellationToken token)
        {
            return Config.RemoveRawValueAsync(key, sections, token);
        }

        public Boolean KeyExist(String? key, IStringCryptor? cryptor, params String[]? sections)
        {
            return Config.KeyExist(key, cryptor, sections);
        }

        public Boolean KeyExist(String? key, IStringCryptor? cryptor, IEnumerable<String>? sections)
        {
            return Config.KeyExist(key, cryptor, sections);
        }

        public Boolean KeyExistRaw(String? key, params String[]? sections)
        {
            return Config.KeyExistRaw(key, sections);
        }

        public Boolean KeyExistRaw(String? key, IEnumerable<String>? sections)
        {
            return Config.KeyExistRaw(key, sections);
        }

        public Task<Boolean> KeyExistAsync(String? key, IStringCryptor? cryptor, params String[]? sections)
        {
            return Config.KeyExistAsync(key, cryptor, sections);
        }

        public Task<Boolean> KeyExistAsync(String? key, IStringCryptor? cryptor, IEnumerable<String>? sections)
        {
            return Config.KeyExistAsync(key, cryptor, sections);
        }

        public Task<Boolean> KeyExistAsync(String? key, IStringCryptor? cryptor, CancellationToken token, params String[]? sections)
        {
            return Config.KeyExistAsync(key, cryptor, token, sections);
        }

        public Task<Boolean> KeyExistAsync(String? key, IStringCryptor? cryptor, IEnumerable<String>? sections, CancellationToken token)
        {
            return Config.KeyExistAsync(key, cryptor, sections, token);
        }

        public Task<Boolean> KeyExistRawAsync(String? key, params String[]? sections)
        {
            return Config.KeyExistRawAsync(key, sections);
        }

        public Task<Boolean> KeyExistRawAsync(String? key, IEnumerable<String>? sections)
        {
            return Config.KeyExistRawAsync(key, sections);
        }

        public Task<Boolean> KeyExistRawAsync(String? key, CancellationToken token, params String[]? sections)
        {
            return Config.KeyExistRawAsync(key, token, sections);
        }

        public Task<Boolean> KeyExistRawAsync(String? key, IEnumerable<String>? sections, CancellationToken token)
        {
            return Config.KeyExistRawAsync(key, sections, token);
        }

        public ConfigurationEntry[]? GetExists(IStringCryptor? cryptor)
        {
            return Config.GetExists(cryptor);
        }

        public ConfigurationEntry[]? GetExistsRaw()
        {
            return Config.GetExistsRaw();
        }

        public Task<ConfigurationEntry[]?> GetExistsAsync(IStringCryptor? cryptor)
        {
            return Config.GetExistsAsync(cryptor);
        }

        public Task<ConfigurationEntry[]?> GetExistsAsync(IStringCryptor? cryptor, CancellationToken token)
        {
            return Config.GetExistsAsync(cryptor, token);
        }

        public Task<ConfigurationEntry[]?> GetExistsRawAsync()
        {
            return Config.GetExistsRawAsync();
        }

        public Task<ConfigurationEntry[]?> GetExistsRawAsync(CancellationToken token)
        {
            return Config.GetExistsRawAsync(token);
        }
        
        public void Dispose()
        {
            Config.Dispose();
            GC.SuppressFinalize(this);
        }
    }
}