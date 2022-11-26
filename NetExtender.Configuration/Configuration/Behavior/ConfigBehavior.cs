// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using NetExtender.Configuration.Behavior.Interfaces;
using NetExtender.Configuration.Behavior.Transactions;
using NetExtender.Configuration.Behavior.Transactions.Interfaces;
using NetExtender.Configuration.Common;
using NetExtender.Configuration.Memory;
using NetExtender.Types.Dictionaries;
using NetExtender.Utilities.Application;
using NetExtender.Utilities.IO;
using NetExtender.Utilities.Types;

namespace NetExtender.Configuration.Behavior
{
    public abstract class ConfigBehavior : IConfigBehavior
    {
        protected const String DefaultName = "config";

        private static String GetDefaultPath(String? extension)
        {
            String filename = DefaultName;
            if (!String.IsNullOrEmpty(extension))
            {
                filename += extension.StartsWith('.') ? extension : '.' + extension;
            }

            return System.IO.Path.Combine(ApplicationUtilities.Directory ?? Environment.CurrentDirectory, filename);
        }

        protected static String ValidatePathOrGetDefault(String? path, String? extension)
        {
            return !String.IsNullOrWhiteSpace(path) && PathUtilities.IsValidFilePath(path) ? path : GetDefaultPath(extension);
        }

        public event ConfigurationChangedEventHandler? Changed;
        public String Path { get; }
        public ConfigOptions Options { get; }

        public Boolean IsReadOnly
        {
            get
            {
                return Options.HasFlag(ConfigOptions.ReadOnly);
            }
        }

        public Boolean IsIgnoreEvent
        {
            get
            {
                return Options.HasFlag(ConfigOptions.IgnoreEvent);
            }
        }

        public Boolean IsLazyWrite
        {
            get
            {
                return Options.HasFlag(ConfigOptions.LazyWrite);
            }
        }

        public virtual Boolean IsThreadSafe
        {
            get
            {
                return false;
            }
        }

        public const String DefaultJoiner = ".";
        public String Joiner { get; init; } = DefaultJoiner;

        protected ConfigBehavior(String path, ConfigOptions options)
        {
            Path = path ?? throw new ArgumentNullException(nameof(path));
            Options = options;
        }

        [return: NotNullIfNotNull("sections")]
        protected virtual IEnumerable<String>? ToSection(IEnumerable<String>? sections)
        {
            return sections;
        }

        [return: NotNullIfNotNull("key")]
        protected virtual IEnumerable<String>? ToSection<T>(String? key, IEnumerable<String>? sections)
        {
            if (key is null)
            {
                return default;
            }

            return sections is null ? new[] { key } : ToSection(sections.Append(key));
        }

        protected virtual String? FromSection(ref IEnumerable<String>? sections)
        {
            if (sections is null)
            {
                return null;
            }

            List<String> collection = sections.ToList();

            if (collection.Count <= 0)
            {
                return null;
            }

            String key = collection[^1];
            collection.RemoveAt(collection.Count - 1);
            sections = collection;
            return key;
        }

        public virtual Boolean Contains(String? key, IEnumerable<String>? sections)
        {
            return Get(key, sections) is not null;
        }

        public abstract String? Get(String? key, IEnumerable<String>? sections);
        public abstract Boolean Set(String? key, String? value, IEnumerable<String>? sections);

        public virtual String? GetOrSet(String? key, String? value, IEnumerable<String>? sections)
        {
            sections = sections.Materialize();

            String? result = Get(key, sections);

            if (result is not null)
            {
                return result;
            }

            return value is not null && Set(key, value, sections) ? value : null;
        }

        public virtual Task<Boolean> ContainsAsync(String? key, IEnumerable<String>? sections, CancellationToken token)
        {
            return Contains(key, sections).ToTask();
        }

        public virtual Task<String?> GetAsync(String? key, IEnumerable<String>? sections, CancellationToken token)
        {
            return Get(key, sections).ToTask();
        }

        public virtual Task<Boolean> SetAsync(String? key, String? value, IEnumerable<String>? sections, CancellationToken token)
        {
            return Set(key, value, sections).ToTask();
        }

        public virtual Task<String?> GetOrSetAsync(String? key, String? value, IEnumerable<String>? sections, CancellationToken token)
        {
            return GetOrSet(key, value, sections).ToTask();
        }

        public abstract ConfigurationEntry[]? GetExists(IEnumerable<String>? sections);

