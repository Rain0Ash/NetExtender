// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.IO;
using System.Linq;
using NetExtender.Utils.Types;
using Microsoft.Win32;
using NetExtender.Registry.Interfaces;
using NetExtender.Utils.IO;

namespace NetExtender.Registry
{
    [System.Diagnostics.CodeAnalysis.SuppressMessage("ReSharper", "PossibleMultipleEnumeration")]
    public sealed class Registry : IRegistry
    {
        private static IDictionary<RegistryKeys, RegistryKey> Keys { get; } = new Dictionary<RegistryKeys, RegistryKey>
        {
            {RegistryKeys.CurrentUser, Microsoft.Win32.Registry.CurrentUser},
            {RegistryKeys.CurrentConfig, Microsoft.Win32.Registry.CurrentConfig},
            {RegistryKeys.LocalMachine, Microsoft.Win32.Registry.LocalMachine},
            {RegistryKeys.Users, Microsoft.Win32.Registry.Users},
            {RegistryKeys.ClassesRoot, Microsoft.Win32.Registry.ClassesRoot},
            {RegistryKeys.PerformanceData, Microsoft.Win32.Registry.PerformanceData}
        }.ToImmutableDictionary();

        public const String Separator = "\\";

        public Boolean IsReadOnly { get; }

        public String Path { get; }
        public RegistryKeys RegistryKey { get; }

        public Registry(String path, RegistryKeys key, FileAccess access)
        {
            Path = path ?? throw new ArgumentNullException(nameof(path));
            RegistryKey = key;
            IsReadOnly = access.IsReadOnly();
        }

        private RegistryKey GetRegistryKey(FileAccess access, IEnumerable<String> sections)
        {
            RegistryKey reg = Keys[RegistryKey];
            String path = $"{Path}\\{sections.Join("\\")}";

            try
            {
                return access.HasFlag(FileAccess.Write) ? reg.CreateSubKey(path) : reg.OpenSubKey(path);
            }
            catch (Exception)
            {
                reg.Dispose();
                return null;
            }
        }

        private RegistryKey GetReadRegistryKey(IEnumerable<String> sections)
        {
            return GetRegistryKey(FileAccess.Read, sections);
        }

        private RegistryKey GetWriteRegistryKey(IEnumerable<String> sections)
        {
            return GetRegistryKey(FileAccess.ReadWrite, sections);
        }
        
        public String GetValue(String key, params String[] sections)
        {
            return GetValue(key, (IEnumerable<String>) sections);
        }

        public String GetValue(String key, IEnumerable<String> sections)
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

        public Boolean SetValue(String key, String value, params String[] sections)
        {
            return SetValue(key, value, (IEnumerable<String>) sections);
        }
        
        public Boolean SetValue(String key, String value, IEnumerable<String> sections)
        {
            try
            {
                if (GetValue(key, sections) == value)
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
        
        public String[] GetValueNames(params String[] sections)
        {
            return GetValueNames((IEnumerable<String>) sections);
        }

        public String[] GetValueNames(IEnumerable<String> sections)
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

        public String[] GetSubKeyNames(params String[] sections)
        {
            return GetSubKeyNames((IEnumerable<String>) sections);
        }
        
        public String[] GetSubKeyNames(IEnumerable<String> sections)
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

        private IEnumerable<RegistryKey> GetSubRegistryKeys(Boolean recursive, IEnumerable<String> sections)
        {
            using RegistryKey key = GetReadRegistryKey(sections);

            if (key is null)
            {
                yield break;
            }
            
            foreach (String subkey in key.GetSubKeyNames())
            {
                using RegistryKey iterkey = GetReadRegistryKey(sections.Append(subkey));
                
                if (iterkey is not null)
                {
                    yield return iterkey;
                }

                if (!recursive)
                {
                    continue;
                }

                foreach (RegistryKey subiterkey in GetSubRegistryKeys(true, sections.Append(subkey)))
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
            return KeyExist(key, (IEnumerable<String>) sections);
        }
        
        public Boolean KeyExist(String key, IEnumerable<String> sections)
        {
            return GetValue(key, sections) is not null;
        }

        public Boolean RemoveKey(String key, params String[] sections)
        {
            return RemoveKey(key, (IEnumerable<String>) sections);
        }
        
        public Boolean RemoveKey(String key, IEnumerable<String> sections)
        {
            try
            {
                using RegistryKey reg = GetReadRegistryKey(sections);

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