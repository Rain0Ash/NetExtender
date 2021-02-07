// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.IO;
using NetExtender.Apps.Domains;
using NetExtender.Registry;
using NetExtender.Registry.Interfaces;
using NetExtender.Utils.IO;

namespace NetExtender.Utils.OS
{
    public static class RegistryUtils
    {
        public const RegistryKeys DefaultKey = RegistryKeys.CurrentUser;
        
        public static IRegistry Create(Boolean write)
        {
            return Create(DefaultKey, write);
        }
        
        public static IRegistry Create(FileAccess access)
        {
            return Create(DefaultKey, access);
        }
        
        public static IRegistry Create(String path, Boolean write)
        {
            return Create(DefaultKey, path, write);
        }
        
        public static IRegistry Create(String path, FileAccess access)
        {
            return Create(DefaultKey, path, access);
        }
        
        public static IRegistry Create(this RegistryKeys key)
        {
            return Create(key, FileAccess.Read);
        }
        
        public static IRegistry Create(this RegistryKeys key, Boolean write)
        {
            return Create(key, FileUtils.GetFileAccess(write));
        }
        
        public static IRegistry Create(this RegistryKeys key, FileAccess access)
        {
            return Create(key, $"Software\\{Domain.AppNameOrFriendlyName}", access);
        }
        
        public static IRegistry Create(this RegistryKeys key, String path, Boolean write)
        {
            return Create(key, path, FileUtils.GetFileAccess(write));
        }
        
        public static IRegistry Create(this RegistryKeys key, String path, FileAccess access)
        {
            return new Registry.Registry(path, key, access);
        }
    }
}