// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System.Windows;
using NetExtender.Types.Singletons;
using NetExtender.Types.Singletons.Interfaces;
using NetExtender.WindowsPresentation.Types;
using NetExtender.WindowsPresentation.Utilities;
using ReactiveUI;

namespace NetExtender.WindowsPresentation.ReactiveUI
{
    public abstract class ReactiveViewModel<TWindow> : ReactiveViewModel where TWindow : Window
    {
        public TWindow Window { get; } = WindowStorageUtilities<TWindow>.Require();
    }
    
    public abstract class ReactiveViewModel : ReactiveObject
    {
        protected static WindowsPresentationServiceProvider Provider
        {
            get
            {
                return WindowsPresentationServiceProvider.Instance;
            }
        }
    }

    public abstract class ReactiveViewModelSingleton<T> : ReactiveViewModel where T : ReactiveViewModel, new()
    {
        private static ISingleton<T> Internal { get; } = new Singleton<T>();

        public static T Instance
        {
            get
            {
                return Internal.Instance;
            }
        }
    }
}