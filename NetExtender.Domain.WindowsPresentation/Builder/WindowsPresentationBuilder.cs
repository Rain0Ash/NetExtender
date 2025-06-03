// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Collections.Immutable;
using System.Windows;
using NetExtender.Domains.Builder;
using NetExtender.Domains.WindowsPresentation.Builder.Interfaces;
using NetExtender.Types.Console.Interfaces;
using NetExtender.UserInterface.Console.Interfaces;

namespace NetExtender.Domains.WindowsPresentation.Builder
{
    public class WindowsPresentationBuilder : WindowsPresentationBuilder<Window>
    {
    }

    public class WindowsPresentationBuilder<T> : ApplicationBuilder<T>, IWindowsPresentationBuilder<T> where T : Window
    {
        public override IWindowConsole Console
        {
            get
            {
                return IWindowConsole.Default;
            }
        }

        protected override void Initialize(ImmutableArray<String> arguments)
        {
            Initialize(arguments, Console);
        }

        protected sealed override void Initialize(ImmutableArray<String> arguments, IConsole console)
        {
            Initialize(arguments, console as IWindowConsole ?? throw new InvalidOperationException());
        }

        protected virtual void Initialize(ImmutableArray<String> arguments, IWindowConsole console)
        {
            base.Initialize(arguments, console);
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
        protected override void Initialize(ImmutableArray<String> arguments, IWindowConsole console)
        {
            console.IsVisible = true;
            base.Initialize(arguments, console);
        }
    }
}