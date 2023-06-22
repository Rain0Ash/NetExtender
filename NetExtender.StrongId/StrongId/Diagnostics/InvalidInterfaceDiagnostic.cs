// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using Microsoft.CodeAnalysis;

namespace NetExtender.StrongId.Diagnostics
{
    internal static class InvalidInterfaceDiagnostic
    {
        private const String Id = $"{nameof(NetExtender)}.{nameof(StrongId)}.5";
        private const String Title = "Invalid interfaces value";
        private const String Message = $"The {nameof(StrongIdInterfaceType)} value provided is not a valid combination of flags.";

        public static Diagnostic Create(SyntaxNode node)
        {
            DiagnosticDescriptor descriptor = new DiagnosticDescriptor(Id, Title, Message, StrongIdUtilities.Categories.Usage, DiagnosticSeverity.Error, true);
            return Diagnostic.Create(descriptor, node.GetLocation());
        }
    }
}