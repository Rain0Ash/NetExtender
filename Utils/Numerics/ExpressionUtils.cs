﻿// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Linq.Expressions;
using JetBrains.Annotations;

namespace NetExtender.Utils.Numerics
{
    public static class ExpressionUtils
    {
        public static Func<T1, TResult> CreateExpression<T1, TResult>(Func<Expression, UnaryExpression> body)
        {
            ParameterExpression inp = Expression.Parameter(typeof(T1), "inp");
            
            try
            {
                return Expression.Lambda<Func<T1, TResult>>(body(inp), inp).Compile();
            }
            catch (Exception ex)
            {
                return delegate
                {
                    throw new InvalidOperationException(ex.Message);
                };
            }
        }

        public static Func<T1, T2, TResult> CreateExpression<T1, T2, TResult>(
            Func<Expression, Expression, BinaryExpression> body)
        {
            return CreateExpression<T1, T2, TResult>(body, false);
        }

        public static Func<T1, T2, TResult> CreateExpression<T1, T2, TResult>(
            Func<Expression, Expression, BinaryExpression> body, Boolean castArgsToResultOnFailure)
        {
            ParameterExpression lhs = Expression.Parameter(typeof(T1), "lhs");
            ParameterExpression rhs = Expression.Parameter(typeof(T2), "rhs");
            try
            {
                try
                {
                    return Expression.Lambda<Func<T1, T2, TResult>>(body(lhs, rhs), lhs, rhs).Compile();
                }
                catch (InvalidOperationException)
                {
                    if (!castArgsToResultOnFailure || typeof(T1) == typeof(TResult) &&
                        typeof(T2) == typeof(TResult))
                    {
                        throw;
                    }

                    Expression castLhs = typeof(T1) == typeof(TResult) ? lhs : Expression.Convert(lhs, typeof(TResult));
                    Expression castRhs = typeof(T2) == typeof(TResult) ? rhs : Expression.Convert(rhs, typeof(TResult));

                    return Expression.Lambda<Func<T1, T2, TResult>>(
                        body(castLhs, castRhs), lhs, rhs).Compile();
                }
            }
            catch (Exception ex)
            {
                return delegate { throw new InvalidOperationException(ex.Message); };
            }
        }

        public static Expression<Func<T, Boolean>> LogicalNot<T>([NotNull] this Expression<Func<T, Boolean>> expression)
        {
            if (expression is null)
            {
                throw new ArgumentNullException(nameof(expression));
            }

            return Expression.Lambda<Func<T, Boolean>>(Expression.Not(expression.Body), expression.Parameters);
        }
        
        public static Expression<Func<T1, T2, Boolean>> LogicalNot<T1, T2>([NotNull] this Expression<Func<T1, T2, Boolean>> expression)
        {
            if (expression is null)
            {
                throw new ArgumentNullException(nameof(expression));
            }

            return Expression.Lambda<Func<T1, T2, Boolean>>(Expression.Not(expression.Body), expression.Parameters);
        }
        
        public static Expression<Func<T1, T2, T3, Boolean>> LogicalNot<T1, T2, T3>([NotNull] this Expression<Func<T1, T2, T3, Boolean>> expression)
        {
            if (expression is null)
            {
                throw new ArgumentNullException(nameof(expression));
            }

            return Expression.Lambda<Func<T1, T2, T3, Boolean>>(Expression.Not(expression.Body), expression.Parameters);
        }
        
        public static Expression<Func<T1, T2, T3, T4, Boolean>> LogicalNot<T1, T2, T3, T4>([NotNull] this Expression<Func<T1, T2, T3, T4, Boolean>> expression)
        {
            if (expression is null)
            {
                throw new ArgumentNullException(nameof(expression));
            }

            return Expression.Lambda<Func<T1, T2, T3, T4, Boolean>>(Expression.Not(expression.Body), expression.Parameters);
        }
        
        public static Expression<Func<T1, T2, T3, T4, T5, Boolean>> LogicalNot<T1, T2, T3, T4, T5>([NotNull] this Expression<Func<T1, T2, T3, T4, T5, Boolean>> expression)
        {
            if (expression is null)
            {
                throw new ArgumentNullException(nameof(expression));
            }

            return Expression.Lambda<Func<T1, T2, T3, T4, T5, Boolean>>(Expression.Not(expression.Body), expression.Parameters);
        }
    }
}