// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Drawing;
using System.Windows.Forms;
using NetExtender.UserInterface.Events;
using NetExtender.UserInterface.Interfaces;
using NetExtender.Utilities.Numerics;
using NetExtender.Utilities.UserInterface;
using NetExtender.Utilities.UserInterface.Types;
using NetExtender.Windows;

namespace NetExtender.UserInterface.WinForms.Forms
{
    public abstract class FixedForm : HotKeyForm, IWindow
    {
        public String Title
        {
            get
            {
                return Text;
            }
            set
            {
                Text = value;
            }
        }

        public new Double Top
        {
            get
            {
                return base.Top;
            }
            set
            {
                base.Top = (Int32) value;
            }
        }

        Double IWindow.Width
        {
            get
            {
                return Width;
            }
            set
            {
                Width = (Int32) value.Round();
            }
        }

        Double IWindow.Height
        {
            get
            {
                return Height;
            }
            set
            {
                Height = (Int32) value.Round();
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
                    return;
                }

                _reason = value;
                OnClosing(this, new FormClosingEventArgs(value, false));
            }
        }
        
        public Boolean IsAltF4Enabled { get; set; }

        public Boolean IsExitOnFocusLost { get; set; }

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
                    this.SetWindowSystemMenu(state);
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

        protected FixedForm()
        {
            Load += OnLoad;
            Load += BringToFront;
            FormClosing += OnClosing;
        }

        protected override void WndProc(ref Message message)
        {
            switch ((WM) message.Msg)
            {
                case WM.QUERYENDSESSION:
                case WM.ENDSESSION:
                    CloseReason = CloseReason.WindowsShutDown;
                    break;
                case WM.SYSCOMMAND:
                    if (((UInt16) message.WParam & 0xFFF0) == 0xF060)
                    {
                        CloseReason = CloseReason.UserClosing;
                    }

                    break;
                case WM.ENTERSIZEMOVE:
                case WM.EXITSIZEMOVE:
                {
                    SizeChangeToggleEventArgs args = new SizeChangeToggleEventArgs { End = message.Msg == (Int32) WM.EXITSIZEMOVE };
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

            base.WndProc(ref message);
        }

        protected virtual void OnSizeChangeToggle(SizeChangeToggleEventArgs args)
        {
            SizeChangeToggle?.Invoke(this, args);
        }

        private void OnLoad(Object? sender, EventArgs args)
        {
            DoubleBuffered = true;
        }
        
        private void OnDeactivated(Object? sender, EventArgs args)
        {
            if (!IsExitOnFocusLost)
            {
                return;
            }

            CloseReason = CloseReason.MdiFormClosing;
        }

        protected virtual void BringToFront(Object? sender, EventArgs args)
        {
            BringToFront();
        }

        private void OnClosing(Object? sender, FormClosingEventArgs args)
        {
            if (args.Cancel)
            {
                return;
            }
            
            // TODO: ALT-F4

            if (args.CloseReason == CloseReason.UserClosing)
            {
                Size size = new Size(32, 32);
                if (new Rectangle(Location, size).Contains(Cursor.Position))
                {
                    args.Cancel = true;
                    return;
                }
            }

            InterfaceClosingEventArgs closing = new InterfaceClosingEventArgs(InterfaceCloseReason.WindowClosing, args.Cancel);
            WindowClosing?.Invoke(sender, closing);

            if (closing.Cancel)
            {
                args.Cancel = true;
            }
        }

        public Boolean BringToForeground()
        {
            return WinFormsUserInterfaceUtilities.BringToForeground(this);
        }

        public new InterfaceDialogResult ShowDialog()
        {
            return base.ShowDialog().ToInterfaceDialogResult();
        }

        protected override void Dispose(Boolean disposing)
        {
            if (disposing)
            {
                Load -= OnLoad;
                Load -= BringToFront;
                FormClosing -= OnClosing;
            }

            base.Dispose(disposing);
        }

        /// <inheritdoc cref="Form.Activate"/>
        public new Boolean Activate()
        {
            base.Activate();
            return true;
        }
    }
}