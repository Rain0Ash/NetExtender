// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.ComponentModel;
using System.Windows;
using System.Windows.Input;
using NetExtender.UserInterface.Events;
using NetExtender.UserInterface.Interfaces;
using NetExtender.UserInterface.Utils;
using NetExtender.UserInterface.Windows.Types;
using NetExtender.Utils.Windows.IO;
using NetExtender.Windows;

namespace NetExtender.UserInterface.WindowsPresentation.Windows
{
    public abstract class FixedWindow : WndProcWindow, IWindow
    {
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

        public event InterfaceClosingEventHandler WindowClosing = null!;
        public event SizeChangeToggleHandler SizeChangeToggle = null!;

        protected FixedWindow()
        {
            WindowStartupLocation = WindowStartupLocation.CenterOwner;
            WindowStyle = WindowStyle.SingleBorderWindow;
            Closing += OnClosing;
            WindowClosing += DisableIconClickExit;
        }
        
        protected override void WndProc(ref WinMessage m)
        {
            switch (m.Message)
            {
                case WM.QUERYENDSESSION:
                case WM.ENDSESSION:
                    CloseReason = InterfaceCloseReason.SystemShutdown;
                    break;

                case WM.SYSCOMMAND:
                    if (((UInt16) m.WParam & 0xfff0) == 0xf060)
                    {
                        CloseReason = InterfaceCloseReason.UserClosing;
                    }

                    break;
                case WM.ENTERSIZEMOVE:
                {
                    SizeChangeToggleEventArgs args = new SizeChangeToggleEventArgs {End = false};
                    OnSizeChangeToggle(args);

                    if (args.Handled)
                    {
                        return;
                    }
                    
                    break;
                }
                case WM.EXITSIZEMOVE:
                {
                    SizeChangeToggleEventArgs args = new SizeChangeToggleEventArgs {End = true};
                    OnSizeChangeToggle(args);

                    if (args.Handled)
                    {
                        return;
                    }
                    
                    break;
                }
                default:
                    break;
            }

            base.WndProc(ref m);
        }

        protected virtual void OnSizeChangeToggle(SizeChangeToggleEventArgs args)
        {
            SizeChangeToggle?.Invoke(this, args);
        }

        private void OnClosing(Object sender, CancelEventArgs args)
        {
            if (args.Cancel)
            {
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

            if (KeyboardUtils.Alt.IsAlt && Keyboard.IsKeyDown(Key.F4))
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
            return InterfaceDialogResultUtils.ToInterfaceDialogResult(base.ShowDialog());
        }
    }
}