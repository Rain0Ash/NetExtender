// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System.Windows;
using NetExtender.Domains.Applications;
using NetExtender.Domains.Initializer;
using NetExtender.Domains.View;

namespace NetExtender.Domain.WindowsPresentation.Initializer
{
    public abstract class WindowsPresentationApplicationInitializer<T> : WindowsPresentationApplicationInitializer<System.Windows.Application, T> where T : Window, new()
    {
    }

    public abstract class WindowsPresentationApplicationInitializer<TApplication, TWindow> : ApplicationInitializer<WindowsPresentationApplication<TApplication>, WindowsPresentationView<TWindow>> where TApplication : System.Windows.Application, new() where TWindow : Window, new()
    {
    }
}