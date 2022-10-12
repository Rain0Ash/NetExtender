// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Threading;
using System.Threading.Tasks;
using NetExtender.Configuration.Behavior.Interfaces;
using NetExtender.Configuration.Behavior.Transactions.Interfaces;
using NetExtender.Configuration.Common;
using NetExtender.Configuration.Interfaces;
using NetExtender.Configuration.Transactions;
using NetExtender.Configuration.Transactions.Interfaces;

namespace NetExtender.Configuration
{
    public class Config : IConfig, IReadOnlyConfig
    {
        protected IConfigBehavior Behavior { get; }

        public event ConfigurationChangedEventHandler Changed
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

        public Boolean IsLazyWrite
        {
            get
            {
                return Behavior.IsLazyWrite;
            }
        }

        public Boolean IsThreadSafe
        {
            get
            {
                return Behavior.IsThreadSafe;
            }
        }

        protected internal Config(IConfigBehavior behavior)
        {
            Behavior = behavior ?? throw new ArgumentNullException(nameof(behavior));
        }

        public String? GetValue(String? key, params String[]? sections)
        {
            return GetValue(key, (IEnumerable<String>?) sections);
        }

        public virtual String? GetValue(String? key, IEnumerable<String>? sections)
        {
            return Behavior.Get(key, sections);
        }

        public String? GetValue(String? key, String? alternate, params String[]? sections)
        {
            return GetValue(key, alternate, (IEnumerable<String>?) sections);
        }

        public String? GetValue(String? key, String? alternate, IEnumerable<String>? sections)
        {
            return GetValue(key, sections) ?? alternate;
        }
        
        public Task<String?> GetValueAsync(String? key, params String[]? sections)
        {
            return GetValueAsync(key, (IEnumerable<String>?) sections);
        }
        
        public Task<String?> GetValueAsync(String? key, IEnumerable<String>? sections)
        {
            return GetValueAsync(key, sections, CancellationToken.None);
        }
        
        public Task<String?> GetValueAsync(String? key, CancellationToken token, params String[]? sections)
        {
            return GetValueAsync(key, sections, token);
        }
        
        public virtual Task<String?> GetValueAsync(String? key, IEnumerable<String>? sections, CancellationToken token)
        {
            return Behavior.GetAsync(key, sections, token);
        }
        
        public Task<String?> GetValueAsync(String? key, String? alternate, params String[]? sections)
        {
            return GetValueAsync(key, alternate, (IEnumerable<String>?) sections);
        }
        
        public Task<String?> GetValueAsync(String? key, String? alternate, IEnumerable<String>? sections)
        {
            return GetValueAsync(key, alternate, sections, CancellationToken.None);
        }
        
        public Task<String?> GetValueAsync(String? key, String? alternate, CancellationToken token, params String[]? sections)
        {
            return GetValueAsync(key, alternate, sections, token);
        }
        
        public async Task<String?> GetValueAsync(String? key, String? alternate, IEnumerable<String>? sections, CancellationToken token)
        {
            return await GetValueAsync(key, sections, token).ConfigureAwait(false) ?? alternate;
        }
        
        public Boolean SetValue(String? key, String? value, params String[]? sections)
        {
            return SetValue(key, value, (IEnumerable<String>?) sections);
        }

        public virtual Boolean SetValue(String? key, String? value, IEnumerable<String>? sections)
        {
            return Behavior.Set(key, value, sections);
        }
        
        public Task<Boolean> SetValueAsync(String? key, String? value, params String[]? sections)
        {
            return SetValueAsync(key, value, (IEnumerable<String>?) sections);
        }
        
        public Task<Boolean> SetValueAsync(String? key, String? value, IEnumerable<String>? sections)
        {
            return SetValueAsync(key, value, sections, CancellationToken.None);
        }
        
        public Task<Boolean> SetValueAsync(String? key, String? value, CancellationToken token, params String[]? sections)
        {
            return SetValueAsync(key, value, sections, token);
        }
        
        public virtual Task<Boolean> SetValueAsync(String? key, String? value, IEnumerable<String>? sections, CancellationToken token)
        {
            return Behavior.SetAsync(key, value, sections, token);
        }
        
