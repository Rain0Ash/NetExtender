// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using NetExtender.Configuration.Common;

namespace NetExtender.Configuration.Interfaces
{
    public interface IConfig : IDisposable
    {
        public event EventHandler<ConfigurationEntry> Changed;
        public String Path { get; }
        public ConfigOptions Options { get; }
        public Boolean IsReadOnly { get; }
        public Boolean IsLazyWrite { get; }
        
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
        public ConfigurationEntry[]? GetExists();
        public Task<ConfigurationEntry[]?> GetExistsAsync();
        public Task<ConfigurationEntry[]?> GetExistsAsync(CancellationToken token);
        public Boolean Reload();
        public Task<Boolean> ReloadAsync();
        public Task<Boolean> ReloadAsync(CancellationToken token);
    }
}