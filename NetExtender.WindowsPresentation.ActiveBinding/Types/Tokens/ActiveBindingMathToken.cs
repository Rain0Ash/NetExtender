// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;

namespace NetExtender.WindowsPresentation.ActiveBinding
{
    public class ActiveBindingMathToken : ActiveBindingToken
    {
        public String Member { get; }
        public override ActiveBindingPathTokenId Id { get; }

        public ActiveBindingMathToken(Int32 start, Int32 end, String member)
            : base(start, end)
        {
            Member = member ?? throw new ArgumentNullException(nameof(member));
            Id = new ActiveBindingPathTokenId(ActiveBindingPathTokenType.Math, String.Join(".", "Math", Member));
        }
    }
}