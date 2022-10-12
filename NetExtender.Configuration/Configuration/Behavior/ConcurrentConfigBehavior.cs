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
using NetExtender.Utilities.Types;

namespace NetExtender.Configuration.Wrappers
{
    internal sealed class ConcurrentConfigBehavior : IConfigBehavior
    {
        private Object Synchronization { get; }
        private IConfigBehavior Internal { get; }

        public event ConfigurationChangedEventHandler? Changed
        {
            add
            {
                lock (Synchronization)
                {
                    Internal.Changed += value;
                }
            }
            remove
            {
                lock (Synchronization)
                {
                    Internal.Changed -= value;
                }
            }
        }

        public String Path
        {
            get
            {
                lock (Synchronization)
                {
                    return Internal.Path;
                }
            }
        }

        public ConfigOptions Options
        {
            get
            {
                lock (Synchronization)
                {
                    return Internal.Options;
                }
            }
        }

        public Boolean IsReadOnly
        {
            get
            {
                lock (Synchronization)
                {
                    return Internal.IsReadOnly;
                }
            }
        }

        public Boolean IsIgnoreEvent
        {
            get
            {
                lock (Synchronization)
                {
                    return Internal.IsIgnoreEvent;
                }
            }
        }

        public Boolean IsLazyWrite
        {
            get
            {
                lock (Synchronization)
                {
                    return Internal.IsLazyWrite;
                }
            }
        }

        public Boolean IsThreadSafe
        {
            get
            {
                return true;
            }
        }

        public String Joiner
        {
            get
            {
                lock (Synchronization)
                {
                    return Internal.Joiner;
                }
            }
        }

        public ConcurrentConfigBehavior(IConfigBehavior behavior)
            : this(behavior, null)
        {
        }

        public ConcurrentConfigBehavior(IConfigBehavior behavior, Object? synchronization)
        {
            Internal = behavior ?? throw new ArgumentNullException(nameof(behavior));
            Synchronization = synchronization ?? behavior;
        }

        public Boolean Contains(String? key, IEnumerable<String>? sections)
        {
            lock (Synchronization)
            {
                return Internal.Contains(key, sections);
            }
        }

        public Task<Boolean> ContainsAsync(String? key, IEnumerable<String>? sections, CancellationToken token)
        {
            lock (Synchronization)
            {
                return Internal.ContainsAsync(key, sections, token);
            }
        }

        public String? Get(String? key, IEnumerable<String>? sections)
        {
            lock (Synchronization)
            {
                return Internal.Get(key, sections);
            }
        }

        public Task<String?> GetAsync(String? key, IEnumerable<String>? sections, CancellationToken token)
        {
            lock (Synchronization)
            {
                return Internal.GetAsync(key, sections, token);
            }
        }

        public Boolean Set(String? key, String? value, IEnumerable<String>? sections)
        {
            lock (Synchronization)
            {
                return Internal.Set(key, value, sections);
            }
        }

        public Task<Boolean> SetAsync(String? key, String? value, IEnumerable<String>? sections, CancellationToken token)
        {
            lock (Synchronization)
            {
                return Internal.SetAsync(key, value, sections, token);
            }
        }

        public String? GetOrSet(String? key, String? value, IEnumerable<String>? sections)
        {
            lock (Synchronization)
            {
                return Internal.GetOrSet(key, value, sections);
            }
        }

        public Task<String?> GetOrSetAsync(String? key, String? value, IEnumerable<String>? sections, CancellationToken token)
        {
            lock (Synchronization)
            {
                return Internal.GetOrSetAsync(key, value, sections, token);
            }
        }

        public ConfigurationEntry[]? GetExists(IEnumerable<String>? sections)
        {
            lock (Synchronization)
            {
                return Internal.GetExists(sections);
            }
        }

