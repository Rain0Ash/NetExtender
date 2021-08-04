// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using NetExtender.Windows.Services.Types.Services;

namespace NetExtender.AspNetCore.Windows.Services.Types.Services
{
    public abstract class AspHostService : WindowsService
    {
        protected Boolean StopRequestedByWindows { get; set; }
        
        protected abstract IServiceProvider Provider { get; }

        protected internal void Start()
        {
            OnStart(Array.Empty<String>());
        }
        
        /// <inheritdoc />
        protected override void OnStart(String[] args)
        {
            OnStarting(args);

            StartHost();

            OnStarted();
            
            OnStartRegistration(Provider);
        }

        protected abstract void StartHost();
        
        protected virtual void OnStartRegistration(IServiceProvider provider)
        {
            if (provider is null)
            {
                throw new ArgumentNullException(nameof(provider));
            }

            // Register callback for application stopping after we've
            // started the service, because otherwise we might introduce unwanted
            // race conditions.
            provider.GetRequiredService<IHostApplicationLifetime>().ApplicationStopping.Register(StopCancellationTokenRegister);
        }

        protected virtual void StopCancellationTokenRegister()
        {
            if (!StopRequestedByWindows)
            {
                Stop();
            }
        }

        /// <inheritdoc />
        protected override void OnStop()
        {
            StopRequestedByWindows = true;
            OnStopping();
            try
            {
                StopHost();
            }
            finally
            {
                DisposeHost();
                OnStopped();
            }
        }

        protected abstract void StopHost();
        protected abstract void DisposeHost();

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