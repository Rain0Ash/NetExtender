// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Runtime.CompilerServices;
using Microsoft.AspNetCore.Http;

namespace NetExtender.Utilities.AspNetCore.Types
{
    public static class FormFileUtilities
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean IsEmpty(this IFormFile? value)
        {
            return value is null || value.Length <= 0;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean HasFile(this IFormFile? value)
        {
            return !IsEmpty(value);
        }
    }
}