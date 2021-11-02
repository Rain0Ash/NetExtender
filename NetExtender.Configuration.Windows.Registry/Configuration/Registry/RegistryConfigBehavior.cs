// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Collections.Generic;
using System.Linq;
using NetExtender.Configuration.Behavior;
using NetExtender.Configuration.Common;
using NetExtender.Crypto.CryptKey.Interfaces;
using NetExtender.Registry;
using NetExtender.Registry.Interfaces;
using NetExtender.Utilities.Application;
using NetExtender.Utilities.IO;
using NetExtender.Utilities.Registry;

namespace NetExtender.Configuration.Windows.Registry
{
    public class RegistryConfigBehavior : ConfigBehavior
    {
        protected IRegistry Registry { get; }

        public RegistryKeys RegistryKey
        {
            get
            {
                return Registry.Key;
            }
        }

        protected static String GetDefaultPath(String? path)
        {
            return String.IsNullOrEmpty(path) ? $"Software\\{ApplicationUtilities.FriendlyName}" : path;
        }
        
        public RegistryConfigBehavior()
            : this(null, null, ConfigOptions.None)
        {
        }
        
        public RegistryConfigBehavior(ConfigOptions options)
            : this(null, null, options)
        {
        }
        
        public RegistryConfigBehavior(ICryptKey? crypt)
            : this(null, crypt, ConfigOptions.None)
        {
        }
        
        public RegistryConfigBehavior(ICryptKey? crypt, ConfigOptions options)
            : this(null, crypt, options)
        {
        }
        
        public RegistryConfigBehavior(String? path)
            : this(path, null, ConfigOptions.None)
        {
        }
        
        public RegistryConfigBehavior(String? path, ConfigOptions options)
            : this(path, null, options)
        {
        }
        
        public RegistryConfigBehavior(String? path, ICryptKey? crypt)
            : this(path, crypt, ConfigOptions.None)
        {
        }

        public RegistryConfigBehavior(String? path, ICryptKey? crypt, ConfigOptions options)
            : this(RegistryKeys.CurrentUser, path, crypt, options)
        {
        }
        
        public RegistryConfigBehavior(RegistryKeys key)
            : this(key, null, null, ConfigOptions.None)
        {
        }
        
        public RegistryConfigBehavior(RegistryKeys key, ConfigOptions options)
            : this(key, null, null, options)
        {
        }
        
        public RegistryConfigBehavior(RegistryKeys key, ICryptKey? crypt)
            : this(key, null, crypt, ConfigOptions.None)
        {
        }

        public RegistryConfigBehavior(RegistryKeys key, ICryptKey? crypt, ConfigOptions options)
            : this(key, null, crypt, options)
        {
        }
        
        public RegistryConfigBehavior(RegistryKeys key, String? path)
            : this(key, path, null, ConfigOptions.None)
        {
        }
        
        public RegistryConfigBehavior(RegistryKeys key, String? path, ConfigOptions options)
            : this(key, path, null, options)
        {
        }
        
        public RegistryConfigBehavior(RegistryKeys key, String? path, ICryptKey? crypt)
            : this(key, path, crypt, ConfigOptions.None)
        {
        }
        
        public RegistryConfigBehavior(RegistryKeys key, String? path, ICryptKey? crypt, ConfigOptions options)
            : base(GetDefaultPath(path), crypt, options)
        {
            Registry = key.Create(Path.Split(PathUtilities.Separators), !IsReadOnly);
        }

        public override String? Get(String? key, IEnumerable<String>? sections)
        {
            return Registry.GetValue(key, sections);
        }

        public override Boolean Set(String? key, String? value, IEnumerable<String>? sections)
        {
            Registry.SetValue(key, value, sections);
            return true;
        }

        public override ConfigurationEntry[]? GetExists()
        {
            return Registry.Dump()?.Select(item => new ConfigurationEntry(item.Name, item.Sections)).ToArray();
        }

        public override Boolean Reload()
        {
            return false;
        }
    }
}