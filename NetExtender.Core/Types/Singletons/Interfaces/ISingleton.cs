// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

namespace NetExtender.Types.Singletons.Interfaces
{
    public interface ISingleton<out T>
    {
        public T Instance { get; }
    }
}