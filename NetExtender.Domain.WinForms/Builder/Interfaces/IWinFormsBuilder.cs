using System;
using System.Windows.Forms;
using NetExtender.Domains.Builder.Interfaces;

namespace NetExtender.Domains.WinForms.Builder.Interfaces
{
    public interface IWinFormsBuilder<out T> : IWinFormsBuilder, IApplicationBuilder<T> where T : Form
    {
    }
    
    public interface IWinFormsBuilder : IApplicationBuilder
    {
        public Boolean IsConsoleVisible { get; }
    }
}