// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Windows.Forms;
using NetExtender.GUI.WinForms.Controls;
using NetExtender.GUI.WinForms.ToolTips;

namespace NetExtender.GUI.WinForms.TextBoxes
{
    public class AdvancedTextBox<T> : LocalizationControl where T : TextBox
    {
        public T TextBox { get; }

        public new event EventHandler TextChanged;

        public override String Text
        {
            get
            {
                return TextBox.Text;
            }
            set
            {
                if (TextBox.Text == value)
                {
                    return;
                }

                TextBox.Text = value;
                TextChanged?.Invoke(TextBox, EventArgs.Empty);
            }
        }

        protected HelpToolTip HelpToolTip { get; }

        public AdvancedTextBox(T textBox)
        {
            AutoSize = false;
            TextBox = textBox;
            HelpToolTip = new HelpToolTip();
        }

        protected override void Dispose(Boolean disposing)
        {
            HelpToolTip.Dispose();
            base.Dispose(disposing);
        }
    }
}