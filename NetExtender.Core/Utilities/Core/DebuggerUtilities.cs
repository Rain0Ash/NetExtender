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