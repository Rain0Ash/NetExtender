// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using Microsoft.AspNetCore.Hosting;

namespace NetExtender.AspNetCore.Windows.Services.Types.Services
{
    /// <summary>
    ///     Provides an implementation of a Windows service that hosts ASP.NET Core.
    /// </summary>
    public sealed class WebHostService : AspHostService
    {
        private IWebHost Host { get; }

        /// <summary>
        /// Creates an instance of <c>WebHostService</c> which hosts the specified web application.
        /// </summary>
        /// <param name="host">The configured web host containing the web application to host in the Windows service.</param>
        public WebHostService(IWebHost host)
            : base(host?.Services ?? throw new ArgumentNullException(nameof(host)))
        {
            Host = host ?? throw new ArgumentNullException(nameof(host));
        }

        protected override Boolean StartInternal(String[] args)
        {
            try
            {
                Host.Start();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        protected override Boolean StopInternal()
        {
            Host.StopAsync().GetAwaiter().GetResult();
            return true;
        }

        protected override Boolean FinallyStopInternalHandler()
        {
            Host.Dispose();
            return true;
        }
    }
}