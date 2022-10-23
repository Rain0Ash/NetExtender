// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Collections.Generic;
using System.Reflection;

namespace NetExtender.Types.Anonymous.Interfaces
{
    public interface IAnonymousObject : IEnumerable<KeyValuePair<String, MemberInfo>>
    {
        public AnonymousObjectProperty this[Int32 index] { get; }
        public AnonymousObjectProperty this[String property] { get; }
    }
}