// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Runtime.CompilerServices;
using NetExtender.Types.Expressions.Querying;
using NetExtender.Utilities.Numerics;

namespace NetExtender.Types.Expressions
{
    public class ExtensionExpressionVisitorExpander : ExpressionVisitor
    {
        private static MethodInfo AsQueryableMethod { get; }
        private static MethodInfo? QueryableEmptyMethod { get; }

        static ExtensionExpressionVisitorExpander()
        {
            AsQueryableMethod = typeof(Queryable).GetMethods(BindingFlags.Static | BindingFlags.Public)
                .First(info => info.Name == nameof(Queryable.AsQueryable) && info.IsGenericMethod);

            QueryableEmptyMethod = typeof(ExtensionExpressionVisitorRebinder).GetMethod(nameof(QueryableEmpty), BindingFlags.Static | BindingFlags.NonPublic);
        }

        protected override Expression VisitMethodCall(MethodCallExpression node)
        {
            MethodInfo method = node.Method;

            if (!method.IsDefined(typeof(ExtensionAttribute), true) || !method.IsDefined(typeof(ExpandableAttribute), true))
            {
                return base.VisitMethodCall(node);
            }

            ParameterInfo[] parameters = method.GetParameters();
            Type queryable = parameters.First().ParameterType;
            Type entity = queryable.GetGenericArguments().Single();

            Object input = MakeEnumerableQuery(entity) ?? throw new InvalidOperationException("Can't make query");
            Object?[] arguments = new Object[parameters.Length];
            arguments[0] = input;

            List<KeyValuePair<String, Expression>> replacements = new List<KeyValuePair<String, Expression>>();

            for (Int32 i = 1; i < parameters.Length; i++)
            {
                try
                {
                    arguments[i] = node.Arguments[i].GetValue();
                }
                catch (InvalidOperationException exception)
                {
                    ParameterInfo info = parameters[i];
                    arguments[i] = default;

                    if (info.Name is null)
                    {
                        throw new InvalidOperationException($"Parameter {info} mush have name", exception);
                    }

                    replacements.Add(new KeyValuePair<String, Expression>(info.Name, node.Arguments[i]));
                }
            }

            Object? output = method.Invoke(null, arguments);
            Expression? expression = ((IQueryable?) output)?.Expression;
            Expression argument = node.Arguments[0];

            if (!typeof(IQueryable).IsAssignableFrom(argument.Type))
            {
                argument = Expression.Call(AsQueryableMethod.MakeGenericMethod(entity), argument);
            }

            expression = new ExtensionExpressionVisitorRebinder(input, argument, replacements).Visit(expression);
            return Visit(expression) ?? throw new InvalidOperationException("Can't visit expression");
        }

        private static Object? MakeEnumerableQuery(Type type)
        {
            if (type is null)
            {
                throw new ArgumentNullException(nameof(type));
            }

            return QueryableEmptyMethod?.MakeGenericMethod(type).Invoke(null, null);
        }

        private static IQueryable<T> QueryableEmpty<T>()
        {
            return Enumerable.Empty<T>().AsQueryable();
        }
    }
}