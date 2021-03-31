// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Windows;
using NetExtender.Apps.Domains.Applications;
using NetExtender.Apps.Domains.GUIViews.Common;
using NetExtender.Exceptions;

namespace NetExtender.Apps.Domains.GUIViews.WPF
{
    public class AppWPFView : AppGUIView
    {
        public Window Context { get; private set; }
        
        public AppWPFView()
        {
        }

        public AppWPFView(Window context)
        {
            Context = context;
        }
        
        private protected override void InitializeInternal()
        {
            Domain.ShutdownMode = ShutdownMode.OnExplicitShutdown;
        }
        
        protected override void Run()
        {
            Run(Context);
        }

        protected virtual void Run(Window window)
        {
            WPFApplication application = Domain.Current.Application as WPFApplication ?? throw new InitializeException("Application is not wpf");
            
            if (window is null)
            {
                application.Run();
                return;
            }

            Context ??= window;
            if (window != Context)
            {
                throw new ArgumentException($"{nameof(window)} not reference equals with {nameof(Context)}");
            }
            
            Context.Closed += OnFormClosed;
            application.Run(Context);
        }
        
        protected void Run<T>() where T : Window, new()
        {
            Run(new T());
        }

        protected virtual void OnFormClosed(Object sender, EventArgs e)
        {
            if (Domain.ShutdownMode != ShutdownMode.OnMainWindowClose)
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

        public override void Dispose()
        {
            Context.Closed -= OnFormClosed;
            base.Dispose();
        }
    }
}