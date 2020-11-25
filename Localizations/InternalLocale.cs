// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using NetExtender.Config;
using NetExtender.Converters;
using NetExtender.Crypto;
using NetExtender.Crypto.CryptKey.Interfaces;
using ReactiveUI;

namespace NetExtender.Localizations
{
    internal class InternalLocale<T> : ReactiveObject, IReadOnlyConfigProperty<T>
    {
        public String Path { get; }
        public Config.Config Config { get; }
        public String Key { get; }
        public String[] Sections { get; }

        public CryptAction Crypt { get; }

        public ICryptKey CryptKey
        {
            get
            {
                return null;
            }
        }

        public Boolean Caching { get; }
        public Boolean IsReadOnly { get; }
        public Boolean AlwaysDefault { get; }
        public ConfigPropertyOptions Options { get; }
        public void Read()
        {
            throw new NotImplementedException();
        }

        public Boolean KeyExist()
        {
            throw new NotImplementedException();
        }

        public Boolean ThrowOnInvalid { get; }
        public T DefaultValue { get; }
        public T Value { get; }
        public Boolean IsValid { get; }
        public Func<T, Boolean> Validate { get; }
        public TryConverter<String, T> Converter { get; }
        public T GetValue()
        {
            throw new NotImplementedException();
        }

        public T GetValue(Func<T, Boolean> validate)
        {
            throw new NotImplementedException();
        }
    }
}