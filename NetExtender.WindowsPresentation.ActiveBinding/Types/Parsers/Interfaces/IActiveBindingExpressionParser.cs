// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Collections.Generic;
using DynamicExpresso;

namespace NetExtender.WindowsPresentation.ActiveBinding
{
    public interface IActiveBindingExpressionParser
    {
        public Lambda Parse(String expression, params Parameter[] parameters);
        public void SetReference(IEnumerable<ReferenceType> reference);
    }
}
