// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Reflection.Emit;
using System.Text;
using NetExtender.Types.Anonymous.Interfaces;
using NetExtender.Utilities.Core;
using NetExtender.Utilities.Types;

namespace NetExtender.Types.Anonymous
{
    public class AnonymousTypeGenerator : AnonymousTypeGeneratorAbstraction
    {
        public AnonymousTypeGenerator(String assembly)
            : base(assembly)
        {
        }

        protected AnonymousTypeGenerator(AssemblyName assemblyname, AssemblyBuilder assembly, String modulename, ModuleBuilder module)
            : base(assemblyname, assembly, modulename, module)
        {
        }

        protected override String GenerateTypeName(AnonymousTypePropertyInfo[] properties)
        {
            if (properties is null)
            {
                throw new ArgumentNullException(nameof(properties));
            }

            return $"<>f__AnonymousType<{HashCodeUtilities.Combine(properties)}>";
        }

        protected override TypeBuilder DefineType<TParent>(String typename) where TParent : class
        {
            if (typename is null)
            {
                throw new ArgumentNullException(nameof(typename));
            }

            TypeBuilder builder = Module.DefineType(typename, TypeAttributes.Class | TypeAttributes.Sealed | TypeAttributes.Public | TypeAttributes.SpecialName);
            builder.SetParent(typeof(TParent));
            return builder;
        }

        protected override ConstructorBuilder[] DefineConstructor(TypeBuilder builder, (PropertyBuilder? Property, FieldBuilder Field)[] fields)
        {
            if (builder is null)
            {
                throw new ArgumentNullException(nameof(builder));
            }

            if (fields is null)
            {
                throw new ArgumentNullException(nameof(fields));
            }

            const MethodAttributes attributes = MethodAttributes.Public | MethodAttributes.HideBySig;

            if (fields.Length <= 0)
            {
                return new[] { builder.DefineConstructor(attributes) };
            }

            static KeyValuePair<String, FieldBuilder> Convert((PropertyBuilder? Property, FieldBuilder Field) value)
            {
                (PropertyBuilder? property, FieldBuilder? field) = value;

                String name = property?.Name ?? field.Name;
                return new KeyValuePair<String, FieldBuilder>(name, field);
            }

            return new[] { builder.DefineConstructor(attributes), builder.DefineConstructor(fields.Select(Convert).ToArray(), attributes) };
        }

        protected override FieldBuilder DefineField(TypeBuilder builder, AnonymousTypePropertyInfo info)
        {
            if (builder is null)
            {
                throw new ArgumentNullException(nameof(builder));
            }

            return builder.DefineField(info.Name, info.Type, FieldAttributes.Public);
        }

        protected override FieldBuilder DefinePropertyField(TypeBuilder builder, AnonymousTypePropertyInfo info)
        {
            if (builder is null)
            {
                throw new ArgumentNullException(nameof(builder));
            }

            return builder.DefineField($"_{info.Name.ToLowerInvariant()}", info.Type, FieldAttributes.Private | FieldAttributes.SpecialName);
        }

        protected override Boolean DefineGetMethod(TypeBuilder builder, PropertyBuilder property, FieldBuilder field, AnonymousTypePropertyInfo info)
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

        protected override Boolean DefineSetMethod(TypeBuilder builder, PropertyBuilder property, FieldBuilder field, AnonymousTypePropertyInfo info)
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

        protected override (PropertyBuilder Property, FieldBuilder Field) DefineProperty(TypeBuilder builder, AnonymousTypePropertyInfo info)
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

        protected override (PropertyBuilder? Property, FieldBuilder Field) Define(TypeBuilder builder, AnonymousTypePropertyInfo info)
        {
            if (builder is null)
            {
                throw new ArgumentNullException(nameof(builder));
            }

            return info.Read is not null || info.Write is not null ? DefineProperty(builder, info) : (null, DefineField(builder, info));
        }

