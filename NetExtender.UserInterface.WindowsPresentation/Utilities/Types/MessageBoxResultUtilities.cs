// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Runtime.CompilerServices;
using System.Windows;
using NetExtender.Types.Exceptions;
using NetExtender.UserInterface;

namespace NetExtender.Utilities.UserInterface.Types
{
    public static class MessageBoxResultUtilities
    {
        public static MessageBoxResult ToMessageBoxResult(Boolean value)
        {
            return value ? MessageBoxResult.Yes : MessageBoxResult.No;
        }

        public static MessageBoxResult ToMessageBoxResult(Boolean? value)
        {
            return value switch
            {
                true => MessageBoxResult.Yes,
                false => MessageBoxResult.No,
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
                _ => throw new EnumUndefinedOrNotSupportedException<MessageBoxResult>(value, nameof(value), null)
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
                InterfaceDialogResult.TryAgain => MessageBoxResult.Yes,
                InterfaceDialogResult.Continue => MessageBoxResult.Yes,
                _ => throw new EnumUndefinedOrNotSupportedException<InterfaceDialogResult>(value, nameof(value), null)
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
                _ => throw new EnumUndefinedOrNotSupportedException<MessageBoxResult>(value, nameof(value), null)
            };
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean None(this MessageBoxResult value)
        {
            return value == MessageBoxResult.None;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean OK(this MessageBoxResult value)
        {
            return value == MessageBoxResult.OK;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean Cancel(this MessageBoxResult value)
        {
            return value == MessageBoxResult.Cancel;
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean Yes(this MessageBoxResult value)
        {
            return value == MessageBoxResult.Yes;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean No(this MessageBoxResult value)
        {
            return value == MessageBoxResult.No;
        }
    }
}