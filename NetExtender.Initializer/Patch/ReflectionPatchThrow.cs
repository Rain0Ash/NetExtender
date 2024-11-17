using System;

namespace NetExtender.Patch
{
    public enum ReflectionPatchThrow : Byte
    {
        Throw,
        Ignore,
        Log,
        LogThrow,
    }
}