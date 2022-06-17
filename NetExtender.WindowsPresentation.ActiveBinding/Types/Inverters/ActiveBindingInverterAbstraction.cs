// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq.Expressions;
using DynamicExpresso;
using NetExtender.WindowsPresentation.ActiveBinding.Interfaces;

namespace NetExtender.WindowsPresentation.ActiveBinding
{
    public abstract class ActiveBindingInverterAbstraction : IActiveBindingInverter
    {
        public abstract Lambda InverseExpression(Expression expression, ParameterExpression parameter);
        
        [SuppressMessage("ReSharper", "SwitchExpressionHandlesSomeKnownEnumValuesWithExceptionInDefault")]
        protected virtual String NodeTypeToString(ExpressionType type)
        {
            return type switch
            {
                ExpressionType.Not => "!",
                ExpressionType.Add => "+",
                ExpressionType.Subtract => "-",
                ExpressionType.Multiply => "*",
                ExpressionType.Divide => "/",
                _ => throw new NotSupportedException("Unkwnown binary node type: " + type + "!")
            };
        }

        protected enum NodeType
        {
            Variable,
            Constant
        }

        protected enum ConstantPlace
        {
            Left,
            Right,
            Wherever
        }

        protected record RecursiveInfo
        {
            public String? Parameter { get; set; }
            public String? InvertExpression { get; set; }
        }

        protected delegate String FunctionExpressionDelegate(String? constant);

        protected sealed class ExpressionFunctionDictionary<T> : Dictionary<T, Dictionary<ConstantPlace, FunctionExpressionDelegate>> where T : notnull
        {
            public FunctionExpressionDelegate this[T first, ConstantPlace second]
            {
                get
                {
                    Dictionary<ConstantPlace, FunctionExpressionDelegate> dictionary = this[first];
                    return dictionary.ContainsKey(second) ? dictionary[second] :
                        dictionary.ContainsKey(ConstantPlace.Wherever) ? dictionary[ConstantPlace.Wherever] : dictionary[second];
                }
            }

            public void Add(T first, ConstantPlace place, FunctionExpressionDelegate value)
            {
                if (!ContainsKey(first))
                {
                    Add(first, new Dictionary<ConstantPlace, FunctionExpressionDelegate>());
                }

                this[first].Add(place, value);
            }
        }
    }
}