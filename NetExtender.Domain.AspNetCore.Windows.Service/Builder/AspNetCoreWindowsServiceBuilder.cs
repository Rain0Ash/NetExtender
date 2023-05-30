// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

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
}