// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using NetExtender.Apps.Domains;
using NetExtender.Config.Common;
using NetExtender.Registry;

namespace NetExtender.Config.REG
{
    public sealed class REGConfig : Config
    {
        private readonly Registry.Registry _registry;

        public RegistryKeys RegistryKey
        {
            get
            {
                return _registry.RegistryKey;
            }
        }

        public REGConfig(String path = null, ConfigOptions options = ConfigOptions.None)
            : base(String.IsNullOrEmpty(path) ? $"Software\\{Domain.Current.AppName}" : path, options)
        {
            _registry = new Registry.Registry(Path)
            {
                IsReadOnly = IsReadOnly
            };
        }

        public REGConfig(RegistryKeys key, String path, ConfigOptions options = ConfigOptions.None)
            : base(path, options)
        {
            _registry = new Registry.Registry(Path, key)
            {
                IsReadOnly = IsReadOnly
            };
        }

        protected override String Get(String key, params String[] sections)
        {
            return _registry.GetValue(key, sections);
        }

        protected override Boolean Set(String key, String value, params String[] sections)
        {
            _registry.SetValue(key, value, sections);
            return true;
        }
    }
}