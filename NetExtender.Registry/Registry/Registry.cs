// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Security.AccessControl;
using Microsoft.Win32;
using NetExtender.Registry.Interfaces;
using NetExtender.Utilities.Registry;
using NetExtender.Utilities.Types;

namespace NetExtender.Registry
{
    [SuppressMessage("ReSharper", "PossibleMultipleEnumeration")]
    [SuppressMessage("ReSharper", "InvertIf")]
    public sealed class Registry : IRegistry, IReadOnlyRegistry
    {
        private static IImmutableDictionary<RegistryKeys, RegistryKey> Keys { get; } = new Dictionary<RegistryKeys, RegistryKey>
        {
            [RegistryKeys.CurrentUser] = Microsoft.Win32.Registry.CurrentUser,
            [RegistryKeys.CurrentConfig] = Microsoft.Win32.Registry.CurrentConfig,
            [RegistryKeys.LocalMachine] = Microsoft.Win32.Registry.LocalMachine,
            [RegistryKeys.Users] = Microsoft.Win32.Registry.Users,
            [RegistryKeys.ClassesRoot] = Microsoft.Win32.Registry.ClassesRoot,
            [RegistryKeys.PerformanceData] = Microsoft.Win32.Registry.PerformanceData
            
        }.ToImmutableDictionary();
        
        public const String Separator = "\\";

        private RegistryKey? _registry;
        private RegistryKey RegistryKey
        {
            get
            {
                return _registry ??= Keys.TryGetValue(Key, out RegistryKey? registry) ? registry : throw new NotSupportedException();
            }
        }

        public RegistryKeys Key { get; }
        
        public ImmutableArray<String> Sections { get; }
        
        private String? _path;
        public String Path
        {
            get
            {
                return _path ??= Sections.Join(Separator);
            }
        }

        private String? _fullpath;
        public String FullPath
        {
            get
            {
                return _fullpath ??= $"{Key.ToRegistryName()}{Separator}{Path}";
            }
        }

        private Registry? Nested { get; }

        private Registry? _parent;
        public Registry Parent
        {
            get
            {
                if (_parent is not null)
                {
                    return _parent;
                }

                if (Sections.Length <= 0)
                {
                    return _parent ??= new Registry(Key);
                }

                return _parent ??= new Registry(Key, Sections.RemoveAt(Sections.Length - 1));
            }
        }

        private RegistryKey? _read;
        private RegistryKey? Read
        {
            get
            {
                if (_read is not null)
                {
                    return _read;
                }

                try
                {
                    return _read ??= RegistryKey.OpenSubKey(Path);
                }
                catch (Exception)
                {
                    return null;
                }
            }
        }
        
        private RegistryKey? _write;
        private RegistryKey? Write
        {
            get
            {
                if (_write is not null)
                {
                    return _write;
                }
                
                if (IsReadOnly)
                {
                    return null;
                }

                try
                {
                    return _write ??= RegistryKey.OpenSubKey(Path, RegistryKeyPermissionCheck.ReadWriteSubTree);
                }
                catch (Exception)
                {
                    return null;
                }
            }
        }
        
        private RegistryKey? _full;
        private RegistryKey? Full
        {
            get
            {
                if (_full is not null)
                {
                    return _full;
                }
                
                if (IsReadOnly)
                {
                    return null;
                }

                try
                {
                    return _full ??= RegistryKey.OpenSubKey(Path, RegistryRights.FullControl);
                }
                catch (Exception)
                {
                    return null;
                }
            }
        }

        private RegistryKey? _create;
        private RegistryKey? Create
        {
            get
            {
                if (_create is not null)
                {
                    return _create;
                }

                if (IsReadOnly)
                {
                    return null;
                }
                
                try
                {
                    return _create ??= RegistryKey.CreateSubKey(Path, Permission, Options, Security);
                }
                catch (Exception)
                {
                    return null;
                }
            }
        }
        
