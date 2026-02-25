using System;
using NetExtender.Utilities.Core;

namespace NetExtender.Harmony.Types.Interfaces
{
    public interface IHarmonyMethod
    {
        public static IHarmonyMethod Create(HarmonySignatureUtilities.Transpiler transpiler)
        {
            if (transpiler is null)
            {
                throw new ArgumentNullException(nameof(transpiler));
            }

            return HarmonySignatureUtilities.CreateTranspilerMethod(transpiler);
        }

        public static IHarmonyMethod Create(HarmonySignatureUtilities.GeneratorTranspiler transpiler)
        {
            if (transpiler is null)
            {
                throw new ArgumentNullException(nameof(transpiler));
            }

            return HarmonySignatureUtilities.CreateGeneratorMethod(transpiler);
        }
    }
}