// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Windows;
using NetExtender.UserInterface;

namespace NetExtender.Utils.UserInterface.Types
{
    public static class MessageBoxResultUtils
    {
        public static MessageBoxResult ToMessageBoxResult(Boolean? result)
        {
            return result switch
            {
                true => MessageBoxResult.OK,
                false => MessageBoxResult.Cancel,
                _ => MessageBoxResult.None
            };
        }
        
        public static Boolean ToBoolean(this MessageBoxResult value)
        {
            return value switch
            {
                MessageBoxResult.None => false,
                MessageBoxResult.OK => true,
                MessageBoxResult.Cancel => false,
                MessageBoxResult.Yes => true,
                MessageBoxResult.No => false,
                _ => throw new NotSupportedException()
            };
        }
        
        public static MessageBoxResult ToMessageBoxResult(this InterfaceDialogResult value)
        {
            return value switch
            {
                InterfaceDialogResult.None => MessageBoxResult.None,
                InterfaceDialogResult.OK => MessageBoxResult.OK,
                InterfaceDialogResult.Cancel => MessageBoxResult.Cancel,
                InterfaceDialogResult.Abort => MessageBoxResult.Cancel,
                InterfaceDialogResult.Retry => MessageBoxResult.Cancel,
                InterfaceDialogResult.Ignore => MessageBoxResult.Cancel,
                InterfaceDialogResult.Yes => MessageBoxResult.Yes,
                InterfaceDialogResult.No => MessageBoxResult.No,
                _ => throw new NotSupportedException()
            };
        }
        
        public static InterfaceDialogResult ToInterfaceDialogResult(this MessageBoxResult value)
        {
            return value switch
            {
                MessageBoxResult.None => InterfaceDialogResult.None,
                MessageBoxResult.OK => InterfaceDialogResult.OK,
                MessageBoxResult.Cancel => InterfaceDialogResult.Cancel,
                MessageBoxResult.Yes => InterfaceDialogResult.Yes,
                MessageBoxResult.No => InterfaceDialogResult.No,
                _ => throw new NotSupportedException()
            };
        }
    }
}