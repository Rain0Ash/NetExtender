using System;
using System.Reflection;
using System.Reflection.Emit;
using NetExtender.Types.Assemblies.Interfaces;

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
                throw new ArgumentException("Value cannot be null or empty.", nameof(name));
            }
            
            Name = new AssemblyName(name);
            DynamicInitializerAssembly.Initialize(Name, access, out AssemblyBuilder assembly, out ModuleBuilder module);
            Assembly = assembly;
            Module = module;
        }
    }
}