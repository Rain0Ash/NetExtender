// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Collections.Generic;

namespace NetExtender.Types.Flags.Interfaces
{
    public interface IFlag : IEquatable<IFlag>, IReadOnlyList<Boolean>
    {
        public Int32 Size { get; }
        public Int32 PopCount { get; }
        public ReadOnlySpan<Byte> AsSpan();
        public Boolean HasFlag(ReadOnlySpan<Byte> value);
        public Boolean HasFlag<T>(T value) where T : unmanaged, Enum;
        public Boolean HasFlag(Flag64 value);
        public Boolean HasFlag(Flag128 value);
        public Boolean HasFlag(Flag256 value);
        public Boolean HasFlag(Flag512 value);
        public Boolean HasFlag(Flag1024 value);
        public Boolean HasFlag<T>(EnumFlag<T> value) where T : unmanaged, Enum;
        public Boolean HasFlag(IFlag value);
        public Boolean Equals(ReadOnlySpan<Byte> value);
        public IEnumerable<Int32> Enumerate();
        public IEnumerable<TEnum> Enumerate<TEnum>() where TEnum : unmanaged, Enum;
    }
}