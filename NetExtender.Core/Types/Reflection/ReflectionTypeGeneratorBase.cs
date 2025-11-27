// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Reflection;
using System.Reflection.Emit;

namespace NetExtender.Types.Reflection
{
    public abstract class ReflectionTypeGeneratorBase
    {
        protected AssemblyName AssemblyName { get; }
        protected AssemblyBuilder Assembly { get; }
        protected String ModuleName { get; }
        protected ModuleBuilder Module { get; }

        protected ReflectionTypeGeneratorBase(String assembly)
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

        protected ReflectionTypeGeneratorBase(AssemblyName assemblyname, AssemblyBuilder assembly, String modulename, ModuleBuilder module)
        {
            AssemblyName = assemblyname ?? throw new ArgumentNullException(nameof(assemblyname));
            Assembly = assembly ?? throw new ArgumentNullException(nameof(assembly));
            ModuleName = modulename ?? throw new ArgumentNullException(nameof(modulename));
            Module = module ?? throw new ArgumentNullException(nameof(module));
        }
    }
}