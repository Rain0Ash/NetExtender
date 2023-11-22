// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Diagnostics.CodeAnalysis;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using NetExtender.Domains.Applications.Interfaces;
using NetExtender.Domains.Builder.Interfaces;
using NetExtender.Domains.Utilities;
using NetExtender.Domains.View.Interfaces;
using NetExtender.Domains.WindowsPresentation.AspNetCore.Applications;
using NetExtender.Domains.WindowsPresentation.AspNetCore.Builder;
using NetExtender.Domains.WindowsPresentation.AspNetCore.Context;
using NetExtender.Domains.WindowsPresentation.View;
using NetExtender.Types.Exceptions;

namespace NetExtender.Domains.WindowsPresentation.AspNetCore.View
{
    public abstract class WindowsPresentationAspNetCoreViewAbstraction<T, TApplication> : WindowsPresentationView where T : class where TApplication : class, IApplication
    {
        protected Window? Window
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
            return RunAsync(Window, token);
        }

        protected abstract override Task<IApplicationView> RunAsync(Window? window, CancellationToken token);
        protected abstract Task<IApplicationView> RunAsync(T? host, CancellationToken token);

        protected virtual async Task<IApplicationView> RunAsync(Window? window, T? host, CancellationToken token)
        {
            if (host is null)
            {
                return await RunAsync(window, token).ConfigureAwait(false);
            }
            
            TApplication application = Domain.Current.Application.As<TApplication>();
            
            if (window is null)
            {
                await RunAsync(application, host, token).ConfigureAwait(false);
                return this;
            }

            Window ??= window;
            if (window != Window)
            {
                throw new ArgumentException($"{nameof(window)} not reference equals with {nameof(Window)}");
            }

            Window.Closed += OnFormClosed;

            Host ??= host;
            if (host != Host)
            {
                throw new ArgumentException($"{nameof(host)} not reference equals with {nameof(Host)}");
            }

            await RunAsync(application, Window, Host, token).ConfigureAwait(false);
            return this;
        }

        protected abstract Task RunAsync(TApplication application, T host, CancellationToken token);
        protected abstract Task RunAsync(TApplication application, Window window, T host, CancellationToken token);
        
        protected Task<IApplicationView> RunAsync<TWindow, THost>() where TWindow : Window, new() where THost : class, T, new()
        {
            return RunAsync<TWindow, THost>(CancellationToken.None);
        }

