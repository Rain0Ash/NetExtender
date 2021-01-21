// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using JetBrains.Annotations;
using NetExtender.Configuration.Common;
using NetExtender.Configuration.Interfaces;
using NetExtender.Converters;
using NetExtender.Crypto;
using NetExtender.Crypto.CryptKey.Interfaces;
using NetExtender.Utils.Types;
using ReactiveUI;
using ReactiveUI.Fody.Helpers;

namespace NetExtender.Configuration
{
    public partial class Config : ReactiveObject, IPropertyConfig
    {
        public const ConfigType DefaultConfigType = ConfigType.Registry;
        public const ConfigOptions DefaultConfigOptions = ConfigOptions.None;
        
        public static IPropertyConfig Create([NotNull] IConfigBehavior behavior)
        {
            if (behavior is null)
            {
                throw new ArgumentNullException(nameof(behavior));
            }

            return new Config(behavior);
        }
        
        public static IPropertyConfig Create(String path)
        {
            return new Config(ConfigBehavior.Create(path));
        }
        
        public static IPropertyConfig Create(String path, ConfigType type)
        {
            return new Config(ConfigBehavior.Create(path, type));
        }
        
        public static IPropertyConfig Create(String path, ConfigOptions options)
        {
            return new Config(ConfigBehavior.Create(path, options));
        }

