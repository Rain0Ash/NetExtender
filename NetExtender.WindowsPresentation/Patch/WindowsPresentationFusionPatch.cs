// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

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