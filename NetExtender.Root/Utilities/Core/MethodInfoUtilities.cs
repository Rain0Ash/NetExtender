using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Reflection;
using System.Reflection.Emit;
using System.Runtime.CompilerServices;
using NetExtender.Types.Reflection;
using NetExtender.Utilities.Numerics;
using NetExtender.Utilities.Types;

namespace NetExtender.Utilities.Core
{
    public static class MethodInfoUtilities
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean HasImplementation(this MethodInfo method)
        {
            return HasImplementation(method, out Boolean? _);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean HasImplementation(this MethodInfo method, out Boolean @virtual)
        {
            Boolean result = HasImplementation(method, out Boolean? state);
            @virtual = state is true;
            return result;
        }

        [MethodImpl(MethodImplOptions.AggressiveOptimization)]
        public static Boolean HasImplementation(this MethodInfo method, out Boolean? @virtual)
        {
            if (method is null)
            {
                throw new ArgumentNullException(nameof(method));
            }

            if (method.IsAbstract)
            {
                @virtual = true;
                return false;
            }

            @virtual = method switch
            {
                { IsVirtual: true } => true,
                { IsStatic: true } => null,
                _ => false
            };

            if (method.GetMethodBody() is not { } body)
            {
                return false;
            }

            if (body.GetILAsByteArray() is not { } array)
            {
                return false;
            }

            ReadOnlySpan<OpByte> code = array.AsSpan().As<Byte, OpByte>();

            Int32 nop = 0;
            while (code[nop] == OpCodes.Nop)
            {
                nop++;
            }

            return nop < code.Length && HasImplementation(method, code.Slice(nop));
        }

        // ReSharper disable once CognitiveComplexity
        [MethodImpl(MethodImplOptions.AggressiveOptimization)]
        private static Boolean HasImplementation(this MethodInfo method, ReadOnlySpan<OpByte> code)
        {
            if (method is null)
            {
                throw new ArgumentNullException(nameof(method));
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            static Boolean IsThrow(ReadOnlySpan<OpByte> code, Int32 i)
            {
                return code.Length <= i + 6 && code[i++] == OpCodes.Newobj && code[i + sizeof(Int32)] == OpCodes.Throw;
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            static Boolean IsNull(ReadOnlySpan<OpByte> code, Int32 i)
            {
                if (code.Length <= i + 2)
                {
                    return code[i] == OpCodes.Ldnull && code[i + 1] == OpCodes.Ret;
                }

                return code.Length <= i + 6 && code[i++] == OpCodes.Ldnull && code[i++] == OpCodes.Stloc_0 && code[i++] == OpCodes.Br_S && code[++i] == OpCodes.Ldloc_0 && code[++i] == OpCodes.Ret;
            }

            // ReSharper disable once LocalFunctionHidesMethod
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            static Boolean IsVoid(ReadOnlySpan<OpByte> code, Int32 i)
            {
                return i + 1 == code.Length && code[i] == OpCodes.Ret;
            }

            // ReSharper disable once LocalFunctionHidesMethod
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            static Boolean IsDefault(ReadOnlySpan<OpByte> code, Int32 i)
            {
                return Read<Int32>(code, ref i) != 0 && code[i++] == OpCodes.Ldloc_0 && code[i++] == OpCodes.Stloc_1 && code[i++] == OpCodes.Br_S && code[++i] == OpCodes.Ldloc_1 && code[++i] == OpCodes.Ret;
            }

            // ReSharper disable once LocalFunctionHidesMethod
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            static Boolean IsField(ReadOnlySpan<OpByte> code, Int32 i)
            {
                return Read<Int32>(code, ref i) != 0 && code[i++] == OpCodes.Stloc_0 && code[i++] == OpCodes.Br_S && code[++i] == OpCodes.Ldloc_0 && code[++i] == OpCodes.Ret;
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            static Boolean IsInt32(ReadOnlySpan<OpByte> code, Int32 i)
            {
                return code[i++] == OpCodes.Stloc_0 && code[i++] == OpCodes.Br_S && code[++i] == OpCodes.Ldloc_0 && code[++i] == OpCodes.Ret;
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            static Boolean IsInt64(ReadOnlySpan<OpByte> code, Int32 i)
            {
                return code[i++] == OpCodes.Conv_I8 && code[i++] == OpCodes.Stloc_0 && code[i++] == OpCodes.Br_S && code[++i] == OpCodes.Ldloc_0 && code[++i] == OpCodes.Ret;
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            static unsafe T Read<T>(ReadOnlySpan<OpByte> code, ref Int32 start) where T : unmanaged
            {
                T value = code.Slice(start, sizeof(T)).As<OpByte, Byte>().Read<T>();
                start += sizeof(T);
                return value;
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            static T ReadNext<T>(ReadOnlySpan<OpByte> code, ref Int32 start) where T : unmanaged
            {
                ++start;
                return Read<T>(code, ref start);
            }

            Int32 i = 0;
            if (IsThrow(code, i))
            {
                return false;
            }

            if (!method.ReturnType.IsValueType && IsNull(code, i))
            {
                return false;
            }

            if (method.ReturnType.IsVoid())
            {
                return !IsVoid(code, i);
            }

            if (!method.ReturnType.IsValueType)
            {
                return true;
            }

            i = 0;
            if ((code[i++] == OpCodes.Ldc_I4_0 || code[i = 0] == OpCodes.Ldc_I4 && ReadNext<Int32>(code, ref i) == 0) && (IsInt32(code, i) || IsInt64(code, i)))
            {
                return false;
            }

            if (code[i = 0] == OpCodes.Ldc_R4 && ReadNext<Single>(code, ref i) == 0 && code[i++] == OpCodes.Stloc_0 && code[i++] == OpCodes.Br_S && code[++i] == OpCodes.Ldloc_0 && code[++i] == OpCodes.Ret)
            {
                return false;
            }

            if (code[i = 0] == OpCodes.Ldc_R8 && ReadNext<Double>(code, ref i) == 0 && code[i++] == OpCodes.Stloc_0 && code[i++] == OpCodes.Br_S && code[++i] == OpCodes.Ldloc_0 && code[++i] == OpCodes.Ret)
            {
                return false;
            }

            if (code[i = 0] == OpCodes.Ldloca_S && code[i += 2] == unchecked((UInt16) OpCodes.Initobj.Value).High() && code[++i] == OpCodes.Initobj.Value.Low())
            {
                return !IsDefault(code, ++i);
            }

            if (code[i = 0] == OpCodes.Ldsfld)
            {
                return !IsField(code, ++i);
            }

            return true;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static MethodInfo? GetMethod(this Type type, String name, ParameterInfo[] parameters)
        {
            if (type is null)
            {
                throw new ArgumentNullException(nameof(type));
            }

            if (name is null)
            {
                throw new ArgumentNullException(nameof(name));
            }

            if (parameters is null)
            {
                throw new ArgumentNullException(nameof(parameters));
            }

            return type.GetMethod(name, parameters.Types());
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static MethodInfo? GetMethod(this Type type, String name, ParameterInfo[] parameters, ParameterModifier[]? modifiers)
        {
            if (type is null)
            {
                throw new ArgumentNullException(nameof(type));
            }

            if (name is null)
            {
                throw new ArgumentNullException(nameof(name));
            }

            if (parameters is null)
            {
                throw new ArgumentNullException(nameof(parameters));
            }

            return type.GetMethod(name, parameters.Types(), modifiers);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static MethodInfo? GetMethod(this Type type, String name, BindingFlags bindingAttr, ParameterInfo[] parameters)
        {
            if (type is null)
            {
                throw new ArgumentNullException(nameof(type));
            }

            if (name is null)
            {
                throw new ArgumentNullException(nameof(name));
            }

            if (parameters is null)
            {
                throw new ArgumentNullException(nameof(parameters));
            }

            return type.GetMethod(name, bindingAttr, parameters.Types());
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static MethodInfo? GetMethod(this Type type, String name, BindingFlags bindingAttr, Type[] parameters, ParameterModifier[]? modifiers)
        {
            if (type is null)
            {
                throw new ArgumentNullException(nameof(type));
            }

            if (name is null)
            {
                throw new ArgumentNullException(nameof(name));
            }

            if (parameters is null)
            {
                throw new ArgumentNullException(nameof(parameters));
            }

            return type.GetMethod(name, bindingAttr, null, parameters, modifiers);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static MethodInfo? GetMethod(this Type type, String name, BindingFlags bindingAttr, ParameterInfo[] parameters, ParameterModifier[]? modifiers)
        {
            return GetMethod(type, name, bindingAttr, null, parameters, modifiers);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static MethodInfo? GetMethod(this Type type, String name, BindingFlags bindingAttr, Binder? binder, ParameterInfo[] parameters, ParameterModifier[]? modifiers)
        {
            if (type is null)
            {
                throw new ArgumentNullException(nameof(type));
            }

            if (name is null)
            {
                throw new ArgumentNullException(nameof(name));
            }

            if (parameters is null)
            {
                throw new ArgumentNullException(nameof(parameters));
            }

            return type.GetMethod(name, bindingAttr, binder, parameters.Types(), modifiers);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static MethodInfo? GetMethod(this Type type, String name, BindingFlags bindingAttr, Binder? binder, CallingConventions callConvention, ParameterInfo[] parameters, ParameterModifier[]? modifiers)
        {
            if (type is null)
            {
                throw new ArgumentNullException(nameof(type));
            }

            if (name is null)
            {
                throw new ArgumentNullException(nameof(name));
            }

            if (parameters is null)
            {
                throw new ArgumentNullException(nameof(parameters));
            }

            return type.GetMethod(name, bindingAttr, binder, callConvention, parameters.Types(), modifiers);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static MethodInfo? GetMethod(this Type type, String name, Int32 genericParameterCount, ParameterInfo[] parameters)
        {
            if (type is null)
            {
                throw new ArgumentNullException(nameof(type));
            }

            if (name is null)
            {
                throw new ArgumentNullException(nameof(name));
            }

            if (parameters is null)
            {
                throw new ArgumentNullException(nameof(parameters));
            }

            return type.GetMethod(name, genericParameterCount, parameters.Types());
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static MethodInfo? GetMethod(this Type type, String name, Int32 genericParameterCount, ParameterInfo[] parameters, ParameterModifier[]? modifiers)
        {
            if (type is null)
            {
                throw new ArgumentNullException(nameof(type));
            }

            if (name is null)
            {
                throw new ArgumentNullException(nameof(name));
            }

            if (parameters is null)
            {
                throw new ArgumentNullException(nameof(parameters));
            }

            return type.GetMethod(name, genericParameterCount, parameters.Types(), modifiers);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static MethodInfo? GetMethod(this Type type, String name, Int32 genericParameterCount, BindingFlags bindingAttr, Type[] types)
        {
            if (type is null)
            {
                throw new ArgumentNullException(nameof(type));
            }

            if (name is null)
            {
                throw new ArgumentNullException(nameof(name));
            }

            if (types is null)
            {
                throw new ArgumentNullException(nameof(types));
            }

            return type.GetMethod(name, genericParameterCount, bindingAttr, null, types, null);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static MethodInfo? GetMethod(this Type type, String name, Int32 genericParameterCount, BindingFlags bindingAttr, ParameterInfo[] parameters)
        {
            if (type is null)
            {
                throw new ArgumentNullException(nameof(type));
            }

            if (name is null)
            {
                throw new ArgumentNullException(nameof(name));
            }

            if (parameters is null)
            {
                throw new ArgumentNullException(nameof(parameters));
            }

            return GetMethod(type, name, genericParameterCount, bindingAttr, parameters.Types());
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static MethodInfo? GetMethod(this Type type, String name, Int32 genericParameterCount, BindingFlags bindingAttr, Type[] types, ParameterModifier[]? modifiers)
        {
            if (type is null)
            {
                throw new ArgumentNullException(nameof(type));
            }

            if (name is null)
            {
                throw new ArgumentNullException(nameof(name));
            }

            if (types is null)
            {
                throw new ArgumentNullException(nameof(types));
            }

            return type.GetMethod(name, genericParameterCount, bindingAttr, null, types, modifiers);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static MethodInfo? GetMethod(this Type type, String name, Int32 genericParameterCount, BindingFlags bindingAttr, ParameterInfo[] parameters, ParameterModifier[]? modifiers)
        {
            if (type is null)
            {
                throw new ArgumentNullException(nameof(type));
            }

            if (name is null)
            {
                throw new ArgumentNullException(nameof(name));
            }

            if (parameters is null)
            {
                throw new ArgumentNullException(nameof(parameters));
            }

            return GetMethod(type, name, genericParameterCount, bindingAttr, parameters.Types(), modifiers);
        }

        [MethodImpl(MethodImplOptions.AggressiveOptimization)]
        [SuppressMessage("ReSharper", "CognitiveComplexity")]
        private static IEnumerable<MethodInfo> Filter(this MethodInfo[] methods, String name, Type[] generics, Type[] types)
        {
            if (methods is null)
            {
                throw new ArgumentNullException(nameof(methods));
            }

            if (name is null)
            {
                throw new ArgumentNullException(nameof(name));
            }

            if (generics is null)
            {
                throw new ArgumentNullException(nameof(generics));
            }

            if (types is null)
            {
                throw new ArgumentNullException(nameof(types));
            }

            foreach (MethodInfo method in methods)
            {
                if (method.Name != name || !method.IsGenericMethod)
                {
                    continue;
                }

                if (method.GetGenericArguments().Length != generics.Length)
                {
                    continue;
                }

                MethodInfo generic = method.MakeGenericMethod(generics);
                if (generic.GetSafeParameters() is not { } parameters || parameters.Length != types.Length)
                {
                    continue;
                }

                Boolean match = true;
                // ReSharper disable once LoopCanBeConvertedToQuery
                for (Int32 i = 0; i < parameters.Length; i++)
                {
                    if (parameters[i].ParameterType == types[i])
                    {
                        continue;
                    }

                    match = false;
                    break;
                }

                if (match)
                {
                    yield return generic;
                }
            }
        }

        public static MethodInfo? GetMethod(this Type type, String name, Type[] generics, Type[] types)
        {
            if (type is null)
            {
                throw new ArgumentNullException(nameof(type));
            }

            if (name is null)
            {
                throw new ArgumentNullException(nameof(name));
            }

            if (generics is null)
            {
                throw new ArgumentNullException(nameof(generics));
            }

            if (types is null)
            {
                throw new ArgumentNullException(nameof(types));
            }

            MethodInfo[] array = type.GetMethods().Filter(name, generics, types).ToArray();

            return array.Length switch
            {
                <= 0 => null,
                1 => array[0],
                _ => throw new AmbiguousMatchException()
            };
        }

        public static MethodInfo? GetMethod(this Type type, String name, Type[] generics, ParameterInfo[] parameters)
        {
            if (type is null)
            {
                throw new ArgumentNullException(nameof(type));
            }

            if (name is null)
            {
                throw new ArgumentNullException(nameof(name));
            }

            if (generics is null)
            {
                throw new ArgumentNullException(nameof(generics));
            }

            if (parameters is null)
            {
                throw new ArgumentNullException(nameof(parameters));
            }

            return GetMethod(type, name, generics, parameters.Types());
        }

        public static MethodInfo? GetMethod(this Type type, String name, BindingFlags bindingAttr, Type[] generics, Type[] types)
        {
            if (type is null)
            {
                throw new ArgumentNullException(nameof(type));
            }

            if (name is null)
            {
                throw new ArgumentNullException(nameof(name));
            }

            if (generics is null)
            {
                throw new ArgumentNullException(nameof(generics));
            }

            if (types is null)
            {
                throw new ArgumentNullException(nameof(types));
            }

            MethodInfo[] array = type.GetMethods(bindingAttr).Filter(name, generics, types).ToArray();

            return array.Length switch
            {
                <= 0 => null,
                1 => array[0],
                _ => throw new AmbiguousMatchException()
            };
        }

        public static MethodInfo? GetMethod(this Type type, String name, BindingFlags bindingAttr, Type[] generics, ParameterInfo[] parameters)
        {
            if (type is null)
            {
                throw new ArgumentNullException(nameof(type));
            }

            if (name is null)
            {
                throw new ArgumentNullException(nameof(name));
            }

            if (generics is null)
            {
                throw new ArgumentNullException(nameof(generics));
            }

            if (parameters is null)
            {
                throw new ArgumentNullException(nameof(parameters));
            }

            return GetMethod(type, name, bindingAttr, generics, parameters.Types());
        }

        public static MethodInfo GetParentDefinition(this MethodInfo method)
        {
            if (method is null)
            {
                throw new ArgumentNullException(nameof(method));
            }

            if (!method.IsVirtual)
            {
                return method;
            }

            Type? parent = method.DeclaringType?.BaseType;
            MethodInfo @base = method.GetBaseDefinition();

            if (method == @base)
            {
                return method;
            }

            while (parent is not null)
            {
                const BindingFlags binding = BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic;
                foreach (MethodInfo info in parent.GetMethods(binding))
                {
                    if (method.Name == info.Name && @base == info.GetBaseDefinition() && info.DeclaringType == parent)
                    {
                        return info;
                    }
                }

                parent = parent.BaseType;
            }

            return method;
        }

        public static TDelegate GetParentMethod<TDelegate>(this MethodInfo method, Object instance) where TDelegate : Delegate
        {
            if (method is null)
            {
                throw new ArgumentNullException(nameof(method));
            }

            if (instance is null)
            {
                throw new ArgumentNullException(nameof(instance));
            }

            if (method.GetParentDefinition() is not { IsAbstract: false } parent)
            {
                return method.CreateDelegate<TDelegate>(instance);
            }

            IntPtr pointer = parent.MethodHandle.GetFunctionPointer();
            return TypeUtilities.New<TDelegate, Object, IntPtr>().Invoke(instance, pointer);
        }

        public static IEnumerable<MethodInfo> GetMethods(this Type type, Type attribute)
        {
            if (type is null)
            {
                throw new ArgumentNullException(nameof(type));
            }

            if (attribute is null)
            {
                throw new ArgumentNullException(nameof(attribute));
            }

            foreach (MethodInfo method in type.GetMethods())
            {
                if (method.HasAttribute(attribute))
                {
                    yield return method;
                }
            }
        }

        public static IEnumerable<MethodInfo> GetMethods(this Type type, Type attribute, Boolean inherit)
        {
            if (type is null)
            {
                throw new ArgumentNullException(nameof(type));
            }

            if (attribute is null)
            {
                throw new ArgumentNullException(nameof(attribute));
            }

            foreach (MethodInfo method in type.GetMethods())
            {
                if (method.HasAttribute(attribute, inherit))
                {
                    yield return method;
                }
            }
        }

        public static IEnumerable<MethodInfo> GetMethods(this Type type, Type attribute, BindingFlags binding)
        {
            if (type is null)
            {
                throw new ArgumentNullException(nameof(type));
            }

            if (attribute is null)
            {
                throw new ArgumentNullException(nameof(attribute));
            }

            foreach (MethodInfo method in type.GetMethods(binding))
            {
                if (method.HasAttribute(attribute))
                {
                    yield return method;
                }
            }
        }

        public static IEnumerable<MethodInfo> GetMethods(this Type type, Type attribute, BindingFlags binding, Boolean inherit)
        {
            if (type is null)
            {
                throw new ArgumentNullException(nameof(type));
            }

            if (attribute is null)
            {
                throw new ArgumentNullException(nameof(attribute));
            }

            foreach (MethodInfo method in type.GetMethods(binding))
            {
                if (method.HasAttribute(attribute, inherit))
                {
                    yield return method;
                }
            }
        }

        public static IEnumerable<MethodInfo> GetMethods<TAttribute>(this Type type) where TAttribute : Attribute
        {
            if (type is null)
            {
                throw new ArgumentNullException(nameof(type));
            }

            MethodInfo[] methods = type.GetMethods();
            return methods.Where(static method => method.HasAttribute<TAttribute>());
        }

        public static IEnumerable<MethodInfo> GetMethods<TAttribute>(this Type type, Boolean inherit) where TAttribute : Attribute
        {
            if (type is null)
            {
                throw new ArgumentNullException(nameof(type));
            }

            MethodInfo[] methods = type.GetMethods();
            return inherit ? methods.Where(static method => method.HasAttribute<TAttribute>(false)) : methods.Where(static method => method.HasAttribute<TAttribute>(true));
        }

        public static IEnumerable<MethodInfo> GetMethods<TAttribute>(this Type type, BindingFlags binding) where TAttribute : Attribute
        {
            if (type is null)
            {
                throw new ArgumentNullException(nameof(type));
            }

            MethodInfo[] methods = type.GetMethods(binding);
            return methods.Where(static method => method.HasAttribute<TAttribute>());
        }

        public static IEnumerable<MethodInfo> GetMethods<TAttribute>(this Type type, BindingFlags binding, Boolean inherit) where TAttribute : Attribute
        {
            if (type is null)
            {
                throw new ArgumentNullException(nameof(type));
            }

            MethodInfo[] methods = type.GetMethods(binding);
            return inherit ? methods.Where(static method => method.HasAttribute<TAttribute>(false)) : methods.Where(static method => method.HasAttribute<TAttribute>(true));
        }
    }
}