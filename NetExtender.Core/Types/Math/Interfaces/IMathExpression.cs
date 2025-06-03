using System;
using NetExtender.Interfaces;
using NetExtender.Types.Monads;
using NetExtender.Types.Monads.Interfaces;
using NetExtender.Types.Numerics;

namespace NetExtender.Types.Mathematics.Interfaces
{
    public interface IMutableMathBinaryExpression<T, TExpression> : IMutableMathBinaryExpression<T>, ICloneable<IMutableMathBinaryExpression<T, TExpression>> where T : struct, IEquatable<T>, IFormattable where TExpression : IMathExpression<T>
    {
        public new TExpression FirstInner { get; set; }
        public new TExpression SecondInner { get; set; }

        public new IMutableMathBinaryExpression<T, TExpression> Clone();
    }
    
    public interface IMutableMathBinaryExpression<T> : IMutableMathBinaryExpression, IMathBinaryExpression<T>, IMutableMathExpression<T>, ICloneable<IMutableMathBinaryExpression<T>> where T : struct, IEquatable<T>, IFormattable
    {
        public new MathResult<T> First { get; set; }
        public new MathResult<T> Second { get; set; }

        public IMutableMathBinaryExpression<T>? ApplyToFirst(MathUnaryOperator @operator);
        public IMutableMathBinaryExpression<T>? ApplyToSecond(MathUnaryOperator @operator);
        public IMutableMathBinaryExpression<T>? ApplyToFirst(MathBinaryOperator @operator);
        public IMutableMathBinaryExpression<T>? ApplyToSecond(MathBinaryOperator @operator);
        public IMutableMathBinaryExpression<T>? ApplyInverseToFirst(MathBinaryOperator @operator);
        public IMutableMathBinaryExpression<T>? ApplyInverseToSecond(MathBinaryOperator @operator);
        public IMutableMathBinaryExpression<T>? ApplyToFirst(MathBinaryOperator @operator, T value);
        public IMutableMathBinaryExpression<T>? ApplyToSecond(MathBinaryOperator @operator, T value);
        public IMutableMathBinaryExpression<T>? ApplyInverseToFirst(MathBinaryOperator @operator, T value);
        public IMutableMathBinaryExpression<T>? ApplyInverseToSecond(MathBinaryOperator @operator, T value);
        public IMutableMathBinaryExpression<T>? ApplyToFirst(MathBinaryOperator @operator, MathResult<T> value);
        public IMutableMathBinaryExpression<T>? ApplyToSecond(MathBinaryOperator @operator, MathResult<T> value);
        public IMutableMathBinaryExpression<T>? ApplyInverseToFirst(MathBinaryOperator @operator, MathResult<T> value);
        public IMutableMathBinaryExpression<T>? ApplyInverseToSecond(MathBinaryOperator @operator, MathResult<T> value);
        public IMutableMathBinaryExpression<T>? ApplyToFirst(MathBinaryOperator @operator, IMathExpression<T> expression);
        public IMutableMathBinaryExpression<T>? ApplyToSecond(MathBinaryOperator @operator, IMathExpression<T> expression);
        public IMutableMathBinaryExpression<T>? ApplyInverseToFirst(MathBinaryOperator @operator, IMathExpression<T> expression);
        public IMutableMathBinaryExpression<T>? ApplyInverseToSecond(MathBinaryOperator @operator, IMathExpression<T> expression);

        public new IMutableMathBinaryExpression<T> Clone();
    }

    public interface IMathBinaryExpression<T> : IMathBinaryExpression, IMathExpression<T>, ICloneable<IMathBinaryExpression<T>> where T : struct, IEquatable<T>, IFormattable
    {
        public MathResult<T> First { get; }
        public new IMathExpression<T> FirstInner { get; }
        public MathResult<T> Second { get; }
        public new IMathExpression<T> SecondInner { get; }
        
        public new IMathBinaryExpression<T> Clone();
    }
    
    public interface IMutableMathBinaryExpression : IMathBinaryExpression, ICloneable<IMutableMathBinaryExpression>
    {
        public new MathBinaryOperator Operator { get; set; }

        public new IMutableMathBinaryExpression Clone();
    }

    public interface IMathBinaryExpression : IMathExpression, ICloneable<IMathBinaryExpression>
    {
        public MathBinaryOperator Operator { get; }
        
        public new IMathBinaryExpression Clone();
    }
    
    public interface IMutableMathUnaryExpression<T, TExpression> : IMutableMathUnaryExpression<T>, ICloneable<IMutableMathUnaryExpression<T, TExpression>> where T : struct, IEquatable<T>, IFormattable where TExpression : IMathExpression<T>?
    {
        public new Maybe<TExpression> Inner { get; set; }

        public new IMutableMathUnaryExpression<T, TExpression> Clone();
    }
    
    public interface IMutableMathUnaryExpression<T> : IMutableMathUnaryExpression, IMathUnaryExpression<T>, IMutableMathExpression<T>, ICloneable<IMutableMathUnaryExpression<T>> where T : struct, IEquatable<T>, IFormattable
    {
        public new MathResult<T> Value { get; set; }
        
