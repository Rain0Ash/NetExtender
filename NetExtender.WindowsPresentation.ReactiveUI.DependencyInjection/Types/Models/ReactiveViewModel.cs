// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System.Windows;
using NetExtender.WindowsPresentation.Utilities;

namespace NetExtender.WindowsPresentation.ReactiveUI
{
    public abstract class TransientReactiveViewModel<TWindow> : TransientReactiveViewModel where TWindow : Window
    {
        public TWindow Window { get; } = WindowStorageUtilities<TWindow>.Require();
    }
    
    public abstract class TransientReactiveViewModel : ReactiveViewModel, ITransientReactiveViewModel
    {
    }
    
    public abstract class ScopedReactiveViewModel<TWindow> : ScopedReactiveViewModel where TWindow : Window
    {
        public TWindow Window { get; } = WindowStorageUtilities<TWindow>.Require();
    }
    
    public abstract class ScopedReactiveViewModel : ReactiveViewModel, IScopedReactiveViewModel
    {
    }
    
    public abstract class SingletonReactiveViewModel<TWindow> : SingletonReactiveViewModel where TWindow : Window
    {
        public TWindow Window { get; } = WindowStorageUtilities<TWindow>.Require();
    }

    public abstract class SingletonReactiveViewModel : ReactiveViewModel, ISingletonReactiveViewModel
    {
    }
}