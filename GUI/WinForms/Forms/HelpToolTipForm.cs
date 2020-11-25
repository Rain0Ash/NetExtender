// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Windows.Forms;
using NetExtender.GUI.WinForms.ToolTips;

namespace NetExtender.GUI.WinForms.Forms
{
    public abstract class HelpToolTipForm : LocalizationForm
    {
        private readonly HelpToolTip _helpToolTip;

        protected HelpToolTipForm()
        {
            _helpToolTip = new HelpToolTip();
        }

        public void SetMessage(Control control, String message)
        {
            if (!Controls.Contains(control))
            {
                return;
            }

            _helpToolTip.SetToolTip(control, message);
        }

        public void RemoveMessage(Control control)
        {
            _helpToolTip.SetToolTip(control, null);
        }

        public void RemoveAllMessages()
        {
            _helpToolTip.RemoveAll();
        }

        protected override void Dispose(Boolean disposing)
        {
            if (disposing)
            {
                _helpToolTip?.Dispose();
            }
            
            base.Dispose(disposing);
        }
    }
}