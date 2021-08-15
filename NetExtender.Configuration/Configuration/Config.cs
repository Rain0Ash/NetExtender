// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using NetExtender.Configuration.Common;
using NetExtender.Configuration.Interfaces;
using NetExtender.Crypto;
using NetExtender.Crypto.CryptKey.Interfaces;
using NetExtender.Exceptions;
using NetExtender.Utilities.Types;

namespace NetExtender.Configuration
{
    public class Config : IConfig
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
            set
            {
                Behavior.ThrowOnReadOnly = value;
            }
        }

        public Boolean CryptByDefault
        {
            get
            {
                return Behavior.CryptByDefault;
            }
            set
            {
                Behavior.CryptByDefault = value;
            }
        }

        public ConfigPropertyOptions DefaultOptions { get; set; } = ConfigPropertyOptions.Caching;

        public Config(IConfigBehavior behavior)
        {
            Behavior = behavior ?? throw new ArgumentNullException(nameof(behavior));
        }

        protected String ConvertToValue<T>(T value)
        {
            return Behavior.ConvertToValue(value);
        }

        private String SetValueInternalCrypt<T>(T value, ICryptKey crypt)
        {
            crypt ??= Crypt;
            
            // ReSharper disable once ConvertIfStatementToReturnStatement
            if (crypt.IsEncrypt)
            {
                return crypt.Encrypt(ConvertToValue(value));
            }

            return ConvertToValue(value);
        }

        public Boolean SetValue<T>(String key, T value, IEnumerable<String> sections)
        {
            return SetInternal(key, ConvertToValue(value), sections);
        }
        
        public Boolean SetValue<T>(String key, T value, ICryptKey crypt, IEnumerable<String> sections)
        {
            String result = SetValueInternalCrypt(value, crypt);
            return SetValue(key, result, sections);
        }
        
        public Task<Boolean> SetValueAsync<T>(String key, T value, IEnumerable<String> sections)
        {
            return SetValueAsync(key, value, sections, CancellationToken.None);
        }
        
        public Task<Boolean> SetValueAsync<T>(String key, T value, IEnumerable<String> sections, CancellationToken token)
        {
            return SetInternalAsync(key, ConvertToValue(value), sections, token);
        }

        public Task<Boolean> SetValueAsync<T>(String key, T value, ICryptKey crypt, IEnumerable<String> sections)
        {
            return SetValueAsync(key, value, crypt, sections, CancellationToken.None);
        }
        
        public Task<Boolean> SetValueAsync<T>(String key, T value, ICryptKey crypt, IEnumerable<String> sections, CancellationToken token)
        {
            String result = SetValueInternalCrypt(value, crypt);
            return SetValueAsync(key, result, sections, token);
        }

        protected T ConvertFromValue<T>(String value)
        {
            return Behavior.ConvertFromValue<T>(value);
        }

        private String? GetValueInternalCrypt(String? value, Boolean decrypt, ICryptKey? crypt)
        {
            crypt ??= Crypt;
            
            if (!decrypt || value is null)
            {
                return value;
            }

            return crypt.Decrypt(value) ?? value;
        }

        public String? GetValue(String key, IEnumerable<String> sections)
        {
            return GetInternal(key, sections);
        }
        
        public Task<String> GetValueAsync(String key, IEnumerable<String> sections)
        {
            return GetValueAsync(key, sections, CancellationToken.None);
        }
        
        public Task<String> GetValueAsync(String key, IEnumerable<String> sections, CancellationToken token)
        {
            return GetInternalAsync(key, sections, token);
        }

        public String GetValue(String key, String defaultValue, IEnumerable<String> sections)
        {
            return GetValue(key, sections) ?? defaultValue;
        }
        
        public Task<String> GetValueAsync(String key, String defaultValue, IEnumerable<String> sections)
        {
            return GetValueAsync(key, defaultValue, sections, CancellationToken.None);
        }
        
        public async Task<String> GetValueAsync(String key, String defaultValue, IEnumerable<String> sections, CancellationToken token)
        {
            return await GetValueAsync(key, sections, token).ConfigureAwait(false) ?? defaultValue;
        }

        public String GetValue(String key, String defaultValue, Boolean decrypt, IEnumerable<String> sections)
        {
            return GetValue(key, defaultValue, decrypt, null, sections);
        }

        public String GetValue(String key, String defaultValue, Boolean decrypt, ICryptKey crypt, IEnumerable<String> sections)
        {
            String value = GetValue(key, defaultValue, sections);
            return GetValueInternalCrypt(value, decrypt, crypt);
        }
        
        public Task<String> GetValueAsync(String key, String defaultValue, Boolean decrypt, IEnumerable<String> sections)
        {
            return GetValueAsync(key, defaultValue, decrypt, sections, CancellationToken.None);
        }
        
        public Task<String> GetValueAsync(String key, String defaultValue, Boolean decrypt, IEnumerable<String> sections, CancellationToken token)
        {
            return GetValueAsync(key, defaultValue, decrypt, null, sections, token);
        }

        public Task<String> GetValueAsync(String key, String defaultValue, Boolean decrypt, ICryptKey crypt, IEnumerable<String> sections)
        {
            return GetValueAsync(key, defaultValue, decrypt, crypt, sections, CancellationToken.None);
        }

        public async Task<String> GetValueAsync(String key, String defaultValue, Boolean decrypt, ICryptKey crypt, IEnumerable<String> sections, CancellationToken token)
        {
            String value = await GetValueAsync(key, defaultValue, sections, token).ConfigureAwait(false);
            return GetValueInternalCrypt(value, decrypt, crypt);
        }
        
        private T GetValueInternalCrypt<T>(String value, T defaultValue, ICryptKey crypt, TryConverter<String, T> converter)
        {
            crypt ??= Crypt;
            converter ??= ConvertUtilities.TryConvert;
            
            T cval;
            if (!crypt.IsDecrypt || value is null)
            {
                return converter(value, out cval) ? cval : defaultValue;
            }

            return converter(crypt.Decrypt(value) ?? value, out cval) ? cval : defaultValue;
        }

        public T GetValue<T>(String key, IEnumerable<String> sections)
        {
            return ConvertFromValue<T>(GetValue(key, sections));
        }
        
        public Task<T> GetValueAsync<T>(String key, IEnumerable<String> sections)
        {
            return GetValueAsync<T>(key, sections, CancellationToken.None);
        }
        
        public async Task<T> GetValueAsync<T>(String key, IEnumerable<String> sections, CancellationToken token)
        {
            return ConvertFromValue<T>(await GetValueAsync(key, sections, token).ConfigureAwait(false));
        }

        public T GetValue<T>(String key, T defaultValue, IEnumerable<String> sections)
        {
            return GetValue(key, defaultValue, (ICryptKey) null, sections);
        }

        public T GetValue<T>(String key, T defaultValue, ICryptKey crypt, IEnumerable<String> sections)
        {
            return GetValue(key, defaultValue, crypt, ConvertUtilities.TryConvert, sections);
        }

        public T GetValue<T>(String key, T defaultValue, TryConverter<String, T> converter, IEnumerable<String> sections)
        {
            return GetValue(key, defaultValue, null, converter, sections);
        }

        public T GetValue<T>(String key, T defaultValue, ICryptKey crypt, TryConverter<String, T> converter, IEnumerable<String> sections)
        {
            String value = GetValue(key, defaultValue.GetString(CultureInfo.InvariantCulture), sections);
            return GetValueInternalCrypt(value, defaultValue, crypt, converter);
        }
        
        public Task<T> GetValueAsync<T>(String key, T defaultValue, IEnumerable<String> sections)
        {
            return GetValueAsync(key, defaultValue, sections, CancellationToken.None);
        }
        
        public Task<T> GetValueAsync<T>(String key, T defaultValue, IEnumerable<String> sections, CancellationToken token)
        {
            return GetValueAsync(key, defaultValue, (ICryptKey) null, sections, token);
        }
        
        public Task<T> GetValueAsync<T>(String key, T defaultValue, ICryptKey crypt, IEnumerable<String> sections)
        {
            return GetValueAsync(key, defaultValue, crypt, sections, CancellationToken.None);
        }
        
        public Task<T> GetValueAsync<T>(String key, T defaultValue, ICryptKey crypt, IEnumerable<String> sections, CancellationToken token)
        {
            return GetValueAsync(key, defaultValue, crypt, ConvertUtilities.TryConvert, sections, token);
        }
        
        public Task<T> GetValueAsync<T>(String key, T defaultValue, TryConverter<String, T> converter, IEnumerable<String> sections)
        {
            return GetValueAsync(key, defaultValue, converter, sections, CancellationToken.None);
        }
        
        public Task<T> GetValueAsync<T>(String key, T defaultValue, TryConverter<String, T> converter, IEnumerable<String> sections, CancellationToken token)
        {
            return GetValueAsync(key, defaultValue, null, converter, sections, CancellationToken.None);
        }

        public Task<T> GetValueAsync<T>(String key, T defaultValue, ICryptKey crypt, TryConverter<String, T> converter, IEnumerable<String> sections)
        {
            return GetValueAsync(key, defaultValue, crypt, converter, sections, CancellationToken.None);
        }
        
        public async Task<T> GetValueAsync<T>(String key, T defaultValue, ICryptKey crypt, TryConverter<String, T> converter, IEnumerable<String> sections, CancellationToken token)
        {
            String value = await GetValueAsync(key, defaultValue.GetString(CultureInfo.InvariantCulture), sections, token).ConfigureAwait(false);
            return GetValueInternalCrypt(value, defaultValue, crypt, converter);
        }

        public Boolean GetOrSetValueInternalCrypt(String value, CryptAction crypt, ICryptKey cryptKey, out String result)
        {
            cryptKey ??= Crypt;

            if (value is null)
            {
                result = default;
                return false;
            }

            result = crypt.HasFlag(CryptAction.Decrypt) ? cryptKey.Decrypt(value) ?? value : value;
            return true;
        }

        public String GetOrSetValue(String key, String defaultValue, IEnumerable<String> sections)
        {
            return GetOrSetValue(key, defaultValue, CryptAction.Decrypt, sections);
        }

        public String GetOrSetValue(String key, String defaultValue, CryptAction crypt, IEnumerable<String> sections)
        {
            return GetOrSetValue(key, defaultValue, crypt, null, sections);
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("ReSharper", "PossibleMultipleEnumeration")]
        public String GetOrSetValue(String key, String defaultValue, CryptAction crypt, ICryptKey cryptKey, IEnumerable<String> sections)
        {
            sections = sections.Materialize();
            String value = GetValue(key, sections);

            if (GetOrSetValueInternalCrypt(value, crypt, cryptKey, out String result))
            {
                return result;
            }

            SetValue(key, defaultValue, cryptKey, sections);
            return defaultValue;
        }
        
        public Task<String> GetOrSetValueAsync(String key, String defaultValue, IEnumerable<String> sections)
        {
            return GetOrSetValueAsync(key, defaultValue, sections, CancellationToken.None);
        }
        
        public Task<String> GetOrSetValueAsync(String key, String defaultValue, IEnumerable<String> sections, CancellationToken token)
        {
            return GetOrSetValueAsync(key, defaultValue, CryptAction.Decrypt, sections, token);
        }
        
        public Task<String> GetOrSetValueAsync(String key, String defaultValue, CryptAction crypt, IEnumerable<String> sections)
        {
            return GetOrSetValueAsync(key, defaultValue, crypt, sections, CancellationToken.None);
        }
        
        public Task<String> GetOrSetValueAsync(String key, String defaultValue, CryptAction crypt, IEnumerable<String> sections, CancellationToken token)
        {
            return GetOrSetValueAsync(key, defaultValue, crypt, null, sections, token);
        }

        public Task<String> GetOrSetValueAsync(String key, String defaultValue, CryptAction crypt, ICryptKey cryptKey, IEnumerable<String> sections)
        {
            return GetOrSetValueAsync(key, defaultValue, crypt, cryptKey, sections, CancellationToken.None);
        }
        
        [System.Diagnostics.CodeAnalysis.SuppressMessage("ReSharper", "PossibleMultipleEnumeration")]
        public async Task<String> GetOrSetValueAsync(String key, String defaultValue, CryptAction crypt, ICryptKey cryptKey, IEnumerable<String> sections, CancellationToken token)
        {
            sections = sections.Materialize();
            String value = await GetValueAsync(key, sections, token).ConfigureAwait(false);

            if (GetOrSetValueInternalCrypt(value, crypt, cryptKey, out String result))
            {
                return result;
            }

            await SetValueAsync(key, defaultValue, cryptKey, sections, token).ConfigureAwait(false);
            return defaultValue;
        }
        
        public Boolean GetOrSetValueInternalCrypt<T>(String value, T defaultValue, ICryptKey crypt, TryConverter<String, T> converter, out T result)
        {
            crypt ??= Crypt;
            converter ??= ConvertUtilities.TryConvert;
            
            if (value is null)
            {
                result = default;
                return false;
            }
            
            if (crypt.IsDecrypt)
            {
                value = crypt.Decrypt(value) ?? value;
            }

            result = converter(value, out T cval) ? cval : defaultValue;
            return true;
        }

        public T GetOrSetValue<T>(String key, T defaultValue, IEnumerable<String> sections)
        {
            return GetOrSetValue(key, defaultValue, (ICryptKey) null, sections);
        }
        
        public T GetOrSetValue<T>(String key, T defaultValue, ICryptKey crypt, IEnumerable<String> sections)
        {
            return GetOrSetValue(key, defaultValue, crypt, ConvertUtilities.TryConvert, sections);
        }

        public T GetOrSetValue<T>(String key, T defaultValue, TryConverter<String, T> converter, IEnumerable<String> sections)
        {
            return GetOrSetValue(key, defaultValue, null, converter, sections);
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("ReSharper", "PossibleMultipleEnumeration")]
        public T GetOrSetValue<T>(String key, T defaultValue, ICryptKey crypt, TryConverter<String, T> converter, IEnumerable<String> sections)
        {
            sections = sections.Materialize();
            String value = GetValue(key, sections);

            if (GetOrSetValueInternalCrypt(value, defaultValue, crypt, converter, out T result))
            {
                return result;
            }

            if (!IsReadOnly)
            {
                SetValue(key, defaultValue, crypt, sections);
            }
            
            return defaultValue;
        }
        
        public Task<T> GetOrSetValueAsync<T>(String key, T defaultValue, IEnumerable<String> sections)
        {
            return GetOrSetValueAsync(key, defaultValue, sections, CancellationToken.None);
        }
        
        public Task<T> GetOrSetValueAsync<T>(String key, T defaultValue, IEnumerable<String> sections, CancellationToken token)
        {
            return GetOrSetValueAsync(key, defaultValue, (ICryptKey) null, sections, token);
        }
        
        public Task<T> GetOrSetValueAsync<T>(String key, T defaultValue, ICryptKey crypt, IEnumerable<String> sections)
        {
            return GetOrSetValueAsync(key, defaultValue, crypt, sections, CancellationToken.None);
        }
        
        public Task<T> GetOrSetValueAsync<T>(String key, T defaultValue, ICryptKey crypt, IEnumerable<String> sections, CancellationToken token)
        {
            return GetOrSetValueAsync(key, defaultValue, crypt, ConvertUtilities.TryConvert, sections, token);
        }
        
        public Task<T> GetOrSetValueAsync<T>(String key, T defaultValue, TryConverter<String, T> converter, IEnumerable<String> sections)
        {
            return GetOrSetValueAsync(key, defaultValue, converter, sections, CancellationToken.None);
        }
        
        public Task<T> GetOrSetValueAsync<T>(String key, T defaultValue, TryConverter<String, T> converter, IEnumerable<String> sections, CancellationToken token)
        {
            return GetOrSetValueAsync(key, defaultValue, null, converter, sections, token);
        }

        public Task<T> GetOrSetValueAsync<T>(String key, T defaultValue, ICryptKey crypt, TryConverter<String, T> converter, IEnumerable<String> sections)
        {
            return GetOrSetValueAsync(key, defaultValue, crypt, converter, sections, CancellationToken.None);
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("ReSharper", "PossibleMultipleEnumeration")]
        public async Task<T> GetOrSetValueAsync<T>(String key, T defaultValue, ICryptKey crypt, TryConverter<String, T> converter, IEnumerable<String> sections, CancellationToken token)
        {
            sections = sections.Materialize();
            String value = await GetValueAsync(key, sections, token).ConfigureAwait(false);

            if (GetOrSetValueInternalCrypt(value, defaultValue, crypt, converter, out T result))
            {
                return result;
            }

            if (!IsReadOnly)
            {
                await SetValueAsync(key, defaultValue, crypt, sections, token).ConfigureAwait(false);
            }
            
            return defaultValue;
        }

        public Boolean KeyExist(String key, IEnumerable<String> sections)
        {
            return GetValue(key, sections) is not null;
        }
        
        public Task<Boolean> KeyExistAsync(String key, IEnumerable<String> sections)
        {
            return KeyExistAsync(key, sections, CancellationToken.None);
        }
        
        public async Task<Boolean> KeyExistAsync(String key, IEnumerable<String> sections, CancellationToken token)
        {
            return await GetValueAsync(key, sections, token).ConfigureAwait(false) is not null;
        }

        public Boolean RemoveValue(String key, IEnumerable<String> sections)
        {
            return SetValue<String>(key, null, sections);
        }
        
        public Task<Boolean> RemoveValueAsync(String key, IEnumerable<String> sections)
        {
            return RemoveValueAsync(key, sections, CancellationToken.None);
        }
        
        public Task<Boolean> RemoveValueAsync(String key, IEnumerable<String> sections, CancellationToken token)
        {
            return SetValueAsync<String>(key, null, sections, token);
        }

        protected String Get(String key, IEnumerable<String> sections)
        {
            return Behavior.Get(key, sections);
        }

        // ReSharper disable once UnusedParameter.Global
        protected Task<String> GetAsync(String key, IEnumerable<String> sections, CancellationToken token)
        {
            return Behavior.GetAsync(key, sections, token);
        }

        protected Boolean Set(String key, String value, IEnumerable<String> sections)
        {
            return Behavior.Set(key, value, sections);
        }

        // ReSharper disable once UnusedParameter.Global
        protected Task<Boolean> SetAsync(String key, String value, IEnumerable<String> sections, CancellationToken token)
        {
            return Behavior.SetAsync(key, value, sections, token);
        }

        private Boolean GetInternalCrypt(ref String key, ref IEnumerable<String> sections)
        {
            if (!IsCryptData)
            {
                return true;
            }

            key = Crypt.Encrypt(key);
            sections = Crypt.Encrypt(sections).ToArray();
            return true;
        }

        private String? GetInternal(String key, IEnumerable<String> sections)
        {
            // ReSharper disable once ConvertIfStatementToReturnStatement
            if (GetInternalCrypt(ref key, ref sections))
            {
                return Get(key, sections);
            }

            return null;
        }
        
        private Task<String?> GetInternalAsync(String key, IEnumerable<String> sections, CancellationToken token)
        {
            // ReSharper disable once ConvertIfStatementToReturnStatement
            if (GetInternalCrypt(ref key, ref sections))
            {
                return GetAsync(key, sections, token);
            }

            return StringUtilities.Null;
        }

        private Boolean SetInternalCrypt(ref String key, ref String value, ref IEnumerable<String> sections)
        {
            if (CheckReadOnly())
            {
                return false;
            }

            // ReSharper disable once InvertIf
            if (IsCryptData)
            {
                key = Crypt.Encrypt(key);
                value = Crypt.Encrypt(value);
                sections = Crypt.Encrypt(sections).ToArray();
            }

            return true;
        }
        
        private Boolean SetInternal(String key, String value, IEnumerable<String> sections)
        {
            // ReSharper disable once ConvertIfStatementToReturnStatement
            if (SetInternalCrypt(ref key, ref value, ref sections))
            {
                return Set(key, value, sections);
            }

            return false;
        }
        
        private Task<Boolean> SetInternalAsync(String key, String value, IEnumerable<String> sections, CancellationToken token)
        {
            // ReSharper disable once ConvertIfStatementToReturnStatement
            if (SetInternalCrypt(ref key, ref value, ref sections))
            {
                return SetAsync(key, value, sections, token);
            }

            return TaskUtilities.False;
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