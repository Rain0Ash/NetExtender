// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Windows.Forms;
using NetExtender.GUI.Common.Interfaces;
using NetExtender.Utils.Numerics;

namespace NetExtender.GUI.WinForms.Forms
{
    public class FormWrapper : IWindow
    {
        public static implicit operator FormWrapper(Form window)
        {
            return new FormWrapper(window);
        }
        
        public static implicit operator Form(FormWrapper wrapper)
        {
            return wrapper._form;
        }

        public event FormClosingEventHandler WindowClosing
        {
            add
            {
                _form.FormClosing += value;
            }
            remove
            {
                _form.FormClosing -= value;
            }
        }

        public IntPtr Handle
        {
            get
            {
                return _form.Handle;
            }
        }

        public String Name
        {
            get
            {
                return _form.Name;
            }
            set
            {
                _form.Name = value;
            }
        }

        public String Title
        {
            get
            {
                return _form.Text;
            }
            set
            {
                _form.Text = value;
            }
        }

        public Boolean ShowInTaskbar
        {
            get
            {
                return _form.ShowInTaskbar;
            }
            set
            {
                _form.ShowInTaskbar = value;
            }
        }

        public Boolean TopMost
        {
            get
            {
                return _form.TopMost;
            }
            set
            {
                _form.TopMost = value;
            }
        }

        public Double Top
        {
            get
            {
                return _form.Top;
            }
            set
            {
                _form.Top = (Int32) value;
            }
        }

        public Double Width
        {
            get
            {
                return _form.Width;
            }
            set
            {
                _form.Width = (Int32) value.Round();
            }
        }

        public Double Height
        {
            get
            {
                return _form.Height;
            }
            set
            {
                _form.Height = (Int32) value.Round();
            }
        }

        private readonly Form _form;
        
        public FormWrapper(Form form)
        {
            _form = form;
        }
        
        public void Show()
        {
            _form.Show();
        }

        public DialogResult ShowDialog()
        {
            return _form.ShowDialog();
        }

        public Boolean Activate()
        {
            _form.Activate();
            return true;
        }
    }
}