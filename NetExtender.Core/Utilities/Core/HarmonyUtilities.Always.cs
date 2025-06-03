// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Reflection.Emit;
using HarmonyLib;

namespace NetExtender.Utilities.Core
{
    public static partial class HarmonyUtilities
    {
        private static class AlwaysPatch
        {
            private static ConcurrentDictionary<MethodBase, AlwaysPatchInfo> AlwaysPatchInfoStorage { get; } = new ConcurrentDictionary<MethodBase, AlwaysPatchInfo>();
            private static MethodInfo GetMethodFromHandle { get; }
            private static MethodInfo GetItem { get; }
            private static HarmonyMethod HarmonyAlwaysTranspiler { get; }

            static AlwaysPatch()
            {
                GetMethodFromHandle = typeof(MethodBase).GetMethod(nameof(MethodBase.GetMethodFromHandle), new[] { typeof(RuntimeMethodHandle) }) ?? throw new MissingMethodException(nameof(MethodBase), nameof(MethodBase.GetMethodFromHandle));
                GetItem = typeof(ConcurrentDictionary<MethodBase, AlwaysPatchInfo>).GetMethod("get_Item") ?? throw new MissingMethodException(nameof(ConcurrentDictionary<MethodBase, AlwaysPatchInfo>), "get_Item");
                Func<MethodBase, IEnumerable<CodeInstruction>, ILGenerator, IEnumerable<CodeInstruction>> transpiler = AlwaysTranspiler;
                HarmonyAlwaysTranspiler = new HarmonyMethod(transpiler.Method);
            }

            private static MethodInfo? Apply(HarmonyLib.Harmony harmony, MethodInfo original, AlwaysPatchInfo patch)
            {
                if (harmony is null)
                {
                    throw new ArgumentNullException(nameof(harmony));
                }

                if (original is null)
                {
                    throw new ArgumentNullException(nameof(original));
                }

                AlwaysPatchInfoStorage[original] = patch ?? throw new ArgumentNullException(nameof(patch));

                try
                {
                    return harmony.Transpiler(HarmonyAlwaysTranspiler, original);
                }
                catch (TargetInvocationException exception) when (exception.InnerException is InvalidOperationException operation)
                {
                    throw operation;
                }
            }
            
            public static MethodInfo? Apply(HarmonyLib.Harmony harmony, MethodInfo original, Object? @return, params Object?[]? values)
            {
                if (harmony is null)
                {
                    throw new ArgumentNullException(nameof(harmony));
                }

                if (original is null)
                {
                    throw new ArgumentNullException(nameof(original));
                }

                AlwaysPatchInfo patch = new AlwaysPatchInfo(original, values, @return);
                return Apply(harmony, original, patch);
            }

            // ReSharper disable once CognitiveComplexity
            private static IEnumerable<CodeInstruction> AlwaysTranspiler(MethodBase original, IEnumerable<CodeInstruction> instructions, ILGenerator generator)
            {
                if (!AlwaysPatchInfoStorage.TryGetValue(original, out AlwaysPatchInfo? info) || original is not MethodInfo method)
                {
                    throw new InvalidOperationException();
                }

                LocalBuilder patch = generator.DeclareLocal(typeof(AlwaysPatchInfo));
                yield return new CodeInstruction(OpCodes.Call, AccessTools.PropertyGetter(typeof(AlwaysPatch), nameof(AlwaysPatchInfoStorage)));
                yield return new CodeInstruction(OpCodes.Ldtoken, method);
                yield return new CodeInstruction(OpCodes.Call, GetMethodFromHandle);
                yield return new CodeInstruction(OpCodes.Callvirt, GetItem);
                yield return new CodeInstruction(OpCodes.Stloc, patch);

                Int32 index = 0;
                for (Int32 i = 0; i < info.Parameters.Length; i++)
                {
                    ParameterInfo parameter = info.Parameters[i];
                    if (!parameter.ParameterType.IsByRef)
                    {
                        continue;
                    }

                    yield return new CodeInstruction(OpCodes.Ldarg, i + info.Offset);
                    yield return new CodeInstruction(OpCodes.Ldloc, patch);
                    yield return new CodeInstruction(OpCodes.Call, AccessTools.PropertyGetter(typeof(AlwaysPatchInfo), nameof(AlwaysPatchInfo.Values)));
                    yield return new CodeInstruction(OpCodes.Ldc_I4, index++);
                    yield return new CodeInstruction(OpCodes.Ldelem_Ref);
                    
                    if (parameter.ParameterType.GetElementType() is not { } element)
                    {
                        throw new InvalidOperationException($"Element type for parameter type '{parameter.ParameterType}' cannot be null.");
                    }

                    if (element.IsValueType)
                    {
                        yield return new CodeInstruction(OpCodes.Unbox_Any, element);
                        yield return new CodeInstruction(OpCodes.Stobj, element);
                    }
                    else
                    {
                        yield return new CodeInstruction(OpCodes.Castclass, element);
                        yield return new CodeInstruction(OpCodes.Stind_Ref);
                    }
                }

                if (!method.ReturnType.IsVoid())
                {
                    yield return new CodeInstruction(OpCodes.Ldloc, patch);
                    yield return new CodeInstruction(OpCodes.Call, AccessTools.PropertyGetter(typeof(AlwaysPatchInfo), nameof(AlwaysPatchInfo.Return)));
                    yield return new CodeInstruction(method.ReturnType.IsValueType ? OpCodes.Unbox_Any : OpCodes.Castclass, method.ReturnType);
                }

                yield return new CodeInstruction(OpCodes.Ret);
            }

