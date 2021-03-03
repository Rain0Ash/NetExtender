// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using JetBrains.Annotations;
using NetExtender.Configuration;
using NetExtender.Configuration.Common;
using NetExtender.Configuration.Interfaces.Property;
using NetExtender.Converters;
using NetExtender.Crypto;
using NetExtender.Crypto.CryptKey.Interfaces;
using NetExtender.Localizations.Interfaces;
using NetExtender.Localizations.Sub.Interfaces;
using NetExtender.Types.Strings.Interfaces;

namespace NetExtender.Localizations.Sub
{
    public sealed class SubLocalizationAdapter : ISubLocalization
    {
        private IPropertyConfig Config { get; }

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

        public Boolean ThrowOnReadOnly
        {
            get
            {
                return Config.ThrowOnReadOnly;
            }
            set
            {
                Config.ThrowOnReadOnly = value;
            }
        }

        public Boolean CryptByDefault
        {
            get
            {
                return Config.CryptByDefault;
            }
            set
            {
                Config.CryptByDefault = value;
            }
        }

        public ConfigPropertyOptions DefaultOptions
        {
            get
            {
                return Config.DefaultOptions;
            }
            set
            {
                Config.DefaultOptions = value;
            }
        }

        public SubLocalizationAdapter([NotNull] IPropertyConfig config)
        {
            Config = config ?? throw new ArgumentNullException(nameof(config));
        }

        public Boolean SetValue<T>(String key, T value, IEnumerable<String> sections)
        {
            return Config.SetValue(key, value, sections);
        }

        public Boolean SetValue<T>(String key, T value, ICryptKey crypt, IEnumerable<String> sections)
        {
            return Config.SetValue(key, value, crypt, sections);
        }

        public Task<Boolean> SetValueAsync<T>(String key, T value, IEnumerable<String> sections)
        {
            return Config.SetValueAsync(key, value, sections);
        }

        public Task<Boolean> SetValueAsync<T>(String key, T value, IEnumerable<String> sections, CancellationToken token)
        {
            return Config.SetValueAsync(key, value, sections, token);
        }

        public Task<Boolean> SetValueAsync<T>(String key, T value, ICryptKey crypt, IEnumerable<String> sections)
        {
            return Config.SetValueAsync(key, value, crypt, sections);
        }

        public Task<Boolean> SetValueAsync<T>(String key, T value, ICryptKey crypt, IEnumerable<String> sections, CancellationToken token)
        {
            return Config.SetValueAsync(key, value, crypt, sections, token);
        }

        public String GetOrSetValue(String key, String defaultValue, IEnumerable<String> sections)
        {
            return Config.GetOrSetValue(key, defaultValue, sections);
        }

        public String GetOrSetValue(String key, String defaultValue, CryptAction crypt, IEnumerable<String> sections)
        {
            return Config.GetOrSetValue(key, defaultValue, crypt, sections);
        }

        public String GetOrSetValue(String key, String defaultValue, CryptAction crypt, ICryptKey cryptKey, IEnumerable<String> sections)
        {
            return Config.GetOrSetValue(key, defaultValue, crypt, cryptKey, sections);
        }

        public Task<String> GetOrSetValueAsync(String key, String defaultValue, IEnumerable<String> sections)
        {
            return Config.GetOrSetValueAsync(key, defaultValue, sections);
        }

        public Task<String> GetOrSetValueAsync(String key, String defaultValue, IEnumerable<String> sections, CancellationToken token)
        {
            return Config.GetOrSetValueAsync(key, defaultValue, sections, token);
        }

        public Task<String> GetOrSetValueAsync(String key, String defaultValue, CryptAction crypt, IEnumerable<String> sections)
        {
            return Config.GetOrSetValueAsync(key, defaultValue, crypt, sections);
        }

        public Task<String> GetOrSetValueAsync(String key, String defaultValue, CryptAction crypt, IEnumerable<String> sections, CancellationToken token)
        {
            return Config.GetOrSetValueAsync(key, defaultValue, crypt, sections, token);
        }

        public Task<String> GetOrSetValueAsync(String key, String defaultValue, CryptAction crypt, ICryptKey cryptKey, IEnumerable<String> sections)
        {
            return Config.GetOrSetValueAsync(key, defaultValue, crypt, cryptKey, sections);
        }

        public Task<String> GetOrSetValueAsync(String key, String defaultValue, CryptAction crypt, ICryptKey cryptKey, IEnumerable<String> sections, CancellationToken token)
        {
            return Config.GetOrSetValueAsync(key, defaultValue, crypt, cryptKey, sections, token);
        }

        public T GetOrSetValue<T>(String key, T defaultValue, IEnumerable<String> sections)
        {
            return Config.GetOrSetValue(key, defaultValue, sections);
        }

        public T GetOrSetValue<T>(String key, T defaultValue, ICryptKey crypt, IEnumerable<String> sections)
        {
            return Config.GetOrSetValue(key, defaultValue, crypt, sections);
        }

        public T GetOrSetValue<T>(String key, T defaultValue, TryConverter<String, T> converter, IEnumerable<String> sections)
        {
            return Config.GetOrSetValue(key, defaultValue, converter, sections);
        }

