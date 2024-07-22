// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using NetExtender.AspNetCore.Types.Wrappers;
using NetExtender.Domains.AspNetCore.Service.Applications;
using NetExtender.Domains.AspNetCore.Service.Builder;
using NetExtender.Domains.AspNetCore.Service.View;
using NetExtender.Domains.Builder.Interfaces;
using NetExtender.Types.Middlewares;

namespace NetExtender.Domains.Initializer
{
    public abstract class AspNetCoreWindowsServiceApplicationInitializer<T> : ApplicationInitializer<AspNetCoreWindowsServiceApplication, AspNetCoreWindowsServiceView<T>> where T : class, IHost, new()
    {
        public abstract class Middleware<TBuilder> : NetExtender.Types.Middlewares.Middleware<TBuilder> where TBuilder : AspNetCoreWindowsServiceBuilder<T>
        {
        }
    }
    
    public abstract class AspNetCoreWindowsServiceApplicationInitializer<T, TBuilder> : ApplicationInitializer<AspNetCoreWindowsServiceApplication, AspNetCoreWindowsServiceView<T, TBuilder>> where T : class, IHost, new() where TBuilder : IApplicationBuilder<T>, new()
    {
        public abstract class Builder : AspNetCoreWindowsServiceBuilder<T>
        {
        }
        
        public abstract class Middleware : Middleware<TBuilder>
        {
        }
    }
    
    public abstract class AspNetCoreWindowsServiceApplicationInitializer<T, TBuilder, TApplicationBuilder> : ApplicationInitializer<AspNetCoreWindowsServiceApplication, AspNetCoreWindowsServiceView<T, TApplicationBuilder>> where T : class, IHost where TBuilder : class, IHostBuilder where TApplicationBuilder : IApplicationBuilder<T>, new()
    {
        public abstract class Builder : AspNetCoreWindowsServiceBuilder<T, TBuilder>
        {
        }
        
        public abstract class Middleware : Middleware<TBuilder>
        {
        }
    }
    
    public abstract class AspNetCoreWindowsServiceWebApplicationInitializer<T> : ApplicationInitializer<AspNetCoreWindowsServiceWebApplication, AspNetCoreWindowsServiceWebView<T>> where T : class, IWebHost, new()
    {
        public abstract class Middleware<TBuilder> : NetExtender.Types.Middlewares.Middleware<TBuilder> where TBuilder : AspNetCoreWindowsServiceWebBuilder<T>
        {
        }
    }
    
    public abstract class AspNetCoreWindowsServiceWebApplicationInitializer<T, TBuilder> : ApplicationInitializer<AspNetCoreWindowsServiceWebApplication, AspNetCoreWindowsServiceWebView<T, TBuilder>> where T : class, IWebHost, new() where TBuilder : IApplicationBuilder<T>, new()
    {
        public abstract class Builder : AspNetCoreWindowsServiceWebBuilder<T>
        {
        }
        
        public abstract class Middleware : Middleware<TBuilder>
        {
        }
    }
    
    public abstract class AspNetCoreWindowsServiceWebApplicationInitializer<T, TBuilder, TApplicationBuilder> : ApplicationInitializer<AspNetCoreWindowsServiceWebApplication, AspNetCoreWindowsServiceWebView<T, TApplicationBuilder>> where T : class, IWebHost where TBuilder : class, IWebHostBuilder, new() where TApplicationBuilder : IApplicationBuilder<T>, new()
    {
        public abstract class Builder : AspNetCoreWindowsServiceWebBuilder<T, TBuilder>
        {
        }
        
        public abstract class Middleware : Middleware<TBuilder>
        {
        }
    }
    
    public abstract class AspNetCoreWindowsServiceWebApplicationWrapperInitializer<TBuilder> : AspNetCoreWindowsServiceApplicationInitializer<WebApplication, WebApplicationBuilderWrapper, TBuilder> where TBuilder : IApplicationBuilder<WebApplication>, new()
    {
        public new abstract class Builder : AspNetCoreWindowsServiceWebApplicationBuilder
        {
        }
        
        public new abstract class Middleware : Middleware<TBuilder>
        {
        }
    }
}