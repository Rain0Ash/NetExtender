// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Collections.Generic;
using NetExtender.Interfaces;

namespace NetExtender.Crypto.CryptKey.Interfaces
{
    public interface ICryptKey : ICloneable<ICryptKey>, IDisposable
    {
        public Int32 KeySize { get; }
        
        public CryptAction Crypt { get; set; }
        
        public Boolean IsEncrypt { get; }
        public Boolean IsDecrypt { get; }

        public String Encrypt(String value);

        public String EncryptString(String value);
        
        public IEnumerable<String> Encrypt(IEnumerable<String> source);

        public IEnumerable<String> EncryptString(IEnumerable<String> source);

        public Byte[] Encrypt(Byte[] value);
        
        public Byte[] EncryptBytes(Byte[] value);

        public String Decrypt(String value);
        
        public String DecryptString(String value);
        
        public IEnumerable<String> Decrypt(IEnumerable<String> source);

        public IEnumerable<String> DecryptString(IEnumerable<String> source);

        public Byte[] Decrypt(Byte[] value);
        
        public Byte[] DecryptBytes(Byte[] value);

        public ICryptKey Clone(CryptAction crypt);
    }
}