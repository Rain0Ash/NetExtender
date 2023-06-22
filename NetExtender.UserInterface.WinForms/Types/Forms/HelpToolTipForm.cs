// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Windows.Forms;
using NetExtender.UserInterface.WinForms.ToolTips;

namespace NetExtender.UserInterface.WinForms.Forms
{
    public abstract class HelpToolTipForm : FixedForm
    {
        private HelpToolTip? ToolTip { get; set; }

        protected HelpToolTipForm()
        {
            ToolTip = new HelpToolTip();
        }

        public void SetMessage(Control control, String message)
        {
            if (ToolTip is null)
            {
                throw new ObjectDisposedException(nameof(ToolTip));
            }
            
            if (!Controls.Contains(control))
            {
                return;
            }

            ToolTip.SetToolTip(control, message);
        }

        public void RemoveMessage(Control control)
        {
            if (ToolTip is null)
            {
                throw new ObjectDisposedException(nameof(ToolTip));
            }
            
            ToolTip.SetToolTip(control, null);
        }

        public void RemoveAllMessages()
        {
            if (ToolTip is null)
            {
                throw new ObjectDisposedException(nameof(ToolTip));
            }
            
            ToolTip.RemoveAll();
        }

        protected override void Dispose(Boolean disposing)
        {
            if (disposing)
            {
                ToolTip?.Dispose();
                ToolTip = null;
            }

            base.Dispose(disposing);
        }
    }
}