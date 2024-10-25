// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Diagnostics.CodeAnalysis;
using System.Windows;
using NetExtender.Types.Exceptions;
using NetExtender.Types.Times;
using NetExtender.UserInterface.Utilities;
using NetExtender.Utilities.Types;
using NetExtender.WindowsPresentation.Types;
using NetExtender.WindowsPresentation.Types.Interfaces;
using NetExtender.WindowsPresentation.Utilities.Types;

namespace NetExtender.UserInterface.WindowsPresentation
{
    [SuppressMessage("ReSharper", "StaticMemberInGenericType")]
    public class SingletonWindow<T> : SingletonWindow where T : Window
    {
        private static SingletonWindow<T>? _singleton;
        public static SingletonWindow<T> Singleton
        {
            get
            {
                return _singleton ??= new SingletonWindow<T>();
            }
        }

        protected static Object SyncRoot { get; } = ConcurrentUtilities.SyncRoot;

        private T? _window;
        public T Window
        {
            get
            {
                lock (SyncRoot)
                {
                    if (_window is not null)
                    {
                        return _window;
                    }

                    T window = Factory.Invoke() ?? throw new ArgumentNullException(nameof(Factory));
                    window.Closed += InstanceClosed;
                    window.Deactivated += InstanceLostFocus;
                    return _window = window;
                }
            }
        }

        private Func<T> Factory { get; }
        private TimePointWatcher Watcher { get; } = new TimePointWatcher();
        public Boolean IsExitOnFocusLost { get; set; }
        public TimeSpan TimeDelay { get; set; } = Time.Second.Quarter;

        public SingletonWindow()
            : this(Create)
        {
        }

        public SingletonWindow(Func<T> factory)
        {
            Factory = factory ?? throw new ArgumentNullException(nameof(factory));
        }

        public SingletonWindow(IWindowServiceProvider provider)
        {
            if (provider is null)
            {
                throw new ArgumentNullException(nameof(provider));
            }
            
            Factory = provider.Require<T>;
        }

        private static T Create()
        {
            return Activator.CreateInstance(typeof(T), true) as T ?? throw new InvalidOperationException();
        }
        
        public static SingletonWindow<T> Initialize(Boolean exit)
        {
            return Initialize((Func<T>?) null, exit);
        }
        
        public static Boolean Initialize(Boolean exit, out SingletonWindow<T> result)
        {
            return Initialize((Func<T>?) null, exit, out result);
        }
        
        public static SingletonWindow<T> Initialize(Func<T>? factory)
        {
            return Initialize(factory, false);
        }
        
        public static SingletonWindow<T> Initialize(Func<T>? factory, Boolean exit)
        {
            lock (SyncRoot)
            {
                if (_singleton is not null)
                {
                    throw new AlreadyInitializedException("Singleton window is already initialized");
                }

                return _singleton = new SingletonWindow<T>(factory ?? Create) { IsExitOnFocusLost = exit };
            }
        }
        
        public static Boolean Initialize(Func<T>? factory, out SingletonWindow<T> result)
        {
            return Initialize(factory, false, out result);
        }
        
        public static Boolean Initialize(Func<T>? factory, Boolean exit, out SingletonWindow<T> result)
        {
            lock (SyncRoot)
            {
                if (_singleton is not null)
                {
                    result = _singleton;
                    return false;
                }

                result = _singleton = new SingletonWindow<T>(factory ?? Create) { IsExitOnFocusLost = exit };
                return true;
            }
        }

        public static SingletonWindow<T> Initialize(IWindowServiceProvider provider)
        {
            return Initialize(provider, false);
        }

        public static SingletonWindow<T> Initialize(IWindowServiceProvider provider, Boolean exit)
        {
            if (provider is null)
            {
                throw new ArgumentNullException(nameof(provider));
            }
            
            lock (SyncRoot)
            {
                if (_singleton is not null)
                {
                    throw new AlreadyInitializedException("Singleton window is already initialized");
                }

                return _singleton = new SingletonWindow<T>(provider) { IsExitOnFocusLost = exit };
            }
        }
        
        public static Boolean Initialize(IWindowServiceProvider provider, out SingletonWindow<T> result)
        {
            return Initialize(provider, false, out result);
        }
        
        public static Boolean Initialize(IWindowServiceProvider provider, Boolean exit, out SingletonWindow<T> result)
        {
            if (provider is null)
            {
                throw new ArgumentNullException(nameof(provider));
            }
            
            lock (SyncRoot)
            {
                if (_singleton is not null)
                {
                    result = _singleton;
                    return false;
                }
                
                result = _singleton = new SingletonWindow<T>(provider) { IsExitOnFocusLost = exit };
                return true;
            }
        }
        
        public Boolean? Activate()
        {
            lock (SyncRoot)
            {
                return _window?.Activate() ?? Show();
            }
        }

        public Boolean? Show()
        {
            lock (SyncRoot)
            {
                if (_window is null)
                {
                    Window.Show();
                    return true;
                }

                if (Watcher.UpdateNow(TimeDelay, TimeWatcherComparison.GreaterOrEqualAbsolute) && (Window.WindowState == WindowState.Minimized || !Window.IsActive))
                {
                    Window.WindowState = WindowState.Normal;
                    Window.Activate();
                    return false;
                }

                Window.Close();
                return null;
            }
        }

