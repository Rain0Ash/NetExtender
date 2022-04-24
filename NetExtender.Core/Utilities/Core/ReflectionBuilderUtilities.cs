// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Reflection.Emit;
using System.Threading;

namespace NetExtender.Utilities.Core
{
    public static class ReflectionBuilderUtilities
    {
        private static Type ConverterType(ParameterInfo info)
        {
            if (info is null)
            {
                throw new ArgumentNullException(nameof(info));
            }

            return info.ParameterType;
        }
        
        private static String ConverterName(MemberInfo type)
        {
            if (type is null)
            {
                throw new ArgumentNullException(nameof(type));
            }

            return type.Name;
        }
        
        public static ConstructorBuilder DefineConstructor(this TypeBuilder builder, ConstructorInfo info)
        {
            if (builder is null)
            {
                throw new ArgumentNullException(nameof(builder));
            }

            if (info is null)
            {
                throw new ArgumentNullException(nameof(info));
            }

            if (!info.IsInheritable())
            {
                throw new MemberAccessException();
            }

            ConstructorBuilder constructor = builder.DefineConstructor(info.Attributes, info.CallingConvention, Array.ConvertAll(info.GetParameters(), ConverterType));

            ParameterInfo[] parameters = info.GetParameters();
            for (Int32 index = 0; index < parameters.Length; index++)
            {
                ParameterInfo parameter = parameters[index];
                constructor.DefineParameter(index + 1, parameter.Attributes, parameter.Name);
            }

            return constructor;
        }

        public static ConstructorBuilder DefineBaseInvokeConstructor(this TypeBuilder builder, ConstructorInfo info)
        {
            if (builder is null)
            {
                throw new ArgumentNullException(nameof(builder));
            }

            if (info is null)
            {
                throw new ArgumentNullException(nameof(info));
            }

            if (!info.IsInheritable())
            {
                throw new MemberAccessException();
            }

            ConstructorBuilder constructor = builder.DefineConstructor(info);

            ILGenerator generator = constructor.GetILGenerator();
            generator.Emit(OpCodes.Ldarg_0);
            
            for (Int32 index = 0; index < info.GetParameters().Length; index++)
            {
                generator.EmitLdarg(index + 1);
            }

            generator.Emit(OpCodes.Call, info);
            generator.Emit(OpCodes.Ret);

            return constructor;
        }

        public static MethodBuilder DefineMethodOverride(this TypeBuilder builder, MethodInfo info)
        {
            return DefineMethodOverride(builder, info, false);
        }

        public static MethodBuilder DefineMethodOverride(this TypeBuilder builder, MethodInfo info, Boolean @explicit)
        {
            if (builder is null)
            {
                throw new ArgumentNullException(nameof(builder));
            }

            if (info is null)
            {
                throw new ArgumentNullException(nameof(info));
            }

            if (!info.IsOverridable())
            {
                throw new MemberAccessException();
            }

            Type? declaring = info.DeclaringType;
            if (declaring is null)
            {
                throw new TypeAccessException();
            }

            Type[] generic = info.GetGenericArguments();
            ParameterInfo parameter = info.ReturnParameter;
            ParameterInfo[] parameters = info.GetParameters();
            String name = info.Name;
            
            if (@explicit)
            {
                name += $"#{info.MethodHandle.Value.ToString()}";
            }

            MethodAttributes attributes = info.Attributes;
            attributes &= ~MethodAttributes.Abstract;
            
            if (!declaring.IsInterface)
            {
                attributes &= ~MethodAttributes.NewSlot;
            }

            if (@explicit)
            {
                attributes &= ~MethodAttributes.MemberAccessMask;
                attributes |= MethodAttributes.Private;
            }

            MethodBuilder method = builder.DefineMethod(name, attributes, info.CallingConvention, parameter.ParameterType, Array.ConvertAll(parameters, ConverterType));

            if (generic.Length > 0)
            {
                method.DefineGenericParameters(Array.ConvertAll(generic, ConverterName));
            }

            method.DefineParameter(0, parameter.Attributes, parameter.Name);
            
            for (Int32 index = 0; index < parameters.Length; index++)
            {
                parameter = parameters[index];
                method.DefineParameter(index + 1, parameter.Attributes, parameter.Name);
            }

            if (@explicit)
            {
                builder.DefineMethodOverride(method, info);
            }

            return method;
        }

