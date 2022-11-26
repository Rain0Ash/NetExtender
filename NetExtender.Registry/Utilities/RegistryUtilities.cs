// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.IO;
using Microsoft.Win32;
using NetExtender.Registry;
using NetExtender.Registry.Interfaces;
using NetExtender.Utilities.IO;
using NetExtender.Utilities.Types;

namespace NetExtender.Utilities.Registry
{
    public static class RegistryUtilities
    {
        private static IImmutableDictionary<RegistryKeys, String> RegistryKeyName { get; } = new Dictionary<RegistryKeys, String>
        {
            [RegistryKeys.CurrentUser] = "HKEY_CURRENT_USER",
            [RegistryKeys.CurrentConfig] = "HKEY_CURRENT_CONFIG",
            [RegistryKeys.LocalMachine] = "HKEY_LOCAL_MACHINE",
            [RegistryKeys.Users] = "HKEY_USERS",
            [RegistryKeys.ClassesRoot] = "HKEY_CLASSES_ROOT",
            [RegistryKeys.PerformanceData] = "HKEY_PERFORMANCE_DATA"

        }.ToImmutableDictionary();

        public const RegistryKeys DefaultKey = RegistryKeys.CurrentUser;

        public static String ToRegistryName(this RegistryKeys key)
        {
            return RegistryKeyName.TryGetValue(key, out String? value) ? value : throw new NotSupportedException();
        }

        public static IRegistry Create(this RegistryKeys key, params String[]? sections)
        {
            return Create(key, sections, false);
        }

        public static IRegistry Create(this RegistryKeys key, Boolean write, params String[]? sections)
        {
            return Create(key, sections, write);
        }

        public static IRegistry Create(this RegistryKeys key, IEnumerable<String>? sections, Boolean write)
        {
            return Create(key, sections, FileUtilities.GetFileAccess(write));
        }

        public static IRegistry Create(this RegistryKeys key, FileAccess access, params String[]? sections)
        {
            return Create(key, sections, access);
        }

        public static IRegistry Create(this RegistryKeys key, IEnumerable<String>? sections, FileAccess access)
        {
            return new NetExtender.Registry.Registry(key, sections)
            {
                Permission = access == FileAccess.Read ? RegistryKeyPermissionCheck.ReadSubTree : RegistryKeyPermissionCheck.ReadWriteSubTree
            };
        }

        public static Boolean GetValue<T>(this RegistryKey registry, String? key, out T? result)
        {
            if (registry is null)
            {
                throw new ArgumentNullException(nameof(registry));
            }

            try
            {
                Object? value = registry.GetValue(key);

                if (value is null)
                {
                    result = default;
                    return false;
                }

                if (typeof(T) == typeof(Object))
                {
                    result = (T) value;
                    return true;
                }

                if (typeof(T) == typeof(String))
                {
                    result = (T?) (Object?) value.GetString();
                    return true;
                }

                if (value is IConvertible convertible)
                {
                    result = (T) Convert.ChangeType(convertible, typeof(T));
                    return true;
                }

                result = (T) value;
                return true;
            }
            catch (Exception)
            {
                result = default;
                return false;
            }
        }
    }
}