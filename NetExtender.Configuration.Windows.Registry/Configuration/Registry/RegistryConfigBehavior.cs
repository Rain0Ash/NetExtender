// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using NetExtender.Configuration.Behavior;
using NetExtender.Configuration.Common;
using NetExtender.Registry;
using NetExtender.Registry.Interfaces;
using NetExtender.Types.Dictionaries;
using NetExtender.Utilities.Application;
using NetExtender.Utilities.IO;
using NetExtender.Utilities.Registry;
using NetExtender.Utilities.Types;

namespace NetExtender.Configuration.Windows.Registry
{
    public class RegistryConfigBehavior : ConfigBehavior
    {
        protected IRegistry Registry { get; }

        public RegistryKeys RegistryKey
        {
            get
            {
                return Registry.Key;
            }
        }

        protected static String GetDefaultPath(String? path)
        {
            return String.IsNullOrEmpty(path) ? $"Software\\{ApplicationUtilities.FriendlyName}" : path;
        }

        public RegistryConfigBehavior()
            : this(null, ConfigOptions.None)
        {
        }

        public RegistryConfigBehavior(ConfigOptions options)
            : this(null, options)
        {
        }

        public RegistryConfigBehavior(String? path)
            : this(path, ConfigOptions.None)
        {
        }

        public RegistryConfigBehavior(String? path, ConfigOptions options)
            : this(RegistryKeys.CurrentUser, path, options)
        {
        }

        public RegistryConfigBehavior(RegistryKeys key)
            : this(key, null, ConfigOptions.None)
        {
        }

        public RegistryConfigBehavior(RegistryKeys key, ConfigOptions options)
            : this(key, null, options)
        {
        }

        public RegistryConfigBehavior(RegistryKeys key, String? path)
            : this(key, path, ConfigOptions.None)
        {
        }

        public RegistryConfigBehavior(RegistryKeys key, String? path, ConfigOptions options)
            : base(GetDefaultPath(path), options)
        {
            Registry = key.Create(Path.Split(PathUtilities.Separators), !IsReadOnly);
        }

        public override String? Get(String? key, IEnumerable<String>? sections)
        {
            return Registry.GetValue(key, sections);
        }

        public override Boolean Set(String? key, String? value, IEnumerable<String>? sections)
        {
            if (IsReadOnly || Registry.IsReadOnly)
            {
                return false;
            }

            if (IsIgnoreEvent && !IsLazyWrite)
            {
                return Registry.SetValue(key, value, sections);
            }

            sections = sections.AsIImmutableList();

            if (IsLazyWrite && Get(key, sections) == value)
            {
                return true;
            }

            if (!Registry.SetValue(key, value, sections))
            {
                return false;
            }

            OnChanged(new ConfigurationValueEntry(key, value, sections));
            return true;
        }

        protected virtual ConfigurationEntry EntriesConvert(RegistryEntry entry)
        {
            return new ConfigurationEntry(entry.Name, entry.Sections);
        }

        protected virtual ConfigurationValueEntry ValueEntriesConvert(RegistryEntry entry)
        {
            return new ConfigurationValueEntry(entry.Name, Get(entry.Name, entry.Sections), entry.Sections);
        }

        public override ConfigurationEntry[]? GetExists(IEnumerable<String>? sections)
        {
            return Registry.Dump(sections)?.Select(EntriesConvert).ToArray();
        }

        public override ConfigurationValueEntry[]? GetExistsValues(IEnumerable<String>? sections)
        {
            return Registry.Dump(sections)?.Select(ValueEntriesConvert).ToArray();
        }

        public override Boolean Clear(IEnumerable<String>? sections)
        {
            return !IsReadOnly && Registry.RemoveAllSubKeyTree(sections);
        }

        public override Boolean Reload()
        {
            return false;
        }

