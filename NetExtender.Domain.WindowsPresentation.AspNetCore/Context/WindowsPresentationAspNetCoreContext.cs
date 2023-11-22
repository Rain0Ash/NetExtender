// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System.Windows;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;

namespace NetExtender.Domains.WindowsPresentation.AspNetCore.Context
{
    public abstract class WindowsPresentationAspNetCoreContextAbstraction<T> where T : class
    {
        public abstract Window? Window { get; }
        public abstract T? Host { get; }

        public void Deconstruct(out Window? window, out T? host)
        {
            window = Window;
            host = Host;
        }
    }
    
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
    
    public abstract class WindowsPresentationAspNetCoreContext : WindowsPresentationAspNetCoreContextAbstraction<IHost>
    {
    }
    
    public class WindowsPresentationAspNetCoreWebContext<TWindow, THost> : WindowsPresentationAspNetCoreWebContext<TWindow> where TWindow : Window where THost : class, IWebHost
    {
        public sealed override THost? Host { get; }

        public WindowsPresentationAspNetCoreWebContext(TWindow? window, THost? host)
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
    
    public abstract class WindowsPresentationAspNetCoreWebContext<TWindow> : WindowsPresentationAspNetCoreWebContext where TWindow : Window
    {
        public sealed override TWindow? Window { get; }

        protected WindowsPresentationAspNetCoreWebContext(TWindow? window)
        {
            Window = window;
        }
        
        public void Deconstruct(out TWindow? window, out IWebHost? host)
        {
            window = Window;
            host = Host;
        }
    }
    
    public abstract class WindowsPresentationAspNetCoreWebContext : WindowsPresentationAspNetCoreContextAbstraction<IWebHost>
    {
    }
    
    public class WindowsPresentationAspNetCoreWebApplicationContext<TWindow> : WindowsPresentationAspNetCoreContext<TWindow, WebApplication> where TWindow : Window
    {
        public WindowsPresentationAspNetCoreWebApplicationContext(TWindow? window, WebApplication? host)
            : base(window, host)
        {
        }
    }
}