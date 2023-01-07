// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Windows.Forms;
using NetExtender.Domains.Builder;

namespace NetExtender.Domains.WinForms.Builder
{
    public abstract class WinFormsBuilder : ApplicationBuilder<Form>
    {
    }

    public class WinFormsBuilder<T> : ApplicationBuilder<T> where T : Form, new()
    {
        public override T Build(String[] arguments)
        {
            if (arguments is null)
            {
                throw new ArgumentNullException(nameof(arguments));
            }

            return new T();
        }
    }
}