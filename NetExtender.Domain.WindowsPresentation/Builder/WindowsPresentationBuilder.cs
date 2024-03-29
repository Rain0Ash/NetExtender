// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Collections.Immutable;
using System.Windows;
using NetExtender.Domains.Builder;

namespace NetExtender.Domains.WindowsPresentation.Builder
{
    public abstract class WindowsPresentationBuilder : ApplicationBuilder<Window>
    {
    }

    public class WindowsPresentationBuilder<T> : ApplicationBuilder<T> where T : Window
    {
        public override T Build(ImmutableArray<String> arguments)
        {
            return New(arguments);
        }
    }
}