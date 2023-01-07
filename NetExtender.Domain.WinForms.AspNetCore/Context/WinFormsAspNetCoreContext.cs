// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System.Windows.Forms;
using Microsoft.Extensions.Hosting;

namespace NetExtender.Domains.WinForms.AspNetCore.Context
{
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
    
    public abstract class WinFormsAspNetCoreContext
    {
        public abstract Form? Form { get; }
        public abstract IHost? Host { get; }

        public void Deconstruct(out Form? form, out IHost? host)
        {
            form = Form;
            host = Host;
        }
    }
}