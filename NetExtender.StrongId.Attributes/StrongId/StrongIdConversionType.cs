using System;

namespace NetExtender.StrongId
{
    /// <summary>
    /// The type of conversion operator for strongly-typed ID values
    /// </summary>
    [Flags]
    public enum StrongIdConversionType : Byte
    {
        None = 0,
        ToImplicit = 1,
        ToExplicit = 2 | ToImplicit,
        FromImplicit = 4,
        FromExplicit = 8 | FromImplicit,
        Implicit = ToImplicit | FromImplicit,
        Explicit = ToExplicit | FromExplicit
    }
}
