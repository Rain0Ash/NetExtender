// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System.Windows;
using NetExtender.Localization.Behavior.Settings;
using NetExtender.Types.Singletons;
using NetExtender.Types.Singletons.Interfaces;
using NetExtender.WindowsPresentation.Utilities;

namespace NetExtender.WindowsPresentation.ReactiveUI
{
    public abstract class SettingsReactiveViewModel<TWindow> : SettingsReactiveViewModel where TWindow : Window
    {
        public TWindow Window { get; }

        protected SettingsReactiveViewModel(SettingsLocalizationSynchronizedBehavior settings)
            : base(settings)
        {
            Window = WindowStorageUtilities<TWindow>.Require();
        }
    }

    public abstract class SettingsReactiveViewModel : SettingsReactiveViewModelAbstraction<SettingsReactiveViewModelInitializer>
    {
        protected SettingsReactiveViewModel(SettingsLocalizationSynchronizedBehavior settings)
            : base(settings)
        {
        }
    }

    public abstract class SettingsReactiveViewModelSingleton<T> : SettingsReactiveViewModel where T : SettingsReactiveViewModel, new()
    {
        private static ISingleton<T> Internal { get; } = new Singleton<T>();

        public static T Instance
        {
            get
            {
                return Internal.Instance;
            }
        }

        protected SettingsReactiveViewModelSingleton(SettingsLocalizationSynchronizedBehavior settings)
            : base(settings)
        {
        }
    }

    public abstract class SettingsReactiveViewModelSingleton<T, TWindow> : SettingsReactiveViewModel<TWindow> where T : SettingsReactiveViewModel<TWindow>, new() where TWindow : Window
    {
        private static ISingleton<T> Internal { get; } = new Singleton<T>();

        public static T Instance
        {
            get
            {
                return Internal.Instance;
            }
        }

        protected SettingsReactiveViewModelSingleton(SettingsLocalizationSynchronizedBehavior settings)
            : base(settings)
        {
        }
    }
}