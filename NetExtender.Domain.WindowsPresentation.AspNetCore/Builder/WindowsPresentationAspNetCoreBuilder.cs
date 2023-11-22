// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Collections.Immutable;
using System.Linq;
using System.Windows;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using NetExtender.AspNetCore.Types.Wrappers;
using NetExtender.Domains.Builder;
using NetExtender.Domains.Builder.Interfaces;
using NetExtender.Domains.WindowsPresentation.AspNetCore.Context;

namespace NetExtender.Domains.WindowsPresentation.AspNetCore.Builder
{
    public class WindowsPresentationAspNetCoreBuilder<TWindow, THost, THostBuilder> : WindowsPresentationAspNetCoreBuilder<TWindow, THost> where TWindow : Window where THost : class, IHost where THostBuilder : class, IHostBuilder
    {
        protected override THost Host(ImmutableArray<String> arguments)
        {
            return Host(New<THostBuilder>(arguments));
        }
        
        protected virtual THost Host(THostBuilder builder)
        {
            if (builder is null)
            {
                throw new ArgumentNullException(nameof(builder));
            }

            return builder.Build() is THost host ? Host(host) : throw new InvalidOperationException();
        }
    }
    
    public class WindowsPresentationAspNetCoreBuilder<TWindow, THost> : WindowsPresentationAspNetCoreBuilder<TWindow>, IApplicationBuilder<WindowsPresentationAspNetCoreContext<TWindow, THost>> where TWindow : Window where THost : class, IHost
    {
        protected override THost Host(ImmutableArray<String> arguments)
        {
            return Host(New<THost>(arguments));
        }
        
        protected virtual THost Host(THost host)
        {
            return host ?? throw new ArgumentNullException(nameof(host));
        }

        public override WindowsPresentationAspNetCoreContext<TWindow, THost> Build()
        {
            return Build(ImmutableArray<String>.Empty);
        }
        
        public override WindowsPresentationAspNetCoreContext<TWindow, THost> Build(String[]? arguments)
        {
            return Build(arguments?.ToImmutableArray() ?? ImmutableArray<String>.Empty);
        }

        public override WindowsPresentationAspNetCoreContext<TWindow, THost> Build(ImmutableArray<String> arguments)
        {
            return new WindowsPresentationAspNetCoreContext<TWindow, THost>(Window(arguments), Host(arguments));
        }
    }
    
    public abstract class WindowsPresentationAspNetCoreBuilder<TWindow> : WindowsPresentationAspNetCoreBuilder, IApplicationBuilder<WindowsPresentationAspNetCoreContext<TWindow>> where TWindow : Window
    {
        protected override TWindow Window(ImmutableArray<String> arguments)
        {
            return New<TWindow>(arguments);
        }

        public override WindowsPresentationAspNetCoreContext<TWindow> Build()
        {
            return Build(ImmutableArray<String>.Empty);
        }

        public override WindowsPresentationAspNetCoreContext<TWindow> Build(String[]? arguments)
        {
            return Build(arguments?.ToImmutableArray() ?? ImmutableArray<String>.Empty);
        }

        public override WindowsPresentationAspNetCoreContext<TWindow> Build(ImmutableArray<String> arguments)
        {
            return new WindowsPresentationAspNetCoreContext<TWindow, IHost>(Window(arguments), Host(arguments));
        }
    }

    public abstract class WindowsPresentationAspNetCoreBuilder : ApplicationBuilder<WindowsPresentationAspNetCoreContext>
    {
        public Boolean UseDefaultHostBuilder { get; init; } = true;
        
        protected abstract Window Window(ImmutableArray<String> arguments);
        
        protected virtual IHost Host(ImmutableArray<String> arguments)
        {
            IHostBuilder builder = new HostBuilder();

            if (UseDefaultHostBuilder)
            {
                builder.ConfigureDefaults(arguments.ToArray());
            }

            return builder.ConfigureWebHostDefaults(Build).Build();
        }
        
        public override WindowsPresentationAspNetCoreContext Build(ImmutableArray<String> arguments)
        {
            return new WindowsPresentationAspNetCoreContext<Window, IHost>(Window(arguments), Host(arguments));
        }
        
        protected virtual void Build(IWebHostBuilder builder)
        {
        }
    }
    
    public class WindowsPresentationAspNetCoreWebBuilder<TWindow, THost, THostBuilder> : WindowsPresentationAspNetCoreWebBuilder<TWindow, THost> where TWindow : Window where THost : class, IWebHost where THostBuilder : class, IWebHostBuilder
    {
        protected override THost Host(ImmutableArray<String> arguments)
        {
            return Host(New<THostBuilder>(arguments));
        }
        
        protected virtual THost Host(THostBuilder builder)
        {
            if (builder is null)
            {
                throw new ArgumentNullException(nameof(builder));
            }

            return builder.Build() is THost host ? Host(host) : throw new InvalidOperationException();
        }
    }
    
