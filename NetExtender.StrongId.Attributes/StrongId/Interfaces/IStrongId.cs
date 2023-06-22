namespace NetExtender.StrongId
{
    public interface IStrongId<out TId, out TUnderlying> : IStrongId<TUnderlying> where TId : struct
    {
        public TId Id { get; }
    }
    
    public interface IStrongId<out TUnderlying> : IStrongId
    {
        public TUnderlying Underlying { get; }
    }
    
    public interface IStrongId
    {
    }
}