// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Reflection;
using System.Reflection.Emit;
using System.Runtime.CompilerServices;
using NetExtender.Initializer.Types.Core;
using NetExtender.Utilities.Types;

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

        /*private static class Generator
        {
            public static class AttributeCache
            {
                public static CustomAttributeBuilder CompilerGeneratedAttribute { get; } = GetCompilerGeneratedAttribute();
                public static CustomAttributeBuilder DebuggerBrowsableAttribute { get; } = GetDebuggerBrowsableAttribute();
                public static CustomAttributeBuilder DebuggerHiddenAttribute { get; } = GetDebuggerHiddenAttribute();
                
                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                private static CustomAttributeBuilder GetCompilerGeneratedAttribute()
                {
                    ConstructorInfo constructor = typeof(CompilerGeneratedAttribute).GetConstructor(Type.EmptyTypes) ?? throw new InvalidOperationException(nameof(CompilerGeneratedAttribute));
                    return new CustomAttributeBuilder(constructor, Array.Empty<Object>());
                }

                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                private static CustomAttributeBuilder GetDebuggerBrowsableAttribute()
                {
                    ConstructorInfo constructor = typeof(DebuggerBrowsableAttribute).GetConstructor(new[] { typeof(DebuggerBrowsableState) }) ?? throw new InvalidOperationException(nameof(DebuggerBrowsableAttribute));
                    return new CustomAttributeBuilder(constructor, new Object[] { DebuggerBrowsableState.Never });
                }
            
                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                private static CustomAttributeBuilder GetDebuggerHiddenAttribute()
                {
                    ConstructorInfo constructor = typeof(DebuggerHiddenAttribute).GetConstructor(Type.EmptyTypes) ?? throw new InvalidOperationException(nameof(DebuggerHiddenAttribute));
                    return new CustomAttributeBuilder(constructor, Array.Empty<Object>());
                }
            }

            public static class ObjectCache
            {
                public static ConstructorInfo Constructor { get; }
                public static MethodInfo ToStringMethod { get; }

                static ObjectCache()
                {
                    const BindingFlags binding = BindingFlags.Instance | BindingFlags.Public;

                    Constructor = typeof(Object).GetConstructor(Type.EmptyTypes) ?? throw new InvalidOperationException(nameof(Constructor));
                    ToStringMethod = typeof(Object).GetMethod(nameof(ToString), binding, Type.EmptyTypes) ?? throw new InvalidOperationException(nameof(ToString));
                }
            }
            
            public static class EqualityCache
            {
                public static Type Comparer { get; }
                public static Type GenericArgument { get; }
                public static MethodInfo Default { get; }
                public static MethodInfo GetHashCodeMethod { get; }
                public static MethodInfo EqualsMethod { get; }

                static EqualityCache()
                {
                    const BindingFlags binding = BindingFlags.Instance | BindingFlags.Public;
                    
                    Comparer = typeof(EqualityComparer<>);
                    GenericArgument = Comparer.GetGenericArguments()[0];
                    Default = Comparer.GetMethod($"get_{Default}", binding, Type.EmptyTypes) ?? throw new InvalidOperationException(nameof(Default));
                    GetHashCodeMethod = Comparer.GetMethod(nameof(GetHashCode), binding, new[] { GenericArgument }) ?? throw new InvalidOperationException(nameof(GetHashCode));
                    EqualsMethod = Comparer.GetMethod(nameof(Equals), binding, new[] { GenericArgument, GenericArgument }) ?? throw new InvalidOperationException(nameof(Equals));
                }
            }

            public static class StringBuilderCache
            {
                public static ConstructorInfo Constructor { get; }
                public static MethodInfo AppendString { get; }
                public static MethodInfo AppendObject { get; }

                static StringBuilderCache()
                {
                    const BindingFlags binding = BindingFlags.Instance | BindingFlags.Public;
                    
                    Type type = typeof(StringBuilder);
                    Constructor = type.GetConstructor(Type.EmptyTypes) ?? throw new InvalidOperationException(nameof(Constructor));
                    AppendString = type.GetMethod(nameof(StringBuilder.Append), binding, new[] { typeof(String) }) ?? throw new InvalidOperationException(nameof(AppendString));
                    AppendObject = type.GetMethod(nameof(StringBuilder.Append), binding, new[] { typeof(Object) }) ?? throw new InvalidOperationException(nameof(AppendObject));
                }
            }
        }*/
        
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
        
        private static class AnonymousType
        {
            private const String AnonymousTypeGeneratorAssembly = nameof(AnonymousTypeGeneratorAssembly);
            public static AssemblyName AssemblyName { get; }
            public static AssemblyBuilder Assembly { get; }
            public static String ModuleName { get; }
            public static ModuleBuilder Module { get; }
            
            static AnonymousType()
            {
                AssemblyName = new AssemblyName(AnonymousTypeGeneratorAssembly);
                Assembly = AssemblyBuilder.DefineDynamicAssembly(AssemblyName, AssemblyBuilderAccess.Run);
                ModuleName = AssemblyName.Name ?? AnonymousTypeGeneratorAssembly;
                Module = Assembly.GetDynamicModule(ModuleName) ?? Assembly.DefineDynamicModule(ModuleName);
            }
        }

        private readonly struct PropertyAnonymousInfo
        {
            public static implicit operator PropertyAnonymousInfo(PropertyInfo? info)
            {
                return info is not null ? new PropertyAnonymousInfo(info.Name, info.PropertyType, info.IsRead(), info.IsWrite()) : default;
            }

            public static implicit operator PropertyAnonymousInfo(KeyValuePair<String, Type> info)
            {
                return new PropertyAnonymousInfo(info.Key, info.Value, MethodVisibilityType.Public, MethodVisibilityType.Public);
            }
        
            public String Name { get; }
            public Type Type { get; }
            public Boolean? Read { get; }
            public Boolean? Write { get; }
        
            private PropertyAnonymousInfo(String name, Type type, MethodVisibilityType read, MethodVisibilityType write)
            {
                Name = name ?? throw new ArgumentNullException(nameof(name));
                Type = type ?? throw new ArgumentNullException(nameof(type));
                Read = read.ToBoolean();
                Write = write.ToBoolean();
            }

            public override Int32 GetHashCode()
            {
                return HashCode.Combine(Name, Type, Read, Write);
            }
        }

        public static Type DefineAnonymousType(this ExpandoObject value)
        {
            if (value is null)
            {
                throw new ArgumentNullException(nameof(value));
            }

            static IEnumerable<KeyValuePair<String, Type>> Enumerate(ExpandoObject value)
            {
                foreach ((String name, Object? item) in value)
                {
                    if (item is null)
                    {
                        continue;
                    }

                    yield return new KeyValuePair<String, Type>(name, item.GetType());
                }
            }

            return DefineAnonymousType(Enumerate(value));
        }

        private static PropertyAnonymousInfo[] ToProperties(this IEnumerable<PropertyAnonymousInfo> properties)
        {
            if (properties is null)
            {
                throw new ArgumentNullException(nameof(properties));
            }

            return properties.DistinctByThrow(property => property.Name).OrderBy(property => property.Name, StringComparer.Ordinal).ToArray();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Type DefineAnonymousType(this IEnumerable<PropertyInfo> properties)
        {
            if (properties is null)
            {
                throw new ArgumentNullException(nameof(properties));
            }

            return DefineAnonymousType(properties.Select(property => (PropertyAnonymousInfo) property).ToProperties());
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Type DefineAnonymousType(this IEnumerable<KeyValuePair<String, Type>> properties)
        {
            if (properties is null)
            {
                throw new ArgumentNullException(nameof(properties));
            }
            
            return DefineAnonymousType(properties.Select(property => (PropertyAnonymousInfo) property).ToProperties());
        }

        //TODO: ToString, Equals, GetHashCode
        private static Type DefineAnonymousType(this PropertyAnonymousInfo[] properties)
        {
            if (properties is null)
            {
                throw new ArgumentNullException(nameof(properties));
            }
            
            properties = properties.ToArray();

            lock (AnonymousType.Assembly)
            {
                String typename = $"<>f__DynamicAnonymousType<{HashCodeUtilities.Combine(properties)}>";
                if (AnonymousType.Assembly.GetType(typename, false, false) is Type type)
                {
                    return type;
                }
            
                TypeBuilder builder = AnonymousType.Module.DefineType(typename, TypeAttributes.Class | TypeAttributes.Sealed | TypeAttributes.Public | TypeAttributes.SpecialName);
                builder.SetParent(typeof(DynamicAnonymousObject));

                FieldBuilder[] fields = new FieldBuilder[properties.Length];
                for (Int32 i = 0; i < properties.Length; i++)
                {
                    PropertyAnonymousInfo info = properties[i];
                    if (info.Read is null && info.Write is null)
                    {
                        builder.DefineField(info.Name, info.Type, FieldAttributes.Public);
                        continue;
                    }

                    FieldBuilder field = builder.DefineField($"m_{info.Name.ToLowerInvariant()}", info.Type, FieldAttributes.Private | FieldAttributes.SpecialName);
                    PropertyBuilder property = builder.DefineProperty(info.Name, PropertyAttributes.HasDefault, info.Type, null);
                    fields[i] = field;

                    const MethodAttributes attributes = MethodAttributes.SpecialName | MethodAttributes.HideBySig;

                    if (info.Read is not null)
                    {
                        builder.DefineGetMethod(property, field, info.Name, info.Type, attributes | (info.Read.Value ? MethodAttributes.Public : MethodAttributes.Private));
                    }

                    if (info.Write is not null)
                    {
                        builder.DefineSetMethod(property, field, info.Name, info.Type, attributes | (info.Write.Value ? MethodAttributes.Public : MethodAttributes.Private));
                    }
                }

                return builder.CreateType() ?? throw new InvalidOperationException();
            }
        }

        private static DynamicAnonymousObject FillAnonymousObject(this DynamicAnonymousObject value, IEnumerable<KeyValuePair<String, Object?>> properties)
        {
            if (value is null)
            {
                throw new ArgumentNullException(nameof(value));
            }

            if (properties is null)
            {
                throw new ArgumentNullException(nameof(properties));
            }

            Type type = value.GetType();
            
            foreach ((String? key, Object? item) in properties)
            {
                const BindingFlags binding = BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic;

                if (type.GetProperty(key, binding) is PropertyInfo property)
                {
                    property.SetValue(value, item);
                    continue;
                }

                if (type.GetField(key, binding) is FieldInfo field)
                {
                    field.SetValue(value, item);
                }

                throw new InvalidOperationException($"Can't set value '{value}' to property or field '{key}'");
            }

            return value;
        }

        public static DynamicAnonymousObject CreateAnonymousObject(this IEnumerable<KeyValuePair<String, Object>> properties)
        {
            if (properties is null)
            {
                throw new ArgumentNullException(nameof(properties));
            }

            static KeyValuePair<String, (Type Type, Object? Value)> Convert(KeyValuePair<String, Object> pair)
            {
                (String property, Object? value) = pair;
                
                if (String.IsNullOrEmpty(property))
                {
                    throw new ArgumentException("Value cannot be null or empty.", nameof(property));
                }

                if (value is null)
                {
                    throw new ArgumentException(nameof(value));
                }
                
                Type type = value.GetType();
                return new KeyValuePair<String, (Type Type, Object? Value)>(property, (type, value));
            }

            return CreateAnonymousObject(properties.Select(Convert));
        }

        public static DynamicAnonymousObject CreateAnonymousObject(this IEnumerable<KeyValuePair<String, (Type Type, Object? Value)>> properties)
        {
            if (properties is null)
            {
                throw new ArgumentNullException(nameof(properties));
            }

            List<KeyValuePair<String, Type>> types = new List<KeyValuePair<String, Type>>(16);
            List<KeyValuePair<String, Object?>> values = new List<KeyValuePair<String, Object?>>(16);
            foreach ((String property, (Type type, Object? value)) in properties)
            {
                if (String.IsNullOrEmpty(property))
                {
                    throw new ArgumentException("Value cannot be null or empty.", nameof(property));
                }

                if (type is null)
                {
                    throw new ArgumentException(nameof(type));
                }
                
                types.Add(new KeyValuePair<String, Type>(property, type));
                values.Add(new KeyValuePair<String, Object?>(property, value));
            }

            Type anonymous = DefineAnonymousType(types);
            DynamicAnonymousObject result = Activator.CreateInstance(anonymous) as DynamicAnonymousObject ?? throw new InvalidOperationException($"Can't activate '{anonymous}' instance");
            return result.FillAnonymousObject(values);
        }

        public static DynamicAnonymousObject CreateAnonymousObject(this ExpandoObject value)
        {
            if (value is null)
            {
                throw new ArgumentNullException(nameof(value));
            }

            Type anonymous = DefineAnonymousType(value);
            DynamicAnonymousObject result = Activator.CreateInstance(anonymous) as DynamicAnonymousObject ?? throw new InvalidOperationException($"Can't activate '{anonymous}' instance");
            return result.FillAnonymousObject(value);
        }
    }
}