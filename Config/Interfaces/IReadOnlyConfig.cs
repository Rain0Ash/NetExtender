// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using NetExtender.Config.Common;
using NetExtender.Converters;
using NetExtender.Crypto.CryptKey.Interfaces;

namespace NetExtender.Config.Interfaces
{
    public interface IReadOnlyConfig : IDisposable
    {
        public String Path { get; }
        public ConfigOptions Options { get; }
        public Boolean IsReadOnly { get; }
        public Boolean ThrowOnReadOnly { get; set; }
        public Boolean CryptByDefault { get; set; }
        public ConfigPropertyOptions DefaultOptions { get; set; }
        public String GetValue(String key, params String[] sections);
        public String GetValue(String key, String defaultValue, params String[] sections);
        public String GetValue(String key, Object defaultValue, params String[] sections);
        public String GetValue(String key, Object defaultValue, Boolean decrypt, params String[] sections);
        public String GetValue(String key, Object defaultValue, Boolean decrypt, ICryptKey crypt, params String[] sections);
        public T GetValue<T>(String key, params String[] sections);
        public T GetValue<T>(String key, T defaultValue, params String[] sections);
        public T GetValue<T>(String key, T defaultValue, ICryptKey crypt, params String[] sections);
        public T GetValue<T>(String key, T defaultValue, ICryptKey crypt, TryConverter<String, T> converter, params String[] sections);
        public Boolean KeyExist(String key, params String[] sections);
    }
}