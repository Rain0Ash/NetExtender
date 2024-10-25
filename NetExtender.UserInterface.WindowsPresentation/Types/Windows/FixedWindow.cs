// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.ComponentModel;
using System.Windows;
using System.Windows.Input;
using NetExtender.UserInterface.Events;
using NetExtender.UserInterface.Interfaces;
using NetExtender.UserInterface.Utilities;
using NetExtender.UserInterface.WindowsPresentation.Types.Events;
using NetExtender.Utilities.UserInterface;
using NetExtender.Utilities.Windows.IO;
using NetExtender.Windows;
using NetExtender.Windows.Types;
using NetExtender.Windows.Utilities.Native;

namespace NetExtender.UserInterface.WindowsPresentation
{
    public abstract class FixedWindow : HotKeyWindow, IWindow
    {
        public static readonly DependencyProperty IsAltF4EnabledProperty = DependencyProperty.Register(nameof(IsAltF4Enabled), typeof(Boolean), typeof(Window), new PropertyMetadata(true));
        public static readonly DependencyProperty IsExitOnFocusLostProperty = DependencyProperty.Register(nameof(IsExitOnFocusLost), typeof(Boolean), typeof(Window), new PropertyMetadata(false));
        public static readonly DependencyProperty AllowSystemMenuProperty = DependencyProperty.Register(nameof(AllowSystemMenu), typeof(Boolean), typeof(Window), new PropertyMetadata(true, IsSystemMenuEnabledChanged));
        public static readonly DependencyProperty AllowSystemMenuRightClickProperty = DependencyProperty.Register(nameof(AllowSystemMenuRightClick), typeof(Boolean), typeof(WndProcWindow), new PropertyMetadata(true));
        public static readonly DependencyProperty AllowSystemMenuDoubleClickProperty = DependencyProperty.Register(nameof(AllowSystemMenuDoubleClick), typeof(Boolean), typeof(WndProcWindow), new PropertyMetadata(true));
        public static readonly DependencyProperty DisplayAffinityProperty = DependencyProperty.Register(nameof(DisplayAffinity), typeof(WindowDisplayAffinity), typeof(Window), new PropertyMetadata(WindowDisplayAffinity.None, DisplayAffinityChanged));
        public static readonly RoutedEvent SystemMenuMouseDownEvent = EventManager.RegisterRoutedEvent(nameof(SystemMenuMouseDown), RoutingStrategy.Bubble, typeof(MouseSystemMenuEventHandler), typeof(FixedWindow));
        public static readonly RoutedEvent SystemMenuMouseUpEvent = EventManager.RegisterRoutedEvent(nameof(SystemMenuMouseUp), RoutingStrategy.Bubble, typeof(MouseSystemMenuEventHandler), typeof(FixedWindow));
        public static readonly RoutedEvent SystemMenuMouseDoubleClickEvent = EventManager.RegisterRoutedEvent(nameof(SystemMenuMouseDoubleClick), RoutingStrategy.Bubble, typeof(MouseSystemMenuEventHandler), typeof(FixedWindow));
        
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
            [System.Diagnostics.DebuggerStepThrough]
            get
            {
                return (Boolean) GetValue(IsAltF4EnabledProperty);
            }
            [System.Diagnostics.DebuggerStepThrough]
            set
            {
                SetValue(IsAltF4EnabledProperty, value);
            }
        }

        public Boolean IsExitOnFocusLost
        {
            [System.Diagnostics.DebuggerStepThrough]
            get
            {
                return (Boolean) GetValue(IsExitOnFocusLostProperty);
            }
            [System.Diagnostics.DebuggerStepThrough]
            set
            {
                SetValue(IsExitOnFocusLostProperty, value);
            }
        }

        public Boolean? AllowSystemMenu
        {
            get
            {
                return this.GetWindowSystemMenu();
            }
            set
            {
                if (value is { } state)
                {
                    SetValue(AllowSystemMenuProperty, state);
                }
            }
        }
        
        public Boolean AllowSystemMenuRightClick
        {
            [System.Diagnostics.DebuggerStepThrough]
            get
            {
                return (Boolean) GetValue(AllowSystemMenuRightClickProperty);
            }
            [System.Diagnostics.DebuggerStepThrough]
            set
            {
                SetValue(AllowSystemMenuRightClickProperty, value);
            }
        }
        
