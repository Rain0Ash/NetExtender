// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Diagnostics.CodeAnalysis;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using NetExtender.Domains.Applications.Interfaces;
using NetExtender.Domains.Builder.Interfaces;
using NetExtender.Domains.Utilities;
using NetExtender.Domains.View.Interfaces;
using NetExtender.Domains.WinForms.AspNetCore.Applications;
using NetExtender.Domains.WinForms.AspNetCore.Builder;
using NetExtender.Domains.WinForms.AspNetCore.Context;
using NetExtender.Domains.WinForms.View;
using NetExtender.Types.Exceptions;

namespace NetExtender.Domains.WinForms.AspNetCore.View
{
    public abstract class WinFormsAspNetCoreViewAbstraction<T, TApplication> : WinFormsView where T : class where TApplication : class, IApplication
    {
        protected Form? Form
        {
            get
            {
                return Context;
            }
            set
            {
                Context = value;
            }
        }

        protected abstract T? Host { get; set; }

        protected override Task<IApplicationView> RunAsync(CancellationToken token)
        {
            return RunAsync(Form, token);
        }

        protected abstract override Task<IApplicationView> RunAsync(Form? form, CancellationToken token);
        protected abstract Task<IApplicationView> RunAsync(T? host, CancellationToken token);

        protected virtual async Task<IApplicationView> RunAsync(Form? form, T? host, CancellationToken token)
        {
            if (host is null)
            {
                return await RunAsync(form, token).ConfigureAwait(false);
            }
            
            TApplication application = Domain.Current.Application.As<TApplication>();
            
            if (form is null)
            {
                await RunAsync(application, host, token).ConfigureAwait(false);
                return this;
            }

            Form ??= form;
            if (form != Form)
            {
                throw new ArgumentException($"{nameof(form)} not reference equals with {nameof(Form)}");
            }

            Form.Closed += OnFormClosed;

            Host ??= host;
            if (host != Host)
            {
                throw new ArgumentException($"{nameof(host)} not reference equals with {nameof(Host)}");
            }

            await RunAsync(application, Form, Host, token).ConfigureAwait(false);
            return this;
        }
        
        protected abstract Task RunAsync(TApplication application, T host, CancellationToken token);
        protected abstract Task RunAsync(TApplication application, Form form, T host, CancellationToken token);
        
        protected Task<IApplicationView> RunAsync<TForm, THost>() where TForm : Form, new() where THost : class, IWebHost, T, new()
        {
            return RunAsync<TForm, THost>(CancellationToken.None);
        }

        protected Task<IApplicationView> RunAsync<TForm, THost>(CancellationToken token) where TForm : Form, new() where THost : class, IWebHost, T, new()
        {
            return RunAsync(new TForm(), new THost(), token);
        }
    }
    
    public class WinFormsAspNetCoreView<TForm, THost> : WinFormsAspNetCoreView<TForm, THost, WinFormsAspNetCoreBuilder<TForm, THost>> where TForm : Form, new() where THost : class, IHost, new()
    {
        public WinFormsAspNetCoreView()
            : base(new TForm(), new THost())
        {
        }

        public WinFormsAspNetCoreView(TForm? form)
            : base(form, new THost())
        {
        }

        public WinFormsAspNetCoreView(THost host)
            : base(new TForm(), host)
        {
        }

        public WinFormsAspNetCoreView(TForm? form, THost host)
            : base(form, host)
        {
        }
    }
    
    public class WinFormsAspNetCoreView<TForm, THost, TBuilder> : WinFormsAspNetCoreView<TBuilder> where TForm : Form where THost : class, IHost where TBuilder : IApplicationBuilder<WinFormsAspNetCoreContext<TForm, THost>>, new()
    {
        protected THost? Internal { get; set; }
        
        protected sealed override IHost? Host
        {
            get
            {
                return Internal;
            }
            set
            {
                Internal = value is not null ? value as THost ?? throw new InitializeException($"{nameof(value)} is not {typeof(THost).Name}") : null;
            }
        }

