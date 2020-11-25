// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Collections.Immutable;
using System.Text;
using NetExtender.Workstation;

namespace NetExtender.Crypto
{
    [Flags]
    public enum CryptAction
    {
        None = 0,
        Decrypt = 1,
        Encrypt = 2,
        Crypt = 3
    }

    public static partial class Cryptography
    {
        public static ImmutableArray<Byte> DefaultHash { get; } = CurrentUserCryptoSIDHash().ToImmutableArray();

        // ReSharper disable once ReturnTypeCanBeEnumerable.Global
        public static Byte[] CurrentUserCryptoSIDHash(HashType hash = Hash.DefaultHashType)
        {
            return Hashing(Encoding.UTF8.GetBytes(WorkStation.CurrentUserSID), hash);
        }
    }
}