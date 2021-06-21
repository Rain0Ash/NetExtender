// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;

namespace NetExtender.UserInterface.Utils
{
    public static class InterfaceDialogResultUtils
    {
        public static InterfaceDialogResult ToInterfaceDialogResult(Boolean? result)
        {
            return result switch
            {
                true => InterfaceDialogResult.OK,
                false => InterfaceDialogResult.Cancel,
                _ => InterfaceDialogResult.None
            };
        }
        
        public static Boolean ToBoolean(this InterfaceDialogResult value)
        {
            return value switch
            {
                InterfaceDialogResult.None => false,
                InterfaceDialogResult.OK => true,
                InterfaceDialogResult.Cancel => false,
                InterfaceDialogResult.Abort => false,
                InterfaceDialogResult.Retry => true,
                InterfaceDialogResult.Ignore => false,
                InterfaceDialogResult.Yes => true,
                InterfaceDialogResult.No => false,
                _ => throw new NotSupportedException()
            };
        }
    }
}