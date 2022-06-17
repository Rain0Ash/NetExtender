// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Collections.Generic;
using DynamicExpresso;

namespace NetExtender.WindowsPresentation.ActiveBinding
{
    public sealed class ActiveBindingExpressionParser : IActiveBindingExpressionParser
    {
        private Interpreter Interpreter { get; }

        public ActiveBindingExpressionParser()
            : this(new Interpreter())
        {
        }
        
        public ActiveBindingExpressionParser(Interpreter interpreter)
        {
            Interpreter = interpreter ?? throw new ArgumentNullException(nameof(interpreter));
        }

        public Lambda Parse(String expression, Parameter[] parameters)
        {
            return Interpreter.Parse(expression, parameters);
        }

        public void SetReference(IEnumerable<ReferenceType> reference)
        {
            Interpreter.Reference(reference);
        }
    }
}
