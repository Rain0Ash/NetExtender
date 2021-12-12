// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using NetExtender.Configuration.Common;
using NetExtender.Configuration.Interfaces;

namespace NetExtender.Configuration.Builder.Interfaces
{
    public interface IConfigBuilder
    {
        public Int32 Count { get; }
        
        public IConfigBuilder Add(IConfigInfo config);
        public IConfigBuilder Add(IConfigInfo config, Func<IConfigInfo, ConfigurationValueEntry[]> predicate);
        public IConfigBuilder Remove(IConfigInfo config);
        public IConfigBuilder Remove(IConfigInfo config, Func<IConfigInfo, ConfigurationValueEntry[]> predicate);
        public IConfigBuilder Sort(IComparer<IConfigInfo>? comparer);
        public IConfig Build();
        public Task<IConfig> BuildAsync();
        public Task<IConfig> BuildAsync(CancellationToken token);
        public IConfigBuilder Clear();
    }
}