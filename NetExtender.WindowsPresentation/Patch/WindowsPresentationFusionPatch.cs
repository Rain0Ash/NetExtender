using System;
using NetExtender.Types.Reflection;

namespace NetExtender.Patch
{
    public partial class WindowsPresentationFusionPatch : AutoReflectionPatch<WindowsPresentationFusionPatch>
    {
        protected static Func<Patch> Factory { get; set; }

        static WindowsPresentationFusionPatch()
        {
            Factory = static () => new Patch();
        }

        protected sealed override Patch Create()
        {
            return Factory();
        }
    }
}