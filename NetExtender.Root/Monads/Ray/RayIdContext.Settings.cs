using System;

namespace NetExtender.Monads
{
    public partial struct RayIdContext
    {
        private static class Settings
        {
            internal const Int32 Size = RayId.Settings.Size + Context;
            internal const Int32 Context = sizeof(UInt64) + sizeof(UInt64) + sizeof(UInt64);
        }
    }
}