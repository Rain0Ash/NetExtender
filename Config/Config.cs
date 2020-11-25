// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Data;
using System.Globalization;
using System.Linq;
using NetExtender.Apps.Domains;
using NetExtender.Config.Common;
using NetExtender.Config.INI;
using NetExtender.Config.Interfaces;
using NetExtender.Config.JSON;
using NetExtender.Config.RAM;
using NetExtender.Config.REG;
using NetExtender.Config.XML;
using NetExtender.Converters;
using NetExtender.Crypto;
using NetExtender.Crypto.CryptKey;
using NetExtender.Crypto.CryptKey.Interfaces;
using NetExtender.Types.Dictionaries;
using NetExtender.Utils.IO;
using NetExtender.Utils.Types;

// ReSharper disable HeuristicUnreachableCode
// ReSharper disable ConstantNullCoalescingCondition

namespace NetExtender.Config
{
    public abstract partial class Config : IPropertyConfig
    {
        protected const String DefaultName = "config";

        private static readonly IndexDictionary<Config, IndexDictionary<String, IConfigPropertyBase>> Properties =
            new IndexDictionary<Config, IndexDictionary<String, IConfigPropertyBase>>();
        
        public static Config Create(ConfigType type = ConfigType.Registry, ConfigOptions options = ConfigOptions.None)
        {
            return Create(null, type, options);
        }
        
        public static Config Create(String path, ConfigType type = ConfigType.Registry, ConfigOptions options = ConfigOptions.None)
        {
            return type switch
            {
                ConfigType.Registry => new REGConfig(path, options),
                ConfigType.INI => new INIConfig(path, options),
                ConfigType.XML => new XMLConfig(path, options),
                ConfigType.RAM => new RAMConfig(path, options),
                ConfigType.JSON => new JSONConfig(path, options),
                _ => throw new NotSupportedException()
            };
        }
        
        private static String GetDefaultPath(String extension)
        {
            if (String.IsNullOrWhiteSpace(extension))
            {
                throw new ArgumentException(@"Value cannot be null or whitespace.", nameof(extension));
            }

            return System.IO.Path.Combine(Domain.Directory, $"{DefaultName}.{extension}");
        }
        
        protected static String ValidatePathOrGetDefault(String path, String extension)
        {
            return !String.IsNullOrWhiteSpace(path) && PathUtils.IsValidFilePath(path) ? path : GetDefaultPath(extension);
        }

        public String Path { get; }
        public ICryptKey Crypt { get; }
        public ConfigOptions Options { get; }

        public Boolean IsReadOnly
        {
            get
            {
                return Options.HasFlag(ConfigOptions.ReadOnly);
            }
        }
        
        public Boolean IsCryptData
        {
            get
            {
                return Options.HasFlag(ConfigOptions.CryptData);
            }
        }
        
        public Boolean IsCryptConfig
        {
            get
            {
                return Options.HasFlag(ConfigOptions.CryptConfig);
            }
        }
        
        public Boolean IsCryptAll
        {
            get
            {
                return Options.HasFlag(ConfigOptions.CryptAll);
            }
        }

        public Boolean ThrowOnReadOnly { get; set; } = true;

        public Boolean CryptByDefault { get; set; }
        
        public ConfigPropertyOptions DefaultOptions { get; set; } = ConfigPropertyOptions.Caching;

        protected Config(String path, ConfigOptions options)
            : this(path, null, options)
        {
        }
        
        protected Config(String path, ICryptKey crypt, ConfigOptions options)
        {
            if (String.IsNullOrEmpty(path))
            {
                throw new ArgumentException(@"Not null or empty", nameof(path));
            }
            
            Path = path;
            Crypt = crypt ?? CryptKey.Create(CryptAction.Crypt);
            Options = options;
        }

        protected virtual String ConvertToValue<T>(T value)
        {
            return value.GetString();
        }

        public void SetValue<T>(String key, T value, params String[] sections)
        {
            this[key, sections] = ConvertToValue(value);
        }

        public void SetValue<T>(String key, T value, ICryptKey crypt, params String[] sections)
        {
            crypt ??= Crypt;
            
            if (crypt.IsEncrypt)
            {
                SetValue(key, crypt.Encrypt(ConvertToValue(value)), sections);
                return;
            }

            SetValue(key, value, sections);
        }

        protected virtual T ConvertFromValue<T>(String value)
        {
            return value.Convert<T>();
        }

        public String GetValue(String key, params String[] sections)
        {
            return this[key, sections];
        }

        public String GetValue(String key, String defaultValue, params String[] sections)
        {
            return GetValue(key, sections) ?? defaultValue;
        }

