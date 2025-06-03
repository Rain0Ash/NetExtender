// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

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