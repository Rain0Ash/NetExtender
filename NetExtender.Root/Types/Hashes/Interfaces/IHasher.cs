using System;
using System.Runtime.CompilerServices;

namespace NetExtender.Types.Hashes.Interfaces
{
    public interface IHasher
    {
        public static IHasher Default
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                return Hasher.Default;
            }
        }

        public IHasher With<T>(T value, Span<Byte> buffer);
        public IHasher With<T>(in T value, Span<Byte> buffer);
    }
}