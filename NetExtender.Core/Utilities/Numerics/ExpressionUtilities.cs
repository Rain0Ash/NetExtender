// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;

namespace NetExtender.Utilities.Numerics
{
    public static class ExpressionUtilities
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
                return delegate { throw new InvalidOperationException(exception.Message); };
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
                return delegate { throw new InvalidOperationException(exception.Message); };
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
                return delegate { throw new InvalidOperationException(exception.Message); };
            }
        }

        public static Func<T1, T2, TResult> CreateExpression<T1, T2, TResult>(this Func<Expression, Expression, BinaryExpression> function, Boolean casting)
        {
            return casting ? CreateExpression<T1, T2, TResult>(function) : CreateExpressionWithoutCasting<T1, T2, TResult>(function);
        }

        public static Expression<Func<Boolean>> Not(this Expression<Func<Boolean>> expression)
        {
            if (expression is null)
            {
                throw new ArgumentNullException(nameof(expression));
            }

            return Expression.Lambda<Func<Boolean>>(Expression.Not(expression.Body), expression.Parameters);
        }

        public static Expression<Func<T, Boolean>> Not<T>(this Expression<Func<T, Boolean>> expression)
        {
            if (expression is null)
            {
                throw new ArgumentNullException(nameof(expression));
            }

            return Expression.Lambda<Func<T, Boolean>>(Expression.Not(expression.Body), expression.Parameters);
        }

        public static Expression<Func<T1, T2, Boolean>> Not<T1, T2>(this Expression<Func<T1, T2, Boolean>> expression)
        {
            if (expression is null)
            {
                throw new ArgumentNullException(nameof(expression));
            }

            return Expression.Lambda<Func<T1, T2, Boolean>>(Expression.Not(expression.Body), expression.Parameters);
        }

        public static Expression<Func<T1, T2, T3, Boolean>> Not<T1, T2, T3>(this Expression<Func<T1, T2, T3, Boolean>> expression)
        {
            if (expression is null)
            {
                throw new ArgumentNullException(nameof(expression));
            }

            return Expression.Lambda<Func<T1, T2, T3, Boolean>>(Expression.Not(expression.Body), expression.Parameters);
        }

        public static Expression<Func<T1, T2, T3, T4, Boolean>> Not<T1, T2, T3, T4>(this Expression<Func<T1, T2, T3, T4, Boolean>> expression)
        {
            if (expression is null)
            {
                throw new ArgumentNullException(nameof(expression));
            }

            return Expression.Lambda<Func<T1, T2, T3, T4, Boolean>>(Expression.Not(expression.Body), expression.Parameters);
        }

        public static Expression<Func<T1, T2, T3, T4, T5, Boolean>> Not<T1, T2, T3, T4, T5>(this Expression<Func<T1, T2, T3, T4, T5, Boolean>> expression)
        {
            if (expression is null)
            {
                throw new ArgumentNullException(nameof(expression));
            }

            return Expression.Lambda<Func<T1, T2, T3, T4, T5, Boolean>>(Expression.Not(expression.Body), expression.Parameters);
        }

        public static Expression<Func<T, Boolean>> And<T>(this Expression<Func<T, Boolean>> first, Expression<Func<T, Boolean>> second)
        {
            if (first is null)
            {
                throw new ArgumentNullException(nameof(first));
            }

            if (second is null)
            {
                throw new ArgumentNullException(nameof(second));
            }

            ParameterExpression left = first.Parameters[0];
            ParameterExpression right = second.Parameters[0];

            return Expression.Lambda<Func<T, Boolean>>(Expression.AndAlso(first.Body, new ParameterReplacer(right, left).Visit(second.Body)), left);
        }

        public static Expression<Func<T, Boolean>> Or<T>(this Expression<Func<T, Boolean>> first, Expression<Func<T, Boolean>> second)
        {
            if (first is null)
            {
                throw new ArgumentNullException(nameof(first));
            }

            if (second is null)
            {
                throw new ArgumentNullException(nameof(second));
            }

            ParameterExpression left = first.Parameters[0];
            ParameterExpression right = second.Parameters[0];

            return Expression.Lambda<Func<T, Boolean>>(Expression.OrElse(first.Body, new ParameterReplacer(right, left).Visit(second.Body)), left);
        }

        private class ParameterReplacer : ExpressionVisitor
        {
            private ParameterExpression Parameter { get; }
            private ParameterExpression Replacement { get; }

            public ParameterReplacer(ParameterExpression parameter, ParameterExpression replacement)
            {
                Parameter = parameter ?? throw new ArgumentNullException(nameof(parameter));
                Replacement = replacement ?? throw new ArgumentNullException(nameof(replacement));
            }

            protected override Expression VisitParameter(ParameterExpression node)
            {
                return base.VisitParameter(Parameter == node ? Replacement : node);
            }
        }

        private static class ExpressionEvaluator
        {
            private delegate Boolean ExpressionHandler(Expression expression, out Object? result);
            private delegate Boolean ExpressionHandler<in T>(T expression, out Object? result) where T : Expression;

            private static ExpressionHandler ConvertHandler<T>(ExpressionHandler<T> handler) where T : Expression
            {
                if (handler is null)
                {
                    throw new ArgumentNullException(nameof(handler));
                }

                Boolean ExpressionHandler(Expression inner, out Object? result)
                {
                    if (inner is not T expression)
                    {
                        throw new InvalidOperationException($"Cannot convert {handler} into {typeof(T).Name}");
                    }

                    return handler.Invoke(expression, out result);
                }

                return ExpressionHandler;
            }

            private static Boolean ConstantExpressionHandler(ConstantExpression expression, out Object? result)
            {
                if (expression is null)
                {
                    throw new ArgumentNullException(nameof(expression));
                }

                result = expression.Value;
                return true;
            }

            private static Boolean MemberAccessExpressionHandler(MemberExpression expression, out Object? result)
            {
                if (expression is null)
                {
                    throw new ArgumentNullException(nameof(expression));
                }

                Expression? inner = expression.Expression;
                Object? instance = inner is not null ? GetValue(inner) : null;
                
                switch (expression.Member)
                {
                    case FieldInfo field:
                        result = field.GetValue(instance);
                        return true;
                    case PropertyInfo property:
                        result = property.GetValue(instance);
                        return true;
                    default:
                        result = default;
                        return false;
                }
            }

            private static Boolean ConvertExpressionHandler(UnaryExpression expression, out Object? result)
            {
                if (expression is null)
                {
                    throw new ArgumentNullException(nameof(expression));
                }

                if (expression.Method is not null)
                {
                    result = default;
                    return false;
                }

                Type type = Nullable.GetUnderlyingType(expression.Type) ?? expression.Type;
                Object? value = GetValue(expression.Operand);
                result = Convert.ChangeType(value, type);
                return true;
            }

            private static Boolean ArrayIndexExpressionHandler(BinaryExpression expression, out Object? result)
            {
                if (expression is null)
                {
                    throw new ArgumentNullException(nameof(expression));
                }

                Array array = (Array) GetValue(expression.Left)!;
                Int32 index = (Int32) GetValue(expression.Right)!;
                result = array.GetValue(index);
                return true;
            }

            private static Boolean ArrayLengthExpressionHandler(UnaryExpression expression, out Object? result)
            {
                if (expression is null)
                {
                    throw new ArgumentNullException(nameof(expression));
                }

                Array array = (Array) GetValue(expression.Operand)!;
                result = array.Length;
                return true;
            }

            private static Boolean CallExpressionHandler(MethodCallExpression expression, out Object? result)
            {
                if (expression is null)
                {
                    throw new ArgumentNullException(nameof(expression));
                }

                if (expression.Method.Name != "get_Item")
                {
                    result = default;
                    return false;
                }

                Expression? value = expression.Object;
                Object? instance = value is not null ? GetValue(value) : null;
                Object?[] arguments = new Object[expression.Arguments.Count];
                
                for (Int32 i = 0; i < arguments.Length; i++)
                {
                    arguments[i] = GetValue(expression.Arguments[i]);
                }

                result = expression.Method.Invoke(instance, arguments);
                return true;
            }

            private static Boolean QuoteExpressionHandler(UnaryExpression expression, out Object? result)
            {
                if (expression is null)
                {
                    throw new ArgumentNullException(nameof(expression));
                }
                
                result = GetValue(expression.Operand);
                return true;
            }
            
            private static Boolean LambdaExpressionHandler(Expression expression, out Object? result)
            {
                result = expression ?? throw new ArgumentNullException(nameof(expression));
                return true;
            }

            public static Object? ValueExpressionHandler(Expression expression)
            {
                if (expression is null)
                {
                    throw new ArgumentNullException(nameof(expression));
                }

                ExpressionHandler? handler = expression.NodeType switch
                {
                    ExpressionType.Constant => ConvertHandler<ConstantExpression>(ConstantExpressionHandler),
                    ExpressionType.MemberAccess => ConvertHandler<MemberExpression>(MemberAccessExpressionHandler),
                    ExpressionType.Convert => ConvertHandler<UnaryExpression>(ConvertExpressionHandler),
                    ExpressionType.ArrayIndex => ConvertHandler<BinaryExpression>(ArrayIndexExpressionHandler),
                    ExpressionType.ArrayLength => ConvertHandler<UnaryExpression>(ArrayLengthExpressionHandler),
                    ExpressionType.Call => ConvertHandler<MethodCallExpression>(CallExpressionHandler),
                    ExpressionType.Quote => ConvertHandler<UnaryExpression>(QuoteExpressionHandler),
                    ExpressionType.Lambda => LambdaExpressionHandler,
                    _ => null
                };

                if (handler is not null && handler.Invoke(expression, out Object? result))
                {
                    return result;
                }

                UnaryExpression unary = Expression.Convert(expression, typeof(Object));
                Expression<Func<Object>> lambda = Expression.Lambda<Func<Object>>(unary);

                return lambda.Compile().Invoke();
            }
        }

        public static Object? GetValue(this Expression expression)
        {
            if (expression is null)
            {
                throw new ArgumentNullException(nameof(expression));
            }

            return ExpressionEvaluator.ValueExpressionHandler(expression);
        }

        public static T? GetValue<T>(this Expression expression)
        {
            if (expression is null)
            {
                throw new ArgumentNullException(nameof(expression));
            }

            dynamic? result = GetValue(expression);
            return (T?) result;
        }
        
        public static Boolean TryGetValue(this Expression expression, out Object? result)
        {
            if (expression is null)
            {
                throw new ArgumentNullException(nameof(expression));
            }

            try
            {
                result = GetValue(expression);
                return true;
            }
            catch (Exception)
            {
                result = default;
                return false;
            }
        }

        public static Boolean TryGetValue<T>(this Expression expression, out T? result)
        {
            if (expression is null)
            {
                throw new ArgumentNullException(nameof(expression));
            }

            try
            {
                result = GetValue<T>(expression);
                return true;
            }
            catch (Exception)
            {
                result = default;
                return false;
            }
        }
        
        public static PropertyInfo? GetPropertyInfo<T, TProperty>(this Expression<Func<T, TProperty>> expression)
        {
            if (expression.Body is not MemberExpression member)
            {
                return null;
            }

            if (member.Member is not PropertyInfo info)
            {
                return null;
            }

            Type? reflection = info.ReflectedType;
            
            if (reflection is null)
            {
                return null;
            }

            Type type = typeof(T);
            if (type != reflection && !type.IsSubclassOf(reflection))
            {
                return null;
            }

            return info;
        }

        public static Expression? Visit(this IEnumerable<ExpressionVisitor>? visitors)
        {
            return Visit(visitors, null);
        }

        [return: NotNullIfNotNull("node")]
        public static Expression? Visit(this IEnumerable<ExpressionVisitor>? visitors, Expression? node)
        {
            return visitors is not null ? visitors.Aggregate(node, (current, visitor) => visitor.Visit(current)) : node;
        }
    }
}