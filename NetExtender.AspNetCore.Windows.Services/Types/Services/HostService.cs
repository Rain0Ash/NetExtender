// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using Microsoft.Extensions.Hosting;

namespace NetExtender.AspNetCore.Windows.Services.Types.Services
{
    /// <summary>
    ///     Provides an implementation of a Windows service that hosts ASP.NET Core.
    /// </summary>
    public class HostService : AspHostService
    {
        protected IHost Host { get; }
        
        protected override IServiceProvider Provider
        {
            get
            {
                return Host.Services;
            }
        }

        /// <summary>
        /// Creates an instance of <c>WebHostService</c> which hosts the specified web application.
        /// </summary>
        /// <param name="host">The configured web host containing the web application to host in the Windows service.</param>
        public HostService(IHost host)
        {
            Host = host ?? throw new ArgumentNullException(nameof(host));
        }

        protected override void StartHost()
        {
            Host.Start();
        }

        protected override void StopHost()
        {
            Host.StopAsync().GetAwaiter().GetResult();
        }

        protected override void DisposeHost()
        {
            Host.Dispose();
        }
    }
}