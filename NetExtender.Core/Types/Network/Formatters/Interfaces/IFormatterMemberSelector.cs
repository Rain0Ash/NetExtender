// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Reflection;

namespace NetExtender.Types.Network.Formatters.Interfaces
{
    public interface IFormatterMemberSelector
    {
        public Boolean IsRequired(MemberInfo member);
    }
}