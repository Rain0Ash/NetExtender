// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Threading;
using System.Threading.Tasks;
using NetExtender.Configuration.Common;

namespace NetExtender.Configuration.Interfaces
{
    public interface IConfigInfo
    {
        public String Path { get; }
        public ConfigOptions Options { get; }
        public Boolean IsReadOnly { get; }
        public Boolean IsLazyWrite { get; }
        
        public ConfigurationEntry[]? GetExists();
        public Task<ConfigurationEntry[]?> GetExistsAsync();
        public Task<ConfigurationEntry[]?> GetExistsAsync(CancellationToken token);
        public ConfigurationValueEntry[]? GetExistsValues();
        public Task<ConfigurationValueEntry[]?> GetExistsValuesAsync();
        public Task<ConfigurationValueEntry[]?> GetExistsValuesAsync(CancellationToken token);
    }
}