            private sealed record AlwaysPatchInfo
            {
                public MethodInfo Method { get; }
                public ParameterInfo[] Parameters { get; }
                public ParameterInfo[] Reference { get; }
                public Object?[]? Values { get; }
                public Object? Return { get; }

                public Int32 Offset
                {
                    get
                    {
                        return Method.IsStatic ? 0 : 1;
                    }
                }

                public AlwaysPatchInfo(MethodInfo method, Object?[]? values, Object? @return)
                {
                    Method = method ?? throw new ArgumentNullException(nameof(method));
                    Parameters = method.GetSafeParameters() ?? throw new NotSupportedException();
                    Values = values;
                    Return = @return;

                    if (Validate(Method.ReturnType, out ParameterInfo[] reference) is { } exception)
                    {
                        throw exception;
                    }

                    Reference = reference;
                }

                private Exception? Validate(Type @return, out ParameterInfo[] reference)
                {
                    if (@return is null)
                    {
                        throw new ArgumentNullException(nameof(@return));
                    }

                    reference = null!;
                    if (@return.IsVoid() && Return is not null)
                    {
                        return new ArgumentException("Method has void return type but a return value was provided.");
                    }

                    if (!@return.IsVoid() && Return is null && @return.IsValueType && Nullable.GetUnderlyingType(@return) is null)
                    {
                        return new ArgumentException($"Cannot assign null to non-nullable return type '{@return}'.");
                    }

                    if (!@return.IsVoid() && Return is not null && !@return.IsInstanceOfType(Return))
                    {
                        return new ArgumentException($"Return value type '{Return.GetType()}' cannot be assigned to method return type '{@return}'.");
                    }
                    
                    Object?[] values = Values ?? Array.Empty<Object?>();
                    reference = Parameters.Where(static parameter => parameter.ParameterType.IsByRef).ToArray();
                    
                    if (reference.Length != values.Length)
                    {
                        return new ArgumentException("The number of values for ref/out parameters does not match the number of such parameters in the method.");
                    }

                    return Validate(reference);
                }

                // ReSharper disable once CognitiveComplexity
                private Exception? Validate(ParameterInfo[] reference)
                {
                    if (reference is null)
                    {
                        throw new ArgumentNullException(nameof(reference));
                    }

                    if (Values is null)
                    {
                        return null;
                    }
                    
                    for (Int32 i = 0; i < reference.Length; i++)
                    {
                        ParameterInfo parameter = reference[i];
                        Type? element = parameter.ParameterType.GetElementType();
                        Object? value = Values[i];

                        if (element is null)
                        {
                            return new InvalidOperationException($"Element type for parameter type '{parameter.ParameterType}' cannot be null.");
                        }

                        if (value is null && element.IsValueType && Nullable.GetUnderlyingType(element) is null)
                        {
                            return new ArgumentException($"Cannot assign null to non-nullable parameter '{parameter.Name}'.");
                        }

                        if (value is not null && !element.IsInstanceOfType(value))
                        {
                            return new ArgumentException($"Value type '{value.GetType()}' cannot be assigned to parameter '{parameter.Name}' of type '{element}'.");
                        }
                    }

                    return null;
                }
            }
        }
    }
}