// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

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