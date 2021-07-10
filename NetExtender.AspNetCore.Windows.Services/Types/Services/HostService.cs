// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.ServiceProcess;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace NetExtender.AspNetCore.Windows.Services.Types.Services
{
    /// <summary>
    ///     Provides an implementation of a Windows service that hosts ASP.NET Core.
    /// </summary>
    public class HostService : ServiceBase
    {
        protected IHost Host { get; }
        protected Boolean StopRequestedByWindows { get; set; }

        /// <summary>
        /// Creates an instance of <c>WebHostService</c> which hosts the specified web application.
        /// </summary>
        /// <param name="host">The configured web host containing the web application to host in the Windows service.</param>
        public HostService(IHost host)
        {
            Host = host ?? throw new ArgumentNullException(nameof(host));
        }

        /// <summary>
        /// This method is not intended for direct use. Its sole purpose is to allow
        /// the service to be started by the tests.
        /// </summary>
        internal void Start()
        {
            OnStart(Array.Empty<String>());
        }

        /// <inheritdoc />
        protected sealed override void OnStart(String[] args)
        {
            OnStarting(args);

            Host.Start();

            OnStarted();

            // Register callback for application stopping after we've
            // started the service, because otherwise we might introduce unwanted
            // race conditions.
            Host
                .Services
                .GetRequiredService<IHostApplicationLifetime>()
                .ApplicationStopping
                .Register(() =>
                {
                    if (!StopRequestedByWindows)
                    {
                        Stop();
                    }
                });
        }

        /// <inheritdoc />
        protected sealed override void OnStop()
        {
            StopRequestedByWindows = true;
            OnStopping();
            try
            {
                Host.StopAsync().GetAwaiter().GetResult();
            }
            finally
            {
                Host.Dispose();
                OnStopped();
            }
        }

        /// <summary>
        /// Executes before ASP.NET Core starts.
        /// </summary>
        /// <param name="args">The command line arguments passed to the service.</param>
        protected virtual void OnStarting(String[] args)
        {
        }

        /// <summary>
        /// Executes after ASP.NET Core starts.
        /// </summary>
        protected virtual void OnStarted()
        {
        }

        /// <summary>
        /// Executes before ASP.NET Core shuts down.
        /// </summary>
        protected virtual void OnStopping()
        {
        }

        /// <summary>
        /// Executes after ASP.NET Core shuts down.
        /// </summary>
        protected virtual void OnStopped()
        {
        }
    }
}