// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Windows.Forms;

namespace NetExtender.GUI.Common.Interfaces
{
    public interface IWindow : IGUIHandle, IWindowExitHandler
    {
        public String Name { get; set; }

        public String Title { get; set; }
        
        public Boolean ShowInTaskbar { get; set; }
        
        public Boolean TopMost { get; set; }
        
        public Double Top { get; set; }
        
        public Double Width { get; set; }
        public Double Height { get; set; }

        public void Show();
        public DialogResult ShowDialog();

        public Boolean Activate();
    }
}