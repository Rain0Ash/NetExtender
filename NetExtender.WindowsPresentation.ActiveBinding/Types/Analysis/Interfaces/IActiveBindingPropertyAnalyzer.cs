// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Collections.Generic;

namespace NetExtender.WindowsPresentation.ActiveBinding.Interfaces
{
    public interface IActiveBindingPropertyAnalyzer
    {
        public IReadOnlySet<Char> UnknownDelimiters { get; }
        public IReadOnlySet<Char> KnownDelimiters { get; }
        public IReadOnlySet<Char> QuoteTerminals { get; }
        public IReadOnlySet<Char> Delimiters { get; }
        public IReadOnlySet<String> Keywords { get; }

        public IEnumerable<ActiveBindingToken> EnumerateTokens(String path);
        
    }
}