        public WinFormsAspNetCoreView()
        {
        }

        // ReSharper disable once SuggestBaseTypeForParameterInConstructor
        public WinFormsAspNetCoreView(WinFormsAspNetCoreContext<TForm, THost> context)
            : base(context)
        {
        }

        public WinFormsAspNetCoreView(TForm? form, THost? host)
            : base(form, host)
        {
        }
        
        public WinFormsAspNetCoreView(TBuilder builder)
            : base(builder)
        {
        }
    }
    
    public class WinFormsAspNetCoreView<TBuilder> : WinFormsAspNetCoreView where TBuilder : IApplicationBuilder<WinFormsAspNetCoreContext>, new()
    {
        protected sealed override Form? Context { get; set; }
        protected override IHost? Host { get; set; }
        
        protected TBuilder? Builder { get; }

        public WinFormsAspNetCoreView()
        {
            Builder = new TBuilder();
        }

        [SuppressMessage("ReSharper", "VirtualMemberCallInConstructor")]
        public WinFormsAspNetCoreView(WinFormsAspNetCoreContext context)
        {
            if (context is null)
            {
                throw new ArgumentNullException(nameof(context));
            }

            Form = context.Form;
            Host = context.Host;
        }

        [SuppressMessage("ReSharper", "VirtualMemberCallInConstructor")]
        public WinFormsAspNetCoreView(Form? form, IHost? host)
        {
            Form = form;
            Host = host;
        }
        
        public WinFormsAspNetCoreView(TBuilder builder)
        {
            Builder = builder ?? throw new ArgumentNullException(nameof(builder));
        }

        protected override Task<IApplicationView> RunAsync(CancellationToken token)
        {
            if (Builder is null)
            {
                return RunAsync(Form, Host, token);
            }

            (Form? form, IHost? host) = Builder.Build(Arguments);
            return RunAsync(Form ?? form, Host ?? host, token);
        }
        
        protected override Task<IApplicationView> RunAsync(Form? form, CancellationToken token)
        {
            return RunAsync(form, Host ?? Builder?.Build(Arguments).Host, token);
        }
        
        protected override Task<IApplicationView> RunAsync(IHost? host, CancellationToken token)
        {
            return RunAsync(Form ?? Builder?.Build(Arguments).Form, host, token);
        }
    }

    public abstract class WinFormsAspNetCoreView : WinFormsAspNetCoreViewAbstraction<IHost, WinFormsAspNetCoreApplication>
    {
        protected override Task RunAsync(WinFormsAspNetCoreApplication application, IHost host, CancellationToken token)
        {
            if (application is null)
            {
                throw new ArgumentNullException(nameof(application));
            }

            if (host is null)
            {
                throw new ArgumentNullException(nameof(host));
            }

            return application.RunAsync(host, token);
        }

        protected override Task RunAsync(WinFormsAspNetCoreApplication application, Form form, IHost host, CancellationToken token)
        {
            if (application is null)
            {
                throw new ArgumentNullException(nameof(application));
            }

            if (form is null)
            {
                throw new ArgumentNullException(nameof(form));
            }

            if (host is null)
            {
                throw new ArgumentNullException(nameof(host));
            }

            return application.RunAsync(form, host, token);
        }
    }
    
    public class WinFormsAspNetCoreWebView<TForm, THost> : WinFormsAspNetCoreWebView<TForm, THost, WinFormsAspNetCoreWebBuilder<TForm, THost>> where TForm : Form, new() where THost : class, IWebHost, new()
    {
        public WinFormsAspNetCoreWebView()
            : base(new TForm(), new THost())
        {
        }

        public WinFormsAspNetCoreWebView(TForm? form)
            : base(form, new THost())
        {
        }

        public WinFormsAspNetCoreWebView(THost host)
            : base(new TForm(), host)
        {
        }

        public WinFormsAspNetCoreWebView(TForm? form, THost host)
            : base(form, host)
        {
        }
    }
    
