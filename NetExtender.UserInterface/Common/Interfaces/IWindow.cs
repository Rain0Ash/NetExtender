// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;

namespace NetExtender.UserInterface.Interfaces
{
    public interface IWindow : IUserInterfaceHandle, IWindowExitHandler
    {
        public String Name { get; set; }

        public String Title { get; set; }
        
        public Boolean ShowInTaskbar { get; set; }
        
        public Boolean TopMost { get; set; }
        
        public Double Top { get; set; }
        
        public Double Width { get; set; }
        public Double Height { get; set; }

        public void Show();
        public InterfaceDialogResult ShowDialog();

        public Boolean Activate();
    }
}