        public RegistryKeyPermissionCheck Permission { get; init; }
        public RegistryOptions Options { get; init; }
        public RegistrySecurity? Security { get; init; }

        public Boolean IsReadOnly
        {
            get
            {
                return Permission switch
                {
                    RegistryKeyPermissionCheck.Default => Nested?.IsReadOnly ?? false,
                    RegistryKeyPermissionCheck.ReadSubTree => true,
                    RegistryKeyPermissionCheck.ReadWriteSubTree => false,
                    _ => throw new NotSupportedException()
                };
            }
        }

        private Registry(Registry nested, IEnumerable<String>? sections = null)
            : this(nested, false, sections)
        {
        }

        private Registry(Registry nested, Boolean inherit, IEnumerable<String>? sections = null)
            : this(nested?.Key ?? throw new ArgumentNullException(nameof(nested)), nested, inherit, sections)
        {
        }

        private Registry(RegistryKeys key, Registry nested, IEnumerable<String>? sections = null)
            : this(key, nested, false, sections)
        {
        }

        private Registry(RegistryKeys key, Registry nested, Boolean inherit, IEnumerable<String>? sections = null)
        {
            Key = nested?.Key ?? key;
            Sections = nested?.Sections ?? ImmutableArray<String>.Empty;
            if (sections is not null)
            {
                Sections = Sections.AddRange(sections).RemoveAll(String.IsNullOrEmpty);
            }

            if (nested is null)
            {
                return;
            }

            Nested = nested;

            if (!inherit)
            {
                return;
            }

            Permission = Nested.Permission;
            Options = Nested.Options;
            Security = Nested.Security;
        }

        public Registry(RegistryKeys key)
            : this(key, null)
        {
        }
        
        public Registry(RegistryKeys key, IEnumerable<String>? sections)
        {
            Key = key;
            Sections = sections is not null ? sections.AsImmutableArray().RemoveAll(String.IsNullOrEmpty) : ImmutableArray<String>.Empty;
        }

        public IRegistry SubKey(params String[]? sections)
        {
            return SubKey((IEnumerable<String>?) sections);
        }
        
        public IRegistry SubKey(IEnumerable<String>? sections)
        {
            return new Registry(Key, this, sections);
        }
        
        public IRegistry SubKey(RegistryKeyPermissionCheck permission, params String[]? sections)
        {
            return SubKey(sections, permission);
        }
        
        public IRegistry SubKey(IEnumerable<String>? sections, RegistryKeyPermissionCheck permission)
        {
            return new Registry(Key, this, sections)
            {
                Permission = permission
            };
        }
        
        public IRegistry SubKey(RegistryKeyPermissionCheck permission, RegistryOptions options, params String[]? sections)
        {
            return SubKey(sections, permission, options);
        }
        
        public IRegistry SubKey(IEnumerable<String>? sections, RegistryKeyPermissionCheck permission, RegistryOptions options)
        {
            return new Registry(Key, this, sections)
            {
                Permission = permission,
                Options = options
            };
        }
        
        public IRegistry SubKey(RegistryKeyPermissionCheck permission, RegistryOptions options, RegistrySecurity? security, params String[]? sections)
        {
            return SubKey(sections, permission, options, security);
        }
        
        public IRegistry SubKey(IEnumerable<String>? sections, RegistryKeyPermissionCheck permission, RegistryOptions options, RegistrySecurity? security)
        {
            return new Registry(Key, this, sections)
            {
                Permission = permission,
                Options = options,
                Security = security
            };
        }
        
        public IRegistry NestedSubKey(params String[]? sections)
        {
            return NestedSubKey((IEnumerable<String>?) sections);
        }
        
        public IRegistry NestedSubKey(IEnumerable<String>? sections)
        {
            return new Registry(Key, this, true, sections);
        }
        
        public IRegistry NestedSubKey(RegistryKeyPermissionCheck permission, params String[]? sections)
        {
            return NestedSubKey(sections, permission);
        }
        
