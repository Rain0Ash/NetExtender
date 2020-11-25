// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Windows.Forms;

namespace NetExtender.GUI.WinForms.Controls
{
    public class MultiTextCheckBox : CheckBox
    {
        public String CheckedText { get; set; }
        public String IndeterminateText { get; set; }
        public String UncheckedText { get; set; }

        protected override void OnCheckedChanged(EventArgs e)
        {
            base.OnCheckedChanged(e);

            Text = CheckState switch
            {
                CheckState.Unchecked => UncheckedText,
                CheckState.Checked => CheckedText,
                CheckState.Indeterminate => IndeterminateText,
                _ => Text
            } ?? Text;
        }
    }
}