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