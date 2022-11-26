// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Reflection;
using System.Runtime.Serialization;
using NetExtender.Utilities.Serialization;

namespace NetExtender.Types.Exceptions
{
    [Serializable]
    public class ModuleNotInitializedException : AssemblyNotInitializedException
    {
        public Module Module { get; }

        public ModuleNotInitializedException(Module module)
            : this(module, null)
        {
        }

        public ModuleNotInitializedException(Module module, String? message)
            : base(module?.Assembly ?? throw new ArgumentNullException(nameof(module)), message ?? $"Module {module.Assembly.GetName().Name} is not initialized")
        {
            Module = module ?? throw new ArgumentNullException(nameof(module));
        }

        protected ModuleNotInitializedException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
            Module = info.GetValue<Module>(nameof(Module));
        }
    }

    [Serializable]
    public class AssemblyNotInitializedException : NotInitializedException
    {
        public Assembly Assembly { get; }

        public AssemblyNotInitializedException(Assembly assembly)
            : this(assembly, null)
        {
        }

        public AssemblyNotInitializedException(Assembly assembly, String? message)
            : base(assembly is not null ? message ?? $"Assembly {assembly.GetName().Name} is not initialized" : throw new ArgumentNullException(nameof(assembly)))
        {
            Assembly = assembly ?? throw new ArgumentNullException(nameof(assembly));
        }

        protected AssemblyNotInitializedException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
            Assembly = info.GetValue<Assembly>(nameof(Assembly));
        }
    }
}