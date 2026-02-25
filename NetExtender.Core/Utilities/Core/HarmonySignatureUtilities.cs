using System;
using System.Collections.Generic;
using System.Reflection.Emit;
using NetExtender.Harmony.Types;
using NetExtender.Harmony.Types.Interfaces;

namespace NetExtender.Utilities.Core
{
    public class HarmonySignatureUtilities
    {
        internal static Func<IHarmony> Harmony { get; set; } = static () => throw new NoHarmonyException();
        public static IHarmony NetExtender
        {
            get
            {
                return Harmony();
            }
        }

        public delegate IEnumerable<IHarmonyInstruction> Transpiler(IEnumerable<IHarmonyInstruction> instructions);
        public delegate IEnumerable<IHarmonyInstruction> GeneratorTranspiler(IEnumerable<IHarmonyInstruction> instructions, ILGenerator generator);

        internal static Action<Boolean?> DebuggerIsAttachedSetter { get; set; } = static _ => throw new NoHarmonyException();
        internal static Func<String?, IHarmony> CreateHarmony { get; set; } = static _ => throw new NoHarmonyException();
        internal static Func<Transpiler, IHarmonyMethod> CreateTranspilerMethod { get; set; } = static _ => throw new NoHarmonyException();
        internal static Func<GeneratorTranspiler, IHarmonyMethod> CreateGeneratorMethod { get; set; } = static _ => throw new NoHarmonyException();
        internal static Func<OpCode, Object?, IHarmonyInstruction> CreateHarmonyInstruction { get; set; } = static (_, _) => throw new NoHarmonyException();
    }
}