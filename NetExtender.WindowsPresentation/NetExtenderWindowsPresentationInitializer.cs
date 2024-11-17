using System.Runtime.CompilerServices;
using NetExtender.Patch;

namespace NetExtender.DependencyInjection
{
    internal static class NetExtenderWindowsPresentationInitializer
    {
#pragma warning disable CA2255
        [ModuleInitializer]
        public static void Initialize()
        {
            Initializer.Initializer.Module(Patch);
        }
        
        private static void Patch()
        {
            WindowsPresentationFusionPatch.Auto();
            WindowsPresentationCommandSenderPatch.Auto();
        }
#pragma warning restore CA2255
    }
}