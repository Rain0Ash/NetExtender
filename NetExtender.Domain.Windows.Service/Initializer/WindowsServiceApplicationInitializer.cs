// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using NetExtender.Domains.Initializer;
using NetExtender.Domains.Service.Applications;
using NetExtender.Domains.Service.Views;
using NetExtender.Windows.Services.Types.Services.Interfaces;

namespace NetExtender.Domain.Windows.Service.Initializer
{
    public abstract class WindowsServiceApplicationInitializer<T> : ApplicationInitializer<WindowsServiceApplication, WindowsServiceView<T>> where T : class, IWindowsService, new()
    {
    }
}