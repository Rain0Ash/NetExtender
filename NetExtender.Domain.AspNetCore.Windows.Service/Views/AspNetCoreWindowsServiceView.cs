// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;
using NetExtender.Domains.AspNetCore.Service.Applications;
using NetExtender.Domains.AspNetCore.Service.Builder;
using NetExtender.Domains.Builder.Interfaces;
using NetExtender.Domains.View;
using NetExtender.Domains.View.Interfaces;
using NetExtender.Types.Exceptions;

namespace NetExtender.Domains.AspNetCore.Service.View
{
    public class AspNetCoreWindowsServiceView<T> : AspNetCoreWindowsServiceView<T, AspNetCoreWindowsServiceBuilder<T>> where T : class, IHost, new()
    {
        public AspNetCoreWindowsServiceView()
            : base(new T())
        {
        }

        public AspNetCoreWindowsServiceView(T host)
            : base(host)
        {
        }
    }
    
    public class AspNetCoreWindowsServiceView<T, TBuilder> : AspNetCoreWindowsServiceView where T : class, IHost where TBuilder : IApplicationBuilder<T>, new()
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

        public AspNetCoreWindowsServiceView()
        {
            Builder = new TBuilder();
        }

        public AspNetCoreWindowsServiceView(T host)
        {
            Context = host ?? throw new ArgumentNullException(nameof(host));
        }
        
        public AspNetCoreWindowsServiceView(TBuilder builder)
        {
            Builder = builder ?? throw new ArgumentNullException(nameof(builder));
        }

        protected override Task<IApplicationView> RunAsync(CancellationToken token)
        {
            return RunAsync(Context ?? Builder?.Build(Arguments), token);
        }
    }

    public abstract class AspNetCoreWindowsServiceView : ContextApplicationView<IHost, AspNetCoreWindowsServiceApplication>
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