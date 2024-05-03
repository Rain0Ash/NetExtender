// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

namespace NetExtender.Types.Entities.Interfaces
{
    public interface IEntityId<T> : IEntity<T>
    {
        public T Id { get; init; }
    }
    
    public interface IEntityValue<T> : IEntity<T>
    {
        public T Value { get; init; }
    }
    
    public interface IEntity<out T> : IEntity
    {
        public T Get();
    }
    
    public interface IEntity
    {
    }
}