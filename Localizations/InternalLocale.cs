// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using NetExtender.Configuration;
using NetExtender.Configuration.Interfaces;
using NetExtender.Converters;
using NetExtender.Crypto;
using NetExtender.Crypto.CryptKey.Interfaces;
using ReactiveUI;

namespace NetExtender.Localizations
{
    internal class InternalLocale : ReactiveObject, IReadOnlyConfigProperty<String>
    {
        public String Path { get; }
        public IPropertyConfig Config { get; }
        public String Key { get; }
        public String[] Sections { get; }
        public CryptAction Crypt { get; }
        public ICryptKey CryptKey { get; }
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
        public String DefaultValue { get; }
        public String Value { get; }
        public Boolean IsValid { get; }
        public Func<String, Boolean> Validate { get; }
        public TryConverter<String, String> Converter { get; }
        public String GetValue()
        {
            throw new NotImplementedException();
        }

        public String GetValue(Func<String, Boolean> validate)
        {
            throw new NotImplementedException();
        }
    }
}