        public Boolean? ShowDialog()
        {
            lock (SyncRoot)
            {
                if (_window is null)
                {
                    return Window.ShowDialog();
                }

                if (Watcher.UpdateNow(TimeDelay, TimeWatcherComparison.GreaterOrEqualAbsolute) && (Window.WindowState == WindowState.Minimized || !Window.IsActive))
                {
                    Window.WindowState = WindowState.Normal;
                    Window.Activate();
                    return false;
                }

                Window.Close();
                return null;
            }
        }
        
        public InterfaceDialogResult ShowDialogResult()
        {
            lock (SyncRoot)
            {
                if (_window is null)
                {
                    T window = Window;
                    
                    if (window is FixedWindow @fixed)
                    {
                        return @fixed.ShowDialog();
                    }
                    
                    return InterfaceDialogResultUtilities.ToInterfaceDialogResult(window.ShowDialog());
                }
                
                if (Watcher.UpdateNow(TimeDelay, TimeWatcherComparison.GreaterOrEqualAbsolute) && (Window.WindowState == WindowState.Minimized || !Window.IsActive))
                {
                    Window.WindowState = WindowState.Normal;
                    Window.Activate();
                    return InterfaceDialogResult.Cancel;
                }
                
                Window.Close();
                return InterfaceDialogResult.None;
            }
        }
        
        public Boolean? Hide()
        {
            lock (SyncRoot)
            {
                if (_window is null)
                {
                    return false;
                }

                try
                {
                    _window.Hide();
                    return true;
                }
                catch (Exception)
                {
                    return null;
                }
            }
        }

        public Boolean Close()
        {
            lock (SyncRoot)
            {
                if (_window is null)
                {
                    return false;
                }
                
                _window.Close();
                return true;
            }
        }

        private void InstanceClosed(Object? sender, EventArgs args)
        {
            lock (SyncRoot)
            {
                _window = null;
            }
        }

        private void InstanceLostFocus(Object? sender, EventArgs args)
        {
            lock (SyncRoot)
            {
                Watcher.SetNow();

                if (!IsExitOnFocusLost)
                {
                    return;
                }

                try
                {
                    if (sender is Window window)
                    {
                        window.Close();
                    }
                }
                catch (Exception)
                {
                }
                finally
                {
                    _window = null;
                }
            }
        }
    }

    public abstract class SingletonWindow
    {
        public static Boolean Activate<TWindow>() where TWindow : Window
        {
            return SingletonWindow<TWindow>.Singleton.Activate() is not null;
        }
        
        public static Boolean Activate<TWindow>(IWindowServiceProvider provider) where TWindow : Window
        {
            if (provider is null)
            {
                throw new ArgumentNullException(nameof(provider));
            }
            
            SingletonWindow<TWindow>.Initialize(provider, out SingletonWindow<TWindow> window);
            return window.Activate() is not null;
        }
        
        public static Boolean Show<TWindow>() where TWindow : Window
        {
            return SingletonWindow<TWindow>.Singleton.Show() is not null;
        }
        
        public static Boolean Show<TWindow>(IWindowServiceProvider provider) where TWindow : Window
        {
            if (provider is null)
            {
                throw new ArgumentNullException(nameof(provider));
            }
            
            SingletonWindow<TWindow>.Initialize(provider, out SingletonWindow<TWindow> window);
            return window.Show() is not null;
        }
        
        public static Boolean ShowDialog<TWindow>() where TWindow : Window
        {
            return SingletonWindow<TWindow>.Singleton.ShowDialog() is not null;
        }
        
        public static Boolean ShowDialog<TWindow>(IWindowServiceProvider provider) where TWindow : Window
        {
            if (provider is null)
            {
                throw new ArgumentNullException(nameof(provider));
            }
            
            SingletonWindow<TWindow>.Initialize(provider, out SingletonWindow<TWindow> window);
            return window.ShowDialog() is not null;
        }
        
        public static InterfaceDialogResult ShowDialogResult<TWindow>() where TWindow : Window
        {
            return SingletonWindow<TWindow>.Singleton.ShowDialogResult();
        }
        
        public static InterfaceDialogResult ShowDialogResult<TWindow>(IWindowServiceProvider provider) where TWindow : Window
        {
            if (provider is null)
            {
                throw new ArgumentNullException(nameof(provider));
            }
            
            SingletonWindow<TWindow>.Initialize(provider, out SingletonWindow<TWindow> window);
            return window.ShowDialogResult();
        }
        
        public static Boolean Hide<TWindow>() where TWindow : Window
        {
            return SingletonWindow<TWindow>.Singleton.Hide() is not false;
        }
        
        public static Boolean Hide<TWindow>(IWindowServiceProvider provider) where TWindow : Window
        {
            if (provider is null)
            {
                throw new ArgumentNullException(nameof(provider));
            }
            
            SingletonWindow<TWindow>.Initialize(provider, out SingletonWindow<TWindow> window);
            return window.Hide() is not false;
        }
        
        public static Boolean Close<TWindow>() where TWindow : Window
        {
            return SingletonWindow<TWindow>.Singleton.Close();
        }
        
        public static Boolean Close<TWindow>(IWindowServiceProvider provider) where TWindow : Window
        {
            if (provider is null)
            {
                throw new ArgumentNullException(nameof(provider));
            }
            
            SingletonWindow<TWindow>.Initialize(provider, out SingletonWindow<TWindow> window);
            return window.Close();
        }
    }
}