using System;
using NetExtender.Monads;

namespace NetExtender.Exceptions.Interfaces
{
    public interface IUnsafeTraceException : ITraceException
    {
        public new RayIdContext RayId { get; set; }
        public Boolean Set(RayIdContext context);
    }

    public interface ITraceException : IException
    {
        public Guid Id { get; }
        public RayIdContext RayId { get; }
        public DateTime When { get; }
    }
}