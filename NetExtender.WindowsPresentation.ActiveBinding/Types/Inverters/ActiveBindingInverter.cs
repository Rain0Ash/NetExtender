// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Globalization;
using System.Linq.Expressions;
using System.Text;
using DynamicExpresso;

namespace NetExtender.WindowsPresentation.ActiveBinding
{
    /// <summary>
    /// Validate and inverse expression of one parameter
    /// </summary>
    public class ActiveBindingInverter : ActiveBindingInverterAbstraction
    {
        protected const String Resource = "({0})";
        
        protected static ExpressionFunctionDictionary<ExpressionType> InversionFunction { get; } = new ExpressionFunctionDictionary<ExpressionType>
        {
            { ExpressionType.Add, ConstantPlace.Wherever, constant => Resource + "-" + constant },
            { ExpressionType.Subtract, ConstantPlace.Left, constant => constant + "-" + Resource },
            { ExpressionType.Subtract, ConstantPlace.Right, constant => Resource + "+" + constant },
            { ExpressionType.Multiply, ConstantPlace.Wherever, constant => Resource + "/" + constant },
            { ExpressionType.Divide, ConstantPlace.Left, constant => constant + "/" + Resource },
            { ExpressionType.Divide, ConstantPlace.Right, constant => Resource + "*" + constant },
        };

        protected static ExpressionFunctionDictionary<String> InversionMathFunction { get; } = new ExpressionFunctionDictionary<String>
        {
            { "Math.Sin", ConstantPlace.Wherever, _ => "Math.Asin" + Resource },
            { "Math.Asin", ConstantPlace.Wherever, _ => "Math.Sin" + Resource },
            { "Math.Cos", ConstantPlace.Wherever, _ => "Math.Acos" + Resource },
            { "Math.Acos", ConstantPlace.Wherever, _ => "Math.Cos" + Resource },
            { "Math.Tan", ConstantPlace.Wherever, _ => "Math.Atan" + Resource },
            { "Math.Atan", ConstantPlace.Wherever, _ => "Math.Tan" + Resource },
            { "Math.Pow", ConstantPlace.Left, constant => "Math.Log(" + Resource + ", " + constant + ")" },
            { "Math.Pow", ConstantPlace.Right, constant => "Math.Pow(" + Resource + ", 1.0/" + constant + ")" },
            { "Math.Log", ConstantPlace.Left, constant => "Math.Pow(" + constant + ", 1.0/" + Resource + ")" },
            { "Math.Log", ConstantPlace.Right, constant => "Math.Pow(" + constant + ", " + Resource + ")" },
        };
        
        protected IActiveBindingExpressionParser Interpreter { get; }
        
        public ActiveBindingInverter(IActiveBindingExpressionParser interpreter)
        {
            Interpreter = interpreter ?? throw new ArgumentNullException(nameof(interpreter));
        }

        /// <summary>
        /// Inverse expression of one parameter
        /// </summary>
        /// <param name="expression">Expression Y=F(X)</param>
        /// <param name="parameter">Type and name of Y parameter</param>
        /// <returns>Inverted expression X = F_back(Y)</returns>
        public override Lambda InverseExpression(Expression expression, ParameterExpression parameter)
        {
            if (expression is null)
            {
                throw new ArgumentNullException(nameof(expression));
            }

            if (parameter is null)
            {
                throw new ArgumentNullException(nameof(parameter));
            }

            RecursiveInfo info = new RecursiveInfo();
            InverseExpressionInternal(expression, info, out String? _);

            if (info.Parameter is null)
            {
                throw new InverseException($"Parameter was not found in expression '{expression}'!");
            }

            if (info.InvertExpression is null)
            {
                throw new InverseException($"Inverted expression was not found in expression '{expression}'!");
            }

            // difficult with constant subtrees: we write to string all constant subtrees,
            // but some of them can take Convert operator, which converted to string as Convert(arg).
            // when we try to parse this string, an error occured, because "Convert(arg)" is not
            // a valid expression
            // Solution: remove all Convert(arg) substrings from result string using regex
            // Big problem: we can't remove Convert because it play important role: 1/2 = 0, ((double)1)/2 = 0.5 !!
            // Solution № 2: as Convert element looks bad in ToString() we need to generate substring by constant subtree manually,
            // this is not very hard task. Good.
            // Other solution: switch to Expression based inverse, where we no need to generate string by Expression,
            // only expressions. But I don't wish to do this, because 

            return Interpreter.Parse(String.Format(info.InvertExpression, parameter.Name), new Parameter(parameter.Name, parameter.Type));
        }

