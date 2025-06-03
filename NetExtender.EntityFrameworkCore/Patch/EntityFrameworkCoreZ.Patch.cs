// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Collections.Immutable;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Reflection;
using System.Reflection.Emit;
using HarmonyLib;
using NetExtender.Types.Exceptions;
using NetExtender.Types.Reflection;
using NetExtender.Types.Sets;
using NetExtender.Utilities.Core;

namespace NetExtender.Patch
{
    public partial class EntityFrameworkCoreZPatch
    {
        protected static class Signature
        {
            public delegate void Add(String key, String value);
        }
        
        [SuppressMessage("ReSharper", "MemberHidesStaticFromOuterClass")]
        protected class Patch : AutoReflectionPatch
        {
            private Type? _manager;
            protected virtual Type Manager
            {
                get
                {
                    const String type = @"Z/GqxnzFscpi|uyk/G{xjtzippv2QojeouhQfthgft/$_4Lnukw}Kxhmfyrvp4Lxugqwnuus/GIGtxl";
                    return _manager ??= Load(type) ?? throw new ReflectionOperationException($"Can't get '{nameof(Manager)}' type.");
                }
            }
            
            protected virtual Signature.Add? Add
            {
                get
                {
                    if (typeof(Signature.Add).GetMethod(nameof(Action.Invoke))?.GetSafeParameters() is not { } signature)
                    {
                        return null;
                    }
                    
                    const BindingFlags binding = BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic;
                    foreach (MethodInfo method in Manager.GetMethods(binding).Where(static method => method.Name.StartsWith(nameof(Signature.Add))))
                    {
                        if (method.GetSafeParameters() is not { } parameters || parameters.Length != signature.Length || !ReflectionUtilities.IsVoid(method.ReturnType))
                        {
                            continue;
                        }
                        
                        if (parameters.Zip(signature).All(static zip => zip.First.ParameterType == zip.Second.ParameterType))
                        {
                            return method.CreateDelegate<Signature.Add>();
                        }
                    }
                    
                    return null;
                }
            }
            
            protected virtual Type? CustomManager
            {
                get
                {
                    return Manager.Assembly.GetSafeTypes().FirstOrDefault(static type => type.GetMethods(BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic, typeof(Boolean), typeof(String).MakeByRefType(), new Predicate<Type>(static type => type.IsEnum), typeof(Boolean?), typeof(Boolean?), typeof(Boolean?), typeof(Boolean)).Any());
                }
            }
            
            protected virtual MethodInfo? Access
            {
                get
                {
                    const BindingFlags binding = BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic;
                    return CustomManager?.GetMethods(binding, typeof(Boolean?), TypeSignature.Enum, typeof(Boolean), typeof(Boolean), typeof(Boolean), typeof(Boolean)).FirstOrDefault();
                }
            }

            public override String Name
            {
                get
                {
                    return GetName(typeof(EntityFrameworkCoreZPatch));
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
                    _ = Manager;
                }
                catch (ReflectionOperationException)
                {
                    return ReflectionPatchState.NotRequired;
                }
                
                if (Add is not { } add || Access is not { } access)
                {
                    return ReflectionPatchState.Failed;
                }
                
                Harmony harmony = new Harmony($"{nameof(NetExtender)}.{nameof(EntityFrameworkCore)}.{nameof(EntityFrameworkCoreZPatch)}");
                
                if (!harmony.Instructions<OrderedSet<MethodInfo>>(access, Getters, out OrderedSet<MethodInfo>? getters) || getters.Count <= 0)
                {
                    return ReflectionPatchState.Failed;
                }
                
                foreach (MethodInfo getter in getters)
                {
                    ImmutableArray<CodeInstruction> instructions = harmony.Instructions(getter);
                    if (Field(instructions) is { } field)
                    {
                        field.SetValue(null, true);
                    }
                }
                
                add(nameof(NetExtender), nameof(ReflectionPatchCategory.Aphilargyria));
                return ReflectionPatchState.Apply;
            }
            
            protected virtual Boolean Getters(ImmutableArray<CodeInstruction> instructions, [MaybeNullWhen(false)] out OrderedSet<MethodInfo> result)
            {
                result = new OrderedSet<MethodInfo>();
                
                foreach (CodeInstruction instruction in instructions)
                {
                    if (instruction.opcode != OpCodes.Call || instruction.operand is not MethodInfo method)
                    {
                        continue;
                    }
                    
                    const String name = "get_";
                    if (method.ReturnType != typeof(Boolean?) || !method.Name.StartsWith(name))
                    {
                        continue;
                    }
                    
                    result.Add(method);
                }
                
                return result.Count > 0;
            }
            
            protected virtual FieldInfo? Field(ImmutableArray<CodeInstruction> instructions)
            {
                foreach (CodeInstruction instruction in instructions)
                {
                    if (instruction.opcode == OpCodes.Ldsfld && instruction.operand is FieldInfo field)
                    {
                        return field;
                    }
                }
                
                return null;
            }
            
            protected Type? Load(ReadOnlySpan<Char> name)
            {
                return TypeName(name) is { } type ? Type.GetType(type) : null;
            }
            
            protected virtual unsafe String? TypeName(ReadOnlySpan<Char> name)
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
                _manager = null;
            }
        }
    }
}