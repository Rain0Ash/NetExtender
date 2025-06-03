using System;

namespace NetExtender
{
    public interface IEqualityStruct<T> : IEquatableStruct<T>, IComparableStruct<T>, IEquality<T> where T : struct
    {
    }
    
    public interface IComparableStruct<T> : IStruct<T>, IComparable<T> where T : struct
    {
    }
    
    public interface IEquatableStruct<T> : IStruct<T>, IEquatable<T> where T : struct
    {
    }
    
    public interface IStruct<T> : IStruct where T : struct
    {
    }

    public interface IStruct
    {
        public Boolean IsEmpty { get; }
    }
}