        public virtual Task<ConfigurationEntry[]?> GetExistsAsync(IEnumerable<String>? sections, CancellationToken token)
        {
            return GetExists(sections).ToTask();
        }

        public abstract ConfigurationValueEntry[]? GetExistsValues(IEnumerable<String>? sections);

        public virtual Task<ConfigurationValueEntry[]?> GetExistsValuesAsync(IEnumerable<String>? sections, CancellationToken token)
        {
            return GetExistsValues(sections).ToTask();
        }

        public abstract Boolean Clear(IEnumerable<String>? sections);

        public virtual Task<Boolean> ClearAsync(IEnumerable<String>? sections, CancellationToken token)
        {
            return Clear(sections).ToTask();
        }

        public abstract Boolean Reload();

        public virtual Task<Boolean> ReloadAsync(CancellationToken token)
        {
            return Reload().ToTask();
        }

        public virtual Boolean Reset()
        {
            return false;
        }

        public virtual Task<Boolean> ResetAsync(CancellationToken token)
        {
            return Reset().ToTask();
        }

        public abstract Boolean Merge(IEnumerable<ConfigurationValueEntry>? entries);

        public virtual Task<Boolean> MergeAsync(IEnumerable<ConfigurationValueEntry>? entries, CancellationToken token)
        {
            return Merge(entries).ToTask();
        }

        public abstract Boolean Replace(IEnumerable<ConfigurationValueEntry>? entries);

        public virtual Task<Boolean> ReplaceAsync(IEnumerable<ConfigurationValueEntry>? entries, CancellationToken token)
        {
            return Replace(entries).ToTask();
        }

        public virtual ConfigurationValueEntry[]? Difference(IEnumerable<ConfigurationValueEntry>? entries)
        {
            ConfigurationValueEntry[]? values = GetExistsValues(null);

            if (entries is null)
            {
                return values;
            }

            if (values is null)
            {
                return entries.ToArray();
            }

            IDictionary<ConfigurationEntry, ConfigurationValueEntry> equality = new IndexDictionary<ConfigurationEntry, ConfigurationValueEntry>(values.Length);

            foreach (ConfigurationValueEntry entry in entries)
            {
                equality.TryAdd(entry, entry);
            }

            List<ConfigurationValueEntry> difference = new List<ConfigurationValueEntry>(equality.Count);

            foreach (ConfigurationValueEntry value in values)
            {
                if (!equality.TryGetValue(value, out ConfigurationValueEntry result))
                {
                    difference.Add(result);
                    continue;
                }

                if (value.Value == result.Value)
                {
                    continue;
                }

                difference.Add(result);
            }

            return difference.ToArray();
        }

        public virtual Task<ConfigurationValueEntry[]?> DifferenceAsync(IEnumerable<ConfigurationValueEntry>? entries, CancellationToken token)
        {
            return Difference(entries).ToTask();
        }

        public virtual IConfigBehaviorTransaction? Transaction()
        {
            if (IsReadOnly)
            {
                return null;
            }

            ConfigurationValueEntry[]? entries = GetExistsValues(null);

            IConfigBehavior transaction = new MemoryConfigBehavior(ConfigOptions.IgnoreEvent);

            transaction.Merge(entries);
            return new ConfigBehaviorTransaction(this, transaction);
        }

        public virtual async Task<IConfigBehaviorTransaction?> TransactionAsync(CancellationToken token)
        {
            if (IsReadOnly)
            {
                return null;
            }

            ConfigurationValueEntry[]? entries = await GetExistsValuesAsync(null, token);

            IConfigBehavior transaction = new MemoryConfigBehavior(ConfigOptions.IgnoreEvent);

            await transaction.MergeAsync(entries, token);
            return new ConfigBehaviorTransaction(this, transaction);
        }

        protected void OnChanged(ConfigurationValueEntry entry)
        {
            Changed?.Invoke(this, new ConfigurationChangedEventArgs(entry));
        }

        public virtual IEnumerator<ConfigurationValueEntry> GetEnumerator()
        {
            ConfigurationValueEntry[]? values = GetExistsValues(null);

            if (values is null)
            {
                yield break;
            }

            foreach (ConfigurationValueEntry item in values)
            {
                yield return item;
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        public async ValueTask DisposeAsync()
        {
            await DisposeAsync(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(Boolean disposing)
        {
        }

        protected virtual ValueTask DisposeAsync(Boolean disposing)
        {
            return ValueTask.CompletedTask;
        }
    }
}