        protected Task<IApplicationView> RunAsync<TWindow, THost>(CancellationToken token) where TWindow : Window, new() where THost : class, T, new()
        {
            return RunAsync(new TWindow(), new THost(), token);
        }
    }

    public class WindowsPresentationAspNetCoreView<TWindow, THost> : WindowsPresentationAspNetCoreView<TWindow, THost, WindowsPresentationAspNetCoreBuilder<TWindow, THost>> where TWindow : Window, new() where THost : class, IHost, new()
    {
        public WindowsPresentationAspNetCoreView()
            : base(new TWindow(), new THost())
        {
        }

        public WindowsPresentationAspNetCoreView(TWindow? window)
            : base(window, new THost())
        {
        }

        public WindowsPresentationAspNetCoreView(THost host)
            : base(new TWindow(), host)
        {
        }

        public WindowsPresentationAspNetCoreView(TWindow? window, THost host)
            : base(window, host)
        {
        }
    }
    
    public class WindowsPresentationAspNetCoreView<TWindow, THost, TBuilder> : WindowsPresentationAspNetCoreView<TBuilder> where TWindow : Window where THost : class, IHost where TBuilder : IApplicationBuilder<WindowsPresentationAspNetCoreContext<TWindow, THost>>, new()
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

        public WindowsPresentationAspNetCoreView()
        {
        }

        // ReSharper disable once SuggestBaseTypeForParameterInConstructor
        public WindowsPresentationAspNetCoreView(WindowsPresentationAspNetCoreContext<TWindow, THost> context)
            : base(context)
        {
        }

        public WindowsPresentationAspNetCoreView(TWindow? window, THost? host)
            : base(window, host)
        {
        }
        
        public WindowsPresentationAspNetCoreView(TBuilder builder)
            : base(builder)
        {
        }
    }
    
    public class WindowsPresentationAspNetCoreView<TBuilder> : WindowsPresentationAspNetCoreView where TBuilder : IApplicationBuilder<WindowsPresentationAspNetCoreContext>, new()
    {
        protected sealed override Window? Context { get; set; }
        protected override IHost? Host { get; set; }
        
        protected TBuilder? Builder { get; }

        public WindowsPresentationAspNetCoreView()
        {
            Builder = new TBuilder();
        }

        [SuppressMessage("ReSharper", "VirtualMemberCallInConstructor")]
        public WindowsPresentationAspNetCoreView(WindowsPresentationAspNetCoreContext context)
        {
            if (context is null)
            {
                throw new ArgumentNullException(nameof(context));
            }

            Window = context.Window;
            Host = context.Host;
        }

        [SuppressMessage("ReSharper", "VirtualMemberCallInConstructor")]
        public WindowsPresentationAspNetCoreView(Window? window, IHost? host)
        {
            Window = window;
            Host = host;
        }
        
        public WindowsPresentationAspNetCoreView(TBuilder builder)
        {
            Builder = builder ?? throw new ArgumentNullException(nameof(builder));
        }

        protected override Task<IApplicationView> RunAsync(CancellationToken token)
        {
            if (Builder is null)
            {
                return RunAsync(Window, Host, token);
            }

            (Window? window, IHost? host) = Builder.Build(Arguments);
            return RunAsync(Window ?? window, Host ?? host, token);
        }
        
        protected override Task<IApplicationView> RunAsync(Window? window, CancellationToken token)
        {
            return RunAsync(window, Host ?? Builder?.Build(Arguments).Host, token);
        }
        
        protected override Task<IApplicationView> RunAsync(IHost? host, CancellationToken token)
        {
            return RunAsync(Window ?? Builder?.Build(Arguments).Window, host, token);
        }
    }

    public abstract class WindowsPresentationAspNetCoreView : WindowsPresentationAspNetCoreViewAbstraction<IHost, WindowsPresentationAspNetCoreApplication>
    {
        protected override Task RunAsync(WindowsPresentationAspNetCoreApplication application, IHost host, CancellationToken token)
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

        protected override Task RunAsync(WindowsPresentationAspNetCoreApplication application, Window window, IHost host, CancellationToken token)
        {
            if (application is null)
            {
                throw new ArgumentNullException(nameof(application));
            }

            if (window is null)
            {
                throw new ArgumentNullException(nameof(window));
            }

            if (host is null)
            {
                throw new ArgumentNullException(nameof(host));
            }

            return application.RunAsync(window, host, token);
        }
    }
    
    public class WindowsPresentationAspNetCoreWebView<TWindow, THost> : WindowsPresentationAspNetCoreWebView<TWindow, THost, WindowsPresentationAspNetCoreWebBuilder<TWindow, THost>> where TWindow : Window, new() where THost : class, IWebHost, new()
    {
        public WindowsPresentationAspNetCoreWebView()
            : base(new TWindow(), new THost())
        {
        }

        public WindowsPresentationAspNetCoreWebView(TWindow? window)
            : base(window, new THost())
        {
        }

        public WindowsPresentationAspNetCoreWebView(THost host)
            : base(new TWindow(), host)
        {
        }

        public WindowsPresentationAspNetCoreWebView(TWindow? window, THost host)
            : base(window, host)
        {
        }
    }
    
    public class WindowsPresentationAspNetCoreWebView<TWindow, THost, TBuilder> : WindowsPresentationAspNetCoreWebView<TBuilder> where TWindow : Window where THost : class, IWebHost where TBuilder : IApplicationBuilder<WindowsPresentationAspNetCoreWebContext<TWindow, THost>>, new()
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

        public WindowsPresentationAspNetCoreWebView()
        {
        }

        // ReSharper disable once SuggestBaseTypeForParameterInConstructor
        public WindowsPresentationAspNetCoreWebView(WindowsPresentationAspNetCoreWebContext<TWindow, THost> context)
            : base(context)
        {
        }

        public WindowsPresentationAspNetCoreWebView(TWindow? window, THost? host)
            : base(window, host)
        {
        }
        
        public WindowsPresentationAspNetCoreWebView(TBuilder builder)
            : base(builder)
        {
        }
    }
    
    public class WindowsPresentationAspNetCoreWebView<TBuilder> : WindowsPresentationAspNetCoreWebView where TBuilder : IApplicationBuilder<WindowsPresentationAspNetCoreWebContext>, new()
    {
        protected sealed override Window? Context { get; set; }
        protected override IWebHost? Host { get; set; }
        
        protected TBuilder? Builder { get; }

        public WindowsPresentationAspNetCoreWebView()
        {
            Builder = new TBuilder();
        }

        [SuppressMessage("ReSharper", "VirtualMemberCallInConstructor")]
        public WindowsPresentationAspNetCoreWebView(WindowsPresentationAspNetCoreWebContext context)
        {
            if (context is null)
            {
                throw new ArgumentNullException(nameof(context));
            }

            Window = context.Window;
            Host = context.Host;
        }

        [SuppressMessage("ReSharper", "VirtualMemberCallInConstructor")]
        public WindowsPresentationAspNetCoreWebView(Window? window, IWebHost? host)
        {
            Window = window;
            Host = host;
        }
        
        public WindowsPresentationAspNetCoreWebView(TBuilder builder)
        {
            Builder = builder ?? throw new ArgumentNullException(nameof(builder));
        }

        protected override Task<IApplicationView> RunAsync(CancellationToken token)
        {
            if (Builder is null)
            {
                return RunAsync(Window, Host, token);
            }

            (Window? window, IWebHost? host) = Builder.Build(Arguments);
            return RunAsync(Window ?? window, Host ?? host, token);
        }
        
        protected override Task<IApplicationView> RunAsync(Window? window, CancellationToken token)
        {
            return RunAsync(window, Host ?? Builder?.Build(Arguments).Host, token);
        }
        
        protected override Task<IApplicationView> RunAsync(IWebHost? host, CancellationToken token)
        {
            return RunAsync(Window ?? Builder?.Build(Arguments).Window, host, token);
        }
    }

    public abstract class WindowsPresentationAspNetCoreWebView : WindowsPresentationAspNetCoreViewAbstraction<IWebHost, WindowsPresentationAspNetCoreWebApplication>
    {
        protected override Task RunAsync(WindowsPresentationAspNetCoreWebApplication application, IWebHost host, CancellationToken token)
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

        protected override Task RunAsync(WindowsPresentationAspNetCoreWebApplication application, Window window, IWebHost host, CancellationToken token)
        {
            if (application is null)
            {
                throw new ArgumentNullException(nameof(application));
            }

            if (window is null)
            {
                throw new ArgumentNullException(nameof(window));
            }

            if (host is null)
            {
                throw new ArgumentNullException(nameof(host));
            }

            return application.RunAsync(window, host, token);
        }
    }
}