// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using NetExtender.Configuration.Behavior.Interfaces;
using NetExtender.Configuration.Common;
using NetExtender.Configuration.Interfaces;

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

        public Config(IConfigBehavior behavior)
        {
            Behavior = behavior ?? throw new ArgumentNullException(nameof(behavior));
        }

        public String? GetValue(String? key, params String[]? sections)
        {
            return GetValue(key, (IEnumerable<String>?) sections);
        }

        public String? GetValue(String? key, IEnumerable<String>? sections)
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
        
        public Task<String?> GetValueAsync(String? key, IEnumerable<String>? sections, CancellationToken token)
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

        public Boolean SetValue(String? key, String? value, IEnumerable<String>? sections)
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
        
        public Task<Boolean> SetValueAsync(String? key, String? value, IEnumerable<String>? sections, CancellationToken token)
        {
            return Behavior.SetAsync(key, value, sections, token);
        }
        
        public String? GetOrSetValue(String? key, String? value, params String[]? sections)
        {
            return GetOrSetValue(key, value, (IEnumerable<String>?) sections);
        }

        public String? GetOrSetValue(String? key, String? value, IEnumerable<String>? sections)
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
        
        public Task<String?> GetOrSetValueAsync(String? key, String? value, IEnumerable<String>? sections, CancellationToken token)
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

        public Boolean KeyExist(String? key, IEnumerable<String>? sections)
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
        
        public Task<Boolean> KeyExistAsync(String? key, IEnumerable<String>? sections, CancellationToken token)
        {
            return Behavior.ContainsAsync(key, sections, token);
        }

        public ConfigurationEntry[]? GetExists()
        {
            return Behavior.GetExists();
        }

        public Task<ConfigurationEntry[]?> GetExistsAsync()
        {
            return GetExistsAsync(CancellationToken.None);
        }

        public Task<ConfigurationEntry[]?> GetExistsAsync(CancellationToken token)
        {
            return Behavior.GetExistsAsync(token);
        }

        public Boolean Reload()
        {
            return Behavior.Reload();
        }

        public Task<Boolean> ReloadAsync()
        {
            return ReloadAsync(CancellationToken.None);
        }

        public Task<Boolean> ReloadAsync(CancellationToken token)
        {
            return Behavior.ReloadAsync(token);
        }

        public override String ToString()
        {
            return Path;
        }

        private Boolean _disposed;
        public void Dispose()
        {
            DisposeInternal(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(Boolean disposing)
        {
        }

        private void DisposeInternal(Boolean disposing)
        {
            if (_disposed)
            {
                return;
            }

            if (disposing)
            {
                Behavior.Dispose();
            }
            
            Dispose(disposing);

            _disposed = true;
        }

        ~Config()
        {
            Dispose(false);
        }
    }
}