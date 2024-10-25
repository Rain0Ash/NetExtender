// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Collections.Immutable;
using System.Windows.Forms;
using NetExtender.Domains.Builder;
using NetExtender.Domains.WinForms.Builder.Interfaces;

namespace NetExtender.Domains.WinForms.Builder
{
    public class WinFormsBuilder : WinFormsBuilder<Form>
    {
    }

    public class WinFormsBuilder<T> : ApplicationBuilder<T>, IWinFormsBuilder<T> where T : Form
    {
        public virtual Boolean IsConsoleVisible
        {
            get
            {
                return false;
            }
        }
        
        public override T Build(ImmutableArray<String> arguments)
        {
            Manager?.Invoke(this, this);
            return New(arguments);
        }
    }

    public class WinFormsConsoleBuilder : WinFormsConsoleBuilder<Form>
    {
    }

    public class WinFormsConsoleBuilder<T> : WinFormsBuilder<T> where T : Form
    {
        public override Boolean IsConsoleVisible
        {
            get
            {
                return true;
            }
        }
    }
}