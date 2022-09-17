// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Linq.Expressions;

namespace NetExtender.Types.Expressions.Specification.Interfaces
{
    public interface IEntitySpecification<T>
    {
        public Boolean IsSatisfiedBy(T entity);
        public Expression<Func<T, Boolean>> ToExpression();
    }
}