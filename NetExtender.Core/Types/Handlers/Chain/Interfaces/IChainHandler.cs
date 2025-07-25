// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

namespace NetExtender.Types.Handlers.Chain.Interfaces
{
    public interface IChainHandler<T>
    {
        public T Handle(T value);
    }
}