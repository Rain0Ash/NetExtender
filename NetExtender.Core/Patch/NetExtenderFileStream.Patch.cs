// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Reflection;
using System.Reflection.Emit;
using NetExtender.Exceptions;
using NetExtender.Harmony.Types.Interfaces;
using NetExtender.Types.Reflection;
using NetExtender.Types.Streams;
using NetExtender.Utilities.Core;

namespace NetExtender.Patch
{
    public partial class NetExtenderFileStreamPatch
    {
        [SuppressMessage("ReSharper", "MemberHidesStaticFromOuterClass")]
        protected class Patch : AutoReflectionPatch
        {
            [ReflectionNaming]
            protected static Type FileStreamHelpers
            {
                get
                {
                    const String assembly = "System.Private.CoreLib";
                    const String @namespace = $"{nameof(System)}.{nameof(System.IO)}.Strategies";
                    Type? result = Type.GetType($"{@namespace}.{nameof(FileStreamHelpers)}, {assembly}");
                    return result ?? throw new ReflectionOperationException($"Can't get type '{nameof(FileStreamHelpers)}' from '{assembly}'.");
                }
            }

            [ReflectionNaming]
            protected static IEnumerable<MethodInfo> ChooseStrategy
            {
                get
                {
                    const BindingFlags binding = BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic;
                    return FileStreamHelpers.GetMethods(binding).Where(static method => method.Name == nameof(ChooseStrategy));
                }
            }

            protected virtual HarmonySignatureUtilities.GeneratorTranspiler ChooseStrategyTranspiler
            {
                get
                {
                    static IEnumerable<IHarmonyInstruction> Factory(IEnumerable<IHarmonyInstruction> instructions, ILGenerator generator)
                    {
                        yield return IHarmonyInstruction.Create(OpCodes.Ldarg_0);
                        yield return IHarmonyInstruction.Create(OpCodes.Isinst, typeof(FileStreamWrapper2));

                        Label label = generator.DefineLabel();
                        yield return IHarmonyInstruction.Create(OpCodes.Brfalse_S, label);
                        yield return IHarmonyInstruction.Create(OpCodes.Ldnull);
                        yield return IHarmonyInstruction.Create(OpCodes.Ret);

                        using IEnumerator<IHarmonyInstruction> enumerator = instructions.GetEnumerator();

                        if (enumerator.MoveNext())
                        {
                            enumerator.Current.Labels.Add(label);
                            yield return enumerator.Current;
                        }

                        while (enumerator.MoveNext())
                        {
                            yield return enumerator.Current;
                        }
                    }

                    return Factory;
                }
            }

            public override String Name
            {
                get
                {
                    return GetName(typeof(NetExtenderFileStreamPatch));
                }
            }

            public sealed override ReflectionPatchCategory Category
            {
                get
                {
                    return ReflectionPatchCategory.Capability;
                }
            }

            public sealed override ReflectionPatchState State { get; protected set; }

            public override ReflectionPatchThrow IsThrow
            {
                get
                {
                    return ReflectionPatchThrow.Log;
                }
            }

            protected override ReflectionPatchState Make()
            {
                IHarmony harmony = IHarmony.Create($"{nameof(NetExtender)}.{nameof(NetExtenderFileStreamPatch)}");

                Boolean any = false;
                IHarmonyMethod transpiler = IHarmonyMethod.Create(ChooseStrategyTranspiler);
                foreach (MethodInfo method in ChooseStrategy)
                {
                    harmony.Transpiler(transpiler, method);
                    any = true;
                }

                return any ? ReflectionPatchState.Apply : ReflectionPatchState.Failed;
            }

            protected override void Dispose(Boolean disposing)
            {
            }
        }
    }
}