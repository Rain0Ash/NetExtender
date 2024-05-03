// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

namespace System.Security.Cryptography
{
    public abstract class SHA224 : HashAlgorithm
    {
        protected SHA224()
        {
            HashSizeValue = 224;
        }

        public new static SHA224 Create()
        {
            return Create("SHA224");
        }

        public new static SHA224 Create(String name)
        {
            return CryptoConfig.CreateFromName(name) as SHA224 ?? new SHA224Managed();
        }
    }
}