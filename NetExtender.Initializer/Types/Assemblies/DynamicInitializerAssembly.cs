// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Reflection;
using System.Reflection.Emit;
using NetExtender.Types.Assemblies.Interfaces;

namespace NetExtender.Types.Assemblies
{
    internal sealed class DynamicInitializerAssembly : IDynamicAssembly
    {
        public AssemblyName Name { get; }
        public AssemblyBuilder Assembly { get; }
        public ModuleBuilder Module { get; }
        
        internal DynamicInitializerAssembly(AssemblyName name, AssemblyBuilder assembly, ModuleBuilder module)
        {
            Name = name ?? throw new ArgumentNullException(nameof(name));
            Assembly = assembly ?? throw new ArgumentNullException(nameof(assembly));
            Module = module ?? throw new ArgumentNullException(nameof(module));
        }
        
        public DynamicInitializerAssembly(AssemblyBuilderAccess access)
            : this(Guid(), access)
        {
        }
        
        public DynamicInitializerAssembly(String name, AssemblyBuilderAccess access)
        {
            if (name is null)
            {
                throw new ArgumentNullException(nameof(name));
            }

            if (String.IsNullOrEmpty(name))
            {
                throw new ArgumentException("Value cannot be empty.", nameof(name));
            }

            Name = new AssemblyName(name);
            Initialize(Name, access, out AssemblyBuilder assembly, out ModuleBuilder module);
            Assembly = assembly;
            Module = module;
        }
        
        internal static String Guid()
        {
            return System.Guid.NewGuid().ToString();
        }
        
        internal static void Initialize(AssemblyName name, AssemblyBuilderAccess access, out AssemblyBuilder assembly, out ModuleBuilder module)
        {
            assembly = AssemblyBuilder.DefineDynamicAssembly(name, access);
            module = assembly.DefineDynamicModule(name.Name ?? name.FullName);
        }
    }
}