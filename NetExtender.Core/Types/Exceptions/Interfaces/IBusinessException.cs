using System;

namespace NetExtender.Types.Exceptions.Interfaces
{
    public interface IBusinessException
    {
        public Exception Exception
        {
            get
            {
                return this as Exception ?? throw new InvalidOperationException();
            }
        }
    }
}