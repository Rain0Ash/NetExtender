// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Collections.Generic;

namespace NetExtender.WindowsPresentation.ActiveBinding
{
    public class ActiveBindingStaticPropertyToken : ActiveBindingPropertyToken
    {
        public String Class { get; }
        public String Namespace { get; }
        public override ActiveBindingPathTokenId Id { get; }

        public ActiveBindingStaticPropertyToken(Int32 start, Int32 end, String @namespace, String @class, IEnumerable<String> properties)
            : base(start, end, properties)
        {
            Class = @class ?? throw new ArgumentNullException(nameof(@class));
            Namespace = @namespace ?? throw new ArgumentNullException(nameof(@namespace));
            Id = new ActiveBindingPathTokenId(ActiveBindingPathTokenType.StaticProperty, $"{Namespace}:{Class}.{String.Join(".", Properties)}");
        }
    }
}
