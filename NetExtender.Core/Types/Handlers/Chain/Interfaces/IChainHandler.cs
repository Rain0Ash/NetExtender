namespace NetExtender.Types.Handlers.Chain.Interfaces
{
    public interface IChainHandler<T>
    {
        public T Handle(T value);
    }
}