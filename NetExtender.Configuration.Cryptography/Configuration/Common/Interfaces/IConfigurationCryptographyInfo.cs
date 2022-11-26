// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using NetExtender.Utilities.Cryptography;

namespace NetExtender.Configuration.Cryptography.Common.Interfaces
{
    public interface IConfigurationCryptographyInfo
    {
        public CryptAction Crypt { get; }
        public CryptographyConfigOptions CryptographyOptions { get; }

        public Boolean IsCryptDefault { get; }
        public Boolean IsCryptKey { get; }
        public Boolean IsCryptValue { get; }
        public Boolean IsCryptSections { get; }
        public Boolean IsCryptConfig { get; }
        public Boolean IsCryptAll { get; }
    }
}