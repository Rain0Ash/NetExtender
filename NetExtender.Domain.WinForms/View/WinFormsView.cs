// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using NetExtender.Domains.Builder.Interfaces;
using NetExtender.Domains.Utilities;
using NetExtender.Domains.View;
using NetExtender.Domains.View.Interfaces;
using NetExtender.Domains.WinForms.Applications;
using NetExtender.Domains.WinForms.Builder;

namespace NetExtender.Domains.WinForms.View
{
    public class WinFormsView<T> : WinFormsView<T, WinFormsBuilder<T>> where T : Form, new()
    {
        public WinFormsView()
            : base(new T())
        {
        }

        public WinFormsView(T? form)
            : base(form)
        {
        }
    }
    
    public class WinFormsView<T, TBuilder> : WinFormsView where T : Form where TBuilder : IApplicationBuilder<T>, new()
    {
        protected sealed override Form? Context { get; set; }
        protected TBuilder? Builder { get; }

        public WinFormsView()
        {
            Builder = new TBuilder();
        }

        public WinFormsView(T? form)
        {
            Context = form ?? throw new ArgumentNullException(nameof(form));
        }
        
        public WinFormsView(TBuilder builder)
        {
            Builder = builder ?? throw new ArgumentNullException(nameof(builder));
        }

        protected override Task<IApplicationView> RunAsync(CancellationToken token)
        {
            return RunAsync(Context ?? Builder?.Build(Arguments), token);
        }
    }

    public abstract class WinFormsView : ContextApplicationView<Form, WinFormsApplication>
    {
        public virtual Boolean VisualStyle
        {
            get
            {
                return Application.UseVisualStyles;
            }
        }

        protected override ApplicationShutdownMode? ShutdownMode
        {
            get
            {
                return ApplicationShutdownMode.OnMainWindowClose;
            }
        }

        protected static void EnableVisualStyles()
        {
            Application.EnableVisualStyles();
        }

        protected override Task<IApplicationView> RunAsync(CancellationToken token)
        {
            if (Context is not null)
            {
                return RunAsync(Context, token);
            }

            Application.Run();
            return Task.FromResult<IApplicationView>(this);
        }

        protected override async Task<IApplicationView> RunAsync(Form? form, CancellationToken token)
        {
            if (form is null)
            {
                return await RunAsync(token).ConfigureAwait(false);
            }

            Context ??= form;
            if (form != Context)
            {
                throw new ArgumentException($"{nameof(form)} not reference equals with {nameof(Context)}");
            }

            Context.Closed += OnFormClosed;

            WinFormsApplication application = Domain.Current.Application.As<WinFormsApplication>();
            await application.RunAsync(Context, token).ConfigureAwait(false);
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