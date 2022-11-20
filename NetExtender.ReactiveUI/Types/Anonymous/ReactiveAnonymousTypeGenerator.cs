// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Reflection;
using System.Reflection.Emit;
using NetExtender.ReactiveUI.Utilities;
using NetExtender.Types.Anonymous;
using NetExtender.Utilities.Types;

namespace NetExtender.ReactiveUI.Anonymous.Core
{
    public class ReactiveAnonymousTypeGenerator : AnonymousTypeGenerator
    {
        public ReactiveAnonymousTypeGenerator(String assembly)
            : base(assembly)
        {
        }

        protected ReactiveAnonymousTypeGenerator(AssemblyName assemblyname, AssemblyBuilder assembly, String modulename, ModuleBuilder module)
            : base(assemblyname, assembly, modulename, module)
        {
        }
        
        protected override String GenerateTypeName(AnonymousTypePropertyInfo[] properties)
        {
            if (properties is null)
            {
                throw new ArgumentNullException(nameof(properties));
            }

            return $"<>f__ReactiveAnonymousType<{HashCodeUtilities.Combine(properties)}>";
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
            builder.DefineReactiveSetMethod(property, field, typeof(ReactiveAnonymousObject), info.Name, info.Type, attributes | (info.Write.Value ? MethodAttributes.Public : MethodAttributes.Private));
            return true;
        }

        public override Type DefineType(AnonymousTypePropertyInfo[] properties)
        {
            return DefineType<ReactiveAnonymousObject>(properties);
        }
    }
}