        protected virtual NodeType SolveMathExpression(Expression expression, RecursiveInfo info, out String? result)
        {
            if (expression is null)
            {
                throw new ArgumentNullException(nameof(expression));
            }

            if (info is null)
            {
                throw new ArgumentNullException(nameof(info));
            }

            switch (expression.NodeType)
            {
                case ExpressionType.Add:
                case ExpressionType.Subtract:
                case ExpressionType.Multiply:
                case ExpressionType.Divide:
                {
                    break;
                }
                default:
                    throw new InverseException($"Expression '{expression.NodeType}' is not math expression!");
            }

            if (expression is not BinaryExpression binary)
            {
                throw new InverseException($"Expression '{expression}' is not binary expression!");
            }

            NodeType leftnode = InverseExpressionInternal(binary.Left, info, out String? left);
            NodeType rightnode = InverseExpressionInternal(binary.Right, info, out String? right);
            NodeType node = leftnode == NodeType.Variable || rightnode == NodeType.Variable ? NodeType.Variable : NodeType.Constant;
            
            if (node != NodeType.Variable)
            {
                result = $"({left}{NodeTypeToString(binary.NodeType)}{right})";
                return node;
            }
            
            if (info.InvertExpression is null)
            {
                throw new InverseException($"Inverted expression was not found in expression '{expression}'!");
            }

            ConstantPlace place = leftnode == NodeType.Constant ? ConstantPlace.Left : ConstantPlace.Right;
            result = place == ConstantPlace.Right ? right : left;
            
            if (result is null)
            {
                throw new InverseException($"Constant was not found in expression '{expression}'!");
            }
            
            info.InvertExpression = String.Format(info.InvertExpression, InversionFunction[expression.NodeType, place].Invoke(result));
            return node;
        }

        protected virtual NodeType SolveParameterExpression(Expression expression, RecursiveInfo info, out String? result)
        {
            if (expression is null)
            {
                throw new ArgumentNullException(nameof(expression));
            }

            if (info is null)
            {
                throw new ArgumentNullException(nameof(info));
            }

            if (expression is not ParameterExpression parameter)
            {
                throw new InverseException($"Expression '{expression}' is not parameter expression!");
            }

            if (info.Parameter is null)
            {
                result = null;
                info.Parameter = parameter.Name;
                info.InvertExpression = Resource;
                return NodeType.Variable;
            }

            if (info.Parameter == parameter.Name)
            {
                throw new InverseException($"Variable {info.Parameter} is defined more than one time!");
            }

            throw new InverseException($"More than one variables are defined in expression: {info.Parameter} and {parameter.Name}");
        }

        protected virtual NodeType SolveConstantExpression(Expression expression, RecursiveInfo info, out String? result)
        {
            if (expression is not ConstantExpression constant)
            {
                throw new InverseException($"Expression '{expression}' is not constant expression!");
            }
            
            result = String.Format(CultureInfo.InvariantCulture, "({0})", constant.Value);
            return NodeType.Constant;
        }

        protected virtual NodeType SolveConvertExpression(Expression expression, RecursiveInfo info, out String? result)
        {
            if (expression is not UnaryExpression convert)
            {
                throw new InverseException($"Expression '{expression}' is not convert expression!");
            }
            
            NodeType node = InverseExpressionInternal(convert.Operand, info, out String? constant);
            if (node == NodeType.Constant)
            {
                result = "((" + convert.Type.Name + ")" + constant + ")";
                return node;
            }
            
            if (info.InvertExpression is null)
            {
                throw new InverseException($"Inverted expression was not found in expression '{expression}'!");
            }

            result = null;
            info.InvertExpression = String.Format(info.InvertExpression, "((" + convert.Operand.Type.Name + ")" + Resource + ")");
            return node;
        }

        protected virtual NodeType SolveNegateExpression(Expression expression, RecursiveInfo info, out String? result)
        {
            if (expression is not UnaryExpression negate)
            {
                throw new InverseException($"Expression '{expression}' is not negate expression!");
            }

            NodeType node = InverseExpressionInternal(negate.Operand, info, out String? constant);
            if (node == NodeType.Constant)
            {
                result = "(-" + constant + ")";
                return node;
            }
            
            if (info.InvertExpression is null)
            {
                throw new InverseException($"Inverted expression was not found in expression '{expression}'!");
            }

            result = null;
            info.InvertExpression = String.Format(info.InvertExpression, "(-" + Resource + ")");
            return node;
        }