        public T GetOrSetValue<T>(String key, T defaultValue, ICryptKey crypt, TryConverter<String, T> converter, IEnumerable<String> sections)
        {
            return Config.GetOrSetValue(key, defaultValue, crypt, converter, sections);
        }

        public Task<T> GetOrSetValueAsync<T>(String key, T defaultValue, IEnumerable<String> sections)
        {
            return Config.GetOrSetValueAsync(key, defaultValue, sections);
        }

        public Task<T> GetOrSetValueAsync<T>(String key, T defaultValue, IEnumerable<String> sections, CancellationToken token)
        {
            return Config.GetOrSetValueAsync(key, defaultValue, sections, token);
        }

        public Task<T> GetOrSetValueAsync<T>(String key, T defaultValue, ICryptKey crypt, IEnumerable<String> sections)
        {
            return Config.GetOrSetValueAsync(key, defaultValue, crypt, sections);
        }

        public Task<T> GetOrSetValueAsync<T>(String key, T defaultValue, ICryptKey crypt, IEnumerable<String> sections, CancellationToken token)
        {
            return Config.GetOrSetValueAsync(key, defaultValue, crypt, sections, token);
        }

        public Task<T> GetOrSetValueAsync<T>(String key, T defaultValue, TryConverter<String, T> converter, IEnumerable<String> sections)
        {
            return Config.GetOrSetValueAsync(key, defaultValue, converter, sections);
        }

        public Task<T> GetOrSetValueAsync<T>(String key, T defaultValue, TryConverter<String, T> converter, IEnumerable<String> sections, CancellationToken token)
        {
            return Config.GetOrSetValueAsync(key, defaultValue, converter, sections, token);
        }

        public Task<T> GetOrSetValueAsync<T>(String key, T defaultValue, ICryptKey crypt, TryConverter<String, T> converter, IEnumerable<String> sections)
        {
            return Config.GetOrSetValueAsync(key, defaultValue, crypt, converter, sections);
        }

        public Task<T> GetOrSetValueAsync<T>(String key, T defaultValue, ICryptKey crypt, TryConverter<String, T> converter, IEnumerable<String> sections, CancellationToken token)
        {
            return Config.GetOrSetValueAsync(key, defaultValue, crypt, converter, sections, token);
        }

        public Boolean RemoveValue(String key, IEnumerable<String> sections)
        {
            return Config.RemoveValue(key, sections);
        }

        public Task<Boolean> RemoveValueAsync(String key, IEnumerable<String> sections)
        {
            return Config.RemoveValueAsync(key, sections);
        }

        public Task<Boolean> RemoveValueAsync(String key, IEnumerable<String> sections, CancellationToken token)
        {
            return Config.RemoveValueAsync(key, sections, token);
        }

        public String GetValue(String key, IEnumerable<String> sections)
        {
            return Config.GetValue(key, sections);
        }

        public String GetValue(String key, String defaultValue, IEnumerable<String> sections)
        {
            return Config.GetValue(key, defaultValue, sections);
        }

        public String GetValue(String key, String defaultValue, Boolean decrypt, IEnumerable<String> sections)
        {
            return Config.GetValue(key, defaultValue, decrypt, sections);
        }

        public String GetValue(String key, String defaultValue, Boolean decrypt, ICryptKey crypt, IEnumerable<String> sections)
        {
            return Config.GetValue(key, defaultValue, decrypt, crypt, sections);
        }

        public Task<String> GetValueAsync(String key, IEnumerable<String> sections)
        {
            return Config.GetValueAsync(key, sections);
        }

        public Task<String> GetValueAsync(String key, IEnumerable<String> sections, CancellationToken token)
        {
            return Config.GetValueAsync(key, sections, token);
        }

        public Task<String> GetValueAsync(String key, String defaultValue, IEnumerable<String> sections)
        {
            return Config.GetValueAsync(key, defaultValue, sections);
        }

        public Task<String> GetValueAsync(String key, String defaultValue, IEnumerable<String> sections, CancellationToken token)
        {
            return Config.GetValueAsync(key, defaultValue, sections, token);
        }

        public Task<String> GetValueAsync(String key, String defaultValue, Boolean decrypt, IEnumerable<String> sections)
        {
            return Config.GetValueAsync(key, defaultValue, decrypt, sections);
        }

        public Task<String> GetValueAsync(String key, String defaultValue, Boolean decrypt, IEnumerable<String> sections, CancellationToken token)
        {
            return Config.GetValueAsync(key, defaultValue, decrypt, sections, token);
        }

        public Task<String> GetValueAsync(String key, String defaultValue, Boolean decrypt, ICryptKey crypt, IEnumerable<String> sections)
        {
            return Config.GetValueAsync(key, defaultValue, decrypt, crypt, sections);
        }

        public Task<String> GetValueAsync(String key, String defaultValue, Boolean decrypt, ICryptKey crypt, IEnumerable<String> sections, CancellationToken token)
        {
            return Config.GetValueAsync(key, defaultValue, decrypt, crypt, sections, token);
        }

