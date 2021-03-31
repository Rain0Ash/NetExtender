// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Drawing;
using System.Windows.Forms;
using System.Windows.Input;
using NetExtender.GUI.Common.Interfaces;
using NetExtender.Utils.GUI;
using NetExtender.Utils.IO;
using NetExtender.Utils.Numerics;

namespace NetExtender.GUI.WinForms.Forms
{
    public abstract class FixedForm : Form, IWindow
    {
        public Object ReturnValue { get; protected set; }

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

        public event FormClosingEventHandler WindowClosing
        {
            add
            {
                FormClosing += value;
            }
            remove
            {
                FormClosing -= value;
            }
        }

        protected FixedForm()
        {
            Load += OnLoad;
            Load += BringToFront;
            FormClosing += OnClosing;
        }

        private void OnLoad(Object sender, EventArgs e)
        {
            DoubleBuffered = true;
        }

        protected virtual void BringToFront(Object sender, EventArgs args)
        {
            BringToFront();
        }

        private void OnClosing(Object sender, FormClosingEventArgs e)
        {
            if (e.CloseReason != CloseReason.UserClosing)
            {
                return;
            }

            if (KeyboardUtils.Alt.IsAlt && Keyboard.IsKeyDown(Key.F4))
            {
                return;
            }

            Size iconSize = new Size(32, 32);
            if (new Rectangle(Location, iconSize).Contains(System.Windows.Forms.Cursor.Position))
            {
                e.Cancel = true;
            }
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
        
        public Boolean BringToForeground()
        {
            return GUIUtils.BringToForeground((IGUIHandle) this);
        }

        /// <inheritdoc cref="Form.Activate"/>
        public new Boolean Activate()
        {
            base.Activate();
            return true;
        }
    }
}