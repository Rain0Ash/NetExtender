// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using NetExtender.Domain.Utilities;
using NetExtender.Domains.AspNetCore.Applications;
using NetExtender.Domains.View;
using NetExtender.Domains.View.Interfaces;

namespace NetExtender.Domains.AspNetCore.View
{
    public class AspNetCoreView<T> : AspNetCoreView where T : class, IHost, new()
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

    public class AspNetCoreView : ApplicationView
    {
        protected IHost? Context { get; private set; }
        protected Action<IWebHostBuilder>? Builder { get; }
        protected Boolean UseDefaultHostBuilder { get; }

        protected override ApplicationShutdownMode? ShutdownMode
        {
            get
            {
                return ApplicationShutdownMode.OnExplicitShutdown;
            }
        }

        public AspNetCoreView(IHost host)
        {
            Context = host ?? throw new ArgumentNullException(nameof(host));
        }

        public AspNetCoreView(Action<IWebHostBuilder> builder)
            : this(builder, false)
        {
        }

        public AspNetCoreView(Action<IWebHostBuilder> builder, Boolean initialize)
        {
            Builder = builder ?? throw new ArgumentNullException(nameof(builder));
            UseDefaultHostBuilder = initialize;
        }

        protected IHostBuilder CreateHostBuilder()
        {
            return CreateHostBuilder(Arguments.ToArray());
        }

        protected virtual IHostBuilder CreateHostBuilder(String[] args)
        {
            IHostBuilder builder = UseDefaultHostBuilder ? Host.CreateDefaultBuilder(args) : new HostBuilder();
            return builder.ConfigureWebHostDefaults(Build);
        }

        protected virtual void Build(IWebHostBuilder builder)
        {
            Builder?.Invoke(builder);
        }

        protected override Task<IApplicationView> RunAsync(CancellationToken token)
        {
            return RunAsync(Context ?? CreateHostBuilder().Build(), token);
        }

        protected virtual async Task<IApplicationView> RunAsync(IHost? host, CancellationToken token)
        {
            if (host is null)
            {
                return await RunAsync(token);
            }

            Context ??= host;
            if (host != Context)
            {
                throw new ArgumentException($"{nameof(host)} not reference equals with {nameof(Context)}");
            }

            AspNetCoreApplication application = Domain.Current.Application.As<AspNetCoreApplication>();
            await application.RunAsync(Context, token);
            return this;
        }
    }
}