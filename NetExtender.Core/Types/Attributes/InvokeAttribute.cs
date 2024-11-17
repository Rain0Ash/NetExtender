using System;
using NetExtender.Types.Attributes.Interfaces;

namespace NetExtender.Utilities.Core
{
    public abstract class InvokeAttribute : Attribute, IInvokeAttribute
    {
        public Boolean Invoke()
        {
            return Invoke(null, null);
        }

        public Boolean Invoke(Object? value)
        {
            return Invoke(null, value);
        }
        
        public abstract Boolean Invoke(Object? sender, Object? value);
    }
}