// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using NetExtender.Crypto;
using NetExtender.Crypto.CryptKey.Interfaces;
using ReactiveUI;

namespace NetExtender.Config
{
    public interface IReadOnlyConfigPropertyBase : IReactiveObject
    {
        public String Path { get; }
        public Config Config { get; }
        public String Key { get; }
        public String[] Sections { get; }
        public CryptAction Crypt { get; }
        public ICryptKey CryptKey { get; }
        public Boolean Caching { get; }
        public Boolean IsReadOnly { get; }
        public Boolean AlwaysDefault { get; }
        public ConfigPropertyOptions Options { get; }

        public void Read();

        public Boolean KeyExist();
    }
}