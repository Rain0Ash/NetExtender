// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;

namespace NetExtender.Configuration.Common
{
    [Flags]
    public enum ConfigOptions
    {
        None = 0,
        ReadOnly = 1,
        LazyWrite = 2,
        CryptData = 4,
        CryptConfig = 8,
        CryptAll = CryptData | CryptConfig,
    }
}