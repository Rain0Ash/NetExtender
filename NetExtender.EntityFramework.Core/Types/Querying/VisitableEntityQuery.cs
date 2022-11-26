// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Threading;
using Microsoft.EntityFrameworkCore.Query;
using NetExtender.Utilities.EntityFrameworkCore.Types;
using NetExtender.Utilities.Numerics;
using NetExtender.Utilities.Types;

namespace NetExtender.EntityFrameworkCore.Expressions.Querying
{
    public class VisitableEntityQuery<T> : IOrderedQueryable<T>, IAsyncEnumerable<T>
    {
        protected VisitableQueryProvider Provider { get; }

        IQueryProvider IQueryable.Provider
        {
            get
            {
                return Provider;
            }
        }

        protected internal ExpressionVisitor[] Visitors { get; }
        protected internal IQueryable<T> Query { get; }

        public Expression Expression
        {
            get
            {
                return Query.Expression;
            }
        }

        public Type ElementType
        {
            get
            {
                return typeof(T);
            }
        }

        protected VisitableEntityQuery(IQueryable<T> queryable, ExpressionVisitor[] visitors)
        {
            Query = queryable ?? throw new ArgumentNullException(nameof(queryable));
            Visitors = visitors ?? throw new ArgumentNullException(nameof(visitors));
            Provider = new VisitableQueryProvider(this);
        }

        public IEnumerator<T> GetEnumerator()
        {
            return Query.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public override String? ToString()
        {
            return Query.ToString();
        }

        IAsyncEnumerator<T> IAsyncEnumerable<T>.GetAsyncEnumerator(CancellationToken token)
        {
            return Query.GetAsyncEnumerator();
        }

        protected class VisitableQueryProvider : IAsyncQueryProvider
        {
            private VisitableEntityQuery<T> Query { get; }

            private IQueryProvider Provider
            {
                get
                {
                    return Query.Query.Provider;
                }
            }

            private ExpressionVisitor[] Visitors
            {
                get
                {
                    return Query.Visitors;
                }
            }

            public VisitableQueryProvider(VisitableEntityQuery<T> query)
            {
                Query = query ?? throw new ArgumentNullException(nameof(query));
            }

            IQueryable<TElement> IQueryProvider.CreateQuery<TElement>(Expression expression)
            {
                expression = Visitors.Visit(expression);
                return Provider.CreateQuery<TElement>(expression).AsVisitable(Visitors);
            }

            IQueryable IQueryProvider.CreateQuery(Expression expression)
            {
                expression = Visitors.Visit(expression);
                return Provider.CreateQuery(expression);
            }

            TResult IQueryProvider.Execute<TResult>(Expression expression)
            {
                expression = Visitors.Visit(expression);
                return Provider.Execute<TResult>(expression);
            }

            Object? IQueryProvider.Execute(Expression expression)
            {
                expression = Visitors.Visit(expression);
                return Provider.Execute(expression);
            }

            public TResult? ExecuteAsync<TResult>(Expression expression)
            {
                expression = Visitors.Visit(expression);
                return Provider is IAsyncQueryProvider provider ? provider.ExecuteAsync<TResult>(expression) : default;
            }

            public TResult ExecuteAsync<TResult>(Expression expression, CancellationToken token)
            {
                expression = Visitors.Visit(expression);
                return Provider is IAsyncQueryProvider provider ? provider.ExecuteAsync<TResult>(expression, token) : Provider.Execute<TResult>(expression);
            }
        }

        private static class Factory
        {
            private static readonly Func<IQueryable<T>, ExpressionVisitor[], VisitableEntityQuery<T>> Internal;

            static Factory()
            {
                if (!typeof(T).IsClass)
                {
                    Internal = (query, visitors) => new VisitableEntityQuery<T>(query, visitors);
                    return;
                }

                Type querytype = typeof(IQueryable<T>);
                Type visitortype = typeof(ExpressionVisitor[]);
                ConstructorInfo constructor = typeof(VisitableEntityQuery<>).MakeGenericType(typeof(T)).GetConstructor(new[] { querytype, visitortype }) ?? throw new InvalidOperationException();

                ParameterExpression query = Expression.Parameter(querytype);
                ParameterExpression visitor = Expression.Parameter(visitortype);

                Internal = Expression.Lambda<Func<IQueryable<T>, ExpressionVisitor[], VisitableEntityQuery<T>>>(
                    Expression.New(constructor, query, visitor), query, visitor).Compile();
            }

            // ReSharper disable once MemberHidesStaticFromOuterClass
            public static VisitableEntityQuery<T> Create(IQueryable<T> source, params ExpressionVisitor[] visitors)
            {
                if (source is null)
                {
                    throw new ArgumentNullException(nameof(source));
                }

                if (visitors is null)
                {
                    throw new ArgumentNullException(nameof(visitors));
                }

                return Internal.Invoke(source, visitors);
            }
        }

        public static VisitableEntityQuery<T> Create(IQueryable<T> source, params ExpressionVisitor[] visitors)
        {
            return Factory.Create(source, visitors);
        }
    }
}