using System;

namespace NetExtender.Types.Vectors.Interfaces
{
    public interface IUnsafeVector<T, TUnderline, TSize> : IUnsafeVector<T, TUnderline> where T : struct, IUnsafeVector<T, TUnderline, TSize> where TUnderline : struct where TSize : struct, IUnsafeSize<TSize>
    {
    }
    
    public interface IUnsafeVector<T, TUnderline> : IUnsafeVector<T>, IVector<TUnderline> where T : struct, IUnsafeVector<T, TUnderline> where TUnderline : struct
    {
        public new ref T Fill(TUnderline value);
        public new Span<TUnderline> Full { get; }
    }
    
    public interface IUnsafeVector<T> : IUnsafeVector, IEquatableStruct<T> where T : struct, IUnsafeVector<T>
    {
    }
    
    public interface IUnsafeVector : IVector
    {
        public Span<Byte> Full { get; }
    }
}