        public static MethodBuilder DefineNotImplementedMethodOverride(this TypeBuilder builder, MethodInfo info)
        {
            return DefineNotImplementedMethodOverride(builder, info, false);
        }

        public static MethodBuilder DefineNotImplementedMethodOverride(this TypeBuilder builder, MethodInfo info, Boolean @explicit)
        {
            if (builder is null)
            {
                throw new ArgumentNullException(nameof(builder));
            }

            if (info is null)
            {
                throw new ArgumentNullException(nameof(info));
            }

            if (!info.IsOverridable())
            {
                throw new MemberAccessException();
            }

            MethodBuilder method = builder.DefineMethodOverride(info, @explicit);

            ILGenerator generator = method.GetILGenerator();
            generator.Emit(OpCodes.Newobj, typeof(NotImplementedException).GetConstructor(Type.EmptyTypes)!);
            generator.Emit(OpCodes.Throw);

            return method;
        }

        public static PropertyBuilder DefineNotImplementedPropertyOverride(this TypeBuilder builder, PropertyInfo info)
        {
            return DefineNotImplementedPropertyOverride(builder, info, false);
        }

        public static PropertyBuilder DefineNotImplementedPropertyOverride(this TypeBuilder builder, PropertyInfo info, Boolean @explicit)
        {
            if (builder is null)
            {
                throw new ArgumentNullException(nameof(builder));
            }

            if (info is null)
            {
                throw new ArgumentNullException(nameof(info));
            }

            if (!info.GetAccessors().All(accessor => accessor.IsOverridable()))
            {
                throw new MemberAccessException();
            }

            String name = info.Name;
            if (@explicit)
            {
                name += $"#{info.GetAccessors().First().MethodHandle.Value.ToString()}";
            }

            PropertyBuilder property = builder.DefineProperty(name, info.Attributes, info.PropertyType, Array.ConvertAll(info.GetIndexParameters(), ConverterType));

            MethodInfo? getmethod = info.GetMethod;
            if (info.CanRead && getmethod is not null)
            {
                MethodBuilder method = builder.DefineNotImplementedMethodOverride(getmethod, @explicit);
                property.SetGetMethod(method);
            }

            MethodInfo? setmethod = info.SetMethod;
            if (info.CanWrite && setmethod is not null)
            {
                MethodBuilder method = builder.DefineNotImplementedMethodOverride(setmethod, @explicit);
                property.SetSetMethod(method);
            }

            return property;
        }

        public static KeyValuePair<PropertyBuilder, FieldBuilder> DefineAutoPropertyOverride(this TypeBuilder builder, PropertyInfo info)
        {
            return DefineAutoPropertyOverride(builder, info, false);
        }

