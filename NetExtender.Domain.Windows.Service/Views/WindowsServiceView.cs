// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Threading;
using System.Threading.Tasks;
using NetExtender.Domains.Builder.Interfaces;
using NetExtender.Domains.Service.Applications;
using NetExtender.Domains.Service.Builder;
using NetExtender.Domains.View;
using NetExtender.Domains.View.Interfaces;
using NetExtender.Types.Exceptions;
using NetExtender.Windows.Services.Types.Services.Interfaces;

namespace NetExtender.Domains.Service.Views
{
    public class WindowsServiceView<T> : WindowsServiceView<T, WindowsServiceBuilder<T>> where T : class, IWindowsService, new()
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
    
    public class WindowsServiceView<T, TBuilder> : WindowsServiceView where T : class, IWindowsService where TBuilder : IApplicationBuilder<T>, new()
    {
        protected T? Internal { get; set; }

        protected sealed override IWindowsService? Context
        {
            get
            {
                return Internal;
            }
            set
            {
                Internal = value is not null ? value as T ?? throw new InitializeException($"{nameof(value)} is not {typeof(T).Name}") : null;
            }
        }

        protected TBuilder? Builder { get; }

        public WindowsServiceView()
        {
            Builder = new TBuilder();
        }

        public WindowsServiceView(T service)
        {
            Context = service ?? throw new ArgumentNullException(nameof(service));
        }
        
        public WindowsServiceView(TBuilder builder)
        {
            Builder = builder ?? throw new ArgumentNullException(nameof(builder));
        }

        protected override Task<IApplicationView> RunAsync(CancellationToken token)
        {
            return RunAsync(Context ?? Builder?.Build(Arguments), token);
        }
    }

    public abstract class WindowsServiceView : ContextApplicationView<IWindowsService, WindowsServiceApplication>
    {
        protected override ApplicationShutdownMode? ShutdownMode
        {
            get
            {
                return ApplicationShutdownMode.OnExplicitShutdown;
            }
        }
    }
}