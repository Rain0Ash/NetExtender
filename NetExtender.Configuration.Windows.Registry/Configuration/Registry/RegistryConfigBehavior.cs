// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Collections.Generic;
using System.Linq;
using NetExtender.Configuration.Behavior;
using NetExtender.Configuration.Common;
using NetExtender.Registry;
using NetExtender.Registry.Interfaces;
using NetExtender.Utilities.Application;
using NetExtender.Utilities.IO;
using NetExtender.Utilities.Registry;
using NetExtender.Utilities.Types;

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
            : this(null, ConfigOptions.None)
        {
        }
        
        public RegistryConfigBehavior(ConfigOptions options)
            : this(null, options)
        {
        }
        
        public RegistryConfigBehavior(String? path)
            : this(path, ConfigOptions.None)
        {
        }

        public RegistryConfigBehavior(String? path, ConfigOptions options)
            : this(RegistryKeys.CurrentUser, path, options)
        {
        }
        
        public RegistryConfigBehavior(RegistryKeys key)
            : this(key, null, ConfigOptions.None)
        {
        }

        public RegistryConfigBehavior(RegistryKeys key, ConfigOptions options)
            : this(key, null, options)
        {
        }
        
        public RegistryConfigBehavior(RegistryKeys key, String? path)
            : this(key, path, ConfigOptions.None)
        {
        }
        
        public RegistryConfigBehavior(RegistryKeys key, String? path, ConfigOptions options)
            : base(GetDefaultPath(path), options)
        {
            Registry = key.Create(Path.Split(PathUtilities.Separators), !IsReadOnly);
        }

        public override String? Get(String? key, IEnumerable<String>? sections)
        {
            return Registry.GetValue(key, sections);
        }

        public override Boolean Set(String? key, String? value, IEnumerable<String>? sections)
        {
            if (IsReadOnly || Registry.IsReadOnly)
            {
                return false;
            }
            
            if (IsIgnoreEvent && !IsLazyWrite)
            {
                return Registry.SetValue(key, value, sections);
            }
            
            sections = ToSection(sections).AsIImmutableList();

            if (IsLazyWrite && Get(key, sections) == value)
            {
                return true;
            }
            
            if (!Registry.SetValue(key, value, sections))
            {
                return false;
            }

            OnChanged(new ConfigurationValueEntry(key, value, sections));
            return true;
        }

        public override ConfigurationEntry[]? GetExists()
        {
            return Registry.Dump()?.Select(item => new ConfigurationEntry(item.Name, item.Sections)).ToArray();
        }
        
        public override ConfigurationValueEntry[]? GetExistsValues()
        {
            return Registry.Dump()?.Select(item => new ConfigurationValueEntry(item.Name, Get(item.Name, item.Sections), item.Sections)).ToArray();
        }

        public override Boolean Reload()
        {
            return false;
        }
    }
}