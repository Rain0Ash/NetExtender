// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Collections.Generic;
using NetExtender.Apps.Domains;
using NetExtender.Configuration.Common;
using NetExtender.Registry;
using NetExtender.Registry.Interfaces;
using NetExtender.Utils.IO;
using NetExtender.Utils.OS;

namespace NetExtender.Configuration.Registry
{
    public sealed class RegistryConfigBehavior : ConfigBehavior
    {
        private IRegistry Registry { get; }

        public RegistryKeys RegistryKey
        {
            get
            {
                return Registry.Key;
            }
        }

        public RegistryConfigBehavior(String path = null, ConfigOptions options = ConfigOptions.None)
            : base(String.IsNullOrEmpty(path) ? $"Software\\{Domain.AppNameOrFriendlyName}" : path, options)
        {
            Registry = RegistryKeys.CurrentUser.Create(Path.Split(PathUtils.Separators), !IsReadOnly);
        }

        public RegistryConfigBehavior(RegistryKeys key, String path, ConfigOptions options = ConfigOptions.None)
            : base(path, options)
        {
            Registry = key.Create(Path.Split(PathUtils.Separators), !IsReadOnly);
        }

        public override String Get(String key, IEnumerable<String> sections)
        {
            return Registry.GetValue(key, sections);
        }

        public override Boolean Set(String key, String value, IEnumerable<String> sections)
        {
            Registry.SetValue(key, value, sections);
            return true;
        }
    }
}