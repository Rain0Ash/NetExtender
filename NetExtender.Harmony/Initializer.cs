// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System.Runtime.CompilerServices;
using NetExtender.Harmony.Types;
using NetExtender.Harmony.Utilities;
using NetExtender.Utilities.Core;

namespace NetExtender.Initializer
{
    internal static class NetExtenderHarmonyInitializer
    {
#pragma warning disable CA2255
        [ModuleInitializer]
        public static void Initialize()
        {
            HarmonySignatureUtilities.DebuggerIsAttachedSetter = static value => HarmonyUtilities.Debugger = value;
            HarmonySignatureUtilities.CreateHarmony = static identifier => new HarmonyWrapper(identifier);
            HarmonySignatureUtilities.CreateHarmonyInstruction = static (code, operand) => new CodeInstructionWrapper(code, operand);
        }
#pragma warning restore CA2255
    }
}