        public static IPropertyConfig Create(String path, ConfigType type, ConfigOptions options)
        {
            return new Config(ConfigBehavior.Create(path, type, options));
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

        [Reactive]
        public ConfigPropertyOptions DefaultOptions { get; set; } = ConfigPropertyOptions.Caching;

        public Config([NotNull] IConfigBehavior behavior)
        {
            Behavior = behavior ?? throw new ArgumentNullException(nameof(behavior));
        }
        
        public Config(ConfigType type = DefaultConfigType, ConfigOptions options = DefaultConfigOptions)
            : this(null, type, options)
        {
        }
        
        public Config(String path, ConfigType type = DefaultConfigType, ConfigOptions options = DefaultConfigOptions)
            : this(ConfigBehavior.Create(path, type, options))
        {
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

        public Boolean SetValue<T>(String key, T value, params String[] sections)
        {
            return SetInternal(key, ConvertToValue(value), sections);
        }
        
        public Boolean SetValue<T>(String key, T value, ICryptKey crypt, params String[] sections)
        {
            String result = SetValueInternalCrypt(value, crypt);
            return SetValue(key, result, sections);
        }
        
        public Task<Boolean> SetValueAsync<T>(String key, T value, params String[] sections)
        {
            return SetValueAsync(key, value, CancellationToken.None, sections);
        }
        
        public Task<Boolean> SetValueAsync<T>(String key, T value, CancellationToken token, params String[] sections)
        {
            return SetInternalAsync(key, ConvertToValue(value), token, sections);
        }

        public Task<Boolean> SetValueAsync<T>(String key, T value, ICryptKey crypt, params String[] sections)
        {
            return SetValueAsync(key, value, crypt, CancellationToken.None, sections);
        }
        
        public Task<Boolean> SetValueAsync<T>(String key, T value, ICryptKey crypt, CancellationToken token, params String[] sections)
        {
            String result = SetValueInternalCrypt(value, crypt);
            return SetValueAsync(key, result, token, sections);
        }

        protected T ConvertFromValue<T>(String value)
        {
            return Behavior.ConvertFromValue<T>(value);
        }

        private String GetValueInternalCrypt(String value, Boolean decrypt, ICryptKey crypt)
        {
            crypt ??= Crypt;
            
            if (!decrypt || value is null)
            {
                return value;
            }

            return crypt.Decrypt(value) ?? value;
        }

        public String GetValue(String key, params String[] sections)
        {
            return GetInternal(key, sections);
        }
        
        public Task<String> GetValueAsync(String key, params String[] sections)
        {
            return GetValueAsync(key, CancellationToken.None, sections);
        }
        
        public Task<String> GetValueAsync(String key, CancellationToken token, params String[] sections)
        {
            return GetInternalAsync(key, token, sections);
        }

        public String GetValue(String key, String defaultValue, params String[] sections)
        {
            return GetValue(key, sections) ?? defaultValue;
        }
        
        public Task<String> GetValueAsync(String key, String defaultValue, params String[] sections)
        {
            return GetValueAsync(key, defaultValue, CancellationToken.None, sections);
        }
        
        public async Task<String> GetValueAsync(String key, String defaultValue, CancellationToken token, params String[] sections)
        {
            return await GetValueAsync(key, token, sections) ?? defaultValue;
        }

        public String GetValue(String key, String defaultValue, Boolean decrypt, params String[] sections)
        {
            return GetValue(key, defaultValue, decrypt, null, sections);
        }

        public String GetValue(String key, String defaultValue, Boolean decrypt, ICryptKey crypt, params String[] sections)
        {
            String value = GetValue(key, defaultValue, sections);
            return GetValueInternalCrypt(value, decrypt, crypt);
        }
        
        public Task<String> GetValueAsync(String key, String defaultValue, Boolean decrypt, params String[] sections)
        {
            return GetValueAsync(key, defaultValue, decrypt, CancellationToken.None, sections);
        }
        
        public Task<String> GetValueAsync(String key, String defaultValue, Boolean decrypt, CancellationToken token, params String[] sections)
        {
            return GetValueAsync(key, defaultValue, decrypt, null, token, sections);
        }

        public Task<String> GetValueAsync(String key, String defaultValue, Boolean decrypt, ICryptKey crypt, params String[] sections)
        {
            return GetValueAsync(key, defaultValue, decrypt, crypt, CancellationToken.None, sections);
        }

        public async Task<String> GetValueAsync(String key, String defaultValue, Boolean decrypt, ICryptKey crypt, CancellationToken token, params String[] sections)
        {
            String value = await GetValueAsync(key, defaultValue, token, sections);
            return GetValueInternalCrypt(value, decrypt, crypt);
        }
        
        private T GetValueInternalCrypt<T>(String value, T defaultValue, ICryptKey crypt, TryConverter<String, T> converter)
        {
            crypt ??= Crypt;
            converter ??= ConvertUtils.TryConvert;
            
            T cval;
            if (!crypt.IsDecrypt || value is null)
            {
                return converter(value, out cval) ? cval : defaultValue;
            }

            return converter(crypt.Decrypt(value) ?? value, out cval) ? cval : defaultValue;
        }

        public T GetValue<T>(String key, params String[] sections)
        {
            return ConvertFromValue<T>(GetValue(key, sections));
        }
        
        public Task<T> GetValueAsync<T>(String key, params String[] sections)
        {
            return GetValueAsync<T>(key, CancellationToken.None, sections);
        }
        
        public async Task<T> GetValueAsync<T>(String key, CancellationToken token, params String[] sections)
        {
            return ConvertFromValue<T>(await GetValueAsync(key, token, sections));
        }

        public T GetValue<T>(String key, T defaultValue, params String[] sections)
        {
            return GetValue(key, defaultValue, (ICryptKey) null, sections);
        }

        public T GetValue<T>(String key, T defaultValue, ICryptKey crypt, params String[] sections)
        {
            return GetValue(key, defaultValue, crypt, ConvertUtils.TryConvert, sections);
        }

        public T GetValue<T>(String key, T defaultValue, TryConverter<String, T> converter, params String[] sections)
        {
            return GetValue(key, defaultValue, null, converter, sections);
        }

        public T GetValue<T>(String key, T defaultValue, ICryptKey crypt, TryConverter<String, T> converter, params String[] sections)
        {
            String value = GetValue(key, defaultValue.GetString(CultureInfo.InvariantCulture), sections);
            return GetValueInternalCrypt(value, defaultValue, crypt, converter);
        }
        
        public Task<T> GetValueAsync<T>(String key, T defaultValue, params String[] sections)
        {
            return GetValueAsync(key, defaultValue, CancellationToken.None, sections);
        }
        
        public Task<T> GetValueAsync<T>(String key, T defaultValue, CancellationToken token, params String[] sections)
        {
            return GetValueAsync(key, defaultValue, (ICryptKey) null, token, sections);
        }
        
        public Task<T> GetValueAsync<T>(String key, T defaultValue, ICryptKey crypt, params String[] sections)
        {
            return GetValueAsync(key, defaultValue, crypt, CancellationToken.None, sections);
        }
        
        public Task<T> GetValueAsync<T>(String key, T defaultValue, ICryptKey crypt, CancellationToken token, params String[] sections)
        {
            return GetValueAsync(key, defaultValue, crypt, ConvertUtils.TryConvert, token, sections);
        }
        
        public Task<T> GetValueAsync<T>(String key, T defaultValue, TryConverter<String, T> converter, params String[] sections)
        {
            return GetValueAsync(key, defaultValue, converter, CancellationToken.None, sections);
        }
        
        public Task<T> GetValueAsync<T>(String key, T defaultValue, TryConverter<String, T> converter, CancellationToken token, params String[] sections)
        {
            return GetValueAsync(key, defaultValue, null, converter, CancellationToken.None, sections);
        }

        public Task<T> GetValueAsync<T>(String key, T defaultValue, ICryptKey crypt, TryConverter<String, T> converter, params String[] sections)
        {
            return GetValueAsync(key, defaultValue, crypt, converter, CancellationToken.None, sections);
        }
        
        public async Task<T> GetValueAsync<T>(String key, T defaultValue, ICryptKey crypt, TryConverter<String, T> converter, CancellationToken token, params String[] sections)
        {
            String value = await GetValueAsync(key, defaultValue.GetString(CultureInfo.InvariantCulture), token, sections);
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

        public String GetOrSetValue(String key, String defaultValue, params String[] sections)
        {
            return GetOrSetValue(key, defaultValue, CryptAction.Decrypt, sections);
        }

        public String GetOrSetValue(String key, String defaultValue, CryptAction crypt, params String[] sections)
        {
            return GetOrSetValue(key, defaultValue, crypt, null, sections);
        }

        public String GetOrSetValue(String key, String defaultValue, CryptAction crypt, ICryptKey cryptKey, params String[] sections)
        {
            String value = GetValue(key, sections);

            if (GetOrSetValueInternalCrypt(value, crypt, cryptKey, out String result))
            {
                return result;
            }

            SetValue(key, defaultValue, cryptKey, sections);
            return defaultValue;
        }
        
        public Task<String> GetOrSetValueAsync(String key, String defaultValue, params String[] sections)
        {
            return GetOrSetValueAsync(key, defaultValue, CancellationToken.None, sections);
        }
        
        public Task<String> GetOrSetValueAsync(String key, String defaultValue, CancellationToken token, params String[] sections)
        {
            return GetOrSetValueAsync(key, defaultValue, CryptAction.Decrypt, token, sections);
        }
        
        public Task<String> GetOrSetValueAsync(String key, String defaultValue, CryptAction crypt, params String[] sections)
        {
            return GetOrSetValueAsync(key, defaultValue, crypt, CancellationToken.None, sections);
        }
        
        public Task<String> GetOrSetValueAsync(String key, String defaultValue, CryptAction crypt, CancellationToken token, params String[] sections)
        {
            return GetOrSetValueAsync(key, defaultValue, crypt, null, token, sections);
        }

        public Task<String> GetOrSetValueAsync(String key, String defaultValue, CryptAction crypt, ICryptKey cryptKey, params String[] sections)
        {
            return GetOrSetValueAsync(key, defaultValue, crypt, cryptKey, CancellationToken.None, sections);
        }
        
        public async Task<String> GetOrSetValueAsync(String key, String defaultValue, CryptAction crypt, ICryptKey cryptKey, CancellationToken token, params String[] sections)
        {
            String value = await GetValueAsync(key, token, sections);

            if (GetOrSetValueInternalCrypt(value, crypt, cryptKey, out String result))
            {
                return result;
            }

            await SetValueAsync(key, defaultValue, cryptKey, token, sections);
            return defaultValue;
        }
        
        public Boolean GetOrSetValueInternalCrypt<T>(String value, T defaultValue, ICryptKey crypt, TryConverter<String, T> converter, out T result)
        {
            crypt ??= Crypt;
            converter ??= ConvertUtils.TryConvert;
            
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

        public T GetOrSetValue<T>(String key, T defaultValue, params String[] sections)
        {
            return GetOrSetValue(key, defaultValue, (ICryptKey) null, sections);
        }
        
        public T GetOrSetValue<T>(String key, T defaultValue, ICryptKey crypt, params String[] sections)
        {
            return GetOrSetValue(key, defaultValue, crypt, ConvertUtils.TryConvert, sections);
        }

        public T GetOrSetValue<T>(String key, T defaultValue, TryConverter<String, T> converter, params String[] sections)
        {
            return GetOrSetValue(key, defaultValue, null, converter, sections);
        }

        public T GetOrSetValue<T>(String key, T defaultValue, ICryptKey crypt, TryConverter<String, T> converter, params String[] sections)
        {
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
        
        public Task<T> GetOrSetValueAsync<T>(String key, T defaultValue, params String[] sections)
        {
            return GetOrSetValueAsync(key, defaultValue, CancellationToken.None, sections);
        }
        
        public Task<T> GetOrSetValueAsync<T>(String key, T defaultValue, CancellationToken token, params String[] sections)
        {
            return GetOrSetValueAsync(key, defaultValue, (ICryptKey) null, token, sections);
        }
        
        public Task<T> GetOrSetValueAsync<T>(String key, T defaultValue, ICryptKey crypt, params String[] sections)
        {
            return GetOrSetValueAsync(key, defaultValue, crypt, CancellationToken.None, sections);
        }
        
        public Task<T> GetOrSetValueAsync<T>(String key, T defaultValue, ICryptKey crypt, CancellationToken token, params String[] sections)
        {
            return GetOrSetValueAsync(key, defaultValue, crypt, ConvertUtils.TryConvert, token, sections);
        }
        
        public Task<T> GetOrSetValueAsync<T>(String key, T defaultValue, TryConverter<String, T> converter, params String[] sections)
        {
            return GetOrSetValueAsync(key, defaultValue, converter, CancellationToken.None, sections);
        }
        
        public Task<T> GetOrSetValueAsync<T>(String key, T defaultValue, TryConverter<String, T> converter, CancellationToken token, params String[] sections)
        {
            return GetOrSetValueAsync(key, defaultValue, null, converter, token, sections);
        }

        public Task<T> GetOrSetValueAsync<T>(String key, T defaultValue, ICryptKey crypt, TryConverter<String, T> converter, params String[] sections)
        {
            return GetOrSetValueAsync(key, defaultValue, crypt, converter, CancellationToken.None, sections);
        }

        public async Task<T> GetOrSetValueAsync<T>(String key, T defaultValue, ICryptKey crypt, TryConverter<String, T> converter, CancellationToken token, params String[] sections)
        {
            String value = await GetValueAsync(key, token, sections);

            if (GetOrSetValueInternalCrypt(value, defaultValue, crypt, converter, out T result))
            {
                return result;
            }

            if (!IsReadOnly)
            {
                await SetValueAsync(key, defaultValue, crypt, token, sections);
            }
            
            return defaultValue;
        }

        public Boolean KeyExist(String key, params String[] sections)
        {
            return GetValue(key, sections) is not null;
        }
        
        public Task<Boolean> KeyExistAsync(String key, params String[] sections)
        {
            return KeyExistAsync(key, CancellationToken.None, sections);
        }
        
        public async Task<Boolean> KeyExistAsync(String key, CancellationToken token, params String[] sections)
        {
            return await GetValueAsync(key, token, sections) is not null;
        }

        public Boolean RemoveValue(String key, params String[] sections)
        {
            return SetValue<String>(key, null, sections);
        }
        
        public Task<Boolean> RemoveValueAsync(String key, params String[] sections)
        {
            return RemoveValueAsync(key, CancellationToken.None, sections);
        }
        
        public Task<Boolean> RemoveValueAsync(String key, CancellationToken token, params String[] sections)
        {
            return SetValueAsync<String>(key, null, token, sections);
        }

        protected String Get(String key, params String[] sections)
        {
            return Behavior.Get(key, sections);
        }

        // ReSharper disable once UnusedParameter.Global
        protected Task<String> GetAsync(String key, CancellationToken token, params String[] sections)
        {
            return Behavior.GetAsync(key, token, sections);
        }

        protected Boolean Set(String key, String value, params String[] sections)
        {
            return Behavior.Set(key, value, sections);
        }

        // ReSharper disable once UnusedParameter.Global
        protected Task<Boolean> SetAsync(String key, String value, CancellationToken token, params String[] sections)
        {
            return Behavior.SetAsync(key, value, token, sections);
        }

        private Boolean GetInternalCrypt(ref String key, ref String[] sections)
        {
            if (!IsCryptData)
            {
                return true;
            }

            key = Crypt.Encrypt(key);
            sections = Crypt.Encrypt(sections).ToArray();
            return true;
        }

        private String GetInternal(String key, params String[] sections)
        {
            // ReSharper disable once ConvertIfStatementToReturnStatement
            if (GetInternalCrypt(ref key, ref sections))
            {
                return Get(key, sections);
            }

            return null;
        }
        
        private Task<String> GetInternalAsync(String key, CancellationToken token, params String[] sections)
        {
            // ReSharper disable once ConvertIfStatementToReturnStatement
            if (GetInternalCrypt(ref key, ref sections))
            {
                return GetAsync(key, token, sections);
            }

            return StringUtils.Null;
        }

        private Boolean SetInternalCrypt(ref String key, ref String value, ref String[] sections)
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
        
        private Boolean SetInternal(String key, String value, params String[] sections)
        {
            // ReSharper disable once ConvertIfStatementToReturnStatement
            if (SetInternalCrypt(ref key, ref value, ref sections))
            {
                return Set(key, value, sections);
            }

            return false;
        }
        
        private Task<Boolean> SetInternalAsync(String key, String value, CancellationToken token, params String[] sections)
        {
            // ReSharper disable once ConvertIfStatementToReturnStatement
            if (SetInternalCrypt(ref key, ref value, ref sections))
            {
                return SetAsync(key, value, token, sections);
            }

            return TaskUtils.False;
        }

        private Boolean CheckReadOnly()
        {
            if (IsReadOnly && ThrowOnReadOnly)
            {
                throw new ReadOnlyException("Readonly mode");
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
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected void Dispose(Boolean disposing)
        {
            if (_disposed)
            {
                return;
            }

            if (disposing)
            {
                ClearProperties();
                Behavior.Dispose();
            }
            
            DisposeInternal(disposing);

            _disposed = true;
        }

        protected virtual void DisposeInternal(Boolean disposing)
        {
        }

        ~Config()
        {
            Dispose(false);
        }
    }
}