// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;

namespace NetExtender.Configuration.Common
{
    [Flags]
    public enum ConfigPropertyOptions : Byte
    {
        None = 0,
        Initialize = 1,
        Caching = 2,
        ThrowWhenValueSetInvalid = 4,
        ReadOnly = 8,
        IgnoreEvent = 16,
        DisableSave = 32,
        AlwaysDefault = Caching | ReadOnly | IgnoreEvent | DisableSave
    }
}