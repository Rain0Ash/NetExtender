// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

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