        public String? GetOrSetValue(String? key, String? value, params String[]? sections)
        {
            return GetOrSetValue(key, value, (IEnumerable<String>?) sections);
        }

        public virtual String? GetOrSetValue(String? key, String? value, IEnumerable<String>? sections)
        {
            return Behavior.GetOrSet(key, value, sections);
        }
        
        public Task<String?> GetOrSetValueAsync(String? key, String? value, params String[]? sections)
        {
            return GetOrSetValueAsync(key, value, (IEnumerable<String>?) sections);
        }
        
        public Task<String?> GetOrSetValueAsync(String? key, String? value, IEnumerable<String>? sections)
        {
            return GetOrSetValueAsync(key, value, sections, CancellationToken.None);
        }
        
        public Task<String?> GetOrSetValueAsync(String? key, String? value, CancellationToken token, params String[]? sections)
        {
            return GetOrSetValueAsync(key, value, sections, token);
        }
        
        public virtual Task<String?> GetOrSetValueAsync(String? key, String? value, IEnumerable<String>? sections, CancellationToken token)
        {
            return Behavior.GetOrSetAsync(key, value, sections, token);
        }
        
        public Boolean RemoveValue(String? key, params String[]? sections)
        {
            return RemoveValue(key, (IEnumerable<String>?) sections);
        }

        public Boolean RemoveValue(String? key, IEnumerable<String>? sections)
        {
            return SetValue(key, null, sections);
        }
        
        public Task<Boolean> RemoveValueAsync(String? key, params String[]? sections)
        {
            return RemoveValueAsync(key, (IEnumerable<String>?) sections);
        }
        
        public Task<Boolean> RemoveValueAsync(String? key, IEnumerable<String>? sections)
        {
            return RemoveValueAsync(key, sections, CancellationToken.None);
        }
        
        public Task<Boolean> RemoveValueAsync(String? key, CancellationToken token, params String[]? sections)
        {
            return RemoveValueAsync(key, sections, token);
        }
        
        public Task<Boolean> RemoveValueAsync(String? key, IEnumerable<String>? sections, CancellationToken token)
        {
            return SetValueAsync(key, null, sections, token);
        }
        
        public Boolean KeyExist(String? key, params String[]? sections)
        {
            return KeyExist(key, (IEnumerable<String>?) sections);
        }

        public virtual Boolean KeyExist(String? key, IEnumerable<String>? sections)
        {
            return Behavior.Contains(key, sections);
        }
        
        public Task<Boolean> KeyExistAsync(String? key, params String[]? sections)
        {
            return KeyExistAsync(key, (IEnumerable<String>?) sections);
        }
        
        public Task<Boolean> KeyExistAsync(String? key, IEnumerable<String>? sections)
        {
            return KeyExistAsync(key, sections, CancellationToken.None);
        }
        
        public Task<Boolean> KeyExistAsync(String? key, CancellationToken token, params String[]? sections)
        {
            return KeyExistAsync(key, sections, token);
        }
        
        public virtual Task<Boolean> KeyExistAsync(String? key, IEnumerable<String>? sections, CancellationToken token)
        {
            return Behavior.ContainsAsync(key, sections, token);
        }

        public ConfigurationEntry[]? GetExists()
        {
            return GetExists(null);
        }

        public Task<ConfigurationEntry[]?> GetExistsAsync()
        {
            return GetExistsAsync(CancellationToken.None);
        }

        public Task<ConfigurationEntry[]?> GetExistsAsync(CancellationToken token)
        {
            return GetExistsAsync(null, token);
        }

        public ConfigurationEntry[]? GetExists(params String[]? sections)
        {
            return GetExists((IEnumerable<String>?) sections);
        }

        public virtual ConfigurationEntry[]? GetExists(IEnumerable<String>? sections)
        {
            return Behavior.GetExists(sections);
        }

        public Task<ConfigurationEntry[]?> GetExistsAsync(params String[]? sections)
        {
            return GetExistsAsync((IEnumerable<String>?) sections);
        }

