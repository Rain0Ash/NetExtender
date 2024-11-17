using System;
using NetExtender.Types.Reflection;

namespace NetExtender.Patch
{
    public partial class WindowsPresentationCommandSenderPatch : AutoReflectionPatch<WindowsPresentationCommandSenderPatch>
    {
        protected static Func<Patch> Factory { get; set; }

        static WindowsPresentationCommandSenderPatch()
        {
            Factory = static () => new Patch();
        }

        protected sealed override Patch Create()
        {
            return Factory();
        }
    }
}