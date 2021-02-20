// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using JetBrains.Annotations;

namespace NetExtender.Types.Strings.Interfaces
{
    public interface IFormatString : IString
    {
        public Int32 Arguments { get; }

        public String Format();
        public String Format([CanBeNull] IFormatProvider? provider);
        public String Format(params Object[] format);
        public String Format([CanBeNull] IFormatProvider? provider, params Object[] format);
    }
}