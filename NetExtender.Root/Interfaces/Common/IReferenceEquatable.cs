using System;

namespace NetExtender.Interfaces
{
    public interface IReferenceEquatable<in T>
    {
        public Boolean ReferenceEquals(T? other);
    }
}