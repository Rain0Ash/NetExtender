// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using NetExtender.Domains.Builder.Interfaces;
using NetExtender.Domains.Service.Applications;
using NetExtender.Domains.Service.Builder;
using NetExtender.Domains.Service.Views;
using NetExtender.Types.Middlewares;
using NetExtender.Windows.Services.Types.Services.Interfaces;

namespace NetExtender.Domains.Initializer
{
    public abstract class WindowsServiceApplicationInitializer<T> : ApplicationInitializer<WindowsServiceApplication, WindowsServiceView<T>> where T : class, IWindowsService, new()
    {
        public abstract class Middleware<TBuilder> : NetExtender.Types.Middlewares.Middleware<TBuilder> where TBuilder : WindowsServiceBuilder<T>
        {
        }
    }
    
    public abstract class WindowsServiceApplicationInitializer<T, TBuilder> : ApplicationInitializer<WindowsServiceApplication, WindowsServiceView<T, TBuilder>> where T : class, IWindowsService where TBuilder : IApplicationBuilder<T>, new()
    {
        public abstract class Builder : WindowsServiceBuilder<T>
        {
        }

        public abstract class Middleware : Middleware<TBuilder>
        {
        }
    }
}