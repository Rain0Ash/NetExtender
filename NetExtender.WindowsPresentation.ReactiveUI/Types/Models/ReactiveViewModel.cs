// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Windows;
using NetExtender.WindowsPresentation.Utilities;
using ReactiveUI;

namespace NetExtender.WindowsPresentation.ReactiveUI.Types.Models
{
    public abstract class ReactiveViewModel<TWindow> : ReactiveViewModel where TWindow : Window
    {
        public TWindow Window { get; }

        protected ReactiveViewModel()
        {
            Window = WindowStoreUtilities<TWindow>.Require();
        }
    }
    
    public abstract class ReactiveViewModel : ReactiveObject
    {
    }
    
    public abstract class ReactiveViewModelSingleton<T> : ReactiveViewModel where T : ReactiveViewModel, new()
    {
        private static Lazy<T> Internal { get; } = new Lazy<T>(() => new T(), true);

        public static T Instance
        {
            get
            {
                return Internal.Value;
            }
        }
    }
}