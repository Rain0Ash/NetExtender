// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System.Windows.Forms;
using NetExtender.Domains.Applications;
using NetExtender.Domains.Initializer;
using NetExtender.Domains.View;

namespace NetExtender.Domain.WinForms.Initializer
{
    public abstract class WinFormsApplicationInitializer<T> : ApplicationInitializer<WinFormsApplication, WinFormsView<T>> where T : Form, new()
    {
    }
}