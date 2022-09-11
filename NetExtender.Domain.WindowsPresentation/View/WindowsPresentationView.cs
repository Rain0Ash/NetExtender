// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using NetExtender.Domains.Applications;
using NetExtender.Domains.View.Interfaces;
using NetExtender.Types.Exceptions;

namespace NetExtender.Domains.View
{
    public class WindowsPresentationView<T> : WindowsPresentationView where T : Window, new()
    {
        public WindowsPresentationView()
            : base(new T())
        {
        }

        public WindowsPresentationView(T? context)
            : base(context)
        {
        }
    }
    
    public class WindowsPresentationView : ApplicationView
    {
        protected Window? Context { get; private set; }
        
        public WindowsPresentationView()
        {
        }

        public WindowsPresentationView(Window? context)
        {
            Context = context;
        }
        
        protected override void InitializeInternal()
        {
            Domain.ShutdownMode = ApplicationShutdownMode.OnExplicitShutdown;
        }
        
        protected override Task<IApplicationView> RunAsync(CancellationToken token)
        {
            return RunAsync(Context, token);
        }

        protected virtual async Task<IApplicationView> RunAsync(Window? window, CancellationToken token)
        {
            WindowsPresentationApplication application = Domain.Current.Application as WindowsPresentationApplication ?? throw new InitializeException($"{nameof(Domain.Current.Application)} is not {nameof(WindowsPresentationApplication)}");
            
            if (window is null)
            {
                await application.RunAsync(token);
                return this;
            }

            Context ??= window;
            if (window != Context)
            {
                throw new ArgumentException($"{nameof(window)} not reference equals with {nameof(Context)}");
            }
            
            Context.Closed += OnFormClosed;
            await application.RunAsync(Context, token);
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