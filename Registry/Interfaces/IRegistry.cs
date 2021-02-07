// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Collections.Generic;

namespace NetExtender.Registry.Interfaces
{
    public interface IRegistry : IReadOnlyRegistry
    {
        public Boolean SetValue(String key, String value, params String[] sections);
        public Boolean SetValue(String key, String value, IEnumerable<String> sections);
        public Boolean RemoveKey(String key, params String[] sections);
        public Boolean RemoveKey(String key, IEnumerable<String> sections);
    }
}