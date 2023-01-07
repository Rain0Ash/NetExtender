// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Collections.Immutable;
using System.Linq;
using System.Windows;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using NetExtender.Domains.Builder;
using NetExtender.Domains.Builder.Interfaces;
using NetExtender.Domains.WindowsPresentation.AspNetCore.Context;

namespace NetExtender.Domains.WindowsPresentation.AspNetCore.Builder
{
    public class WindowsPresentationAspNetCoreBuilder<TWindow, THost> : WindowsPresentationAspNetCoreBuilder<TWindow>, IApplicationBuilder<WindowsPresentationAspNetCoreContext<TWindow, THost>> where TWindow : Window, new() where THost : class, IHost, new()
    {
        protected override THost Host(String[] arguments)
        {
            if (arguments is null)
            {
                throw new ArgumentNullException(nameof(arguments));
            }

            return new THost();
        }

        public override WindowsPresentationAspNetCoreContext<TWindow, THost> Build()
        {
            return Build(ImmutableArray<String>.Empty);
        }

        public override WindowsPresentationAspNetCoreContext<TWindow, THost> Build(String[] arguments)
        {
            if (arguments is null)
            {
                throw new ArgumentNullException(nameof(arguments));
            }

            return new WindowsPresentationAspNetCoreContext<TWindow, THost>(Window(arguments), Host(arguments));
        }

        public override WindowsPresentationAspNetCoreContext<TWindow, THost> Build(ImmutableArray<String> arguments)
        {
            return Build(arguments.ToArray());
        }
    }
    
    public abstract class WindowsPresentationAspNetCoreBuilder<TWindow> : WindowsPresentationAspNetCoreBuilder, IApplicationBuilder<WindowsPresentationAspNetCoreContext<TWindow>> where TWindow : Window, new()
    {
        protected override TWindow Window(String[] arguments)
        {
            if (arguments is null)
            {
                throw new ArgumentNullException(nameof(arguments));
            }

            return new TWindow();
        }

        public override WindowsPresentationAspNetCoreContext<TWindow> Build()
        {
            return Build(ImmutableArray<String>.Empty);
        }

        public override WindowsPresentationAspNetCoreContext<TWindow> Build(String[] arguments)
        {
            if (arguments is null)
            {
                throw new ArgumentNullException(nameof(arguments));
            }

            return new WindowsPresentationAspNetCoreContext<TWindow, IHost>(Window(arguments), Host(arguments));
        }

        public override WindowsPresentationAspNetCoreContext<TWindow> Build(ImmutableArray<String> arguments)
        {
            return Build(arguments.ToArray());
        }
    }

    public abstract class WindowsPresentationAspNetCoreBuilder : ApplicationBuilder<WindowsPresentationAspNetCoreContext>
    {
        public Boolean UseDefaultHostBuilder { get; init; } = true;
        
        protected abstract Window Window(String[] arguments);
        
        protected virtual IHost Host(String[] arguments)
        {
            if (arguments is null)
            {
                throw new ArgumentNullException(nameof(arguments));
            }

            IHostBuilder builder = new HostBuilder();

            if (UseDefaultHostBuilder)
            {
                builder.ConfigureDefaults(arguments);
            }

            return builder.ConfigureWebHostDefaults(Build).Build();
        }
        
        public override WindowsPresentationAspNetCoreContext Build(String[] arguments)
        {
            if (arguments is null)
            {
                throw new ArgumentNullException(nameof(arguments));
            }

            return new WindowsPresentationAspNetCoreContext<Window, IHost>(Window(arguments), Host(arguments));
        }
        
        protected virtual void Build(IWebHostBuilder builder)
        {
        }
    }
}