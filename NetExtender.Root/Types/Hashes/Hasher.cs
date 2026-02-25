using System;
using System.Runtime.CompilerServices;
using NetExtender.Types.Hashes.Interfaces;

namespace NetExtender.Types.Hashes
{
    public abstract class Hasher : IHasher
    {
        internal static Hasher Default
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                return Seal.Instance;
            }
        }

        public virtual Hasher With<T>(T value, Span<Byte> buffer)
        {
            return With(in value, buffer);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        IHasher IHasher.With<T>(T value, Span<Byte> buffer)
        {
            return With(value, buffer);
        }

        public virtual Hasher With<T>(in T value, Span<Byte> buffer)
        {
            return With(value, buffer);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        IHasher IHasher.With<T>(in T value, Span<Byte> buffer)
        {
            return With(in value, buffer);
        }

        private sealed class Seal : Hasher
        {
            public static Hasher Instance { get; } = new Seal();

            private Seal()
            {
            }

            //TODO: Implement
            public override Seal With<T>(T value, Span<Byte> buffer)
            {
                System.Random.Shared.NextBytes(buffer);
                return this;
            }

            public override Seal With<T>(in T value, Span<Byte> buffer)
            {
                System.Random.Shared.NextBytes(buffer);
                return this;
            }
        }
    }
}