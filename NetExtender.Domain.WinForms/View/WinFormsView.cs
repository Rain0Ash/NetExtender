// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Windows.Forms;
using NetExtender.Domains.Applications;
using NetExtender.Domains.View.Interfaces;
using NetExtender.Exceptions;

namespace NetExtender.Domains.View
{
    public class WinFormsView : ApplicationView
    {
        public Form? Context { get; private set; }

        public virtual Boolean VisualStyle
        {
            get
            {
                return System.Windows.Forms.Application.UseVisualStyles;
            }
        }
        
        public WinFormsView()
        {
        }

        public WinFormsView(Form context)
        {
            Context = context;
        }
        
        protected static void EnableVisualStyles()
        {
            System.Windows.Forms.Application.EnableVisualStyles();
        }
        
        protected override void InitializeInternal()
        {
            Domain.ShutdownMode = ApplicationShutdownMode.OnExplicitShutdown;
        }

        protected override IApplicationView Run()
        {
            if (Context is null)
            {
                System.Windows.Forms.Application.Run();
                return this;
            }
            
            return Run(Context);
        }

        protected virtual IApplicationView Run(Form? form)
        {
            if (form is null)
            {
                return Run();
            }

            Context ??= form;
            if (form != Context)
            {
                throw new ArgumentException($"{nameof(form)} not reference equals with {nameof(Context)}");
            }
            
            Context.Closed += OnFormClosed;
            
            WinFormsApplication application = Domain.Current.Application as WinFormsApplication ?? throw new InitializeException("Application is not winforms");
            application.Run(Context);
            return this;
        }

        protected IApplicationView Run<T>() where T : Form, new()
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