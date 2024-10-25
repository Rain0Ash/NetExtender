// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Collections.Immutable;
using System.Windows;
using NetExtender.Domains.Builder;
using NetExtender.Domains.WindowsPresentation.Builder.Interfaces;

namespace NetExtender.Domains.WindowsPresentation.Builder
{
    public class WindowsPresentationBuilder : WindowsPresentationBuilder<Window>
    {
    }

    public class WindowsPresentationBuilder<T> : ApplicationBuilder<T>, IWindowsPresentationBuilder<T> where T : Window
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
    
    public class WindowsPresentationConsoleBuilder : WindowsPresentationConsoleBuilder<Window>
    {
    }
    
    public class WindowsPresentationConsoleBuilder<T> : WindowsPresentationBuilder<T> where T : Window
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