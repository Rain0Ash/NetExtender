// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Diagnostics.CodeAnalysis;
using System.Linq.Expressions;
using AgileObjects.ReadableExpressions;
using NetExtender.Types.Expressions.Interfaces;
using NetExtender.Utilities.Core;

namespace NetExtender.Types.Expressions
{
    public sealed class ReadableExpression<T> : ReadableExpressionAbstraction<T> where T : Expression
    {
        [return: NotNullIfNotNull("value")]
        public static implicit operator ReadableExpression<T>?(T? value)
        {
            return value is not null ? new ReadableExpression<T>(value) : null;
        }
        public Func<ITranslationSettings?, ITranslationSettings?>? Settings { get; init; }

        public ReadableExpression(T expression)
            : base(expression)
        {
        }

        public ReadableExpression(T expression, ITranslationSettings settings)
            : base(expression)
        {
            Settings = _ => settings;
        }

        protected override ITranslationSettings? Handle(ITranslationSettings? settings)
        {
            if (Settings is { } handler)
            {
                return handler(settings);
            }

            handler = ExpressionUtilities.Settings;
            return handler is not null ? handler(settings) : settings;
        }
    }

    public abstract class ReadableExpressionAbstraction<T> : IReadableExpression<T> where T : Expression
    {
        [return: NotNullIfNotNull("value")]
        public static implicit operator T?(ReadableExpressionAbstraction<T>? value)
        {
            return value?.Expression;
        }

        public static Boolean operator ==(ReadableExpressionAbstraction<T>? first, ReadableExpressionAbstraction<T>? second)
        {
            return ReferenceEquals(first, second) || first is not null && first.Equals((IReadableExpression<T>?) second);
        }

        public static Boolean operator !=(ReadableExpressionAbstraction<T>? first, ReadableExpressionAbstraction<T>? second)
        {
            return !(first == second);
        }

        public T Expression { get; }

        Expression IReadableExpression.Expression
        {
            get
            {
                return Expression;
            }
        }

        protected ReadableExpressionAbstraction(T expression)
        {
            Expression = expression ?? throw new ArgumentNullException(nameof(expression));
        }

        protected abstract ITranslationSettings? Handle(ITranslationSettings? settings);
        
        public override Int32 GetHashCode()
        {
            return Expression.GetHashCode();
        }

        public override Boolean Equals(Object? other)
        {
            return other switch
            {
                Expression expression => Equals(expression),
                IReadableExpression expression => Equals(expression),
                _ => false
            };
        }

        public virtual Boolean Equals(Expression? other)
        {
            return other is T expression && Expression.Equals(expression);
        }

        public Boolean Equals(IReadableExpression? other)
        {
            return other is not null && Equals(other.Expression);
        }

        public override String ToString()
        {
            try
            {
                return Expression.ToReadableString(Handle);
            }
            catch (Exception)
            {
                return Expression.ToString();
            }
        }
    }
}