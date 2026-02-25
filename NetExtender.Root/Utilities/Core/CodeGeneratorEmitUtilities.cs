// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Reflection.Emit;
using System.Runtime.CompilerServices;

namespace NetExtender.Utilities.Core
{
    public static class CodeGeneratorEmitUtilities
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Int32 Id(this Label label)
        {
            return label.GetHashCode();
        }

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

        public static void EmitDefault(this ILGenerator generator, Type type)
        {
            if (generator is null)
            {
                throw new ArgumentNullException(nameof(generator));
            }

            if (type is null)
            {
                throw new ArgumentNullException(nameof(type));
            }

            if (!type.IsValueType)
            {
                generator.Emit(OpCodes.Ldnull);
                return;
            }

            LocalBuilder local = generator.DeclareLocal(type);
            generator.Emit(OpCodes.Ldloca, local);
            generator.Emit(OpCodes.Initobj, type);
            generator.Emit(OpCodes.Ldloc, local);
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
                generator.Emit(OpCodes.Box, typeof(IntPtr));
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
                generator.Emit(OpCodes.Unbox_Any, typeof(IntPtr));
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

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void Return(this ILGenerator generator)
        {
            if (generator is null)
            {
                throw new ArgumentNullException(nameof(generator));
            }

            generator.Emit(OpCodes.Ret);
        }
    }
}