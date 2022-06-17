// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Markup;

namespace NetExtender.WindowsPresentation.ActiveBinding
{
    public abstract class ActiveBindingAbstraction : MarkupExtension
    {
        protected static Regex RelativeSourceRegex { get; } = new Regex(@"(PreviousData|TemplatedParent|Self)|((FindAncestor\.)?(\[(\d+)\]\.)?(.+))", RegexOptions.Compiled);
        
        public String? Path { get; set; }
        public Boolean FalseIsCollapsed { get; set; } = true;
        public Boolean SingleQuotes { get; set; } = false;
        public Object FallbackValue { get; set; } = DependencyProperty.UnsetValue;
        protected virtual IDictionary<String, String> ReplaceDictionary { get; } = new Dictionary<String, String>(16)
        {
            { " and ", " && " },
            { ")and ", ")&& " },
            { " and(", " &&(" },
            { ")and(", ")&&(" },

            { " or ", " || " },
            { ")or ", ")|| " },
            { " or(", " ||(" },
            { ")or(", ")||(" },

            { " less ", " < " },
            { ")less ", ")< " },
            { " less(", " <(" },
            { ")less(", ")<(" },

            { " less=", " <=" },
            { ")less=", ")<=" },

            { "not ", "!" }
        };

        protected ActiveBindingAbstraction(String? path)
        {
            Path = path;
        }
        
        protected virtual String GetEnumName(Int32 i)
        {
            return $"Enum{++i}";
        }

        protected virtual List<PathAppearance> GetSourcePath(String path, IXamlTypeResolver resolver)
        {
            if (path is null)
            {
                throw new ArgumentNullException(nameof(path));
            }

            if (resolver is null)
            {
                throw new ArgumentNullException(nameof(resolver));
            }

            ActiveBindingPropertyAnalyzer analyzer = new ActiveBindingPropertyAnalyzer(resolver);
            return analyzer.EnumerateTokens(path)
                .GroupBy(token => token.Id)
                .Select(group => new PathAppearance(group.Key, group))
                .ToList();
        }

        [return: NotNullIfNotNull("path")]
        protected virtual String? NormalizePath(String? path)
        {
            if (path is null)
            {
                return null;
            }
            
            return ReplaceDictionary
                .Append(SingleQuotes ? new KeyValuePair<String, String>("\"", "\'") : new KeyValuePair<String, String>("\'", "\""))
                .Aggregate(path, (current, pair) => current.Replace(pair.Key, pair.Value));
        }
        
        protected class PathAppearance
        {
            public ActiveBindingPathTokenId Id { get; }
            public IEnumerable<ActiveBindingToken> Tokens { get; }

            public PathAppearance(ActiveBindingPathTokenId id, IEnumerable<ActiveBindingToken> tokens)
            {
                Id = id ?? throw new ArgumentNullException(nameof(id));
                Tokens = tokens is not null ? tokens.ToList() : throw new ArgumentNullException(nameof(tokens));
            }
        }
    }
}