// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using NetExtender.Configuration.Behavior.Interfaces;
using NetExtender.Configuration.Behavior.Transactions.Interfaces;
using NetExtender.Configuration.Common;

namespace NetExtender.Configuration.Wrappers
{
    internal sealed class TemporaryConfigurationBehaviorWrapper : IConfigBehavior
    {
        private IConfigBehavior Internal { get; }

        public event ConfigurationChangedEventHandler? Changed
        {
            add
            {
                Internal.Changed += value;
            }
            remove
            {
                Internal.Changed -= value;
            }
        }

        public String Path
        {
            get
            {
                return Internal.Path;
            }
        }

        public ConfigOptions Options
        {
            get
            {
                return Internal.Options;
            }
        }

        public Boolean IsReadOnly
        {
            get
            {
                return Internal.IsReadOnly;
            }
        }

        public Boolean IsIgnoreEvent
        {
            get
            {
                return Internal.IsIgnoreEvent;
            }
        }

        public Boolean IsLazyWrite
        {
            get
            {
                return Internal.IsLazyWrite;
            }
        }

        public Boolean IsThreadSafe
        {
            get
            {
                return Internal.IsThreadSafe;
            }
        }

        public String Joiner
        {
            get
            {
                return Internal.Joiner;
            }
        }

        public TemporaryConfigurationBehaviorWrapper(IConfigBehavior behavior)
        {
            Internal = behavior ?? throw new ArgumentNullException(nameof(behavior));
        }

        public Boolean Contains(String? key, IEnumerable<String>? sections)
        {
            return Internal.Contains(key, sections);
        }

        public Task<Boolean> ContainsAsync(String? key, IEnumerable<String>? sections, CancellationToken token)
        {
            return Internal.ContainsAsync(key, sections, token);
        }

        public String? Get(String? key, IEnumerable<String>? sections)
        {
            return Internal.Get(key, sections);
        }

        public Task<String?> GetAsync(String? key, IEnumerable<String>? sections, CancellationToken token)
        {
            return Internal.GetAsync(key, sections, token);
        }

        public Boolean Set(String? key, String? value, IEnumerable<String>? sections)
        {
            return Internal.Set(key, value, sections);
        }

        public Task<Boolean> SetAsync(String? key, String? value, IEnumerable<String>? sections, CancellationToken token)
        {
            return Internal.SetAsync(key, value, sections, token);
        }

        public String? GetOrSet(String? key, String? value, IEnumerable<String>? sections)
        {
            return Internal.GetOrSet(key, value, sections);
        }

        public Task<String?> GetOrSetAsync(String? key, String? value, IEnumerable<String>? sections, CancellationToken token)
        {
            return Internal.GetOrSetAsync(key, value, sections, token);
        }

        public ConfigurationEntry[]? GetExists()
        {
            return GetExists(null);
        }

        public Task<ConfigurationEntry[]?> GetExistsAsync(CancellationToken token)
        {
            return GetExistsAsync(null, token);
        }

        public ConfigurationEntry[]? GetExists(IEnumerable<String>? sections)
        {
            return Internal.GetExists(sections);
        }

        public Task<ConfigurationEntry[]?> GetExistsAsync(IEnumerable<String>? sections, CancellationToken token)
        {
            return Internal.GetExistsAsync(sections, token);
        }

        public ConfigurationValueEntry[]? GetExistsValues()
        {
            return GetExistsValues(null);
        }

        public Task<ConfigurationValueEntry[]?> GetExistsValuesAsync(CancellationToken token)
        {
            return GetExistsValuesAsync(null, token);
        }

        public ConfigurationValueEntry[]? GetExistsValues(IEnumerable<String>? sections)
        {
            return Internal.GetExistsValues(sections);
        }

        public Task<ConfigurationValueEntry[]?> GetExistsValuesAsync(IEnumerable<String>? sections, CancellationToken token)
        {
            return Internal.GetExistsValuesAsync(sections, token);
        }

        public Boolean Clear()
        {
            return Clear(null);
        }

        public Boolean Clear(IEnumerable<String>? sections)
        {
            return Internal.Clear(sections);
        }

        public Task<Boolean> ClearAsync(CancellationToken token)
        {
            return ClearAsync(null, token);
        }

        public Task<Boolean> ClearAsync(IEnumerable<String>? sections, CancellationToken token)
        {
            return Internal.ClearAsync(sections, token);
        }

        public Boolean Reload()
        {
            return Internal.Reload();
        }

        public Task<Boolean> ReloadAsync(CancellationToken token)
        {
            return Internal.ReloadAsync(token);
        }

        public Boolean Reset()
        {
            return Internal.Reset();
        }

        public Task<Boolean> ResetAsync(CancellationToken token)
        {
            return Internal.ResetAsync(token);
        }

        public Boolean Merge(IEnumerable<ConfigurationValueEntry>? entries)
        {
            return Internal.Merge(entries);
        }

        public Task<Boolean> MergeAsync(IEnumerable<ConfigurationValueEntry>? entries, CancellationToken token)
        {
            return Internal.MergeAsync(entries, token);
        }

        public Boolean Replace(IEnumerable<ConfigurationValueEntry>?entries)
        {
            return Internal.Replace(entries);
        }

        public Task<Boolean> ReplaceAsync(IEnumerable<ConfigurationValueEntry>? entries, CancellationToken token)
        {
            return Internal.ReplaceAsync(entries, token);
        }

        public ConfigurationValueEntry[]? Difference(IEnumerable<ConfigurationValueEntry>? entries)
        {
            return Internal.Difference(entries);
        }

        public Task<ConfigurationValueEntry[]?> DifferenceAsync(IEnumerable<ConfigurationValueEntry>? entries, CancellationToken token)
        {
            return Internal.DifferenceAsync(entries, token);
        }

        public IConfigBehaviorTransaction? Transaction()
        {
            return Internal.Transaction();
        }

        public Task<IConfigBehaviorTransaction?> TransactionAsync(CancellationToken token)
        {
            return Internal.TransactionAsync(token);
        }

        public void Dispose()
        {
            Internal.Reset();
        }

        public async ValueTask DisposeAsync()
        {
            await Internal.ResetAsync(CancellationToken.None);
        }

        public IEnumerator<ConfigurationValueEntry> GetEnumerator()
        {
            return Internal.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return ((IEnumerable) Internal).GetEnumerator();
        }
    }
}