        // ReSharper disable once CognitiveComplexity
        public override Boolean Merge(IEnumerable<ConfigurationValueEntry>? entries)
        {
            if (IsReadOnly || Registry.IsReadOnly)
            {
                return false;
            }

            if (entries is null)
            {
                return false;
            }

            ConfigurationValueEntry[]? values = GetExistsValues(null);

            if (values is null || values.Length <= 0)
            {
                return entries.DistinctLastBy(item => (ConfigurationEntry) item).Aggregate(false, (current, entry) => current | Set(entry.Key, entry.Value, entry.Sections));
            }

            IndexDictionary<ConfigurationEntry, ConfigurationValueEntry> dictionary = values.ToIndexDictionary(item => (ConfigurationEntry) item, item => item);
            List<ConfigurationValueEntry>? changes = !IsIgnoreEvent ? new List<ConfigurationValueEntry>(dictionary.Count) : null;

            foreach (ConfigurationValueEntry entry in entries.DistinctLastBy(item => (ConfigurationEntry) item))
            {
                if (entry.Key is null)
                {
                    continue;
                }

                if (!dictionary.TryGetValue(entry, out ConfigurationValueEntry result))
                {
                    if (entry.Value is null)
                    {
                        continue;
                    }

                    if (!Registry.SetValue(entry.Key, entry.Value, entry.Sections))
                    {
                        continue;
                    }

                    changes?.Add(entry);
                    continue;
                }

                if (entry.Value == result.Value)
                {
                    continue;
                }

                if (entry.Value is null)
                {
                    if (!Registry.RemoveKey(entry.Key, entry.Sections))
                    {
                        continue;
                    }

                    changes?.Add(entry);
                    continue;
                }

                if (!Registry.SetValue(entry.Key, entry.Value, entry.Sections))
                {
                    continue;
                }

                changes?.Add(entry);
            }

            if (changes is null)
            {
                return true;
            }

            foreach (ConfigurationValueEntry change in changes)
            {
                OnChanged(change);
            }

            return true;
        }

        // ReSharper disable once CognitiveComplexity
        public override Boolean Replace(IEnumerable<ConfigurationValueEntry>? entries)
        {
            if (IsReadOnly || Registry.IsReadOnly)
            {
                return false;
            }

            if (entries is null)
            {
                return false;
            }

            ConfigurationValueEntry[]? values = GetExistsValues(null);

            if (values is null || values.Length <= 0)
            {
                return Merge(entries);
            }

            IndexDictionary<ConfigurationEntry, ConfigurationValueEntry> dictionary = values.ToIndexDictionary(item => (ConfigurationEntry) item, item => item);
            List<ConfigurationValueEntry>? changes = !IsIgnoreEvent ? new List<ConfigurationValueEntry>(dictionary.Count) : null;

            foreach (ConfigurationValueEntry entry in entries.DistinctLastBy(item => (ConfigurationEntry) item))
            {
                if (entry.Key is null)
                {
                    continue;
                }

                if (!dictionary.Remove(entry, out ConfigurationValueEntry result))
                {
                    if (entry.Value is null)
                    {
                        continue;
                    }

                    if (!Registry.SetValue(entry.Key, entry.Value, entry.Sections))
                    {
                        continue;
                    }

                    changes?.Add(entry);
                    continue;
                }

                if (entry.Value == result.Value)
                {
                    continue;
                }

                if (entry.Value is null)
                {
                    if (!Registry.RemoveKey(entry.Key, entry.Sections))
                    {
                        continue;
                    }

                    changes?.Add(entry);
                    continue;
                }

                if (!Registry.SetValue(entry.Key, entry.Value, entry.Sections))
                {
                    continue;
                }

                changes?.Add(entry);
            }

            foreach ((String? key, ImmutableArray<String> sections) in dictionary.Values())
            {
                if (key is null)
                {
                    continue;
                }

                if (!Registry.RemoveKey(key, sections))
                {
                    continue;
                }

                changes?.Add(new ConfigurationValueEntry(key, null, sections));
            }

            if (changes is null)
            {
                return true;
            }

            foreach (ConfigurationValueEntry change in changes)
            {
                OnChanged(change);
            }

            return true;
        }
    }
}