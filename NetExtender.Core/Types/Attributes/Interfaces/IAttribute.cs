using System;

namespace NetExtender.Types.Attributes.Interfaces
{
    public interface IAttribute
    {
        public Object TypeId { get; }
        public Boolean Match(Object? other);
        public Boolean IsDefaultAttribute();
    }
}