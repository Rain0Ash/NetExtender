// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Windows.Forms;
using NetExtender.UserInterface;

namespace NetExtender.Utils.Types
{
    public static class DialogResultUtils
    {
        public static DialogResult ToDialogResult(Boolean? result)
        {
            return result switch
            {
                true => DialogResult.OK,
                false => DialogResult.Cancel,
                _ => DialogResult.None
            };
        }

        public static Boolean ToBoolean(this DialogResult value)
        {
            return value switch
            {
                DialogResult.None => false,
                DialogResult.OK => true,
                DialogResult.Cancel => false,
                DialogResult.Abort => false,
                DialogResult.Retry => true,
                DialogResult.Ignore => false,
                DialogResult.Yes => true,
                DialogResult.No => false,
                _ => throw new NotSupportedException()
            };
        }
        
        public static DialogResult ToDialogResult(this InterfaceDialogResult value)
        {
            return value switch
            {
                InterfaceDialogResult.None => DialogResult.None,
                InterfaceDialogResult.OK => DialogResult.OK,
                InterfaceDialogResult.Cancel => DialogResult.Cancel,
                InterfaceDialogResult.Abort => DialogResult.Abort,
                InterfaceDialogResult.Retry => DialogResult.Retry,
                InterfaceDialogResult.Ignore => DialogResult.Ignore,
                InterfaceDialogResult.Yes => DialogResult.Yes,
                InterfaceDialogResult.No => DialogResult.No,
                _ => throw new NotSupportedException()
            };
        }

        public static InterfaceDialogResult ToInterfaceDialogResult(this DialogResult value)
        {
            return value switch
            {
                DialogResult.None => InterfaceDialogResult.None,
                DialogResult.OK => InterfaceDialogResult.OK,
                DialogResult.Cancel => InterfaceDialogResult.Cancel,
                DialogResult.Abort => InterfaceDialogResult.Abort,
                DialogResult.Retry => InterfaceDialogResult.Retry,
                DialogResult.Ignore => InterfaceDialogResult.Ignore,
                DialogResult.Yes => InterfaceDialogResult.Yes,
                DialogResult.No => InterfaceDialogResult.No,
                _ => throw new NotSupportedException()
            };
        }
    }
}