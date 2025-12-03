using System;
using System.Collections.Generic;

namespace NetExtender.Types.Vectors.Interfaces
{
    public interface IVector<T> : IVector, IReadOnlyList<T> where T : struct
    {
        public void Fill(T value);
        public void CopyTo(Span<T> destination);
        public Boolean TryCopyTo(Span<T> destination);

        public T[] ToArray();
        public T[] ToFullArray();

        public new T this[Int32 index] { get; set; }
    }

    public interface IVector : IStruct
    {
        public Int32 Size { get; }
    }
}