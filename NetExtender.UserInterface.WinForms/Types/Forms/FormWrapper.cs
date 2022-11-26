// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.ComponentModel;
using System.Windows.Forms;
using NetExtender.UserInterface.Events;
using NetExtender.UserInterface.Interfaces;
using NetExtender.Utilities.Numerics;
using NetExtender.Utilities.Types;

namespace NetExtender.UserInterface.WinForms.Forms
{
    public sealed class FormWrapper : IWindow
    {
        public static implicit operator FormWrapper(Form window)
        {
            return new FormWrapper(window);
        }

        public static implicit operator Form(FormWrapper wrapper)
        {
            return wrapper.Form;
        }

        public event InterfaceClosingEventHandler? WindowClosing;

        public IntPtr Handle
        {
            get
            {
                return Form.Handle;
            }
        }

        public String Name
        {
            get
            {
                return Form.Name;
            }
            set
            {
                Form.Name = value;
            }
        }

        public String Title
        {
            get
            {
                return Form.Text;
            }
            set
            {
                Form.Text = value;
            }
        }

        public Boolean ShowInTaskbar
        {
            get
            {
                return Form.ShowInTaskbar;
            }
            set
            {
                Form.ShowInTaskbar = value;
            }
        }

        public Boolean TopMost
        {
            get
            {
                return Form.TopMost;
            }
            set
            {
                Form.TopMost = value;
            }
        }

        public Double Top
        {
            get
            {
                return Form.Top;
            }
            set
            {
                Form.Top = (Int32) value;
            }
        }

        public Double Width
        {
            get
            {
                return Form.Width;
            }
            set
            {
                Form.Width = (Int32) value.Round();
            }
        }

        public Double Height
        {
            get
            {
                return Form.Height;
            }
            set
            {
                Form.Height = (Int32) value.Round();
            }
        }

        private Form Form { get; }

        public FormWrapper(Form form)
        {
            Form = form ?? throw new ArgumentNullException(nameof(form));
            Form.Closing += OnClosing;
        }

        private void OnClosing(Object? sender, CancelEventArgs args)
        {
            InterfaceClosingEventArgs closing = new InterfaceClosingEventArgs(InterfaceCloseReason.WindowClosing, args.Cancel);
            WindowClosing?.Invoke(sender, closing);

            if (closing.Cancel)
            {
                args.Cancel = closing.Cancel;
            }
        }

        public void Show()
        {
            Form.Show();
        }

        public InterfaceDialogResult ShowDialog()
        {
            return Form.ShowDialog().ToInterfaceDialogResult();
        }

        public Boolean Activate()
        {
            Form.Activate();
            return true;
        }
    }
}