        public T GetValue<T>(String key, IEnumerable<String> sections)
        {
            return Config.GetValue<T>(key, sections);
        }

        public T GetValue<T>(String key, T defaultValue, IEnumerable<String> sections)
        {
            return Config.GetValue(key, defaultValue, sections);
        }

        public T GetValue<T>(String key, T defaultValue, ICryptKey crypt, IEnumerable<String> sections)
        {
            return Config.GetValue(key, defaultValue, crypt, sections);
        }

        public T GetValue<T>(String key, T defaultValue, TryConverter<String, T> converter, IEnumerable<String> sections)
        {
            return Config.GetValue(key, defaultValue, converter, sections);
        }

        public T GetValue<T>(String key, T defaultValue, ICryptKey crypt, TryConverter<String, T> converter, IEnumerable<String> sections)
        {
            return Config.GetValue(key, defaultValue, crypt, converter, sections);
        }

        public Task<T> GetValueAsync<T>(String key, IEnumerable<String> sections)
        {
            return Config.GetValueAsync<T>(key, sections);
        }

        public Task<T> GetValueAsync<T>(String key, IEnumerable<String> sections, CancellationToken token)
        {
            return Config.GetValueAsync<T>(key, sections, token);
        }

        public Task<T> GetValueAsync<T>(String key, T defaultValue, IEnumerable<String> sections)
        {
            return Config.GetValueAsync(key, defaultValue, sections);
        }

        public Task<T> GetValueAsync<T>(String key, T defaultValue, IEnumerable<String> sections, CancellationToken token)
        {
            return Config.GetValueAsync(key, defaultValue, sections, token);
        }

        public Task<T> GetValueAsync<T>(String key, T defaultValue, ICryptKey crypt, IEnumerable<String> sections)
        {
            return Config.GetValueAsync(key, defaultValue, crypt, sections);
        }

        public Task<T> GetValueAsync<T>(String key, T defaultValue, ICryptKey crypt, IEnumerable<String> sections, CancellationToken token)
        {
            return Config.GetValueAsync(key, defaultValue, crypt, sections, token);
        }

        public Task<T> GetValueAsync<T>(String key, T defaultValue, TryConverter<String, T> converter, IEnumerable<String> sections)
        {
            return Config.GetValueAsync(key, defaultValue, converter, sections);
        }

        public Task<T> GetValueAsync<T>(String key, T defaultValue, TryConverter<String, T> converter, IEnumerable<String> sections, CancellationToken token)
        {
            return Config.GetValueAsync(key, defaultValue, converter, sections, token);
        }

        public Task<T> GetValueAsync<T>(String key, T defaultValue, ICryptKey crypt, TryConverter<String, T> converter, IEnumerable<String> sections)
        {
            return Config.GetValueAsync(key, defaultValue, crypt, converter, sections);
        }

        public Task<T> GetValueAsync<T>(String key, T defaultValue, ICryptKey crypt, TryConverter<String, T> converter, IEnumerable<String> sections, CancellationToken token)
        {
            return Config.GetValueAsync(key, defaultValue, crypt, converter, sections, token);
        }

        public Boolean KeyExist(String key, IEnumerable<String> sections)
        {
            return Config.KeyExist(key, sections);
        }

        public Task<Boolean> KeyExistAsync(String key, IEnumerable<String> sections)
        {
            return Config.KeyExistAsync(key, sections);
        }

        public Task<Boolean> KeyExistAsync(String key, IEnumerable<String> sections, CancellationToken token)
        {
            return Config.KeyExistAsync(key, sections, token);
        }

        public T GetValue<T>(IReadOnlyConfigProperty<T> property)
        {
            return Config.GetValue(property);
        }

        public Boolean KeyExist(IReadOnlyConfigPropertyBase property)
        {
            return Config.KeyExist(property);
        }

        public Boolean SetValue<T>(IReadOnlyConfigProperty<T> property, T value)
        {
            return Config.SetValue(property, value);
        }

        public T GetOrSetValue<T>(IReadOnlyConfigProperty<T> property)
        {
            return Config.GetOrSetValue(property);
        }

        public T GetOrSetValue<T>(IReadOnlyConfigProperty<T> property, T value)
        {
            return Config.GetOrSetValue(property, value);
        }

        public Boolean RemoveValue(IReadOnlyConfigPropertyBase property)
        {
            return Config.RemoveValue(property);
        }
        
        public IStringLocalizationProperty GetProperty(String key, IEnumerable<String> sections)
        {
            return GetProperty(key, default, sections);
        }

        public IStringLocalizationProperty GetProperty(String key, IString value, IEnumerable<String> sections)
        {
            return new SubLocalizationPropertyAdapter(Config.GetProperty(key, value?.ToString(), sections));
        }

        public void ReadProperties()
        {
            Config.ReadProperties();
        }

        public void SaveProperties()
        {
            Config.SaveProperties();
        }

        public void ResetProperties()
        {
            Config.ResetProperties();
        }

        public void ClearProperties()
        {
            Config.ClearProperties();
        }
        
        public void Dispose()
        {
            Config.Dispose();
        }
    }
}