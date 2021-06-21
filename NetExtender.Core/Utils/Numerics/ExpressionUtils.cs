// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Linq.Expressions;

namespace NetExtender.Utils.Numerics
{
    public static class ExpressionUtils
    {
        public static Func<T, TResult> CreateExpression<T, TResult>(this Func<Expression, UnaryExpression> function)
        {
            if (function is null)
            {
                throw new ArgumentNullException(nameof(function));
            }

            ParameterExpression expression = Expression.Parameter(typeof(T));
            
            try
            {
                return Expression.Lambda<Func<T, TResult>>(function(expression), expression).Compile();
            }
            catch (Exception exception)
            {
                return delegate
                {
                    throw new InvalidOperationException(exception.Message);
                };
            }
        }

        public static Func<T1, T2, TResult> CreateExpression<T1, T2, TResult>(this Func<Expression, Expression, BinaryExpression> function)
        {
            if (function is null)
            {
                throw new ArgumentNullException(nameof(function));
            }

            ParameterExpression left = Expression.Parameter(typeof(T1));
            ParameterExpression right = Expression.Parameter(typeof(T2));
            
            try
            {
                try
                {
                    return Expression.Lambda<Func<T1, T2, TResult>>(function(left, right), left, right).Compile();
                }
                catch (InvalidOperationException)
                {
                    if (typeof(T1) == typeof(TResult) && typeof(T2) == typeof(TResult))
                    {
                        throw;
                    }

                    Expression lcast = typeof(T1) == typeof(TResult) ? left : Expression.Convert(left, typeof(TResult));
                    Expression rcast = typeof(T2) == typeof(TResult) ? right : Expression.Convert(right, typeof(TResult));

                    return Expression.Lambda<Func<T1, T2, TResult>>(function(lcast, rcast), left, right).Compile();
                }
            }
            catch (Exception exception)
            {
                return delegate
                {
                    throw new InvalidOperationException(exception.Message);
                };
            }
        }

        public static Func<T1, T2, TResult> CreateExpressionWithoutCasting<T1, T2, TResult>(this Func<Expression, Expression, BinaryExpression> function)
        {
            if (function is null)
            {
                throw new ArgumentNullException(nameof(function));
            }

            ParameterExpression left = Expression.Parameter(typeof(T1));
            ParameterExpression right = Expression.Parameter(typeof(T2));
            
            try
            {
                try
                {
                    return Expression.Lambda<Func<T1, T2, TResult>>(function(left, right), left, right).Compile();
                }
                catch (InvalidOperationException)
                {
                    if (true)
                    {
                        throw;
                    }
                }
            }
            catch (Exception exception)
            {
                return delegate
                {
                    throw new InvalidOperationException(exception.Message);
                };
            }
        }

        public static Func<T1, T2, TResult> CreateExpression<T1, T2, TResult>(this Func<Expression, Expression, BinaryExpression> function, Boolean casting)
        {
            return casting ? CreateExpression<T1, T2, TResult>(function) : CreateExpressionWithoutCasting<T1, T2, TResult>(function);
        }

        public static Expression<Func<Boolean>> LogicalNot(this Expression<Func<Boolean>> expression)
        {
            if (expression is null)
            {
                throw new ArgumentNullException(nameof(expression));
            }

            return Expression.Lambda<Func<Boolean>>(Expression.Not(expression.Body), expression.Parameters);
        }

        public static Expression<Func<T, Boolean>> LogicalNot<T>(this Expression<Func<T, Boolean>> expression)
        {
            if (expression is null)
            {
                throw new ArgumentNullException(nameof(expression));
            }

            return Expression.Lambda<Func<T, Boolean>>(Expression.Not(expression.Body), expression.Parameters);
        }
        
        public static Expression<Func<T1, T2, Boolean>> LogicalNot<T1, T2>(this Expression<Func<T1, T2, Boolean>> expression)
        {
            if (expression is null)
            {
                throw new ArgumentNullException(nameof(expression));
            }

            return Expression.Lambda<Func<T1, T2, Boolean>>(Expression.Not(expression.Body), expression.Parameters);
        }
        
        public static Expression<Func<T1, T2, T3, Boolean>> LogicalNot<T1, T2, T3>(this Expression<Func<T1, T2, T3, Boolean>> expression)
        {
            if (expression is null)
            {
                throw new ArgumentNullException(nameof(expression));
            }

            return Expression.Lambda<Func<T1, T2, T3, Boolean>>(Expression.Not(expression.Body), expression.Parameters);
        }
        
        public static Expression<Func<T1, T2, T3, T4, Boolean>> LogicalNot<T1, T2, T3, T4>(this Expression<Func<T1, T2, T3, T4, Boolean>> expression)
        {
            if (expression is null)
            {
                throw new ArgumentNullException(nameof(expression));
            }

            return Expression.Lambda<Func<T1, T2, T3, T4, Boolean>>(Expression.Not(expression.Body), expression.Parameters);
        }
        
        public static Expression<Func<T1, T2, T3, T4, T5, Boolean>> LogicalNot<T1, T2, T3, T4, T5>(this Expression<Func<T1, T2, T3, T4, T5, Boolean>> expression)
        {
            if (expression is null)
            {
                throw new ArgumentNullException(nameof(expression));
            }

            return Expression.Lambda<Func<T1, T2, T3, T4, T5, Boolean>>(Expression.Not(expression.Body), expression.Parameters);
        }
    }
}