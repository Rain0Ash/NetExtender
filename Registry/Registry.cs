// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Collections.Generic;
using System.Linq;
using NetExtender.Utils.Types;
using Microsoft.Win32;
using NetExtender.Apps.Domains;

namespace NetExtender.Registry
{
    public sealed class Registry
    {
        private static readonly IReadOnlyDictionary<RegistryKeys, RegistryKey> Keys = new Dictionary<RegistryKeys, RegistryKey>
        {
            {RegistryKeys.CurrentUser, Microsoft.Win32.Registry.CurrentUser},
            {RegistryKeys.CurrentConfig, Microsoft.Win32.Registry.CurrentConfig},
            {RegistryKeys.LocalMachine, Microsoft.Win32.Registry.LocalMachine},
            {RegistryKeys.Users, Microsoft.Win32.Registry.Users},
            {RegistryKeys.ClassesRoot, Microsoft.Win32.Registry.ClassesRoot},
            {RegistryKeys.PerformanceData, Microsoft.Win32.Registry.PerformanceData}
        };

        public Boolean IsReadOnly { get; set; } = true;

        public String Path { get; }
        public RegistryKeys RegistryKey { get; }

        public Registry(String path = null, RegistryKeys key = RegistryKeys.CurrentUser)
        {
            Path = path ?? $"Software\\{Domain.Current.AppName}";
            RegistryKey = key;
        }

        private RegistryKey GetRegistryKey(Boolean write, params String[] sections)
        {
            RegistryKey reg = Keys[RegistryKey];
            String path = $"{Path}\\{sections.Join("\\")}";

            try
            {
                return write ? reg.CreateSubKey(path) : reg.OpenSubKey(path);
            }
            catch (Exception)
            {
                return null;
            }
        }

        private RegistryKey GetReadRegistryKey(params String[] sections)
        {
            return GetRegistryKey(false, sections);
        }

        private RegistryKey GetWriteRegistryKey(params String[] sections)
        {
            return GetRegistryKey(true, sections);
        }

        public Boolean SetValue(String key, String value, params String[] sections)
        {
            try
            {
                if (GetValue(key) == value)
                {
                    return true;
                }

                if (IsReadOnly)
                {
                    return false;
                }

                if (value is null)
                {
                    return RemoveKey(key, sections);
                }

                using RegistryKey reg = GetWriteRegistryKey(sections);

                if (reg is null)
                {
                    return false;
                }

                reg.SetValue(key, value);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public String GetValue(String key, params String[] sections)
        {
            try
            {
                using RegistryKey reg = GetReadRegistryKey(sections);

                return reg?.GetValue(key)?.ToString();
            }
            catch (Exception)
            {
                return null;
            }
        }

        public String[] GetValueNames(String[] sections)
        {
            try
            {
                using RegistryKey reg = GetReadRegistryKey(sections);

                return reg?.GetValueNames();
            }
            catch (Exception)
            {
                return null;
            }
        }

        public String[] GetSubKeyNames(String[] sections)
        {
            try
            {
                using RegistryKey reg = GetReadRegistryKey(sections);

                return reg?.GetSubKeyNames();
            }
            catch (Exception)
            {
                return null;
            }
        }

        private IEnumerable<RegistryKey> GetSubRegistryKeys(Boolean recursive, params String[] sections)
        {
            using RegistryKey key = GetReadRegistryKey(sections);

            if (key is null)
            {
                yield break;
            }
            
            foreach (String subkey in key.GetSubKeyNames())
            {
                using RegistryKey iterkey = GetReadRegistryKey(sections.Append(subkey).ToArray());
                
                if (iterkey is not null)
                {
                    yield return iterkey;
                }

                if (!recursive)
                {
                    continue;
                }

                foreach (RegistryKey subiterkey in GetSubRegistryKeys(true, sections.Append(subkey).ToArray()))
                {
                    if (subiterkey is not null)
                    {
                        yield return subiterkey;
                    }
                }
            }
        }

        public Boolean KeyExist(String key, params String[] sections)
        {
            return GetValue(key, sections) is not null;
        }

        public Boolean RemoveKey(String key, params String[] sections)
        {
            try
            {
                using RegistryKey reg = GetRegistryKey(false, sections);

                if (reg?.GetValue(key) is null)
                {
                    return true;
                }

                if (IsReadOnly)
                {
                    return false;
                }

                reg.DeleteValue(key);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}