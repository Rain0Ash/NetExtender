// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using NetExtender.AspNetCore.Windows.Services.Interfaces;
using NetExtender.Windows.Services.Types.Services;

namespace NetExtender.AspNetCore.Windows.Services.Types.Services
{
    public abstract class AspHostService : WindowsService
    {
        protected Boolean StopRequestsByWindows { get; set; }

        protected IServiceProvider Provider { get; }

        protected AspHostService(IServiceProvider provider)
        {
            Provider = provider ?? throw new ArgumentNullException(nameof(provider));

            PauseStateHandler = Provider.GetService<IWindowsServicePauseStateService>();
            CanPauseAndContinue = PauseStateHandler is not null;
        }

        protected override Boolean AfterStartCore(String[] args)
        {
            return OnStartRegistration(Provider);
        }

        protected virtual Boolean OnStartRegistration(IServiceProvider provider)
        {
            if (provider is null)
            {
                throw new ArgumentNullException(nameof(provider));
            }

            // Register callback for application stopping after we've
            // started the service, because otherwise we might introduce unwanted
            // race conditions.
            provider.GetRequiredService<IHostApplicationLifetime>().ApplicationStopping.Register(StopCancellationTokenRegister);
            return true;
        }

        protected virtual void StopCancellationTokenRegister()
        {
            if (!StopRequestsByWindows)
            {
                Stop();
            }
        }

        protected override Boolean BeforeStopCore()
        {
            StopRequestsByWindows = true;
            return base.BeforeStopCore();
        }
    }
}