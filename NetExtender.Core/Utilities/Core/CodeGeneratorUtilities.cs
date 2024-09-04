// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Reflection.Emit;
using System.Runtime.CompilerServices;

namespace NetExtender.Utilities.Core
{
    public static class CodeGeneratorUtilities
    {
        public static void EmitInstance(this ILGenerator generator, Type type)
        {
            if (generator is null)
            {
                throw new ArgumentNullException(nameof(generator));
            }

            if (type is null)
            {
                throw new ArgumentNullException(nameof(type));
            }

            generator.Emit(OpCodes.Ldarg_0);
            generator.EmitUnbox(type);
        }

        public static void EmitLdarg(this ILGenerator generator, Int32 position)
        {
            if (generator is null)
            {
                throw new ArgumentNullException(nameof(generator));
            }

            switch (position)
            {
                case 0:
                    generator.Emit(OpCodes.Ldarg_0);
                    return;
                case 1:
                    generator.Emit(OpCodes.Ldarg_1);
                    return;
                case 2:
                    generator.Emit(OpCodes.Ldarg_2);
                    return;
                case 3:
                    generator.Emit(OpCodes.Ldarg_3);
                    return;
                default:
                    generator.Emit((Byte) position == position ? OpCodes.Ldarg_S : OpCodes.Ldarg, position);
                    return;
            }
        }

        public static void EmitLdcI4(this ILGenerator generator, Int32 value)
        {
            if (generator is null)
            {
                throw new ArgumentNullException(nameof(generator));
            }

            switch (value)
            {
                case -1:
                    generator.Emit(OpCodes.Ldc_I4_M1);
                    return;
                case 0:
                    generator.Emit(OpCodes.Ldc_I4_0);
                    return;
                case 1:
                    generator.Emit(OpCodes.Ldc_I4_1);
                    return;
                case 2:
                    generator.Emit(OpCodes.Ldc_I4_2);
                    return;
                case 3:
                    generator.Emit(OpCodes.Ldc_I4_3);
                    return;
                case 4:
                    generator.Emit(OpCodes.Ldc_I4_4);
                    return;
                case 5:
                    generator.Emit(OpCodes.Ldc_I4_5);
                    return;
                case 6:
                    generator.Emit(OpCodes.Ldc_I4_6);
                    return;
                case 7:
                    generator.Emit(OpCodes.Ldc_I4_7);
                    return;
                case 8:
                    generator.Emit(OpCodes.Ldc_I4_8);
                    return;
                default:
                    generator.Emit(unchecked((SByte) value == value) ? OpCodes.Ldc_I4_S : OpCodes.Ldc_I4, value);
                    return;
            }
        }

        public static void EmitBox(this ILGenerator generator, Type type)
        {
            if (generator is null)
            {
                throw new ArgumentNullException(nameof(generator));
            }

            if (type is null)
            {
                throw new ArgumentNullException(nameof(type));
            }

            if (type.IsGenericParameter)
            {
                generator.Emit(OpCodes.Box, type);
                return;
            }

            if (type.IsByRef || type.IsPointer)
            {
                generator.Emit(OpCodes.Box, typeof(nint));
                return;
            }

            if (type.IsValueType)
            {
                generator.Emit(OpCodes.Box, type);
            }
        }

        public static void EmitUnbox(this ILGenerator generator, Type type)
        {
            if (generator is null)
            {
                throw new ArgumentNullException(nameof(generator));
            }

            if (type is null)
            {
                throw new ArgumentNullException(nameof(type));
            }

            if (type.IsGenericParameter)
            {
                generator.Emit(OpCodes.Unbox_Any, type);
                return;
            }

            if (type.IsByRef || type.IsPointer)
            {
                generator.Emit(OpCodes.Unbox_Any, typeof(nint));
                return;
            }

            if (type.IsValueType)
            {
                generator.Emit(OpCodes.Unbox_Any, type);
                return;
            }

            if (type != typeof(Object))
            {
                generator.Emit(OpCodes.Castclass, type);
            }
        }

        public static void Return(this ILGenerator generator)
        {
            if (generator is null)
            {
                throw new ArgumentNullException(nameof(generator));
            }

            generator.Emit(OpCodes.Ret);
        }

