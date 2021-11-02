// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using NetExtender.Configuration.Behavior.Interfaces;
using NetExtender.Configuration.Common;
using NetExtender.Configuration.Interfaces;
using NetExtender.Crypto;
using NetExtender.Crypto.CryptKey.Interfaces;
using NetExtender.Types.Exceptions;
using NetExtender.Utilities.Types;

namespace NetExtender.Configuration
{
    public class Config : IConfig, IReadOnlyConfig
    {
        public const ConfigOptions DefaultConfigOptions = ConfigOptions.None;
        
        public static IConfig Create(IConfigBehavior behavior)
        {
            if (behavior is null)
            {
                throw new ArgumentNullException(nameof(behavior));
            }

            return new Config(behavior);
        }

        protected IConfigBehavior Behavior { get; }

        public String Path
        {
            get
            {
                return Behavior.Path;
            }
        }

        public ICryptKey Crypt
        {
            get
            {
                return Behavior.Crypt;
            }
        }

        public ConfigOptions Options
        {
            get
            {
                return Behavior.Options;
            }
        }

        public Boolean IsReadOnly
        {
            get
            {
                return Behavior.IsReadOnly;
            }
        }

        public Boolean IsLazyWrite
        {
            get
            {
                return Behavior.IsLazyWrite;
            }
        }
        
        public Boolean IsCryptData
        {
            get
            {
                return Behavior.IsCryptData;
            }
        }
        
        public Boolean IsCryptConfig
        {
            get
            {
                return Behavior.IsCryptConfig;
            }
        }
        
        public Boolean IsCryptAll
        {
            get
            {
                return Behavior.IsCryptAll;
            }
        }

        public Boolean ThrowOnReadOnly
        {
            get
            {
                return Behavior.ThrowOnReadOnly;
            }
        }

        public Boolean CryptByDefault
        {
            get
            {
                return Behavior.CryptByDefault;
            }
        }

        public ConfigPropertyOptions DefaultOptions
        {
            get
            {
                return Behavior.DefaultOptions;
            }
        }

        public Config(IConfigBehavior behavior)
        {
            Behavior = behavior ?? throw new ArgumentNullException(nameof(behavior));
        }

        protected String? ConvertToValue<T>(T value)
        {
            return Behavior.ConvertToValue(value);
        }

        private String? SetValueInternalCrypt<T>(T value, ICryptKey? crypt)
        {
            crypt ??= Crypt;
            
            String? convert = ConvertToValue(value);
            
            if (convert is not null && crypt.IsEncrypt)
            {
                return crypt.Encrypt(convert);
            }

            return convert;
        }
        
        public Boolean SetValue<T>(String? key, T value, params String[]? sections)
        {
            return SetValue(key, value, (IEnumerable<String>?) sections);
        }

        public Boolean SetValue<T>(String? key, T value, IEnumerable<String>? sections)
        {
            return SetInternal(key, ConvertToValue(value), sections);
        }
        
        public Boolean SetValue<T>(String? key, T value, ICryptKey? crypt, params String[]? sections)
        {
            return SetValue(key, value, crypt, (IEnumerable<String>?) sections);
        }
        
