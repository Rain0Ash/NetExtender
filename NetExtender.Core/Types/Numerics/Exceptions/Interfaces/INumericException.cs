using System;
using NetExtender.Types.Exceptions.Interfaces;

namespace NetExtender.Types.Numerics.Exceptions.Interfaces
{
    public interface INumericException : IException
    {
        public Boolean IsArgument { get; }
        public Boolean IsOverflow { get; }
        public Boolean IsInfinity { get; }
        public Boolean IsPositiveInfinity { get; }
        public Boolean IsNegativeInfinity { get; }
        public Boolean IsNaN { get; }
    }
}