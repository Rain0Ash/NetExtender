// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using NetExtender.Configuration.Common;
using NetExtender.Configuration.Transactions.Interfaces;

namespace NetExtender.Configuration.Interfaces
{
    public interface IConfig : IConfigInfo, IEnumerable<ConfigurationEntry>, IDisposable, IAsyncDisposable
    {
        public event ConfigurationChangedEventHandler Changed;

        public String? GetValue(String? key, params String[]? sections);
        public String? GetValue(String? key, IEnumerable<String>? sections);
        public String? GetValue(String? key, String? alternate, params String[]? sections);
        public String? GetValue(String? key, String? alternate, IEnumerable<String>? sections);
        public Task<String?> GetValueAsync(String? key, params String[]? sections);
        public Task<String?> GetValueAsync(String? key, IEnumerable<String>? sections);
        public Task<String?> GetValueAsync(String? key, CancellationToken token, params String[]? sections);
        public Task<String?> GetValueAsync(String? key, IEnumerable<String>? sections, CancellationToken token);
        public Task<String?> GetValueAsync(String? key, String? alternate, params String[]? sections);
        public Task<String?> GetValueAsync(String? key, String? alternate, IEnumerable<String>? sections);
        public Task<String?> GetValueAsync(String? key, String? alternate, CancellationToken token, params String[]? sections);
        public Task<String?> GetValueAsync(String? key, String? alternate, IEnumerable<String>? sections, CancellationToken token);
        public Boolean SetValue(String? key, String? value, params String[]? sections);
        public Boolean SetValue(String? key, String? value, IEnumerable<String>? sections);
        public Task<Boolean> SetValueAsync(String? key, String? value, params String[]? sections);
        public Task<Boolean> SetValueAsync(String? key, String? value, IEnumerable<String>? sections);
        public Task<Boolean> SetValueAsync(String? key, String? value, CancellationToken token, params String[]? sections);
        public Task<Boolean> SetValueAsync(String? key, String? value, IEnumerable<String>? sections, CancellationToken token);
        public String? GetOrSetValue(String? key, String? value, params String[]? sections);
        public String? GetOrSetValue(String? key, String? value, IEnumerable<String>? sections);
        public Task<String?> GetOrSetValueAsync(String? key, String? value, params String[]? sections);
        public Task<String?> GetOrSetValueAsync(String? key, String? value, IEnumerable<String>? sections);
        public Task<String?> GetOrSetValueAsync(String? key, String? value, CancellationToken token, params String[]? sections);
        public Task<String?> GetOrSetValueAsync(String? key, String? value, IEnumerable<String>? sections, CancellationToken token);
        public Boolean RemoveValue(String? key, params String[]? sections);
        public Boolean RemoveValue(String? key, IEnumerable<String>? sections);
        public Task<Boolean> RemoveValueAsync(String? key, params String[]? sections);
        public Task<Boolean> RemoveValueAsync(String? key, IEnumerable<String>? sections);
        public Task<Boolean> RemoveValueAsync(String? key, CancellationToken token, params String[]? sections);
        public Task<Boolean> RemoveValueAsync(String? key, IEnumerable<String>? sections, CancellationToken token);
        public Boolean KeyExist(String? key, params String[]? sections);
        public Boolean KeyExist(String? key, IEnumerable<String>? sections);
        public Task<Boolean> KeyExistAsync(String? key, params String[]? sections);
        public Task<Boolean> KeyExistAsync(String? key, IEnumerable<String>? sections);
        public Task<Boolean> KeyExistAsync(String? key, CancellationToken token, params String[]? sections);
        public Task<Boolean> KeyExistAsync(String? key, IEnumerable<String>? sections, CancellationToken token);
        public Boolean Clear();
        public Boolean Clear(params String[]? sections);
        public Boolean Clear(IEnumerable<String>? sections);
        public Task<Boolean> ClearAsync();
        public Task<Boolean> ClearAsync(params String[]? sections);
        public Task<Boolean> ClearAsync(IEnumerable<String>? sections);
        public Task<Boolean> ClearAsync(CancellationToken token);
        public Task<Boolean> ClearAsync(CancellationToken token, params String[]? sections);
        public Task<Boolean> ClearAsync(IEnumerable<String>? sections, CancellationToken token);
        public Boolean Reload();
        public Task<Boolean> ReloadAsync();
        public Task<Boolean> ReloadAsync(CancellationToken token);
        public Boolean Reset();
        public Task<Boolean> ResetAsync();
        public Task<Boolean> ResetAsync(CancellationToken token);
        public Boolean Merge(IEnumerable<ConfigurationValueEntry>? entries);
        public Task<Boolean> MergeAsync(IEnumerable<ConfigurationValueEntry>? entries);
        public Task<Boolean> MergeAsync(IEnumerable<ConfigurationValueEntry>? entries, CancellationToken token);
        public Boolean Replace(IEnumerable<ConfigurationValueEntry>? entries);
        public Task<Boolean> ReplaceAsync(IEnumerable<ConfigurationValueEntry>? entries);
        public Task<Boolean> ReplaceAsync(IEnumerable<ConfigurationValueEntry>? entries, CancellationToken token);
        public ConfigurationValueEntry[]? Difference(IEnumerable<ConfigurationValueEntry>? entries);
        public Task<ConfigurationValueEntry[]?> DifferenceAsync(IEnumerable<ConfigurationValueEntry>? entries);
        public Task<ConfigurationValueEntry[]?> DifferenceAsync(IEnumerable<ConfigurationValueEntry>? entries, CancellationToken token);
        public IConfigTransaction? Transaction();
        public Task<IConfigTransaction?> TransactionAsync();
        public Task<IConfigTransaction?> TransactionAsync(CancellationToken token);
        public void CopyTo(IConfig config);
        public Task CopyToAsync(IConfig config);
        public Task CopyToAsync(IConfig config, CancellationToken token);
        public String? this[String? key, params String[]? sections] { get; set; }
        public String? this[String? key, IEnumerable<String>? sections] { get; set; }
    }
}