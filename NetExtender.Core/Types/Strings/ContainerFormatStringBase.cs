// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using NetExtender.Types.Strings.Interfaces;
using NetExtender.Utils.Types;

namespace NetExtender.Types.Strings
{
    public abstract class ContainerFormatStringBase : FormatStringBase, IContainerFormatString
    {
        protected abstract Object[] FormatArguments { get; }

        public virtual String Format()
        {
            return Format(null);
        }
        
        public virtual String Format(IFormatProvider? provider)
        {
            return Arguments > 0 ? Format(provider, StringUtils.FormatSafeGetArguments(FormatArguments, Arguments)) : NonFormatToString(provider);
        }
        
        public override String ToString()
        {
            return Format() ?? String.Empty;
        }
        
        public override String ToString(IFormatProvider? provider)
        {
            return Format(provider) ?? String.Empty;
        }
    }
}