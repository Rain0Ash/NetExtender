// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Reflection;
using System.Reflection.Emit;
using System.Runtime.CompilerServices;
using ReactiveUI;

namespace NetExtender.ReactiveUI.Utilities
{
    public static class ReactiveCodeGeneratorUtilities
    {
        private static MethodInfo RaiseAndSetIfChanged { get; }

        static ReactiveCodeGeneratorUtilities()
        {
            RaiseAndSetIfChanged = typeof(IReactiveObjectExtensions).GetMethod(nameof(RaiseAndSetIfChanged)) ?? throw new InvalidOperationException(nameof(RaiseAndSetIfChanged));
        }

        [MethodImpl(MethodImplOptions.AggressiveOptimization)]
        public static ILGenerator DefineReactiveSetMethod(this ILGenerator generator, FieldBuilder field, Type caller, String name, Type type)
        {
            if (generator is null)
            {
                throw new ArgumentNullException(nameof(generator));
            }

            if (field is null)
            {
                throw new ArgumentNullException(nameof(field));
            }

            if (caller is null)
            {
                throw new ArgumentNullException(nameof(caller));
            }

            if (String.IsNullOrEmpty(name))
            {
                throw new ArgumentException("Value cannot be null or empty.", nameof(name));
            }

            if (type is null)
            {
                throw new ArgumentNullException(nameof(type));
            }

            generator.Emit(OpCodes.Ldarg_0);
            generator.Emit(OpCodes.Ldarg_0);
            generator.Emit(OpCodes.Ldflda, field);
            generator.Emit(OpCodes.Ldarg_1);
            generator.Emit(OpCodes.Ldstr, name);
            generator.Emit(OpCodes.Call, RaiseAndSetIfChanged.MakeGenericMethod(caller, type));
            generator.Emit(OpCodes.Pop);
            generator.Emit(OpCodes.Ret);
            return generator;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static MethodBuilder DefineReactiveSetMethod(this MethodBuilder builder, FieldBuilder field, Type caller, String name, Type type)
        {
            if (builder is null)
            {
                throw new ArgumentNullException(nameof(builder));
            }

            if (field is null)
            {
                throw new ArgumentNullException(nameof(field));
            }

            if (caller is null)
            {
                throw new ArgumentNullException(nameof(caller));
            }

            if (String.IsNullOrEmpty(name))
            {
                throw new ArgumentException("Value cannot be null or empty.", nameof(name));
            }

            if (type is null)
            {
                throw new ArgumentNullException(nameof(type));
            }

            ILGenerator generator = builder.GetILGenerator();
            DefineReactiveSetMethod(generator, field, caller, name, type);
            return builder;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static MethodBuilder DefineReactiveSetMethod(this TypeBuilder builder, PropertyBuilder property, FieldBuilder field, Type caller, String name, Type type)
        {
            return DefineReactiveSetMethod(builder, property, field, caller, name, type, true);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static MethodBuilder DefineReactiveSetMethod(this TypeBuilder builder, PropertyBuilder property, FieldBuilder field, Type caller, String name, Type type, Boolean visible)
        {
            const MethodAttributes attributes = MethodAttributes.SpecialName | MethodAttributes.HideBySig;
            return DefineReactiveSetMethod(builder, property, field, caller, name, type, attributes | (visible ? MethodAttributes.Public : MethodAttributes.Private));
        }

        public static MethodBuilder DefineReactiveSetMethod(this TypeBuilder builder, PropertyBuilder property, FieldBuilder field, Type caller, String name, Type type, MethodAttributes attributes)
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

            if (caller is null)
            {
                throw new ArgumentNullException(nameof(caller));
            }

            if (String.IsNullOrEmpty(name))
            {
                throw new ArgumentException("Value cannot be null or empty.", nameof(name));
            }

            if (type is null)
            {
                throw new ArgumentNullException(nameof(type));
            }

            MethodBuilder accessor = builder.DefineMethod($"set_{name}", attributes, null, new[] { type }).DefineReactiveSetMethod(field, caller, name, type);
            property.SetSetMethod(accessor);
            return accessor;
        }
    }
}