    public class WindowsPresentationAspNetCoreWebBuilder<TWindow, THost> : WindowsPresentationAspNetCoreWebBuilder<TWindow>, IApplicationBuilder<WindowsPresentationAspNetCoreWebContext<TWindow, THost>> where TWindow : Window where THost : class, IWebHost
    {
        protected override THost Host(ImmutableArray<String> arguments)
        {
            return Host(New<THost>(arguments));
        }
        
        protected virtual THost Host(THost host)
        {
            return host ?? throw new ArgumentNullException(nameof(host));
        }

        public override WindowsPresentationAspNetCoreWebContext<TWindow, THost> Build()
        {
            return Build(ImmutableArray<String>.Empty);
        }
        
        public override WindowsPresentationAspNetCoreWebContext<TWindow, THost> Build(String[]? arguments)
        {
            return Build(arguments?.ToImmutableArray() ?? ImmutableArray<String>.Empty);
        }

        public override WindowsPresentationAspNetCoreWebContext<TWindow, THost> Build(ImmutableArray<String> arguments)
        {
            return new WindowsPresentationAspNetCoreWebContext<TWindow, THost>(Window(arguments), Host(arguments));
        }
    }
    
    public abstract class WindowsPresentationAspNetCoreWebBuilder<TWindow> : WindowsPresentationAspNetCoreWebBuilder, IApplicationBuilder<WindowsPresentationAspNetCoreWebContext<TWindow>> where TWindow : Window
    {
        protected override TWindow Window(ImmutableArray<String> arguments)
        {
            return New<TWindow>(arguments);
        }

        public override WindowsPresentationAspNetCoreWebContext<TWindow> Build()
        {
            return Build(ImmutableArray<String>.Empty);
        }

        public override WindowsPresentationAspNetCoreWebContext<TWindow> Build(String[]? arguments)
        {
            return Build(arguments?.ToImmutableArray() ?? ImmutableArray<String>.Empty);
        }

        public override WindowsPresentationAspNetCoreWebContext<TWindow> Build(ImmutableArray<String> arguments)
        {
            return new WindowsPresentationAspNetCoreWebContext<TWindow, IWebHost>(Window(arguments), Host(arguments));
        }
    }

    public abstract class WindowsPresentationAspNetCoreWebBuilder : ApplicationBuilder<WindowsPresentationAspNetCoreWebContext>
    {
        protected abstract Window Window(ImmutableArray<String> arguments);

        protected virtual IWebHost Host(ImmutableArray<String> arguments)
        {
            return Host(new WebHostBuilder());
        }

        protected virtual IWebHost Host(IWebHostBuilder builder)
        {
            if (builder is null)
            {
                throw new ArgumentNullException(nameof(builder));
            }

            return builder.Build() is IWebHost application ? Host(application) : throw new InvalidOperationException();
        }

        protected virtual IWebHost Host(IWebHost application)
        {
            return application ?? throw new ArgumentNullException(nameof(application));
        }
        
        public override WindowsPresentationAspNetCoreWebContext Build(ImmutableArray<String> arguments)
        {
            return new WindowsPresentationAspNetCoreWebContext<Window, IWebHost>(Window(arguments), Host(arguments));
        }
        
        protected virtual void Build(IWebHostBuilder builder)
        {
        }
    }
    
    public abstract class WindowsPresentationAspNetCoreWebApplicationBuilder<TWindow> : WindowsPresentationAspNetCoreBuilder<TWindow, WebApplication, WebApplicationBuilderWrapper>, IApplicationBuilder<WindowsPresentationAspNetCoreWebApplicationContext<TWindow>> where TWindow : Window
    {
        public override WindowsPresentationAspNetCoreWebApplicationContext<TWindow> Build()
        {
            return Build(ImmutableArray<String>.Empty);
        }

        public override WindowsPresentationAspNetCoreWebApplicationContext<TWindow> Build(String[]? arguments)
        {
            return Build(arguments?.ToImmutableArray() ?? ImmutableArray<String>.Empty);
        }

        public override WindowsPresentationAspNetCoreWebApplicationContext<TWindow> Build(ImmutableArray<String> arguments)
        {
            return new WindowsPresentationAspNetCoreWebApplicationContext<TWindow>(Window(arguments), Host(arguments));
        }
    }
    
    public abstract class WindowsPresentationAspNetCoreWebApplicationBuilder : WindowsPresentationAspNetCoreBuilder<Window, WebApplication, WebApplicationBuilderWrapper>, IApplicationBuilder<WindowsPresentationAspNetCoreWebApplicationContext<Window>>
    {
        public override WindowsPresentationAspNetCoreWebApplicationContext<Window> Build()
        {
            return Build(ImmutableArray<String>.Empty);
        }

        public override WindowsPresentationAspNetCoreWebApplicationContext<Window> Build(String[]? arguments)
        {
            return Build(arguments?.ToImmutableArray() ?? ImmutableArray<String>.Empty);
        }

        public override WindowsPresentationAspNetCoreWebApplicationContext<Window> Build(ImmutableArray<String> arguments)
        {
            return new WindowsPresentationAspNetCoreWebApplicationContext<Window>(Window(arguments), Host(arguments));
        }
    }
}