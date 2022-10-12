// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using NetExtender.Types.Strings.Interfaces;
using NetExtender.Utilities.Types;

namespace NetExtender.Types.Strings
{
    public abstract class FormatStringBase : StringBase, IFormatString
    {
        public abstract Int32 Arguments { get; }

        public virtual String Format(params Object[] format)
        {
            return Format(null, format);
        }

        public virtual String Format(IFormatProvider? provider, params Object[] format)
        {
            if (Arguments <= 0)
            {
                return NonFormatToString(provider);
            }
            
            if (format is null)
            {
                throw new ArgumentNullException(nameof(format));
            }

            if (format.Length != Arguments)
            {
                throw new ArgumentOutOfRangeException(nameof(format));
            }

            return NonFormatToString(provider).Format(provider, format);
        }

        public virtual IContainerFormatString ToContainer(params Object[] format)
        {
            return ContainerFormatStringAdapter.Create(this, format);
        }

        protected String NonFormatToString()
        {
            return base.ToString();
        }

        protected String NonFormatToString(IFormatProvider? provider)
        {
            return base.ToString(provider);
        }

        public override String ToString()
        {
            return NonFormatToString();
        }
        
        public override String ToString(IFormatProvider? provider)
        {
            return NonFormatToString(provider);
        }
    }
}