        public IRegistry NestedSubKey(IEnumerable<String>? sections, RegistryKeyPermissionCheck permission)
        {
            return new Registry(Key, this, true, sections)
            {
                Permission = permission
            };
        }
        
        public IRegistry NestedSubKey(RegistryKeyPermissionCheck permission, RegistryOptions options, params String[]? sections)
        {
            return NestedSubKey(sections, permission, options);
        }
        
        public IRegistry NestedSubKey(IEnumerable<String>? sections, RegistryKeyPermissionCheck permission, RegistryOptions options)
        {
            return new Registry(Key, this, true, sections)
            {
                Permission = permission,
                Options = options
            };
        }
        
        public IRegistry NestedSubKey(RegistryKeyPermissionCheck permission, RegistrySecurity? security, params String[]? sections)
        {
            return NestedSubKey(sections, permission, security);
        }
        
        public IRegistry NestedSubKey(IEnumerable<String>? sections, RegistryKeyPermissionCheck permission, RegistrySecurity? security)
        {
            return new Registry(Key, this, true, sections)
            {
                Permission = permission,
                Security = security
            };
        }
        
        public IRegistry NestedSubKey(RegistryOptions options, RegistrySecurity? security, params String[]? sections)
        {
            return NestedSubKey(sections, options, security);
        }
        
        public IRegistry NestedSubKey(IEnumerable<String>? sections, RegistryOptions options, RegistrySecurity? security)
        {
            return new Registry(Key, this, true, sections)
            {
                Options = options,
                Security = security
            };
        }
        
        public IRegistry NestedSubKey(RegistryKeyPermissionCheck permission, RegistryOptions options, RegistrySecurity? security, params String[]? sections)
        {
            return NestedSubKey(sections, permission, options, security);
        }
        
        public IRegistry NestedSubKey(IEnumerable<String>? sections, RegistryKeyPermissionCheck permission, RegistryOptions options, RegistrySecurity? security)
        {
            return new Registry(Key, this, true, sections)
            {
                Permission = permission,
                Options = options,
                Security = security
            };
        }
        
        public T? GetValue<T>(String? key)
        {
            return GetValue(key, out T? result) ? result : result is null ? default : throw new InvalidCastException();
        }
        
        public T? GetValue<T>(String? key, params String[]? sections)
        {
            return GetValue<T>(key, (IEnumerable<String>?) sections);
        }

        public T? GetValue<T>(String? key, IEnumerable<String>? sections)
        {
            if (sections?.CountIfMaterialized() > 0)
            {
                return GetValue(key, sections, out T? result) ? result : result is null ? default : throw new InvalidCastException();
            }
            
            return GetValue<T>(key);
        }
        
