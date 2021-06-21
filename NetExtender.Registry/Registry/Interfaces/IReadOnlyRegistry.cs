// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Collections.Generic;
using Microsoft.Win32;

namespace NetExtender.Registry.Interfaces
{
    public interface IReadOnlyRegistry : IDisposable
    {
        public RegistryKeys Key { get; }
        public String Path { get; }
        public String FullPath { get; }
        public Boolean IsReadOnly { get; }
        public T? GetValue<T>(String? key);
        public T? GetValue<T>(String? key, params String[]? sections);
        public T? GetValue<T>(String? key, IEnumerable<String>? sections);
        public Boolean GetValue<T>(String? key, out T? result);
        public Boolean GetValue<T>(String? key, out T? result, params String[] sections);
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
    }
}