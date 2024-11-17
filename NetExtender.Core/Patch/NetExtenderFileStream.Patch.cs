using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Reflection;
using System.Reflection.Emit;
using HarmonyLib;
using NetExtender.Types.Exceptions;
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

            protected virtual HarmonyUtilities.Signature.GeneratorTranspiler ChooseStrategyTranspiler
            {
                get
                {
                    static IEnumerable<CodeInstruction> Factory(IEnumerable<CodeInstruction> instructions, ILGenerator generator)
                    {
                        yield return new CodeInstruction(OpCodes.Ldarg_0);
                        yield return new CodeInstruction(OpCodes.Isinst, typeof(FileStreamWrapper2));

                        Label label = generator.DefineLabel();
                        yield return new CodeInstruction(OpCodes.Brfalse_S, label);
                        yield return new CodeInstruction(OpCodes.Ldnull);
                        yield return new CodeInstruction(OpCodes.Ret);

                        using IEnumerator<CodeInstruction> enumerator = instructions.GetEnumerator();

                        if (enumerator.MoveNext())
                        {
                            enumerator.Current.labels.Add(label);
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
                Harmony harmony = new Harmony($"{nameof(NetExtender)}.{nameof(NetExtenderFileStreamPatch)}");

                Boolean any = false;
                HarmonyMethod transpiler = new HarmonyMethod(ChooseStrategyTranspiler);
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