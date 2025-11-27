using System;
using NetExtender.Interfaces;
using NetExtender.Types.Monads;

namespace NetExtender.Types.Monads.Interfaces
{
    public interface IFlowResult<T, TException> : IFlowResult<T>, IResult<T, TException>, ICloneable<IFlowResult<T, TException>> where TException : Exception
    {
        public new Result<T, TException> Result { get; }
        
        public new IFlowResult<T, TException> Clone();
    }
    
    public interface IFlowResult<T> : IFlowResult, IResult<T>, IResultEquality<T, IFlowResult<T>>, ICloneable<IFlowResult<T>>
    {
        public new T Next { get; }
        public new Result<T> Result { get; }

        public new IFlowResult<T> Clone();
    }
    
    public interface IFlowResult : IResult, ICloneable<IFlowResult>
    {
        public Boolean HasNext { get; }
        public Object? Next { get; }
        public Object? Result { get; }
        
        public new IFlowResult Clone();
    }
}