        public Boolean AllowSystemMenuDoubleClick
        {
            [System.Diagnostics.DebuggerStepThrough]
            get
            {
                return (Boolean) GetValue(AllowSystemMenuDoubleClickProperty);
            }
            [System.Diagnostics.DebuggerStepThrough]
            set
            {
                SetValue(AllowSystemMenuDoubleClickProperty, value);
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
        
        public event MouseSystemMenuEventHandler SystemMenuMouseDown
        {
            [System.Diagnostics.DebuggerStepThrough]
            add
            {
                AddHandler(SystemMenuMouseDownEvent, value);
            }
            [System.Diagnostics.DebuggerStepThrough]
            remove
            {
                RemoveHandler(SystemMenuMouseDownEvent, value);
            }
        }
        
        public event MouseSystemMenuEventHandler SystemMenuMouseUp
        {
            [System.Diagnostics.DebuggerStepThrough]
            add
            {
                AddHandler(SystemMenuMouseUpEvent, value);
            }
            [System.Diagnostics.DebuggerStepThrough]
            remove
            {
                RemoveHandler(SystemMenuMouseUpEvent, value);
            }
        }
        
        public event MouseSystemMenuEventHandler SystemMenuMouseDoubleClick
        {
            [System.Diagnostics.DebuggerStepThrough]
            add
            {
                AddHandler(SystemMenuMouseDoubleClickEvent, value);
            }
            [System.Diagnostics.DebuggerStepThrough]
            remove
            {
                RemoveHandler(SystemMenuMouseDoubleClickEvent, value);
            }
        }

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
        
        protected Boolean IsSystemMenu()
        {
            return IsSystemMenu(out _);
        }
        
        protected Boolean IsSystemMenu(out Point click)
        {
            IInputElement previous = Mouse.Captured;
            Mouse.Capture(this);
            click = Mouse.GetPosition(this);
            Mouse.Capture(previous);
            return IsSystemMenu(click);
        }
        
        protected Boolean IsSystemMenu(Point click)
        {
            const Int32 icon = 32;
            return click is { X: <= icon, Y: <= 0 };
        }

        private void SystemMenuInitialize(Object? sender, RoutedEventArgs args)
        {
            if (sender is not DependencyObject dependency)
            {
                return;
            }

            Object? current = GetValue(AllowSystemMenuProperty);
            DependencyPropertyChangedEventArgs @event = new DependencyPropertyChangedEventArgs(AllowSystemMenuProperty, AllowSystemMenu, current);
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
        
        private void Create(in WindowsMessage message, out MouseSystemMenuEventArgs args)
        {
            args = new MouseSystemMenuEventArgs(Mouse.PrimaryDevice, Environment.TickCount, message.Message.ToMouse(message))
            {
                IsTitleBar = true,
                IsSystemMenu = IsSystemMenu(out Point position),
                Position = position
            };
        }
        
        // ReSharper disable once CognitiveComplexity
        protected override Boolean WndProc(ref WindowsMessage message)
        {
            switch (message.Message)
            {
                case WM.NCLBUTTONDOWN:
                case WM.NCRBUTTONDOWN:
                case WM.NCMBUTTONDOWN:
                case WM.NCXBUTTONDOWN:
                {
                    Create(message, out MouseSystemMenuEventArgs args);
                    OnSystemMenuMouseDown(args);

                    if (args.Handled)
                    {
                        return true;
                    }
                    
                    break;
                }
                case WM.NCLBUTTONUP:
                case WM.NCRBUTTONUP:
                case WM.NCMBUTTONUP:
                case WM.NCXBUTTONUP:
                {
                    Create(message, out MouseSystemMenuEventArgs args);
                    OnSystemMenuMouseUp(args);
                    
                    if (args.Handled)
                    {
                        return true;
                    }
                    
                    break;
                }
                case WM.NCLBUTTONDBLCLK:
                case WM.NCRBUTTONDBLCLK:
                case WM.NCMBUTTONDBLCLK:
                case WM.NCXBUTTONDBLCLK:
                {
                    Create(message, out MouseSystemMenuEventArgs args);
                    OnSystemMenuMouseDoubleClick(args);
                    
                    if (args.Handled)
                    {
                        return true;
                    }
                    
                    break;
                }
                default:
                    break;
            }
            
            switch (message.Message)
            {
                case WM.QUERYENDSESSION:
                case WM.ENDSESSION:
                    Set(InterfaceCloseReason.SystemShutdown);
                    break;
                case WM.NCLBUTTONDBLCLK:
                case WM.NCRBUTTONDBLCLK:
                case WM.NCMBUTTONDBLCLK:
                case WM.NCXBUTTONDBLCLK:
                    if (!AllowSystemMenuDoubleClick && (IntPtr) (message.WParam.ToInt64() & UInt16.MaxValue) == (IntPtr) 2)
                    {
                        return true;
                    }
                    
                    break;
                case WM.NCRBUTTONDOWN:
                case WM.NCRBUTTONUP:
                    if (!AllowSystemMenuRightClick && message.WParam == (IntPtr) 2)
                    {
                        return true;
                    }
                    
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
        
        protected virtual void OnSystemMenuMouseDown(MouseSystemMenuEventArgs args)
        {
            args.RoutedEvent = SystemMenuMouseDownEvent;
            RaiseEvent(args);
        }
        
        protected virtual void OnSystemMenuMouseUp(MouseSystemMenuEventArgs args)
        {
            args.RoutedEvent = SystemMenuMouseUpEvent;
            RaiseEvent(args);
        }
        
        protected virtual void OnSystemMenuMouseDoubleClick(MouseSystemMenuEventArgs args)
        {
            args.RoutedEvent = SystemMenuMouseDoubleClickEvent;
            RaiseEvent(args);
        }
        
        private void DisableIconClickExit(Object? sender, InterfaceClosingEventArgs args)
        {
            if (args.Reason != InterfaceCloseReason.UserClosing)
            {
                return;
            }

            if (args.Cancel || KeyboardUtilities.Alt.HasAlt && Keyboard.IsKeyDown(Key.F4))
            {
                return;
            }
            
            args.Cancel = IsSystemMenu();
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