// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Reflection;

namespace NetExtender.Utilities.Core
{
    public static partial class HarmonyUtilities
    {
        internal static Boolean? Debugger
        {
            get
            {
                return DebuggerPatch.IsAttached;
            }
            set
            {
                DebuggerPatch.IsAttached = value;
            }
        }
        
        [SuppressMessage("ReSharper", "MemberHidesStaticFromOuterClass")]
        private static class DebuggerPatch
        {
            private static Boolean? state;
            public static Boolean? IsAttached
            {
                get
                {
                    return state;
                }
                set
                {
                    lock (Harmony)
                    {
                        if (state == value)
                        {
                            return;
                        }

                        switch (value)
                        {
                            case null:
                                throw new NotSupportedException("Debugger unpatching is not supported.");
                            case false:
                                state = value;
                                Patch = Harmony.Always(Getter, false);
                                return;
                            case true:
                                state = value;
                                Patch = Harmony.Always(Getter, true);
                                return;
                        }
                    }
                }
            }
            
            private static HarmonyLib.Harmony Harmony { get; } = new HarmonyLib.Harmony($"{nameof(NetExtender)}.{nameof(System.Diagnostics.Debugger)}");
            private static PropertyInfo Debugger { get; }
            private static MethodInfo Getter { get; }
            private static MethodInfo? Patch { get; set; }

            static DebuggerPatch()
            {
                Debugger = typeof(Debugger).GetProperty(nameof(System.Diagnostics.Debugger.IsAttached)) ?? throw new MissingMethodException(nameof(System.Diagnostics.Debugger), nameof(System.Diagnostics.Debugger.IsAttached));
                Getter = Debugger.GetMethod ?? throw new MissingMethodException(nameof(System.Diagnostics.Debugger), nameof(System.Diagnostics.Debugger.IsAttached));
            }
        }
    }
}