        public Task<ConfigurationEntry[]?> GetExistsAsync(IEnumerable<String>? sections)
        {
            return GetExistsAsync(sections, CancellationToken.None);
        }

        public Task<ConfigurationEntry[]?> GetExistsAsync(CancellationToken token, params String[]? sections)
        {
            return GetExistsAsync(sections, token);
        }

        public virtual Task<ConfigurationEntry[]?> GetExistsAsync(IEnumerable<String>? sections, CancellationToken token)
        {
            return Behavior.GetExistsAsync(sections, token);
        }

        public ConfigurationValueEntry[]? GetExistsValues()
        {
            return GetExistsValues(null);
        }

        public Task<ConfigurationValueEntry[]?> GetExistsValuesAsync()
        {
            return GetExistsValuesAsync(CancellationToken.None);
        }

        public Task<ConfigurationValueEntry[]?> GetExistsValuesAsync(CancellationToken token)
        {
            return GetExistsValuesAsync(null, token);
        }

        public ConfigurationValueEntry[]? GetExistsValues(params String[]? sections)
        {
            return GetExistsValues((IEnumerable<String>?) sections);
        }

        public virtual ConfigurationValueEntry[]? GetExistsValues(IEnumerable<String>? sections)
        {
            return Behavior.GetExistsValues(sections);
        }

        public Task<ConfigurationValueEntry[]?> GetExistsValuesAsync(params String[]? sections)
        {
            return GetExistsValuesAsync((IEnumerable<String>?) sections);
        }

        public Task<ConfigurationValueEntry[]?> GetExistsValuesAsync(IEnumerable<String>? sections)
        {
            return GetExistsValuesAsync(sections, CancellationToken.None);
        }

        public Task<ConfigurationValueEntry[]?> GetExistsValuesAsync(CancellationToken token, params String[]? sections)
        {
            return GetExistsValuesAsync(sections, token);
        }

        public virtual Task<ConfigurationValueEntry[]?> GetExistsValuesAsync(IEnumerable<String>? sections, CancellationToken token)
        {
            return Behavior.GetExistsValuesAsync(sections, token);
        }
        
        public Boolean Clear()
        {
            return Clear(null);
        }
        
        public Boolean Clear(params String[]? sections)
        {
            return Clear((IEnumerable<String>?) sections);
        }

        public virtual Boolean Clear(IEnumerable<String>? sections)
        {
            return Behavior.Clear(sections);
        }

        public Task<Boolean> ClearAsync()
        {
            return ClearAsync(null);
        }

        public Task<Boolean> ClearAsync(params String[]? sections)
        {
            return ClearAsync((IEnumerable<String>?) sections);
        }

        public Task<Boolean> ClearAsync(IEnumerable<String>? sections)
        {
            return ClearAsync(sections, CancellationToken.None);
        }
        
        public Task<Boolean> ClearAsync(CancellationToken token)
        {
            return ClearAsync(null, token);
        }
        
        public Task<Boolean> ClearAsync(CancellationToken token, params String[]? sections)
        {
            return ClearAsync(sections, token);
        }

        public virtual Task<Boolean> ClearAsync(IEnumerable<String>? sections, CancellationToken token)
        {
            return Behavior.ClearAsync(sections, token);
        }

        public virtual Boolean Reload()
        {
            return Behavior.Reload();
        }

        public Task<Boolean> ReloadAsync()
        {
            return ReloadAsync(CancellationToken.None);
        }

        public virtual Task<Boolean> ReloadAsync(CancellationToken token)
        {
            return Behavior.ReloadAsync(token);
        }

        public virtual Boolean Reset()
        {
            return Behavior.Reset();
        }

        public Task<Boolean> ResetAsync()
        {
            return ResetAsync(CancellationToken.None);
        }

        public virtual Task<Boolean> ResetAsync(CancellationToken token)
        {
            return Behavior.ResetAsync(token);
        }

        public virtual Boolean Merge(IEnumerable<ConfigurationValueEntry>? entries)
        {
            return Behavior.Merge(entries);
        }

