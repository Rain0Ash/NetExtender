// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;

namespace NetExtender.Configuration.Cryptography.Common
{
    [Flags]
    public enum CryptographyConfigOptions
    {
        None = 0,
        Try = 1,
        CryptDefault = 2,
        CryptKey = 4,
        CryptValue = 8,
        CryptSections = 16,
        CryptConfig = 32,
        CryptAll = CryptKey | CryptValue | CryptSections | CryptConfig,
        TryCryptAll = Try | CryptAll,
        All = CryptDefault | CryptAll,
        TryAll = Try | All
    }
}