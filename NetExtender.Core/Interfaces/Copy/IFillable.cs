using System;

namespace NetExtender.Interfaces
{
    public interface IFillable<in T>
    {
        public Boolean From(T other);
    }
}