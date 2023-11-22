// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Collections.Immutable;
using System.Windows.Forms;
using NetExtender.Domains.Builder;

namespace NetExtender.Domains.WinForms.Builder
{
    public abstract class WinFormsBuilder : ApplicationBuilder<Form>
    {
    }

    public class WinFormsBuilder<T> : ApplicationBuilder<T> where T : Form
    {
        public override T Build(ImmutableArray<String> arguments)
        {
            return New(arguments);
        }
    }
}