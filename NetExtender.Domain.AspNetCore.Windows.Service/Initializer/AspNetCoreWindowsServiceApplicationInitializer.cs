// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using Microsoft.Extensions.Hosting;
using NetExtender.Domains.AspNetCore.Service.Applications;
using NetExtender.Domains.AspNetCore.Service.View;
using NetExtender.Domains.Builder.Interfaces;
using NetExtender.Domains.Initializer;

namespace NetExtender.Domains.AspNetCore.Service.Initializer
{
    public abstract class AspNetCoreWindowsServiceApplicationInitializer<T> : ApplicationInitializer<AspNetCoreWindowsServiceApplication, AspNetCoreWindowsServiceView<T>> where T : class, IHost, new()
    {
    }
    
    public abstract class AspNetCoreWindowsServiceApplicationInitializer<T, TBuilder> : ApplicationInitializer<AspNetCoreWindowsServiceApplication, AspNetCoreWindowsServiceView<T, TBuilder>> where T : class, IHost where TBuilder : IApplicationBuilder<T>, new()
    {
    }
}