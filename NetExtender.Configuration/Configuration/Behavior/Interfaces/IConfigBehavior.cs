// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using NetExtender.Configuration.Behavior.Transactions.Interfaces;
using NetExtender.Configuration.Common;
using NetExtender.Initializer.Types.Behavior.Interfaces;

namespace NetExtender.Configuration.Behavior.Interfaces
{
    public interface IConfigBehavior : IBehavior<ConfigOptions>, IEnumerable<ConfigurationValueEntry>, IDisposable, IAsyncDisposable
    {
        public event ConfigurationChangedEventHandler Changed;
        public String Path { get; }
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
        public Boolean Clear(IEnumerable<String>? sections);
        public Task<Boolean> ClearAsync(IEnumerable<String>? sections, CancellationToken token);
        public Boolean Reload();
        public Task<Boolean> ReloadAsync(CancellationToken token);
        public Boolean Reset();
        public Task<Boolean> ResetAsync(CancellationToken token);
        public Boolean Merge(IEnumerable<ConfigurationValueEntry>? entries);
        public Task<Boolean> MergeAsync(IEnumerable<ConfigurationValueEntry>? entries, CancellationToken token);
        public Boolean Replace(IEnumerable<ConfigurationValueEntry>? entries);
        public Task<Boolean> ReplaceAsync(IEnumerable<ConfigurationValueEntry>? entries, CancellationToken token);
        public ConfigurationValueEntry[]? Difference(IEnumerable<ConfigurationValueEntry>? entries);
        public Task<ConfigurationValueEntry[]?> DifferenceAsync(IEnumerable<ConfigurationValueEntry>? entries, CancellationToken token);
        public IConfigBehaviorTransaction? Transaction();
        public Task<IConfigBehaviorTransaction?> TransactionAsync(CancellationToken token);
    }
}