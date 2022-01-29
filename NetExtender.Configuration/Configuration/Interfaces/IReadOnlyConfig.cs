// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using NetExtender.Configuration.Common;

namespace NetExtender.Configuration.Interfaces
{
    public interface IReadOnlyConfig : IConfigInfo, IEnumerable<ConfigurationEntry>
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
        public Boolean KeyExist(String? key, params String[]? sections);
        public Boolean KeyExist(String? key, IEnumerable<String>? sections);
        public Task<Boolean> KeyExistAsync(String? key, params String[]? sections);
        public Task<Boolean> KeyExistAsync(String? key, IEnumerable<String>? sections);
        public Task<Boolean> KeyExistAsync(String? key, CancellationToken token, params String[]? sections);
        public Task<Boolean> KeyExistAsync(String? key, IEnumerable<String>? sections, CancellationToken token);
        public ConfigurationValueEntry[]? Difference(IEnumerable<ConfigurationValueEntry>? entries);
        public Task<ConfigurationValueEntry[]?> DifferenceAsync(IEnumerable<ConfigurationValueEntry>? entries);
        public Task<ConfigurationValueEntry[]?> DifferenceAsync(IEnumerable<ConfigurationValueEntry>? entries, CancellationToken token);
        public void CopyTo(IConfig config);
        public Task CopyToAsync(IConfig config);
        public Task CopyToAsync(IConfig config, CancellationToken token);
        public String? this[String? key, params String[]? sections] { get; }
        public String? this[String? key, IEnumerable<String>? sections] { get; }
    }
}