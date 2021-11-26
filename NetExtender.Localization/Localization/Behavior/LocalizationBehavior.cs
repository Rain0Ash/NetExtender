// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using NetExtender.Configuration.Behavior.Interfaces;
using NetExtender.Configuration.Common;
using NetExtender.Localization.Behavior.Interfaces;
using NetExtender.Localization.Common;
using NetExtender.Types.Culture;
using NetExtender.Types.Strings.Interfaces;
using NetExtender.Utilities.Types;

namespace NetExtender.Localization.Behavior
{
    public class LocalizationBehavior : ILocalizationBehavior
    {
        protected IConfigBehavior Behavior { get; }

        public event EventHandler<LCID>? Changed;

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

        public String Joiner
        {
            get
            {
                return Behavior.Joiner;
            }
        }
        
        public LocalizationOptions LocalizationOptions { get; }
        
        public LocalizationBehavior(IConfigBehavior behavior, LocalizationOptions options)
        {
            Behavior = behavior ?? throw new ArgumentNullException(nameof(behavior));
            LocalizationOptions = options;
        }

        public Boolean Contains(String? key, LCID lcid)
        {
            return Behavior.Contains(key, null); //TODO:
        }

        public Task<Boolean> ContainsAsync(String? key, LCID lcid, CancellationToken token)
        {
            return Behavior.ContainsAsync(key, null, token);
        }

        public IString? Get(String? key, LCID lcid)
        {
            return Behavior.Get(key, null);
        }

        public Task<IString?> GetAsync(String? key, LCID lcid, CancellationToken token)
        {
            return Behavior.GetAsync(key, null, token);
        }

        public LocalizationEntry[]? GetExists()
        {
            return Behavior.GetExists()?.Select(item => (LocalizationEntry) item).ToArray();
        }

        public async Task<LocalizationEntry[]?> GetExistsAsync(CancellationToken token)
        {
            return (await Behavior.GetExistsAsync(token))?.Select(item => (LocalizationEntry) item).ToArray();
        }

        public Boolean Reload()
        {
            return Behavior.Reload();
        }

        public Task<Boolean> ReloadAsync(CancellationToken token)
        {
            return Behavior.ReloadAsync(token);
        }
    }
}