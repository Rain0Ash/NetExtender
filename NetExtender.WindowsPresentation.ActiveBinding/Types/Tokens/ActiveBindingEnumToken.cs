// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;

namespace NetExtender.WindowsPresentation.ActiveBinding
{
    public class ActiveBindingEnumToken : ActiveBindingToken
    {
        public Type Enum { get; }
        public String Member { get; }
        public String Namespace { get; }
        public override ActiveBindingPathTokenId Id { get; }

        public ActiveBindingEnumToken(Int32 start, Int32 end, Type @enum, String @namespace, String member)
            : base(start, end)
        {
            Enum = @enum ?? throw new ArgumentNullException(nameof(@enum));
            Namespace = @namespace ?? throw new ArgumentNullException(nameof(@namespace));
            Member = member ?? throw new ArgumentNullException(nameof(member));
            Id = new ActiveBindingPathTokenId(ActiveBindingPathTokenType.Enum, $"{Namespace}:{@enum.Name}.{Member}");
        }
    }
}
