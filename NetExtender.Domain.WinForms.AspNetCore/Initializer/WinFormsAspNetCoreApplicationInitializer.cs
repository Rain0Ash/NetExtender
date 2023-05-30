// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System.Windows.Forms;
using Microsoft.Extensions.Hosting;
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
}