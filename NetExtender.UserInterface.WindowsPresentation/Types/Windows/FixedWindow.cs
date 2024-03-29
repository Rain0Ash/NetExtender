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
using NetExtender.Utilities.Windows.IO;
using NetExtender.Windows;

namespace NetExtender.UserInterface.WindowsPresentation.Windows
{
    public abstract class FixedWindow : HotKeyWindow, IWindow
    {
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
        
        public Boolean TopMost
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

        public virtual Boolean IsAltF4Enabled { get; set; } = true;

        public Boolean IsExitOnFocusLost { get; set; }

        public event InterfaceClosingEventHandler? WindowClosing;
        public event SizeChangeToggleHandler? SizeChangeToggle;

        protected FixedWindow()
        {
            WindowStartupLocation = WindowStartupLocation.CenterOwner;
            WindowStyle = WindowStyle.SingleBorderWindow;
            Deactivated += OnDeactivated;
            Closing += OnClosing;
            WindowClosing += DisableIconClickExit;
        }

        protected override Boolean WndProc(ref WinMessage message)
        {
            switch (message.Message)
            {
                case WM.QUERYENDSESSION:
                case WM.ENDSESSION:
                    CloseReason = InterfaceCloseReason.SystemShutdown;
                    break;

                case WM.SYSCOMMAND:
                    if (((UInt16) message.WParam & 0xFFF0) == 0xF060)
                    {
                        CloseReason = InterfaceCloseReason.UserClosing;
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

        public new InterfaceDialogResult ShowDialog()
        {
            return InterfaceDialogResultUtilities.ToInterfaceDialogResult(base.ShowDialog());
        }
    }
}