// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using NetExtender.Configuration.Builder.Interfaces;
using NetExtender.Configuration.Common;
using NetExtender.Configuration.Interfaces;
using NetExtender.Configuration.Memory;
using NetExtender.Utilities.Configuration;
using NetExtender.Utilities.Types;

namespace NetExtender.Configuration.Builder
{
    public class ConfigBuilder : IConfigBuilder
    {
        protected List<(IConfigInfo Config, Func<IConfigInfo, ConfigurationValueEntry[]>? Selector)> Internal { get; }

        public Int32 Count
        {
            get
            {
                return Internal.Count;
            }
        }

        public ConfigBuilder()
        {
            Internal = new List<(IConfigInfo Config, Func<IConfigInfo, ConfigurationValueEntry[]>? Selector)>();
        }

        public virtual IConfigBuilder Add(IConfigInfo config)
        {
            return Add(config, null);
        }

        public virtual IConfigBuilder Add(IConfigInfo config, Func<IConfigInfo, ConfigurationValueEntry[]>? predicate)
        {
            if (config is null)
            {
                throw new ArgumentNullException(nameof(config));
            }

            Internal.Add((config, predicate));
            return this;
        }

        public virtual IConfigBuilder Remove(IConfigInfo config)
        {
            if (config is null)
            {
                throw new ArgumentNullException(nameof(config));
            }

            Internal.RemoveAll(item => item.Config == config);
            return this;
        }
        
        public virtual IConfigBuilder Remove(IConfigInfo config, Func<IConfigInfo, ConfigurationValueEntry[]>? predicate)
        {
            if (config is null)
            {
                throw new ArgumentNullException(nameof(config));
            }

            Internal.Remove((config, predicate));
            return this;
        }

        public virtual IConfigBuilder Sort(IComparer<IConfigInfo>? comparer)
        {
            comparer ??= Comparer<IConfigInfo>.Default;
            Internal.Sort(item => item.Config, comparer);
            return this;
        }

        public virtual IConfig Build()
        {
            IConfig config = new MemoryConfigBehavior().Create();
            
            ConfigurationValueEntry[]? Selector((IConfigInfo Config, Func<IConfigInfo, ConfigurationValueEntry[]>? Selector) item)
            {
                return item.Selector?.Invoke(item.Config) ?? item.Config.GetExistsValues();
            }

            foreach ((String? key, String? value, ImmutableArray<String> sections) in Internal.Select(Selector).WhereNotNull().SelectMany())
            {
                config.SetValue(key, value, sections);
            }

            return config;
        }

        public Task<IConfig> BuildAsync()
        {
            return BuildAsync(CancellationToken.None);
        }

        public virtual async Task<IConfig> BuildAsync(CancellationToken token)
        {
            IConfig config = new MemoryConfigBehavior().Create();

            async Task<ConfigurationValueEntry[]?> Selector((IConfigInfo Config, Func<IConfigInfo, ConfigurationValueEntry[]>? Selector) item)
            {
                return item.Selector?.Invoke(item.Config) ?? await item.Config.GetExistsValuesAsync(token).ConfigureAwait(false);
            }

            foreach (Task<ConfigurationValueEntry[]?> task in Internal.Select(Selector))
            {
                ConfigurationValueEntry[]? entries = await task.ConfigureAwait(false);

                if (entries is null)
                {
                    continue;
                }

                foreach ((String? key, String? value, ImmutableArray<String> sections) in entries)
                {
                    await config.SetValueAsync(key, value, sections, token);
                }
            }

            return config;
        }

        public virtual IConfigBuilder Clear()
        {
            Internal.Clear();
            return this;
        }
    }
}