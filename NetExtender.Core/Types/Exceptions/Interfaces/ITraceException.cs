using System;

namespace NetExtender.Types.Exceptions.Interfaces
{
    public interface ITraceException : IException
    {
        public Guid Id { get; }
    }
}