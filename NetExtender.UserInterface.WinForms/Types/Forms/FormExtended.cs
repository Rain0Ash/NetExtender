// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Drawing;
using System.Windows.Forms;
using NetExtender.UserInterface.Events;
using NetExtender.UserInterface.Interfaces;
using NetExtender.Utilities.Numerics;
using NetExtender.Utilities.Types;
using NetExtender.Utilities.UserInterface;
using NetExtender.Windows;

namespace NetExtender.UserInterface.WinForms.Forms
{
    public abstract class FormExtended : Form, IWindow
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

        public event InterfaceClosingEventHandler WindowClosing = null!;

        public event SizeChangeToggleHandler SizeChangeToggle = null!;

        protected FormExtended()
        {
            Load += OnLoad;
            Load += BringToFront;
            FormClosing += OnClosing;
        }

        protected override void WndProc(ref Message message)
        {
            switch ((WM) message.Msg)
            {
                case WM.ENTERSIZEMOVE:
                {
                    SizeChangeToggleEventArgs args = new SizeChangeToggleEventArgs { End = false };
                    OnSizeChangeToggle(args);

                    if (args.Handled)
                    {
                        return;
                    }

                    break;
                }
                case WM.EXITSIZEMOVE:
                {
                    SizeChangeToggleEventArgs args = new SizeChangeToggleEventArgs { End = true };
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