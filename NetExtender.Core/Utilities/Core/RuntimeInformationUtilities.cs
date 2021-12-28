// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace NetExtender.Utilities.Core
{
    public static class RuntimeInformationUtilities
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean IsOSPlatform(this OSPlatform platform)
        {
            return RuntimeInformation.IsOSPlatform(platform);
        }
    }
}