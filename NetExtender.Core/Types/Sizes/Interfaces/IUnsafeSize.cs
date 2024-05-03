// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;

namespace NetExtender.Types.Sizes.Interfaces
{
    public interface IUnsafeSize
    {
        public Type Type { get; }
        public Int32 Length { get; }
        public Int32 Size { get; }
        public Boolean IsEmpty { get; }
        
        public ref Byte GetPinnableReference();
    }

    public interface IUnsafeSize<T> : IUnsafeSize where T : struct, IUnsafeSize<T>
    {
        public ref T Self { get; }
        public Int32 Count { get; set; }

        public ref TStruct As<TStruct>() where TStruct : struct;
        
        public ReadOnlySpan<Byte> AsReadOnlySpan();
        public Span<Byte> AsSpan();
    }
}