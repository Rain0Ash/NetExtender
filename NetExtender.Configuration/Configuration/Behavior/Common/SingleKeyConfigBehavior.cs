// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using NetExtender.Configuration.Common;
using NetExtender.Types.Dictionaries;
using NetExtender.Utilities.Types;

namespace NetExtender.Configuration.Behavior
{
    public abstract class SingleKeyConfigBehavior : ConfigBehavior
    {
        protected SingleKeyConfigBehavior(String path, ConfigOptions options)
            : base(path, options)
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

        protected abstract String? TryGetValue(String? key);
        protected abstract Task<String?> TryGetValueAsync(String? key, CancellationToken token);
        protected abstract Boolean TrySetValue(String? key, String? value);
        protected abstract Task<Boolean> TrySetValueAsync(String? key, String? value, CancellationToken token);
        protected abstract String[]? TryGetExists();
        protected abstract Task<String[]?> TryGetExistsAsync(CancellationToken token);
        protected abstract ConfigurationSingleKeyEntry[]? TryGetExistsValues();
        protected abstract Task<ConfigurationSingleKeyEntry[]?> TryGetExistsValuesAsync(CancellationToken token);

        public override String? Get(String? key, IEnumerable<String>? sections)
        {
            try
            {
                key = Join(key, sections);
                return key is not null ? TryGetValue(key) : null;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public override async Task<String?> GetAsync(String? key, IEnumerable<String>? sections, CancellationToken token)
        {
            try
            {
                key = Join(key, sections);
                return key is not null ? await TryGetValueAsync(key, token) : null;
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
                    return key is not null && TrySetValue(key, value);
                }

                sections = sections.AsIImmutableList();

                if (IsLazyWrite && Get(key, sections) == value)
                {
                    return true;
                }

                key = Join(key, sections);

                if (key is null)
                {
                    return false;
                }

                TrySetValue(key, value);

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

        public override async Task<Boolean> SetAsync(String? key, String? value, IEnumerable<String>? sections, CancellationToken token)
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
                    return key is not null && await TrySetValueAsync(key, value, token);
                }

                sections = sections.AsIImmutableList();

                if (IsLazyWrite && await GetAsync(key, sections, token) == value)
                {
                    return true;
                }

                key = Join(key, sections);

                if (key is null)
                {
                    return false;
                }

                await TrySetValueAsync(key, value, token);

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

        protected virtual ConfigurationValueEntry ValueEntriesConvert(ConfigurationSingleKeyEntry entry)
        {
            return new ConfigurationValueEntry(entry.Key, entry.Value);
        }

        public override ConfigurationEntry[]? GetExists(IEnumerable<String>? sections)
        {
            try
            {
                if (sections is null)
                {
                    return TryGetExists()?.Select(EntriesConvert).ToArray();
                }

                sections = sections.Materialize(out Int32 count);

                Boolean IsEqualSections(String entry)
                {
                    return Deconstruct(entry, out _, out IEnumerable<String>? sequence) && (sequence?.SequencePartialEqual(sections) ?? count <= 0);
                }

                return TryGetExists()?.Where(IsEqualSections).Select(EntriesConvert).ToArray();
            }
            catch (Exception)
            {
                return null;
            }
        }

        public override async Task<ConfigurationEntry[]?> GetExistsAsync(IEnumerable<String>? sections, CancellationToken token)
        {
            try
            {
                if (sections is null)
                {
                    return (await TryGetExistsAsync(token))?.Select(EntriesConvert).ToArray();
                }

                sections = sections.Materialize(out Int32 count);

                Boolean IsEqualSections(String entry)
                {
                    return Deconstruct(entry, out _, out IEnumerable<String>? sequence) && (sequence?.SequencePartialEqual(sections) ?? count <= 0);
                }

                return (await TryGetExistsAsync(token))?.Where(IsEqualSections).Select(EntriesConvert).ToArray();
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
                    return TryGetExistsValues()?.Select(ValueEntriesConvert).ToArray();
                }

                sections = sections.Materialize(out Int32 count);

                Boolean IsEqualSections(ConfigurationSingleKeyEntry entry)
                {
                    return Deconstruct(entry.Key, out _, out IEnumerable<String>? sequence) && (sequence?.SequencePartialEqual(sections) ?? count <= 0);
                }

                return TryGetExistsValues()?.Where(IsEqualSections).Select(ValueEntriesConvert).ToArray();
            }
            catch (Exception)
            {
                return null;
            }
        }

        public override async Task<ConfigurationValueEntry[]?> GetExistsValuesAsync(IEnumerable<String>? sections, CancellationToken token)
        {
            try
            {
                if (sections is null)
                {
                    return (await TryGetExistsValuesAsync(token))?.Select(ValueEntriesConvert).ToArray();
                }

                sections = sections.Materialize(out Int32 count);

                Boolean IsEqualSections(ConfigurationSingleKeyEntry entry)
                {
                    return Deconstruct(entry.Key, out _, out IEnumerable<String>? sequence) && (sequence?.SequencePartialEqual(sections) ?? count <= 0);
                }

                return (await TryGetExistsValuesAsync(token))?.Where(IsEqualSections).Select(ValueEntriesConvert).ToArray();
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
            return exists is not null && exists.Aggregate(false, (current, key) => current | Set(key.Key, null, key.Sections));
        }

        public override async Task<Boolean> ClearAsync(IEnumerable<String>? sections, CancellationToken token)
        {
            if (IsReadOnly)
            {
                return false;
            }

            ConfigurationEntry[]? exists = await GetExistsAsync(sections, token);

            if (exists is null)
            {
                return false;
            }
            
            Boolean result = false;
            // ReSharper disable once LoopCanBeConvertedToQuery
            foreach (ConfigurationEntry exist in exists)
            {
                result |= await SetAsync(exist.Key, null, exist.Sections, token);
            }

            return result;
        }

        public override Boolean Reload()
        {
            return false;
        }

        public override Task<Boolean> ReloadAsync(CancellationToken token)
        {
            return TaskUtilities.False;
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

                    if (!TrySetValue(Join(entry.Key, entry.Sections), entry.Value))
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
                    if (!TrySetValue(Join(entry.Key, entry.Sections), null))
                    {
                        continue;
                    }

                    changes?.Add(entry);
                    continue;
                }

                if (!TrySetValue(Join(entry.Key, entry.Sections), entry.Value))
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
        public override async Task<Boolean> MergeAsync(IEnumerable<ConfigurationValueEntry>? entries, CancellationToken token)
        {
            if (IsReadOnly)
            {
                return false;
            }

            if (entries is null)
            {
                return false;
            }

            ConfigurationValueEntry[]? values = await GetExistsValuesAsync(null, token);

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

                    if (!await TrySetValueAsync(Join(entry.Key, entry.Sections), entry.Value, token))
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
                    if (!await TrySetValueAsync(Join(entry.Key, entry.Sections), null, token))
                    {
                        continue;
                    }

                    changes?.Add(entry);
                    continue;
                }

                if (!await TrySetValueAsync(Join(entry.Key, entry.Sections), entry.Value, token))
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

                    if (!TrySetValue(Join(entry.Key, entry.Sections), entry.Value))
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
                    if (!TrySetValue(Join(entry.Key, entry.Sections), null))
                    {
                        continue;
                    }

                    changes?.Add(entry);
                    continue;
                }

                if (!TrySetValue(Join(entry.Key, entry.Sections), entry.Value))
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

                if (!TrySetValue(Join(key, sections), null))
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

        // ReSharper disable once CognitiveComplexity
        public override async Task<Boolean> ReplaceAsync(IEnumerable<ConfigurationValueEntry>? entries, CancellationToken token)
        {
            if (IsReadOnly)
            {
                return false;
            }

            if (entries is null)
            {
                return false;
            }

            ConfigurationValueEntry[]? values = await GetExistsValuesAsync(null, token);

            if (values is null || values.Length <= 0)
            {
                return await MergeAsync(entries, token);
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

                    if (!await TrySetValueAsync(Join(entry.Key, entry.Sections), entry.Value, token))
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
                    if (!await TrySetValueAsync(Join(entry.Key, entry.Sections), null, token))
                    {
                        continue;
                    }

                    changes?.Add(entry);
                    continue;
                }

                if (!await TrySetValueAsync(Join(entry.Key, entry.Sections), entry.Value, token))
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

                if (!await TrySetValueAsync(Join(key, sections), null, token))
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