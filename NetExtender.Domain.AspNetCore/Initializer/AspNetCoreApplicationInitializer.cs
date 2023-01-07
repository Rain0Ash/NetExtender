// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using Microsoft.Extensions.Hosting;
using NetExtender.Domains.AspNetCore.Applications;
using NetExtender.Domains.AspNetCore.View;
using NetExtender.Domains.Builder.Interfaces;
using NetExtender.Domains.Initializer;

namespace NetExtender.Domains.AspNetCore.Initializer
{
    public abstract class AspNetCoreApplicationInitializer<T> : ApplicationInitializer<AspNetCoreApplication, AspNetCoreView<T>> where T : class, IHost, new()
    {
    }
    
    public abstract class AspNetCoreApplicationInitializer<T, TBuilder> : ApplicationInitializer<AspNetCoreApplication, AspNetCoreView<T, TBuilder>> where T : class, IHost where TBuilder : IApplicationBuilder<T>, new()
    {
    }
}