        public Task<ConfigurationEntry[]?> GetExistsAsync(IEnumerable<String>? sections, CancellationToken token)
        {
            lock (Synchronization)
            {
                return Internal.GetExistsAsync(sections, token);
            }
        }

        public ConfigurationValueEntry[]? GetExistsValues(IEnumerable<String>? sections)
        {
            lock (Synchronization)
            {
                return Internal.GetExistsValues(sections);
            }
        }

        public Task<ConfigurationValueEntry[]?> GetExistsValuesAsync(IEnumerable<String>? sections, CancellationToken token)
        {
            lock (Synchronization)
            {
                return Internal.GetExistsValuesAsync(sections, token);
            }
        }

        public Boolean Clear()
        {
            return Clear(null);
        }

        public Boolean Clear(IEnumerable<String>? sections)
        {
            lock (Synchronization)
            {
                return Internal.Clear(sections);
            }
        }

        public Task<Boolean> ClearAsync(CancellationToken token)
        {
            return ClearAsync(null, token);
        }

        public Task<Boolean> ClearAsync(IEnumerable<String>? sections, CancellationToken token)
        {
            lock (Synchronization)
            {
                return Internal.ClearAsync(sections, token);
            }
        }

        public Boolean Reload()
        {
            lock (Synchronization)
            {
                return Internal.Reload();
            }
        }

        public Task<Boolean> ReloadAsync(CancellationToken token)
        {
            lock (Synchronization)
            {
                return Internal.ReloadAsync(token);
            }
        }

        public Boolean Reset()
        {
            lock (Synchronization)
            {
                return Internal.Reset();
            }
        }

        public Task<Boolean> ResetAsync(CancellationToken token)
        {
            lock (Synchronization)
            {
                return Internal.ResetAsync(token);
            }
        }

        public Boolean Merge(IEnumerable<ConfigurationValueEntry>? entries)
        {
            lock (Synchronization)
            {
                return Internal.Merge(entries);
            }
        }

        public Task<Boolean> MergeAsync(IEnumerable<ConfigurationValueEntry>? entries, CancellationToken token)
        {
            lock (Synchronization)
            {
                return Internal.MergeAsync(entries, token);
            }
        }

        public Boolean Replace(IEnumerable<ConfigurationValueEntry>? entries)
        {
            lock (Synchronization)
            {
                return Internal.Replace(entries);
            }
        }

        public Task<Boolean> ReplaceAsync(IEnumerable<ConfigurationValueEntry>? entries, CancellationToken token)
        {
            lock (Synchronization)
            {
                return Internal.ReplaceAsync(entries, token);
            }
        }

        public ConfigurationValueEntry[]? Difference(IEnumerable<ConfigurationValueEntry>? entries)
        {
            lock (Synchronization)
            {
                return Internal.Difference(entries);
            }
        }

        public Task<ConfigurationValueEntry[]?> DifferenceAsync(IEnumerable<ConfigurationValueEntry>? entries, CancellationToken token)
        {
            lock (Synchronization)
            {
                return Internal.DifferenceAsync(entries, token);
            }
        }

        public IConfigBehaviorTransaction? Transaction()
        {
            lock (Synchronization)
            {
                return Internal.Transaction();
            }
        }

        public Task<IConfigBehaviorTransaction?> TransactionAsync(CancellationToken token)
        {
            lock (Synchronization)
            {
                return Internal.TransactionAsync(token);
            }
        }

        public void Dispose()
        {
            lock (Synchronization)
            {
                Internal.Dispose();
            }
        }

        public ValueTask DisposeAsync()
        {
            lock (Synchronization)
            {
                return Internal.DisposeAsync();
            }
        }

        public IEnumerator<ConfigurationValueEntry> GetEnumerator()
        {
            lock (Synchronization)
            {
                return Internal.GetThreadSafeEnumerator();
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            lock (Synchronization)
            {
                return ((IEnumerable) Internal).GetThreadSafeEnumerator();
            }
        }
    }
}