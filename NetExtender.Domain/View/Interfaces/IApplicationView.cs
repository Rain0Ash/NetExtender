// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace NetExtender.Domains.View.Interfaces
{
    public interface IApplicationView : IDisposable
    {
        public IApplicationView Start();
        public IApplicationView Start(IEnumerable<String>? args);
        public IApplicationView Start(params String[]? args);
        public Task<IApplicationView> StartAsync();
        public Task<IApplicationView> StartAsync(CancellationToken token);
        public Task<IApplicationView> StartAsync(IEnumerable<String>? args);
        public Task<IApplicationView> StartAsync(params String[]? args);
        public Task<IApplicationView> StartAsync(IEnumerable<String>? args, CancellationToken token);
        public Task<IApplicationView> StartAsync(CancellationToken token, params String[]? args);
    }
}