using System;
using System.Runtime.CompilerServices;

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
        public static T Empty
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                return default;
            }
        }
    }

    public interface IStruct
    {
        public Boolean IsEmpty { get; }
    }
}