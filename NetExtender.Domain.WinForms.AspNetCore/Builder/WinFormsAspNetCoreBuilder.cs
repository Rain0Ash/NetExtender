// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Collections.Immutable;
using System.Linq;
using System.Windows.Forms;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using NetExtender.Domains.Builder;
using NetExtender.Domains.Builder.Interfaces;
using NetExtender.Domains.WinForms.AspNetCore.Context;

namespace NetExtender.Domains.WinForms.AspNetCore.Builder
{
    public class WinFormsAspNetCoreBuilder<TForm, THost> : WinFormsAspNetCoreBuilder<TForm>, IApplicationBuilder<WinFormsAspNetCoreContext<TForm, THost>> where TForm : Form, new() where THost : class, IHost, new()
    {
        protected override THost Host(String[] arguments)
        {
            if (arguments is null)
            {
                throw new ArgumentNullException(nameof(arguments));
            }

            return new THost();
        }
        
        public override WinFormsAspNetCoreContext<TForm, THost> Build()
        {
            return Build(ImmutableArray<String>.Empty);
        }

        public override WinFormsAspNetCoreContext<TForm, THost> Build(String[] arguments)
        {
            if (arguments is null)
            {
                throw new ArgumentNullException(nameof(arguments));
            }

            return new WinFormsAspNetCoreContext<TForm, THost>(Form(arguments), Host(arguments));
        }
        
        public override WinFormsAspNetCoreContext<TForm, THost> Build(ImmutableArray<String> arguments)
        {
            return Build(arguments.ToArray());
        }
    }

    public abstract class WinFormsAspNetCoreBuilder<TForm> : WinFormsAspNetCoreBuilder, IApplicationBuilder<WinFormsAspNetCoreContext<TForm>> where TForm : Form, new()
    {
        protected override TForm Form(String[] arguments)
        {
            if (arguments is null)
            {
                throw new ArgumentNullException(nameof(arguments));
            }

            return new TForm();
        }

        public override WinFormsAspNetCoreContext<TForm> Build()
        {
            return Build(ImmutableArray<String>.Empty);
        }

        public override WinFormsAspNetCoreContext<TForm> Build(String[] arguments)
        {
            if (arguments is null)
            {
                throw new ArgumentNullException(nameof(arguments));
            }

            return new WinFormsAspNetCoreContext<TForm, IHost>(Form(arguments), Host(arguments));
        }
        
        public override WinFormsAspNetCoreContext<TForm> Build(ImmutableArray<String> arguments)
        {
            return Build(arguments.ToArray());
        }
    }

    public abstract class WinFormsAspNetCoreBuilder : ApplicationBuilder<WinFormsAspNetCoreContext>
    {
        public Boolean UseDefaultHostBuilder { get; init; } = true;
        
        protected abstract Form Form(String[] arguments);

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
        
        public override WinFormsAspNetCoreContext Build(String[] arguments)
        {
            if (arguments is null)
            {
                throw new ArgumentNullException(nameof(arguments));
            }

            return new WinFormsAspNetCoreContext<Form, IHost>(Form(arguments), Host(arguments));
        }

        protected virtual void Build(IWebHostBuilder builder)
        {
        }
    }
}