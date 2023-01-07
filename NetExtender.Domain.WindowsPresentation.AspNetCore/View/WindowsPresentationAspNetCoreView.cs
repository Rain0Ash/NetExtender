// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Diagnostics.CodeAnalysis;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using Microsoft.Extensions.Hosting;
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

    public abstract class WindowsPresentationAspNetCoreView : WindowsPresentationView
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

        protected abstract IHost? Host { get; set; }

        protected override Task<IApplicationView> RunAsync(CancellationToken token)
        {
            return RunAsync(Window, token);
        }

        protected abstract override Task<IApplicationView> RunAsync(Window? window, CancellationToken token);
        protected abstract Task<IApplicationView> RunAsync(IHost? host, CancellationToken token);

        protected virtual async Task<IApplicationView> RunAsync(Window? window, IHost? host, CancellationToken token)
        {
            if (host is null)
            {
                return await RunAsync(window, token);
            }
            
            WindowsPresentationAspNetCoreApplication application = Domain.Current.Application.As<WindowsPresentationAspNetCoreApplication>();
            
            if (window is null)
            {
                await application.RunAsync(host, token);
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

            await application.RunAsync(Window, Host, token);
            return this;
        }
        
        protected Task<IApplicationView> RunAsync<TWindow, THost>() where TWindow : Window, new() where THost : class, IHost, new()
        {
            return RunAsync<TWindow, THost>(CancellationToken.None);
        }

        protected Task<IApplicationView> RunAsync<TWindow, THost>(CancellationToken token) where TWindow : Window, new() where THost : class, IHost, new()
        {
            return RunAsync(new TWindow(), new THost(), token);
        }
    }
}