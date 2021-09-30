// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Collections.Generic;
using System.Security.AccessControl;
using Microsoft.Win32;

namespace NetExtender.Registry.Interfaces
{
    public interface IRegistry : IDisposable
    {
        public RegistryKeys Key { get; }
        public String Path { get; }
        public String FullPath { get; }
        public Boolean IsReadOnly { get; }
        public IRegistry SubKey(params String[]? sections);
        public IRegistry SubKey(IEnumerable<String>? sections);
        public IRegistry SubKey(RegistryKeyPermissionCheck permission, params String[]? sections);
        public IRegistry SubKey(IEnumerable<String>? sections, RegistryKeyPermissionCheck permission);
        public IRegistry SubKey(RegistryKeyPermissionCheck permission, RegistryOptions options, params String[]? sections);
        public IRegistry SubKey(IEnumerable<String>? sections, RegistryKeyPermissionCheck permission, RegistryOptions options);
        public IRegistry SubKey(RegistryKeyPermissionCheck permission, RegistryOptions options, RegistrySecurity? security, params String[]? sections);
        public IRegistry SubKey(IEnumerable<String>? sections, RegistryKeyPermissionCheck permission, RegistryOptions options, RegistrySecurity? security);
        public IRegistry NestedSubKey(params String[]? sections);
        public IRegistry NestedSubKey(IEnumerable<String>? sections);
        public IRegistry NestedSubKey(RegistryKeyPermissionCheck permission, params String[]? sections);
        public IRegistry NestedSubKey(IEnumerable<String>? sections, RegistryKeyPermissionCheck permission);
        public IRegistry NestedSubKey(RegistryKeyPermissionCheck permission, RegistryOptions options, params String[]? sections);
        public IRegistry NestedSubKey(IEnumerable<String>? sections, RegistryKeyPermissionCheck permission, RegistryOptions options);
        public IRegistry NestedSubKey(RegistryKeyPermissionCheck permission, RegistrySecurity? security, params String[]? sections);
        public IRegistry NestedSubKey(IEnumerable<String>? sections, RegistryKeyPermissionCheck permission, RegistrySecurity? security);
        public IRegistry NestedSubKey(RegistryOptions options, RegistrySecurity? security, params String[]? sections);
        public IRegistry NestedSubKey(IEnumerable<String>? sections, RegistryOptions options, RegistrySecurity? security);
        public IRegistry NestedSubKey(RegistryKeyPermissionCheck permission, RegistryOptions options, RegistrySecurity? security, params String[]? sections);
        public IRegistry NestedSubKey(IEnumerable<String>? sections, RegistryKeyPermissionCheck permission, RegistryOptions options, RegistrySecurity? security);
        public T? GetValue<T>(String? key);
        public T? GetValue<T>(String? key, params String[]? sections);
        public T? GetValue<T>(String? key, IEnumerable<String>? sections);
        public Boolean GetValue<T>(String? key, out T? result);
        public Boolean GetValue<T>(String? key, out T? result, params String[]? sections);
        public Boolean GetValue<T>(String? key, IEnumerable<String>? sections, out T? result);
        public String? GetValue(String? key);
        public String? GetValue(String? key, params String[]? sections);
        public String? GetValue(String? key, IEnumerable<String>? sections);
        public String? GetValue(String? key, out RegistryValueKind kind);
        public String? GetValue(String? key, out RegistryValueKind kind, params String[]? sections);
        public String? GetValue(String? key, IEnumerable<String>? sections, out RegistryValueKind kind);
        public Object? GetObjectValue(String? key);
        public Object? GetObjectValue(String? key, params String[]? sections);
        public Object? GetObjectValue(String? key, IEnumerable<String>? sections);
        public Object? GetObjectValue(String? key, out RegistryValueKind kind);
        public Object? GetObjectValue(String? key, out RegistryValueKind kind, params String[]? sections);
        public Object? GetObjectValue(String? key, IEnumerable<String>? sections, out RegistryValueKind kind);
        public RegistryValueKind GetValueKind(String? key);
        public RegistryValueKind GetValueKind(String? key, params String[]? sections);
        public RegistryValueKind GetValueKind(String? key, IEnumerable<String>? sections);
        public Boolean KeyExist(String? key);
        public Boolean KeyExist(String? key, params String[]? sections);
        public Boolean KeyExist(String? key, IEnumerable<String>? sections);
        public Boolean SetValue(String? key, String? value);
        public Boolean SetValue(String? key, String? value, params String[]? sections);
        public Boolean SetValue(String? key, String? value, IEnumerable<String>? sections);
        public Boolean SetValue(String? key, Object? value);
        public Boolean SetValue(String? key, Object? value, params String[]? sections);
        public Boolean SetValue(String? key, Object? value, IEnumerable<String>? sections);
        public Boolean SetValue(String? key, Object? value, RegistryValueKind kind);
        public Boolean SetValue(String? key, Object? value, RegistryValueKind kind, params String[]? sections);
        public Boolean SetValue(String? key, Object? value, IEnumerable<String>? sections, RegistryValueKind kind);
        public Boolean WriteValue(String? key, String? value);
        public Boolean WriteValue(String? key, String? value, params String[]? sections);
        public Boolean WriteValue(String? key, String? value, IEnumerable<String>? sections);
        public Boolean WriteValue(String? key, Object? value);
        public Boolean WriteValue(String? key, Object? value, params String[]? sections);
        public Boolean WriteValue(String? key, Object? value, IEnumerable<String>? sections);
        public Boolean WriteValue(String? key, Object? value, RegistryValueKind kind);
        public Boolean WriteValue(String? key, Object? value, RegistryValueKind kind, params String[]? sections);
        public Boolean WriteValue(String? key, Object? value, IEnumerable<String>? sections, RegistryValueKind kind);
        public Boolean RemoveKey(String? key);
        public Boolean RemoveKey(String? key, params String[]? sections);
        public Boolean RemoveKey(String? key, IEnumerable<String>? sections);
        public String[]? GetValueNames();
        public String[]? GetValueNames(params String[]? sections);
        public String[]? GetValueNames(IEnumerable<String>? sections);
        public RegistryEntry[]? GetValues();
        public RegistryEntry[]? GetValues(params String[]? sections);
        public RegistryEntry[]? GetValues(IEnumerable<String>? sections);
        public RegistryEntry[]? Dump();
        public RegistryEntry[]? Dump(params String[]? sections);
        public RegistryEntry[]? Dump(IEnumerable<String>? sections);
        public String[]? GetSubKeyNames();
        public String[]? GetSubKeyNames(params String[]? sections);
        public String[]? GetSubKeyNames(IEnumerable<String>? sections);
        public Boolean RemoveSubKey();
        public Boolean RemoveSubKey(params String[]? sections);
        public Boolean RemoveSubKey(IEnumerable<String>? sections);
        public Boolean RemoveSubKey(String? subkey);
        public Boolean RemoveSubKey(String? subkey, params String[]? sections);
        public Boolean RemoveSubKey(String? subkey, IEnumerable<String>? sections);
        public Boolean RemoveAllSubKey();
        public Boolean RemoveAllSubKey(params String[]? sections);
        public Boolean RemoveAllSubKey(IEnumerable<String>? sections);
        public Boolean RemoveSubKeyTree();
        public Boolean RemoveSubKeyTree(params String[]? sections);
        public Boolean RemoveSubKeyTree(IEnumerable<String>? sections);
        public Boolean RemoveSubKeyTree(String? subkey);
        public Boolean RemoveSubKeyTree(String? subkey, params String[]? sections);
        public Boolean RemoveSubKeyTree(String? subkey, IEnumerable<String>? sections);
        public Boolean RemoveAllSubKeyTree();
        public Boolean RemoveAllSubKeyTree(params String[]? sections);
        public Boolean RemoveAllSubKeyTree(IEnumerable<String>? sections);
    }
}