// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using NetExtender.Configuration.Behavior;
using NetExtender.Configuration.Common;
using NetExtender.Types.Dictionaries;
using NetExtender.Types.Environments;
using NetExtender.Utilities.Application;
using NetExtender.Utilities.Types;

namespace NetExtender.Configuration.Environment
{
    public class EnvironmentConfigBehavior : ConfigBehavior
    {
        public EnvironmentVariableTarget Target { get; init; } = EnvironmentVariableTarget.Process;

        public EnvironmentConfigBehavior()
            : this(ConfigOptions.None)
        {
        }

        public EnvironmentConfigBehavior(ConfigOptions options)
            : this(null, options)
        {
        }

        public EnvironmentConfigBehavior(String? path)
            : this(path, ConfigOptions.None)
        {
        }

        public EnvironmentConfigBehavior(String? path, ConfigOptions options)
            : base(path ?? nameof(System.Environment), options)
        {
        }

        [return: NotNullIfNotNull("key")]
        protected virtual String? Join(String? key, IEnumerable<String>? sections)
        {
            if (key is null)
            {
                return null;
            }

            return sections is not null ? Joiner.Join(sections.Append(key)) : key;
        }

        protected virtual Boolean Deconstruct(String? entry, [NotNullIfNotNull("entry")] out String? key, out IEnumerable<String>? sections)
        {
            if (entry is null)
            {
                key = default;
                sections = default;
                return true;
            }

            String[] split = entry.Split(Joiner);
            switch (split.Length)
            {
                case 0:
                    key = String.Empty;
                    sections = default;
                    return true;
                case 1:
                    key = split[0];
                    sections = default;
                    return true;
                default:
                    key = split[0];
                    sections = split.Skip(1);
                    return true;
            }
        }

        public override String? Get(String? key, IEnumerable<String>? sections)
        {
            try
            {
                key = Join(key, sections);
                return key is not null ? EnvironmentUtilities.TryGetEnvironmentVariable(key, Target) : null;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public override Boolean Set(String? key, String? value, IEnumerable<String>? sections)
        {
            if (IsReadOnly)
            {
                return false;
            }

            try
            {
                if (IsIgnoreEvent && !IsLazyWrite)
                {
                    key = Join(key, sections);

                    if (key is null)
                    {
                        return false;
                    }

                    return EnvironmentUtilities.TrySetEnvironmentVariable(key, value, Target);
                }

                sections = ToSection(sections).AsIImmutableList();

                if (IsLazyWrite && Get(key, sections) == value)
                {
                    return true;
                }

                key = Join(key, sections);

                if (key is null)
                {
                    return false;
                }

                EnvironmentUtilities.TrySetEnvironmentVariable(key, value, Target);

                if (!IsIgnoreEvent)
                {
                    OnChanged(new ConfigurationValueEntry(key, value, sections));
                }

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        protected virtual ConfigurationEntry EntriesConvert(String entry)
        {
            return new ConfigurationEntry(entry);
        }

        protected virtual ConfigurationValueEntry ValueEntriesConvert(EnvironmentValueEntry entry)
        {
            return new ConfigurationValueEntry(entry.Key, entry.Value);
        }

        public override ConfigurationEntry[]? GetExists(IEnumerable<String>? sections)
        {
            try
            {
                if (sections is null)
                {
                    return EnvironmentUtilities.TryGetExistsEnvironmentVariables(Target)?.Select(EntriesConvert).ToArray();
                }

                sections = sections.Materialize(out Int32 count);

                Boolean IsEqualSections(String entry)
                {
                    return Deconstruct(entry, out _, out IEnumerable<String>? sequence) && (sequence?.SequencePartialEqual(sections) ?? count <= 0);
                }

                return EnvironmentUtilities.TryGetExistsEnvironmentVariables(Target)?.Where(IsEqualSections).Select(EntriesConvert).ToArray();
            }
            catch (Exception)
            {
                return null;
            }
        }

        public override ConfigurationValueEntry[]? GetExistsValues(IEnumerable<String>? sections)
        {
            try
            {
                if (sections is null)
                {
                    return EnvironmentUtilities.TryGetExistsValuesEnvironmentVariables(Target)?.Select(ValueEntriesConvert).ToArray();
                }

                sections = sections.Materialize(out Int32 count);

                Boolean IsEqualSections(EnvironmentValueEntry entry)
                {
                    return Deconstruct(entry.Key, out _, out IEnumerable<String>? sequence) && (sequence?.SequencePartialEqual(sections) ?? count <= 0);
                }

                return EnvironmentUtilities.TryGetExistsValuesEnvironmentVariables(Target)?.Where(IsEqualSections).Select(ValueEntriesConvert).ToArray();
            }
            catch (Exception)
            {
                return null;
            }
        }

        public override Boolean Clear(IEnumerable<String>? sections)
        {
            if (IsReadOnly)
            {
                return false;
            }

            ConfigurationEntry[]? exists = GetExists(sections);

            if (exists is null)
            {
                return false;
            }

            Boolean successful = false;
            foreach (ConfigurationEntry key in exists)
            {
                successful |= Set(key.Key, null, key.Sections);
            }

            return successful;
        }

        public override Boolean Reload()
        {
            return false;
        }

        // ReSharper disable once CognitiveComplexity
        public override Boolean Merge(IEnumerable<ConfigurationValueEntry>? entries)
        {
            if (IsReadOnly)
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

                    if (!EnvironmentUtilities.TrySetEnvironmentVariable(Join(entry.Key, entry.Sections), entry.Value, Target))
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
                    if (!EnvironmentUtilities.TryRemoveEnvironmentVariable(Join(entry.Key, entry.Sections), Target))
                    {
                        continue;
                    }

                    changes?.Add(entry);
                    continue;
                }

                if (!EnvironmentUtilities.TrySetEnvironmentVariable(Join(entry.Key, entry.Sections), entry.Value, Target))
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
            if (IsReadOnly)
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

                    if (!EnvironmentUtilities.TrySetEnvironmentVariable(Join(entry.Key, entry.Sections), entry.Value, Target))
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
                    if (!EnvironmentUtilities.TryRemoveEnvironmentVariable(Join(entry.Key, entry.Sections), Target))
                    {
                        continue;
                    }

                    changes?.Add(entry);
                    continue;
                }

                if (!EnvironmentUtilities.TrySetEnvironmentVariable(Join(entry.Key, entry.Sections), entry.Value, Target))
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

                if (!EnvironmentUtilities.TryRemoveEnvironmentVariable(Join(key, sections), Target))
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