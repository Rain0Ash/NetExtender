// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System.Linq.Expressions;
using NetExtender.Types.Expressions.Specification;

namespace NetExtender.Types.Expressions
{
    public class QueryableExpressionVisitor : ExpressionVisitor
    {
        protected ExpressionVisitor Expander { get; } = new QueryableExpressionVisitorExpander();
        
        protected override Expression VisitUnary(UnaryExpression node)
        {
            return node.NodeType == ExpressionType.Quote ? Expander.Visit(node) : base.VisitUnary(node);
        }
    }
}