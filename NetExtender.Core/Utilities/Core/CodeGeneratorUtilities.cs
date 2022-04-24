// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Reflection.Emit;

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

        // ReSharper disable once CognitiveComplexity
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
                    break;
                case 0:
                    generator.Emit(OpCodes.Ldc_I4_0);
                    break;
                case 1:
                    generator.Emit(OpCodes.Ldc_I4_1);
                    break;
                case 2:
                    generator.Emit(OpCodes.Ldc_I4_2);
                    break;
                case 3:
                    generator.Emit(OpCodes.Ldc_I4_3);
                    break;
                case 4:
                    generator.Emit(OpCodes.Ldc_I4_4);
                    break;
                case 5:
                    generator.Emit(OpCodes.Ldc_I4_5);
                    break;
                case 6:
                    generator.Emit(OpCodes.Ldc_I4_6);
                    break;
                case 7:
                    generator.Emit(OpCodes.Ldc_I4_7);
                    break;
                case 8:
                    generator.Emit(OpCodes.Ldc_I4_8);
                    break;
                default:
                    generator.Emit((SByte) value == value ? OpCodes.Ldc_I4_S : OpCodes.Ldc_I4, value);
                    break;
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
    }
}