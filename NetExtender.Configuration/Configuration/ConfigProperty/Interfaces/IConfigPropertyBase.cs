// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using NetExtender.Crypto;
using NetExtender.Crypto.CryptKey.Interfaces;

namespace NetExtender.Configuration
{
    public interface IConfigPropertyBase : IReadOnlyConfigPropertyBase, IDisposable
    {
        public new CryptAction Crypt { get; set; }
        public new ICryptKey CryptKey { get; set; }
        public new Boolean Caching { get; set; }
        public new Boolean IsReadOnly { get; set; }
        public new Boolean AlwaysDefault { get; set; }
        public new ConfigPropertyOptions Options { get; set; }
        public void Save();
        public void Reset();
    }
}