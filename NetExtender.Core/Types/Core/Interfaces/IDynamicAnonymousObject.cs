// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Collections.Generic;
using System.Reflection;

namespace NetExtender.Initializer.Types.Core.Interfaces
{
    public interface IDynamicAnonymousObject : IEnumerable<KeyValuePair<String, MemberInfo>>
    {
        public DynamicAnonymousObjectProperty this[Int32 index] { get; }
        public DynamicAnonymousObjectProperty this[String property] { get; }
    }
}