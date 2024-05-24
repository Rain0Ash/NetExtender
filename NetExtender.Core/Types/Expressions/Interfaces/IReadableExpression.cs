using System;
using System.Linq.Expressions;

namespace NetExtender.Types.Expressions.Interfaces
{
    public interface IReadableExpression<T> : IReadableExpression where T : Expression
    {
        public new T Expression { get; }
    }
    
    public interface IReadableExpression : IEquatable<Expression>, IEquatable<IReadableExpression>
    {
        public Expression Expression { get; }
    }
}