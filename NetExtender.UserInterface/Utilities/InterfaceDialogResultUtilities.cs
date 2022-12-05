// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using NetExtender.Types.Exceptions;

namespace NetExtender.UserInterface.Utilities
{
    public static class InterfaceDialogResultUtilities
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
                InterfaceDialogResult.TryAgain => true,
                InterfaceDialogResult.Continue => true,
                _ => throw new EnumUndefinedOrNotSupportedException<InterfaceDialogResult>(value, nameof(value), null)
            };
        }
    }
}