// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Runtime.CompilerServices;
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
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean None(this InterfaceDialogResult value)
        {
            return value == InterfaceDialogResult.None;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean OK(this InterfaceDialogResult value)
        {
            return value == InterfaceDialogResult.OK;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean Cancel(this InterfaceDialogResult value)
        {
            return value == InterfaceDialogResult.Cancel;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean Abort(this InterfaceDialogResult value)
        {
            return value == InterfaceDialogResult.Abort;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean Retry(this InterfaceDialogResult value)
        {
            return value == InterfaceDialogResult.Retry;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean Ignore(this InterfaceDialogResult value)
        {
            return value == InterfaceDialogResult.Ignore;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean Yes(this InterfaceDialogResult value)
        {
            return value == InterfaceDialogResult.Yes;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean No(this InterfaceDialogResult value)
        {
            return value == InterfaceDialogResult.No;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean TryAgain(this InterfaceDialogResult value)
        {
            return value == InterfaceDialogResult.TryAgain;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean Continue(this InterfaceDialogResult value)
        {
            return value == InterfaceDialogResult.Continue;
        }
    }
}