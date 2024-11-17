using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Reflection;
using System.Reflection.Emit;
using System.Runtime.CompilerServices;

namespace NetExtender.Utilities.Core
{
    public static partial class CodeGeneratorUtilities
    {
        public static IEnumerable<CustomAttributeNamedArgument> Where<T>(this CustomAttributeData source) where T : MemberInfo
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            return source.NamedArguments.Where<T>();
        }

        public static IEnumerable<CustomAttributeNamedArgument> Where<T>(this IEnumerable<CustomAttributeNamedArgument> source) where T : MemberInfo
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            return source.Where(static argument => argument.MemberInfo is T);
        }

        [MethodImpl]
        public static Boolean InheritStack(this ILGenerator generator, MethodBase method)
        {
            if (generator is null)
            {
                throw new ArgumentNullException(nameof(generator));
            }

            if (method is null)
            {
                throw new ArgumentNullException(nameof(method));
            }

            if (method.GetMethodBody() is not { } body)
            {
                return false;
            }

            foreach (LocalVariableInfo local in body.LocalVariables)
            {
                generator?.DeclareLocal(local.LocalType, local.IsPinned);
            }
            
            return true;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static FieldBuilder InheritField(this TypeBuilder builder, FieldInfo field)
        {
            return InheritField(builder, field, null, null);
        }

        public static FieldBuilder InheritField(this TypeBuilder builder, FieldInfo field, Type? type, Type? inherit)
        {
            if (builder is null)
            {
                throw new ArgumentNullException(nameof(builder));
            }

            if (field is null)
            {
                throw new ArgumentNullException(nameof(field));
            }
            
            return InheritField(builder, field, builder.InheritFieldInit(field), type, inherit);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static FieldBuilder InheritField(this TypeBuilder builder, FieldBuilder field)
        {
            return InheritField(builder, field, null, null);
        }

        [SuppressMessage("ReSharper", "ParameterOnlyUsedForPreconditionCheck.Global")]
        public static FieldBuilder InheritField(this TypeBuilder builder, FieldInfo original, FieldBuilder field, Type? type, Type? inherit)
        {
            if (builder is null)
            {
                throw new ArgumentNullException(nameof(builder));
            }

            if (field is null)
            {
                throw new ArgumentNullException(nameof(field));
            }

            field.InheritCustomAttributes(original);
            return field;
        }

        public static FieldBuilder InheritFieldInit(this TypeBuilder builder, FieldInfo field)
        {
            if (builder is null)
            {
                throw new ArgumentNullException(nameof(builder));
            }

            if (field is null)
            {
                throw new ArgumentNullException(nameof(field));
            }
            
            return builder.DefineField(field.Name, field.FieldType, field.Attributes);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static PropertyBuilder InheritProperty(this TypeBuilder builder, PropertyInfo property)
        {
            return InheritProperty(builder, property, null, null);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static PropertyBuilder InheritProperty(this TypeBuilder builder, PropertyInfo property, Type? type, Type? inherit)
        {
            return InheritProperty(builder, property, type, inherit, out _, out _);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static PropertyBuilder InheritProperty(this TypeBuilder builder, PropertyInfo property, out MethodBuilder? get, out MethodBuilder? set)
        {
            return InheritProperty(builder, property, null, null, out get, out set);
        }

        public static PropertyBuilder InheritProperty(this TypeBuilder builder, PropertyInfo property, Type? type, Type? inherit, out MethodBuilder? get, out MethodBuilder? set)
        {
            if (builder is null)
            {
                throw new ArgumentNullException(nameof(builder));
            }

            if (property is null)
            {
                throw new ArgumentNullException(nameof(property));
            }
            
            return InheritProperty(builder, property, builder.InheritPropertyInit(property, out get, out set), get, set, type, inherit, null);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static PropertyBuilder InheritProperty(this TypeBuilder builder, PropertyInfo original, PropertyBuilder property, MethodBuilder? get, MethodBuilder? set)
        {
            return InheritProperty(builder, original, property, get, set, null, null);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static PropertyBuilder InheritProperty(this TypeBuilder builder, PropertyInfo original, PropertyBuilder property, MethodBuilder? get, MethodBuilder? set, Type? type, Type? inherit)
        {
            return InheritProperty(builder, original, property, get, set, type, inherit, null);
        }

        public static PropertyBuilder InheritProperty(this TypeBuilder builder, PropertyInfo original, PropertyBuilder property, MethodBuilder? get, MethodBuilder? set, Type? type, Type? inherit, MemberInfo[]? members)
        {
            if (builder is null)
            {
                throw new ArgumentNullException(nameof(builder));
            }

            if (original is null)
            {
                throw new ArgumentNullException(nameof(original));
            }

            if (property is null)
            {
                throw new ArgumentNullException(nameof(property));
            }

            property.InheritCustomAttributes(original);

            MethodInfo? accessor = original.GetMethod;
            if (accessor is not null && get is not null)
            {
                ILGenerator generator = get.GetILGenerator();
                builder.InheritMethod(accessor, get, generator, type, inherit, members);
                property.SetGetMethod(get);
            }

            accessor = original.SetMethod;
            if (accessor is not null && set is not null)
            {
                ILGenerator generator = set.GetILGenerator();
                builder.InheritMethod(accessor, set, generator, type, inherit, members);
                property.SetSetMethod(set);
            }

            return property;
        }

        public static PropertyBuilder InheritPropertyInit(this TypeBuilder builder, PropertyInfo property, out MethodBuilder? get, out MethodBuilder? set)
        {
            if (builder is null)
            {
                throw new ArgumentNullException(nameof(builder));
            }

            if (property is null)
            {
                throw new ArgumentNullException(nameof(property));
            }
            
            PropertyBuilder result = builder.DefineProperty(property.Name, property.Attributes, property.PropertyType, property.GetIndexParameterTypes());
            
            get = set = default;
            MethodInfo? accessor = property.GetMethod;
            if (accessor is not null)
            {
                get = builder.DefineMethod(accessor.Name, accessor.Attributes, accessor.CallingConvention, accessor.ReturnType, Type.EmptyTypes);
            }
            
            accessor = property.SetMethod;
            if (accessor is not null)
            {
                set = builder.DefineMethod(accessor.Name, accessor.Attributes, accessor.CallingConvention, accessor.ReturnType, accessor.GetParameterTypes());
            }

            return result;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static EventBuilder InheritEvent(this TypeBuilder builder, EventInfo @event)
        {
            return InheritEvent(builder, @event, null, null);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static EventBuilder InheritEvent(this TypeBuilder builder, EventInfo @event, Type? type, Type? inherit)
        {
            return InheritEvent(builder, @event, type, inherit, out _, out _, out _);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static EventBuilder InheritEvent(this TypeBuilder builder, EventInfo @event, out MethodBuilder? raise, out MethodBuilder? add, out MethodBuilder? remove)
        {
            return InheritEvent(builder, @event, null, null, out raise, out add, out remove);
        }

        public static EventBuilder InheritEvent(this TypeBuilder builder, EventInfo @event, Type? type, Type? inherit, out MethodBuilder? raise, out MethodBuilder? add, out MethodBuilder? remove)
        {
            if (builder is null)
            {
                throw new ArgumentNullException(nameof(builder));
            }

            if (@event is null)
            {
                throw new ArgumentNullException(nameof(@event));
            }
            
            return InheritEvent(builder, @event, builder.InheritEventInit(@event, out raise, out add, out remove), raise, add, remove, type, inherit, null);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static EventBuilder InheritEvent(this TypeBuilder builder, EventInfo original, EventBuilder @event, MethodBuilder? raise, MethodBuilder? add, MethodBuilder? remove)
        {
            return InheritEvent(builder, original, @event, raise, add, remove, null, null);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static EventBuilder InheritEvent(this TypeBuilder builder, EventInfo original, EventBuilder @event, MethodBuilder? raise, MethodBuilder? add, MethodBuilder? remove, Type? type, Type? inherit)
        {
            return InheritEvent(builder, original, @event, raise, add, remove, type, inherit, null);
        }

        public static EventBuilder InheritEvent(this TypeBuilder builder, EventInfo original, EventBuilder @event, MethodBuilder? raise, MethodBuilder? add, MethodBuilder? remove, Type? type, Type? inherit, MemberInfo[]? members)
        {
            if (builder is null)
            {
                throw new ArgumentNullException(nameof(builder));
            }

            if (original is null)
            {
                throw new ArgumentNullException(nameof(original));
            }

            if (@event is null)
            {
                throw new ArgumentNullException(nameof(@event));
            }

            @event.InheritCustomAttributes(original);

            MethodInfo? accessor = original.RaiseMethod;
            if (accessor is not null && raise is not null)
            {
                ILGenerator generator = raise.GetILGenerator();
                builder.InheritMethod(accessor, raise, generator, type, inherit, members);
                @event.SetAddOnMethod(raise);
            }

            accessor = original.AddMethod;
            if (accessor is not null && add is not null)
            {
                ILGenerator generator = add.GetILGenerator();
                builder.InheritMethod(accessor, add, generator, type, inherit, members);
                @event.SetAddOnMethod(add);
            }

            accessor = original.RemoveMethod;
            if (accessor is not null && remove is not null)
            {
                ILGenerator generator = remove.GetILGenerator();
                builder.InheritMethod(accessor, remove, generator, type, inherit, members);
                @event.SetRemoveOnMethod(remove);
            }

            return @event;
        }

        public static EventBuilder InheritEventInit(this TypeBuilder builder, EventInfo @event, out MethodBuilder? raise, out MethodBuilder? add, out MethodBuilder? remove)
        {
            if (builder is null)
            {
                throw new ArgumentNullException(nameof(builder));
            }

            if (@event is null)
            {
                throw new ArgumentNullException(nameof(@event));
            }
            
            EventBuilder result = builder.DefineEvent(@event.Name, @event.Attributes, @event.EventHandlerType!);
            
            raise = add = remove = default;
            MethodInfo? accessor = @event.RaiseMethod;
            if (accessor is not null)
            {
                raise = builder.DefineMethod(accessor.Name, accessor.Attributes, accessor.CallingConvention, accessor.ReturnType, Type.EmptyTypes);
            }
            
            accessor = @event.AddMethod;
            if (accessor is not null)
            {
                add = builder.DefineMethod(accessor.Name, accessor.Attributes, accessor.CallingConvention, accessor.ReturnType, Type.EmptyTypes);
            }
            
            accessor = @event.RemoveMethod;
            if (accessor is not null)
            {
                remove = builder.DefineMethod(accessor.Name, accessor.Attributes, accessor.CallingConvention, accessor.ReturnType, accessor.GetParameterTypes());
            }

            return result;
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static MethodBuilder InheritMethod(this TypeBuilder builder, MethodInfo method)
        {
            return InheritMethod(builder, method, (Type?) null, null);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static MethodBuilder InheritMethod(this TypeBuilder builder, MethodInfo method, Type? type, Type? inherit)
        {
            if (builder is null)
            {
                throw new ArgumentNullException(nameof(builder));
            }

            if (method is null)
            {
                throw new ArgumentNullException(nameof(method));
            }

            return InheritMethod(builder, method, builder.InheritMethodInit(method), null, type, inherit, null);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static MethodBuilder InheritMethod(this TypeBuilder builder, MethodInfo method, out ILGenerator? generator)
        {
            return InheritMethod(builder, method, null, null, out generator);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static MethodBuilder InheritMethod(this TypeBuilder builder, MethodInfo method, Type? type, Type? inherit, out ILGenerator? generator)
        {
            if (builder is null)
            {
                throw new ArgumentNullException(nameof(builder));
            }

            if (method is null)
            {
                throw new ArgumentNullException(nameof(method));
            }

            return InheritMethod(builder, method, builder.InheritMethodInit(method, out generator), generator, type, inherit, null);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static MethodBuilder InheritMethod(this TypeBuilder builder, MethodInfo original, MethodBuilder method, ILGenerator? generator)
        {
            return InheritMethod(builder, original, method, generator, null, null);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static MethodBuilder InheritMethod(this TypeBuilder builder, MethodInfo original, MethodBuilder method, ILGenerator? generator, Type? type, Type? inherit)
        {
            return InheritMethod(builder, original, method, generator, type, inherit, null);
        }

        public static MethodBuilder InheritMethod(this TypeBuilder builder, MethodInfo original, MethodBuilder method, ILGenerator? generator, Type? type, Type? inherit, MemberInfo[]? members)
        {
            if (builder is null)
            {
                throw new ArgumentNullException(nameof(builder));
            }

            if (original is null)
            {
                throw new ArgumentNullException(nameof(original));
            }

            if (method is null)
            {
                throw new ArgumentNullException(nameof(method));
            }

            method.InheritCustomAttributes(original);

            if (generator is null)
            {
                return method;
            }

            generator.InheritStack(original);
            generator.Emit(type, inherit, members, original.Instructions());
            return method;
        }

        // ReSharper disable once CognitiveComplexity
        [MethodImpl(MethodImplOptions.AggressiveOptimization)]
        public static MethodBuilder InheritMethodInit(this TypeBuilder builder, MethodInfo method)
        {
            if (builder is null)
            {
                throw new ArgumentNullException(nameof(builder));
            }

            if (method is null)
            {
                throw new ArgumentNullException(nameof(method));
            }

            MethodBuilder result = builder.DefineMethod(method.Name, method.Attributes, method.CallingConvention, method.ReturnType, method.GetParameterTypes());

            if (method.IsGenericMethodDefinition)
            {
                Type[] types = method.GetGenericArguments();
                GenericTypeParameterBuilder[] generic = result.DefineGenericParameters(types.Select(static type => type.Name).ToArray());

                for (Int32 i = 0; i < generic.Length; i++)
                {
                    generic[i].SetGenericParameterAttributes(types[i].GenericParameterAttributes);
                    
                    List<Type> interfaces = new List<Type>();
                    foreach (Type constraint in types[i].GetGenericParameterConstraints())
                    {
                        if (constraint.IsClass)
                        {
                            generic[i].SetBaseTypeConstraint(constraint);
                        }
                        else if (constraint.IsInterface)
                        {
                            interfaces.Add(constraint);
                        }
                    }

                    if (interfaces.Count > 0)
                    {
                        generic[i].SetInterfaceConstraints(interfaces.ToArray());
                    }
                }
            }

            ParameterInfo[] parameters = method.GetParameters();

            result.SetReturnType(method.ReturnType);
            result.SetParameters(parameters.Types());

            for (Int32 i = 0; i < parameters.Length; i++)
            {
                result.DefineParameter(i + 1, parameters[i].Attributes, parameters[i].Name);
            }

            Storage.Parameters.MethodBuilder[result] = parameters;
            return result;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static MethodBuilder InheritMethodInit(this TypeBuilder builder, MethodInfo method, out ILGenerator generator)
        {
            if (builder is null)
            {
                throw new ArgumentNullException(nameof(builder));
            }

            if (method is null)
            {
                throw new ArgumentNullException(nameof(method));
            }

            MethodBuilder result = InheritMethodInit(builder, method);
            generator = result.GetILGenerator();
            return result;
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ConstructorBuilder InheritStaticConstructor(this TypeBuilder builder, ConstructorInfo constructor)
        {
            return InheritStaticConstructor(builder, constructor, (Type?) null, null);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ConstructorBuilder InheritStaticConstructor(this TypeBuilder builder, ConstructorInfo constructor, Type? type, Type? inherit)
        {
            if (builder is null)
            {
                throw new ArgumentNullException(nameof(builder));
            }

            if (constructor is null)
            {
                throw new ArgumentNullException(nameof(constructor));
            }

            return InheritStaticConstructor(builder, constructor, builder.InheritStaticConstructorInit(constructor), null, type, inherit, null);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ConstructorBuilder InheritStaticConstructor(this TypeBuilder builder, ConstructorInfo constructor, out ILGenerator? generator)
        {
            return InheritStaticConstructor(builder, constructor, null, null, out generator);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ConstructorBuilder InheritStaticConstructor(this TypeBuilder builder, ConstructorInfo constructor, Type? type, Type? inherit, out ILGenerator? generator)
        {
            if (builder is null)
            {
                throw new ArgumentNullException(nameof(builder));
            }

            if (constructor is null)
            {
                throw new ArgumentNullException(nameof(constructor));
            }

            return InheritStaticConstructor(builder, constructor, builder.InheritStaticConstructorInit(constructor, out generator), generator, type, inherit, null);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ConstructorBuilder InheritStaticConstructor(this TypeBuilder builder, ConstructorInfo original, ConstructorBuilder constructor, ILGenerator? generator)
        {
            return InheritStaticConstructor(builder, original, constructor, generator, null, null);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ConstructorBuilder InheritStaticConstructor(this TypeBuilder builder, ConstructorInfo original, ConstructorBuilder constructor, ILGenerator? generator, Type? type, Type? inherit)
        {
            return InheritStaticConstructor(builder, original, constructor, generator, type, inherit, null);
        }

        public static ConstructorBuilder InheritStaticConstructor(this TypeBuilder builder, ConstructorInfo original, ConstructorBuilder constructor, ILGenerator? generator, Type? type, Type? inherit, MemberInfo[]? members)
        {
            if (builder is null)
            {
                throw new ArgumentNullException(nameof(builder));
            }

            if (original is null)
            {
                throw new ArgumentNullException(nameof(original));
            }

            if (constructor is null)
            {
                throw new ArgumentNullException(nameof(constructor));
            }

            constructor.InheritCustomAttributes(original);

            if (generator is null)
            {
                return constructor;
            }
            
            generator.InheritStack(original);
            generator.Emit(type, inherit, members, original.Instructions());
            return constructor;
        }

        public static ConstructorBuilder InheritStaticConstructorInit(this TypeBuilder builder, ConstructorInfo constructor)
        {
            if (builder is null)
            {
                throw new ArgumentNullException(nameof(builder));
            }

            if (constructor is null)
            {
                throw new ArgumentNullException(nameof(constructor));
            }

            ConstructorBuilder result = builder.DefineTypeInitializer();
            Storage.Parameters.ConstructorBuilder[result] = constructor.GetParameters();
            return result;
        }

        public static ConstructorBuilder InheritStaticConstructorInit(this TypeBuilder builder, ConstructorInfo constructor, out ILGenerator generator)
        {
            if (builder is null)
            {
                throw new ArgumentNullException(nameof(builder));
            }

            if (constructor is null)
            {
                throw new ArgumentNullException(nameof(constructor));
            }

            ConstructorBuilder result = InheritStaticConstructorInit(builder, constructor);
            generator = result.GetILGenerator();
            return result;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ImmutableArray<CustomAttributeBuilder> InheritCustomAttributes(this TypeBuilder builder, Type type)
        {
            return InheritCustomAttributes(builder, type, null);
        }

        public static ImmutableArray<CustomAttributeBuilder> InheritCustomAttributes(this TypeBuilder builder, Type type, Func<Type, TypeBuilder, CustomAttributeData, CustomAttributeBuilder, Boolean>? handler)
        {
            if (builder is null)
            {
                throw new ArgumentNullException(nameof(builder));
            }

            if (type is null)
            {
                throw new ArgumentNullException(nameof(type));
            }

            List<CustomAttributeBuilder> result = new List<CustomAttributeBuilder>();
            foreach (CustomAttributeData attribute in type.GetCustomAttributesData())
            {
                CustomAttributeBuilder custom = InheritCustomAttributeBuilder(attribute);

                if (handler?.Invoke(type, builder, attribute, custom) is false)
                {
                    continue;
                }
                
                builder.SetCustomAttribute(custom);
                result.Add(custom);
            }
            
            return result.ToImmutableArray();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ImmutableArray<CustomAttributeBuilder> InheritCustomAttributes(this FieldBuilder builder, FieldInfo field)
        {
            return InheritCustomAttributes(builder, field, null);
        }

        public static ImmutableArray<CustomAttributeBuilder> InheritCustomAttributes(this FieldBuilder builder, FieldInfo field, Func<FieldInfo, FieldBuilder, CustomAttributeData, CustomAttributeBuilder, Boolean>? handler)
        {
            if (builder is null)
            {
                throw new ArgumentNullException(nameof(builder));
            }

            if (field is null)
            {
                throw new ArgumentNullException(nameof(field));
            }

            List<CustomAttributeBuilder> result = new List<CustomAttributeBuilder>();
            foreach (CustomAttributeData attribute in field.GetCustomAttributesData())
            {
                CustomAttributeBuilder custom = InheritCustomAttributeBuilder(attribute);

                if (handler?.Invoke(field, builder, attribute, custom) is false)
                {
                    continue;
                }
                
                builder.SetCustomAttribute(custom);
                result.Add(custom);
            }
            
            return result.ToImmutableArray();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ImmutableArray<CustomAttributeBuilder> InheritCustomAttributes(this PropertyBuilder builder, PropertyInfo property)
        {
            return InheritCustomAttributes(builder, property, null);
        }

        public static ImmutableArray<CustomAttributeBuilder> InheritCustomAttributes(this PropertyBuilder builder, PropertyInfo property, Func<PropertyInfo, PropertyBuilder, CustomAttributeData, CustomAttributeBuilder, Boolean>? handler)
        {
            if (builder is null)
            {
                throw new ArgumentNullException(nameof(builder));
            }

            if (property is null)
            {
                throw new ArgumentNullException(nameof(property));
            }

            List<CustomAttributeBuilder> result = new List<CustomAttributeBuilder>();
            foreach (CustomAttributeData attribute in property.GetCustomAttributesData())
            {
                CustomAttributeBuilder custom = InheritCustomAttributeBuilder(attribute);

                if (handler?.Invoke(property, builder, attribute, custom) is false)
                {
                    continue;
                }
                
                builder.SetCustomAttribute(custom);
                result.Add(custom);
            }
            
            return result.ToImmutableArray();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ImmutableArray<CustomAttributeBuilder> InheritCustomAttributes(this EventBuilder builder, EventInfo @event)
        {
            return InheritCustomAttributes(builder, @event, null);
        }

        public static ImmutableArray<CustomAttributeBuilder> InheritCustomAttributes(this EventBuilder builder, EventInfo @event, Func<EventInfo, EventBuilder, CustomAttributeData, CustomAttributeBuilder, Boolean>? handler)
        {
            if (builder is null)
            {
                throw new ArgumentNullException(nameof(builder));
            }

            if (@event is null)
            {
                throw new ArgumentNullException(nameof(@event));
            }

            List<CustomAttributeBuilder> result = new List<CustomAttributeBuilder>();
            foreach (CustomAttributeData attribute in @event.GetCustomAttributesData())
            {
                CustomAttributeBuilder custom = InheritCustomAttributeBuilder(attribute);

                if (handler?.Invoke(@event, builder, attribute, custom) is false)
                {
                    continue;
                }
                
                builder.SetCustomAttribute(custom);
                result.Add(custom);
            }
            
            return result.ToImmutableArray();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ImmutableArray<CustomAttributeBuilder> InheritCustomAttributes(this MethodBuilder builder, MethodInfo method)
        {
            return InheritCustomAttributes(builder, method, null);
        }

        public static ImmutableArray<CustomAttributeBuilder> InheritCustomAttributes(this MethodBuilder builder, MethodInfo method, Func<MethodInfo, MethodBuilder, CustomAttributeData, CustomAttributeBuilder, Boolean>? handler)
        {
            if (builder is null)
            {
                throw new ArgumentNullException(nameof(builder));
            }

            if (method is null)
            {
                throw new ArgumentNullException(nameof(method));
            }

            List<CustomAttributeBuilder> result = new List<CustomAttributeBuilder>();
            foreach (CustomAttributeData attribute in method.GetCustomAttributesData())
            {
                CustomAttributeBuilder custom = InheritCustomAttributeBuilder(attribute);

                if (handler?.Invoke(method, builder, attribute, custom) is false)
                {
                    continue;
                }
                
                builder.SetCustomAttribute(custom);
                result.Add(custom);
            }
            
            return result.ToImmutableArray();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ImmutableArray<CustomAttributeBuilder> InheritCustomAttributes(this ConstructorBuilder builder, ConstructorInfo constructor)
        {
            return InheritCustomAttributes(builder, constructor, null);
        }

        public static ImmutableArray<CustomAttributeBuilder> InheritCustomAttributes(this ConstructorBuilder builder, ConstructorInfo constructor, Func<ConstructorInfo, ConstructorBuilder, CustomAttributeData, CustomAttributeBuilder, Boolean>? handler)
        {
            if (builder is null)
            {
                throw new ArgumentNullException(nameof(builder));
            }

            if (constructor is null)
            {
                throw new ArgumentNullException(nameof(constructor));
            }

            List<CustomAttributeBuilder> result = new List<CustomAttributeBuilder>();
            foreach (CustomAttributeData attribute in constructor.GetCustomAttributesData())
            {
                CustomAttributeBuilder custom = InheritCustomAttributeBuilder(attribute);

                if (handler?.Invoke(constructor, builder, attribute, custom) is false)
                {
                    continue;
                }
                
                builder.SetCustomAttribute(custom);
                result.Add(custom);
            }
            
            return result.ToImmutableArray();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ImmutableArray<CustomAttributeBuilder> InheritCustomAttributes(this ParameterBuilder builder, ParameterInfo parameter)
        {
            return InheritCustomAttributes(builder, parameter, null);
        }

        public static ImmutableArray<CustomAttributeBuilder> InheritCustomAttributes(this ParameterBuilder builder, ParameterInfo parameter, Func<ParameterInfo, ParameterBuilder, CustomAttributeData, CustomAttributeBuilder, Boolean>? handler)
        {
            if (builder is null)
            {
                throw new ArgumentNullException(nameof(builder));
            }

            if (parameter is null)
            {
                throw new ArgumentNullException(nameof(parameter));
            }

            List<CustomAttributeBuilder> result = new List<CustomAttributeBuilder>();
            foreach (CustomAttributeData attribute in parameter.GetCustomAttributesData())
            {
                CustomAttributeBuilder custom = InheritCustomAttributeBuilder(attribute);

                if (handler?.Invoke(parameter, builder, attribute, custom) is false)
                {
                    continue;
                }
                
                builder.SetCustomAttribute(custom);
                result.Add(custom);
            }

            return result.ToImmutableArray();
        }

        public static CustomAttributeBuilder InheritCustomAttributeBuilder(this CustomAttributeData attribute)
        {
            if (attribute is null)
            {
                throw new ArgumentNullException(nameof(attribute));
            }

            ConstructorInfo constructor = attribute.Constructor;
            Object?[] arguments = attribute.ConstructorArguments.Select(static argument => argument.Value).ToArray();

            PropertyInfo[] properties = attribute.Where<PropertyInfo>().Select(static argument => (PropertyInfo) argument.MemberInfo).ToArray();
            Object?[] pvalues = attribute.Where<PropertyInfo>().Select(static argument => argument.TypedValue.Value).ToArray();
            FieldInfo[] fields = attribute.Where<FieldInfo>().Select(static argument => (FieldInfo) argument.MemberInfo).ToArray();
            Object?[] fvalues = attribute.Where<FieldInfo>().Select(static argument => argument.TypedValue.Value).ToArray();

            return new CustomAttributeBuilder(constructor, arguments, properties, pvalues, fields, fvalues);
        }
    }
}