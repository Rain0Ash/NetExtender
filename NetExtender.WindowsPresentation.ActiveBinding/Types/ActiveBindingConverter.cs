// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Linq.Expressions;
using System.Windows;
using System.Windows.Data;
using DynamicExpresso;
using NetExtender.WindowsPresentation.Types.Converters;

namespace NetExtender.WindowsPresentation.ActiveBinding
{
    public class ActiveBindingConverter : IValueConverter, IMultiValueConverter
    {
        protected IActiveBindingExpressionParser Parser { get; }
        protected Object? Fallback { get; }
        protected Lambda? CompiledExpression { get; set; }
        protected Lambda? CompiledInversedExpression { get; set; }
        protected Type[]? SourceValuesTypes { get; set; }

        public Boolean FalseIsCollapsed { get; set; } = true;
        public Boolean StringFormatDefined { get; set; }

        public ActiveBindingConverter(IActiveBindingExpressionParser parser, Object? fallback, Dictionary<String, Type>? values)
        {
            Parser = parser ?? throw new ArgumentNullException(nameof(parser));
            Fallback = fallback;

            if (values is null || values.Count <= 0)
            {
                return;
            }

            parser.SetReference(values.Select(pair => new ReferenceType(pair.Key, pair.Value)));
        }

        public Object? Convert(Object? value, Type? target, Object? parameter, CultureInfo? culture)
        {
            return Convert(new[] { value }, target, parameter, culture);
        }
        
        public Object? ConvertBack(Object? value, Type? target, Object? parameter, CultureInfo? culture)
        {
            if (target is null)
            {
                return null;
            }
            
            CompiledExpression ??= CompileExpression(null, (String?) parameter, true, new List<Type> { target });
            if (CompiledExpression is null)
            {
                return null;
            }

            CompiledInversedExpression ??= CompileInvertedExpression(CompiledExpression);
            if (CompiledInversedExpression is null)
            {
                return null;
            }

            try
            {
                if (value is null)
                {
                    return CompiledInversedExpression.Invoke(value);
                }
                
                if (target == typeof(Boolean) && value.GetType() == typeof(Visibility))
                {
                    value = BooleanToVisibilityConverter.Create(FalseIsCollapsed).ConvertBack(value, target, null, culture);
                }

                if (value is String result && CompiledExpression.Expression.Type != result.GetType())
                {
                    value = System.Convert.ChangeType(result, CompiledExpression.Expression.Type, CultureInfo.InvariantCulture);
                }

                return CompiledInversedExpression.Invoke(value);
            }
            catch (Exception)
            {
                return null;
            }
        }

        public Object? Convert(Object?[]? values, Type? target, Object? parameter, CultureInfo? culture)
        {
            if (values is null)
            {
                return null;
            }

            Type[] types = GetTypes(values)!;
            if (SourceValuesTypes is null || !SourceValuesTypes.SequenceEqual(types))
            {
                SourceValuesTypes = types;
                CompiledExpression = null;
                CompiledInversedExpression = null;
            }

            CompiledExpression ??= CompileExpression(values, (String?) parameter);
            if (CompiledExpression is null)
            {
                return Fallback;
            }

            try
            {
                Object? result = CompiledExpression.Invoke(values);
                if (StringFormatDefined)
                {
                    return result;
                }

                if (target == typeof(Visibility))
                {
                    return result is not Visibility ? BooleanToVisibilityConverter.Create(FalseIsCollapsed).Convert(result, target, null, culture) : result;
                }

                return target == typeof(String) ? String.Format(CultureInfo.InvariantCulture, "{0}", result) : result;
            }
            catch (Exception)
            {
                return null;
            }
        }

        protected virtual Type?[] GetTypes(IEnumerable<Object?> values)
        {
            if (values is null)
            {
                throw new ArgumentNullException(nameof(values));
            }

            return values.Select(item => item?.GetType()).ToArray();
        }

        protected virtual Lambda? CompileExpression(IEnumerable<Object?>? values, String? expression, Boolean convertBack = false, List<Type>? target = null)
        {
            if (expression is null)
            {
                return null;
            }

            try
            {
                if (!convertBack && values is not null && values.Contains(DependencyProperty.UnsetValue) || SourceValuesTypes is null)
                {
                    return null;
                }

                List<Type>? arguments = convertBack ? target : SourceValuesTypes.Select(type => type).ToList();
                return arguments is not null ? CompileExpression(arguments, expression) : null;
            }
            catch (Exception)
            {
                return null;
            }
        }

        protected virtual Lambda CompileExpression(IReadOnlyList<Type> arguments, String expression)
        {
            if (arguments is null)
            {
                throw new ArgumentNullException(nameof(arguments));
            }

            if (expression is null)
            {
                throw new ArgumentNullException(nameof(expression));
            }

            List<Parameter> parameters = new List<Parameter>();

            for (Int32 i = 0; i < arguments.Count; i++)
            {
                String parameter = GetVariableName(i);
                expression = expression.Replace("{" + i + "}", parameter);
                parameters.Add(new Parameter(parameter, arguments[i]));
            }

            return Parser.Parse(expression, parameters.ToArray());
        }

        protected virtual Lambda? CompileInvertedExpression(Lambda lambda)
        {
            if (lambda is null)
            {
                throw new ArgumentNullException(nameof(lambda));
            }

            try
            {
                Type type = lambda.Expression.Type;
                ParameterExpression expression = System.Linq.Expressions.Expression.Parameter(type, "Path");
                return new ActiveBindingInverter(Parser).InverseExpression(lambda.Expression, expression);
            }
            catch (Exception)
            {
                return null;
            }
        }

        protected virtual String GetVariableName(Int32 i)
        {
            return $"p{++i}";
        }

        public Object[] ConvertBack(Object? value, Type[]? target, Object? parameter, CultureInfo? culture)
        {
            throw new NotSupportedException();
        }
    }
}