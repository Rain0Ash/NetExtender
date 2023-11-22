// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System.Windows.Forms;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using NetExtender.AspNetCore.Types.Wrappers;
using NetExtender.Domains.Builder.Interfaces;
using NetExtender.Domains.Initializer;
using NetExtender.Domains.WinForms.AspNetCore.Applications;
using NetExtender.Domains.WinForms.AspNetCore.Builder;
using NetExtender.Domains.WinForms.AspNetCore.Context;
using NetExtender.Domains.WinForms.AspNetCore.View;
using NetExtender.Domains.WinForms.Initializer;

namespace NetExtender.Domains.WinForms.AspNetCore.Initializer
{
    public abstract class WinFormsAspNetCoreApplicationInitializer : WinFormsApplicationInitializer
    {
    }
    
    public abstract class WinFormsAspNetCoreApplicationInitializer<TBuilder> : ApplicationInitializer<WinFormsAspNetCoreApplication, WinFormsAspNetCoreView<TBuilder>> where TBuilder : IApplicationBuilder<WinFormsAspNetCoreContext>, new()
    {
        public abstract class Builder : WinFormsAspNetCoreBuilder
        {
        }
    }
    
    public abstract class WinFormsAspNetCoreApplicationInitializer<TForm, THost> : ApplicationInitializer<WinFormsAspNetCoreApplication, WinFormsAspNetCoreView<TForm, THost>> where TForm : Form, new() where THost : class, IHost, new()
    {
    }

    public abstract class WinFormsAspNetCoreApplicationInitializer<TForm, THost, TBuilder> : ApplicationInitializer<WinFormsAspNetCoreApplication, WinFormsAspNetCoreView<TForm, THost, TBuilder>> where TForm : Form where THost : class, IHost where TBuilder : IApplicationBuilder<WinFormsAspNetCoreContext<TForm, THost>>, new()
    {
        public abstract class Builder : WinFormsAspNetCoreBuilder<TForm, THost>
        {
        }
    }

    public abstract class WinFormsAspNetCoreApplicationInitializer<TForm, THost, THostBuilder, TBuilder> : ApplicationInitializer<WinFormsAspNetCoreApplication, WinFormsAspNetCoreView<TForm, THost, TBuilder>> where TForm : Form where THost : class, IHost where THostBuilder : class, IHostBuilder where TBuilder : IApplicationBuilder<WinFormsAspNetCoreContext<TForm, THost>>, new()
    {
        public abstract class Builder : WinFormsAspNetCoreBuilder<TForm, THost, THostBuilder>
        {
        }
    }
    
    public abstract class WinFormsAspNetCoreWebApplicationInitializer : WinFormsApplicationInitializer
    {
    }
    
    public abstract class WinFormsAspNetCoreWebApplicationInitializer<TBuilder> : ApplicationInitializer<WinFormsAspNetCoreWebApplication, WinFormsAspNetCoreWebView<TBuilder>> where TBuilder : IApplicationBuilder<WinFormsAspNetCoreWebContext>, new()
    {
        public abstract class Builder : WinFormsAspNetCoreWebBuilder
        {
        }
    }
    
    public abstract class WinFormsAspNetCoreWebApplicationInitializer<TForm, THost> : ApplicationInitializer<WinFormsAspNetCoreWebApplication, WinFormsAspNetCoreWebView<TForm, THost>> where TForm : Form, new() where THost : class, IWebHost, new()
    {
    }

    public abstract class WinFormsAspNetCoreWebApplicationInitializer<TForm, THost, TBuilder> : ApplicationInitializer<WinFormsAspNetCoreWebApplication, WinFormsAspNetCoreWebView<TForm, THost, TBuilder>> where TForm : Form where THost : class, IWebHost where TBuilder : IApplicationBuilder<WinFormsAspNetCoreWebContext<TForm, THost>>, new()
    {
        public abstract class Builder : WinFormsAspNetCoreWebBuilder<TForm, THost>
        {
        }
    }

    public abstract class WinFormsAspNetCoreWebApplicationInitializer<TForm, THost, THostBuilder, TBuilder> : ApplicationInitializer<WinFormsAspNetCoreWebApplication, WinFormsAspNetCoreWebView<TForm, THost, TBuilder>> where TForm : Form where THost : class, IWebHost where THostBuilder : class, IWebHostBuilder where TBuilder : IApplicationBuilder<WinFormsAspNetCoreWebContext<TForm, THost>>, new()
    {
        public abstract class Builder : WinFormsAspNetCoreWebBuilder<TForm, THost, THostBuilder>
        {
        }
    }
    
    public abstract class WinFormsAspNetCoreWebApplicationWrapperInitializer<TBuilder> : WinFormsAspNetCoreApplicationInitializer<Form, WebApplication, WebApplicationBuilderWrapper, TBuilder> where TBuilder : IApplicationBuilder<WinFormsAspNetCoreContext<Form, WebApplication>>, new()
    {
        public new abstract class Builder : WinFormsAspNetCoreWebApplicationBuilder
        {
        }
    }
    
    public abstract class WinFormsAspNetCoreWebApplicationWrapperInitializer<TForm, TBuilder> : WinFormsAspNetCoreApplicationInitializer<TForm, WebApplication, WebApplicationBuilderWrapper, TBuilder> where TForm : Form where TBuilder : IApplicationBuilder<WinFormsAspNetCoreContext<TForm, WebApplication>>, new()
    {
        public new abstract class Builder : WinFormsAspNetCoreWebApplicationBuilder<TForm>
        {
        }
    }
}