// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.ComponentModel;

namespace NetExtender.UserInterface.Events
{
    public class InterfaceClosingEventArgs : CancelEventArgs
    {
        /// <summary>
        ///  Provides the reason for the Form close.
        /// </summary>
        public InterfaceCloseReason Reason { get; }

        public InterfaceClosingEventArgs(InterfaceCloseReason reason, Boolean cancel)
            : base(cancel)
        {
            Reason = reason;
        }
    }
}