        public static KeyValuePair<PropertyBuilder, FieldBuilder> DefineAutoPropertyOverride(this TypeBuilder builder, PropertyInfo info, Boolean @explicit)
        {
            if (builder is null)
            {
                throw new ArgumentNullException(nameof(builder));
            }

            if (info is null)
            {
                throw new ArgumentNullException(nameof(info));
            }

            if (info.GetIndexParameters().Length != 0)
            {
                throw new TargetParameterCountException();
            }

            if (!info.GetAccessors().All(accessor => accessor.IsOverridable()))
            {
                throw new MemberAccessException();
            }

            String name = info.Name;
            if (@explicit)
            {
                name += $"#{info.GetAccessors().First().MethodHandle.Value.ToString()}";
            }

            PropertyBuilder property = builder.DefineProperty(name, info.Attributes, info.PropertyType, Array.ConvertAll(info.GetIndexParameters(), ConverterType));

            FieldBuilder field = builder.DefineField(name, info.PropertyType, FieldAttributes.Private);

            MethodInfo? getmethod = info.GetMethod;
            if (info.CanRead && getmethod is not null)
            {
                MethodBuilder method = builder.DefineMethodOverride(getmethod, @explicit);

                ILGenerator ilGen = method.GetILGenerator();
                ilGen.Emit(OpCodes.Ldarg_0);
                ilGen.Emit(OpCodes.Ldfld, field);
                ilGen.Emit(OpCodes.Ret);

                property.SetGetMethod(method);
            }

            MethodInfo? setmethod = info.SetMethod;
            if (info.CanWrite && setmethod is not null)
            {
                MethodBuilder method = builder.DefineMethodOverride(setmethod, @explicit);

                ILGenerator ilGen = method.GetILGenerator();
                ilGen.Emit(OpCodes.Ldarg_0);
                ilGen.Emit(OpCodes.Ldarg_1);
                ilGen.Emit(OpCodes.Stfld, field);
                ilGen.Emit(OpCodes.Ret);

                property.SetSetMethod(method);
            }

            return new KeyValuePair<PropertyBuilder, FieldBuilder>(property, field);
        }

        public static EventBuilder DefineNotImplementedEventOverride(this TypeBuilder builder, EventInfo info)
        {
            return DefineNotImplementedEventOverride(builder, info, false);
        }

        public static EventBuilder DefineNotImplementedEventOverride(this TypeBuilder builder, EventInfo info, Boolean @explicit)
        {
            if (builder is null)
            {
                throw new ArgumentNullException(nameof(builder));
            }

            if (info is null)
            {
                throw new ArgumentNullException(nameof(info));
            }

            MethodInfo? addmethod = info.AddMethod;

            if (addmethod is null || !addmethod.IsOverridable())
            {
                throw new MemberAccessException();
            }
            
            Type? handlertype = info.EventHandlerType;

            if (handlertype is null)
            {
                throw new TypeAccessException();
            }

            String name = info.Name;
            if (@explicit)
            {
                name += $"#{addmethod.MethodHandle.Value.ToString()}";
            }

            EventBuilder @event = builder.DefineEvent(name, info.Attributes, handlertype);

            MethodBuilder method = builder.DefineNotImplementedMethodOverride(addmethod, @explicit);
            @event.SetAddOnMethod(method);

            MethodInfo? removemethod = info.RemoveMethod;
            if (removemethod is not null)
            {
                method = builder.DefineNotImplementedMethodOverride(removemethod, @explicit);
                @event.SetRemoveOnMethod(method);
            }

            return @event;
        }

        public static KeyValuePair<EventBuilder, FieldBuilder> DefineDefaultEventOverride(this TypeBuilder builder, EventInfo info)
        {
            return DefineDefaultEventOverride(builder, info, false);
        }

