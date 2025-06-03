// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Windows.Forms;
using NetExtender.Types.Exceptions;
using NetExtender.UserInterface;

namespace NetExtender.Utilities.UserInterface
{
    public static class CloseReasonUtilities
    {
        public static CloseReason ToCloseReason(this InterfaceCloseReason value)
        {
            return value switch
            {
                InterfaceCloseReason.None => CloseReason.None,
                InterfaceCloseReason.SystemShutdown => CloseReason.WindowsShutDown,
                InterfaceCloseReason.WindowClosing => CloseReason.MdiFormClosing,
                InterfaceCloseReason.UserClosing => CloseReason.UserClosing,
                InterfaceCloseReason.TaskManagerClosing => CloseReason.TaskManagerClosing,
                InterfaceCloseReason.OwnerClosing => CloseReason.FormOwnerClosing,
                InterfaceCloseReason.ApplicationExitCall => CloseReason.ApplicationExitCall,
                _ => throw new EnumUndefinedOrNotSupportedException<InterfaceCloseReason>(value, nameof(value), null)
            };
        }
        
        public static InterfaceCloseReason ToInterfaceCloseReason(this CloseReason value)
        {
            return value switch
            {
                CloseReason.None => InterfaceCloseReason.None,
                CloseReason.WindowsShutDown => InterfaceCloseReason.SystemShutdown,
                CloseReason.MdiFormClosing => InterfaceCloseReason.WindowClosing,
                CloseReason.UserClosing => InterfaceCloseReason.UserClosing,
                CloseReason.TaskManagerClosing => InterfaceCloseReason.TaskManagerClosing,
                CloseReason.FormOwnerClosing => InterfaceCloseReason.OwnerClosing,
                CloseReason.ApplicationExitCall => InterfaceCloseReason.ApplicationExitCall,
                _ => throw new EnumUndefinedOrNotSupportedException<CloseReason>(value, nameof(value), null)
            };
        }
    }
}