        public Boolean SetValue<T>(String? key, T value, ICryptKey? crypt, IEnumerable<String>? sections)
        {
            String? result = SetValueInternalCrypt(value, crypt);
            return SetValue(key, result, sections);
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
        
        public Task<Boolean> SetValueAsync<T>(String? key, T value, IEnumerable<String>? sections, CancellationToken token)
        {
            return SetInternalAsync(key, ConvertToValue(value), sections, token);
        }
        
        public Task<Boolean> SetValueAsync<T>(String? key, T value, ICryptKey? crypt, params String[]? sections)
        {
            return SetValueAsync(key, value, crypt, (IEnumerable<String>?) sections);
        }

        public Task<Boolean> SetValueAsync<T>(String? key, T value, ICryptKey? crypt, IEnumerable<String>? sections)
        {
            return SetValueAsync(key, value, crypt, sections, CancellationToken.None);
        }
        
        public Task<Boolean> SetValueAsync<T>(String? key, T value, ICryptKey? crypt, CancellationToken token, params String[]? sections)
        {
            return SetValueAsync(key, value, crypt, sections, token);
        }
        
        public Task<Boolean> SetValueAsync<T>(String? key, T value, ICryptKey? crypt, IEnumerable<String>? sections, CancellationToken token)
        {
            String? result = SetValueInternalCrypt(value, crypt);
            return SetValueAsync(key, result, sections, token);
        }

        protected T? ConvertFromValue<T>(String? value)
        {
            return Behavior.ConvertFromValue<T>(value);
        }

        private String? GetValueInternalCrypt(String? value, Boolean decrypt, ICryptKey? crypt)
        {
            if (!decrypt || value is null)
            {
                return value;
            }
            
            crypt ??= Crypt;
            return crypt.Decrypt(value) ?? value;
        }
        
        public String? GetValue(String? key, params String[]? sections)
        {
            return GetValue(key, (IEnumerable<String>?) sections);
        }

        public String? GetValue(String? key, IEnumerable<String>? sections)
        {
            return GetInternal(key, sections);
        }
        
        public Task<String?> GetValueAsync(String? key, params String[]? sections)
        {
            return GetValueAsync(key, (IEnumerable<String>?) sections);
        }
        
        public Task<String?> GetValueAsync(String? key, IEnumerable<String>? sections)
        {
            return GetValueAsync(key, sections, CancellationToken.None);
        }
        
        public Task<String?> GetValueAsync(String? key, CancellationToken token, params String[]? sections)
        {
            return GetValueAsync(key, (IEnumerable<String>?) sections, token);
        }
        
        public Task<String?> GetValueAsync(String? key, IEnumerable<String>? sections, CancellationToken token)
        {
            return GetInternalAsync(key, sections, token);
        }
        
        public String? GetValue(String? key, String? alternate, params String[]? sections)
        {
            return GetValue(key, alternate, (IEnumerable<String>?) sections);
        }

        public String? GetValue(String? key, String? alternate, IEnumerable<String>? sections)
        {
            return GetValue(key, sections) ?? alternate;
        }
        
        public Task<String?> GetValueAsync(String? key, String? alternate, params String[]? sections)
        {
            return GetValueAsync(key, alternate, (IEnumerable<String>?) sections);
        }
        
        public Task<String?> GetValueAsync(String? key, String? alternate, IEnumerable<String>? sections)
        {
            return GetValueAsync(key, alternate, sections, CancellationToken.None);
        }
        
        public Task<String?> GetValueAsync(String? key, String? alternate, CancellationToken token, params String[]? sections)
        {
            return GetValueAsync(key, alternate, sections, token);
        }
        
        public async Task<String?> GetValueAsync(String? key, String? alternate, IEnumerable<String>? sections, CancellationToken token)
        {
            return await GetValueAsync(key, sections, token).ConfigureAwait(false) ?? alternate;
        }
        
        public String? GetValue(String? key, String? alternate, Boolean decrypt, params String[]? sections)
        {
            return GetValue(key, alternate, decrypt, (IEnumerable<String>?) sections);
        }

        public String? GetValue(String? key, String? alternate, Boolean decrypt, IEnumerable<String>? sections)
        {
            return GetValue(key, alternate, decrypt, null, sections);
        }
        
        public String? GetValue(String? key, String? alternate, Boolean decrypt, ICryptKey? crypt, params String[]? sections)
        {
            return GetValue(key, alternate, decrypt, crypt, (IEnumerable<String>?) sections);
        }

        public String? GetValue(String? key, String? alternate, Boolean decrypt, ICryptKey? crypt, IEnumerable<String>? sections)
        {
            String? value = GetValue(key, alternate, sections);
            return GetValueInternalCrypt(value, decrypt, crypt);
        }
        
        public Task<String?> GetValueAsync(String? key, String? alternate, Boolean decrypt, params String[]? sections)
        {
            return GetValueAsync(key, alternate, decrypt, (IEnumerable<String>?) sections);
        }
        
        public Task<String?> GetValueAsync(String? key, String? alternate, Boolean decrypt, IEnumerable<String>? sections)
        {
            return GetValueAsync(key, alternate, decrypt, sections, CancellationToken.None);
        }
        
        public Task<String?> GetValueAsync(String? key, String? alternate, Boolean decrypt, CancellationToken token, params String[]? sections)
        {
            return GetValueAsync(key, alternate, decrypt, sections, token);
        }
        
        public Task<String?> GetValueAsync(String? key, String? alternate, Boolean decrypt, IEnumerable<String>? sections, CancellationToken token)
        {
            return GetValueAsync(key, alternate, decrypt, null, sections, token);
        }
        
        public Task<String?> GetValueAsync(String? key, String? alternate, Boolean decrypt, ICryptKey? crypt, params String[]? sections)
        {
            return GetValueAsync(key, alternate, decrypt, crypt, (IEnumerable<String>?) sections);
        }

        public Task<String?> GetValueAsync(String? key, String? alternate, Boolean decrypt, ICryptKey? crypt, IEnumerable<String>? sections)
        {
            return GetValueAsync(key, alternate, decrypt, crypt, sections, CancellationToken.None);
        }

        public Task<String?> GetValueAsync(String? key, String? alternate, Boolean decrypt, ICryptKey? crypt, CancellationToken token, params String[]? sections)
        {
            return GetValueAsync(key, alternate, decrypt, crypt, sections, token);
        }
        
        public async Task<String?> GetValueAsync(String? key, String? alternate, Boolean decrypt, ICryptKey? crypt, IEnumerable<String>? sections, CancellationToken token)
        {
            String? value = await GetValueAsync(key, alternate, sections, token).ConfigureAwait(false);
            return GetValueInternalCrypt(value, decrypt, crypt);
        }
        
        private T GetValueInternalCrypt<T>(String? value, T alternate, ICryptKey? crypt, TryConverter<String?, T>? converter)
        {
            crypt ??= Crypt;
            converter ??= ConvertUtilities.TryConvert;
            
            T? convert;
            if (value is null || !crypt.IsDecrypt)
            {
                return converter(value, out convert) ? convert : alternate;
            }

            return converter(crypt.Decrypt(value) ?? value, out convert) ? convert : alternate;
        }
        
        public T? GetValue<T>(String? key, params String[]? sections)
        {
            return GetValue<T>(key, (IEnumerable<String>?) sections);
        }

        public T? GetValue<T>(String? key, IEnumerable<String>? sections)
        {
            return ConvertFromValue<T>(GetValue(key, sections));
        }
        
        public Task<T?> GetValueAsync<T>(String key, params String[]? sections)
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
        
        public async Task<T?> GetValueAsync<T>(String? key, IEnumerable<String>? sections, CancellationToken token)
        {
            return ConvertFromValue<T>(await GetValueAsync(key, sections, token).ConfigureAwait(false));
        }
        
        public T GetValue<T>(String? key, T alternate, params String[]? sections)
        {
            return GetValue(key, alternate, (IEnumerable<String>?) sections);
        }

        public T GetValue<T>(String? key, T alternate, IEnumerable<String>? sections)
        {
            return GetValue(key, alternate, (ICryptKey?) null, sections);
        }
        
        public T GetValue<T>(String? key, T alternate, ICryptKey? crypt, params String[]? sections)
        {
            return GetValue(key, alternate, crypt, (IEnumerable<String>?) sections);
        }

        public T GetValue<T>(String? key, T alternate, ICryptKey? crypt, IEnumerable<String>? sections)
        {
            return GetValue(key, alternate, crypt, ConvertUtilities.TryConvert, sections);
        }
        
        public T GetValue<T>(String? key, T alternate, TryConverter<String?, T>? converter, params String[]? sections)
        {
            return GetValue(key, alternate, converter, (IEnumerable<String>?) sections);
        }

        public T GetValue<T>(String? key, T alternate, TryConverter<String?, T>? converter, IEnumerable<String>? sections)
        {
            return GetValue(key, alternate, null, converter, sections);
        }
        
        public T GetValue<T>(String? key, T alternate, ICryptKey? crypt, TryConverter<String?, T>? converter, params String[]? sections)
        {
            return GetValue(key, alternate, crypt, converter, (IEnumerable<String>?) sections);
        }

        public T GetValue<T>(String? key, T alternate, ICryptKey? crypt, TryConverter<String?, T>? converter, IEnumerable<String>? sections)
        {
            String? value = GetValue(key, alternate.GetString(CultureInfo.InvariantCulture), sections);
            return GetValueInternalCrypt(value, alternate, crypt, converter);
        }
        
        public Task<T?> GetValueAsync<T>(String? key, T alternate, params String[]? sections)
        {
            return GetValueAsync(key, alternate, (IEnumerable<String>?) sections);
        }
        
        public Task<T?> GetValueAsync<T>(String? key, T alternate, IEnumerable<String>? sections)
        {
            return GetValueAsync(key, alternate, sections, CancellationToken.None);
        }
        
        public Task<T?> GetValueAsync<T>(String? key, T alternate, CancellationToken token, params String[]? sections)
        {
            return GetValueAsync(key, alternate, sections, token);
        }
        
        public Task<T?> GetValueAsync<T>(String? key, T alternate, IEnumerable<String>? sections, CancellationToken token)
        {
            return GetValueAsync(key, alternate, (ICryptKey?) null, sections, token);
        }
        
        public Task<T?> GetValueAsync<T>(String? key, T alternate, ICryptKey? crypt, params String[]? sections)
        {
            return GetValueAsync(key, alternate, crypt, (IEnumerable<String>?) sections);
        }
        
        public Task<T?> GetValueAsync<T>(String? key, T alternate, ICryptKey? crypt, IEnumerable<String>? sections)
        {
            return GetValueAsync(key, alternate, crypt, sections, CancellationToken.None);
        }
        
        public Task<T?> GetValueAsync<T>(String? key, T alternate, ICryptKey? crypt, CancellationToken token, params String[]? sections)
        {
            return GetValueAsync(key, alternate, crypt, sections, token);
        }
        
        public Task<T?> GetValueAsync<T>(String? key, T alternate, ICryptKey? crypt, IEnumerable<String>? sections, CancellationToken token)
        {
            return GetValueAsync(key, alternate, crypt, ConvertUtilities.TryConvert, sections, token);
        }
        
        public Task<T?> GetValueAsync<T>(String? key, T alternate, TryConverter<String?, T>? converter, params String[]? sections)
        {
            return GetValueAsync(key, alternate, converter, (IEnumerable<String>?) sections);
        }
        
        public Task<T?> GetValueAsync<T>(String? key, T alternate, TryConverter<String?, T>? converter, IEnumerable<String>? sections)
        {
            return GetValueAsync(key, alternate, converter, sections, CancellationToken.None);
        }
        
        public Task<T?> GetValueAsync<T>(String? key, T alternate, TryConverter<String?, T>? converter, CancellationToken token, params String[]? sections)
        {
            return GetValueAsync(key, alternate, converter, sections, token);
        }
        
        public Task<T?> GetValueAsync<T>(String? key, T alternate, TryConverter<String?, T>? converter, IEnumerable<String>? sections, CancellationToken token)
        {
            return GetValueAsync(key, alternate, null, converter, sections, token);
        }

        public Task<T?> GetValueAsync<T>(String? key, T alternate, ICryptKey? crypt, TryConverter<String?, T>? converter, params String[]? sections)
        {
            return GetValueAsync(key, alternate, crypt, converter, (IEnumerable<String>?) sections);
        }
        
        public Task<T?> GetValueAsync<T>(String? key, T alternate, ICryptKey? crypt, TryConverter<String?, T>? converter, IEnumerable<String>? sections)
        {
            return GetValueAsync(key, alternate, crypt, converter, sections, CancellationToken.None);
        }
        
        public Task<T?> GetValueAsync<T>(String? key, T alternate, ICryptKey? crypt, TryConverter<String?, T>? converter, CancellationToken token, params String[]? sections)
        {
            return GetValueAsync(key, alternate, crypt, converter, sections, token);
        }
        
        public async Task<T?> GetValueAsync<T>(String? key, T alternate, ICryptKey? crypt, TryConverter<String?, T>? converter, IEnumerable<String>? sections, CancellationToken token)
        {
            String? value = await GetValueAsync(key, alternate.GetString(CultureInfo.InvariantCulture), sections, token).ConfigureAwait(false);
            return GetValueInternalCrypt(value, alternate, crypt, converter);
        }

        public Boolean GetOrSetValueInternalCrypt(String? value, CryptAction crypt, ICryptKey? cryptkey, out String? result)
        {
            if (value is null)
            {
                result = default;
                return false;
            }
            
            cryptkey ??= Crypt;

            result = crypt.HasFlag(CryptAction.Decrypt) ? cryptkey.Decrypt(value) ?? value : value;
            return true;
        }
        
        public String? GetOrSetValue(String? key, String? value, params String[]? sections)
        {
            return GetOrSetValue(key, value, (IEnumerable<String>?) sections);
        }

        public String? GetOrSetValue(String? key, String? value, IEnumerable<String>? sections)
        {
            return GetOrSetValue(key, value, CryptAction.Decrypt, sections);
        }
        
        public String? GetOrSetValue(String? key, String? value, CryptAction crypt, params String[]? sections)
        {
            return GetOrSetValue(key, value, crypt, (IEnumerable<String>?) sections);
        }

        public String? GetOrSetValue(String? key, String? value, CryptAction crypt, IEnumerable<String>? sections)
        {
            return GetOrSetValue(key, value, crypt, null, sections);
        }
        
        public String? GetOrSetValue(String? key, String? value, CryptAction crypt, ICryptKey? cryptkey, params String[]? sections)
        {
            return GetOrSetValue(key, value, crypt, cryptkey, (IEnumerable<String>?) sections);
        }

        public String? GetOrSetValue(String? key, String? value, CryptAction crypt, ICryptKey? cryptkey, IEnumerable<String>? sections)
        {
            sections = sections.Materialize();
            String? current = GetValue(key, sections);

            if (GetOrSetValueInternalCrypt(current, crypt, cryptkey, out String? result))
            {
                return result;
            }

            SetValue(key, value, cryptkey, sections);
            return value;
        }
        
        public Task<String?> GetOrSetValueAsync(String? key, String? value, params String[]? sections)
        {
            return GetOrSetValueAsync(key, value, (IEnumerable<String>?) sections);
        }
        
        public Task<String?> GetOrSetValueAsync(String? key, String? value, IEnumerable<String>? sections)
        {
            return GetOrSetValueAsync(key, value, sections, CancellationToken.None);
        }
        
        public Task<String?> GetOrSetValueAsync(String? key, String? value, CancellationToken token, params String[]? sections)
        {
            return GetOrSetValueAsync(key, value, sections, token);
        }
        
        public Task<String?> GetOrSetValueAsync(String? key, String? value, IEnumerable<String>? sections, CancellationToken token)
        {
            return GetOrSetValueAsync(key, value, CryptAction.Decrypt, sections, token);
        }
        
        public Task<String?> GetOrSetValueAsync(String? key, String? value, CryptAction crypt, params String[]? sections)
        {
            return GetOrSetValueAsync(key, value, crypt, (IEnumerable<String>?) sections);
        }
        
        public Task<String?> GetOrSetValueAsync(String? key, String? value, CryptAction crypt, IEnumerable<String>? sections)
        {
            return GetOrSetValueAsync(key, value, crypt, sections, CancellationToken.None);
        }
        
        public Task<String?> GetOrSetValueAsync(String? key, String? value, CryptAction crypt, CancellationToken token, params String[]? sections)
        {
            return GetOrSetValueAsync(key, value, crypt, sections, token);
        }
        
        public Task<String?> GetOrSetValueAsync(String? key, String? value, CryptAction crypt, IEnumerable<String>? sections, CancellationToken token)
        {
            return GetOrSetValueAsync(key, value, crypt, null, sections, token);
        }
        
        public Task<String?> GetOrSetValueAsync(String? key, String? value, CryptAction crypt, ICryptKey? cryptkey, params String[]? sections)
        {
            return GetOrSetValueAsync(key, value, crypt, cryptkey, (IEnumerable<String>?) sections);
        }

        public Task<String?> GetOrSetValueAsync(String? key, String? value, CryptAction crypt, ICryptKey? cryptkey, IEnumerable<String>? sections)
        {
            return GetOrSetValueAsync(key, value, crypt, cryptkey, sections, CancellationToken.None);
        }
        
        public Task<String?> GetOrSetValueAsync(String? key, String? value, CryptAction crypt, ICryptKey? cryptkey, CancellationToken token, params String[]? sections)
        {
            return GetOrSetValueAsync(key, value, crypt, cryptkey, sections, token);
        }
        
        public async Task<String?> GetOrSetValueAsync(String? key, String? value, CryptAction crypt, ICryptKey? cryptkey, IEnumerable<String>? sections, CancellationToken token)
        {
            sections = sections.Materialize();
            String? current = await GetValueAsync(key, sections, token).ConfigureAwait(false);

            if (GetOrSetValueInternalCrypt(current, crypt, cryptkey, out String? result))
            {
                return result;
            }

            await SetValueAsync(key, value, cryptkey, sections, token).ConfigureAwait(false);
            return value;
        }
        
        public Boolean GetOrSetValueInternalCrypt<T>(String? value, T alternate, ICryptKey? crypt, TryConverter<String?, T>? converter, out T? result)
        {
            if (value is null)
            {
                result = default;
                return false;
            }
            
            crypt ??= Crypt;

            if (crypt.IsDecrypt)
            {
                value = crypt.Decrypt(value) ?? value;
            }
            
            converter ??= ConvertUtilities.TryConvert;

            result = converter(value, out T? convert) ? convert : alternate;
            return true;
        }
        
        public T? GetOrSetValue<T>(String? key, T value, params String[]? sections)
        {
            return GetOrSetValue(key, value, (IEnumerable<String>?) sections);
        }

        public T? GetOrSetValue<T>(String? key, T value, IEnumerable<String>? sections)
        {
            return GetOrSetValue(key, value, (ICryptKey?) null, sections);
        }
        
        public T? GetOrSetValue<T>(String? key, T value, ICryptKey? crypt, params String[]? sections)
        {
            return GetOrSetValue(key, value, crypt, (IEnumerable<String>?) sections);
        }
        
        public T? GetOrSetValue<T>(String? key, T value, ICryptKey? crypt, IEnumerable<String>? sections)
        {
            return GetOrSetValue(key, value, crypt, ConvertUtilities.TryConvert, sections);
        }
        
        public T? GetOrSetValue<T>(String? key, T value, TryConverter<String?, T>? converter, params String[]? sections)
        {
            return GetOrSetValue(key, value, converter, (IEnumerable<String>?) sections);
        }

        public T? GetOrSetValue<T>(String? key, T value, TryConverter<String?, T>? converter, IEnumerable<String>? sections)
        {
            return GetOrSetValue(key, value, null, converter, sections);
        }
        
        public T? GetOrSetValue<T>(String? key, T value, ICryptKey? crypt, TryConverter<String?, T>? converter, params String[]? sections)
        {
            return GetOrSetValue(key, value, crypt, converter, (IEnumerable<String>?) sections);
        }

        public T? GetOrSetValue<T>(String? key, T value, ICryptKey? crypt, TryConverter<String?, T>? converter, IEnumerable<String>? sections)
        {
            sections = sections.Materialize();
            String? current = GetValue(key, sections);

            if (GetOrSetValueInternalCrypt(current, value, crypt, converter, out T? result))
            {
                return result;
            }

            if (!IsReadOnly)
            {
                SetValue(key, value, crypt, sections);
            }
            
            return value;
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
            return GetOrSetValueAsync(key, value, (ICryptKey?) null, sections, token);
        }
        
        public Task<T?> GetOrSetValueAsync<T>(String? key, T value, ICryptKey? crypt, params String[]? sections)
        {
            return GetOrSetValueAsync(key, value, crypt, (IEnumerable<String>?) sections);
        }
        
        public Task<T?> GetOrSetValueAsync<T>(String? key, T value, ICryptKey? crypt, IEnumerable<String>? sections)
        {
            return GetOrSetValueAsync(key, value, crypt, sections, CancellationToken.None);
        }
        
        public Task<T?> GetOrSetValueAsync<T>(String? key, T value, ICryptKey? crypt, CancellationToken token, params String[]? sections)
        {
            return GetOrSetValueAsync(key, value, crypt, sections, token);
        }
        
        public Task<T?> GetOrSetValueAsync<T>(String? key, T value, ICryptKey? crypt, IEnumerable<String>? sections, CancellationToken token)
        {
            return GetOrSetValueAsync(key, value, crypt, ConvertUtilities.TryConvert, sections, token);
        }
        
        public Task<T?> GetOrSetValueAsync<T>(String? key, T value, TryConverter<String?, T>? converter, params String[]? sections)
        {
            return GetOrSetValueAsync(key, value, converter, (IEnumerable<String>?) sections);
        }
        
        public Task<T?> GetOrSetValueAsync<T>(String? key, T value, TryConverter<String?, T>? converter, IEnumerable<String>? sections)
        {
            return GetOrSetValueAsync(key, value, converter, sections, CancellationToken.None);
        }
        
        public Task<T?> GetOrSetValueAsync<T>(String? key, T value, TryConverter<String?, T>? converter, CancellationToken token, params String[]? sections)
        {
            return GetOrSetValueAsync(key, value, converter, sections, token);
        }
        
        public Task<T?> GetOrSetValueAsync<T>(String? key, T value, TryConverter<String?, T>? converter, IEnumerable<String>? sections, CancellationToken token)
        {
            return GetOrSetValueAsync(key, value, null, converter, sections, token);
        }
        
        public Task<T?> GetOrSetValueAsync<T>(String? key, T value, ICryptKey? crypt, TryConverter<String?, T>? converter, params String[]? sections)
        {
            return GetOrSetValueAsync(key, value, crypt, converter, (IEnumerable<String>?) sections);
        }

        public Task<T?> GetOrSetValueAsync<T>(String? key, T value, ICryptKey? crypt, TryConverter<String?, T>? converter, IEnumerable<String>? sections)
        {
            return GetOrSetValueAsync(key, value, crypt, converter, sections, CancellationToken.None);
        }
        
        public Task<T?> GetOrSetValueAsync<T>(String? key, T value, ICryptKey? crypt, TryConverter<String?, T>? converter, CancellationToken token, params String[]? sections)
        {
            return GetOrSetValueAsync(key, value, crypt, converter, sections, token);
        }

        public async Task<T?> GetOrSetValueAsync<T>(String? key, T value, ICryptKey? crypt, TryConverter<String?, T>? converter, IEnumerable<String>? sections, CancellationToken token)
        {
            sections = sections.Materialize();
            String? current = await GetValueAsync(key, sections, token).ConfigureAwait(false);

            if (GetOrSetValueInternalCrypt(current, value, crypt, converter, out T? result))
            {
                return result;
            }

            if (!IsReadOnly)
            {
                await SetValueAsync(key, value, crypt, sections, token).ConfigureAwait(false);
            }
            
            return value;
        }
        
        public Boolean KeyExist(String? key, params String[]? sections)
        {
            return KeyExist(key, (IEnumerable<String>?) sections);
        }

        public Boolean KeyExist(String? key, IEnumerable<String>? sections)
        {
            return GetValue(key, sections) is not null;
        }
        
        public Task<Boolean> KeyExistAsync(String? key, params String[]? sections)
        {
            return KeyExistAsync(key, (IEnumerable<String>?) sections);
        }
        
        public Task<Boolean> KeyExistAsync(String? key, IEnumerable<String>? sections)
        {
            return KeyExistAsync(key, sections, CancellationToken.None);
        }
        
        public Task<Boolean> KeyExistAsync(String? key, CancellationToken token, params String[]? sections)
        {
            return KeyExistAsync(key, sections, token);
        }
        
        public async Task<Boolean> KeyExistAsync(String? key, IEnumerable<String>? sections, CancellationToken token)
        {
            return await GetValueAsync(key, sections, token).ConfigureAwait(false) is not null;
        }

        public ConfigurationEntry[]? GetExists()
        {
            return Behavior.GetExists();
        }

        public Task<ConfigurationEntry[]?> GetExistsAsync()
        {
            return Behavior.GetExistsAsync();
        }

        public Task<ConfigurationEntry[]?> GetExistsAsync(CancellationToken token)
        {
            return Behavior.GetExistsAsync(token);
        }
        
        public Boolean RemoveValue(String? key, params String[]? sections)
        {
            return RemoveValue(key, (IEnumerable<String>?) sections);
        }

        public Boolean RemoveValue(String? key, IEnumerable<String>? sections)
        {
            return SetValue<String?>(key, null, sections);
        }
        
        public Task<Boolean> RemoveValueAsync(String? key, params String[]? sections)
        {
            return RemoveValueAsync(key, (IEnumerable<String>?) sections);
        }
        
        public Task<Boolean> RemoveValueAsync(String? key, IEnumerable<String>? sections)
        {
            return RemoveValueAsync(key, sections, CancellationToken.None);
        }
        
        public Task<Boolean> RemoveValueAsync(String? key, CancellationToken token, params String[]? sections)
        {
            return RemoveValueAsync(key, sections, token);
        }
        
        public Task<Boolean> RemoveValueAsync(String? key, IEnumerable<String>? sections, CancellationToken token)
        {
            return SetValueAsync<String?>(key, null, sections, token);
        }

        protected String? Get(String? key, IEnumerable<String>? sections)
        {
            return Behavior.Get(key, sections);
        }

        protected Task<String?> GetAsync(String? key, IEnumerable<String>? sections, CancellationToken token)
        {
            return Behavior.GetAsync(key, sections, token);
        }

        protected Boolean Set(String? key, String? value, IEnumerable<String>? sections)
        {
            return Behavior.Set(key, value, sections);
        }

        protected Task<Boolean> SetAsync(String? key, String? value, IEnumerable<String>? sections, CancellationToken token)
        {
            return Behavior.SetAsync(key, value, sections, token);
        }

        private Boolean GetInternalCrypt(ref String? key, ref IEnumerable<String>? sections)
        {
            if (!IsCryptData)
            {
                return true;
            }

            key = key is not null ? Crypt.Encrypt(key) : null;
            sections = sections is not null ? Crypt.Encrypt(sections).WhereNotNull().ToArray() : null;
            return true;
        }

        private String? GetInternal(String? key, IEnumerable<String>? sections)
        {
            return GetInternalCrypt(ref key, ref sections) ? Get(key, sections) : null;
        }
        
        private Task<String?> GetInternalAsync(String? key, IEnumerable<String>? sections, CancellationToken token)
        {
            return GetInternalCrypt(ref key, ref sections) ? GetAsync(key, sections, token) : StringUtilities.Null;
        }

        private Boolean SetInternalCrypt(ref String? key, ref String? value, ref IEnumerable<String>? sections)
        {
            if (CheckReadOnly())
            {
                return false;
            }

            if (!IsCryptData)
            {
                return true;
            }

            key = key is not null ? Crypt.Encrypt(key) : null;
            value = value is not null ? Crypt.Encrypt(value) : null;
            sections = sections is not null ? Crypt.Encrypt(sections).WhereNotNull().ToArray() : null;
            return true;
        }
        
        private Boolean SetInternal(String? key, String? value, IEnumerable<String>? sections)
        {
            return SetInternalCrypt(ref key, ref value, ref sections) && Set(key, value, sections);
        }
        
        private Task<Boolean> SetInternalAsync(String? key, String? value, IEnumerable<String>? sections, CancellationToken token)
        {
            return SetInternalCrypt(ref key, ref value, ref sections) ? SetAsync(key, value, sections, token) : TaskUtilities.False;
        }
        
        public Boolean Reload()
        {
            return Behavior.Reload();
        }

        public Task<Boolean> ReloadAsync()
        {
            return Behavior.ReloadAsync();
        }

        public Task<Boolean> ReloadAsync(CancellationToken token)
        {
            return Behavior.ReloadAsync(token);
        }

        private Boolean CheckReadOnly()
        {
            if (IsReadOnly && ThrowOnReadOnly)
            {
                throw new ReadOnlyException("Config in readonly mode");
            }

            return IsReadOnly;
        }

        public override String ToString()
        {
            return Path;
        }

        private Boolean _disposed;
        public void Dispose()
        {
            DisposeInternal(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(Boolean disposing)
        {
        }

        private void DisposeInternal(Boolean disposing)
        {
            if (_disposed)
            {
                return;
            }

            if (disposing)
            {
                Behavior.Dispose();
            }
            
            Dispose(disposing);

            _disposed = true;
        }

        ~Config()
        {
            Dispose(false);
        }
    }
}