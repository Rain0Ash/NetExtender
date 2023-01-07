// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System.Windows;
using Microsoft.Extensions.Hosting;

namespace NetExtender.Domains.WindowsPresentation.AspNetCore.Context
{
    public class WindowsPresentationAspNetCoreContext<TWindow, THost> : WindowsPresentationAspNetCoreContext<TWindow> where TWindow : Window where THost : class, IHost
    {
        public sealed override THost? Host { get; }

        public WindowsPresentationAspNetCoreContext(TWindow? window, THost? host)
            : base(window)
        {
            Host = host;
        }
        
        public void Deconstruct(out TWindow? window, out THost? host)
        {
            window = Window;
            host = Host;
        }
    }
    
    public abstract class WindowsPresentationAspNetCoreContext<TWindow> : WindowsPresentationAspNetCoreContext where TWindow : Window
    {
        public sealed override TWindow? Window { get; }

        protected WindowsPresentationAspNetCoreContext(TWindow? window)
        {
            Window = window;
        }
        
        public void Deconstruct(out TWindow? window, out IHost? host)
        {
            window = Window;
            host = Host;
        }
    }
    
    public abstract class WindowsPresentationAspNetCoreContext
    {
        public abstract Window? Window { get; }
        public abstract IHost? Host { get; }

        public void Deconstruct(out Window? window, out IHost? host)
        {
            window = Window;
            host = Host;
        }
    }
}