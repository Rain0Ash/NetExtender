using System;
using NetExtender.Types.Reflection;

namespace NetExtender.Patch
{
    public partial class WindowsPresentationPatch : ReflectionPatch<WindowsPresentationPatch>
    {
        protected static Func<Patch> Factory { get; set; }

        static WindowsPresentationPatch()
        {
            Factory = static () => new Patch();
        }

        protected sealed override Patch Create()
        {
            return Factory();
        }
    }
}