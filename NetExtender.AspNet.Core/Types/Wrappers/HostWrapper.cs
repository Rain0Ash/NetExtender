// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.Extensions.Hosting;

namespace NetExtender.AspNet.Core.Types.Wrappers
{
    public sealed class HostWrapper : IHost, IWebHost
    {
        private IHost Host { get; }

        public IFeatureCollection ServerFeatures { get; }

        public IServiceProvider Services
        {
            get
            {
                return Host.Services;
            }
        }

        public HostWrapper(IHost host)
        {
            Host = host ?? throw new ArgumentNullException(nameof(host));
            ServerFeatures = new FeatureCollection();
        }

        public void Start()
        {
            Host.Start();
        }

        public Task StartAsync()
        {
            return StartAsync(CancellationToken.None);
        }

        public Task StartAsync(CancellationToken token)
        {
            return Host.StartAsync(token);
        }

        public Task StopAsync()
        {
            return StopAsync(CancellationToken.None);
        }

        public Task StopAsync(CancellationToken token)
        {
            return Host.StopAsync(token);
        }

        public void Dispose()
        {
            Host.Dispose();
        }
    }
}