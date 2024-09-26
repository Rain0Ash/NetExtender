using System;
using System.Reflection;
using HarmonyLib;

namespace NetExtender.Utilities.Core
{
    public static class HarmonyUtilities
    {
        public static MethodInfo Transpiler(this HarmonyLib.Harmony harmony, MethodInfo original, HarmonyMethod transpiler)
        {
            if (harmony is null)
            {
                throw new ArgumentNullException(nameof(harmony));
            }
            
            if (original is null)
            {
                throw new ArgumentNullException(nameof(original));
            }
            
            if (transpiler is null)
            {
                throw new ArgumentNullException(nameof(transpiler));
            }
            
            return harmony.Patch(original, transpiler: transpiler);
        }
    }
}