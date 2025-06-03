// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Diagnostics;

namespace NetExtender.Utilities.Core
{
    public static class DebuggerUtilities
    {
        public static Boolean? IsAttached
        {
            get
            {
                return Debugger.IsAttached;
            }
            set
            {
                HarmonyUtilities.Debugger = value;
            }
        }
    }
}