        public Task<Boolean> MergeAsync(IEnumerable<ConfigurationValueEntry>? entries)
        {
            return MergeAsync(entries, CancellationToken.None);
        }

        public virtual Task<Boolean> MergeAsync(IEnumerable<ConfigurationValueEntry>? entries, CancellationToken token)
        {
            return Behavior.MergeAsync(entries, token);
        }

        public virtual Boolean Replace(IEnumerable<ConfigurationValueEntry>? entries)
        {
            return Behavior.Replace(entries);
        }

        public Task<Boolean> ReplaceAsync(IEnumerable<ConfigurationValueEntry>? entries)
        {
            return ReplaceAsync(entries, CancellationToken.None);
        }

        public virtual Task<Boolean> ReplaceAsync(IEnumerable<ConfigurationValueEntry>? entries, CancellationToken token)
        {
            return Behavior.ReplaceAsync(entries, token);
        }

        public virtual ConfigurationValueEntry[]? Difference(IEnumerable<ConfigurationValueEntry>? entries)
        {
            return Behavior.Difference(entries);
        }

        public Task<ConfigurationValueEntry[]?> DifferenceAsync(IEnumerable<ConfigurationValueEntry>? entries)
        {
            return DifferenceAsync(entries, CancellationToken.None);
        }

        public virtual Task<ConfigurationValueEntry[]?> DifferenceAsync(IEnumerable<ConfigurationValueEntry>? entries, CancellationToken token)
        {
            return Behavior.DifferenceAsync(entries, token);
        }

        public virtual IConfigTransaction? Transaction()
        {
            IConfigBehaviorTransaction? transaction = Behavior.Transaction();
            return transaction is not null ? new ConfigTransaction(this, transaction) : null;
        }

        public Task<IConfigTransaction?> TransactionAsync()
        {
            return TransactionAsync(CancellationToken.None);
        }

        public virtual async Task<IConfigTransaction?> TransactionAsync(CancellationToken token)
        {
            IConfigBehaviorTransaction? transaction = await Behavior.TransactionAsync(token);
            return transaction is not null ? new ConfigTransaction(this, transaction) : null;
        }

        public virtual void CopyTo(IConfig config)
        {
            if (config is null)
            {
                throw new ArgumentNullException(nameof(config));
            }

            foreach ((String? key, ImmutableArray<String> sections) in this)
            {
                config.SetValue(key, this[key, sections], sections);
            }
        }
        
        public Task CopyToAsync(IConfig config)
        {
            return CopyToAsync(config, CancellationToken.None);
        }
        
        public virtual async Task CopyToAsync(IConfig config, CancellationToken token)
        {
            if (config is null)
            {
                throw new ArgumentNullException(nameof(config));
            }
            
            foreach ((String? key, ImmutableArray<String> sections) in this)
            {
                await config.SetValueAsync(key, this[key, sections], sections, token);
            }
        }

        public String? this[String? key, params String[]? sections]
        {
            get
            {
                return this[key, (IEnumerable<String>?) sections];
            }
            set
            {
                this[key, (IEnumerable<String>?) sections] = value;
            }
        }

        public virtual String? this[String? key, IEnumerable<String>? sections]
        {
            get
            {
                return GetValue(key, sections);
            }
            set
            {
                SetValue(key, value, sections);
            }
        }

        public virtual IEnumerator<ConfigurationEntry> GetEnumerator()
        {
            ConfigurationEntry[]? exists = GetExists();

            if (exists is null)
            {
                yield break;
            }

            foreach (ConfigurationEntry entry in exists)
            {
                yield return entry;
            }
        }
        
        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public override String ToString()
        {
            return Path;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        public async ValueTask DisposeAsync()
        {
            await DisposeAsync(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(Boolean disposing)
        {
            if (disposing)
            {
                Behavior.Dispose();
            }
        }

        protected virtual async ValueTask DisposeAsync(Boolean disposing)
        {
            if (disposing)
            {
                await Behavior.DisposeAsync();
            }
        }

        ~Config()
        {
            Dispose(false);
        }
    }
}