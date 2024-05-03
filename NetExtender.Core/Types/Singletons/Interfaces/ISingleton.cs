namespace NetExtender.Types.Singletons.Interfaces
{
    public interface ISingleton<out T>
    {
        public T Instance { get; }
    }
}