// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Collections.Immutable;
using System.Linq;
using System.Windows.Forms;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using NetExtender.AspNetCore.Types.Wrappers;
using NetExtender.Domains.Builder;
using NetExtender.Domains.Builder.Interfaces;
using NetExtender.Domains.WinForms.AspNetCore.Context;

namespace NetExtender.Domains.WinForms.AspNetCore.Builder
{
    public class WinFormsAspNetCoreBuilder<TForm, THost, THostBuilder> : WinFormsAspNetCoreBuilder<TForm, THost> where TForm : Form where THost : class, IHost where THostBuilder : class, IHostBuilder
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
        
        public override WinFormsAspNetCoreContext<TForm, THost> Build()
        {
            return Build(ImmutableArray<String>.Empty);
        }

        public override WinFormsAspNetCoreContext<TForm, THost> Build(String[]? arguments)
        {
            return Build(arguments?.ToImmutableArray() ?? ImmutableArray<String>.Empty);
        }
        
        public override WinFormsAspNetCoreContext<TForm, THost> Build(ImmutableArray<String> arguments)
        {
            return new WinFormsAspNetCoreContext<TForm, THost>(Form(arguments), Host(arguments));
        }
    }
    
    public class WinFormsAspNetCoreBuilder<TForm, THost> : WinFormsAspNetCoreBuilder<TForm>, IApplicationBuilder<WinFormsAspNetCoreContext<TForm, THost>> where TForm : Form where THost : class, IHost
    {
        protected override THost Host(ImmutableArray<String> arguments)
        {
            return Host(New<THost>(arguments));
        }
        
        protected virtual THost Host(THost host)
        {
            return host ?? throw new ArgumentNullException(nameof(host));
        }
        
        public override WinFormsAspNetCoreContext<TForm, THost> Build()
        {
            return Build(ImmutableArray<String>.Empty);
        }

        public override WinFormsAspNetCoreContext<TForm, THost> Build(String[]? arguments)
        {
            return Build(arguments?.ToImmutableArray() ?? ImmutableArray<String>.Empty);
        }
        
        public override WinFormsAspNetCoreContext<TForm, THost> Build(ImmutableArray<String> arguments)
        {
            return new WinFormsAspNetCoreContext<TForm, THost>(Form(arguments), Host(arguments));
        }
    }

    public abstract class WinFormsAspNetCoreBuilder<TForm> : WinFormsAspNetCoreBuilder, IApplicationBuilder<WinFormsAspNetCoreContext<TForm>> where TForm : Form
    {
        protected override TForm Form(ImmutableArray<String> arguments)
        {
            return New<TForm>(arguments);
        }

        public override WinFormsAspNetCoreContext<TForm> Build()
        {
            return Build(ImmutableArray<String>.Empty);
        }

        public override WinFormsAspNetCoreContext<TForm> Build(String[]? arguments)
        {
            return Build(arguments?.ToImmutableArray() ?? ImmutableArray<String>.Empty);
        }
        
        public override WinFormsAspNetCoreContext<TForm> Build(ImmutableArray<String> arguments)
        {
            return new WinFormsAspNetCoreContext<TForm, IHost>(Form(arguments), Host(arguments));
        }
    }

    public abstract class WinFormsAspNetCoreBuilder : ApplicationBuilder<WinFormsAspNetCoreContext>
    {
        public Boolean UseDefaultHostBuilder { get; init; } = true;
        
        protected abstract Form Form(ImmutableArray<String> arguments);

        protected virtual IHost Host(ImmutableArray<String> arguments)
        {
            IHostBuilder builder = new HostBuilder();

            if (UseDefaultHostBuilder)
            {
                builder.ConfigureDefaults(arguments.ToArray());
            }

            return builder.ConfigureWebHostDefaults(Build).Build();
        }
        
        public override WinFormsAspNetCoreContext Build(ImmutableArray<String> arguments)
        {
            return new WinFormsAspNetCoreContext<Form, IHost>(Form(arguments), Host(arguments));
        }

        protected virtual void Build(IWebHostBuilder builder)
        {
        }
    }
    
    public class WinFormsAspNetCoreWebBuilder<TForm, THost, THostBuilder> : WinFormsAspNetCoreWebBuilder<TForm, THost> where TForm : Form where THost : class, IWebHost where THostBuilder : class, IWebHostBuilder
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
        
        public override WinFormsAspNetCoreWebContext<TForm, THost> Build()
        {
            return Build(ImmutableArray<String>.Empty);
        }

        public override WinFormsAspNetCoreWebContext<TForm, THost> Build(String[]? arguments)
        {
            return Build(arguments?.ToImmutableArray() ?? ImmutableArray<String>.Empty);
        }
        
        public override WinFormsAspNetCoreWebContext<TForm, THost> Build(ImmutableArray<String> arguments)
        {
            return new WinFormsAspNetCoreWebContext<TForm, THost>(Form(arguments), Host(arguments));
        }
    }
    
    public class WinFormsAspNetCoreWebBuilder<TForm, THost> : WinFormsAspNetCoreWebBuilder<TForm>, IApplicationBuilder<WinFormsAspNetCoreWebContext<TForm, THost>> where TForm : Form where THost : class, IWebHost
    {
        protected override THost Host(ImmutableArray<String> arguments)
        {
            return Host(New<THost>(arguments));
        }
        
        protected virtual THost Host(THost host)
        {
            return host ?? throw new ArgumentNullException(nameof(host));
        }
        
        public override WinFormsAspNetCoreWebContext<TForm, THost> Build()
        {
            return Build(ImmutableArray<String>.Empty);
        }

        public override WinFormsAspNetCoreWebContext<TForm, THost> Build(String[]? arguments)
        {
            return Build(arguments?.ToImmutableArray() ?? ImmutableArray<String>.Empty);
        }
        
        public override WinFormsAspNetCoreWebContext<TForm, THost> Build(ImmutableArray<String> arguments)
        {
            return new WinFormsAspNetCoreWebContext<TForm, THost>(Form(arguments), Host(arguments));
        }
    }

    public abstract class WinFormsAspNetCoreWebBuilder<TForm> : WinFormsAspNetCoreWebBuilder, IApplicationBuilder<WinFormsAspNetCoreWebContext<TForm>> where TForm : Form
    {
        protected override TForm Form(ImmutableArray<String> arguments)
        {
            return New<TForm>(arguments);
        }

        public override WinFormsAspNetCoreWebContext<TForm> Build()
        {
            return Build(ImmutableArray<String>.Empty);
        }

        public override WinFormsAspNetCoreWebContext<TForm> Build(String[]? arguments)
        {
            return Build(arguments?.ToImmutableArray() ?? ImmutableArray<String>.Empty);
        }
        
        public override WinFormsAspNetCoreWebContext<TForm> Build(ImmutableArray<String> arguments)
        {
            return new WinFormsAspNetCoreWebContext<TForm, IWebHost>(Form(arguments), Host(arguments));
        }
    }

    public abstract class WinFormsAspNetCoreWebBuilder : ApplicationBuilder<WinFormsAspNetCoreWebContext>
    {
        protected abstract Form Form(ImmutableArray<String> arguments);

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
        
        public override WinFormsAspNetCoreWebContext Build(ImmutableArray<String> arguments)
        {
            return new WinFormsAspNetCoreWebContext<Form, IWebHost>(Form(arguments), Host(arguments));
        }

        protected virtual void Build(IWebHostBuilder builder)
        {
        }
    }
    
    public abstract class WinFormsAspNetCoreWebApplicationBuilder<TForm> : WinFormsAspNetCoreBuilder<TForm, WebApplication, WebApplicationBuilderWrapper>, IApplicationBuilder<WinFormsAspNetCoreWebApplicationContext<TForm>> where TForm : Form
    {
        public override WinFormsAspNetCoreWebApplicationContext<TForm> Build()
        {
            return Build(ImmutableArray<String>.Empty);
        }

        public override WinFormsAspNetCoreWebApplicationContext<TForm> Build(String[]? arguments)
        {
            return Build(arguments?.ToImmutableArray() ?? ImmutableArray<String>.Empty);
        }

        public override WinFormsAspNetCoreWebApplicationContext<TForm> Build(ImmutableArray<String> arguments)
        {
            return new WinFormsAspNetCoreWebApplicationContext<TForm>(Form(arguments), Host(arguments));
        }
    }
    
    public abstract class WinFormsAspNetCoreWebApplicationBuilder : WinFormsAspNetCoreBuilder<Form, WebApplication, WebApplicationBuilderWrapper>, IApplicationBuilder<WinFormsAspNetCoreWebApplicationContext<Form>>
    {
        public override WinFormsAspNetCoreWebApplicationContext<Form> Build()
        {
            return Build(ImmutableArray<String>.Empty);
        }

        public override WinFormsAspNetCoreWebApplicationContext<Form> Build(String[]? arguments)
        {
            return Build(arguments?.ToImmutableArray() ?? ImmutableArray<String>.Empty);
        }

        public override WinFormsAspNetCoreWebApplicationContext<Form> Build(ImmutableArray<String> arguments)
        {
            return new WinFormsAspNetCoreWebApplicationContext<Form>(Form(arguments), Host(arguments));
        }
    }
}