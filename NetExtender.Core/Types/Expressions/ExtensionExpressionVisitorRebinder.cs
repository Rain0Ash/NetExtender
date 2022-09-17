// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using NetExtender.Utilities.Core;

namespace NetExtender.Types.Expressions
{
    public class ExtensionExpressionVisitorRebinder : ExpressionVisitor
    {
        private Object Original { get; }
        private Expression Replacement { get; }
        private KeyValuePair<String, Expression>[] Arguments { get; }

        public ExtensionExpressionVisitorRebinder(Object original, Expression replacement, IEnumerable<KeyValuePair<String, Expression>> arguments)
        {
            Original = original ?? throw new ArgumentNullException(nameof(original));
            Replacement = replacement ?? throw new ArgumentNullException(nameof(replacement));
            Arguments = arguments is not null ? arguments.ToArray() : throw new ArgumentNullException(nameof(arguments));
        }

        protected override Expression VisitConstant(ConstantExpression node)
        {
            return node.Value == Original ? Replacement : node;
        }

        protected override Expression VisitMember(MemberExpression node)
        {
            Expression? expression = node.Expression;
            if (node.NodeType == ExpressionType.MemberAccess && expression is not null && expression.NodeType == ExpressionType.Constant && expression.Type.IsCompilerGenerated())
            {
                return Arguments.Where(pair => pair.Key == node.Member.Name).Select(pair => pair.Value).FirstOrDefault() ?? base.VisitMember(node);
            }

            return base.VisitMember(node);
        }
    }
}