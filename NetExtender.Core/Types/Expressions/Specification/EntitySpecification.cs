// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Diagnostics.CodeAnalysis;
using System.Linq.Expressions;
using NetExtender.Types.Expressions.Specification.Interfaces;
using NetExtender.Utilities.Core;

namespace NetExtender.Types.Expressions.Specification
{
    public class EntitySpecification<T> : IEntitySpecification<T>
    {
        public static EntitySpecification<T> True { get; } = new EntitySpecification<T>(_ => true);
        public static EntitySpecification<T> False { get; } = new EntitySpecification<T>(_ => false);
        public static EntitySpecification<T> Null { get; } = new EntitySpecification<T>(value => value == null);
        public static EntitySpecification<T> NotNull { get; } = new EntitySpecification<T>(value => value != null);

        public static implicit operator EntitySpecification<T>(Boolean value)
        {
            return value ? True : False;
        }

        [return: NotNullIfNotNull("value")]
        public static implicit operator Func<T, Boolean>?(EntitySpecification<T>? value)
        {
            return value?.Function;
        }

        [return: NotNullIfNotNull("value")]
        public static implicit operator EntitySpecification<T>?(Func<T, Boolean>? value)
        {
            Expression<Func<T, Boolean>>? expression = value is not null ? item => value.Invoke(item) : null;
            return expression is not null ? new EntitySpecification<T>(expression) : null;
        }

        [return: NotNullIfNotNull("value")]
        public static implicit operator Expression<Func<T, Boolean>>?(EntitySpecification<T>? value)
        {
            return value?.Predicate;
        }

        [return: NotNullIfNotNull("value")]
        public static implicit operator EntitySpecification<T>?(Expression<Func<T, Boolean>>? value)
        {
            return value is not null ? new EntitySpecification<T>(value) : null;
        }

        /// <summary>
        /// <remarks>
        /// For user-defined conditional logical operators.
        /// https://docs.microsoft.com/en-us/dotnet/csharp/language-reference/operators/true-false-operators
        /// </remarks>
        /// </summary>
        public static Boolean operator true(EntitySpecification<T>? _)
        {
            return false;
        }

        /// <summary>
        /// <remarks>
        /// For user-defined conditional logical operators.
        /// https://docs.microsoft.com/en-us/dotnet/csharp/language-reference/operators/true-false-operators
        /// </remarks>
        /// </summary>
        public static Boolean operator false(EntitySpecification<T>? _)
        {
            return false;
        }

        public static EntitySpecification<T> operator !(EntitySpecification<T>? value)
        {
            return value is not null ? new EntitySpecification<T>(value.Predicate.Not()) : true;
        }

        public static EntitySpecification<T> operator &(EntitySpecification<T>? first, EntitySpecification<T>? second)
        {
            return first is not null && second is not null ? new EntitySpecification<T>(first.Predicate.And(second.Predicate)) : false;
        }

        public static EntitySpecification<T> operator |(EntitySpecification<T>? first, EntitySpecification<T>? second)
        {
            return first is not null ? second is not null ? new EntitySpecification<T>(first.Predicate.Or(second.Predicate)) : first : second ?? false;
        }

        private Func<T, Boolean>? _function;
        private Func<T, Boolean> Function
        {
            get
            {
                return _function ??= Predicate.Compile();
            }
        }

        protected Expression<Func<T, Boolean>> Predicate { get; }

        public EntitySpecification(Expression<Func<T, Boolean>> predicate)
        {
            Predicate = predicate ?? throw new ArgumentNullException(nameof(predicate));
        }

        public Boolean IsSatisfiedBy(T entity)
        {
            return Function.Invoke(entity);
        }

        public Expression<Func<T, Boolean>> ToExpression()
        {
            return Predicate;
        }
    }
}