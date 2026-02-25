// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Diagnostics.CodeAnalysis;
using System.Reflection;
using NetExtender.Utilities.Core;

namespace NetExtender.Harmony.Types.Interfaces
{
    public interface IHarmony
    {
        public static IHarmony Create(String? identifier)
        {
            return HarmonySignatureUtilities.CreateHarmony(identifier);
        }

        public MethodInfo Patch(MethodBase original, IHarmonyMethod? prefix = null, IHarmonyMethod? postfix = null, IHarmonyMethod? transpiler = null, IHarmonyMethod? finalizer = null);
        public void Unpatch(MethodBase original, MethodInfo patch);

        public MethodInfo? Transpiler(HarmonySignatureUtilities.Transpiler transpiler, MethodBase original);
        public MethodInfo? Transpiler(HarmonySignatureUtilities.GeneratorTranspiler transpiler, MethodBase original);
        public MethodInfo? Transpiler(IHarmonyMethod transpiler, MethodBase original);
        public ImmutableArray<KeyValuePair<T, MethodInfo>> Transpiler<T>(HarmonySignatureUtilities.Transpiler transpiler, IEnumerable<T?>? originals) where T : MethodBase;
        public ImmutableArray<KeyValuePair<T, MethodInfo>> Transpiler<T>(HarmonySignatureUtilities.GeneratorTranspiler transpiler, IEnumerable<T?>? originals) where T : MethodBase;
        public ImmutableArray<KeyValuePair<T, MethodInfo>> Transpiler<T>(IHarmonyMethod transpiler, IEnumerable<T?>? originals) where T : MethodBase;
        public ImmutableArray<IHarmonyInstruction> Instructions(MethodBase method);
        public Boolean Instructions(HarmonySignatureUtilities.Transpiler? transpiler, MethodInfo method);
        public Boolean Instructions<T>(TryConverter<ImmutableArray<IHarmonyInstruction>, T>? transpiler, MethodBase method, [MaybeNullWhen(false)] out T result);
    }
}