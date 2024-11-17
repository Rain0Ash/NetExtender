using System.Runtime.CompilerServices;
using NetExtender.Patch;

namespace NetExtender.DependencyInjection
{
    internal static class NetExtenderEntityFrameworkCoreInitializer
    {
#pragma warning disable CA2255
        [ModuleInitializer]
        public static void Initialize()
        {
            Initializer.Initializer.Module(Patch);
        }
#pragma warning restore CA2255
        
        private static void Patch()
        {
            EntityFrameworkCoreZPatch.Auto();
        }
    }
}