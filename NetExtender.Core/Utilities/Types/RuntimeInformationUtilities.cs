// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Runtime.InteropServices;

namespace NetExtender.Utilities.Types
{
    public static class RuntimeInformationUtilities
    {
        public static String? ElevateVerbose
        {
            get
            {
                return RuntimeInformation.IsOSPlatform(OSPlatform.Windows) ? "runas" :
                    RuntimeInformation.IsOSPlatform(OSPlatform.Linux) ||
                    RuntimeInformation.IsOSPlatform(OSPlatform.FreeBSD) ||
                    RuntimeInformation.IsOSPlatform(OSPlatform.OSX) ? "sudo" : null;
            }
        }
    }
}