// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using NetExtender.UserInterface.Interfaces;
using NetExtender.Utilities.UserInterface;

namespace NetExtender.UserInterface.WindowsPresentation
{
    public class HandleWindow : RegisterWindow, IUserInterfaceHandle
    {
        public IntPtr Handle
        {
            get
            {
                return this.GetHandle();
            }
        }
    }
}