// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System.Drawing;
using System.Windows.Forms;

namespace NetExtender.UserInterface.WinForms.ToolTips
{
    public sealed class HelpToolTip : ToolTip
    {
        public HelpToolTip()
        {
            IsBalloon = true;
            UseAnimation = true;
            UseFading = true;
            ShowAlways = true;
            AutoPopDelay = 30000;
            BackColor = Color.White;
        }
    }
}