// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Reflection;
using System.Reflection.Emit;
using System.Windows.Input;
using HarmonyLib;
using NetExtender.Types.Exceptions;
using NetExtender.Types.Reflection;
using NetExtender.Utilities.Core;
using NetExtender.WindowsPresentation.Types.Commands;

namespace NetExtender.Patch
{
    public partial class WindowsPresentationCommandSenderPatch
    {
        protected static class Signature
        {
            [ReflectionSignature]
            public delegate Boolean CanExecuteCommandSource(ICommandSource commandSource);
            
            [ReflectionSignature]
            public delegate void CriticalExecuteCommandSource(ICommandSource commandSource, Boolean userInitiated);
        }
        
        [SuppressMessage("ReSharper", "MemberHidesStaticFromOuterClass")]
        protected class Patch : AutoReflectionPatch
        {
            [ReflectionNaming]
            protected static Type CommandHelpers
            {
                get
                {
                    const String assembly = "PresentationFramework";
                    const String @namespace = $"{nameof(MS)}.{nameof(MS.Internal)}.Commands";
                    Type? result = Type.GetType($"{@namespace}.{nameof(CommandHelpers)}, {assembly}");
                    return result ?? throw new ReflectionOperationException($"Can't get type '{nameof(CommandHelpers)}' from '{assembly}'.");
                }
            }
            
            [ReflectionNaming]
            protected virtual Signature.CanExecuteCommandSource? CanExecuteCommandSource
            {
                get
                {
                    const BindingFlags binding = BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic;
                    ParameterInfo[]? parameters = typeof(Signature.CanExecuteCommandSource).GetMethod(nameof(Action.Invoke))?.GetSafeParameters();
                    return parameters is not null ? CommandHelpers.GetMethod(nameof(Signature.CanExecuteCommandSource), binding, parameters)?.CreateDelegate<Signature.CanExecuteCommandSource>() : null;
                }
            }
            
            [ReflectionNaming]
            protected virtual Signature.CriticalExecuteCommandSource? CriticalExecuteCommandSource
            {
                get
                {
                    const BindingFlags binding = BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic;
                    ParameterInfo[]? parameters = typeof(Signature.CriticalExecuteCommandSource).GetMethod(nameof(Action.Invoke))?.GetSafeParameters();
                    return parameters is not null ? CommandHelpers.GetMethod(nameof(Signature.CriticalExecuteCommandSource), binding, parameters)?.CreateDelegate<Signature.CriticalExecuteCommandSource>() : null;
                }
            }

            protected virtual HarmonyUtilities.Signature.Transpiler CanExecuteTranspiler
            {
                get
                {
                    static IEnumerable<CodeInstruction> Factory(IEnumerable<CodeInstruction> instructions)
                    {
                        Boolean successful = false;
                        List<CodeInstruction> result = new List<CodeInstruction>(instructions);
                        for (Int32 i = 0; i < result.Count; i++)
                        {
                            if (i + 2 >= result.Count || result[i].opcode != OpCodes.Ldloc_0 || result[i + 1].opcode != OpCodes.Ldloc_1 || result[i + 2].opcode != OpCodes.Callvirt || result[i + 2].operand is not MethodInfo { Name: nameof(CanExecute) } method || method.DeclaringType != typeof(ICommand))
                            {
                                continue;
                            }

                            result[i + 1].opcode = OpCodes.Ldarg_0;
                            result[i + 2].opcode = OpCodes.Call;
                            result[i + 2].operand = CodeInstruction.Call(typeof(Patch), nameof(CanExecute), new[] { typeof(ICommand), typeof(ICommandSource) }).operand;
                            successful = true;
                            break;
                        }
                        
                        if (!successful)
                        {
                            throw new ReflectionPatchSignatureMissingException(nameof(CanExecuteTranspiler));
                        }
                        
                        return result;
                    }

                    return Factory;
                }
            }

            protected virtual HarmonyUtilities.Signature.Transpiler ExecuteTranspiler
            {
                get
                {
                    static IEnumerable<CodeInstruction> Factory(IEnumerable<CodeInstruction> instructions)
                    {
                        Boolean successful = false;
                        List<CodeInstruction> result = new List<CodeInstruction>(instructions);
                        for (Int32 i = 0; i < result.Count; i++)
                        {
                            if (i + 3 >= result.Count || result[i].opcode != OpCodes.Ldloc_0 || result[i + 1].opcode != OpCodes.Ldloc_1 || result[i + 2].opcode != OpCodes.Callvirt || result[i + 2].operand is not MethodInfo {Name: nameof(CanExecute)} method || method.DeclaringType != typeof(ICommand) || result[i + 3].opcode != OpCodes.Brfalse_S)
                            {
                                continue;
                            }
                            
                            result[i + 1].opcode = OpCodes.Ldarg_0;
                            result[i + 2].opcode = OpCodes.Call;
                            result[i + 2].operand = CodeInstruction.Call(typeof(Patch), nameof(Execute), new[] { typeof(ICommand), typeof(ICommandSource) }).operand;
                            successful = true;
                            break;
                        }
                        
                        if (!successful)
                        {
                            throw new ReflectionPatchSignatureMissingException(nameof(ExecuteTranspiler));
                        }

                        return result;
                    }
                    
                    return Factory;
                }
            }

            public override String Name
            {
                get
                {
                    return GetName(typeof(WindowsPresentationCommandSenderPatch));
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
                if (CanExecuteCommandSource is not { } canexecute || CriticalExecuteCommandSource is not { } execute)
                {
                    return ReflectionPatchState.Failed;
                }

                Harmony harmony = new Harmony($"{nameof(NetExtender)}.{nameof(WindowsPresentation)}.{nameof(WindowsPresentationCommandSenderPatch)}");

                harmony.Transpiler(CanExecuteTranspiler, canexecute.Method);
                harmony.Transpiler(ExecuteTranspiler, execute.Method);

                return ReflectionPatchState.Apply;
            }
            
            [ReflectionSignature]
            protected static Boolean CanExecute(ICommand command, ICommandSource? source)
            {
                Object? parameter = source?.CommandParameter;
                return command.CanExecute(new CommandSenderArgs(source, parameter)) || command.CanExecute(parameter);
            }
            
            [ReflectionSignature]
            protected static Boolean Execute(ICommand command, ICommandSource? source)
            {
                const Boolean result = false;
                
                Object? parameter = source?.CommandParameter;
                CommandSenderArgs args = new CommandSenderArgs(source, parameter);
                if (command.CanExecute(args))
                {
                    command.Execute(args);
                    return result;
                }
                
                if (command.CanExecute(parameter))
                {
                    command.Execute(parameter);
                }
                
                return result;
            }
            
            protected override void Dispose(Boolean disposing)
            {
            }
        }
    }
}