// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Collections.Concurrent;
using System.Collections.Immutable;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Reflection.Emit;
using System.Runtime.CompilerServices;
using NetExtender.Utilities.Core;

namespace NetExtender.Utilities.Types
{
    [SuppressMessage("ReSharper", "CoVariantArrayConversion")]
    public static class DelegateUtilities
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        [return: NotNullIfNotNull("delegate")]
        public static TDelegate? Unsafe<TDelegate>(this Delegate? @delegate) where TDelegate : Delegate
        {
            return System.Runtime.CompilerServices.Unsafe.As<TDelegate>(@delegate);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Predicate<T> AsPredicate<T>(this Func<T, Boolean> function)
        {
            if (function is null)
            {
                throw new ArgumentNullException(nameof(function));
            }

            return As<Predicate<T>>(function);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Func<T, Boolean> AsFunc<T>(this Predicate<T> predicate)
        {
            if (predicate is null)
            {
                throw new ArgumentNullException(nameof(predicate));
            }

            return As<Func<T, Boolean>>(predicate);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Comparison<T> AsComparison<T>(this Func<T, T, Int32> function)
        {
            if (function is null)
            {
                throw new ArgumentNullException(nameof(function));
            }

            return As<Comparison<T>>(function);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Func<T, T, Int32> AsFunc<T>(this Comparison<T> comparison)
        {
            if (comparison is null)
            {
                throw new ArgumentNullException(nameof(comparison));
            }

            return As<Func<T, T, Int32>>(comparison);
        }

        [MethodImpl(MethodImplOptions.AggressiveOptimization)]
        [SuppressMessage("ReSharper", "ConvertTypeCheckToNullCheck")]
        public static TDelegate[] GetInvocationList<TDelegate>(this MulticastDelegate @delegate) where TDelegate : Delegate
        {
            if (@delegate is null)
            {
                throw new ArgumentNullException(nameof(@delegate));
            }

            Delegate[] invocation = @delegate.GetInvocationList();
            return Array.FindAll(invocation.As<Delegate, TDelegate>(), static @delegate => @delegate is TDelegate);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static TDelegate As<TDelegate>(Delegate @delegate)
        {
            if (@delegate is null)
            {
                throw new ArgumentNullException(nameof(@delegate));
            }

            return (TDelegate) (Object) Delegate.CreateDelegate(typeof(TDelegate), @delegate.Target, @delegate.Method);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean Has(this Delegate @delegate, Delegate? value)
        {
            if (@delegate is null)
            {
                throw new ArgumentNullException(nameof(@delegate));
            }

            return @delegate.GetInvocationList().Contains(value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean Contains(this Delegate? @delegate, Delegate? value)
        {
            return @delegate is not null && Has(@delegate, value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T Combine<T>(this T @delegate, T value) where T : Delegate
        {
            return (T) Delegate.Combine(@delegate, value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T? Combine<T>(params T[] delegates) where T : Delegate
        {
            if (delegates is null)
            {
                throw new ArgumentNullException(nameof(delegates));
            }

            return delegates.Length switch
            {
                0 => null,
                1 => delegates[1],
                _ => (T?) Delegate.Combine(delegates)
            };
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T? Combine<T>(this T @delegate, params T[]? delegates) where T : Delegate
        {
            if (@delegate is null)
            {
                throw new ArgumentNullException(nameof(@delegate));
            }

            if (delegates is null || delegates.Length <= 0)
            {
                return @delegate;
            }

            return (T?) Delegate.Combine(delegates.Prepend(@delegate).ToArray());
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T? Remove<T>(this T @delegate, T value) where T : Delegate
        {
            return (T?) Delegate.Remove(@delegate, value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T? RemoveAll<T>(this T @delegate, T value) where T : Delegate
        {
            return (T?) Delegate.RemoveAll(@delegate, value);
        }

        public static Object? ParallelDynamicInvoke(this Delegate @delegate, params Object?[]? args)
        {
            if (@delegate is null)
            {
                throw new ArgumentNullException(nameof(@delegate));
            }

            if (args is null)
            {
                throw new ArgumentNullException(nameof(args));
            }

            return @delegate.GetInvocationList()
                .AsParallel().AsOrdered()
                .Select(item => item.DynamicInvoke(args))
                .LastOrDefault();
        }

        private static ConcurrentDictionary<FieldInfo, DynamicMethod> GetMethods { get; } = new ConcurrentDictionary<FieldInfo, DynamicMethod>();
        private static ConcurrentDictionary<FieldInfo, DynamicMethod> SetMethods { get; } = new ConcurrentDictionary<FieldInfo, DynamicMethod>();
        private static ConcurrentDictionary<ConstructorInfo, DynamicMethod> CreateMethods { get; } = new ConcurrentDictionary<ConstructorInfo, DynamicMethod>();

        public static TDelegate CreateGetDelegate<TDelegate>(this FieldInfo field) where TDelegate : Delegate
        {
            return (TDelegate) CreateGetDelegate(field, typeof(TDelegate));
        }

        public static Delegate CreateGetDelegate(this FieldInfo field, Type type)
        {
            return CreateGetDelegate(field, type, null);
        }

        public static TDelegate CreateGetDelegate<TDelegate>(this FieldInfo field, Object? target) where TDelegate : Delegate
        {
            return (TDelegate) CreateGetDelegate(field, typeof(TDelegate), target);
        }

        public static Delegate CreateGetDelegate(this FieldInfo field, Type type, Object? target)
        {
            if (field is null)
            {
                throw new ArgumentNullException(nameof(field));
            }

            if (type is null)
            {
                throw new ArgumentNullException(nameof(type));
            }

            if (!typeof(Delegate).IsAssignableFrom(type))
            {
                throw new InvalidCastException();
            }

            DynamicMethod method = GetMethods.GetOrAdd(field, CreateGetMethod);
            return method.CreateDelegate(type, target);
        }

        private static DynamicMethod CreateGetMethod(this FieldInfo field)
        {
            if (field is null)
            {
                throw new ArgumentNullException(nameof(field));
            }

            Type? declaring = field.DeclaringType;

            if (declaring is null)
            {
                throw new TypeAccessException();
            }

            Type[] parameters = field.IsStatic ? Type.EmptyTypes : new[] { declaring };

            DynamicMethod method = new DynamicMethod($"{field.Name}.GetValue", field.FieldType, parameters, true);
            ILGenerator generator = method.GetILGenerator();

            if (field.IsStatic)
            {
                generator.Emit(OpCodes.Ldsfld, field);
                generator.Emit(OpCodes.Ret);
                return method;
            }

            method.DefineParameter(1, ParameterAttributes.None, "instance");
            generator.Emit(OpCodes.Ldarg_0);
            generator.Emit(OpCodes.Ldfld, field);
            generator.Emit(OpCodes.Ret);
            return method;
        }

        public static TDelegate CreateSetDelegate<TDelegate>(this FieldInfo field) where TDelegate : Delegate
        {
            return (TDelegate) CreateSetDelegate(field, typeof(TDelegate));
        }

        public static Delegate CreateSetDelegate(this FieldInfo field, Type type)
        {
            return CreateSetDelegate(field, type, null);
        }

        public static TDelegate CreateSetDelegate<TDelegate>(this FieldInfo field, Object? target) where TDelegate : Delegate
        {
            return (TDelegate) CreateSetDelegate(field, typeof(TDelegate), target);
        }

        public static Delegate CreateSetDelegate(this FieldInfo field, Type type, Object? target)
        {
            if (field is null)
            {
                throw new ArgumentNullException(nameof(field));
            }

            if (type is null)
            {
                throw new ArgumentNullException(nameof(type));
            }

            if (!typeof(Delegate).IsAssignableFrom(type))
            {
                throw new ArgumentException("Type is not a delegate type.", nameof(type));
            }

            DynamicMethod method = SetMethods.GetOrAdd(field, CreateSetMethod);
            return method.CreateDelegate(type, target);
        }

        private static DynamicMethod CreateSetMethod(this FieldInfo field)
        {
            if (field is null)
            {
                throw new ArgumentNullException(nameof(field));
            }

            Type[] parameters = field.IsStatic ? new[] { field.FieldType } : new[] { field.DeclaringType!, field.FieldType };

            DynamicMethod method = new DynamicMethod($"{field.Name}.SetValue", typeof(void), parameters, true);
            ILGenerator generator = method.GetILGenerator();

            if (field.IsStatic)
            {
                method.DefineParameter(1, ParameterAttributes.None, "value");
                generator.Emit(OpCodes.Ldarg_0);
                generator.Emit(OpCodes.Stsfld, field);
            }
            else
            {
                method.DefineParameter(1, ParameterAttributes.None, "instance");
                generator.Emit(OpCodes.Ldarg_0);
                method.DefineParameter(2, ParameterAttributes.None, "value");
                generator.Emit(OpCodes.Ldarg_1);
                generator.Emit(OpCodes.Stfld, field);
            }

            generator.Emit(OpCodes.Ret);
            return method;
        }

        public static TDelegate CreateDelegate<TDelegate>(this ConstructorInfo constructor) where TDelegate : Delegate
        {
            return (TDelegate) CreateDelegate(constructor, typeof(TDelegate));
        }

        public static Delegate CreateDelegate(this ConstructorInfo constructor, Type type)
        {
            if (constructor is null)
            {
                throw new ArgumentNullException(nameof(constructor));
            }

            if (type is null)
            {
                throw new ArgumentNullException(nameof(type));
            }

            if (constructor.IsStatic)
            {
                throw new ArgumentException("Constructor must not be static.", nameof(constructor));
            }

            if (!typeof(Delegate).IsAssignableFrom(type))
            {
                throw new ArgumentException("Type is not a delegate type.", nameof(type));
            }

            DynamicMethod method = CreateMethods.GetOrAdd(constructor, CreateInvokeMethod);
            return method.CreateDelegate(type);
        }

        private static DynamicMethod CreateInvokeMethod(this ConstructorInfo constructor)
        {
            if (constructor is null)
            {
                throw new ArgumentNullException(nameof(constructor));
            }

            if (constructor.IsStatic)
            {
                throw new ArgumentException("Constructor must not be static.", nameof(constructor));
            }

            Type? declaring = constructor.DeclaringType;

            if (declaring is null)
            {
                throw new TypeAccessException();
            }

            ParameterInfo[] parameters = constructor.GetParameters();
            Type[] types = Array.ConvertAll(parameters, param => param.ParameterType);
            DynamicMethod method = new DynamicMethod("CreateInstance", declaring, types, true);
            ILGenerator generator = method.GetILGenerator();

            for (Int32 index = 0; index < parameters.Length; index++)
            {
                ParameterInfo param = parameters[index];
                method.DefineParameter(index + 1, param.Attributes, param.Name);
                generator.EmitLdarg(index);
            }

            generator.Emit(OpCodes.Newobj, constructor);
            generator.Emit(OpCodes.Ret);
            return method;
        }

        public static TDelegate CreateDelegate<TDelegate>(this MethodInfo method) where TDelegate : Delegate
        {
            if (method is null)
            {
                throw new ArgumentNullException(nameof(method));
            }

            return (TDelegate) method.CreateDelegate(typeof(TDelegate));
        }

        public static TDelegate CreateDelegate<TDelegate>(this MethodInfo method, Object? target) where TDelegate : Delegate
        {
            if (method is null)
            {
                throw new ArgumentNullException(nameof(method));
            }

            return (TDelegate) method.CreateDelegate(typeof(TDelegate), target);
        }

        public static TDelegate CreateTargetDelegate<TTarget, TDelegate>(this MethodInfo method) where TDelegate : Delegate
        {
            if (method is null)
            {
                throw new ArgumentNullException(nameof(method));
            }

            ParameterExpression instance = Expression.Parameter(typeof(TTarget), nameof(instance));
            ParameterInfo[] parameters = method.GetParameters();
            ParameterExpression[] arguments = new ParameterExpression[parameters.Length + 1];
            arguments[0] = instance;

            Int32 i = 1;
            foreach (ParameterInfo parameter in parameters)
            {
                arguments[i++] = Expression.Parameter(parameter.ParameterType, parameter.Name);
            }
            
            MethodCallExpression call = Expression.Call(instance, method, arguments[1..]);
            return Expression.Lambda<TDelegate>(call, arguments).Compile();
        }

        private static ConcurrentDictionary<FieldInfo, DynamicMethod> DynamicGetMethods { get; } = new ConcurrentDictionary<FieldInfo, DynamicMethod>();
        private static ConcurrentDictionary<FieldInfo, DynamicMethod> DynamicSetMethods { get; } = new ConcurrentDictionary<FieldInfo, DynamicMethod>();
        private static ConcurrentDictionary<ConstructorInfo, DynamicMethod> DynamicCreateMethods { get; } = new ConcurrentDictionary<ConstructorInfo, DynamicMethod>();
        private static ConcurrentDictionary<MethodInfo, DynamicMethod> DynamicInvokeMethods { get; } = new ConcurrentDictionary<MethodInfo, DynamicMethod>();

        public static Func<Object?, Object?> CreateDynamicGetDelegate(this FieldInfo field)
        {
            if (field is null)
            {
                throw new ArgumentNullException(nameof(field));
            }

            DynamicMethod method = DynamicGetMethods.GetOrAdd(field, CreateDynamicGetMethod);
            return method.CreateDelegate<Func<Object?, Object?>>();
        }

        public static Func<Object?> CreateDynamicGetDelegate(this FieldInfo field, Object? target)
        {
            if (field is null)
            {
                throw new ArgumentNullException(nameof(field));
            }

            DynamicMethod method = DynamicGetMethods.GetOrAdd(field, CreateDynamicGetMethod);
            return method.CreateDelegate<Func<Object?>>(target);
        }

        private static DynamicMethod CreateDynamicGetMethod(this FieldInfo field)
        {
            if (field is null)
            {
                throw new ArgumentNullException(nameof(field));
            }

            DynamicMethod method = new DynamicMethod($"{field.Name}.DynamicGetValue", typeof(Object), new[] { typeof(Object) }, true);
            method.DefineParameter(1, ParameterAttributes.None, "instance");
            ILGenerator generator = method.GetILGenerator();

            if (!field.IsStatic)
            {
                generator.Emit(OpCodes.Ldarg_0);
                generator.EmitUnbox(field.DeclaringType!);
            }

            generator.Emit(field.IsStatic ? OpCodes.Ldsfld : OpCodes.Ldfld, field);
            generator.EmitBox(field.FieldType);
            generator.Emit(OpCodes.Ret);

            return method;
        }

        public static Action<Object?, Object?> CreateDynamicSetDelegate(this FieldInfo field)
        {
            if (field is null)
            {
                throw new ArgumentNullException(nameof(field));
            }

            DynamicMethod method = DynamicSetMethods.GetOrAdd(field, CreateDynamicSetMethod);
            return method.CreateDelegate<Action<Object?, Object?>>();
        }

        public static Action<Object?> CreateDynamicSetDelegate(this FieldInfo field, Object? target)
        {
            if (field is null)
            {
                throw new ArgumentNullException(nameof(field));
            }

            DynamicMethod method = DynamicSetMethods.GetOrAdd(field, CreateDynamicSetMethod);
            return method.CreateDelegate<Action<Object?>>(target);
        }

        private static DynamicMethod CreateDynamicSetMethod(this FieldInfo field)
        {
            if (field is null)
            {
                throw new ArgumentNullException(nameof(field));
            }

            DynamicMethod method = new DynamicMethod($"{field.Name}.DynamicSetValue", typeof(void), new[] { typeof(Object), typeof(Object) }, true);
            method.DefineParameter(1, ParameterAttributes.None, "instance");
            method.DefineParameter(2, ParameterAttributes.None, "value");
            ILGenerator generator = method.GetILGenerator();

            if (!field.IsStatic)
            {
                generator.Emit(OpCodes.Ldarg_0);
                generator.EmitUnbox(field.DeclaringType!);
            }

            generator.Emit(OpCodes.Ldarg_1);
            generator.EmitUnbox(field.FieldType);
            generator.Emit(field.IsStatic ? OpCodes.Stsfld : OpCodes.Stfld, field);
            generator.Emit(OpCodes.Ret);

            return method;
        }

        public static Func<Object?[]?, Object> CreateDynamicDelegate(this ConstructorInfo constructor)
        {
            if (constructor is null)
            {
                throw new ArgumentNullException(nameof(constructor));
            }

            if (constructor.IsStatic)
            {
                throw new ArgumentException("Constructor must not be static.", nameof(constructor));
            }

            DynamicMethod method = DynamicCreateMethods.GetOrAdd(constructor, CreateDynamicInvokeMethod);
            return method.CreateDelegate<Func<Object?[]?, Object>>();
        }

        private static DynamicMethod CreateDynamicInvokeMethod(this ConstructorInfo constructor)
        {
            if (constructor is null)
            {
                throw new ArgumentNullException(nameof(constructor));
            }

            if (constructor.IsStatic)
            {
                throw new ArgumentException("Constructor must not be static.", nameof(constructor));
            }

            ParameterInfo[] parameters = constructor.GetParameters();
            DynamicMethod method = new DynamicMethod("DynamicCreateInstance", typeof(Object), new[] { typeof(Object?[]) }, true);
            method.DefineParameter(1, ParameterAttributes.None, "arguments");
            ILGenerator generator = method.GetILGenerator();

            for (Int32 index = 0; index < parameters.Length; index++)
            {
                generator.Emit(OpCodes.Ldarg_0);
                generator.EmitLdcI4(index);
                generator.Emit(OpCodes.Ldelem_Ref);

                ParameterInfo parameter = parameters[index];
                generator.EmitUnbox(parameter.ParameterType);
            }

            generator.Emit(OpCodes.Newobj, constructor);
            generator.EmitBox(constructor.DeclaringType!);
            generator.Emit(OpCodes.Ret);

            return method;
        }

        public static Func<Object?, Object?[]?, Object?> CreateDynamicDelegate(this MethodInfo info)
        {
            if (info is null)
            {
                throw new ArgumentNullException(nameof(info));
            }

            DynamicMethod method = DynamicInvokeMethods.GetOrAdd(info, CreateDynamicInvokeMethod);
            return method.CreateDelegate<Func<Object?, Object?[]?, Object?>>();
        }

        public static Func<Object?[]?, Object?> CreateDynamicDelegate(this MethodInfo info, Object? target)
        {
            if (info is null)
            {
                throw new ArgumentNullException(nameof(info));
            }

            DynamicMethod method = DynamicInvokeMethods.GetOrAdd(info, CreateDynamicInvokeMethod);
            return method.CreateDelegate<Func<Object?[]?, Object?>>(target);
        }

        private static DynamicMethod CreateDynamicInvokeMethod(this MethodInfo info)
        {
            if (info is null)
            {
                throw new ArgumentNullException(nameof(info));
            }

            Type? declaring = info.DeclaringType;

            if (declaring is null)
            {
                throw new TypeAccessException();
            }

            ParameterInfo[] parameters = info.GetParameters();
            DynamicMethod method = new DynamicMethod($"{info.Name}.DynamicInvoke", typeof(Object), new[] { typeof(Object), typeof(Object?[]) }, true);
            ParameterBuilder? instance = method.DefineParameter(1, ParameterAttributes.None, nameof(instance));
            ParameterBuilder? arguments = method.DefineParameter(2, ParameterAttributes.None, nameof(arguments));
            ILGenerator generator = method.GetILGenerator();

            if (!info.IsStatic)
            {
                generator.Emit(OpCodes.Ldarg_0);
                generator.EmitUnbox(declaring);
            }

            for (Int32 index = 0; index < parameters.Length; index++)
            {
                generator.Emit(OpCodes.Ldarg_1);
                generator.EmitLdcI4(index);
                generator.Emit(OpCodes.Ldelem_Ref);

                ParameterInfo parameter = parameters[index];
                generator.EmitUnbox(parameter.ParameterType);
            }

            generator.Emit(info.IsVirtual ? OpCodes.Callvirt : OpCodes.Call, info);

            if (info.ReturnType == typeof(void))
            {
                generator.Emit(OpCodes.Ldnull);
                generator.Emit(OpCodes.Ret);
                return method;
            }

            generator.EmitBox(info.ReturnType);
            generator.Emit(OpCodes.Ret);

            return method;
        }
    }
}