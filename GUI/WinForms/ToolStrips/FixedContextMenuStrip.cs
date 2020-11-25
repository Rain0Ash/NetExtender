// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System.Windows.Forms;

namespace NetExtender.GUI.WinForms.ToolStrips
{
    public class FixedContextMenuStrip : ContextMenuStrip
    {
        public FixedContextMenuStrip()
        {
            DoubleBuffered = true;
        }
        
        public event EmptyHandler ItemsChanged;

        protected override void WndProc(ref Message m)
        {
            base.WndProc(ref m);

            switch (m.Msg)
            {
                case 0x1007:
                    ItemsChanged?.Invoke();
                    break;
                case 0x104D:
                    ItemsChanged?.Invoke();
                    break;
                case 0x1008:
                    ItemsChanged?.Invoke();
                    break;
                case 0x1009:
                    ItemsChanged?.Invoke();
                    break;
                default:
                    break;
            }
        }
    }
}