        //TODO:
        /*protected override MethodBuilder DefineEquals(TypeBuilder builder, (PropertyBuilder? Property, FieldBuilder Field)[] fields)
        {
            if (builder is null)
            {
                throw new ArgumentNullException(nameof(builder));
            }

            if (fields is null)
            {
                throw new ArgumentNullException(nameof(fields));
            }

            Type equatable = CodeGeneratorUtilities.Generator.Equatable.Interface.MakeGenericType(builder);
            builder.AddInterfaceImplementation(equatable);

            const MethodAttributes attributes = MethodAttributes.Public | MethodAttributes.Virtual;
            MethodBuilder method = builder.DefineMethod(nameof(Equals), attributes, CallingConventions.HasThis, typeof(Boolean), new Type[] { builder });
            method.SetCustomAttribute(CodeGeneratorUtilities.Generator.Attribute.DebuggerHiddenAttribute);

            method.DefineParameter(1, ParameterAttributes.None, "value");

            MethodInfo equals = TypeBuilder.GetMethod(equatable, CodeGeneratorUtilities.Generator.Equatable.EqualsMethod);
            builder.DefineMethodOverride(method, equals);

            ILGenerator generator = method.GetILGenerator();

            Label @false = generator.DefineLabel();
            Label exit = generator.DefineLabel();

            generator.Emit(OpCodes.Ldarg_1);
            generator.Emit(OpCodes.Isinst, builder);
            generator.Emit(OpCodes.Brfalse_S, @false);

            generator.DeclareLocal(builder);

            generator.Emit(OpCodes.Ldarg_1);
            generator.Emit(OpCodes.Castclass, builder);
            generator.Emit(OpCodes.Stloc_0);
            generator.Emit(OpCodes.Ldloc_0);

            if (fields.Length <= 0)
            {
                generator.Emit(OpCodes.Ldnull);
                generator.Emit(OpCodes.Ceq);
                generator.Emit(OpCodes.Ldc_I4_0);
                generator.Emit(OpCodes.Ceq);
                generator.Emit(OpCodes.Br_S, exit);

                generator.MarkLabel(@false);
                generator.Emit(OpCodes.Ldc_I4_0);
                generator.Emit(OpCodes.Ret);

                generator.MarkLabel(exit);
                generator.Emit(OpCodes.Ret);

                return method;
            }

            Label label = generator.DefineLabel();

            foreach ((PropertyBuilder? _, FieldBuilder? field) in fields)
            {
                Type type = field.FieldType;
                MethodInfo @default = CodeGeneratorUtilities.Generator.Equality.TakeDefault(type);
                MethodInfo equalsmethod = CodeGeneratorUtilities.Generator.Equality.TakeEquals(type);

                generator.Emit(OpCodes.Brfalse_S, label);
                generator.Emit(OpCodes.Call, @default);
                generator.Emit(OpCodes.Ldarg_0);
                generator.Emit(OpCodes.Ldfld, field);
                generator.Emit(OpCodes.Ldloc_0);
                generator.Emit(OpCodes.Ldfld, field);
                generator.Emit(OpCodes.Callvirt, equalsmethod);
            }

            generator.Emit(OpCodes.Br_S, exit);
            generator.MarkLabel(@false);
            generator.Emit(OpCodes.Ldc_I4_0);
            generator.Emit(OpCodes.Ret);

            generator.MarkLabel(label);
            generator.Emit(OpCodes.Ldc_I4_0);
            generator.Emit(OpCodes.Ret);

            generator.MarkLabel(exit);
            generator.Emit(OpCodes.Ret);

            return method;
        }*/

