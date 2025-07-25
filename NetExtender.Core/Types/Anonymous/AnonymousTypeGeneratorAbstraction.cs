// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Reflection.Emit;
using NetExtender.Types.Anonymous.Interfaces;
using NetExtender.Types.Reflection;
using NetExtender.Utilities.Types;

namespace NetExtender.Types.Anonymous
{
    public abstract class AnonymousTypeGeneratorAbstraction : ReflectionTypeGeneratorAbstraction
    {
        protected AnonymousTypeGeneratorAbstraction(String assembly)
            : base(assembly)
        {
        }

        protected AnonymousTypeGeneratorAbstraction(AssemblyName assemblyname, AssemblyBuilder assembly, String modulename, ModuleBuilder module)
            : base(assemblyname, assembly, modulename, module)
        {
        }

        protected abstract String GenerateTypeName(AnonymousTypePropertyInfo[] properties);
        protected abstract TypeBuilder DefineType<TParent>(String typename) where TParent : class;

        protected virtual ConstructorBuilder[]? DefineConstructor(TypeBuilder builder, (PropertyBuilder? Property, FieldBuilder Field)[] fields)
        {
            return null;
        }

        protected abstract FieldBuilder DefineField(TypeBuilder builder, AnonymousTypePropertyInfo info);
        protected abstract FieldBuilder DefinePropertyField(TypeBuilder builder, AnonymousTypePropertyInfo info);
        protected abstract Boolean DefineGetMethod(TypeBuilder builder, PropertyBuilder property, FieldBuilder field, AnonymousTypePropertyInfo info);
        protected abstract Boolean DefineSetMethod(TypeBuilder builder, PropertyBuilder property, FieldBuilder field, AnonymousTypePropertyInfo info);

        protected virtual void DefineAccessors(TypeBuilder builder, PropertyBuilder property, FieldBuilder field, AnonymousTypePropertyInfo info)
        {
            DefineGetMethod(builder, property, field, info);
            DefineSetMethod(builder, property, field, info);
        }

        protected abstract (PropertyBuilder Property, FieldBuilder Field) DefineProperty(TypeBuilder builder, AnonymousTypePropertyInfo info);
        protected abstract (PropertyBuilder? Property, FieldBuilder Field) Define(TypeBuilder builder, AnonymousTypePropertyInfo info);

        protected virtual MethodBuilder? DefineGetHashCode(TypeBuilder builder, (PropertyBuilder? Property, FieldBuilder Field)[] fields)
        {
            return null;
        }

        protected virtual MethodBuilder? DefineEquals(TypeBuilder builder, (PropertyBuilder? Property, FieldBuilder Field)[] fields)
        {
            return null;
        }

        protected virtual MethodBuilder? DefineToString(TypeBuilder builder, (PropertyBuilder? Property, FieldBuilder Field)[] fields)
        {
            return null;
        }

        protected virtual KeyValuePair<String, MethodBuilder>[] DefineMethods(TypeBuilder builder, (PropertyBuilder? Property, FieldBuilder Field)[] fields)
        {
            if (builder is null)
            {
                throw new ArgumentNullException(nameof(builder));
            }

            if (fields is null)
            {
                throw new ArgumentNullException(nameof(fields));
            }

            IEnumerable<KeyValuePair<String, MethodBuilder?>> Core()
            {
                yield return new KeyValuePair<String, MethodBuilder?>(nameof(GetHashCode), DefineGetHashCode(builder, fields));
                yield return new KeyValuePair<String, MethodBuilder?>(nameof(Equals), DefineEquals(builder, fields));
                yield return new KeyValuePair<String, MethodBuilder?>(nameof(ToString), DefineToString(builder, fields));
            }

            return Core().WhereValueNotNull().ToArray();
        }

        public abstract Type DefineType(AnonymousTypePropertyInfo[] properties);
        protected abstract Type DefineType<TParent>(AnonymousTypePropertyInfo[] properties) where TParent : class;
        public abstract IAnonymousActivatorInfo CreateActivator(AnonymousTypePropertyInfo[] properties);
    }
}