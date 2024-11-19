using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Reflection;
using System.Reflection.Emit;
using HarmonyLib;
using Microsoft.Extensions.DependencyInjection;
using NetExtender.Types.Exceptions;
using NetExtender.Types.Reflection;
using NetExtender.Utilities.Core;

namespace NetExtender.Patch
{
    public partial class DependencyInjectionPatch
    {
        protected static class Signature
        {
            [ReflectionSignature(typeof(ActivatorUtilities))]
            public delegate Object CreateInstance(IServiceProvider provider, Type instanceType, params Object[] parameters);
            
            [ReflectionSignature(typeof(ActivatorUtilities))]
            public delegate Boolean TryFindPreferredConstructor(Type instanceType, Type[] argumentTypes, [NotNullWhen(true)] ref ConstructorInfo? matchingConstructor, [NotNullWhen(true)] ref Int32?[]? parameterMap);
            
            [ReflectionSignature(typeof(ActivatorUtilities))]
            public delegate Object CreateConstructorCallSite(Object lifetime, Type serviceType, Type implementationType, Object callSiteChain);
        }
        
        [SuppressMessage("ReSharper", "MemberHidesStaticFromOuterClass")]
        protected class Patch : AutoReflectionPatch
        {
            [ReflectionNaming(typeof(ActivatorUtilities))]
            protected static Type CallSiteFactory
            {
                get
                {
                    const String @namespace = $"{nameof(Microsoft)}.{nameof(Microsoft.Extensions)}.{nameof(Microsoft.Extensions.DependencyInjection)}";
                    Type? result = Type.GetType($"{@namespace}.{nameof(Microsoft.Extensions.DependencyInjection.ServiceLookup)}.{nameof(CallSiteFactory)}, {@namespace}");
                    return result ?? throw new ReflectionOperationException($"Can't get type '{nameof(CallSiteFactory)}' from '{@namespace}'.");
                }
            }
            
            [ReflectionNaming(typeof(ActivatorUtilities))]
            protected static Type ActivatorUtilities
            {
                get
                {
                    return typeof(ActivatorUtilities);
                }
            }
            
            protected virtual Signature.TryFindPreferredConstructor? TryFindPreferredConstructor
            {
                get
                {
                    const BindingFlags binding = BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic;
                    ParameterInfo[]? parameters = typeof(Signature.TryFindPreferredConstructor).GetMethod(nameof(Action.Invoke))?.GetSafeParameters();
                    return parameters is not null ? ActivatorUtilities.GetMethod(nameof(Signature.TryFindPreferredConstructor), binding, parameters)?.CreateDelegate<Signature.TryFindPreferredConstructor>() : null;
                }
            }
            
            protected virtual Signature.CreateInstance? CreateInstance
            {
                get
                {
                    const BindingFlags binding = BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic;
                    ParameterInfo[]? parameters = typeof(Signature.CreateInstance).GetMethod(nameof(Action.Invoke))?.GetSafeParameters();
                    return parameters is not null ? ActivatorUtilities.GetMethod(nameof(Signature.CreateInstance), binding, parameters)?.CreateDelegate<Signature.CreateInstance>() : null;
                }
            }
            
            protected virtual MethodInfo? CreateConstructorCallSite
            {
                get
                {
                    const BindingFlags binding = BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public;
                    return CallSiteFactory.GetMethod(nameof(Signature.CreateConstructorCallSite), binding);
                }
            }

            protected virtual HarmonyUtilities.Signature.Transpiler Transpiler
            {
                get
                {
                    static IEnumerable<CodeInstruction> Factory(IEnumerable<CodeInstruction> instructions)
                    {
                        if (instructions is null)
                        {
                            throw new ArgumentNullException(nameof(instructions));
                        }
                        
                        MethodInfo? original = typeof(Type).GetMethod(nameof(Type.GetConstructors), Type.EmptyTypes);
                        MethodInfo? @new = typeof(Type).GetMethod(nameof(Type.GetConstructors), new[] { typeof(BindingFlags) });
                        
                        Boolean successful = false;
                        foreach (CodeInstruction instruction in instructions)
                        {
                            if (!instruction.Calls(original))
                            {
                                yield return instruction;
                                continue;
                            }
                            
                            const BindingFlags binding = BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.CreateInstance;
                            yield return new CodeInstruction(OpCodes.Ldc_I4, (Int32) binding);
                            yield return new CodeInstruction(OpCodes.Callvirt, @new);
                            successful = true;
                        }
                        
                        if (!successful)
                        {
                            throw new ReflectionPatchSignatureMissingException(nameof(Transpiler));
                        }
                    }
                    
                    return Factory;
                }
            }

            public override String Name
            {
                get
                {
                    return GetName(typeof(DependencyInjectionPatch));
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
                if (TryFindPreferredConstructor is not { } constructor || CreateInstance is not { } instance || CreateConstructorCallSite is not { } callsite)
                {
                    return ReflectionPatchState.Failed;
                }

                Harmony harmony = new Harmony($"{nameof(NetExtender)}.{nameof(DependencyInjection)}.{nameof(DependencyInjectionPatch)}");
                HarmonyMethod transpiler = new HarmonyMethod(Transpiler);

                harmony.Transpiler(transpiler, constructor.Method);
                
                if (instance.Method.DeclaringType?.Assembly.GetName().Version?.Major <= 7)
                {
                    harmony.Transpiler(transpiler, instance.Method);
                }
                
                harmony.Transpiler(transpiler, callsite);

                return ReflectionPatchState.Apply;
            }
            
            protected override void Dispose(Boolean disposing)
            {
            }
        }
    }
}