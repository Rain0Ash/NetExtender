// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using NetExtender.Cryptography.Keys.Interfaces;
using NetExtender.Utilities.Cryptography;

namespace NetExtender.Configuration.Cryptography.Common.Interfaces
{
    public interface IConfigurationCryptor : IStringCryptor, IConfigurationCryptographyInfo
    {
        public new CryptAction Crypt { get; }
    }
}