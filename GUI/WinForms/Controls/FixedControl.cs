// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Windows.Forms;

namespace NetExtender.GUI.WinForms.Controls
{
    public class FixedControl : Control
    {
        public FixedControl()
        {
            DoubleBuffered = true;
        }

        // ReSharper disable once UnassignedField.Global
        public IButtonControl AcceptButton;

        protected override Boolean ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (AcceptButton is null || keyData != Keys.Enter)
            {
                return base.ProcessCmdKey(ref msg, keyData);
            }

            AcceptButton.PerformClick();
            return true;
        }
    }
}