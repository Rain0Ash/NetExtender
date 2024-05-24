// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.ComponentModel;
using System.Windows;
using System.Windows.Input;
using NetExtender.UserInterface.Events;
using NetExtender.UserInterface.Interfaces;
using NetExtender.UserInterface.Utilities;
using NetExtender.UserInterface.Windows.Types;
using NetExtender.Utilities.UserInterface;
using NetExtender.Utilities.Windows.IO;
using NetExtender.Windows;

namespace NetExtender.UserInterface.WindowsPresentation.Windows
{
    public abstract class FixedWindow : HotKeyWindow, IWindow
    {
        public static readonly DependencyProperty IsAltF4EnabledProperty = DependencyProperty.Register(nameof(IsAltF4Enabled), typeof(Boolean), typeof(Window), new PropertyMetadata(true));
        public static readonly DependencyProperty IsExitOnFocusLostProperty = DependencyProperty.Register(nameof(IsExitOnFocusLost), typeof(Boolean), typeof(Window), new PropertyMetadata(false));
        public static readonly DependencyProperty IsSystemMenuEnabledProperty = DependencyProperty.Register(nameof(IsSystemMenuEnabled), typeof(Boolean), typeof(Window), new PropertyMetadata(true, IsSystemMenuEnabledChanged));
        public static readonly DependencyProperty DisplayAffinityProperty = DependencyProperty.Register(nameof(DisplayAffinity), typeof(WindowDisplayAffinity), typeof(Window), new PropertyMetadata(WindowDisplayAffinity.None, DisplayAffinityChanged));

        public new Window? Owner
        {
            get
            {
                return base.Owner ?? Application.Current.MainWindow;
            }
        }
        
        public Boolean IsOwner
        {
            get
            {
                Window? parent = Owner;
                return parent is null || parent == this;
            }
        }

        Boolean IWindow.TopMost
        {
            get
            {
                return Topmost;
            }
            set
            {
                Topmost = value;
            }
        }

        private InterfaceCloseReason _reason;
        public InterfaceCloseReason CloseReason
        {
            get
            {
                return _reason;
            }
            set
            {
                if (value == InterfaceCloseReason.None)
                {
                    _reason = value;
                    return;
                }

                _reason = value;
                OnClosing(this, new CancelEventArgs());
            }
        }

        public Boolean IsAltF4Enabled
        {
            get
            {
                return (Boolean) GetValue(IsAltF4EnabledProperty);
            }
            set
            {
                SetValue(IsAltF4EnabledProperty, value);
            }
        }

        public Boolean IsExitOnFocusLost
        {
            get
            {
                return (Boolean) GetValue(IsExitOnFocusLostProperty);
            }
            set
            {
                SetValue(IsExitOnFocusLostProperty, value);
            }
        }

        public Boolean? IsSystemMenuEnabled
        {
            get
            {
                return this.GetWindowSystemMenu();
            }
            set
            {
                if (value is { } state)
                {
                    SetValue(IsSystemMenuEnabledProperty, state);
                }
            }
        }

        public WindowDisplayAffinity? DisplayAffinity
        {
            get
            {
                return this.GetWindowDisplayAffinity(out WindowDisplayAffinity affinity) ? affinity : null;
            }
            set
            {
                if (value is { } affinity)
                {
                    this.SetWindowDisplayAffinity(affinity);
                }
            }
        }

        public event InterfaceClosingEventHandler? WindowClosing;
        public event SizeChangeToggleHandler? SizeChangeToggle;

        protected FixedWindow()
        {
            BeginConstructorInit();
            WindowStartupLocation = WindowStartupLocation.CenterOwner;
            WindowStyle = WindowStyle.SingleBorderWindow;
            Loaded += SystemMenuInitialize;
            Loaded += DisplayAffinityInitialize;
            Deactivated += OnDeactivated;
            Closing += OnClosing;
            WindowClosing += DisableIconClickExit;
            EndConstructorInit();
        }

        protected void Set(InterfaceCloseReason value)
        {
            _reason = value;
        }

