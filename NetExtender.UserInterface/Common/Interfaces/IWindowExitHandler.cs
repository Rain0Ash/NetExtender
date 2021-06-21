// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using NetExtender.UserInterface.Events;

namespace NetExtender.UserInterface.Interfaces
{
    public interface IWindowExitHandler
    {
        public event InterfaceClosingEventHandler WindowClosing;
    }
}