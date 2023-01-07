// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using NetExtender.Configuration.Common;

namespace NetExtender.Configuration.Interfaces
{
    public interface IConfigInfo
    {
        public event ConfigurationChangedEventHandler Changed;
        
        public String Path { get; }
        public ConfigOptions Options { get; }
        public Boolean IsReadOnly { get; }
        public Boolean IsLazyWrite { get; }
        public Boolean IsThreadSafe { get; }

        public ConfigurationEntry[]? GetExists();
        public Task<ConfigurationEntry[]?> GetExistsAsync();
        public Task<ConfigurationEntry[]?> GetExistsAsync(CancellationToken token);
        public ConfigurationEntry[]? GetExists(params String[]? sections);
        public ConfigurationEntry[]? GetExists(IEnumerable<String>? sections);
        public Task<ConfigurationEntry[]?> GetExistsAsync(params String[]? sections);
        public Task<ConfigurationEntry[]?> GetExistsAsync(IEnumerable<String>? sections);
        public Task<ConfigurationEntry[]?> GetExistsAsync(CancellationToken token, params String[]? sections);
        public Task<ConfigurationEntry[]?> GetExistsAsync(IEnumerable<String>? sections, CancellationToken token);
        public ConfigurationValueEntry[]? GetExistsValues();
        public Task<ConfigurationValueEntry[]?> GetExistsValuesAsync();
        public Task<ConfigurationValueEntry[]?> GetExistsValuesAsync(CancellationToken token);
        public ConfigurationValueEntry[]? GetExistsValues(params String[]? sections);
        public ConfigurationValueEntry[]? GetExistsValues(IEnumerable<String>? sections);
        public Task<ConfigurationValueEntry[]?> GetExistsValuesAsync(params String[]? sections);
        public Task<ConfigurationValueEntry[]?> GetExistsValuesAsync(IEnumerable<String>? sections);
        public Task<ConfigurationValueEntry[]?> GetExistsValuesAsync(CancellationToken token, params String[]? sections);
        public Task<ConfigurationValueEntry[]?> GetExistsValuesAsync(IEnumerable<String>? sections, CancellationToken token);
    }
}