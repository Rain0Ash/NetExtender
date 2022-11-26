// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Threading;
using System.Threading.Tasks;
using NetExtender.Domain.Utilities;
using NetExtender.Domains.Service.Applications;
using NetExtender.Domains.View;
using NetExtender.Domains.View.Interfaces;
using NetExtender.Windows.Services.Types.Services.Interfaces;

namespace NetExtender.Domains.Service.Views
{
    public class WindowsServiceView<T> : WindowsServiceView where T : class, IWindowsService, new()
    {
        public WindowsServiceView()
            : base(new T())
        {
        }

        public WindowsServiceView(T service)
            : base(service)
        {
        }
    }

    public class WindowsServiceView : ApplicationView
    {
        protected IWindowsService? Context { get; set; }

        protected override ApplicationShutdownMode? ShutdownMode
        {
            get
            {
                return ApplicationShutdownMode.OnExplicitShutdown;
            }
        }

        public WindowsServiceView(IWindowsService service)
        {
            Context = service ?? throw new ArgumentNullException(nameof(service));
        }

        protected override Task<IApplicationView> RunAsync(CancellationToken token)
        {
            return RunAsync(Context, token);
        }

        protected virtual async Task<IApplicationView> RunAsync(IWindowsService? service, CancellationToken token)
        {
            if (service is null)
            {
                return await RunAsync(token);
            }

            Context ??= service;
            if (service != Context)
            {
                throw new ArgumentException($"{nameof(service)} not reference equals with {nameof(Context)}");
            }

            WindowsServiceApplication application = Domain.Current.Application.As<WindowsServiceApplication>();
            await application.RunAsync(Context, token);
            return this;
        }
    }
}