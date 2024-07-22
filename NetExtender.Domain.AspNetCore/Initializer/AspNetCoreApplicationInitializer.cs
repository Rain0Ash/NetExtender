// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using NetExtender.AspNetCore.Types.Wrappers;
using NetExtender.Domains.AspNetCore.Applications;
using NetExtender.Domains.AspNetCore.Builder;
using NetExtender.Domains.AspNetCore.View;
using NetExtender.Domains.Builder.Interfaces;
using NetExtender.Types.Middlewares;

namespace NetExtender.Domains.Initializer
{
    public abstract class AspNetCoreApplicationInitializer<T> : ApplicationInitializer<AspNetCoreApplication, AspNetCoreView<T>> where T : class, IHost, new()
    {
        public abstract class Builder : AspNetCoreBuilder<T>
        {
        }
        
        public abstract class Middleware<TBuilder> : NetExtender.Types.Middlewares.Middleware<TBuilder> where TBuilder : Builder
        {
        }
    }
    
    public abstract class AspNetCoreApplicationInitializer<T, TBuilder> : ApplicationInitializer<AspNetCoreApplication, AspNetCoreView<T, TBuilder>> where T : class, IHost where TBuilder : IApplicationBuilder<T>, new()
    {
        public abstract class Builder : AspNetCoreBuilder<T>
        {
        }
        
        public abstract class Middleware : Middleware<TBuilder>
        {
        }
    }
    
    public abstract class AspNetCoreApplicationInitializer<T, TBuilder, TApplicationBuilder> : AspNetCoreApplicationInitializer<T, TApplicationBuilder> where T : class, IHost where TBuilder : class, IHostBuilder where TApplicationBuilder : IApplicationBuilder<T>, new()
    {
        public new abstract class Builder : AspNetCoreBuilder<T, TBuilder>
        {
        }
        
        public new abstract class Middleware : Middleware<TBuilder>
        {
        }
    }
    
    public abstract class AspNetCoreWebApplicationInitializer<T> : ApplicationInitializer<AspNetCoreWebApplication, AspNetCoreWebView<T>> where T : class, IWebHost, new()
    {
        public abstract class Builder : AspNetCoreWebBuilder<T>
        {
        }
        
        public abstract class Middleware<TBuilder> : NetExtender.Types.Middlewares.Middleware<TBuilder> where TBuilder : Builder
        {
        }
    }
    
    public abstract class AspNetCoreWebApplicationInitializer<T, TBuilder> : ApplicationInitializer<AspNetCoreWebApplication, AspNetCoreWebView<T, TBuilder>> where T : class, IWebHost where TBuilder : IApplicationBuilder<T>, new()
    {
        public abstract class Builder : AspNetCoreWebBuilder<T>
        {
        }
        
        public abstract class Middleware : Middleware<TBuilder>
        {
        }
    }
    
    public abstract class AspNetCoreWebApplicationInitializer<T, TBuilder, TApplicationBuilder> : AspNetCoreWebApplicationInitializer<T, TApplicationBuilder> where T : class, IWebHost where TBuilder : class, IWebHostBuilder where TApplicationBuilder : IApplicationBuilder<T>, new()
    {
        public new abstract class Builder : AspNetCoreWebBuilder<T, TBuilder>
        {
        }
        
        public new abstract class Middleware : Middleware<TBuilder>
        {
        }
    }
    
    public abstract class AspNetCoreWebApplicationWrapperInitializer<TBuilder> : AspNetCoreApplicationInitializer<WebApplication, WebApplicationBuilderWrapper, TBuilder> where TBuilder : IApplicationBuilder<WebApplication>, new()
    {
        public new abstract class Builder : AspNetCoreWebApplicationBuilder
        {
        }
        
        public new abstract class Middleware : Middleware<TBuilder>
        {
        }
    }
}