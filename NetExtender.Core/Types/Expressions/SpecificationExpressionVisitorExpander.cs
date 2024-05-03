// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using NetExtender.Types.Entities;
using NetExtender.Types.Expressions.Specification;
using NetExtender.Types.Expressions.Specification.Interfaces;
using NetExtender.Utilities.Numerics;

namespace NetExtender.Types.Expressions
{
    public class SpecificationExpressionVisitorExpander : ExpressionVisitor
    {
        protected override Expression VisitUnary(UnaryExpression node)
        {
            if (node.NodeType != ExpressionType.Convert)
            {
                return base.VisitUnary(node);
            }

            MethodInfo? method = node.Method;
            if (method is null || method.Name != "op_Implicit")
            {
                return base.VisitUnary(node);
            }

            Type? declaring = method.DeclaringType;
            if (declaring is null || !declaring.IsGenericType || declaring.GetGenericTypeDefinition() != typeof(EntitySpecification<>))
            {
                return base.VisitUnary(node);
            }

            const String name = nameof(EntitySpecification<Any>.ToExpression);
            method = declaring.GetMethod(name);
            return method is not null ? ExpandSpecification(node.Operand, method) : base.VisitUnary(node);
        }

        protected override Expression VisitMethodCall(MethodCallExpression node)
        {
            MethodInfo method = node.Method;
            if (method.Name != nameof(IEntitySpecification<Any>.ToExpression))
            {
                return base.VisitMethodCall(node);
            }

            Type? declaring = method.DeclaringType;
            if (declaring is null)
            {
                return base.VisitMethodCall(node);
            }

            Type[] interfaces = declaring.GetInterfaces();
            return interfaces.Any(HasSpecification) ? ExpandSpecification(node.Object, method) : base.VisitMethodCall(node);
        }

        private static Boolean HasSpecification(Type? type)
        {
            return type is not null && type.IsGenericType && type.GetGenericTypeDefinition() == typeof(IEntitySpecification<>);
        }

        private Expression ExpandSpecification(Expression? specification, MethodInfo method)
        {
            if (method is null)
            {
                throw new ArgumentNullException(nameof(method));
            }

            Object? expression = Expression.Call(specification, method).GetValue();
            return Visit((Expression?) expression) ?? throw new InvalidOperationException($"Can't expand specification for {method}");
        }
    }
}