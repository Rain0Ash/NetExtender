// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Runtime.Serialization;

namespace NetExtender
{
    public interface IUnsafeSpace<T> : IUnsafeSize<T> where T : struct, IUnsafeSize<T>
    {
        public Boolean HasLength { get; }
        public Int32 Length { get; set; }
    }
    
    public interface IUnsafeSize<T> : IUnsafeSize, IEqualityStruct<T> where T : struct, IUnsafeSize<T>
    {
        public ref T This { get; }
        public Span<Byte> Full { get; }

        internal ref T Deserialize(SerializationInfo info, StreamingContext context)
        {
            return ref This;
        }

        public Boolean Support<TStruct>() where TStruct : struct;
        public ref TStruct As<TStruct>() where TStruct : struct;
        public ReadOnlySpan<Byte> AsReadOnlySpan();
        public ReadOnlySpan<TStruct> AsReadOnlySpan<TStruct>() where TStruct : struct;
        public Span<Byte> AsSpan();
        public Span<TStruct> AsSpan<TStruct>() where TStruct : struct;

        public ref T SetFull();
        public ref T Fill(Byte value);
        public ref T Fill<TStruct>(TStruct value) where TStruct : struct;
        public ref T Fill<TStruct>(in TStruct value) where TStruct : struct;
        public ref T Clear();
        public ref T Reset();

        public void CopyTo(Span<Byte> destination);
        public void CopyTo(ref T destination);
        public Boolean TryCopyTo(Span<Byte> destination);
        public Boolean TryCopyTo(ref T destination);

        public Byte[] ToArray();
        public Byte[] ToFullArray();
        public TStruct[] ToArray<TStruct>() where TStruct : struct;
        public TStruct[] ToFullArray<TStruct>() where TStruct : struct;

        public Int32 CompareTo(ReadOnlySpan<Byte> other);
        public Int32 CompareTo(in T other);
        public Boolean Equals(ReadOnlySpan<Byte> other);
        public Boolean Equals(in T other);
    }

    public interface IUnsafeSize : IStruct, ISerializable
    {
        public Type Type { get; }
        public Int32 Size { get; }
        
        public ref Byte GetPinnableReference();
    }
}