// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.ComponentModel;
using System.Windows;
using System.Windows.Forms;
using System.Windows.Input;
using NetExtender.GUI.Common;
using NetExtender.GUI.Common.EventArgs;
using NetExtender.Utils.WPF;
using NetExtender.GUI.Common.Interfaces;
using NetExtender.Native;
using NetExtender.Utils.IO;

namespace NetExtender.GUI.WPF.Windows
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

        private CloseReason _reason;

        public CloseReason CloseReason
        {
            get
            {
                return _reason;
            }
            set
            {
                if (value == CloseReason.None)
                {
                    _reason = value;
                }

                _reason = value;
                OnClosing(this, new CancelEventArgs());
            }
        }

        public event FormClosingEventHandler WindowClosing;
        public event SizeChangeToggleHandler SizeChangeToggle;

        protected FixedWindow()
        {
            WindowStartupLocation = WindowStartupLocation.CenterOwner;
            WindowStyle = WindowStyle.SingleBorderWindow;
            Closing += OnClosing;
            WindowClosing += DisableIconClickExit;
        }
        
        protected override void WndProc(ref Message m)
        {
            switch (m.Msg)
            {
                case WM.QUERYENDSESSION:
                case WM.ENDSESSION:
                    CloseReason = CloseReason.WindowsShutDown;
                    break;

                case WM.SYSCOMMAND:
                    if (((UInt16) m.WParam & 0xfff0) == 0xf060)
                    {
                        CloseReason = CloseReason.UserClosing;
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
            }

            base.WndProc(ref m);
        }

        protected virtual void OnSizeChangeToggle(SizeChangeToggleEventArgs e)
        {
            SizeChangeToggle?.Invoke(this, e);
        }

        private void OnClosing(Object sender, CancelEventArgs e)
        {
            if (e.Cancel)
            {
                return;
            }

            FormClosingEventArgs args = new FormClosingEventArgs(CloseReason, e.Cancel);
            WindowClosing?.Invoke(sender, args);

            if (args.Cancel)
            {
                e.Cancel = true;
            }
        }

        private void DisableIconClickExit(Object sender, FormClosingEventArgs e)
        {
            if (e.CloseReason != CloseReason.UserClosing)
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

            e.Cancel = true;
        }

        public new DialogResult ShowDialog()
        {
            return this.ShowResultDialog();
        }
    }
}