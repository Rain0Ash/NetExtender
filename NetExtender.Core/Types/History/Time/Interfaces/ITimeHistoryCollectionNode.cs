using System;

namespace NetExtender.Types.History.Interfaces
{
    public interface ITimeHistoryCollectionNode<out T>
    {
        public DateTimeOffset Time { get; }
        public T Value { get; }
    }
}