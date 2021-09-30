// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using NetExtender.Configuration.Behavior.Interfaces;
using NetExtender.Configuration.Common;
using NetExtender.Crypto;
using NetExtender.Crypto.CryptKey;
using NetExtender.Crypto.CryptKey.Interfaces;
using NetExtender.Utilities.Application;
using NetExtender.Utilities.IO;
using NetExtender.Utilities.Types;

namespace NetExtender.Configuration.Behavior
{
    public abstract class ConfigBehavior : IConfigBehavior
    {
        protected const String DefaultName = "config";
        
        private static String GetDefaultPath(String? extension)
        {
            String filename = DefaultName;
            if (!String.IsNullOrEmpty(extension))
            {
                filename += extension.StartsWith('.') ? extension : '.' + extension;
            }

            return System.IO.Path.Combine(ApplicationUtilities.Directory ?? Environment.CurrentDirectory, filename);
        }
        
        protected static String ValidatePathOrGetDefault(String? path, String? extension)
        {
            return !String.IsNullOrWhiteSpace(path) && PathUtilities.IsValidFilePath(path) ? path : GetDefaultPath(extension);
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
        
        protected ConfigBehavior(String path, ICryptKey? crypt, ConfigOptions options)
        {
            if (String.IsNullOrEmpty(path))
            {
                throw new ArgumentException(@"Not null or empty", nameof(path));
            }
            
            Path = path;
            Crypt = crypt ?? CryptKey.Create(CryptAction.Crypt);
            Options = options;
        }
        
        public virtual String? ConvertToValue<T>(T value)
        {
            return value.GetString();
        }

        public virtual T? ConvertFromValue<T>(String? value)
        {
            return value.Convert<T>();
        }
        
        public abstract String? Get(String? key, IEnumerable<String>? sections);
        public abstract Boolean Set(String? key, String? value, IEnumerable<String>? sections);
        
        public virtual Task<String?> GetAsync(String? key, IEnumerable<String>? sections, CancellationToken token)
        {
            String? result = Get(key, sections);
            return Task.FromResult(result);
        }
        
        public virtual Task<Boolean> SetAsync(String? key, String? value, IEnumerable<String>? sections, CancellationToken token)
        {
            Boolean result = Set(key, value, sections);
            return result.ToTask();
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
                Crypt.Dispose();
            }
            
            Dispose(disposing);
            _disposed = true;
        }
    }
}