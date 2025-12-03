using System;

#pragma warning disable CS8500

namespace NetExtender.Types.Sizes
{
    public readonly unsafe struct UnsafePointer<T> : IEquatable<UnsafePointer<T>>
    {
        public static implicit operator UnsafePointer<T>(T* value)
        {
            return new UnsafePointer<T>(value);
        }

        public static implicit operator T(UnsafePointer<T> value)
        {
            return value.Value;
        }

        public static Boolean operator ==(UnsafePointer<T> first, UnsafePointer<T> second)
        {
            return first.Equals(second);
        }

        public static Boolean operator !=(UnsafePointer<T> first, UnsafePointer<T> second)
        {
            return !(first == second);
        }

        public readonly T* Pointer;

        public ref T Value
        {
            get
            {
                return ref *Pointer;
            }
        }

        public UnsafePointer(T* pointer)
        {
            Pointer = pointer;
        }

        public override Int32 GetHashCode()
        {
            return Pointer->GetHashCode();
        }

        public override Boolean Equals(Object? other)
        {
            return Pointer->Equals(other);
        }

        public Boolean Equals(UnsafePointer<T> other)
        {
            return Pointer == other.Pointer;
        }

        public override String ToString()
        {
            return ((IntPtr) Pointer).ToString();
        }
    }
}

#pragma warning restore CS8500