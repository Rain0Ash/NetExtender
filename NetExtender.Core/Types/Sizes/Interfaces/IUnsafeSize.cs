// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;

namespace NetExtender
{
    public interface IUnsafeSize<T> : IUnsafeSize, IStruct<T> where T : struct, IUnsafeSize<T>
    {
        public ref T Self { get; }
        public Int32 Count { get; set; }

        public ref TStruct As<TStruct>() where TStruct : struct;
        
        public ReadOnlySpan<Byte> AsReadOnlySpan();
        public Span<Byte> AsSpan();
    }

    public interface IUnsafeSize : IStruct
    {
        public Type Type { get; }
        public Int32 Length { get; }
        public Int32 Size { get; }
        
        public ref Byte GetPinnableReference();
    }
}