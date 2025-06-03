// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

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
