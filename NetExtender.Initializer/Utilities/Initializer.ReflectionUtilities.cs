using System;
using System.Collections.Concurrent;
using System.Linq;
using System.Reflection;
using System.Reflection.Emit;
using System.Runtime.CompilerServices;
using NetExtender.Types.Assemblies;
using NetExtender.Types.Assemblies.Interfaces;

namespace NetExtender.Initializer
{
    public abstract partial class Initializer
    {
        internal static class ReflectionUtilities
        {
            private static IDynamicAssembly Assembly { get; } = new DynamicInitializerAssembly($"{nameof(Initializer)}<Seal>", AssemblyBuilderAccess.Run);
            private static ConcurrentDictionary<Type, Type> Storage { get; } = new ConcurrentDictionary<Type, Type>();

            internal class TypeSealException : NotSupportedException
            {
                public Type Type { get; }
                
                public TypeSealException(Type type, String? message)
                    : base(message)
                {
                    Type = type ?? throw new ArgumentNullException(nameof(type));
                }
            }
            
            [MethodImpl(MethodImplOptions.AggressiveOptimization)]
            internal static Boolean IsAbstract(MemberInfo info)
            {
                if (info is null)
                {
                    throw new ArgumentNullException(nameof(info));
                }
                
                return info switch
                {
                    Type type => type.IsAbstract,
                    PropertyInfo property => IsAbstract(property),
                    MethodBase method => method.IsAbstract,
                    EventInfo method => IsAbstract(method),
                    _ => false
                };
            }
            
            [MethodImpl(MethodImplOptions.AggressiveOptimization)]
            internal static Boolean IsAbstract(PropertyInfo info)
            {
                if (info is null)
                {
                    throw new ArgumentNullException(nameof(info));
                }
                
                return info.GetMethod is { IsAbstract: true } || info.SetMethod is { IsAbstract: true };
            }
            
            [MethodImpl(MethodImplOptions.AggressiveOptimization)]
            internal static Boolean IsAbstract(EventInfo info)
            {
                if (info is null)
                {
                    throw new ArgumentNullException(nameof(info));
                }
                
                return info.AddMethod is { IsAbstract: true } || info.RemoveMethod is { IsAbstract: true };
            }
            
            [MethodImpl(MethodImplOptions.AggressiveOptimization)]
            internal static Boolean HasAbstract(Type type)
            {
                if (type is null)
                {
                    throw new ArgumentNullException(nameof(type));
                }
                
                if (!type.IsAbstract)
                {
                    return false;
                }
                
                const BindingFlags binding = BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.DeclaredOnly;
                return type.GetMembers(binding).Any(IsAbstract);
            }
            
            // ReSharper disable once MemberHidesStaticFromOuterClass
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            internal static Type Seal(Type type)
            {
                return Seal(type, Assembly, Storage);
            }
            
            // ReSharper disable once CognitiveComplexity
            [MethodImpl(MethodImplOptions.AggressiveOptimization)]
            internal static Type Seal(Type type, IDynamicAssembly assembly, ConcurrentDictionary<Type, Type>? storage)
            {
                if (type is null)
                {
                    throw new ArgumentNullException(nameof(type));
                }
                
                if (assembly is null)
                {
                    throw new ArgumentNullException(nameof(assembly));
                }
                
                if (type.IsInterface)
                {
                    throw new TypeSealException(type, $"Cannot seal interface type '{type}'.");
                }
                
                if (type.IsValueType || type.IsSealed || type.IsArray)
                {
                    return type;
                }
                
                if (type.IsAbstract && HasAbstract(type))
                {
                    throw new TypeSealException(type, $"Cannot seal abstract type '{type}' with abstract members.");
                }
                
                if (!type.IsClass)
                {
                    throw new TypeSealException(type, "You can seal only class type.");
                }
                
                static Type Create(Type type, IDynamicAssembly assembly)
                {
                    const TypeAttributes attributes = TypeAttributes.Class | TypeAttributes.Sealed;
                    String @namespace = !String.IsNullOrWhiteSpace(type.Namespace) ? type.Namespace + "." : String.Empty;
                    TypeBuilder builder = assembly.Module.DefineType($"{@namespace}Seal(<{type.Name}>)", attributes, type);
                    
                    const BindingFlags binding = BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic;
                    ConstructorInfo[] constructors = type.GetConstructors(binding);
                    
                    switch (constructors.Length)
                    {
                        case <= 0:
                        {
                            ConstructorBuilder constructor = builder.DefineConstructor(MethodAttributes.Public, CallingConventions.Standard, Type.EmptyTypes);
                            ILGenerator generator = constructor.GetILGenerator();
                            
                            generator.Emit(OpCodes.Ldarg_0);
                            generator.Emit(OpCodes.Call, typeof(Object).GetConstructor(Type.EmptyTypes)!);
                            generator.Emit(OpCodes.Ret);
                            break;
                        }
                        case 1 when constructors[0] is { IsFamily: true } info && info.GetParameters().Length <= 0:
                        {
                            ConstructorBuilder constructor = builder.DefineConstructor(MethodAttributes.Public, CallingConventions.Standard, Type.EmptyTypes);
                            ILGenerator generator = constructor.GetILGenerator();
                            
                            generator.Emit(OpCodes.Ldarg_0);
                            generator.Emit(OpCodes.Call, info);
                            generator.Emit(OpCodes.Ret);
                            break;
                        }
                        default:
                        {
                            foreach (ConstructorInfo info in constructors)
                            {
                                Type[] parameters = info.GetParameters().Select(static info => info.ParameterType).ToArray();
                                ConstructorBuilder constructor = builder.DefineConstructor(info.Attributes, info.CallingConvention, parameters);
                                
                                ILGenerator generator = constructor.GetILGenerator();
                                
                                generator.Emit(OpCodes.Ldarg_0);
                                
                                for (Int32 i = 1; i <= parameters.Length; i++)
                                {
                                    generator.Emit(OpCodes.Ldarg, i);
                                }
                                
                                generator.Emit(OpCodes.Call, info);
                                generator.Emit(OpCodes.Ret);
                            }
                            
                            break;
                        }
                    }
                    
                    static void ToString(Type type, TypeBuilder builder)
                    {
                        const BindingFlags binding = BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic;
                        if (type.GetMethod(nameof(ToString), binding, null, Type.EmptyTypes, null) is not { } @string || @string.DeclaringType != typeof(Object))
                        {
                            return;
                        }
                        
                        MethodBuilder @override = builder.DefineMethod(nameof(ToString), MethodAttributes.Public | MethodAttributes.Virtual, typeof(String), Type.EmptyTypes);
                        ILGenerator generator = @override.GetILGenerator();
                        
                        generator.Emit(OpCodes.Ldarg_0);
                        generator.Emit(OpCodes.Call, typeof(Object).GetMethod(nameof(ToString), Type.EmptyTypes)!);
                        generator.Emit(OpCodes.Ret);
                        
                        builder.DefineMethodOverride(@override, @string);
                    }
                    
                    ToString(type, builder);
                    return builder.CreateType() ?? throw new TypeSealException(type, "Unknown seal exception.");
                }
                
                return storage is not null ? storage.GetOrAdd(type, Create, assembly) : Create(type, assembly);
            }
        }
    }
}