// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System.Windows;
using NetExtender.ReactiveUI;
using NetExtender.Types.Singletons;
using NetExtender.Types.Singletons.Interfaces;
using NetExtender.WindowsPresentation.Utilities;

namespace NetExtender.WindowsPresentation.ReactiveUI
{
    public abstract class ReactiveViewModel<T, TWindow> : ReactiveDisposeViewModel<T> where T : ReactiveDisposeViewModel<T> where TWindow : Window
    {
        public TWindow Window { get; } = WindowStorageUtilities<TWindow>.Require();
    }
    
    public abstract class ReactiveDisposeViewModel : ReactiveDisposeObject
    {
    }
    
    public abstract class ReactiveDisposeViewModel<T> : ReactiveDisposeObject<T> where T : ReactiveDisposeViewModel<T>
    {
    }

    public abstract class ReactiveDisposeViewModelSingleton<T> : ReactiveDisposeViewModel<T> where T : ReactiveDisposeViewModel<T>, new()
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