        protected virtual NodeType SolveNotExpression(Expression expression, RecursiveInfo info, out String? result)
        {
            if (expression is not UnaryExpression convert)
            {
                throw new InverseException($"Expression '{expression}' is not negate expression!");
            }

            NodeType node = InverseExpressionInternal(convert.Operand, info, out String? constant);
            if (node == NodeType.Constant)
            {
                result = "(" + NodeTypeToString(ExpressionType.Not) + constant + ")";
                return node;
            }
            
            if (info.InvertExpression is null)
            {
                throw new InverseException($"Inverted expression was not found in expression '{expression}'!");
            }

            result = null;
            info.InvertExpression = String.Format(info.InvertExpression, "(" + NodeTypeToString(ExpressionType.Not) + Resource + ")");
            return node;
        }
        
        protected virtual NodeType SolveCallExpression(Expression expression, RecursiveInfo info, out String? result)
        {
            if (expression is not MethodCallExpression methodcall)
            {
                throw new InverseException($"Expression '{expression}' is not method call expression!");
            }

            Type? type = methodcall.Method.DeclaringType;

            if (type is null)
            {
                throw new InverseException($"Method '{methodcall.Method.Name}' is not defined!");
            }
            
            String name = type.Name + "." + methodcall.Method.Name;
            if (!InversionMathFunction.ContainsKey(name))
            {
                throw new InverseException($"Unsupported method call expression: {expression}");
            }

            String? right = null;
            NodeType leftnode = InverseExpressionInternal(methodcall.Arguments[0], info, out String? left);
            NodeType? rightnode = methodcall.Arguments.Count == 2 ? InverseExpressionInternal(methodcall.Arguments[1], info, out right) : null;

            if (leftnode == NodeType.Variable || rightnode == NodeType.Variable)
            {
                String invert = leftnode == NodeType.Variable ? InversionMathFunction[name, ConstantPlace.Right](right) : InversionMathFunction[name, ConstantPlace.Left](left);
                
                if (info.InvertExpression is null)
                {
                    throw new InverseException($"Inverted expression was not found in expression '{expression}'!");
                }

                result = default;
                info.InvertExpression = String.Format(info.InvertExpression, invert);
                return NodeType.Variable;
            }

            StringBuilder builder = new StringBuilder(name.Length + left?.Length ?? 0 + right?.Length ?? 0 + 5);
            builder.Append(name);
            builder.Append("(");
            builder.Append(left);
            builder.Append(right is not null ? ", " : null);
            builder.Append(right);
            builder.Append(")");
            
            result = builder.ToString();
            return NodeType.Constant;
        }

        protected virtual NodeType SolveMemberAccessExpression(Expression expression, RecursiveInfo info, out String? result)
        {
            if (expression is not MemberExpression member)
            {
                throw new InverseException($"Expression '{expression}' is not member access expression!");
            }

            Type? type = member.Member.DeclaringType;
            
            if (type is null)
            {
                throw new InverseException($"Type of member '{member.Member.Name}' is null!");
            }
            
            if (type.Name != "Math")
            {
                throw new InverseException($"Unsupported method call expression: {expression}");
            }

            result = String.Format(CultureInfo.InvariantCulture, "({0})", type.Name + "." + member.Member.Name);
            return NodeType.Constant;
        }

        protected virtual NodeType InverseExpressionInternal(Expression expression, RecursiveInfo info, out String? constant)
        {
            if (expression is null)
            {
                throw new ArgumentNullException(nameof(expression));
            }

            switch (expression.NodeType)
            {
                case ExpressionType.Add:
                case ExpressionType.Subtract:
                case ExpressionType.Multiply:
                case ExpressionType.Divide:
                {
                    return SolveMathExpression(expression, info, out constant);
                }
                case ExpressionType.Parameter:
                {
                    return SolveParameterExpression(expression, info, out constant);
                }

                case ExpressionType.Constant:
                {
                    return SolveConstantExpression(expression, info, out constant);
                }
                case ExpressionType.Convert:
                {
                    return SolveConvertExpression(expression, info, out constant);
                }
                case ExpressionType.Negate:
                {
                    return SolveNegateExpression(expression, info, out constant);
                }
                case ExpressionType.Not:
                {
                    return SolveNotExpression(expression, info, out constant);
                }
                case ExpressionType.Call:
                {
                    return SolveCallExpression(expression, info, out constant);
                }
                case ExpressionType.MemberAccess:
                {
                    return SolveMemberAccessExpression(expression, info, out constant);
                }
                default:
                    throw new NotSupportedException($"Unsupported expression: {expression}");
            }
        }
    }
}