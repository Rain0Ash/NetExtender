using System;
using NetExtender.Interfaces;
using NetExtender.Types.Monads.Interfaces;
using NetExtender.Types.Monads.Result;

namespace NetExtender.Types.Numerics.Interfaces
{
    public interface IMathResult<T> : IResult<T, OverflowException>, IResultEquality<IMathResult<T>>, ICloneable<IMathResult<T>> where T : struct, IEquatable<T>, IFormattable
    {
        public Result<T, OverflowException> Result { get; }
        
        public MathResult<T> Real { get; }
        public MathResult<T> IntegerReal { get; }
        public MathResult<T> FractionalReal { get; }
        public MathResult<T> Imaginary { get; }
        public MathResult<T> IntegerImaginary { get; }
        public MathResult<T> FractionalImaginary { get; }

        public Boolean IsFinite { get; }
        public Boolean IsComplex { get; }
        public Boolean IsArgument { get; }
        public Boolean IsOverflow { get; }
        public Boolean IsInfinity { get; }
        public Boolean IsPositiveInfinity { get; }
        public Boolean IsNegativeInfinity { get; }
        public Boolean IsNaN { get; }

        public new IMathResult<T> Clone();
    }
}