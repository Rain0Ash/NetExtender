// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

namespace NetExtender.Types.Monads
{
    public class Box<T> where T : struct
    {
        public static implicit operator T(Box<T>? box)
        {
            return box?.Value ?? default;
        }
        
        public static implicit operator Box<T>(T value)
        {
            return new Box<T>(value);
        }
        
        public T Value { get; }
        
        public Box(T value)
        {
            Value = value;
        }
    }
}