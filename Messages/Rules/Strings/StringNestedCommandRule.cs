// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;

namespace NetExtender.Messages.Rules.Strings
{
    public class StringNestedCommandRule : NestedCommandRule<String>
    {
        public StringNestedCommandRule(String id, ReaderHandler<String> handler = null)
            : base(id, handler)
        {
        }
    }
}