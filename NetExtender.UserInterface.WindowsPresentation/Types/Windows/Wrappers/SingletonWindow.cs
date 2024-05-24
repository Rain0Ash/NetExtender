// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Diagnostics.CodeAnalysis;
using System.Windows;
using NetExtender.Types.Exceptions;
using NetExtender.Types.Times;
using NetExtender.Utilities.Types;

namespace NetExtender.UserInterface.WindowsPresentation.Windows
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

        private static T Create()
        {
            return (T) Activator.CreateInstance(typeof(T), true)!;
        }

        public static SingletonWindow<T> Initialize(Boolean exit)
        {
            return Initialize(null, exit);
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
        
        public static Boolean Show<TWindow>() where TWindow : Window
        {
            return SingletonWindow<TWindow>.Singleton.Show() is not null;
        }
        
        public static Boolean ShowDialog<TWindow>() where TWindow : Window
        {
            return SingletonWindow<TWindow>.Singleton.ShowDialog() is not null;
        }
        
        public static Boolean Hide<TWindow>() where TWindow : Window
        {
            return SingletonWindow<TWindow>.Singleton.Hide() is not false;
        }
        
        public static Boolean Close<TWindow>() where TWindow : Window
        {
            return SingletonWindow<TWindow>.Singleton.Close();
        }
    }
}