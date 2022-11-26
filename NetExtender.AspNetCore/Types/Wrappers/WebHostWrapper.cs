// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.Extensions.Hosting;

namespace NetExtender.AspNetCore.Types.Wrappers
{
    public sealed class WebHostWrapper : IHost, IWebHost
    {
        private IWebHost Host { get; }

        public IFeatureCollection ServerFeatures
        {
            get
            {
                return Host.ServerFeatures;
            }
        }

        public IServiceProvider Services
        {
            get
            {
                return Host.Services;
            }
        }

        public WebHostWrapper(IWebHost host)
        {
            Host = host ?? throw new ArgumentNullException(nameof(host));
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