        public static ConstructorBuilder DefineConstructor(this TypeBuilder builder)
        {
            return DefineConstructor(builder, MethodAttributes.Public | MethodAttributes.HideBySig);
        }

        public static ConstructorBuilder DefineConstructor(this TypeBuilder builder, MethodAttributes attributes)
        {
            if (builder is null)
            {
                throw new ArgumentNullException(nameof(builder));
            }

            return builder.DefineDefaultConstructor(attributes);
        }

        public static ConstructorBuilder DefineConstructor(this TypeBuilder builder, KeyValuePair<String, FieldBuilder>[] parameters)
        {
            return DefineConstructor(builder, (Type?) null, parameters);
        }

        public static ConstructorBuilder DefineConstructor(this TypeBuilder builder, Type? parent, KeyValuePair<String, FieldBuilder>[] parameters)
        {
            return DefineConstructor(builder, parent, parameters, MethodAttributes.Public | MethodAttributes.HideBySig);
        }

        public static ConstructorBuilder DefineConstructor(this TypeBuilder builder, ConstructorInfo parent, KeyValuePair<String, FieldBuilder>[] parameters)
        {
            return DefineConstructor(builder, parent, parameters, MethodAttributes.Public | MethodAttributes.HideBySig);
        }

        public static ConstructorBuilder DefineConstructor(this TypeBuilder builder, KeyValuePair<String, FieldBuilder>[] parameters, MethodAttributes attributes)
        {
            return DefineConstructor(builder, (Type?) null, parameters, attributes);
        }

        public static ConstructorBuilder DefineConstructor(this TypeBuilder builder, Type? parent, KeyValuePair<String, FieldBuilder>[] parameters, MethodAttributes attributes)
        {
            if (builder is null)
            {
                throw new ArgumentNullException(nameof(builder));
            }

            parent ??= builder.BaseType;

            if (parent is null || parent == typeof(Object))
            {
                return DefineConstructor(builder, Generator.Object.Constructor, parameters, attributes);
            }

            const BindingFlags binding = BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic;
            ConstructorInfo constructor = parent.GetConstructor(binding, Type.EmptyTypes) ?? throw new MissingMethodException(parent.FullName, "Constructor");
            return DefineConstructor(builder, constructor, parameters, attributes);
        }

        public static ConstructorBuilder DefineConstructor(this TypeBuilder builder, ConstructorInfo parent, KeyValuePair<String, FieldBuilder>[] parameters, MethodAttributes attributes)
        {
            if (builder is null)
            {
                throw new ArgumentNullException(nameof(builder));
            }

            if (parameters is null)
            {
                throw new ArgumentNullException(nameof(parameters));
            }

            if (parameters.Length <= 0)
            {
                return DefineConstructor(builder, attributes);
            }

            Type[] types = parameters.Select(field => field.Value.FieldType).ToArray();
            ConstructorBuilder constructor = builder.DefineConstructor(attributes, CallingConventions.HasThis, types);

            ILGenerator generator = constructor.GetILGenerator();
            generator.Emit(OpCodes.Ldarg_0);
            generator.Emit(OpCodes.Call, Generator.Object.Constructor);

            for (Int32 i = 0; i < parameters.Length; i++)
            {
                (String name, FieldBuilder field) = parameters[i];

                Int32 id = i + 1;
                constructor.DefineParameter(id, ParameterAttributes.None, name);

                generator.Emit(OpCodes.Ldarg_0);
                generator.EmitLdarg(id);
                generator.Emit(OpCodes.Stfld, field);
            }

            generator.Emit(OpCodes.Ret);

            return constructor;
        }

