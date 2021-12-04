// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Runtime.CompilerServices;
using NetExtender.Utilities.Types;

namespace NetExtender.Utilities.Application
{
    public static class EnvironmentUtilities
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Exception FailFast()
        {
            return ExceptionUtilities.FailFast();
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Exception FailFast(String? message)
        {
            return ExceptionUtilities.FailFast(message);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Exception FailFast(String? message, Exception? exception)
        {
            return ExceptionUtilities.FailFast(message, exception);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Exception FailFast(Exception? exception)
        {
            return exception.FailFast();
        }
    }
}