        [SuppressMessage("ReSharper", "CognitiveComplexity")]
        public Boolean GetValue<T>(String? key, out T? result)
        {
            try
            {
                Object? value = Read?.GetValue(key);

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
                    result = (T?) Convert.ChangeType(convertible, typeof(T));
                    return true;
                }

                if (typeof(T) == typeof(Byte[]))
                {
                    if (value is Byte[])
                    {
                        result = (T) value;
                        return true;
                    }
                }
                else if (typeof(T) == typeof(String[]))
                {
                    if (value is String[])
                    {
                        result = (T) value;
                        return true;
                    }
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
        
        public Boolean GetValue<T>(String? key, out T? result, params String[]? sections)
        {
            return GetValue(key, sections, out result);
        }

        public Boolean GetValue<T>(String? key, IEnumerable<String>? sections, out T? result)
        {
            if (sections?.CountIfMaterialized() > 0)
            {
                using IRegistry registry = NestedSubKey(sections);
                return registry.GetValue(key, out result);
            }
            
            return GetValue(key, out result);
        }
        
        public Object? GetObjectValue(String? key)
        {
            try
            {
                return Read?.GetValue(key);
            }
            catch (Exception)
            {
                return null;
            }
        }
        
        public Object? GetObjectValue(String? key, params String[]? sections)
        {
            return GetObjectValue(key, (IEnumerable<String>?) sections);
        }

        public Object? GetObjectValue(String? key, IEnumerable<String>? sections)
        {
            if (sections?.CountIfMaterialized() > 0)
            {
                using IRegistry registry = NestedSubKey(sections);
                return registry.GetObjectValue(key);
            }

            return GetObjectValue(key);
        }
        
        public Object? GetObjectValue(String? key, out RegistryValueKind kind)
        {
            try
            {
                RegistryKey? read = Read;

                if (read is null)
                {
                    kind = RegistryValueKind.Unknown;
                    return null;
                }
                
                Object? value = read.GetValue(key);
                kind = read.GetValueKind(key);
                
                return value;
            }
            catch (Exception)
            {
                kind = RegistryValueKind.Unknown;
                return null;
            }
        }
        
        public Object? GetObjectValue(String? key, out RegistryValueKind kind, params String[]? sections)
        {
            return GetObjectValue(key, sections, out kind);
        }

        public Object? GetObjectValue(String? key, IEnumerable<String>? sections, out RegistryValueKind kind)
        {
            if (sections?.CountIfMaterialized() > 0)
            {
                using IRegistry registry = NestedSubKey(sections);
                return registry.GetObjectValue(key, out kind);
            }

            return GetObjectValue(key, out kind);
        }
        
        public String? GetValue(String? key)
        {
            try
            {
                return Read?.GetValue(key)?.GetString();
            }
            catch (Exception)
            {
                return null;
            }
        }

        public String? GetValue(String? key, params String[]? sections)
        {
            return GetValue(key, (IEnumerable<String>?) sections);
        }

        public String? GetValue(String? key, IEnumerable<String>? sections)
        {
            if (sections?.CountIfMaterialized() > 0)
            {
                using IRegistry registry = NestedSubKey(sections);
                return registry.GetValue(key);
            }

            return GetValue(key);
        }
        
        public String? GetValue(String? key, out RegistryValueKind kind)
        {
            try
            {
                RegistryKey? read = Read;

                if (read is null)
                {
                    kind = RegistryValueKind.Unknown;
                    return null;
                }
                
                String? item = read.GetValue(key)?.GetString();
                kind = read.GetValueKind(key);

                return item;
            }
            catch (Exception)
            {
                kind = RegistryValueKind.Unknown;
                return null;
            }
        }

        public String? GetValue(String? key, out RegistryValueKind kind, params String[]? sections)
        {
            return GetValue(key, sections, out kind);
        }

        public String? GetValue(String? key, IEnumerable<String>? sections, out RegistryValueKind kind)
        {
            if (sections?.CountIfMaterialized() > 0)
            {
                using IRegistry registry = NestedSubKey(sections);
                return registry.GetValue(key, out kind);
            }

            return GetValue(key, out kind);
        }
        
        public RegistryValueKind GetValueKind(String? key)
        {
            try
            {
                return Read?.GetValueKind(key) ?? RegistryValueKind.Unknown;
            }
            catch (Exception)
            {
                return RegistryValueKind.Unknown;
            }
        }

        public RegistryValueKind GetValueKind(String? key, params String[]? sections)
        {
            return GetValueKind(key, (IEnumerable<String>?) sections);
        }

        public RegistryValueKind GetValueKind(String? key, IEnumerable<String>? sections)
        {
            if (sections?.CountIfMaterialized() > 0)
            {
                using IRegistry registry = NestedSubKey(sections);
                return registry.GetValueKind(key);
            }

            return GetValueKind(key);
        }
        
        public Boolean SetValue(String? key, String? value)
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
                    return RemoveKey(key);
                }

                RegistryKey? create = Create;

                if (create is null)
                {
                    return false;
                }

                create.SetValue(key, value);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public Boolean SetValue(String? key, String? value, params String[]? sections)
        {
            return SetValue(key, value, (IEnumerable<String>?) sections);
        }

        public Boolean SetValue(String? key, String? value, IEnumerable<String>? sections)
        {
            if (sections?.CountIfMaterialized() > 0)
            {
                using IRegistry registry = NestedSubKey(sections);
                return registry.SetValue(key, value);
            }

            return SetValue(key, value);
        }
        
        public Boolean SetValue(String? key, Object? value)
        {
            return SetValue(key, value, RegistryValueKind.Unknown);
        }
        
        public Boolean SetValue(String? key, Object? value, params String[]? sections)
        {
            return SetValue(key, value, (IEnumerable<String>?) sections);
        }

        public Boolean SetValue(String? key, Object? value, IEnumerable<String>? sections)
        {
            return SetValue(key, value, sections, RegistryValueKind.Unknown);
        }
        
        public Boolean SetValue(String? key, Object? value, RegistryValueKind kind)
        {
            try
            {
                Object? current = GetObjectValue(key);
                
                if (current is null && value is null || current is not null && current.Equals(value))
                {
                    return true;
                }

                if (IsReadOnly)
                {
                    return false;
                }

                if (value is null)
                {
                    return RemoveKey(key);
                }

                RegistryKey? create = Create;

                if (create is null)
                {
                    return false;
                }

                create.SetValue(key, value, kind);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public Boolean SetValue(String? key, Object? value, RegistryValueKind kind, params String[]? sections)
        {
            return SetValue(key, value, sections, kind);
        }

        public Boolean SetValue(String? key, Object? value, IEnumerable<String>? sections, RegistryValueKind kind)
        {
            if (sections?.CountIfMaterialized() > 0)
            {
                using IRegistry registry = NestedSubKey(sections);
                return registry.SetValue(key, value, kind);
            }

            return SetValue(key, value, kind);
        }

        public Boolean WriteValue(String? key, String? value)
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
                    return RemoveKey(key);
                }

                RegistryKey? write = Write;

                if (write is null)
                {
                    return false;
                }

                write.SetValue(key, value);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public Boolean WriteValue(String? key, String? value, params String[]? sections)
        {
            return WriteValue(key, value, (IEnumerable<String>?) sections);
        }

        public Boolean WriteValue(String? key, String? value, IEnumerable<String>? sections)
        {
            if (sections?.CountIfMaterialized() > 0)
            {
                using IRegistry registry = NestedSubKey(sections);
                return registry.WriteValue(key, value);
            }

            return WriteValue(key, value);
        }

        public Boolean WriteValue(String? key, Object? value)
        {
            return WriteValue(key, value, RegistryValueKind.Unknown);
        }

        public Boolean WriteValue(String? key, Object? value, params String[]? sections)
        {
            return WriteValue(key, value, (IEnumerable<String>?) sections);
        }

        public Boolean WriteValue(String? key, Object? value, IEnumerable<String>? sections)
        {
            return WriteValue(key, value, sections, RegistryValueKind.Unknown);
        }

        public Boolean WriteValue(String? key, Object? value, RegistryValueKind kind)
        {
            try
            {
                Object? current = GetObjectValue(key);
                
                if (current is null && value is null || current is not null && current.Equals(value))
                {
                    return true;
                }

                if (IsReadOnly)
                {
                    return false;
                }

                if (value is null)
                {
                    return RemoveKey(key);
                }

                RegistryKey? write = Write;

                if (write is null)
                {
                    return false;
                }

                write.SetValue(key, value, kind);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public Boolean WriteValue(String? key, Object? value, RegistryValueKind kind, params String[]? sections)
        {
            return WriteValue(key, value, sections, kind);
        }

        public Boolean WriteValue(String? key, Object? value, IEnumerable<String>? sections, RegistryValueKind kind)
        {
            if (sections?.CountIfMaterialized() > 0)
            {
                using IRegistry registry = NestedSubKey(sections);
                return registry.WriteValue(key, value, kind);
            }

            return WriteValue(key, value, kind);
        }

        public Boolean KeyExist(String? key)
        {
            return GetValue(key) is not null;
        }

        public Boolean KeyExist(String? key, params String[]? sections)
        {
            return KeyExist(key, (IEnumerable<String>?) sections);
        }

        public Boolean KeyExist(String? key, IEnumerable<String>? sections)
        {
            return GetValue(key, sections) is not null;
        }

        public Boolean RemoveKey(String? key)
        {
            if (key is null)
            {
                return false;
            }
            
            try
            {
                RegistryKey? read = Read;
                
                if (read is null)
                {
                    return false;
                }
                
                if (read.GetValue(key) is null)
                {
                    return true;
                }

                if (IsReadOnly)
                {
                    return false;
                }

                RegistryKey? write = Write;

                if (write is null)
                {
                    return false;
                }
                
                write.DeleteValue(key);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public Boolean RemoveKey(String? key, params String[]? sections)
        {
            return RemoveKey(key, (IEnumerable<String>?) sections);
        }

        public Boolean RemoveKey(String? key, IEnumerable<String>? sections)
        {
            if (sections?.CountIfMaterialized() > 0)
            {
                using IRegistry registry = NestedSubKey(sections);
                return registry.RemoveKey(key);
            }

            return RemoveKey(key);
        }
        
        public String[]? GetValueNames()
        {
            try
            {
                return Read?.GetValueNames();
            }
            catch (Exception)
            {
                return null;
            }
        }

        public String[]? GetValueNames(params String[]? sections)
        {
            return GetValueNames((IEnumerable<String>?) sections);
        }

        public String[]? GetValueNames(IEnumerable<String>? sections)
        {
            if (sections?.CountIfMaterialized() > 0)
            {
                using IRegistry registry = NestedSubKey(sections);
                return registry.GetValueNames();
            }

            return GetValueNames();
        }

        public RegistryEntry[]? GetValues()
        {
            try
            {
                String[]? names = GetValueNames();

                if (names is null)
                {
                    return null;
                }

                RegistryEntry[] values = new RegistryEntry[names.Length];

                for (Int32 i = 0; i < names.Length; i++)
                {
                    String name = names[i];
                    Object? value = GetObjectValue(name, out RegistryValueKind kind);
                    
                    values[i] = new RegistryEntry
                    {
                        Key = Key,
                        Sections = Sections,
                        Name = name,
                        Kind = kind,
                        Value = value
                    };
                }

                return values;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public RegistryEntry[]? GetValues(params String[]? sections)
        {
            return GetValues((IEnumerable<String>?) sections);
        }

        public RegistryEntry[]? GetValues(IEnumerable<String>? sections)
        {
            if (sections?.CountIfMaterialized() > 0)
            {
                using IRegistry registry = NestedSubKey(sections);
                return registry.GetValues();
            }

            return GetValues();
        }

        public String[]? GetSubKeyNames()
        {
            try
            {
                return Read?.GetSubKeyNames();
            }
            catch (Exception)
            {
                return null;
            }
        }

        public String[]? GetSubKeyNames(params String[]? sections)
        {
            return GetSubKeyNames((IEnumerable<String>?) sections);
        }

        public String[]? GetSubKeyNames(IEnumerable<String>? sections)
        {
            if (sections?.CountIfMaterialized() > 0)
            {
                using IRegistry registry = NestedSubKey(sections);
                return registry.GetSubKeyNames();
            }

            return GetSubKeyNames();
        }

        private IEnumerable<RegistryEntry> DumpInternal()
        {
            RegistryEntry[]? entries = GetValues();

            if (entries is not null)
            {
                foreach (RegistryEntry entry in entries)
                {
                    yield return entry;
                }
            }

            String[]? subkeys = GetSubKeyNames();

            if (subkeys is null)
            {
                yield break;
            }

            foreach (String subkey in subkeys)
            {
                using IRegistry registry = NestedSubKey(subkey);
                    
                RegistryEntry[]? subentries = registry.Dump();

                if (subentries is null)
                {
                    continue;
                }

                foreach (RegistryEntry entry in subentries)
                {
                    yield return entry;
                }
            }
        }
        
        public RegistryEntry[]? Dump()
        {
            try
            {
                return DumpInternal().ToArray();
            }
            catch (Exception)
            {
                return null;
            }
        }

        public RegistryEntry[]? Dump(params String[]? sections)
        {
            return Dump((IEnumerable<String>?) sections);
        }

        public RegistryEntry[]? Dump(IEnumerable<String>? sections)
        {
            if (sections?.CountIfMaterialized() > 0)
            {
                using IRegistry registry = NestedSubKey(sections);
                return registry.Dump();
            }

            return Dump();
        }

        public Boolean RemoveSubKey()
        {
            try
            {
                RegistryKey? read = Read;

                if (read is null)
                {
                    return true;
                }

                if (IsReadOnly)
                {
                    return false;
                }
                
                if (read.SubKeyCount > 0)
                {
                    return false;
                }

                return Sections.Length > 0 && Parent.RemoveSubKey(Sections[^1]);
            }
            catch (Exception)
            {
                return false;
            }
        }

        public Boolean RemoveSubKey(params String[]? sections)
        {
            return RemoveSubKey((IEnumerable<String>?) sections);
        }
        
        public Boolean RemoveSubKey(IEnumerable<String>? sections)
        {
            if (sections?.CountIfMaterialized() > 0)
            {
                using IRegistry registry = NestedSubKey(sections);
                return registry.RemoveSubKey();
            }

            return RemoveSubKey();
        }

        public Boolean RemoveSubKey(String? subkey)
        {
            try
            {
                if (String.IsNullOrEmpty(subkey))
                {
                    return false;
                }
                
                RegistryKey? read = Read;

                if (read is null)
                {
                    return true;
                }

                if (IsReadOnly)
                {
                    return false;
                }

                RegistryKey? write = Write;

                if (write is null)
                {
                    return false;
                }

                write.DeleteSubKey(subkey);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        
        public Boolean RemoveSubKey(String? subkey, params String[]? sections)
        {
            return RemoveSubKey(subkey, (IEnumerable<String>?) sections);
        }
        
        public Boolean RemoveSubKey(String? subkey, IEnumerable<String>? sections)
        {
            if (sections?.CountIfMaterialized() > 0)
            {
                using IRegistry registry = NestedSubKey(sections);
                return registry.RemoveSubKey(subkey);
            }

            return RemoveSubKey(subkey);
        }
        
        public Boolean RemoveAllSubKey()
        {
            try
            {
                RegistryKey? read = Read;

                if (read is null)
                {
                    return true;
                }

                if (IsReadOnly)
                {
                    return false;
                }

                RegistryKey? write = Write;

                if (write is null)
                {
                    return false;
                }
                
                String[]? subkeys = GetSubKeyNames();

                if (subkeys is null)
                {
                    return false;
                }

                Int32 count = 0;
                foreach (String subkey in subkeys)
                {
                    try
                    {
                        write.DeleteSubKey(subkey);
                        count++;
                    }
                    catch (Exception)
                    {
                        //ignored
                    }
                }
                
                return count > 0;
            }
            catch (Exception)
            {
                return false;
            }
        }
        
        public Boolean RemoveAllSubKey(params String[]? sections)
        {
            return RemoveAllSubKey((IEnumerable<String>?) sections);
        }
        
        public Boolean RemoveAllSubKey(IEnumerable<String>? sections)
        {
            if (sections?.CountIfMaterialized() > 0)
            {
                using IRegistry registry = NestedSubKey(sections);
                return registry.RemoveAllSubKey();
            }

            return RemoveAllSubKey();
        }
        
        public Boolean RemoveSubKeyTree()
        {
            try
            {
                RegistryKey? read = Read;

                if (read is null)
                {
                    return true;
                }

                if (IsReadOnly)
                {
                    return false;
                }

                return Sections.Length > 0 && Parent.RemoveSubKeyTree(Sections[^1]);
            }
            catch (Exception)
            {
                return false;
            }
        }

        public Boolean RemoveSubKeyTree(params String[]? sections)
        {
            return RemoveSubKeyTree((IEnumerable<String>?) sections);
        }
        
        public Boolean RemoveSubKeyTree(IEnumerable<String>? sections)
        {
            if (sections?.CountIfMaterialized() > 0)
            {
                using IRegistry registry = NestedSubKey(sections);
                return registry.RemoveSubKeyTree();
            }

            return RemoveSubKeyTree();
        }

        public Boolean RemoveSubKeyTree(String? subkey)
        {
            try
            {
                if (String.IsNullOrEmpty(subkey))
                {
                    return false;
                }
                
                RegistryKey? read = Read;

                if (read is null)
                {
                    return true;
                }

                if (IsReadOnly)
                {
                    return false;
                }

                RegistryKey? write = Write;

                if (write is null)
                {
                    return false;
                }

                write.DeleteSubKeyTree(subkey);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        
        public Boolean RemoveSubKeyTree(String? subkey, params String[]? sections)
        {
            return RemoveSubKeyTree(subkey, (IEnumerable<String>?) sections);
        }
        
        public Boolean RemoveSubKeyTree(String? subkey, IEnumerable<String>? sections)
        {
            if (sections?.CountIfMaterialized() > 0)
            {
                using IRegistry registry = NestedSubKey(sections);
                return registry.RemoveSubKeyTree(subkey);
            }

            return RemoveSubKeyTree(subkey);
        }
        
        public Boolean RemoveAllSubKeyTree()
        {
            try
            {
                RegistryKey? read = Read;

                if (read is null)
                {
                    return true;
                }

                if (IsReadOnly)
                {
                    return false;
                }

                RegistryKey? write = Write;

                if (write is null)
                {
                    return false;
                }

                String[]? subkeys = GetSubKeyNames();

                if (subkeys is null)
                {
                    return false;
                }
                
                Int32 count = 0;
                foreach (String subkey in subkeys)
                {
                    try
                    {
                        write.DeleteSubKeyTree(subkey);
                        count++;
                    }
                    catch (Exception)
                    {
                        //ignored
                    }
                }
                
                return count > 0;
            }
            catch (Exception)
            {
                return false;
            }
        }
        
        public Boolean RemoveAllSubKeyTree(params String[]? sections)
        {
            return RemoveAllSubKeyTree((IEnumerable<String>?) sections);
        }
        
        
        public Boolean RemoveAllSubKeyTree(IEnumerable<String>? sections)
        {
            if (sections?.CountIfMaterialized() > 0)
            {
                using IRegistry registry = NestedSubKey(sections);
                return registry.RemoveAllSubKeyTree();
            }

            return RemoveAllSubKeyTree();
        }
        
        public override Int32 GetHashCode()
        {
            return HashCode.Combine(Key, FullPath, Permission, Options, Security);
        }

        public override Boolean Equals(Object? obj)
        {
            return ReferenceEquals(this, obj) || obj is Registry registry &&
                registry.Key == Key &&
                registry.FullPath == FullPath &&
                registry.Permission == Permission &&
                registry.Options == Options &&
                (registry.Security is null && Security is null || Security is not null && Security.Equals(registry.Security));
        }

        public override String ToString()
        {
            return FullPath;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        private void Dispose(Boolean disposing)
        {
            if (disposing)
            {
                
            }
            
            _read?.Dispose();
            _write?.Dispose();
            _full?.Dispose();
            _create?.Dispose();
        }
        
        ~Registry()
        {
            Dispose(false);
        }
    }
}