        [MethodImpl(MethodImplOptions.AggressiveOptimization)]
        public static ILGenerator DefineGetMethod(this ILGenerator generator, FieldBuilder field)
        {
            if (generator is null)
            {
                throw new ArgumentNullException(nameof(generator));
            }

            if (field is null)
            {
                throw new ArgumentNullException(nameof(field));
            }

            generator.Emit(OpCodes.Ldarg_0);
            generator.Emit(OpCodes.Ldfld, field);
            generator.Emit(OpCodes.Ret);
            return generator;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static MethodBuilder DefineGetMethod(this MethodBuilder builder, FieldBuilder field)
        {
            if (builder is null)
            {
                throw new ArgumentNullException(nameof(builder));
            }

            if (field is null)
            {
                throw new ArgumentNullException(nameof(field));
            }

            ILGenerator generator = builder.GetILGenerator();
            DefineGetMethod(generator, field);
            return builder;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static MethodBuilder DefineGetMethod(this TypeBuilder builder, PropertyBuilder property, FieldBuilder field, String name, Type type)
        {
            return DefineGetMethod(builder, property, field, name, type, true);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static MethodBuilder DefineGetMethod(this TypeBuilder builder, PropertyBuilder property, FieldBuilder field, String name, Type type, Boolean visible)
        {
            const MethodAttributes attributes = MethodAttributes.SpecialName | MethodAttributes.HideBySig;
            return DefineGetMethod(builder, property, field, name, type, attributes | (visible ? MethodAttributes.Public : MethodAttributes.Private));
        }

        public static MethodBuilder DefineGetMethod(this TypeBuilder builder, PropertyBuilder property, FieldBuilder field, String name, Type type, MethodAttributes attributes)
        {
            if (builder is null)
            {
                throw new ArgumentNullException(nameof(builder));
            }

            if (property is null)
            {
                throw new ArgumentNullException(nameof(property));
            }

            if (field is null)
            {
                throw new ArgumentNullException(nameof(field));
            }

            if (String.IsNullOrEmpty(name))
            {
                throw new ArgumentException("Value cannot be null or empty.", nameof(name));
            }

            if (type is null)
            {
                throw new ArgumentNullException(nameof(type));
            }

            MethodBuilder accessor = builder.DefineMethod($"get_{name}", attributes, type, Type.EmptyTypes).DefineGetMethod(field);
            property.SetGetMethod(accessor);
            return accessor;
        }

        [MethodImpl(MethodImplOptions.AggressiveOptimization)]
        public static ILGenerator DefineSetMethod(this ILGenerator generator, FieldBuilder field)
        {
            if (generator is null)
            {
                throw new ArgumentNullException(nameof(generator));
            }

            if (field is null)
            {
                throw new ArgumentNullException(nameof(field));
            }

            generator.Emit(OpCodes.Ldarg_0);
            generator.Emit(OpCodes.Ldarg_1);
            generator.Emit(OpCodes.Stfld, field);
            generator.Emit(OpCodes.Ret);
            return generator;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static MethodBuilder DefineSetMethod(this MethodBuilder builder, FieldBuilder field)
        {
            if (builder is null)
            {
                throw new ArgumentNullException(nameof(builder));
            }

            if (field is null)
            {
                throw new ArgumentNullException(nameof(field));
            }

            ILGenerator generator = builder.GetILGenerator();
            DefineSetMethod(generator, field);
            return builder;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static MethodBuilder DefineSetMethod(this TypeBuilder builder, PropertyBuilder property, FieldBuilder field, String name, Type type)
        {
            return DefineSetMethod(builder, property, field, name, type, true);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static MethodBuilder DefineSetMethod(this TypeBuilder builder, PropertyBuilder property, FieldBuilder field, String name, Type type, Boolean visible)
        {
            const MethodAttributes attributes = MethodAttributes.SpecialName | MethodAttributes.HideBySig;
            return DefineSetMethod(builder, property, field, name, type, attributes | (visible ? MethodAttributes.Public : MethodAttributes.Private));
        }

        public static MethodBuilder DefineSetMethod(this TypeBuilder builder, PropertyBuilder property, FieldBuilder field, String name, Type type, MethodAttributes attributes)
        {
            if (builder is null)
            {
                throw new ArgumentNullException(nameof(builder));
            }

            if (property is null)
            {
                throw new ArgumentNullException(nameof(property));
            }

            if (field is null)
            {
                throw new ArgumentNullException(nameof(field));
            }

            if (String.IsNullOrEmpty(name))
            {
                throw new ArgumentException("Value cannot be null or empty.", nameof(name));
            }

            if (type is null)
            {
                throw new ArgumentNullException(nameof(type));
            }

            MethodBuilder accessor = builder.DefineMethod($"set_{name}", attributes, null, new[] { type }).DefineSetMethod(field);
            property.SetSetMethod(accessor);
            return accessor;
        }

        public static class Generator
        {
            public static class Attribute
            {
                public static CustomAttributeBuilder CompilerGeneratedAttribute { get; } = GetCompilerGeneratedAttribute();
                public static CustomAttributeBuilder DebuggerBrowsableAttribute { get; } = GetDebuggerBrowsableAttribute();
                public static CustomAttributeBuilder DebuggerHiddenAttribute { get; } = GetDebuggerHiddenAttribute();

                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                private static CustomAttributeBuilder GetCompilerGeneratedAttribute()
                {
                    Type type = typeof(CompilerGeneratedAttribute);
                    ConstructorInfo constructor = type.GetConstructor(Type.EmptyTypes) ?? throw new MissingMethodException(type.FullName, "Constructor");
                    return new CustomAttributeBuilder(constructor, Array.Empty<System.Object>());
                }

                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                private static CustomAttributeBuilder GetDebuggerBrowsableAttribute()
                {
                    Type type = typeof(DebuggerBrowsableAttribute);
                    ConstructorInfo constructor = type.GetConstructor(new[] { typeof(DebuggerBrowsableState) }) ?? throw new MissingMethodException(type.FullName, "Constructor");
                    return new CustomAttributeBuilder(constructor, new System.Object[] { DebuggerBrowsableState.Never });
                }

                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                private static CustomAttributeBuilder GetDebuggerHiddenAttribute()
                {
                    Type type = typeof(DebuggerHiddenAttribute);
                    ConstructorInfo constructor = type.GetConstructor(Type.EmptyTypes) ?? throw new MissingMethodException(type.FullName, "Constructor");
                    return new CustomAttributeBuilder(constructor, Array.Empty<System.Object>());
                }
            }

            public static class Object
            {
                public static ConstructorInfo Constructor { get; }
                public static MethodInfo GetHashCodeMethod { get; }
                public static MethodInfo EqualsMethod { get; }
                public static MethodInfo ToStringMethod { get; }

                static Object()
                {
                    const BindingFlags binding = BindingFlags.Instance | BindingFlags.Public;

                    Type type = typeof(System.Object);
                    Constructor = type.GetConstructor(Type.EmptyTypes) ?? throw new MissingMethodException(nameof(Constructor));
                    GetHashCodeMethod = type.GetMethod(nameof(GetHashCode), binding, Type.EmptyTypes) ?? throw new MissingMethodException(type.FullName, nameof(GetHashCode));
                    EqualsMethod = type.GetMethod(nameof(Equals), binding, new []{ type }) ?? throw new MissingMethodException(type.FullName, nameof(Equals));
                    ToStringMethod = type.GetMethod(nameof(ToString), binding, Type.EmptyTypes) ?? throw new MissingMethodException(type.FullName, nameof(ToString));
                }
            }

            public static class Equality
            {
                public static MethodInfo Default { get; }
                public static MethodInfo GetHashCodeMethod { get; }
                public static MethodInfo EqualsMethod { get; }

                static Equality()
                {
                    Default = FindDefault();
                    (GetHashCodeMethod, EqualsMethod) = FindMethods();
                }

                private static MethodInfo FindDefault()
                {
                    const BindingFlags binding = BindingFlags.Static | BindingFlags.Public;
                    const String method = "Default";

                    Type type = typeof(EqualityComparer<>);
                    return type.GetProperty(method, binding)?.GetMethod ?? throw new MissingMemberException(type.FullName, nameof(Default));
                }

                private static (MethodInfo GetHashCodeMethod, MethodInfo EqualsMethod) FindMethods()
                {
                    const BindingFlags binding = BindingFlags.Instance | BindingFlags.Public;

                    Type type = typeof(EqualityComparer<>);
                    Type generic = type.GetGenericArguments()[0];
                    MethodInfo hash = type.GetMethod(nameof(GetHashCode), binding, new[] { generic }) ?? throw new MissingMethodException(type.FullName, nameof(GetHashCode));
                    MethodInfo equals = type.GetMethod(nameof(Equals), binding, new[] { generic, generic }) ?? throw new MissingMethodException(type.FullName, nameof(Equals));
                    return (hash, equals);
                }

                public static MethodInfo TakeDefault(Type type)
                {
                    if (type is null)
                    {
                        throw new ArgumentNullException(nameof(type));
                    }

                    const BindingFlags binding = BindingFlags.Static | BindingFlags.Public;
                    const String method = "Default";

                    Type comparer = typeof(EqualityComparer<>).MakeGenericType(type);
                    return comparer.GetProperty(method, binding)?.GetMethod ?? throw new MissingMemberException(type.FullName, method);
                }

                public static MethodInfo TakeEquals(Type type)
                {
                    if (type is null)
                    {
                        throw new ArgumentNullException(nameof(type));
                    }

                    const BindingFlags binding = BindingFlags.Instance | BindingFlags.Public;

                    Type comparer = typeof(EqualityComparer<>).MakeGenericType(type);
                    return comparer.GetMethod(nameof(Equals), binding, new[] { type, type }) ?? throw new MissingMethodException(type.FullName, nameof(Equals));
                }
            }

            public static class Equatable
            {
                public static Type Interface { get; }
                public static MethodInfo EqualsMethod { get; }

                static Equatable()
                {
                    const BindingFlags binding = BindingFlags.Instance | BindingFlags.Public;

                    Interface = typeof(IEquatable<>);
                    Type generic = Interface.GetGenericArguments()[0];
                    EqualsMethod = Interface.GetMethod(nameof(Equals), binding, new[] { generic }) ?? throw new MissingMethodException(Interface.FullName, nameof(Equals));
                }
            }

            public static class StringBuilder
            {
                public static ConstructorInfo Constructor { get; }
                private static ImmutableDictionary<Type, MethodInfo> Storage { get; }

                public static MethodInfo String
                {
                    get
                    {
                        Take(typeof(String), out MethodInfo result);
                        return result;
                    }
                }

                // ReSharper disable once MemberHidesStaticFromOuterClass
                public static MethodInfo Object
                {
                    get
                    {
                        Take(typeof(Object), out MethodInfo result);
                        return result;
                    }
                }

                static StringBuilder()
                {
                    Constructor = typeof(System.Text.StringBuilder).GetConstructor(Type.EmptyTypes) ?? throw new InvalidOperationException(nameof(Constructor));
                    Storage = new[]
                    {
                        typeof(Boolean), typeof(Char),
                        typeof(SByte), typeof(Byte),
                        typeof(Int16), typeof(UInt16),
                        typeof(Int32), typeof(UInt32),
                        typeof(Int64), typeof(UInt64),
                        typeof(Single), typeof(Double), typeof(Decimal),
                        typeof(String), typeof(System.Text.StringBuilder), typeof(Char[]),
                        typeof(System.Object)
                    }.ToImmutableDictionary(type => type, Find);
                }

                private static MethodInfo Find<T>()
                {
                    return Find(typeof(T));
                }

                private static MethodInfo Find(Type type)
                {
                    if (type is null)
                    {
                        throw new ArgumentNullException(nameof(type));
                    }

                    const BindingFlags binding = BindingFlags.Instance | BindingFlags.Public;
                    return typeof(System.Text.StringBuilder).GetMethod(nameof(System.Text.StringBuilder.Append), binding, new[] { type }) ??
                           throw new MissingMethodException(type.FullName, nameof(System.Text.StringBuilder.Append));
                }

                public static Boolean Take(Type type, out MethodInfo result)
                {
                    if (type is null)
                    {
                        throw new ArgumentNullException(nameof(type));
                    }

                    if (Storage.TryGetValue(type, out MethodInfo? method))
                    {
                        result = method;
                        return true;
                    }

                    if (!Take(typeof(System.Object), out method))
                    {
                        throw new InvalidOperationException();
                    }

                    result = method;
                    return false;
                }
            }
        }
    }
}