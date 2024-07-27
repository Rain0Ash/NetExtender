using System.Reflection;
using System.Reflection.Emit;

namespace NetExtender.Types.Assemblies.Interfaces
{
    public interface IDynamicAssembly
    {
        public AssemblyName Name { get; }
        public AssemblyBuilder Assembly { get; }
        public ModuleBuilder Module { get; }
    }
}