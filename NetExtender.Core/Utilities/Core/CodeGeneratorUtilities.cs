// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Reflection;
using System.Reflection.Emit;
using System.Runtime.CompilerServices;

namespace NetExtender.Utilities.Core
{
    public static class CodeGeneratorUtilities
    {
        public static void EmitLdarg(this ILGenerator generator, Int32 position)
        {
            if (generator is null)
            {
                throw new ArgumentNullException(nameof(generator));
            }

            switch (position)
            {
                case 0: generator.Emit(OpCodes.Ldarg_0); break;
                case 1: generator.Emit(OpCodes.Ldarg_1); break;
                case 2: generator.Emit(OpCodes.Ldarg_2); break;
                case 3: generator.Emit(OpCodes.Ldarg_3); break;
                default:
                    generator.Emit((Byte)position == position ?
                        OpCodes.Ldarg_S : OpCodes.Ldarg, position);
                    break;
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
    }
}