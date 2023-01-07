// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using Microsoft.Extensions.Hosting;
using NetExtender.Types.Tasks.Interfaces;

namespace NetExtender.Domains.AspNetCore.Server.Interfaces
{
    public interface IAspNetCoreApplicationServer<out THost> : IAspNetCoreApplicationServer where THost : class, IHost
    {
        public new THost Context { get; }
    }

    public interface IAspNetCoreApplicationServer : IStartable
    {
        public IHost Context { get; }
    }
}