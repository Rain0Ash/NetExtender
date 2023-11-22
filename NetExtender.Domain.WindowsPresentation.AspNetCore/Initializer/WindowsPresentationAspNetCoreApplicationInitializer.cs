// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System.Windows;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using NetExtender.AspNetCore.Types.Wrappers;
using NetExtender.Domains.Builder.Interfaces;
using NetExtender.Domains.Initializer;
using NetExtender.Domains.WindowsPresentation.AspNetCore.Applications;
using NetExtender.Domains.WindowsPresentation.AspNetCore.Builder;
using NetExtender.Domains.WindowsPresentation.AspNetCore.Context;
using NetExtender.Domains.WindowsPresentation.AspNetCore.View;
using NetExtender.Domains.WindowsPresentation.Initializer;

namespace NetExtender.Domains.WindowsPresentation.AspNetCore.Initializer
{
    public abstract class WindowsPresentationAspNetCoreApplicationInitializer : WindowsPresentationApplicationInitializer
    {
    }
    
    public abstract class WindowsPresentationAspNetCoreApplicationInitializer<TBuilder> : ApplicationInitializer<WindowsPresentationAspNetCoreApplication<Application>, WindowsPresentationAspNetCoreView<TBuilder>> where TBuilder : IApplicationBuilder<WindowsPresentationAspNetCoreContext>, new()
    {
        public abstract class Builder : WindowsPresentationAspNetCoreBuilder
        {
        }
    }
    
    public abstract class WindowsPresentationAspNetCoreApplicationInitializer<TWindow, THost> : WindowsPresentationAspNetCoreApplicationInitializer<TWindow, THost, Application> where TWindow : Window, new() where THost : class, IHost, new()
    {
    }

    public abstract class WindowsPresentationAspNetCoreApplicationInitializer<TWindow, THost, TApplication> : ApplicationInitializer<WindowsPresentationAspNetCoreApplication<TApplication>, WindowsPresentationAspNetCoreView<TWindow, THost>> where TWindow : Window, new() where THost : class, IHost, new() where TApplication : Application, new()
    {
    }

    public abstract class WindowsPresentationAspNetCoreApplicationInitializer<TWindow, THost, TBuilder, TApplication> : ApplicationInitializer<WindowsPresentationAspNetCoreApplication<TApplication>, WindowsPresentationAspNetCoreView<TWindow, THost, TBuilder>> where TWindow : Window where THost : class, IHost where TBuilder : IApplicationBuilder<WindowsPresentationAspNetCoreContext<TWindow, THost>>, new() where TApplication : Application, new()
    {
        public abstract class Builder : WindowsPresentationAspNetCoreBuilder<TWindow, THost>
        {
        }
    }

    public abstract class WindowsPresentationAspNetCoreApplicationInitializer<TWindow, THost, THostBuilder, TBuilder, TApplication> : ApplicationInitializer<WindowsPresentationAspNetCoreApplication<TApplication>, WindowsPresentationAspNetCoreView<TWindow, THost, TBuilder>> where TWindow : Window where THost : class, IHost where THostBuilder : class, IHostBuilder where TBuilder : IApplicationBuilder<WindowsPresentationAspNetCoreContext<TWindow, THost>>, new() where TApplication : Application, new()
    {
        public abstract class Builder : WindowsPresentationAspNetCoreBuilder<TWindow, THost, THostBuilder>
        {
        }
    }
    
    public abstract class WindowsPresentationAspNetCoreWebApplicationInitializer<TBuilder> : ApplicationInitializer<WindowsPresentationAspNetCoreWebApplication<Application>, WindowsPresentationAspNetCoreWebView<TBuilder>> where TBuilder : IApplicationBuilder<WindowsPresentationAspNetCoreWebContext>, new()
    {
        public abstract class Builder : WindowsPresentationAspNetCoreWebBuilder
        {
        }
    }
    
    public abstract class WindowsPresentationAspNetCoreWebApplicationInitializer<TWindow, THost> : WindowsPresentationAspNetCoreWebApplicationInitializer<TWindow, THost, Application> where TWindow : Window, new() where THost : class, IWebHost, new()
    {
    }

    public abstract class WindowsPresentationAspNetCoreWebApplicationInitializer<TWindow, THost, TApplication> : ApplicationInitializer<WindowsPresentationAspNetCoreWebApplication<TApplication>, WindowsPresentationAspNetCoreWebView<TWindow, THost>> where TWindow : Window, new() where THost : class, IWebHost, new() where TApplication : Application, new()
    {
    }

    public abstract class WindowsPresentationAspNetCoreWebApplicationInitializer<TWindow, THost, TBuilder, TApplication> : ApplicationInitializer<WindowsPresentationAspNetCoreWebApplication<TApplication>, WindowsPresentationAspNetCoreWebView<TWindow, THost, TBuilder>> where TWindow : Window where THost : class, IWebHost where TBuilder : IApplicationBuilder<WindowsPresentationAspNetCoreWebContext<TWindow, THost>>, new() where TApplication : Application, new()
    {
        public abstract class Builder : WindowsPresentationAspNetCoreWebBuilder<TWindow, THost>
        {
        }
    }

    public abstract class WindowsPresentationAspNetCoreWebApplicationInitializer<TWindow, THost, THostBuilder, TBuilder, TApplication> : ApplicationInitializer<WindowsPresentationAspNetCoreWebApplication<TApplication>, WindowsPresentationAspNetCoreWebView<TWindow, THost, TBuilder>> where TWindow : Window where THost : class, IWebHost where THostBuilder : class, IWebHostBuilder where TBuilder : IApplicationBuilder<WindowsPresentationAspNetCoreWebContext<TWindow, THost>>, new() where TApplication : Application, new()
    {
        public abstract class Builder : WindowsPresentationAspNetCoreWebBuilder<TWindow, THost, THostBuilder>
        {
        }
    }
    
    public abstract class WindowsPresentationAspNetCoreWebApplicationWrapperInitializer<TBuilder> : WindowsPresentationAspNetCoreApplicationInitializer<Window, WebApplication, WebApplicationBuilderWrapper, TBuilder, Application> where TBuilder : IApplicationBuilder<WindowsPresentationAspNetCoreContext<Window, WebApplication>>, new()
    {
        public new abstract class Builder : WindowsPresentationAspNetCoreWebApplicationBuilder
        {
        }
    }
    
    public abstract class WindowsPresentationAspNetCoreWebApplicationWrapperInitializer<TWindow, TBuilder> : WindowsPresentationAspNetCoreApplicationInitializer<TWindow, WebApplication, WebApplicationBuilderWrapper, TBuilder, Application> where TWindow : Window where TBuilder : IApplicationBuilder<WindowsPresentationAspNetCoreContext<TWindow, WebApplication>>, new()
    {
        public new abstract class Builder : WindowsPresentationAspNetCoreWebApplicationBuilder<TWindow>
        {
        }
    }
    
    public abstract class WindowsPresentationAspNetCoreWebApplicationWrapperInitializer<TWindow, TBuilder, TApplication> : WindowsPresentationAspNetCoreApplicationInitializer<TWindow, WebApplication, WebApplicationBuilderWrapper, TBuilder, TApplication> where TWindow : Window where TBuilder : IApplicationBuilder<WindowsPresentationAspNetCoreContext<TWindow, WebApplication>>, new() where TApplication : Application, new()
    {
        public new abstract class Builder : WindowsPresentationAspNetCoreWebApplicationBuilder<TWindow>
        {
        }
    }
}