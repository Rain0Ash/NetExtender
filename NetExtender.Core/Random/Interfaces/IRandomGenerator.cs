// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

namespace NetExtender.Random.Interfaces
{
    public interface IRandomGenerator<out T>
    {
        public T Next();
    }

    public interface IRandomComparableGenerator<T> : IRandomGenerator<T>
    {
        public T Next(T max);
        public T Next(T min, T max);
    }
}