// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Linq;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using NetExtender.EntityFrameworkCore.Expressions.Querying;
using NetExtender.Types.Expressions;

namespace NetExtender.Utilities.EntityFrameworkCore.Types
{
    public static class VisitableQueryUtilities
    {
        public static IQueryable<T> Include<T, TProperty>(this VisitableEntityQuery<T> source, Expression<Func<T, TProperty>> property) where T : class
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            if (property is null)
            {
                throw new ArgumentNullException(nameof(property));
            }

            return source.Query.Include(property).AsVisitable(source.Visitors);
        }
        
        public static IQueryable<T> ToExpandable<T>(this IQueryable<T> queryable)
        {
            if (queryable == null)
            {
                throw new ArgumentNullException(nameof(queryable));
            }

            return queryable.AsVisitable(new ExtensionExpressionVisitorExpander(), new QueryableExpressionVisitor());
        }
        
        public static IQueryable<T> AsExpandable<T>(this IQueryable<T> queryable)
        {
            if (queryable == null)
            {
                throw new ArgumentNullException(nameof(queryable));
            }

            return queryable.ToExpandable();
        }
        
        public static IQueryable<T> ToVisitable<T>(this IQueryable<T> source, params ExpressionVisitor[] visitors)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            if (visitors is null)
            {
                throw new ArgumentNullException(nameof(visitors));
            }

            return VisitableEntityQuery<T>.Create(source, visitors);
        }
        
        public static IQueryable<T> AsVisitable<T>(this IQueryable<T> source, params ExpressionVisitor[] visitors)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            if (visitors is null)
            {
                throw new ArgumentNullException(nameof(visitors));
            }

            return source as VisitableEntityQuery<T> ?? VisitableEntityQuery<T>.Create(source, visitors);
        }
    }
}