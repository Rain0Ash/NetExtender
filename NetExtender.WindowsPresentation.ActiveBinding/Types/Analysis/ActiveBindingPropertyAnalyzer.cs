// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Windows.Markup;

namespace NetExtender.WindowsPresentation.ActiveBinding
{
    public class ActiveBindingPropertyAnalyzer : ActiveBindingPropertyAnalyzerAbstraction
    {
        protected IXamlTypeResolver Resolver { get; }
        
        public ActiveBindingPropertyAnalyzer(IXamlTypeResolver resolver)
        {
            Resolver = resolver ?? throw new ArgumentNullException(nameof(resolver));
        }
        
        public ActiveBindingPropertyAnalyzer(IXamlTypeResolver resolver, IEnumerable<Char>? unknown, IEnumerable<Char>? known, IEnumerable<Char>? terminals, IEnumerable<String>? keywords)
            : base(unknown, known, terminals, keywords)
        {
            Resolver = resolver ?? throw new ArgumentNullException(nameof(resolver));
        }
        
        public override IEnumerable<ActiveBindingToken> EnumerateTokens(String path)
        {
            IEnumerable<ActiveBindingChunk> chunks = EnumerateChunks(path);
            return EnumerateTokens(chunks);
        }

        protected override IEnumerable<ActiveBindingChunk> EnumerateChunks(String value)
        {
            Int32 start = 0;
            Int32 position = 0;
            Boolean skip = false;
            Boolean chunk = false;
            Char terminal = '\'';
            Char character;
            List<ActiveBindingChunk> chunks = new List<ActiveBindingChunk>();

            do
            {
                character = value[position];

                if (skip)
                {
                    skip = character != terminal;
                    continue;
                }

                Boolean delimiter = UnknownDelimiters.Contains(character) || QuoteTerminals.Contains(character) || character == '\0';

                switch (chunk)
                {
                    case true when delimiter:
                        chunks.Add(new ActiveBindingChunk(start, position - 1, value.Substring(start, position - start)));
                        chunk = false;
                        continue;
                    case true:
                        continue;
                    case false when delimiter:
                        start = position;
                        chunk = true;
                        continue;
                }

                if (!QuoteTerminals.Contains(character))
                {
                    continue;
                }

                terminal = character;
                skip = true;
                
            } while (character != '\0' && position++ < value.Length);
            
            return chunks;
        }

        protected override Boolean GetPath(ActiveBindingChunk chunk, [MaybeNullWhen(false)] out ActiveBindingToken token)
        {
            if (chunk is null)
            {
                throw new ArgumentNullException(nameof(chunk));
            }

            String value = chunk.Value;

            if (Keywords.Contains(value))
            {
                token = default;
                return false;
            }
            
            String? relative = null;
            String[] split = value.Split('@');
            switch (split.Length)
            {
                case 1:
                    break;
                case 2 when split[0].Length <= 0 || split[1].Length <= 0:
                    throw new ActiveBindingException($"Wrong grammar on '{value}'");
                case 2:
                    value = split[0];
                    relative = split[1];
                    break;
                case > 2:
                    throw new ActiveBindingException($"Wrong grammar on '{value}'");
            }

            Int32 position = value.IndexOf(':');

            String[]? chain;
            if (position <= 0)
            {
                token = GetPropertyChain(value, out chain) ? GetPropertyOrMath(chunk, chain, relative) : null;
                return token is not null;
            }

            String left = value.Substring(0, position);

            if (!IsIdentifier(left))
            {
                token = default;
                return false;
            }

            if (!GetPropertyChain(value.Substring(position + 1, value.Length - position + 1), out chain))
            {
                token = default;
                return false;
            }

            token = chain.Length > 1 ? GetStaticPropertyOrEnum(chunk, left, chain) : null;
            return token is not null;
        }

        protected virtual Boolean GetPropertyChain(String value, [MaybeNullWhen(false)] out String[] chain)
        {
            if (value.Trim() == ".")
            {
                chain = new[] { "." };
                return true;
            }

            String[] properties = value.Split('.');
            if (properties.Length > 0 && properties.All(IsIdentifier))
            {
                chain = properties;
                return true;
            }

            chain = default;
            return false;
        }

        protected virtual ActiveBindingToken GetPropertyOrMath(ActiveBindingChunk chunk, IReadOnlyList<String> chain, String? relative)
        {
            ActiveBindingToken token = chain.Count == 2 && chain[0] == "Math"
                ? new ActiveBindingMathToken(chunk.Start, chunk.End, chain[1])
                : new ActiveBindingPropertyToken(chunk.Start, chunk.End, chain);

            token.Id.RelativeSource = relative;
            return token;
        }
        
        protected virtual ActiveBindingToken GetStaticPropertyOrEnum(ActiveBindingChunk chunk, String @namespace, IReadOnlyList<String> chain)
        {
            String @class = chain[0];
            String fullclass = $"{@namespace}:{@class}";
            
            Type? type = chain.Count == 2 ? TakeEnum(fullclass) : null;
            if (type is not null)
            {
                return new ActiveBindingEnumToken(chunk.Start, chunk.End, type, @namespace, chain[1]);
            }

            return new ActiveBindingStaticPropertyToken(chunk.Start, chunk.End, @namespace, @class, chain.Skip(1));
        }
        
        protected Type? TakeEnum(String typename)
        {
            if (typename is null)
            {
                throw new ArgumentNullException(nameof(typename));
            }

            Type? type = Resolver.Resolve(typename);
            return type is not null && type.IsEnum ? type : null;
        }
    }
}