        public static KeyValuePair<EventBuilder, FieldBuilder> DefineDefaultEventOverride(this TypeBuilder builder, EventInfo info, Boolean @explicit)
        {
            if (builder is null)
            {
                throw new ArgumentNullException(nameof(builder));
            }

            if (info is null)
            {
                throw new ArgumentNullException(nameof(info));
            }

            MethodInfo? addmethod = info.AddMethod;
            
            if (addmethod is null || !addmethod.IsOverridable())
            {
                throw new MemberAccessException();
            }
            
            Type? handlertype = info.EventHandlerType;
            
            if (handlertype is null)
            {
                throw new TypeAccessException();
            }

            String name = info.Name;
            if (@explicit)
            {
                name += $"#{addmethod.MethodHandle.Value.ToString()}";
            }
            
            static Boolean IsGenericMethod(MethodInfo info)
            {
                return info.Name == nameof(Interlocked.CompareExchange) && info.IsGenericMethod;
            }

            MethodInfo generic = typeof(Interlocked).GetMethods().Single(IsGenericMethod).MakeGenericMethod(handlertype);

            EventBuilder @event = builder.DefineEvent(name, info.Attributes, handlertype);

            FieldBuilder field = builder.DefineField(name, handlertype, FieldAttributes.Private);

            MethodBuilder method = builder.DefineMethodOverride(addmethod, @explicit);

            ILGenerator generator = method.GetILGenerator();
            generator.DeclareLocal(handlertype);
            generator.DeclareLocal(handlertype);
            generator.DeclareLocal(handlertype);
            generator.Emit(OpCodes.Ldarg_0);
            generator.Emit(OpCodes.Ldfld, field);
            generator.Emit(OpCodes.Stloc_0);
            Label label = generator.DefineLabel();
            generator.MarkLabel(label);
            generator.Emit(OpCodes.Ldloc_0);
            generator.Emit(OpCodes.Stloc_1);
            generator.Emit(OpCodes.Ldloc_1);
            generator.Emit(OpCodes.Ldarg_1);
            generator.Emit(OpCodes.Call, typeof(Delegate).GetMethod(nameof(Delegate.Combine), new[] { typeof(Delegate), typeof(Delegate) })!);
            generator.Emit(OpCodes.Castclass, handlertype);
            generator.Emit(OpCodes.Stloc_2);
            generator.Emit(OpCodes.Ldarg_0);
            generator.Emit(OpCodes.Ldflda, field);
            generator.Emit(OpCodes.Ldloc_2);
            generator.Emit(OpCodes.Ldloc_1);

            generator.Emit(OpCodes.Call, generic);
            generator.Emit(OpCodes.Stloc_0);
            generator.Emit(OpCodes.Ldloc_0);
            generator.Emit(OpCodes.Ldloc_1);
            generator.Emit(OpCodes.Bne_Un_S, label);
            generator.Emit(OpCodes.Ret);

            @event.SetAddOnMethod(method);
            
            MethodInfo? removemethod = info.RemoveMethod;
            
            if (removemethod is not null)
            {
                method = builder.DefineMethodOverride(removemethod, @explicit);

                generator = method.GetILGenerator();
                generator.DeclareLocal(handlertype);
                generator.DeclareLocal(handlertype);
                generator.DeclareLocal(handlertype);
                generator.Emit(OpCodes.Ldarg_0);
                generator.Emit(OpCodes.Ldfld, field);
                generator.Emit(OpCodes.Stloc_0);
                label = generator.DefineLabel();
                generator.MarkLabel(label);
                generator.Emit(OpCodes.Ldloc_0);
                generator.Emit(OpCodes.Stloc_1);
                generator.Emit(OpCodes.Ldloc_1);
                generator.Emit(OpCodes.Ldarg_1);
                generator.Emit(OpCodes.Call, typeof(Delegate).GetMethod(nameof(Delegate.Remove), new[] { typeof(Delegate), typeof(Delegate) })!);
                generator.Emit(OpCodes.Castclass, handlertype);
                generator.Emit(OpCodes.Stloc_2);
                generator.Emit(OpCodes.Ldarg_0);
                generator.Emit(OpCodes.Ldflda, field);
                generator.Emit(OpCodes.Ldloc_2);
                generator.Emit(OpCodes.Ldloc_1);
                generator.Emit(OpCodes.Call, generic);
                generator.Emit(OpCodes.Stloc_0);
                generator.Emit(OpCodes.Ldloc_0);
                generator.Emit(OpCodes.Ldloc_1);
                generator.Emit(OpCodes.Bne_Un_S, label);
                generator.Emit(OpCodes.Ret);

                @event.SetRemoveOnMethod(method);
            }

            return new KeyValuePair<EventBuilder, FieldBuilder>(@event, field);
        }
    }
}