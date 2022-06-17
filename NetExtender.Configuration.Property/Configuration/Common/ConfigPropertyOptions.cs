// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;

namespace NetExtender.Configuration.Common
{
    [Flags]
    public enum ConfigPropertyOptions
    {
        None = 0,
        Caching = 1,
        ThrowWhenValueSetInvalid = 2,
        ReadOnly = 4,
        IgnoreEvent = 8,
        DisableSave = 16,
        AlwaysDefault = Caching | ReadOnly | IgnoreEvent | DisableSave
    }
}