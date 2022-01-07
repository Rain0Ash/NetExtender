// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using NetExtender.Configuration.Behavior.Interfaces;
using NetExtender.Configuration.Common;

namespace NetExtender.Configuration.Wrappers
{
    internal sealed class TemporaryConfigurationBehaviorWrapper : IConfigBehavior
    {
        private IConfigBehavior Behavior { get; }

        public event ConfigurationChangedEventHandler? Changed
        {
            add
            {
                Behavior.Changed += value;
            }
            remove
            {
                Behavior.Changed -= value;
            }
        }

        public String Path
        {
            get
            {
                return Behavior.Path;
            }
        }

        public ConfigOptions Options
        {
            get
            {
                return Behavior.Options;
            }
        }

        public Boolean IsReadOnly
        {
            get
            {
                return Behavior.IsReadOnly;
            }
        }

        public Boolean IsIgnoreEvent
        {
            get
            {
                return Behavior.IsIgnoreEvent;
            }
        }

        public Boolean IsLazyWrite
        {
            get
            {
                return Behavior.IsLazyWrite;
            }
        }

        public String Joiner
        {
            get
            {
                return Behavior.Joiner;
            }
        }

        public TemporaryConfigurationBehaviorWrapper(IConfigBehavior behavior)
        {
            Behavior = behavior ?? throw new ArgumentNullException(nameof(behavior));
        }

        public Boolean Contains(String? key, IEnumerable<String>? sections)
        {
            return Behavior.Contains(key, sections);
        }

        public Task<Boolean> ContainsAsync(String? key, IEnumerable<String>? sections, CancellationToken token)
        {
            return Behavior.ContainsAsync(key, sections, token);
        }

        public String? Get(String? key, IEnumerable<String>? sections)
        {
            return Behavior.Get(key, sections);
        }

        public Task<String?> GetAsync(String? key, IEnumerable<String>? sections, CancellationToken token)
        {
            return Behavior.GetAsync(key, sections, token);
        }

        public Boolean Set(String? key, String? value, IEnumerable<String>? sections)
        {
            return Behavior.Set(key, value, sections);
        }

        public Task<Boolean> SetAsync(String? key, String? value, IEnumerable<String>? sections, CancellationToken token)
        {
            return Behavior.SetAsync(key, value, sections, token);
        }

        public String? GetOrSet(String? key, String? value, IEnumerable<String>? sections)
        {
            return Behavior.GetOrSet(key, value, sections);
        }

        public Task<String?> GetOrSetAsync(String? key, String? value, IEnumerable<String>? sections, CancellationToken token)
        {
            return Behavior.GetOrSetAsync(key, value, sections, token);
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
            return Behavior.GetExists(sections);
        }

        public Task<ConfigurationEntry[]?> GetExistsAsync(IEnumerable<String>? sections, CancellationToken token)
        {
            return Behavior.GetExistsAsync(sections, token);
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
            return Behavior.GetExistsValues(sections);
        }

        public Task<ConfigurationValueEntry[]?> GetExistsValuesAsync(IEnumerable<String>? sections, CancellationToken token)
        {
            return Behavior.GetExistsValuesAsync(sections, token);
        }

        public Boolean Reload()
        {
            return Behavior.Reload();
        }

        public Task<Boolean> ReloadAsync(CancellationToken token)
        {
            return Behavior.ReloadAsync(token);
        }

        public Boolean Reset()
        {
            return Behavior.Reset();
        }

        public Task<Boolean> ResetAsync(CancellationToken token)
        {
            return Behavior.ResetAsync(token);
        }

        public void Dispose()
        {
            Behavior.Reset();
        }

        public async ValueTask DisposeAsync()
        {
            await Behavior.ResetAsync(CancellationToken.None);
        }
    }
}