        public new String? Format { get; set; }
        public new IFormatProvider? Provider { get; set; }

        public IMutableMathExpression<T>? ApplyToInner(MathUnaryOperator @operator);
        public IMutableMathExpression<T>? ApplyToInner(MathBinaryOperator @operator);
        public IMutableMathExpression<T>? ApplyInverseToInner(MathBinaryOperator @operator);
        public IMutableMathExpression<T>? ApplyToInner(MathBinaryOperator @operator, T value);
        public IMutableMathExpression<T>? ApplyInverseToInner(MathBinaryOperator @operator, T value);
        public IMutableMathExpression<T>? ApplyToInner(MathBinaryOperator @operator, MathResult<T> value);
        public IMutableMathExpression<T>? ApplyInverseToInner(MathBinaryOperator @operator, MathResult<T> value);
        public IMutableMathExpression<T>? ApplyToInner(MathBinaryOperator @operator, IMathExpression<T> expression);
        public IMutableMathExpression<T>? ApplyInverseToInner(MathBinaryOperator @operator, IMathExpression<T> expression);

        public new IMutableMathUnaryExpression<T> Clone();
    }
    
    public interface IMathUnaryExpression<T> : IMathUnaryExpression, IMathExpression<T>, ICloneable<IMathUnaryExpression<T>> where T : struct, IEquatable<T>, IFormattable
    {
        public MathResult<T> Value { get; }

        public new String? Format { get; }
        public new IFormatProvider? Provider { get; }

        public new IMathUnaryExpression<T> Clone();
    }
    
    public interface IMutableMathUnaryExpression : IMathUnaryExpression, ICloneable<IMutableMathUnaryExpression>
    {
        public new MathUnaryOperator Operator { get; set; }

        public new IMutableMathUnaryExpression Clone();
    }
    
    public interface IMathUnaryExpression : IMathExpression, ICloneable<IMathUnaryExpression>
    {
        public MathUnaryOperator Operator { get; }

        public new IMathUnaryExpression Clone();
    }
    
    public interface IMutableMathExpression<T> : IMathExpression<T>, IMutableMathExpression, ICloneable<IMutableMathExpression<T>> where T : struct, IEquatable<T>, IFormattable
    {
        public new IMutableMathExpression<T> Clone();
    }
    
    public interface IMathExpression<T> : IMathExpression, IEquality<T>, IEquality<MathResult<T>>, IEquality<IMathExpression<T>>, ICloneable<IMathExpression<T>> where T : struct, IEquatable<T>, IFormattable
    {
        public Int32 Elements { get; }
        public new IMathExpression<T>? Inner { get; }
        public new IMathExpression<T>? FirstInner { get; }
        public new IMathExpression<T>? SecondInner { get; }
        public MathResult<T> Result { get; }

        public new IMathExpression<T> Clone();

        public IMutableMathUnaryExpression<T> Apply(MathUnaryOperator @operator);
        public IMutableMathBinaryExpression<T> Apply(MathBinaryOperator @operator);
        public IMutableMathBinaryExpression<T> ApplyInverse(MathBinaryOperator @operator);
        public IMutableMathBinaryExpression<T> Apply(MathBinaryOperator @operator, T value);
        public IMutableMathBinaryExpression<T> ApplyInverse(MathBinaryOperator @operator, T value);
        public IMutableMathBinaryExpression<T> Apply(MathBinaryOperator @operator, MathResult<T> value);
        public IMutableMathBinaryExpression<T> ApplyInverse(MathBinaryOperator @operator, MathResult<T> value);
        public IMutableMathBinaryExpression<T> Apply(MathBinaryOperator @operator, IMathExpression<T> expression);
        public IMutableMathBinaryExpression<T> ApplyInverse(MathBinaryOperator @operator, IMathExpression<T> expression);
    }

    public interface IMutableMathExpression : IMathExpression, ICloneable<IMutableMathExpression>
    {
        public new IMutableMathExpression Clone();
    }

    public interface IMathExpression : IAnyEquality, ICloneable<IMathExpression>, ICloneable, IStringable
    {
        internal Boolean IsReference
        {
            get
            {
                return !GetType().IsValueType;
            }
        }
        
        internal MathExpression.State State
        {
            get
            {
                return default;
            }
        }

        public Boolean IsComplex
        {
            get
            {
                return State is not default(MathExpression.State) && !State.HasFlag(MathExpression.State.Simple);
            }
        }

        internal Boolean IsFunction
        {
            get
            {
                return State.HasFlag(MathExpression.State.Function);
            }
        }

        public IMathExpression? Inner { get; }
        public IMathExpression? FirstInner { get; }
        public IMathExpression? SecondInner { get; }
        
        public Boolean IsBinary { get; }
        public Boolean? IsTrue { get; }
        public Boolean? IsFalse { get; }

        public String? Format { get; }
        public IFormatProvider? Provider { get; }
        
        public Boolean IsEmpty { get; }

        public new IMathExpression Clone();
    }
}