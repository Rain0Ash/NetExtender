// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using NetExtender.Domains.AspNetCore.Builder;

namespace NetExtender.Domains.AspNetCore.Service.Builder
{
    public class AspNetCoreWindowsServiceBuilder : AspNetCoreBuilder
    {
    }
    
    public class AspNetCoreWindowsServiceBuilder<T> : AspNetCoreBuilder<T> where T : class, IHost
    {
    }
    
    public class AspNetCoreWindowsServiceBuilder<T, TBuilder> : AspNetCoreBuilder<T, TBuilder> where T : class, IHost where TBuilder : class, IHostBuilder
    {
    }
    
    public class AspNetCoreWindowsServiceWebBuilder : AspNetCoreWebBuilder
    {
    }
    
    public class AspNetCoreWindowsServiceWebBuilder<T> : AspNetCoreWebBuilder<T> where T : class, IWebHost
    {
    }
    
    public class AspNetCoreWindowsServiceWebBuilder<T, TBuilder> : AspNetCoreWebBuilder<T, TBuilder> where T : class, IWebHost where TBuilder : class, IWebHostBuilder
    {
    }
    
    public class AspNetCoreWindowsServiceWebApplicationBuilder : AspNetCoreWebApplicationBuilder
    {
    }
}