// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;

namespace NetExtender.Messages.Rules.Strings
{
    public class StringCommandRule : CommandRule<String>
    {
        public StringCommandRule(String id, ReaderHandler<String> handler = null)
            : base(id, handler)
        {
        }
    }
}