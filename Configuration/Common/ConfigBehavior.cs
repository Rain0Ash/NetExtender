// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using NetExtender.Apps.Domains;
using NetExtender.Configuration.Ini;
using NetExtender.Configuration.Json;
using NetExtender.Configuration.Ram;
using NetExtender.Configuration.Registry;
using NetExtender.Configuration.Xml;
using NetExtender.Crypto;
using NetExtender.Crypto.CryptKey;
using NetExtender.Crypto.CryptKey.Interfaces;
using NetExtender.Utils.IO;
using NetExtender.Utils.Types;

namespace NetExtender.Configuration.Common
{
    public abstract class ConfigBehavior : IConfigBehavior
    {
        public static IConfigBehavior Create(String path)
        {
            return Create(path, Config.DefaultConfigType);
        }
        
        public static IConfigBehavior Create(String path, ConfigType type)
        {
            return Create(path, type, Config.DefaultConfigOptions);
        }
        
        public static IConfigBehavior Create(String path, ConfigOptions options)
        {
            return Create(path, Config.DefaultConfigType, options);
        }

        public static IConfigBehavior Create(String path, ConfigType type, ConfigOptions options)
        {
            return type switch
            {
                ConfigType.Registry => new RegistryConfigBehavior(path, options),
                ConfigType.INI => new IniConfigBehavior(path, options),
                ConfigType.XML => new XmlConfigBehavior(path, options),
                ConfigType.RAM => new RamConfigBehavior(path, options),
                ConfigType.JSON => new JsonConfigBehavior(path, options),
                ConfigType.SQL => throw new NotImplementedException(),
                _ => throw new NotSupportedException()
            };
        }
        
        protected const String DefaultName = "config";
        
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
        
        protected ConfigBehavior(String path, ConfigOptions options)
            : this(path, null, options)
        {
        }
        
        protected ConfigBehavior([JetBrains.Annotations.NotNull] String path, ICryptKey crypt, ConfigOptions options)
        {
            if (String.IsNullOrEmpty(path))
            {
                throw new ArgumentException(@"Not null or empty", nameof(path));
            }
            
            Path = path;
            Crypt = crypt ?? CryptKey.Create(CryptAction.Crypt);
            Options = options;
        }
        
        public virtual String ConvertToValue<T>(T value)
        {
            return value.GetString();
        }

        public virtual T ConvertFromValue<T>(String value)
        {
            return value.Convert<T>();
        }
        
        public abstract String Get(String key, IEnumerable<String> sections);
        public abstract Boolean Set(String key, String value, IEnumerable<String> sections);
        
        // ReSharper disable once UnusedParameter.Global
        public virtual Task<String> GetAsync(String key, IEnumerable<String> sections, CancellationToken token)
        {
            String result = Get(key, sections);
            return Task.FromResult(result);
        }
        
        // ReSharper disable once UnusedParameter.Global
        public virtual Task<Boolean> SetAsync(String key, String value, IEnumerable<String> sections, CancellationToken token)
        {
            Boolean result = Set(key, value, sections);
            return result.ToTask();
        }

        private Boolean _disposed;
        
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void DisposeInternal(Boolean disposing)
        {
        }
        
        public void Dispose(Boolean disposing)
        {
            if (_disposed)
            {
                return;
            }

            if (disposing)
            {
                Crypt.Dispose();
            }
            
            DisposeInternal(disposing);

            _disposed = true;
        }
    }
}