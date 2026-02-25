// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;

namespace NetExtender.Types.Attributes.Interfaces
{
    public interface IInvokeAttribute : IAttribute
    {
        public Boolean Invoke();
        public Boolean Invoke(Object? value);
        public Boolean Invoke(Object? sender, Object? value);
    }
}