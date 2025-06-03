// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

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