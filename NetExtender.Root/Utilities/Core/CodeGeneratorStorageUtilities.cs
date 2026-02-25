using System.Reflection;
using System.Reflection.Emit;
using NetExtender.Types.Storages;
using NetExtender.Types.Storages.Interfaces;

namespace NetExtender.Utilities.Core
{
    public static class CodeGeneratorStorageUtilities
    {
        public static class Parameters
        {
            public static IStorage<ConstructorBuilder, ParameterInfo[]> ConstructorBuilder { get; } = new WeakStorage<ConstructorBuilder, ParameterInfo[]>();
            public static IStorage<MethodBuilder, ParameterInfo[]> MethodBuilder { get; } = new WeakStorage<MethodBuilder, ParameterInfo[]>();
            public static IStorage<PropertyBuilder, ParameterInfo[]> PropertyBuilder { get; } = new WeakStorage<PropertyBuilder, ParameterInfo[]>();
        }
    }
}