    public class WinFormsAspNetCoreWebView<TForm, THost, TBuilder> : WinFormsAspNetCoreWebView<TBuilder> where TForm : Form where THost : class, IWebHost where TBuilder : IApplicationBuilder<WinFormsAspNetCoreWebContext<TForm, THost>>, new()
    {
        protected THost? Internal { get; set; }
        
        protected sealed override IWebHost? Host
        {
            get
            {
                return Internal;
            }
            set
            {
                Internal = value is not null ? value as THost ?? throw new InitializeException($"{nameof(value)} is not {typeof(THost).Name}") : null;
            }
        }

        public WinFormsAspNetCoreWebView()
        {
        }

        // ReSharper disable once SuggestBaseTypeForParameterInConstructor
        public WinFormsAspNetCoreWebView(WinFormsAspNetCoreWebContext<TForm, THost> context)
            : base(context)
        {
        }

        public WinFormsAspNetCoreWebView(TForm? form, THost? host)
            : base(form, host)
        {
        }
        
        public WinFormsAspNetCoreWebView(TBuilder builder)
            : base(builder)
        {
        }
    }
    
    public class WinFormsAspNetCoreWebView<TBuilder> : WinFormsAspNetCoreWebView where TBuilder : IApplicationBuilder<WinFormsAspNetCoreWebContext>, new()
    {
        protected sealed override Form? Context { get; set; }
        protected override IWebHost? Host { get; set; }
        
        protected TBuilder? Builder { get; }

        public WinFormsAspNetCoreWebView()
        {
            Builder = new TBuilder();
        }

        [SuppressMessage("ReSharper", "VirtualMemberCallInConstructor")]
        public WinFormsAspNetCoreWebView(WinFormsAspNetCoreWebContext context)
        {
            if (context is null)
            {
                throw new ArgumentNullException(nameof(context));
            }

            Form = context.Form;
            Host = context.Host;
        }

        [SuppressMessage("ReSharper", "VirtualMemberCallInConstructor")]
        public WinFormsAspNetCoreWebView(Form? form, IWebHost? host)
        {
            Form = form;
            Host = host;
        }
        
        public WinFormsAspNetCoreWebView(TBuilder builder)
        {
            Builder = builder ?? throw new ArgumentNullException(nameof(builder));
        }

        protected override Task<IApplicationView> RunAsync(CancellationToken token)
        {
            if (Builder is null)
            {
                return RunAsync(Form, Host, token);
            }

            (Form? form, IWebHost? host) = Builder.Build(Arguments);
            return RunAsync(Form ?? form, Host ?? host, token);
        }
        
        protected override Task<IApplicationView> RunAsync(Form? form, CancellationToken token)
        {
            return RunAsync(form, Host ?? Builder?.Build(Arguments).Host, token);
        }
        
        protected override Task<IApplicationView> RunAsync(IWebHost? host, CancellationToken token)
        {
            return RunAsync(Form ?? Builder?.Build(Arguments).Form, host, token);
        }
    }

    public abstract class WinFormsAspNetCoreWebView : WinFormsAspNetCoreViewAbstraction<IWebHost, WinFormsAspNetCoreWebApplication>
    {
        protected override Task RunAsync(WinFormsAspNetCoreWebApplication application, IWebHost host, CancellationToken token)
        {
            if (application is null)
            {
                throw new ArgumentNullException(nameof(application));
            }

            if (host is null)
            {
                throw new ArgumentNullException(nameof(host));
            }

            return application.RunAsync(host, token);
        }

        protected override Task RunAsync(WinFormsAspNetCoreWebApplication application, Form form, IWebHost host, CancellationToken token)
        {
            if (application is null)
            {
                throw new ArgumentNullException(nameof(application));
            }

            if (form is null)
            {
                throw new ArgumentNullException(nameof(form));
            }

            if (host is null)
            {
                throw new ArgumentNullException(nameof(host));
            }

            return application.RunAsync(form, host, token);
        }
    }
}