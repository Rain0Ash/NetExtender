// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Reflection;
using System.Reflection.Emit;
using System.Runtime.CompilerServices;
using System.Text;
using NetExtender.Utilities.Core;
using NetExtender.Utilities.Types;

namespace NetExtender.Types.Anonymous
{
    public class AnonymousTypeGenerator
    {
        protected AssemblyName AssemblyName { get; }
        protected AssemblyBuilder Assembly { get; }
        protected String ModuleName { get; }
        protected ModuleBuilder Module { get; }

        public AnonymousTypeGenerator(String assembly)
        {
            if (assembly is null)
            {
                throw new ArgumentNullException(nameof(assembly));
            }
            
            AssemblyName = new AssemblyName(assembly);
            Assembly = AssemblyBuilder.DefineDynamicAssembly(AssemblyName, AssemblyBuilderAccess.Run);
            ModuleName = AssemblyName.Name ?? assembly;
            Module = Assembly.GetDynamicModule(ModuleName) ?? Assembly.DefineDynamicModule(ModuleName);
        }

        protected AnonymousTypeGenerator(AssemblyName assemblyname, AssemblyBuilder assembly, String modulename, ModuleBuilder module)
        {
            AssemblyName = assemblyname ?? throw new ArgumentNullException(nameof(assemblyname));
            Assembly = assembly ?? throw new ArgumentNullException(nameof(assembly));
            ModuleName = modulename ?? throw new ArgumentNullException(nameof(modulename));
            Module = module ?? throw new ArgumentNullException(nameof(module));
        }

        protected virtual String GenerateTypeName(AnonymousTypePropertyInfo[] properties)
        {
            if (properties is null)
            {
                throw new ArgumentNullException(nameof(properties));
            }

            return $"<>f__AnonymousType<{HashCodeUtilities.Combine(properties)}>";
        }

        protected virtual TypeBuilder DefineType(String typename)
        {
            return DefineType<AnonymousObject>(typename);
        }

        protected virtual TypeBuilder DefineType<TParent>(String typename) where TParent : class
        {
            if (typename is null)
            {
                throw new ArgumentNullException(nameof(typename));
            }

            TypeBuilder builder = Module.DefineType(typename, TypeAttributes.Class | TypeAttributes.Sealed | TypeAttributes.Public | TypeAttributes.SpecialName);
            builder.SetParent(typeof(TParent));
            return builder;
        }

        protected virtual FieldBuilder DefineField(TypeBuilder builder, AnonymousTypePropertyInfo info)
        {
            if (builder is null)
            {
                throw new ArgumentNullException(nameof(builder));
            }
            
            return builder.DefineField(info.Name, info.Type, FieldAttributes.Public);
        }

        protected virtual FieldBuilder DefinePropertyField(TypeBuilder builder, AnonymousTypePropertyInfo info)
        {
            if (builder is null)
            {
                throw new ArgumentNullException(nameof(builder));
            }
            
            return builder.DefineField($"m_{info.Name.ToLowerInvariant()}", info.Type, FieldAttributes.Private | FieldAttributes.SpecialName);
        }

        protected virtual Boolean DefineGetMethod(TypeBuilder builder, PropertyBuilder property, FieldBuilder field, AnonymousTypePropertyInfo info)
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

            if (info.Read is null)
            {
                return false;
            }

            const MethodAttributes attributes = MethodAttributes.SpecialName | MethodAttributes.HideBySig;
            builder.DefineGetMethod(property, field, info.Name, info.Type, attributes | (info.Read.Value ? MethodAttributes.Public : MethodAttributes.Private));
            return true;
        }

        protected virtual Boolean DefineSetMethod(TypeBuilder builder, PropertyBuilder property, FieldBuilder field, AnonymousTypePropertyInfo info)
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
            
            if (info.Write is null)
            {
                return false;
            }

            const MethodAttributes attributes = MethodAttributes.SpecialName | MethodAttributes.HideBySig;
            builder.DefineSetMethod(property, field, info.Name, info.Type, attributes | (info.Write.Value ? MethodAttributes.Public : MethodAttributes.Private));
            return true;
        }

        protected virtual void DefineAccessors(TypeBuilder builder, PropertyBuilder property, FieldBuilder field, AnonymousTypePropertyInfo info)
        {
            DefineGetMethod(builder, property, field, info);
            DefineSetMethod(builder, property, field, info);
        }

        protected virtual (PropertyBuilder Property, FieldBuilder Field) DefineProperty(TypeBuilder builder, AnonymousTypePropertyInfo info)
        {
            if (builder is null)
            {
                throw new ArgumentNullException(nameof(builder));
            }
            
            FieldBuilder field = DefinePropertyField(builder, info);
            PropertyBuilder property = builder.DefineProperty(info.Name, PropertyAttributes.HasDefault, info.Type, null);
            DefineAccessors(builder, property, field, info);
            return (property, field);
        }

        protected virtual (PropertyBuilder? Property, FieldBuilder Field) Define(TypeBuilder builder, AnonymousTypePropertyInfo info)
        {
            if (builder is null)
            {
                throw new ArgumentNullException(nameof(builder));
            }

            return info.Read is not null || info.Write is not null ? DefineProperty(builder, info) : (null, DefineField(builder, info));
        }

        //TODO: ToString, Equals, GetHashCode
        public virtual Type DefineType(AnonymousTypePropertyInfo[] properties)
        {
            if (properties is null)
            {
                throw new ArgumentNullException(nameof(properties));
            }
            
            lock (Assembly)
            {
                String typename = GenerateTypeName(properties);
                if (Assembly.GetType(typename, false, false) is Type type)
                {
                    return type;
                }
            
                TypeBuilder builder = DefineType(typename);

                foreach (AnonymousTypePropertyInfo info in properties)
                {
                    Define(builder, info);
                }

                return builder.CreateType() ?? throw new InvalidOperationException($"Can't create type '{typename}'");
            }
        }
        
        protected static class Generator
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
        }
    }
}