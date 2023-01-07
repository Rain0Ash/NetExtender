// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System.Windows;
using Microsoft.Extensions.Hosting;
using NetExtender.Domains.Builder.Interfaces;
using NetExtender.Domains.Initializer;
using NetExtender.Domains.WindowsPresentation.AspNetCore.Applications;
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
    }
    
    public abstract class WindowsPresentationAspNetCoreApplicationInitializer<TWindow, THost> : WindowsPresentationAspNetCoreApplicationInitializer<TWindow, THost, Application> where TWindow : Window, new() where THost : class, IHost, new()
    {
    }

    public abstract class WindowsPresentationAspNetCoreApplicationInitializer<TWindow, THost, TApplication> : ApplicationInitializer<WindowsPresentationAspNetCoreApplication<TApplication>, WindowsPresentationAspNetCoreView<TWindow, THost>> where TWindow : Window, new() where THost : class, IHost, new() where TApplication : Application, new()
    {
    }

    public abstract class WindowsPresentationAspNetCoreApplicationInitializer<TWindow, THost, TBuilder, TApplication> : ApplicationInitializer<WindowsPresentationAspNetCoreApplication<TApplication>, WindowsPresentationAspNetCoreView<TWindow, THost, TBuilder>> where TWindow : Window where THost : class, IHost where TBuilder : IApplicationBuilder<WindowsPresentationAspNetCoreContext<TWindow, THost>>, new() where TApplication : Application, new()
    {
    }
}