// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Reflection;
using System.Reflection.Emit;
using System.Runtime.CompilerServices;
using NetExtender.Types.Assemblies;
using NetExtender.Types.Assemblies.Interfaces;
using NetExtender.Types.Exceptions;
using NetExtender.Utilities.Types;

namespace NetExtender.Utilities.Core
{
    public static partial class ReflectionUtilities
    {

        [SuppressMessage("ReSharper", "MemberHidesStaticFromOuterClass")]
        private static class StaticStorage
        {
            private static IDynamicAssemblyUnsafeStorage Assembly { get; } = new DynamicAssemblyStorage($"{nameof(ReflectionUtilities)}<{nameof(Static)}>", AssemblyBuilderAccess.Run);

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static Type Static(Type type, Func<Type, String, String?>? name, Action<Type, TypeBuilder>? builder)
            {
                return Static(Assembly, type, name, builder);
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static Type Static(IDynamicAssembly assembly, Type type, Func<Type, String, String?>? name, Action<Type, TypeBuilder>? builder)
            {
                if (type is null)
                {
                    throw new ArgumentNullException(nameof(type));
                }

                if (assembly is null)
                {
                    throw new ArgumentNullException(nameof(assembly));
                }

                if (type is not { IsAbstract: true, IsSealed: true })
                {
                    throw new TypeNotSupportedException(type, $"Type '{type}' must be static.");
                }

                static Type Create(Type type, Func<Type, String, String?>? name, Action<Type, TypeBuilder>? handler, IDynamicAssembly assembly)
                {
                    const TypeAttributes attributes = TypeAttributes.Class | TypeAttributes.Abstract | TypeAttributes.Sealed;
                    String @namespace = !String.IsNullOrWhiteSpace(type.Namespace) ? type.Namespace + "." : String.Empty;
                    TypeBuilder builder = assembly.Module.DefineType(name?.Invoke(type, @namespace) ?? $"{@namespace}{nameof(Static)}(<{type.Name}>)", attributes);
                    builder.InheritCustomAttributes(type);

                    MemberInfo[] members = Init(builder, type, out Dictionary<MemberInfo, Object> storage);
                    Inherit(builder, type, members, storage);

                    handler?.Invoke(type, builder);
                    return builder.CreateType() ?? throw new TypeNotSupportedException(type, "Unknown exception.");
                }

                return Create(type, name, builder, assembly);
            }

            private static MemberInfo[] Init(TypeBuilder type, Type @base, out Dictionary<MemberInfo, Object> storage)
            {
                if (type is null)
                {
                    throw new ArgumentNullException(nameof(type));
                }

                if (@base is null)
                {
                    throw new ArgumentNullException(nameof(@base));
                }

                storage = new Dictionary<MemberInfo, Object>();
                List<MemberInfo> members = new List<MemberInfo>();
                
                const BindingFlags binding = BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic;
                foreach (MemberInfo member in @base.GetMembers(binding))
                {
                    switch (member)
                    {
                        case FieldInfo field:
                        {
                            FieldBuilder builder = type.InheritFieldInit(field);
                            members.Add(builder);
                            storage.Add(field, builder);
                            continue;
                        }
                        case PropertyInfo property:
                        {
                            PropertyBuilder builder = type.InheritPropertyInit(property, out MethodBuilder? get, out MethodBuilder? set);
                            members.Add(builder);
                            members.AddIfNotNull(get);
                            members.AddIfNotNull(set);
                            storage.Add(property, (builder, get, set));
                            continue;
                        }
                        case EventInfo @event:
                        {
                            EventBuilder builder = type.InheritEventInit(@event, out MethodBuilder? raise, out MethodBuilder? add, out MethodBuilder? remove);
                            members.AddIfNotNull(raise);
                            members.AddIfNotNull(add);
                            members.AddIfNotNull(remove);
                            storage.Add(@event, (builder, raise, add, remove));
                            continue;
                        }
                        case MethodInfo method:
                        {
                            MethodBuilder builder = type.InheritMethodInit(method, out ILGenerator generator);
                            members.Add(builder);
                            storage.Add(method, (builder, generator));
                            continue;
                        }
                        case ConstructorInfo { IsStatic: true } constructor when constructor.DeclaringType == @base && constructor.GetSafeParameters()?.Length <= 0:
                        {
                            ConstructorBuilder builder = type.InheritStaticConstructorInit(constructor, out ILGenerator? generator);
                            members.Add(builder);
                            storage.Add(constructor, (builder, generator));
                            continue;
                        }
                    }
                }

                members.TrimExcess();
                return members.ToArray();
            }

            private static void Inherit(TypeBuilder type, Type @base, MemberInfo[] members, IEnumerable<KeyValuePair<MemberInfo, Object>> source)
            {
                if (type is null)
                {
                    throw new ArgumentNullException(nameof(type));
                }

                if (@base is null)
                {
                    throw new ArgumentNullException(nameof(@base));
                }

                if (members is null)
                {
                    throw new ArgumentNullException(nameof(members));
                }

                if (source is null)
                {
                    throw new ArgumentNullException(nameof(source));
                }

                foreach ((Object? member, Object? value) in source)
                {
                    switch (member)
                    {
                        case FieldInfo field:
                        {
                            type.InheritField(field, (FieldBuilder) value, @base, type);
                            continue;
                        }
                        case PropertyInfo property:
                        {
                            (PropertyBuilder builder, MethodBuilder get, MethodBuilder set) = (ValueTuple<PropertyBuilder, MethodBuilder, MethodBuilder>) value;
                            type.InheritProperty(property, builder, get, set, @base, type, members);
                            continue;
                        }
                        case EventInfo @event:
                        {
                            (EventBuilder builder, MethodBuilder raise, MethodBuilder add, MethodBuilder remove) = (ValueTuple<EventBuilder, MethodBuilder, MethodBuilder, MethodBuilder>) value;
                            type.InheritEvent(@event, builder, raise, add, remove, @base, type, members);
                            continue;
                        }
                        case MethodInfo method:
                        {
                            (MethodBuilder builder, ILGenerator generator) = (ValueTuple<MethodBuilder, ILGenerator>) value;
                            type.InheritMethod(method, builder, generator, @base, type, members);
                            continue;
                        }
                        case ConstructorInfo { IsStatic: true } constructor when constructor.DeclaringType == @base && constructor.GetSafeParameters()?.Length <= 0:
                        {
                            (ConstructorBuilder builder, ILGenerator generator) = (ValueTuple<ConstructorBuilder, ILGenerator>) value;
                            type.InheritStaticConstructor(constructor, builder, generator, @base, type, members);
                            continue;
                        }
                    }
                }
            }
        }
    }
}