        private void SystemMenuInitialize(Object? sender, RoutedEventArgs args)
        {
            if (sender is not DependencyObject dependency)
            {
                return;
            }

            Object? current = GetValue(IsSystemMenuEnabledProperty);
            DependencyPropertyChangedEventArgs @event = new DependencyPropertyChangedEventArgs(IsSystemMenuEnabledProperty, IsSystemMenuEnabled, current);
            IsSystemMenuEnabledChanged(dependency, @event);
        }

        private void DisplayAffinityInitialize(Object? sender, RoutedEventArgs args)
        {
            if (sender is not DependencyObject dependency)
            {
                return;
            }

            Object? current = GetValue(DisplayAffinityProperty);
            DependencyPropertyChangedEventArgs @event = new DependencyPropertyChangedEventArgs(DisplayAffinityProperty, DisplayAffinity, current);
            DisplayAffinityChanged(dependency, @event);
        }

        protected override Boolean WndProc(ref WinMessage message)
        {
            switch (message.Message)
            {
                case WM.QUERYENDSESSION:
                case WM.ENDSESSION:
                    Set(InterfaceCloseReason.SystemShutdown);
                    break;
                case WM.SYSCOMMAND:
                    if (((UInt16) message.WParam & 0xFFF0) == 0xF060)
                    {
                        Set(InterfaceCloseReason.UserClosing);
                    }

                    break;
                case WM.ENTERSIZEMOVE:
                case WM.EXITSIZEMOVE:
                {
                    SizeChangeToggleEventArgs args = new SizeChangeToggleEventArgs { End = message.Message == WM.EXITSIZEMOVE };
                    OnSizeChangeToggle(args);

                    if (args.Handled)
                    {
                        return true;
                    }

                    break;
                }
                default:
                    break;
            }

            return base.WndProc(ref message);
        }

        protected virtual void OnSizeChangeToggle(SizeChangeToggleEventArgs args)
        {
            SizeChangeToggle?.Invoke(this, args);
        }

        private void OnDeactivated(Object? sender, EventArgs args)
        {
            if (!IsExitOnFocusLost)
            {
                return;
            }

            CloseReason = InterfaceCloseReason.WindowClosing;
        }

        private void OnClosing(Object? sender, CancelEventArgs args)
        {
            if (args.Cancel)
            {
                return;
            }

            if (KeyboardUtilities.Alt.HasAlt && Keyboard.IsKeyDown(Key.F4))
            {
                if (IsAltF4Enabled)
                {
                    return;
                }

                args.Cancel = true;
                return;
            }

            InterfaceClosingEventArgs closing = new InterfaceClosingEventArgs(CloseReason, args.Cancel);
            WindowClosing?.Invoke(sender, closing);

            if (closing.Cancel)
            {
                args.Cancel = true;
            }
        }

        private void DisableIconClickExit(Object? sender, InterfaceClosingEventArgs args)
        {
            if (args.Reason != InterfaceCloseReason.UserClosing)
            {
                return;
            }

            if (KeyboardUtilities.Alt.HasAlt && Keyboard.IsKeyDown(Key.F4))
            {
                return;
            }

            IInputElement previous = Mouse.Captured;
            Mouse.Capture(this);
            Point click = Mouse.GetPosition(this);
            Mouse.Capture(previous);

            const Int32 iconWidth = 32;

            if (!(click.X <= iconWidth) || !(click.Y <= 0))
            {
                return;
            }

            args.Cancel = true;
        }
        
        private static void IsSystemMenuEnabledChanged(DependencyObject @object, DependencyPropertyChangedEventArgs args)
        {
            if (@object is not Window window)
            {
                return;
            }

            if (Equals(args.OldValue, args.NewValue) || args.NewValue is not Boolean menu)
            {
                return;
            }

            window.SetWindowSystemMenu(menu);
        }
        
        private static void DisplayAffinityChanged(DependencyObject @object, DependencyPropertyChangedEventArgs args)
        {
            if (@object is not Window window)
            {
                return;
            }

            if (Equals(args.OldValue, args.NewValue) || args.NewValue is not WindowDisplayAffinity affinity)
            {
                return;
            }

            window.SetWindowDisplayAffinity(affinity);
        }

        public new InterfaceDialogResult ShowDialog()
        {
            return InterfaceDialogResultUtilities.ToInterfaceDialogResult(base.ShowDialog());
        }
    }
}