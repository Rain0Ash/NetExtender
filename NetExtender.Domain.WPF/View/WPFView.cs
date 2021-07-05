// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Windows;
using NetExtender.Domains.Applications;
using NetExtender.Domains.View.Interfaces;
using NetExtender.Exceptions;

namespace NetExtender.Domains.View
{
    public class WPFView : ApplicationView
    {
        public Window? Context { get; private set; }
        
        public WPFView()
        {
        }

        public WPFView(Window context)
        {
            Context = context;
        }
        
        protected override void InitializeInternal()
        {
            Domain.ShutdownMode = ApplicationShutdownMode.OnExplicitShutdown;
        }
        
        protected override IApplicationView Run()
        {
            return Run(Context);
        }

        protected virtual IApplicationView Run(Window? window)
        {
            WPFApplication application = Domain.Current.Application as WPFApplication ?? throw new InitializeException("Application is not wpf");
            
            if (window is null)
            {
                application.Run();
                return this;
            }

            Context ??= window;
            if (window != Context)
            {
                throw new ArgumentException($"{nameof(window)} not reference equals with {nameof(Context)}");
            }
            
            Context.Closed += OnFormClosed;
            application.Run(Context);
            return this;
        }
        
        protected IApplicationView Run<T>() where T : Window, new()
        {
            return Run(new T());
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

            base.Dispose();
        }
    }
}