// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace NetExtender.AspNetCore.Types.Initializers.Interfaces
{
    public interface IStartupProvider
    {
        public void ConfigureServices(IServiceCollection services);
        public void Configure(IApplicationBuilder application, IServiceProvider provider);
    }
}