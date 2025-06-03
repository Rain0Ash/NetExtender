// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System.Windows.Forms;
using NetExtender.Domains.Builder.Interfaces;

namespace NetExtender.Domains.WinForms.Builder.Interfaces
{
    public interface IWinFormsBuilder<out T> : IWinFormsBuilder, IApplicationBuilder<T> where T : Form
    {
    }
    
    public interface IWinFormsBuilder : IApplicationBuilder
    {
    }
}