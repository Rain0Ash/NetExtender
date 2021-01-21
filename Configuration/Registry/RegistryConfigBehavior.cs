// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using NetExtender.Apps.Domains;
using NetExtender.Configuration.Common;
using NetExtender.Registry;

namespace NetExtender.Configuration.Registry
{
    public sealed class RegistryConfigBehavior : ConfigBehavior
    {
        private readonly NetExtender.Registry.Registry _registry;

        public RegistryKeys RegistryKey
        {
            get
            {
                return _registry.RegistryKey;
            }
        }

        public RegistryConfigBehavior(String path = null, ConfigOptions options = ConfigOptions.None)
            : base(String.IsNullOrEmpty(path) ? $"Software\\{Domain.Current.AppName}" : path, options)
        {
            _registry = new NetExtender.Registry.Registry(Path)
            {
                IsReadOnly = IsReadOnly
            };
        }

        public RegistryConfigBehavior(RegistryKeys key, String path, ConfigOptions options = ConfigOptions.None)
            : base(path, options)
        {
            _registry = new NetExtender.Registry.Registry(Path, key)
            {
                IsReadOnly = IsReadOnly
            };
        }

        public override String Get(String key, params String[] sections)
        {
            return _registry.GetValue(key, sections);
        }

        public override Boolean Set(String key, String value, params String[] sections)
        {
            _registry.SetValue(key, value, sections);
            return true;
        }
    }
}