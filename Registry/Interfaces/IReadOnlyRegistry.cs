// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Collections.Generic;

namespace NetExtender.Registry.Interfaces
{
    public interface IReadOnlyRegistry
    {
        public Boolean IsReadOnly { get; }
        public String Path { get; }
        public RegistryKeys RegistryKey { get; }
        public String GetValue(String key, params String[] sections);
        public String GetValue(String key, IEnumerable<String> sections);
        public String[] GetValueNames(params String[] sections);
        public String[] GetValueNames(IEnumerable<String> sections);
        public String[] GetSubKeyNames(params String[] sections);
        public String[] GetSubKeyNames(IEnumerable<String> sections);
        public Boolean KeyExist(String key, params String[] sections);
        public Boolean KeyExist(String key, IEnumerable<String> sections);
    }
}