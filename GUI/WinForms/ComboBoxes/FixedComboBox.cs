// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.ComponentModel;
using System.Windows.Forms;

namespace NetExtender.GUI.WinForms.ComboBoxes
{
    public class FixedComboBox : ComboBox
    {
        public event CancelEventHandler SelectedIndexChanging;

        [Browsable(false)]
        public Int32 LastAcceptedSelectedIndex { get; private set; } = -1;
        
        public FixedComboBox()
        {
            DoubleBuffered = true;
        }

        protected void OnSelectedIndexChanging(CancelEventArgs e)
        {
            CancelEventHandler changing = SelectedIndexChanging;
            changing?.Invoke(this, e);
        }
        
        protected override void OnSelectedIndexChanged(EventArgs e)
        {
            if (LastAcceptedSelectedIndex == SelectedIndex)
            {
                return;
            }

            CancelEventArgs cancel = new CancelEventArgs();
            OnSelectedIndexChanging(cancel);

            if (cancel.Cancel)
            {
                SelectedIndex = LastAcceptedSelectedIndex;
                return;
            }
            
            LastAcceptedSelectedIndex = SelectedIndex;
            base.OnSelectedIndexChanged(e);
        }
    }
}