// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using Microsoft.Extensions.Hosting;
using NetExtender.Domains.AspNetCore.Windows.Service.Applications;
using NetExtender.Domains.Initializer;
using NetExtender.Domains.Windows.Service.AspNetCore.Views;

namespace NetExtender.Domain.AspNetCore.Windows.Service.Initializer
{
    public abstract class AspNetCoreWindowsServiceApplicationInitializer<T> : ApplicationInitializer<AspNetCoreWindowsServiceApplication, AspNetCoreWindowsServiceView<T>> where T : class, IHost, new()
    {
    }
}