// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using NetExtender.Configuration.Common;

namespace NetExtender.Configuration.Behavior.Interfaces
{
    public interface IConfigBehavior : IDisposable, IAsyncDisposable
    {
        public event ConfigurationChangedEventHandler Changed;
        public String Path { get; }
        public ConfigOptions Options { get; }
        public Boolean IsReadOnly { get; }
        public Boolean IsIgnoreEvent { get; }
        public Boolean IsLazyWrite { get; }
        public String Joiner { get; }
        public Boolean Contains(String? key, IEnumerable<String>? sections);
        public Task<Boolean> ContainsAsync(String? key, IEnumerable<String>? sections, CancellationToken token);
        public String? Get(String? key, IEnumerable<String>? sections);
        public Task<String?> GetAsync(String? key, IEnumerable<String>? sections, CancellationToken token);
        public Boolean Set(String? key, String? value, IEnumerable<String>? sections);
        public Task<Boolean> SetAsync(String? key, String? value, IEnumerable<String>? sections, CancellationToken token);
        public String? GetOrSet(String? key, String? value, IEnumerable<String>? sections);
        public Task<String?> GetOrSetAsync(String? key, String? value, IEnumerable<String>? sections, CancellationToken token);
        public ConfigurationEntry[]? GetExists(IEnumerable<String>? sections);
        public Task<ConfigurationEntry[]?> GetExistsAsync(IEnumerable<String>? sections, CancellationToken token);
        public ConfigurationValueEntry[]? GetExistsValues(IEnumerable<String>? sections);
        public Task<ConfigurationValueEntry[]?> GetExistsValuesAsync(IEnumerable<String>? sections, CancellationToken token);
        public Boolean Reload();
        public Task<Boolean> ReloadAsync(CancellationToken token);
        public Boolean Reset();
        public Task<Boolean> ResetAsync(CancellationToken token);
    }
}