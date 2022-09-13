// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using NetExtender.Domain.Utilities;
using NetExtender.Domains.Applications;
using NetExtender.Domains.View.Interfaces;

namespace NetExtender.Domains.View
{
    public class WinFormsView<T> : WinFormsView where T : Form, new()
    {
        public WinFormsView()
            : base(new T())
        {
        }
        
        public WinFormsView(T? context)
            : base(context)
        {
        }
    }
    
    public class WinFormsView : ApplicationView
    {
        protected Form? Context { get; private set; }

        public virtual Boolean VisualStyle
        {
            get
            {
                return System.Windows.Forms.Application.UseVisualStyles;
            }
        }

        protected override ApplicationShutdownMode? ShutdownMode
        {
            get
            {
                return ApplicationShutdownMode.OnMainWindowClose;
            }
        }

        public WinFormsView()
        {
        }

        public WinFormsView(Form? context)
        {
            Context = context;
        }
        
        protected static void EnableVisualStyles()
        {
            System.Windows.Forms.Application.EnableVisualStyles();
        }

        protected override Task<IApplicationView> RunAsync(CancellationToken token)
        {
            if (Context is not null)
            {
                return RunAsync(Context, token);
            }

            System.Windows.Forms.Application.Run();
            return Task.FromResult<IApplicationView>(this);
        }

        protected virtual async Task<IApplicationView> RunAsync(Form? form, CancellationToken token)
        {
            if (form is null)
            {
                return await RunAsync(token);
            }

            Context ??= form;
            if (form != Context)
            {
                throw new ArgumentException($"{nameof(form)} not reference equals with {nameof(Context)}");
            }
            
            Context.Closed += OnFormClosed;
            
            WinFormsApplication application = Domain.Current.Application.As<WinFormsApplication>();
            await application.RunAsync(Context, token);
            return this;
        }

        protected Task<IApplicationView> RunAsync<T>() where T : Form, new()
        {
            return RunAsync<T>(CancellationToken.None);
        }
        
        protected virtual Task<IApplicationView> RunAsync<T>(CancellationToken token) where T : Form, new()
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