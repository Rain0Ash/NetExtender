// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using DynamicExpresso;

namespace NetExtender.WindowsPresentation.ActiveBinding
{
    internal sealed class ActiveBindingCacheExpressionParser : IActiveBindingExpressionParser
    {
        public static IActiveBindingExpressionParser Caching()
        {
            return Caching(null);
        }

        public static IActiveBindingExpressionParser Caching(IActiveBindingExpressionParser? parser)
        {
            return new ActiveBindingCacheExpressionParser(parser ?? new ActiveBindingExpressionParser());
        }

        private ConcurrentDictionary<ExpressionKey, WeakReference> Expressions { get; } = new ConcurrentDictionary<ExpressionKey, WeakReference>();
        private IActiveBindingExpressionParser Parser { get; }

        public ActiveBindingCacheExpressionParser(IActiveBindingExpressionParser parser)
        {
            Parser = parser ?? throw new ArgumentNullException(nameof(parser));
        }

        public Lambda Parse(String expression, Parameter[] parameters)
        {
            if (expression is null)
            {
                throw new ArgumentNullException(nameof(expression));
            }

            if (parameters is null)
            {
                throw new ArgumentNullException(nameof(parameters));
            }

            ExpressionKey key = new ExpressionKey(expression, parameters);

            if (Find(key, out Lambda? cache))
            {
                return cache;
            }

            Lambda lambda = Parser.Parse(expression, parameters);
            return Caching(key, lambda);
        }

        private Lambda Caching(ExpressionKey key, Lambda lambda)
        {
            if (lambda is null)
            {
                throw new ArgumentNullException(nameof(lambda));
            }

            WeakReference reference = new WeakReference(lambda);
            Expressions[key] = reference;
            return lambda;
        }

        private Boolean Find(ExpressionKey key, [MaybeNullWhen(false)] out Lambda result)
        {
            if (!Expressions.TryGetValue(key, out WeakReference? expression))
            {
                result = default;
                return false;
            }

            if (expression.Target is Lambda lambda)
            {
                result = lambda;
                return true;
            }

            Expressions.Remove(key, out _);
            Shrink();
            result = default;
            return false;
        }

        private void Shrink()
        {
            foreach ((ExpressionKey key, WeakReference? _) in Expressions.Where(pair => !pair.Value.IsAlive))
            {
                Expressions.Remove(key, out _);
            }
        }

        public void SetReference(IEnumerable<ReferenceType> reference)
        {
            Parser.SetReference(reference);
        }

        private readonly struct ExpressionKey : IEquatable<ExpressionKey>
        {
            private static ParameterComparer Comparer { get; } = new ParameterComparer();

            public String Text { get; }
            private Parameter[] Parameters { get; }

            public ExpressionKey(String text, Parameter[] parameters)
            {
                Text = text ?? throw new ArgumentNullException(nameof(text));
                Parameters = parameters ?? throw new ArgumentNullException(nameof(parameters));
            }

            public override Int32 GetHashCode()
            {
                return HashCode.Combine(Text, Parameters.Length);
            }

            public Boolean Equals(ExpressionKey other)
            {
                return String.Equals(Text, other.Text) && Parameters.SequenceEqual(other.Parameters, Comparer);
            }
        }

        private class ParameterComparer : IEqualityComparer<Parameter>
        {
            public Boolean Equals(Parameter? x, Parameter? y)
            {
                if (x is null)
                {
                    return y is null;
                }

                return y is not null && String.Equals(x.Name, y.Name) && x.Type == y.Type;
            }

            public Int32 GetHashCode(Parameter parameter)
            {
                return HashCode.Combine(parameter.Name, parameter.Type);
            }
        }
    }
}