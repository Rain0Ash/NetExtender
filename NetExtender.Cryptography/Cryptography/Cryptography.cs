// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Collections.Immutable;
using System.IO;
using System.Text;

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
        public static ImmutableArray<Byte> DefaultHash { get; } = Hashing(Encoding.UTF8.GetBytes(Path.Join(Environment.MachineName, Environment.UserName)), HashType.MD5).ToImmutableArray();
    }
}