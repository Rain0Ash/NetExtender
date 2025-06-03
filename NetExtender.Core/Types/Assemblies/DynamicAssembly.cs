// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Reflection;
using System.Reflection.Emit;
using NetExtender.Types.Assemblies.Interfaces;
using NetExtender.Types.Exceptions;

namespace NetExtender.Types.Assemblies
{
    public class DynamicAssembly : IDynamicAssembly
    {
        public AssemblyName Name { get; }
        public AssemblyBuilder Assembly { get; }
        public ModuleBuilder Module { get; }
        
        public DynamicAssembly()
            : this(AssemblyBuilderAccess.Run)
        {
        }
        
        public DynamicAssembly(AssemblyBuilderAccess access)
            : this(DynamicInitializerAssembly.Guid(), access)
        {
        }
        
        public DynamicAssembly(String name, AssemblyBuilderAccess access)
        {
            if (String.IsNullOrEmpty(name))
            {
                throw new ArgumentNullOrEmptyStringException(name, nameof(name));
            }
            
            Name = new AssemblyName(name);
            DynamicInitializerAssembly.Initialize(Name, access, out AssemblyBuilder assembly, out ModuleBuilder module);
            Assembly = assembly;
            Module = module;
        }
    }
}