// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System.Windows.Forms;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;

namespace NetExtender.Domains.WinForms.AspNetCore.Context
{
    public abstract class WinFormsAspNetCoreContextAbstraction<T> where T : class
    {
        public abstract Form? Form { get; }
        public abstract T? Host { get; }

        public void Deconstruct(out Form? form, out T? host)
        {
            form = Form;
            host = Host;
        }
    }
    
    public class WinFormsAspNetCoreContext<TForm, THost> : WinFormsAspNetCoreContext<TForm> where TForm : Form where THost : class, IHost
    {
        public sealed override THost? Host { get; }

        public WinFormsAspNetCoreContext(TForm? form, THost? host)
            : base(form)
        {
            Host = host;
        }
        
        public void Deconstruct(out TForm? form, out THost? host)
        {
            form = Form;
            host = Host;
        }
    }
    
    public abstract class WinFormsAspNetCoreContext<TForm> : WinFormsAspNetCoreContext where TForm : Form
    {
        public sealed override TForm? Form { get; }

        protected WinFormsAspNetCoreContext(TForm? form)
        {
            Form = form;
        }
        
        public void Deconstruct(out TForm? form, out IHost? host)
        {
            form = Form;
            host = Host;
        }
    }
    
    public abstract class WinFormsAspNetCoreContext : WinFormsAspNetCoreContextAbstraction<IHost>
    {
    }
    
    public class WinFormsAspNetCoreWebContext<TForm, THost> : WinFormsAspNetCoreWebContext<TForm> where TForm : Form where THost : class, IWebHost
    {
        public sealed override THost? Host { get; }

        public WinFormsAspNetCoreWebContext(TForm? form, THost? host)
            : base(form)
        {
            Host = host;
        }
        
        public void Deconstruct(out TForm? form, out THost? host)
        {
            form = Form;
            host = Host;
        }
    }
    
    public abstract class WinFormsAspNetCoreWebContext<TForm> : WinFormsAspNetCoreWebContext where TForm : Form
    {
        public sealed override TForm? Form { get; }

        protected WinFormsAspNetCoreWebContext(TForm? form)
        {
            Form = form;
        }
        
        public void Deconstruct(out TForm? form, out IWebHost? host)
        {
            form = Form;
            host = Host;
        }
    }
    
    public abstract class WinFormsAspNetCoreWebContext : WinFormsAspNetCoreContextAbstraction<IWebHost>
    {
    }
    
    public class WinFormsAspNetCoreWebApplicationContext<TForm> : WinFormsAspNetCoreContext<TForm, WebApplication> where TForm : Form
    {
        public WinFormsAspNetCoreWebApplicationContext(TForm? window, WebApplication? host)
            : base(window, host)
        {
        }
    }
}