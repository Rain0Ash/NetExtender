using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Reflection;
using System.Reflection.Emit;
using HarmonyLib;
using NetExtender.Types.Exceptions;
using NetExtender.Types.Reflection;
using NetExtender.Utilities.Core;

namespace NetExtender.Patch
{
    public partial class WindowsPresentationFusionPatch
    {
        [SuppressMessage("ReSharper", "MemberHidesStaticFromOuterClass")]
        protected class Patch : AutoReflectionPatch
        {
            private static Type? provider;
            protected static Type Provider
            {
                get
                {
                    const String type = "Szpfjzypoo0Omhkusjpj2K{zippOmhkusfRus{okes.#W~tjfvulss4Sidgqwntn";
                    return provider ??= Load(type) ?? throw new ReflectionOperationException($"Can't get '{nameof(Provider)}' type.");
                }
            }
            
            protected virtual MethodInfo? Mode
            {
                get
                {
                    const BindingFlags binding = BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic;
                    
                    foreach (MethodInfo method in Provider.GetMethods(binding))
                    {
                        if (method.GetSafeParameters() is not { Length: 1 } parameters)
                        {
                            continue;
                        }
                        
                        if (method.ReturnType.IsEnum && parameters[0].ParameterType.IsEnum && method.ReturnType != parameters[0].ParameterType)
                        {
                            return method;
                        }
                    }
                    
                    return null;
                }
            }
            
            protected virtual MethodInfo? Project
            {
                get
                {
                    const BindingFlags binding = BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic;
                    
                    foreach (MethodInfo method in Provider.GetMethods(binding))
                    {
                        if (method.GetSafeParameters() is not { Length: 3 } parameters)
                        {
                            continue;
                        }
                        
                        if (method.ReturnType.IsEnum && parameters[0].ParameterType == typeof(String) && parameters[1].ParameterType == typeof(String) && parameters[2].ParameterType.IsEnum && method.ReturnType != parameters[2].ParameterType)
                        {
                            return method;
                        }
                    }
                    
                    return null;
                }
            }

            public override String Name
            {
                get
                {
                    return GetName(typeof(WindowsPresentationFusionPatch));
                }
            }

            public sealed override ReflectionPatchCategory Category
            {
                get
                {
                    return ReflectionPatchCategory.Aphilargyria;
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
                try
                {
                    _ = Provider;
                }
                catch (ReflectionOperationException)
                {
                    return ReflectionPatchState.NotRequired;
                }
                
                Harmony harmony = new Harmony($"{nameof(NetExtender)}.{nameof(WindowsPresentation)}.{nameof(WindowsPresentationFusionPatch)}");
                HarmonyMethod transpiler = new HarmonyMethod(Transpiler());
                
                Boolean any = false;
                if (Mode is { } mode)
                {
                    harmony.Transpiler(transpiler, mode);
                    any = true;
                }
                
                if (Project is { } project)
                {
                    harmony.Transpiler(transpiler, project);
                    any = true;
                }
                
                provider = null;
                return any ? ReflectionPatchState.Apply : ReflectionPatchState.Failed;
            }
            
            protected virtual HarmonyUtilities.Signature.Transpiler Transpiler()
            {
                static IEnumerable<CodeInstruction> Factory(IEnumerable<CodeInstruction> instructions)
                {
                    String name = TypeName("ljehrxk")!;
                    const BindingFlags binding = BindingFlags.Static | BindingFlags.NonPublic;
                    foreach (FieldInfo field in Provider.GetFields(binding).Where(field => field.FieldType == typeof(Boolean) && field.Name.Contains(name)))
                    {
                        yield return new CodeInstruction(OpCodes.Ldc_I4_1);
                        yield return new CodeInstruction(OpCodes.Stsfld, field);
                    }
                    
                    yield return new CodeInstruction(OpCodes.Ldc_I4_0);
                    yield return new CodeInstruction(OpCodes.Ret);
                }
                
                return Factory;
            }
            
            protected static Type? Load(ReadOnlySpan<Char> name)
            {
                return TypeName(name) is { } type ? Type.GetType(type) : null;
            }
            
            protected static unsafe String? TypeName(ReadOnlySpan<Char> name)
            {
                if (name.Length <= 0)
                {
                    return null;
                }
                
                Char* result = stackalloc Char[name.Length];
                for (Int32 i = 0; i < name.Length; i++)
                {
                    result[i] = (Char) (name[i] - i % 8);
                }
                
                return new String(result, 0, name.Length);
            }
            
            protected override void Dispose(Boolean disposing)
            {
                provider = null;
            }
        }
    }
}