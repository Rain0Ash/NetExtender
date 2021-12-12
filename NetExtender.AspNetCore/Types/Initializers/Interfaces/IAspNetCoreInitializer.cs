// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace NetExtender.AspNetCore.Types.Initializers.Interfaces
{
    public interface IAspNetCoreInitializer : IApplicationBuilder, IWebHostEnvironment, IServiceCollection, IConfiguration
    {
    }
}