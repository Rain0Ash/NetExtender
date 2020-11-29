// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Linq;
using NetExtender.Crypto;
using NetExtender.Crypto.CryptKey.Interfaces;
using ReactiveUI;
using ReactiveUI.Fody.Helpers;

namespace NetExtender.Config
{
    public abstract class ConfigPropertyBase : ReactiveObject, IConfigPropertyBase
    {
        public String Path
        {
            get
            {
                return String.Join("\\", Sections.Append(Key));
            }
        }

        public Config Config { get; }
        public String Key { get; }
        public String[] Sections { get; }

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

        [Reactive]
        public ICryptKey CryptKey { get; set; }
        
        [Reactive]
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

        protected ConfigPropertyBase(Config config, String key, ICryptKey cryptKey, ConfigPropertyOptions options, params String[] sections)
        {
            Config = config;
            Key = key;
            CryptKey = cryptKey;
            Sections = sections;
            Options = options;
        }

        public abstract void Read();

        public abstract void Save();

        public abstract void Reset();

        public Boolean KeyExist()
        {
            return Config.KeyExist(Key, Sections);
        }

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

        protected void Dispose(Boolean disposing)
        {
            if (disposing)
            {
                Config.RemoveProperty(this);
            }
        }
    }
}