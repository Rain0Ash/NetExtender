// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using Microsoft.CodeAnalysis;
using NetExtender.StrongId.Attributes;

namespace NetExtender.StrongId.Diagnostics
{
    internal static class NotPartialDiagnostic
    {
        private const String Id = $"{nameof(NetExtender)}.{nameof(StrongId)}.1";
        private const String Title = "Must be partial";
        private const String Message = $"The target of the {nameof(StrongIdAttribute)} must be declared as partial.";

        public static Diagnostic Create(SyntaxNode node)
        {
            DiagnosticDescriptor descriptor = new DiagnosticDescriptor(Id, Title, Message, StrongIdUtilities.Categories.Usage, DiagnosticSeverity.Error, true);
            return Diagnostic.Create(descriptor, node.GetLocation());
        }
    }
}