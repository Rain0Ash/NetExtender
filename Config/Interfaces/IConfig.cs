// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using NetExtender.Converters;
using NetExtender.Crypto;
using NetExtender.Crypto.CryptKey.Interfaces;

namespace NetExtender.Config.Interfaces
{
    public interface IConfig : IReadOnlyConfig
    {
        public new Boolean ThrowOnReadOnly { get; set; }
        public new Boolean CryptByDefault { get; set; }
        public new ConfigPropertyOptions DefaultOptions { get; set; }
        public void SetValue<T>(String key, T value, params String[] sections);
        public void SetValue<T>(String key, T value, ICryptKey crypt, params String[] sections);
        public String GetOrSetValue(String key, Object defaultValue, params String[] sections);
        public String GetOrSetValue(String key, Object defaultValue, CryptAction crypt, params String[] sections);
        public String GetOrSetValue(String key, Object defaultValue, CryptAction crypt, ICryptKey cryptKey, params String[] sections);
        public T GetOrSetValue<T>(String key, T defaultValue, params String[] sections);
        public T GetOrSetValue<T>(String key, T defaultValue, ICryptKey crypt, params String[] sections);
        public T GetOrSetValue<T>(String key, T defaultValue, ICryptKey crypt, TryConverter<String, T> converter, params String[] sections);
        public void RemoveValue(String key, params String[] sections);
    }
}