        protected override MethodBuilder DefineToString(TypeBuilder builder, (PropertyBuilder? Property, FieldBuilder Field)[] fields)
        {
            if (builder is null)
            {
                throw new ArgumentNullException(nameof(builder));
            }

            if (fields is null)
            {
                throw new ArgumentNullException(nameof(fields));
            }

            const MethodAttributes attributes = MethodAttributes.Public | MethodAttributes.Virtual | MethodAttributes.HideBySig;
            MethodBuilder method = builder.DefineMethod(nameof(ToString), attributes, CallingConventions.HasThis, typeof(String), Type.EmptyTypes);
            method.SetCustomAttribute(CodeGeneratorUtilities.Generator.Attribute.DebuggerHiddenAttribute);
            ILGenerator generator = method.GetILGenerator();

            if (fields.Length <= 0)
            {
                generator.Emit(OpCodes.Nop);
                generator.Emit(OpCodes.Ldstr, "{ }");
                generator.Emit(OpCodes.Stloc_0);
                generator.Emit(OpCodes.Br_S);
                generator.Emit(OpCodes.Ldloc_0);
                generator.Emit(OpCodes.Ret);

                return method;
            }

            generator.DeclareLocal(typeof(StringBuilder));

            generator.Emit(OpCodes.Newobj, CodeGeneratorUtilities.Generator.StringBuilder.Constructor);
            generator.Emit(OpCodes.Stloc_0);

            for (Int32 i = 0; i < fields.Length; i++)
            {
                (PropertyBuilder? property, FieldBuilder field) = fields[i];
                String name = property?.Name ?? field.Name;

                generator.Emit(OpCodes.Ldloc_0);
                generator.Emit(OpCodes.Ldstr, i <= 0 ? $"{{ {name} = " : $", {name} = ");
                generator.Emit(OpCodes.Callvirt, CodeGeneratorUtilities.Generator.StringBuilder.String);
                generator.Emit(OpCodes.Pop);
                generator.Emit(OpCodes.Ldloc_0);
                generator.Emit(OpCodes.Ldarg_0);
                generator.Emit(OpCodes.Ldfld, field);

                Type type = field.FieldType;
                if (!CodeGeneratorUtilities.Generator.StringBuilder.Take(type, out MethodInfo info) && type.IsValueType)
                {
                    generator.Emit(OpCodes.Box, type);
                }

                generator.Emit(OpCodes.Callvirt, info);
                generator.Emit(OpCodes.Pop);
            }

            generator.Emit(OpCodes.Ldloc_0);
            generator.Emit(OpCodes.Ldstr, " }");
            generator.Emit(OpCodes.Callvirt, CodeGeneratorUtilities.Generator.StringBuilder.String);
            generator.Emit(OpCodes.Pop);
            generator.Emit(OpCodes.Ldloc_0);
            generator.Emit(OpCodes.Callvirt, CodeGeneratorUtilities.Generator.Object.ToStringMethod);
            generator.Emit(OpCodes.Ret);

            return method;
        }

        public override Type DefineType(AnonymousTypePropertyInfo[] properties)
        {
            return DefineType<AnonymousObject>(properties);
        }

        protected override Type DefineType<TParent>(AnonymousTypePropertyInfo[] properties)
        {
            if (properties is null)
            {
                throw new ArgumentNullException(nameof(properties));
            }

            if (properties.Length <= 0)
            {
                return typeof(TParent);
            }

            lock (Assembly)
            {
                String typename = GenerateTypeName(properties);
                if (Assembly.GetType(typename, false, false) is Type type)
                {
                    return type;
                }

                TypeBuilder builder = DefineType<TParent>(typename);

                (PropertyBuilder? Property, FieldBuilder Field)[] fields = new (PropertyBuilder? Property, FieldBuilder Field)[properties.Length];
                for (Int32 i = 0; i < properties.Length; i++)
                {
                    AnonymousTypePropertyInfo info = properties[i];
                    fields[i] = Define(builder, info);
                }

                DefineConstructor(builder, fields);
                DefineMethods(builder, fields);
                return builder.CreateType() ?? throw new InvalidOperationException($"Can't create type '{typename}'");
            }
        }

        public override IAnonymousActivatorInfo CreateActivator(AnonymousTypePropertyInfo[] properties)
        {
            if (properties is null)
            {
                throw new ArgumentNullException(nameof(properties));
            }

            Type type = DefineType(properties);
            
            ConstructorInfo[] constructors = type.GetConstructors();
            ConstructorInfo constructor = constructors.Length switch
            {
                0 => throw new MissingMethodException(type.Name, ".ctor"),
                1 => constructors[0],
                2 => constructors[1],
                _ => throw new AmbiguousMatchException(type.Name)
            };
            
            Type[] parameters = constructor.GetParameters().Select(parameter => parameter.ParameterType).ToArray();
            return AnonymousActivator.Create(type, parameters);
        }
    }
}