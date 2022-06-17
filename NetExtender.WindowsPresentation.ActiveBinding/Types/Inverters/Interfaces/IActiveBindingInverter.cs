// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System.Linq.Expressions;
using DynamicExpresso;

namespace NetExtender.WindowsPresentation.ActiveBinding.Interfaces
{
    public interface IActiveBindingInverter
    {
        public Lambda InverseExpression(Expression expression, ParameterExpression parameter);
    }
}