        public String GetValue(String key, Object defaultValue, params String[] sections)
        {
            return GetValue(key, Convert.ToString(defaultValue, CultureInfo.InvariantCulture), sections);
        }

        public String GetValue(String key, Object defaultValue, Boolean decrypt, params String[] sections)
        {
            return GetValue(key, defaultValue, decrypt, null, sections);
        }

        public String GetValue(String key, Object defaultValue, Boolean decrypt, ICryptKey crypt, params String[] sections)
        {
            crypt ??= Crypt;
            
            String value = GetValue(key, defaultValue, sections);

            if (!decrypt || value is null)
            {
                return value;
            }

            return crypt.Decrypt(value) ?? value;
        }

        public T GetValue<T>(String key, params String[] sections)
        {
            return ConvertFromValue<T>(this[key, sections]);
        }

        public T GetValue<T>(String key, T defaultValue, params String[] sections)
        {
            return GetValue(key, defaultValue, null, sections);
        }
        
        public T GetValue<T>(String key, T defaultValue, ICryptKey crypt, params String[] sections)
        {
            return GetValue(key, defaultValue, crypt, ConvertUtils.TryConvert, sections);
        }

        public T GetValue<T>(String key, T defaultValue, ICryptKey crypt, TryConverter<String, T> converter, params String[] sections)
        {
            crypt ??= Crypt;
            converter ??= ConvertUtils.TryConvert;
            
            String value = GetValue(key, defaultValue.GetString(CultureInfo.InvariantCulture), sections);
            T cval;

            if (!crypt.IsDecrypt || value is null)
            {
                return converter(value, out cval) ? cval : defaultValue;
            }

            return converter(crypt.Decrypt(value) ?? value, out cval) ? cval : defaultValue;
        }

        public String GetOrSetValue(String key, Object defaultValue, params String[] sections)
        {
            return GetOrSetValue(key, defaultValue, CryptAction.Decrypt, sections);
        }

        public String GetOrSetValue(String key, Object defaultValue, CryptAction crypt, params String[] sections)
        {
            return GetOrSetValue(key, defaultValue, crypt, null, sections);
        }

        public String GetOrSetValue(String key, Object defaultValue, CryptAction crypt, ICryptKey cryptKey, params String[] sections)
        {
            cryptKey ??= Crypt;
            
            String value = GetValue(key, sections);

            if (value is not null)
            {
                return crypt.HasFlag(CryptAction.Decrypt) ? cryptKey.Decrypt(value) ?? value : value;
            }

            SetValue(key, defaultValue, cryptKey, sections);
            return Convert.ToString(defaultValue, CultureInfo.InvariantCulture);
        }

        public T GetOrSetValue<T>(String key, T defaultValue, params String[] sections)
        {
            return GetOrSetValue(key, defaultValue, null, sections);
        }
        
        public T GetOrSetValue<T>(String key, T defaultValue, ICryptKey crypt, params String[] sections)
        {
            return GetOrSetValue(key, defaultValue, crypt, ConvertUtils.TryConvert, sections);
        }

        public T GetOrSetValue<T>(String key, T defaultValue, ICryptKey crypt, TryConverter<String, T> converter, params String[] sections)
        {
            crypt ??= Crypt;
            converter ??= ConvertUtils.TryConvert;

            String value = GetValue(key, sections);

            if (value is not null)
            {
                if (crypt.IsDecrypt)
                {
                    value = crypt.Decrypt(value) ?? value;
                }

                return converter(value, out T cval) ? cval : defaultValue;
            }

            SetValue(key, defaultValue, crypt, sections);
            return defaultValue;
        }

        public Boolean KeyExist(String key, params String[] sections)
        {
            return GetValue(key, sections) is not null;
        }

        public void RemoveValue(String key, params String[] sections)
        {
            SetValue<String>(key, null, sections);
        }

        protected abstract String Get(String key, params String[] sections);
        protected abstract Boolean Set(String key, String value, params String[] sections);

        public String this[String key, params String[] sections]
        {
            get
            {
                return IsCryptData ? Get(Crypt.Encrypt(key), Crypt.Encrypt(sections).ToArray()) : Get(key, sections);
            }
            set
            {
                if (CheckReadOnly())
                {
                    return;
                }

                if (IsCryptData)
                {
                    Set(Crypt.Encrypt(key), Crypt.Encrypt(value), Crypt.Encrypt(sections).ToArray());
                    return;
                }
                
                Set(key, value, sections);
            }
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

        public virtual void Dispose()
        {
            ClearProperties();
            Crypt.Dispose();
        }
    }
}