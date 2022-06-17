// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using NetExtender.Utilities.Types;
using NetExtender.WindowsPresentation.ActiveBinding.Interfaces;

namespace NetExtender.WindowsPresentation.ActiveBinding
{
    public abstract class ActiveBindingPropertyAnalyzerAbstraction : IActiveBindingPropertyAnalyzer
    {
        private static IReadOnlySet<Char> UnknownDelimitersCache { get; } = new HashSet<Char>
        {
            '(', ')', '+', '-', '*', '/', '%', '^', '&', '|', '?', '<', '>', '=', '!', ',', ' '
        };

        private static IReadOnlySet<Char> KnownDelimitersCache { get; } = new HashSet<Char>
        {
            '.', ':'
        };

        private static IReadOnlySet<Char> QuoteTerminalsCache { get; } = new HashSet<Char>
        {
            '\'', '"'
        };

        private static IReadOnlySet<String> KeywordsCache { get; } = new HashSet<String>
        {
            "null"
        };

        private static IReadOnlySet<Char> DelimitersCache { get; }

        static ActiveBindingPropertyAnalyzerAbstraction()
        {
            DelimitersCache = KnownDelimitersCache.Concat(UnknownDelimitersCache).Concat(QuoteTerminalsCache).ToHashSet();
        }

        public IReadOnlySet<Char> UnknownDelimiters { get; }
        public IReadOnlySet<Char> KnownDelimiters { get; }
        public IReadOnlySet<Char> QuoteTerminals { get; }
        public IReadOnlySet<Char> Delimiters { get; }
        public IReadOnlySet<String> Keywords { get; }

        protected ActiveBindingPropertyAnalyzerAbstraction()
            : this(null, null, null, null)
        {
        }

        protected ActiveBindingPropertyAnalyzerAbstraction(IEnumerable<Char>? unknown, IEnumerable<Char>? known, IEnumerable<Char>? terminals, IEnumerable<String?>? keywords)
        {
            Keywords = keywords?.WhereNotNullOrEmpty().ToHashSet() ?? KeywordsCache;

            if (unknown is null && known is null && terminals is null)
            {
                UnknownDelimiters = UnknownDelimitersCache;
                KnownDelimiters = KnownDelimitersCache;
                QuoteTerminals = QuoteTerminalsCache;
                Delimiters = DelimitersCache;
                return;
            }

            UnknownDelimiters = unknown?.ToHashSet() ?? UnknownDelimitersCache;
            KnownDelimiters = known?.ToHashSet() ?? KnownDelimitersCache;
            QuoteTerminals = terminals?.ToHashSet() ?? QuoteTerminalsCache;
            Delimiters = KnownDelimiters.Concat(UnknownDelimiters).Concat(QuoteTerminals).ToHashSet();
        }

        public virtual IEnumerable<ActiveBindingToken> EnumerateTokens(String path)
        {
            return EnumerateTokens(EnumerateChunks(path));
        }
        
        protected IEnumerable<ActiveBindingToken> EnumerateTokens(IEnumerable<ActiveBindingChunk> source)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            return source.TryParse<ActiveBindingChunk, ActiveBindingToken>(GetPath);
        }

        protected abstract Boolean GetPath(ActiveBindingChunk chunk, [MaybeNullWhen(false)] out ActiveBindingToken token);

        protected abstract IEnumerable<ActiveBindingChunk> EnumerateChunks(String value);

        protected virtual Boolean IsIdentifier(String value)
        {
            if (value.Length <= 0)
            {
                return false;
            }

            Char character = value[0];

            if (Char.IsDigit(character) || Delimiters.Contains(character))
            {
                return false;
            }

            for (Int32 i = 1; i < value.Length; i++)
            {
                if (Delimiters.Contains(value[i]))
                {
                    return false;
                }
            }

            return true;
        }
        
        protected class ActiveBindingChunk
        {
            public Int32 Start { get; }
            public Int32 End { get; }
            public String Value { get; }

            public ActiveBindingChunk(Int32 start, Int32 end, String value)
            {
                Value = value ?? throw new ArgumentNullException(nameof(value));
                Start = start;
                End = end;
            }
        }
    }
}