using System.Diagnostics.CodeAnalysis;
using NetExtender.Harmony.Types;
using NetExtender.Harmony.Types.Interfaces;

namespace NetExtender.Harmony.Utilities
{
    public static partial class InterceptHarmonyUtilities
    {
        [SuppressMessage("ReSharper", "MemberHidesStaticFromOuterClass")]
        private static class Transpilers
        {
            public static IHarmonyMethod HttpClientTranspiler { get; } = new HarmonyMethodWrapper(InterceptHarmonyUtilities.HttpClientTranspiler);
        }
    }
}