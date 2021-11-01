// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using NetExtender.Configuration.Common;
using NetExtender.Configuration.Property.Interfaces;
using NetExtender.Configuration.Property.Interfaces.Common;
using NetExtender.Crypto;
using NetExtender.Crypto.CryptKey.Interfaces;
using NetExtender.Utilities.Types;

namespace NetExtender.Configuration.Property
{
    public abstract class ConfigPropertyBase : IConfigPropertyBase
    {
        public static String GetPath(String key, IEnumerable<String> sections)
        {
            if (key is null)
            {
                throw new ArgumentNullException(nameof(key));
            }

            return String.Join("\\", sections.Append(key));
        }
        
        public String Path
        {
            get
            {
                return GetPath(Key, Sections);
            }
        }

        public IPropertyConfigBase Config { get; }
        public String Key { get; }
        public IImmutableList<String> Sections { get; }

        public CryptAction Crypt
        {
            get
            {
                return CryptKey.Crypt;
            }
            set
            {
                CryptKey.Crypt = value;
            }
        }

        public ICryptKey CryptKey { get; set; }
        
        public ConfigPropertyOptions Options { get; set; }
        
        public Boolean Caching
        {
            get
            {
                return Options.HasFlag(ConfigPropertyOptions.Caching);
            }
            set
            {
                if (value)
                {
                    Options |= ConfigPropertyOptions.Caching;
                }
                else
                {
                    Options &= ~ConfigPropertyOptions.Caching;
                }
            }
        }

        public Boolean IsReadOnly
        {
            get
            {
                return Options.HasFlag(ConfigPropertyOptions.ReadOnly);
            }
            set
            {
                if (value)
                {
                    Options |= ConfigPropertyOptions.ReadOnly;
                }
                else
                {
                    Options &= ~ConfigPropertyOptions.ReadOnly;
                }
            }
        }

        public Boolean AlwaysDefault
        {
            get
            {
                return Options.HasFlag(ConfigPropertyOptions.AlwaysDefault);
            }
            set
            {
                if (value)
                {
                    Options |= ConfigPropertyOptions.AlwaysDefault;
                }
                else
                {
                    Options &= ~ConfigPropertyOptions.AlwaysDefault;
                }
            }
        }
        
        public Boolean DisableSave
        {
            get
            {
                return Options.HasFlag(ConfigPropertyOptions.DisableSave);
            }
            set
            {
                if (value)
                {
                    Options |= ConfigPropertyOptions.DisableSave;
                }
                else
                {
                    Options &= ~ConfigPropertyOptions.DisableSave;
                }
            }
        }

        private Boolean _disposed;

        protected ConfigPropertyBase(IPropertyConfigBase config, String key, ICryptKey cryptkey, ConfigPropertyOptions options, IEnumerable<String> sections)
        {
            Config = config;
            Key = key ?? throw new ArgumentNullException(nameof(key));
            CryptKey = cryptkey;
            Sections = sections.AsIImmutableList();
            Options = options;
        }

        public abstract void Read();

        public abstract void Save();

        public abstract void Reset();

        public abstract Boolean KeyExist();

        public void Dispose()
        {
            if (_disposed)
            {
                return;
            }

            Dispose(true);
            GC.SuppressFinalize(this);
            _disposed = true;
        }

        protected virtual void Dispose(Boolean disposing)
        {
            if (disposing)
            {
                ConfigurationPropertyObserver.RemoveProperty(this);
            }
        }
    }
}