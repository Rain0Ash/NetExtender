// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;
using NetExtender.Utilities.Types;

namespace NetExtender.Types.Expressions.Specification
{
    public class QueryableExpressionVisitorExpander : ExpressionVisitor
    {
        private static Dictionary<MethodInfo, MethodInfo> Replacements { get; }

        static QueryableExpressionVisitorExpander()
        {
            Replacements = new Dictionary<MethodInfo, MethodInfo>();

            var methods = typeof(Queryable)
                .GetMethods(BindingFlags.Static | BindingFlags.Public)
                .Where(method => method.IsDefined(typeof(ExtensionAttribute), true))
                .Select(method => new
                {
                    method.Name,
                    Method = method,
                    Signature = GetMethodSignature(method)
                });

            var lookup = typeof(Enumerable)
                .GetMethods(BindingFlags.Static | BindingFlags.Public)
                .Where(method => method.IsDefined(typeof(ExtensionAttribute), true))
                .ToLookup(method => method.Name, method => new
                {
                    Method = method,
                    Signature = GetMethodSignature(method),
                });

            foreach (var queryable in methods.WhereNotNull())
            {
                var enumerable = lookup[queryable.Name];
                var signature = enumerable.FirstOrDefault(method => method.Signature == queryable.Signature);

                if (signature is null)
                {
                    continue;
                }

                Replacements[queryable.Method] = signature.Method;
            }
        }

        private static String GetMethodSignature(MethodInfo method)
        {
            if (method is null)
            {
                throw new ArgumentNullException(nameof(method));
            }

            StringBuilder builder = new StringBuilder();

            foreach (ParameterInfo info in method.GetParameters())
            {
                AddTypeSignature(builder, info.ParameterType);
            }

            return builder.ToString();
        }

        private static Type GetSignatureType(Type type)
        {
            if (type is null)
            {
                throw new ArgumentNullException(nameof(type));
            }

            if (type == typeof(IQueryable))
            {
                type = typeof(IEnumerable);
            }

            if (!type.GetTypeInfo().IsGenericType)
            {
                return type;
            }

            Type generic = type.GetGenericTypeDefinition();

            if (generic == typeof(Expression<>))
            {
                return type.GetGenericArguments().First();
            }

            if (generic == typeof(IQueryable<>))
            {
                return typeof(IEnumerable<>).MakeGenericType(type.GetGenericArguments());
            }

            if (generic == typeof(IOrderedQueryable<>))
            {
                return typeof(IOrderedEnumerable<>).MakeGenericType(type.GetGenericArguments());
            }

            return type;
        }

        private static void AddTypeSignature(StringBuilder builder, Type type)
        {
            if (builder is null)
            {
                throw new ArgumentNullException(nameof(builder));
            }

            if (type is null)
            {
                throw new ArgumentNullException(nameof(type));
            }

            type = GetSignatureType(type);

            builder.Append(type.Name);

            if (type.GetTypeInfo().IsGenericType)
            {
                builder.Append('[');
                builder.Append(' ');

                foreach (Type argument in type.GetGenericArguments())
                {
                    AddTypeSignature(builder, argument);
                }

                builder.Append(']');
            }

            builder.Append(' ');
        }

        protected override Expression VisitMethodCall(MethodCallExpression node)
        {
            MethodInfo method = node.Method;

            if (method.DeclaringType != typeof(Queryable) || !method.IsDefined(typeof(ExtensionAttribute), true))
            {
                return base.VisitMethodCall(node);
            }

            if (method.Name == nameof(Queryable.AsQueryable))
            {
                return Visit(node.Arguments[0]);
            }

            Type[]? types = null;

            if (method.IsGenericMethod)
            {
                types = method.GetGenericArguments();
                method = method.GetGenericMethodDefinition();
            }

            if (!Replacements.TryGetValue(method, out MethodInfo? replacement))
            {
                return base.VisitMethodCall(node);
            }

            if (types is not null)
            {
                replacement = replacement.MakeGenericMethod(types);
            }

            Expression[] arguments = new Expression[node.Arguments.Count];

            for (Int32 i = 0; i < node.Arguments.Count; i++)
            {
                Expression argument = node.Arguments[i];
                arguments[i] = argument.NodeType == ExpressionType.Quote ? ((UnaryExpression) argument).Operand : argument;
            }

            if (typeof(IOrderedQueryable).IsAssignableFrom(arguments[0].Type))
            {
                arguments[0] = Visit(arguments[0]);
            }

            return Visit(Expression.Call(replacement, arguments));
        }
    }
}