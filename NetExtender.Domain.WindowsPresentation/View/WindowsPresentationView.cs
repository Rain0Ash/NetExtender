// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using NetExtender.Domains.Builder.Interfaces;
using NetExtender.Domains.Utilities;
using NetExtender.Domains.View;
using NetExtender.Domains.View.Interfaces;
using NetExtender.Domains.WindowsPresentation.Applications;
using NetExtender.Domains.WindowsPresentation.Builder;

namespace NetExtender.Domains.WindowsPresentation.View
{
    public class WindowsPresentationView<T> : WindowsPresentationView<T, WindowsPresentationBuilder<T>> where T : Window, new()
    {
        public WindowsPresentationView()
            : base(new T())
        {
        }

        public WindowsPresentationView(T? window)
            : base(window)
        {
        }
    }
    
    public class WindowsPresentationView<T, TBuilder> : WindowsPresentationView where T : Window where TBuilder : IApplicationBuilder<T>, new()
    {
        protected sealed override Window? Context { get; set; }
        protected TBuilder? Builder { get; }

        public WindowsPresentationView()
        {
            Builder = new TBuilder();
        }

        public WindowsPresentationView(T? window)
        {
            Context = window ?? throw new ArgumentNullException(nameof(window));
        }
        
        public WindowsPresentationView(TBuilder builder)
        {
            Builder = builder ?? throw new ArgumentNullException(nameof(builder));
        }

        protected override Task<IApplicationView> RunAsync(CancellationToken token)
        {
            return RunAsync(Context ?? Builder?.Build(Arguments), token);
        }
    }

    public abstract class WindowsPresentationView : ContextApplicationView<Window, WindowsPresentationApplication>
    {
        protected override ApplicationShutdownMode? ShutdownMode
        {
            get
            {
                return ApplicationShutdownMode.OnMainWindowClose;
            }
        }

        protected override Task<IApplicationView> RunAsync(CancellationToken token)
        {
            return RunAsync(Context, token);
        }

        protected override async Task<IApplicationView> RunAsync(Window? window, CancellationToken token)
        {
            WindowsPresentationApplication application = Domain.Current.Application.As<WindowsPresentationApplication>();

            if (window is null)
            {
                await application.RunAsync(token).ConfigureAwait(false);
                return this;
            }

            Context ??= window;
            if (window != Context)
            {
                throw new ArgumentException($"{nameof(window)} not reference equals with {nameof(Context)}");
            }

            Context.Closed += OnFormClosed;
            await application.RunAsync(Context, token).ConfigureAwait(false);
            return this;
        }

        protected Task<IApplicationView> RunAsync<T>() where T : Window, new()
        {
            return RunAsync<T>(CancellationToken.None);
        }

        protected Task<IApplicationView> RunAsync<T>(CancellationToken token) where T : Window, new()
        {
            return RunAsync(new T(), token);
        }

        protected virtual void OnFormClosed(Object? sender, EventArgs args)
        {
            if (Domain.ShutdownMode != ApplicationShutdownMode.OnMainWindowClose)
            {
                return;
            }

            try
            {
                Domain.Shutdown(true);
            }
            catch (Exception)
            {
                //ignore
            }
        }

        protected override void Dispose(Boolean disposing)
        {
            if (Context is not null)
            {
                Context.Closed -= OnFormClosed;
            }

            base.Dispose(disposing);
        }
    }
}