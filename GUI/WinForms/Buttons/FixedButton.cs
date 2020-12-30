// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Windows.Forms;

namespace NetExtender.GUI.WinForms.Buttons
{
    public class FixedButton : Button
    {
        protected sealed override Boolean DoubleBuffered
        {
            get
            {
                return base.DoubleBuffered;
            }
            set
            {
                base.DoubleBuffered = value;
            }
        }
        
        public FixedButton()
        {
            DoubleBuffered = true;
        }
    }
}