// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using NetExtender.Domains.AspNetCore.Applications;
using NetExtender.Domains.AspNetCore.Builder;
using NetExtender.Domains.Builder.Interfaces;
using NetExtender.Domains.View;
using NetExtender.Domains.View.Interfaces;
using NetExtender.Types.Exceptions;

namespace NetExtender.Domains.AspNetCore.View
{
    public class AspNetCoreView<T> : AspNetCoreView<T, AspNetCoreBuilder<T>> where T : class, IHost, new()
    {
        public AspNetCoreView()
            : base(new T())
        {
        }

        public AspNetCoreView(T host)
            : base(host)
        {
        }
    }
    
    public class AspNetCoreView<T, TBuilder> : AspNetCoreView where T : class, IHost where TBuilder : IApplicationBuilder<T>, new()
    {
        protected T? Internal { get; set; }

        protected sealed override IHost? Context
        {
            get
            {
                return Internal;
            }
            set
            {
                Internal = value is not null ? value as T ?? throw new InitializeException($"{nameof(value)} is not {typeof(T).Name}") : null;
            }
        }

        protected TBuilder? Builder { get; }

        public AspNetCoreView()
        {
            Builder = new TBuilder();
        }

        public AspNetCoreView(T host)
        {
            Context = host ?? throw new ArgumentNullException(nameof(host));
        }
        
        public AspNetCoreView(TBuilder builder)
        {
            Builder = builder ?? throw new ArgumentNullException(nameof(builder));
        }

        protected override Task<IApplicationView> RunAsync(CancellationToken token)
        {
            return RunAsync(Context ?? Builder?.Build(Arguments), token);
        }
    }

    public abstract class AspNetCoreView : ContextApplicationView<IHost, AspNetCoreApplication>
    {
        protected override ApplicationShutdownMode? ShutdownMode
        {
            get
            {
                return ApplicationShutdownMode.OnExplicitShutdown;
            }
        }
    }
    
    public class AspNetCoreWebView<T> : AspNetCoreWebView<T, AspNetCoreWebBuilder<T>> where T : class, IWebHost, new()
    {
        public AspNetCoreWebView()
            : base(new T())
        {
        }

        public AspNetCoreWebView(T host)
            : base(host)
        {
        }
    }
    
    public class AspNetCoreWebView<T, TBuilder> : AspNetCoreWebView where T : class, IWebHost where TBuilder : IApplicationBuilder<T>, new()
    {
        protected T? Internal { get; set; }

        protected sealed override IWebHost? Context
        {
            get
            {
                return Internal;
            }
            set
            {
                Internal = value is not null ? value as T ?? throw new InitializeException($"{nameof(value)} is not {typeof(T).Name}") : null;
            }
        }

        protected TBuilder? Builder { get; }

        public AspNetCoreWebView()
        {
            Builder = new TBuilder();
        }

        public AspNetCoreWebView(T host)
        {
            Context = host ?? throw new ArgumentNullException(nameof(host));
        }
        
        public AspNetCoreWebView(TBuilder builder)
        {
            Builder = builder ?? throw new ArgumentNullException(nameof(builder));
        }

        protected override Task<IApplicationView> RunAsync(CancellationToken token)
        {
            return RunAsync(Context ?? Builder?.Build(Arguments), token);
        }
    }

    public abstract class AspNetCoreWebView : ContextApplicationView<IWebHost, AspNetCoreWebApplication>
    {
        protected override ApplicationShutdownMode? ShutdownMode
        {
            get
            {
                return ApplicationShutdownMode.OnExplicitShutdown;
            }
        }
    }
}